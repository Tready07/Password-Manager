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
    public class DeleteUsernameRequest : MessageBase
    {
        public const int MessageID = 4;

        public DeleteUsernameRequest(Shared.Application app) :
            base(MessageID, MessageType.Request)
        {
            application = app;
        }
        public Shared.Application application;
    }
}
