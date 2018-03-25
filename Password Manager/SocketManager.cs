using System;
using System.Threading.Tasks;
using System.Net.Sockets;
using Networking;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace Password_Manager
{
    public class SocketManager
    {
        private const int Timeout = 500;

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
        /// Sends a message to the server.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>The asynchronous operation that represents the message being sent.</returns>
        public async Task SendMessage(MessageBase message)
        {
            this.socket.Send(await MessageUtils.SerializeMessage(message));
        }

        /// <summary>
        /// Sends a message to the server and returns the response received.
        /// </summary>
        /// <typeparam name="Response">The type of response. This should be a ISerializable.</typeparam>
        /// <param name="message">The request to send.</param>
        /// <returns>The response or <c>null</c> if the message couldn't be fetched because the server disconnected.</returns>
        public async Task<TResponse> SendRequest<TResponse>(MessageBase request) where TResponse : class
        {
            this.socket.Send(await MessageUtils.SerializeMessage(request));

            // Read the message header
            byte[] buffer = new byte[MessageHeader.HeaderSize];
            int bytesReceived = await this.socket.ReceiveAsync(buffer);
            if (bytesReceived == 0)
            {
                // We got disconnected from the server, return null.
                return null;
            }
            else if (bytesReceived != buffer.Length)
            {
                // The message header couldn't be read.
                throw new BadHeaderException("Unable to read the header from the socket");
            }

            int messageID = 0;
            int messageSize = 0;
            using (var memoryStream = new MemoryStream(buffer))
            {
                var messageHeader = MessageUtils.DeserializeMessageHeader(memoryStream);
                messageSize = messageHeader.Size;
                messageID = messageHeader.ID;
            }

            int totalSizeRead = 0;
            byte[] messageData = new byte[messageSize];
            int bufferSize = Math.Min(messageSize, 4096);
            do
            {
                bytesReceived = await this.socket.ReceiveAsync(messageData, totalSizeRead, bufferSize, SocketFlags.None);
                bufferSize = Math.Min(messageSize - totalSizeRead, 4096);
                totalSizeRead += bytesReceived;
            }
            while (totalSizeRead < messageSize);

            // Deserialize the message, and return it.
            using (var memoryStream = new MemoryStream(messageData))
            {
                var formatter = new BinaryFormatter();
                return (TResponse)formatter.Deserialize(memoryStream);
            }
        }
    }
}
