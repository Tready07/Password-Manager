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
    public class ChangeAdminRequest : MessageBase
    {
        public const int MessageID = 7;

        public ChangeAdminRequest(Shared.Username user, bool admin):
            base(MessageID, MessageType.Request)
        {
            username = user;
            makeAdmin = admin;
        }

        public Shared.Username username { get; set; }
        public bool makeAdmin { get; set; }
    }
}
