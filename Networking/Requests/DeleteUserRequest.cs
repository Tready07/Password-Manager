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
    public class DeleteUserRequest : MessageBase
    {
        public const int MessageID = 9;

        public DeleteUserRequest(String user) :
            base(MessageID, MessageType.Request)
        {
            username = user;
        }
        public string username {get; set;}
    }
}