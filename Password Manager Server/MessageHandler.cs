using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;
using Networking.Request;
using Networking.Response;
using System.Net.Sockets;

namespace Password_Manager_Server
{
    public class MessageHandler
    {
        Func<byte [],Socket, bool>[] functions = {handleLogin};
        public MessageHandler()
        {

        }

        public bool handleMessage(byte [] message, Socket socket)
        {
            bool isComplete = false;
            MessageDeserializer ds = new MessageDeserializer(message);
            int id = ds.getID();
            isComplete = functions[id](message,socket);
            return isComplete;
        }

        private static bool handleLogin(byte [] message,Socket socket)
        {
            // Implement Deserializer class
            MessageDeserializer ds = new MessageDeserializer(message);
            LoginRequest msg =(LoginRequest) ds.getMessage();
            var con = databaseInitializer.makeConnection();
            DatabaseQuerier db = new DatabaseQuerier(con);
            db.checkLoginInfo(msg.username.name,msg.username.password);
            ApplicationsResponse resp = new ApplicationsResponse(db.getApplications(msg.username.name));
            byte [] payLoad = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
            socket.Send(payLoad);
            return true;
        }

        
    }
}
