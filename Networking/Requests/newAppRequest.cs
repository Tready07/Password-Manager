using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Requests
{
    class newAppRequest : MessageBase
    {
        public const int MessageID = 2;

        public newAppRequest(Shared.Application app) :
            base(MessageID, MessageType.Request)
        {
            application = app;
        }
        Shared.Application application;

        public override byte[] ToByteArray()
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new BinaryFormatter();
                serializer.Serialize(stream, this);

                return stream.ToArray();
            }
        }
    }
}
