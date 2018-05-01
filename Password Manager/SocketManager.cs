using System;
using System.Threading.Tasks;
using System.Net.Sockets;
using Networking;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Networking.Responses;

namespace Password_Manager
{
    public class SocketManager
    {
        private const int Timeout = 500;

        private readonly MessageReader messageReader = new MessageReader();

        private static SocketManager instance;
        public static SocketManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SocketManager();
                }
                return instance;
            }
        }

        public Socket socket { get; private set; } = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        /// <summary>
        /// Occurs when the client has been disconnected from the server.
        /// </summary>
        public event EventHandler Disconnected;

        public void connect(String host, int port)
        {   
            socket.Connect(host, port);
        }

        public bool IsConnected()
        {
            return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
        }

        /// <summary>
        /// Disconnect from the server.
        /// </summary>
        public void Disconnect()
        {
            this.Disconnected?.Invoke(this, EventArgs.Empty);

            this.socket.Shutdown(SocketShutdown.Both);
            this.socket.Disconnect(true);
            this.socket.Dispose();

            // Note that the .NET Framework does not like it when you connect to a socket that's
            // already been disconnected (unless it's a different endpoint), so initialize a new
            // Socket.
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        protected SocketManager()
        {
            this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);
        }

        /// <summary>
        /// Sends a message to the server and returns the response received.
        /// </summary>
        /// <typeparam name="Response">The type of response. This should be a ISerializable.</typeparam>
        /// <param name="message">The request to send.</param>
        /// <returns>The response or <c>null</c> if the message couldn't be fetched because the server disconnected.</returns>
        public async Task<TResponse> SendRequest<TResponse>(MessageBase request) where TResponse : MessageBase
        {
            try
            {
                this.socket.Send(await MessageUtils.SerializeMessage(request));

                while (true)
                {
                    if (!this.socket.Poll(Timeout, SelectMode.SelectRead))
                    {
                        continue;
                    }

                    // There's no data to read, so that must mean that the server disconnected.
                    if (this.socket.Available == 0)
                    {
                        this.Disconnect();
                        return null;
                    }

                    using (var stream = new NetworkStream(this.socket))
                    {
                        await this.messageReader.Process(stream);
                    }

                    if (this.messageReader.IsMessageReady)
                    {
                        this.messageReader.ProcessNextMessage();
                        return (TResponse)this.messageReader.GetMessage();
                    }
                }
            }
            catch (SocketException ex) when (ex.ErrorCode == 10054)
            {
                // The server disconnected on us, return null.
                this.Disconnect();
                return null;
            }
            catch (BadHeaderException)
            {
                // The header is malformed, return null.
                this.Disconnect();
                return null;
            }
        }
    }
}
