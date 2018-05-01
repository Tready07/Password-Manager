using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Responses
{
    [Serializable]
    public class DeleteUserResponse : MessageBase
    {
        public const int MessageID = 9;

        public DeleteUserResponse(String user) :
            base(MessageID, MessageType.Response)
        {
            username = user;
        }
        public string username {get; set;}
    }
}