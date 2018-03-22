using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Networking
{
    //TODO: consider some public file to contain all messageIDs
    /// <summary>
    /// The base class for all messages (i.e., requests and responses) that are sent between the
    /// server and client.
    /// </summary>
    [Serializable]
    public abstract class MessageBase
    {
        [NonSerialized]
        private int id;

        [NonSerialized]
        private MessageType type;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBase" /> class.
        /// </summary>
        /// <param name="messageID">The ID of this message.</param>
        /// <param name="messageType">The type of message that this message represents.</param>
        protected MessageBase(int messageID, MessageType messageType)
        {
            this.id = messageID;
            this.type = messageType;
        }

        /// <summary>
        /// Gets an ID that is used to uniquely identify this message.
        /// </summary>
        public int ID => id;

        /// <summary>
        /// Gets the type of message being sent.
        /// </summary>
        public MessageType Type => type;

        /// <summary>
        /// Gets the payload data for this message as a <see cref="byte" />[], if there are any.
        /// </summary>
        /// <returns>The payload data for this message.</returns>
        /// <remarks>
        /// <para>
        ///     Implementers do not need to serialize the <see cref="MessageBase.ID" /> and
        ///     <see cref="MessageBase.Type" /> as they're included with the message's header.
        /// </para>
        /// <para>
        ///     If there is no payload data that needs to be sent with this message, then this
        ///     function should return <c>null</c>.
        /// </para>
        /// </remarks>
        public abstract byte[] ToByteArray();
    }
}
