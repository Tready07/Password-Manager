using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Networking;
using Networking.Requests;

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
            this.tcpListener = new TcpListener(IPAddress.Any, 12086);
            this.cancellationToken = new CancellationTokenSource();
        }

        /// <summary>
        /// Gets the <see cref="IPEndPoint" /> that the server is listening on.
        /// </summary>
        public IPEndPoint Address
        {
            get
            {
                return (IPEndPoint)this.tcpListener.LocalEndpoint;
            }
        }

        /// <summary>
        /// Start accepting clients.
        /// </summary>
        public void Start()
        {
            this.tcpListener.Start();

            var handleClient = this.HandleNewClient(this.cancellationToken.Token);
            var handleMessages = this.HandleNewMessages(this.cancellationToken.Token);
            var tasks = new Task[] { handleClient, handleMessages };

            Task.WaitAll(tasks);

            Debug.WriteLine("The server has been stopped.");
        }

        /// <summary>
        /// Stops the server.
        /// </summary>
        public void Stop()
        {
            this.tcpListener.Stop();
            this.cancellationToken.Cancel();
        }

        private ClientSession FindClientBySocket(Socket socket)
        {
            return this.clientList.SingleOrDefault(cs => cs.Client.Client == socket);
        }

        private async Task HandleNewClient(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    var tcpClient = await this.tcpListener.AcceptTcpClientAsync();

                    // Print some information about the client that joined
                    var ipAddress = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
                    Debug.WriteLine($"{ipAddress.Address}: Client Joined", "Server");

                    lock (this.clientListLock)
                    {
                        this.clientList.Add(new ClientSession(tcpClient));
                    }
                }
            }
            catch (ObjectDisposedException ex) when (ex.ObjectName == "System.Net.Sockets.Socket")
            {
                // This exception gets thrown by TcpListener.AcceptTcpClientAsync when
                // TcpListener.Stop is called.
                //
                // TODO: Is there a better way to properly cancel TcpListener.AcceptTcpClientAsync?
                Debug.WriteLine("Server is no longer accepting clients.");
            }
        }

        private async Task HandleNewMessages(CancellationToken token)
        {
            const int Timeout = 100;

            MessageHandler handler = new MessageHandler();
            while (!token.IsCancellationRequested)
            {
                if (this.clientList.Count == 0)
                {
                    // Put the thread to sleep for a few milliseconds if we don't have any clients
                    // to process.
                    await Task.Delay(Timeout);
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
                    var clientSession = this.FindClientBySocket(client);
                    var ipAddress = (IPEndPoint)client.RemoteEndPoint;
                    var reader = clientSession.Reader;

                    if (client.Available == 0)
                    { 
                        // The client has disconnected. Move on to the next client.
                        Trace.WriteLine($"{ipAddress.Address}: Client Disconnected", "Server");
                        this.RemoveClient(clientSession);
                        continue;
                    }

                    // Process the message
                    await reader.Process(networkStream);
                    if (!reader.IsMessageReady)
                    {
                        continue;
                    }

                    var header = reader.Header.Value;

                    Trace.WriteLine($"{ipAddress.Address}: Message ID {header.ID} Received ({header.Size} byte(s))", "Server");

                    bool succeeded = handler.handleMessage(clientSession);
                    if (!succeeded && header.ID == LoginRequest.MessageID)
                    {
                        // The login failed, so force a disconnect.
                        this.RemoveClient(clientSession);
                        continue;
                    }

                    reader.ProcessNextMessage();
                }
            }

            Debug.WriteLine("Server is no longer reading messages");
        }

        private void RemoveClient(ClientSession client)
        {
            client.Client.Close();
            lock (this.clientListLock)
            {
                this.clientList.Remove(client);
            }
        }
    }
}