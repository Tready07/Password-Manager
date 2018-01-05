using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{   
    [Serializable]
    public class LoginMessage : MessageBase
    {
        public LoginMessage(Shared.Username user)
        {
            username = user;
            header = new MessageHeader(1);
        }
        /**
         * @brief
         *      The message contains a Username
         *      this will be used on server side to ensure
         *      the client can log in cause he has the correct info
         *      server should implement this with matching hashs
         */
        Shared.Username username;
    }
}
