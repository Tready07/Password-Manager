using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    /// <summary>
    /// Represents the header of a message that can be sent between the client and server.
    /// </summary>
    public struct MessageHeader
    {
        /// <summary>
        /// The magic number sequence used to indicate the beginning of a message.
        /// </summary>
        public const string MagicNumber = "PASS";

        /// <summary>
        /// The size of the message header.
        /// </summary>
        /// <remarks>
        /// This is composed of the following: the magic number (4 bytes) + message ID (4 bytes) +
        /// the message type (1 byte) + the payload size (4 bytes).
        /// </remarks>
        public const int HeaderSize = 13;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageHeader" /> struct.
        /// </summary>
        /// <param name="messageID">The ID of the message being sent.</param>
        /// <param name="messageType">The type of message being sent.</param>
        /// <param name="payloadSize">The size of the payload.</param>
        public MessageHeader(int messageID, MessageType messageType, int payloadSize)
        {
            this.ID = messageID;
            this.Size = payloadSize;
            this.Type = messageType;
        }

        /// <summary>
        /// Gets the ID of the message.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Gets the size of the payload.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gets the type of message.
        /// </summary>
        public MessageType Type { get; }
    }
}
