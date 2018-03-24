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
    public class LoginResponse : MessageBase
    {
        public const int MessageID = 7;

        public LoginResponse(bool admin) :
            base(MessageID, MessageType.Response)
        {
            isAdmin = admin;
        }
        public bool isAdmin { get; set; } = false;

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
