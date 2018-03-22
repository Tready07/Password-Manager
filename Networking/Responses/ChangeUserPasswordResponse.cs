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
    class ChangeUserPasswordResponse : MessageBase
    {
        public const int MessageID = 4;

        public ChangeUserPasswordResponse(bool success):
            base(MessageID, MessageType.Response)
        {
            isSuccessful = success;
        }
        public bool isSuccessful { get; set; }

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
