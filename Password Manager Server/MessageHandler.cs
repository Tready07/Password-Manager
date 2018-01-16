using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;
using Networking.Request;

namespace Password_Manager_Server
{
    public class MessageHandler
    {
        Func<byte [], bool>[] functions = {handleLogin};
        public MessageHandler()
        {

        }

        public bool handleMessage(byte [] message)
        {
            bool isComplete = false;
            MessageDeserializer ds = new MessageDeserializer(message);
            int id = ds.getID();
            isComplete = functions[id](message);
            return isComplete;
        }

        private static bool handleLogin(byte [] message)
        {
            // Implement Deserializer class
            MessageDeserializer ds = new MessageDeserializer(message);
            LoginRequest msg =(LoginRequest) ds.getMessage();
            var con = databaseInitializer.makeConnection();
            DatabaseQuerier db = new DatabaseQuerier(con);
            db.checkLoginInfo(msg.username.name,msg.username.password);
            return true;
        }
        
    }
}
