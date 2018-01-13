using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;
using System.Data.SQLite;
using System.Net;
using Networking.Request;
using Networking.Response;

namespace Password_Manager_Server
{
    class Server
    {
        enum MessageType {Login=1, ApplicationsRequest }

        public void  start(SQLiteConnection conn)
        {
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"),12086);
            clients = new List<ClientSession>();
            querier = new DatabaseQuerier(conn);
            sender = new Networking.MessageSender();
            listener.Start();
            Thread thread = new Thread(handleClients);
            thread.Start();
            while (true)
            {
                ClientSession client = new ClientSession(listener.AcceptTcpClient(),"");
                lock(clientLock)
                {
                    Console.WriteLine("A Client is trying to connect from {0}", 
                        ((IPEndPoint)(client.client.Client.RemoteEndPoint)).Address.ToString());
                    clients.Add(client);
                }            
            }
        }

        private void handleClients()
        {
            while (true)
            {
                lock(clientLock)
                {

                    foreach (var connection in clients)
                    {
                        handleMessage(connection);
                    }

                }
            }
        }

        private void handleMessage(ClientSession session) 
        {            
            //TODO: handle error the getstream method throws.
            // stream.length will not work for networkstream have to change that RIP
            var stream = session.client.GetStream();
            if (!stream.DataAvailable)
            {
                return;
            }
            var msgHeader = getHeader(stream);
            var messageID = msgHeader.messageID;
            sender.socket = session.client.Client;
            byte[] result = new byte[msgHeader.messageSize];
            if (msgHeader.messageSize > 0)
            {
                using (stream)
                {
                    int bytesRead = 0;
                    do
                    {
                        bytesRead += stream.Read(result, 0, result.Length);
                    }
                    while (bytesRead != msgHeader.messageSize);
                }
            }
            Console.Write("WE have read in the entire message! for {0}", messageID);
            var memoryStream = new MemoryStream(result);
            if(messageID == (int)MessageType.Login)
            {
                LoginRequest msg = new BinaryFormatter().Deserialize(memoryStream) as LoginRequest;
                msg.header = msgHeader;
                session.user = msg.username.name;
                bool correctUser = true;
                if(!correctUser)
                {
                        Console.Write("Removing Client {0}", 
                            IPAddress.Parse(((IPEndPoint)session.client.Client.RemoteEndPoint).Address.ToString()));
                        clients.Remove(session);
                        stream.Close();
                    return;
                }
                Console.Write(msg.username.name + msg.username.password);
                //TODO: use db querier to ensure username and password checkout then make response
                // then remove client from list of clients
            }
            else if(messageID == (int)MessageType.ApplicationsRequest)
            {
                ApplicationsRequest msg = new BinaryFormatter().Deserialize(memoryStream) as ApplicationsRequest;
                msg.header = msgHeader;
                var applications = querier.getApplications(session.user);
                var response = new ApplicationsResponse(applications);
                sender.send(response);
            }
            
        }

        /**
         * @brief
         *      deserializes the network stream to a msg header so the
         *      function handle messages knows what message is coming
         */      
        private Networking.MessageHeader getHeader(NetworkStream stream)
        {
            byte[] b_headerSize = new byte[5];
            stream.Read(b_headerSize, 0, b_headerSize.Length);
            var headerSize = BitConverter.ToInt32(b_headerSize,0);
            byte[] b_header = new byte[headerSize];
            int byteCount = 0;
            while (byteCount < headerSize) //TODO: INSERT TIMEOUT
            {     
                do
                {
                    byteCount +=stream.Read(b_header, 0, headerSize-byteCount);
                    if(byteCount == headerSize)
                    {
                        break;
                    }
                }
                while (stream.DataAvailable);
            }
            MemoryStream memStream = new MemoryStream(b_header); 
            var formatter = new BinaryFormatter();
            //ERROR in deserializing
            Networking.MessageHeader header = (Networking.MessageHeader)(formatter.Deserialize(memStream));
            return header;
        }

        TcpListener listener;
        List<ClientSession> clients;
        private Object clientLock = new Object();
        DatabaseQuerier querier;
        Networking.MessageSender sender;
    }
}
