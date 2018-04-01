using System;

namespace Networking
{
    /// <summary>
    /// Represents the available kind of messages that can be sent between the server and client.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// The message being sent contains request data.
        /// </summary>
        Request,
        
        /// <summary>
        /// The message being sent contains response data.
        /// </summary>
        Response,

        /// <summary>
        /// The message being sent contains response error.
        /// </summary>
        ErrorResponse
    }
}
