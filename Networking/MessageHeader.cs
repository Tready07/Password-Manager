using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    [Serializable]
    public class MessageHeader
    {
        public MessageHeader(int ID)
        {
            messageID = ID;
        }

        /**
         * @brief
         * MessageID is informs what message type it is.
         */
        public int messageID { get; set; }

        /**
         * @brief
         * Message Size is the size of the payload that is
         * delivered.
         */
        public long messageSize { get; set; } = 0;

        public byte [] toByteArray()
        {
            var formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, this);
            return stream.ToArray();
        }
        
    }
}
