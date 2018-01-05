using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [Serializable]
    public class MessageHeader
    {
        public MessageHeader(int ID,int size)
        {
            messageID = ID;
            messageSize = size;
        }
        /**
         * @brief
         * MessageID is informs what message type it is.
         */
        int messageID;

        /**
         * @brief
         * Message Size is the size of the payload that is
         * delivered.
         */
        int messageSize;
    }
}
