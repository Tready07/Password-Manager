using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private readonly CancellationTokenSource cancellationToken;

        private readonly Object clientListLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="Server" /> class.
        /// </summary>
        public Server()
        {
            this.clientList = new List<ClientSession>();
            this.tcpListener = new TcpListener(IPAddress.Loopback, 12086);
            this.cancellationToken = new CancellationTokenSource();
        }

        /// <summary>
        /// Start accepting clients.
        /// </summary>
        public void Start()
        {
            this.tcpListener.Start();

            var acceptClientTask = Task.Factory.StartNew(async () =>
            {
                while (!this.cancellationToken.IsCancellationRequested)
                {
                    var tcpClient = await this.tcpListener.AcceptTcpClientAsync();

                    // Print some information about the client that joined
                    var ipAddress = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
                    Debug.WriteLine($"New client joined: {ipAddress.Address.ToString()}", "Server");

                    lock (this.clientListLock)
                    {
                        this.clientList.Add(new ClientSession(tcpClient));
                    }
                }

                Debug.WriteLine("Server is no longer accepting clients.");
            }, this.cancellationToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            var handleClientMessages = Task.Factory.StartNew(() =>
            {
                const int Timeout = 100;

                List<ReadyClient> pendingReadingTasks = new List<ReadyClient>();
                byte[] buffer = new byte[4096];
                MessageHandler handler = new MessageHandler();
                while (!this.cancellationToken.IsCancellationRequested)
                {
                    if (this.clientList.Count == 0)
                    {
                        // Put the thread to sleep for a few milliseconds if we don't have any clients
                        // to process.
                        Thread.Sleep(Timeout);
                        continue;
                    }

                    // Go through each client and check to see if there are any data to be read in
                    // by calling Socket.Select.
                    System.Collections.IList clientToRead = null;
                    lock (this.clientListLock)
                    {
                        clientToRead = this.clientList.Select(cs => cs.Client.Client).ToList();
                    }
                    
                    // Socket.Select will modify the clientToRead list so that it contains only
                    // the clients that are ready to read. Note that we set the timeout to 100ms so
                    // that it'll wait 100ms on each socket to see if it's available for reading
                    // before moving on to the next one (don't do it indefinitely, in case a new
                    // client joins).
                    Socket.Select(clientToRead, null, null, Timeout);
                    foreach (Socket client in clientToRead)
                    {
                        var networkStream = new NetworkStream(client);

                        // Is there any data available to be read? If not, then that means the
                        // socket has been disconnected.
                        if (client.Available > 0)
                        {
                            pendingReadingTasks.Add(new ReadyClient(networkStream.ReadAsync(buffer, 0, buffer.Length),client));
                        }
                        else
                        {
                            var ipAddress = (IPEndPoint)client.RemoteEndPoint;
                            Debug.WriteLine($"Client disconnected: {ipAddress.Address}", "Server");

                            lock (this.clientListLock)
                            {
                                this.clientList.Remove(this.clientList.Single(cs => cs.Client.Client == client));
                            }
                        }
                    }

                    
                    // Start processing the pending reading tasks   
                    int i = pendingReadingTasks.Count - 1;
                    while (pendingReadingTasks.Count > 0)
                    {
                        var currentReadyClient = pendingReadingTasks[i].task.GetAwaiter().GetResult();
                        // "Process" the message
                        Debug.WriteLine($"We received bytes: { pendingReadingTasks[i].task.Result.ToString()}", "Server");
                        handler.handleMessage(buffer,pendingReadingTasks[i].socket);
                        pendingReadingTasks.Remove(pendingReadingTasks[i]);
                        i--;
                    }


                }

                Debug.WriteLine("Server is no longer handling clients.");
            }, this.cancellationToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            var tasksToWait = new Task[] { acceptClientTask, handleClientMessages };
            Task.WaitAll(tasksToWait);

            Debug.WriteLine("The server has been stopped.");
        }

        /// <summary>
        /// Stops the server.
        /// </summary>
        public void Stop()
        {
            this.cancellationToken.Cancel();
        }
    }
}