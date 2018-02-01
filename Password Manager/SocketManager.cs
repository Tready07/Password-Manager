using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Networking;
using System.Diagnostics;
using System.IO;

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
        public void connect(String host, int port)
        {   
            socket.Connect(host, port);

            // Spawn a new Task for reading messages received from the server once we're connected.
            Task.Factory.StartNew(() =>
            {
                var networkStream = new NetworkStream(this.socket);
                while (true)
                {
                    if (!this.socket.Poll(Timeout, SelectMode.SelectRead))
                    {
                        // Nothing to read, try again later.
                        continue;
                    }

                    // Have we disconnected from the server?
                    if (this.socket.Available == 0)
                    {
                        Trace.WriteLine("Server has disconnected from client.", "Client");
                        this.socket.Shutdown(SocketShutdown.Both);
                        this.socket.Disconnect(true);
                        this.socket.Dispose();

                        this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        break;
                    }

                    // Read incoming message
                    var buffer = new byte[4096];
                    int actualBytesRead = networkStream.Read(buffer, 0, buffer.Length);
                    try
                    {
                        using (var stream = new MemoryStream(buffer))
                        {
                            var messageHeader = MessageUtils.DeserializeMessageHeader(stream);

                            Debug.WriteLine("Message ID: " + messageHeader.ID.ToString(), "Client");
                            Debug.WriteLine("Message Type: " + messageHeader.Type.ToString(), "Client");
                            Debug.WriteLine("Message Size: " + messageHeader.Size.ToString(), "Client");

                            if (actualBytesRead - MessageHeader.HeaderSize < messageHeader.Size)
                            {
                                // There's more data that needs to be received from the server, so
                                // don't process this message yet.

                                // TODO: Implement this properly.
                                Debug.WriteLine("Not enough data was sent from the server to complete this message.", "Client");
                                Debug.WriteLine("Skipping...");
                                continue;
                            }

                            Debug.WriteLine("Processing message...", "Client");
                        }
                    }
                    catch (BadHeaderException ex)
                    {
                        Trace.WriteLine("Bad message header was received from server: " + ex, "Client");
                    }
                }
            });
        }

        protected SocketManager()
        {
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
    }
}
