﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Request
{
    [Serializable]
    public class ApplicationsRequest : MessageBase
    {
        public const int MessageID = 2;

        /**
         * @brief
         *      The ApplicationsRequest doesn't need a username
         *      because once a user is logged in the server will store
         *      unique usernames with sockets so that when it gets a request
         *      for applications the server knows it means the application
         *      for the user that sent the request. (This can be subject to change)
         */
        public ApplicationsRequest() :
            base(MessageID, MessageType.Request)
        {
        }

        public override byte[] ToByteArray()
        {
            return null;
        }
    }
}
