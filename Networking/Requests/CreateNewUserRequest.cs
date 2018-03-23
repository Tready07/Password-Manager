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
    public class CreateNewUserRequest : MessageBase
    {
        public const int MessageID = 6;

        public CreateNewUserRequest(Shared.Username user, bool isAdmin = false) :
            base(MessageID, MessageType.Request)
        {
            username = user;
            makeAdmin = isAdmin;
        }

        public Shared.Username username { get; set; }
        public bool makeAdmin { get; set; } = false;
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
