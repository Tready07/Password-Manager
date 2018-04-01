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
    public class ErrorResponse : MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse" /> class.
        /// </summary>
        /// <param name="id">The message ID.</param>
        /// <param name="message">The error message to show to the user.</param>
        public ErrorResponse(int id, string message) :
            base(id, MessageType.ErrorResponse)
        {
            this.Message = message;
        }

        public string Message { get; }

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
