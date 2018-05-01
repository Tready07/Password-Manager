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
    public class CreateNewUserResponse : MessageBase
    {
        public const int MessageID = 5;

        public CreateNewUserResponse(bool success):
            base(MessageID, MessageType.Response)
        {
            isSuccessful = success;
        }
        public bool isSuccessful { get; set; }
    }
}
