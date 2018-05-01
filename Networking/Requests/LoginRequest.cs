using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Requests
{   
    [Serializable]
    public class LoginRequest : MessageBase
    {
        public const int MessageID = 0;

        public LoginRequest(Shared.Username user) :
            base(MessageID, MessageType.Request)
        {
            username = user;
        }
        /**
         * @brief
         *      The message contains a Username
         *      this will be used on server side to ensure
         *      the client can log in cause he has the correct info
         *      server should implement this with matching hashs
         */
        public Shared.Username username;
    }
}
