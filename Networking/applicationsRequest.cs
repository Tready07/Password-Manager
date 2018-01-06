using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    [Serializable]
    public class ApplicationsRequest : MessageBase
    {   
        /**
         * @brief
         *      The ApplicationsRequest doesn't need a username
         *      because once a user is logged in the server will store
         *      unique usernames with sockets so that when it gets a request
         *      for applications the server knows it means the application
         *      for the user that sent the request. (This can be subject to change)
         */
        public ApplicationsRequest()
        {
            header = new MessageHeader(2);
        }
    }
}
