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
    public class SendUsersRequest : MessageBase
    {
        public const int MessageID = 8;
        public SendUsersRequest():
            base(MessageID, MessageType.Request)
        {

        }
    }
}
