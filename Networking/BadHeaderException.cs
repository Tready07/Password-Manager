using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{

    /// <summary>
    /// Represents an exception that should be thrown when attempting to deserialize a stream that
    /// contains a malformed or bad header.
    /// </summary>
    [Serializable]
    public class BadHeaderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadHeaderException" /> class.
        /// </summary>
        public BadHeaderException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadHeaderException" /> with a specified
        /// error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BadHeaderException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadHeaderException" /> with a specified
        /// error message and a reference to the inner exception that is the cause of this
        /// exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a null reference if
        /// no inner exception is specified.
        /// </param>
        public BadHeaderException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadHeaderException" /> class with
        /// serialized data.
        /// </summary>
        /// <param name="info">The serialized object data about the exception being thrown.</param>
        /// <param name="context">The context that contains contextual information about the source or destination.</param>
        protected BadHeaderException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
