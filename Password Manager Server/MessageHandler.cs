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
        Func<byte [],ClientSession, bool>[] functions = {handleLogin};
        public MessageHandler()
        {

        }

        public bool handleMessage(byte [] message, ClientSession session)
        {
            bool isComplete = false;
            MessageDeserializer ds = new MessageDeserializer(message);
            int id = ds.getID();
            isComplete = functions[id](message,session);
            return isComplete;
        }

        private static bool handleLogin(byte [] message,ClientSession session)
        {
            MessageDeserializer ds = new MessageDeserializer(message);
            LoginRequest msg =(LoginRequest) ds.getMessage();
            Console.WriteLine(msg.username.name);
            Console.WriteLine(msg.username.password);
            var con = databaseInitializer.makeConnection();
            DatabaseQuerier db = new DatabaseQuerier(con);
            if(db.checkLoginInfo(msg.username.name,msg.username.password))
            {
                Console.Write("The info is True!");
                session.loginUsername.name = msg.username.name;
                ApplicationsResponse resp = new ApplicationsResponse(db.getApplications(msg.username.name));
                byte[] payLoad = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                session.Client.Client.Send(payLoad);
                return true;
            }
            return false;
        }

        private static bool handleNewApp(byte [] message, ClientSession session)
        {
            //TODO: handleNewApp
            return false;
        }

        
    }
}
