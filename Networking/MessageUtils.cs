using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    /// <summary>
    /// Provides utility functions for reading and writing a message.
    /// </summary>
    public static class MessageUtils
    {
        /// <summary>
        /// Deserializes a message header from a stream.
        /// </summary>
        /// <param name="stream">The stream to read the message header from.</param>
        /// <returns>The message header.</returns>
        /// <exception cref="BadHeaderException">
        /// <paramref name="stream" /> does not contain a valid message header.
        /// </exception>
        public static MessageHeader DeserializeMessageHeader(MemoryStream stream)
        {
            // Ensure that the magic number is available as the first thing that gets read in
            byte[] buffer = new byte[MessageHeader.MagicNumber.Length];
            stream.Read(buffer, 0, buffer.Length);
            if (Encoding.ASCII.GetString(buffer) != MessageHeader.MagicNumber)
            {
                throw new BadHeaderException("The message header received is not valid.");
            }

            // Now read in the message ID (which should be an uint of 4 bytes)
            buffer = new byte[4];
            if (stream.Read(buffer, 0, 4) != 4)
            {
                throw new BadHeaderException("There is not enough data to read in the message ID.");
            }
            uint messageID = ToBigEndian(BitConverter.ToUInt32(buffer, 0));

            // Now read in the message type (which should be a single byte)
            buffer = new byte[1];
            if (stream.Read(buffer, 0, 1) != 1)
            {
                throw new BadHeaderException("There is not enough data to read in the message type.");
            }
            MessageType messageType = (MessageType)buffer[0];

            // Now read in the payload size
            buffer = new byte[4];
            if (stream.Read(buffer, 0, 4) != 4)
            {
                throw new BadHeaderException("There is not enough data to read in the payload size.");
            }
            uint payloadSize = ToBigEndian(BitConverter.ToUInt32(buffer, 0));

            return new MessageHeader((int)messageID, messageType, (int)payloadSize);
        }

        /// <summary>
        /// Serializes a message as a <see cref="byte" />[] so it can be written to a socket.
        /// </summary>
        /// <param name="message">The message to serialize.</param>
        /// <returns>A <see cref="byte" />[] that represents the serialized message.</returns>
        public static async Task<byte[]> SerializeMessage(MessageBase message)
        {
            using (var stream = new MemoryStream())
            {
                var messageHeaderTask = MessageUtils.SerializeMessageHeader(stream, message);

                // Do we need to pass in the payload too?
                var messagePayload = message.ToByteArray();
                if (messagePayload != null)
                {
                    var messagePayloadTask = stream.WriteAsync(messagePayload, 0, messagePayload.Length);
                    await Task.WhenAll(messageHeaderTask, messagePayloadTask);
                }
                else
                {
                    await messageHeaderTask;
                }

                return stream.ToArray();
            }
        }

        private static Task SerializeMessageHeader(MemoryStream stream, MessageBase message)
        {
            // First thing that gets written in the magic number
            var magicNumberBytes = Encoding.ASCII.GetBytes(MessageHeader.MagicNumber);
            var magicNumberTask = stream.WriteAsync(magicNumberBytes, 0, magicNumberBytes.Length);

            // Next up is the message ID
            var messageIDBytes = BitConverter.GetBytes(MessageUtils.ToBigEndian((uint)message.ID));
            var messageIDTask = stream.WriteAsync(messageIDBytes, 0, messageIDBytes.Length);

            // Then write the message type. Use a single byte, since there is < 255 possible
            // message types.
            var messageTypeByte = new byte[] { (byte)message.Type };
            var messageTypeTask = stream.WriteAsync(messageTypeByte, 0, messageTypeByte.Length);

            // Now write the message payload size, if we have one.
            var messagePayload = message.ToByteArray();
            int messagePayloadSize = 0;
            if (messagePayload != null)
            {
                messagePayloadSize = messagePayload.Length;
            }
            var messagePayloadBytes = BitConverter.GetBytes(MessageUtils.ToBigEndian((uint)messagePayloadSize));
            var messagePayloadTask = stream.WriteAsync(messagePayloadBytes, 0, messagePayloadBytes.Length);

            return Task.WhenAll(magicNumberTask, messageIDTask, messageTypeTask, messagePayloadTask);
        }

        private static uint ToBigEndian(uint value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                // It's already in big endian, so we can just return the value.
                return value;
            }

            return ConvertEndianess(value);
        }

        public static uint ConvertEndianess(uint value)
        {
            // Flip the endianess of an unsigned int (i.e., a 32-bit number) with some fancy
            // bit shifting magic.
            return ((value & 0xFF000000) >> 24) |
                   ((value & 0x00FF0000) >> 8) |
                   ((value & 0x0000FF00) << 8) |
                   ((value & 0x000000FF) << 24);
        }
    }
}
