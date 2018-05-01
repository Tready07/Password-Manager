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
    public class ChangeAdminResponse : MessageBase
    {
        public const int MessageID = 6;

        public ChangeAdminResponse(Shared.Username user) :
            base(MessageID, MessageType.Response)
        {
            username = user;
        }

        public Shared.Username username {get; set;}
    }
}
