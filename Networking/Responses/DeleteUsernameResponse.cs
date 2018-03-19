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
    public class DeleteUsernameResponse : MessageBase
    {
        public const int MessageID = 3;

        public DeleteUsernameResponse(Shared.Application app) :
            base(MessageID, MessageType.Response)
        {
            application = app;
        }

        public Shared.Application application { get; set; }

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
