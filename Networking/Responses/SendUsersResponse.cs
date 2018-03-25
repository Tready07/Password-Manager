using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Responses
{
    [Serializable]
    public class SendUsersResponse  : MessageBase
    {
        public const int MessageID = 8;
        public SendUsersResponse(Shared.Username [] usernames) :
            base(MessageID, MessageType.Response)
        {
            users = usernames;
        }

        public Shared.Username[] users { get; set; }

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
