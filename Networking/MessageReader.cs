using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Networking
{
    /// <summary>
    /// Defines the list of possible states that the <see cref="MessageReader" /> can be in.
    /// </summary>
    public enum MessageReaderState
    {
        /// <summary>
        /// The message reader is expecting to process a header.
        /// </summary>
        ProcessingHeader,

        /// <summary>
        /// The message reader is expecting to process or is in the middle of processing a message.
        /// </summary>
        ProcessingMessage,

        /// <summary>
        /// A message is ready.
        /// </summary>
        MessageReady
    }


    /// <summary>
    /// Provides a way to read a <see cref="MessageBase" /> from a stream, e.g., a
    /// <see cref="Socket" />.
    /// </summary>
    public class MessageReader
    {
        private int bytesRead = 0;
        private byte[] payloadData = null;
        private readonly BinaryFormatter binaryFormatter = new BinaryFormatter();

        /// <summary>
        /// Gets the header of the message currently being processed.
        /// </summary>
        public MessageHeader? Header { get; private set; } = null;

        /// <summary>
        /// Gets the payload data for the current message that was processed.
        /// </summary>
        public byte[] Payload => this.payloadData;

        /// <summary>
        /// Gets a value indicating whether a message is ready.
        /// </summary>
        public bool IsMessageReady
        {
            get
            {
                return this.State == MessageReaderState.MessageReady;
            }
        }

        /// <summary>
        /// Gets the current state.
        /// </summary>
        public MessageReaderState State { get; private set; } = MessageReaderState.ProcessingHeader;

        /// <summary>
        /// Gets the deserialized message that was just processed.
        /// </summary>
        /// <returns>The deserialized message.</returns>
        public MessageBase GetMessage()
        {
            using (var memoryStream = new MemoryStream(this.payloadData))
            {
                return (MessageBase)binaryFormatter.Deserialize(memoryStream);
            }
        }

        /// <summary>
        /// Processes the message (or the header of a message) in a <see cref="NetworkStream" />.
        /// </summary>
        /// <param name="stream"></param>
        public async Task Process(NetworkStream stream)
        {
            if (this.State == MessageReaderState.MessageReady)
            {
                // Don't do anything if we have a message ready.
                return;
            }

            do
            {
                switch (this.State)
                {
                    case MessageReaderState.ProcessingHeader:
                        await this.ProcessHeader(stream);
                        break;
                    case MessageReaderState.ProcessingMessage:
                        await this.ProcessPayload(stream);
                        break;
                }
            }
            while (stream.DataAvailable);
        }

        /// <summary>
        /// Tell the <see cref="MessageReader" /> that it can process the next message.
        /// </summary>
        public void ProcessNextMessage()
        {
            this.State = MessageReaderState.ProcessingHeader;
        }

        private async Task ProcessHeader(NetworkStream stream)
        {
            byte[] buffer = new byte[MessageHeader.HeaderSize];
            await stream.ReadAsync(buffer, 0, buffer.Length);

            using (var memoryStream = new MemoryStream(buffer))
            {
                this.Header = MessageUtils.DeserializeMessageHeader(memoryStream);
            }

            this.payloadData = new byte[this.Header.Value.Size];

            // We're either done or processing the message depending on if the message has
            this.State = this.Header.Value.Size > 0 ? MessageReaderState.ProcessingMessage :
                                                      MessageReaderState.MessageReady;
        }

        private async Task ProcessPayload(NetworkStream stream)
        {
            do
            {
                int bytesRemaining = this.Header.Value.Size - this.bytesRead;
                int bytesReceived = await stream.ReadAsync(this.payloadData, this.bytesRead, bytesRemaining);
                if (bytesReceived == bytesRemaining)
                {
                    // We've read the entire message, we're done! Move on to the next message.
                    this.State = MessageReaderState.MessageReady;
                    return;
                }

                bytesRead += bytesReceived;
            }
            while (stream.DataAvailable);
        }
    }
}
