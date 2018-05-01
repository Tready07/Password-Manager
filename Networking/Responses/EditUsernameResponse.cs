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
    public class EditUsernameResponse : MessageBase
    {
        public const int MessageID = 12;
        public EditUsernameResponse(bool success) :
            base(MessageID, MessageType.Response)
        {
            isSuccess = success;
        }
        public bool isSuccess { get; set; }
    }
}
