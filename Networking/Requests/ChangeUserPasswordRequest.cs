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
    public class ChangeUserPasswordRequest : MessageBase
    {
        public const int MessageID = 5;
        public ChangeUserPasswordRequest(String plainTextPw) :
            base(MessageID, MessageType.Response)
        {
            plainTextPassword = plainTextPw;
        }

        public String plainTextPassword { get; set; }
    }
}
