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
    public class ChangeAppTypeRequest : MessageBase
    {
        public const int MessageID = 10;

        public ChangeAppTypeRequest(Shared.Application [] applications):
            base(MessageID, MessageType.Request)
        {
            apps = applications;
        }

        public ChangeAppTypeRequest(Shared.Application application) :
           base(MessageID, MessageType.Request)
        {
            apps = new Shared.Application [] {application};
        }

        public Shared.Application [] apps {get; set;}
    }
}
