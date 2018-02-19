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
        Func<byte [],ClientSession, bool>[] functions = {handleLogin,handleApplications,handleNewApp};
        public MessageHandler()
        {

        }

        public bool handleMessage(byte [] message, ClientSession session, MessageHeader header)
        {
            bool isComplete = false;
            MessageDeserializer ds = new MessageDeserializer(message);
            int id = header.ID;
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

        private static bool handleApplications(byte [] message,ClientSession session)
        {
            //TODO: send all of the users apps to him or her or it:D
            return false;
        }

        private static bool handleNewApp(byte [] message, ClientSession session)
        {
            MessageDeserializer ds = new MessageDeserializer(message);
            NewAppRequest request = (NewAppRequest)ds.getMessage();
            Console.WriteLine(request.application.Usernames[0].name);
            Console.WriteLine(request.application.Usernames[0].password);
            var con = databaseInitializer.makeConnection();
            DatabaseQuerier db = new DatabaseQuerier(con);
            db.addUsername(request.application, session.loginUsername.name);
            //TODO: send back the application so the client can update the tree.
            return true;
        }

        
    }
}
