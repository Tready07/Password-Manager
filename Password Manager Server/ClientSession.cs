using System;
using System.Net.Sockets;
using System.Threading;
using Shared;

namespace Password_Manager_Server
{
    /// <summary>
    /// Encapsulates information about a client.
    /// </summary>
    public class ClientSession
    {
        private readonly TcpClient tcpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientSession" /> class.
        /// </summary>
        /// <param name="tcpClient">The <see cref="TcpClient" /> that contains the client connection.</param>
        public ClientSession(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            loginUsername = new Username();
        }
        public Username loginUsername { get; set; }

        /// <summary>
        /// Gets the <see cref="TcpClient" /> for this client connection.
        /// </summary>
        public TcpClient Client => this.tcpClient;
    }
}
