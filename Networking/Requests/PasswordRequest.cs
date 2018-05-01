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
    public class PasswordRequest : MessageBase
    {
        public const int MessageID = 3;
        public PasswordRequest(Shared.Application app) :
            base(MessageID, MessageType.Request)
        {
            application = app;
        }
        public Shared.Application application;
        public bool updatePassword {get; set;} = false;
    }
}
