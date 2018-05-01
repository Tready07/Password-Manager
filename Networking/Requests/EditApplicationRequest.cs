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
    public class EditApplicationRequest : MessageBase
    {
        public const int MessageID = 11;

        public EditApplicationRequest(Shared.Application  application):
            base(MessageID, MessageType.Request)
        {
            AppToEdit = application;
        }
        public Shared.Application AppToEdit { get; set; }
        public String NewAppType { get; set; }
        public String NewAppName { get; set; }
    }
}
