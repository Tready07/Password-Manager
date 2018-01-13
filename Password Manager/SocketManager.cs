using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Networking;

namespace Password_Manager
{
    public class SocketManager
    {
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
        }
        protected SocketManager() { }

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
