using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Password_Manager_Server
{
    /// <summary>
    /// Responsible for accepting and handling client connections.
    /// </summary>
    public class Server
    {
        private readonly TcpListener tcpListener;
        private readonly List<ClientSession> clientList;
        private readonly Thread messageThread;

        private readonly Object clientListLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="Server" /> class.
        /// </summary>
        public Server()
        {
            this.clientList = new List<ClientSession>();
            this.tcpListener = new TcpListener(IPAddress.Loopback, 12086);
            this.messageThread = new Thread(new ParameterizedThreadStart(Server.HandleClientMessages));
        }

        /// <summary>
        /// Start accepting clients.
        /// </summary>
        public async Task Start()
        {
            this.tcpListener.Start();
            this.messageThread.Start(this);

            while (true)
            {
                var tcpClient = await this.tcpListener.AcceptTcpClientAsync();

                var ipAddress = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
                Debug.WriteLine($"New client joined: {ipAddress.Address.ToString()}");

                lock (this.clientListLock)
                {
                    this.clientList.Add(new ClientSession(tcpClient));
                }
            }
        }

        private static void HandleClientMessages(object serverObj)
        {
            Server server = serverObj as Server;

            List<Task<int>> pendingReadingTasks = new List<Task<int>>();
            byte[] buffer = new byte[4096];
            while (true)
            {
                // Go through each client and check to see if there are any data to be read in
                lock (server.clientListLock)
                {
                    foreach (var client in server.clientList)
                    {
                        var networkStream = client.Client.GetStream();
                        if (networkStream.DataAvailable)
                        {
                            pendingReadingTasks.Add(networkStream.ReadAsync(buffer, 0, buffer.Length));
                        }
                    }
                }

                // Start processing the pending reading tasks
                while (pendingReadingTasks.Count > 0)
                {
                    var task = Task.WhenAny(pendingReadingTasks).GetAwaiter().GetResult();
                    pendingReadingTasks.Remove(task);

                    // "Process" the message
                    Debug.WriteLine($"We received bytes: {task.Result.ToString()}");
                }
            }
        }
    }
}