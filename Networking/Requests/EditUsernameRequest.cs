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
    public class EditUsernameRequest : MessageBase
    {
        const int MessageID = 12;
        public EditUsernameRequest(Shared.Application application):
            base(MessageID, MessageType.Request)
        {
            app = application;
        }
        public Shared.Application app { get; set; }
        public string NewUsername { get; set; }
    }
}
