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
    public class PasswordResponse : MessageBase
    {
        public const int MessageID = 2;

        public PasswordResponse(Shared.Application app) :
            base(MessageID, MessageType.Response)
        {
            application = app;
        }

        public Shared.Application application { get; set; }

    }
}
