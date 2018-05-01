using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
        /// 
        /// The default implementation uses BinaryFormatter to serialize the message.
        /// </summary>
        /// <returns>The payload data for this message.</returns>
        /// <remarks>
        /// <para>
        ///     If there is no payload data that needs to be sent with this message, then this
        ///     function should return <c>null</c>.
        /// </para>
        /// </remarks>
        public virtual byte[] ToByteArray()
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
