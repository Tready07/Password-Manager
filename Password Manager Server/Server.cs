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

namespace Password_Manager_Server
{
    class Server
    {
        enum MessageType {Login=1, ApplicationsRequest }

        public void  start(SQLiteConnection conn)
        {
            listener = new TcpListener(12086);
            listener.Start();
            clients = new List<ClientSession>();
            querier = new DatabaseQuerier(conn);
            Thread thread = new Thread(handleClients);
            while (true)
            {
                ClientSession client = new ClientSession(listener.AcceptTcpClient(),"");
                lock(clientLock)
                {
                    clients.Add(client);
                }            
            }
        }
                
        private void handleClients()
        {
            while(true)
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

        private async void handleMessage(ClientSession session) 
        {
            
            var stream = session.client.GetStream();
            if(stream.Length == 0)
            {
                return;
            }
            var msgHeader = getHeader(stream);
            var messageID = msgHeader.messageID;
            if(msgHeader.MAGIC_NUMBER != "asdf1231")
            {
                return;
            }
            sender.socket = session.client.Client;
            byte[] result;
            using (stream)
            {
               result = new byte[stream.Length];
               await stream.ReadAsync(result, 0, (int)stream.Length);
            }
            var memoryStream = new MemoryStream(result);
            if(messageID == (int)MessageType.Login)
            {
                Networking.LoginMessage msg = new BinaryFormatter().Deserialize(memoryStream) as Networking.LoginMessage;
                msg.header = msgHeader;
                session.user = msg.username.name;
                //TODO: use db querier to ensure username and password checkout then make response
                // then add client to list of clients
            }
            else if(messageID == (int)MessageType.ApplicationsRequest)
            {
                Networking.ApplicationsRequest msg = new BinaryFormatter().Deserialize(memoryStream) as Networking.ApplicationsRequest;
                msg.header = msgHeader;
                var applications = querier.getApplications(session.user);
                var response = new Networking.ApplicationsResponse(applications);
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
            Networking.MessageHeader header = new BinaryFormatter().Deserialize(stream) as Networking.MessageHeader;
            return header;
        }

        TcpListener listener;
        List<ClientSession> clients;
        private Object clientLock = new Object();
        DatabaseQuerier querier;
        Networking.MessageSender sender;
    }
}
