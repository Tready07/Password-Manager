﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Responses
{
   [Serializable]
   public class ApplicationsResponse : MessageBase
    {
        public const int MessageID = 0;

        public ApplicationsResponse(Shared.Application[] apps) :
            base(MessageID, MessageType.Response)
        {
            applications = apps;
        }

        public Shared.Application[] applications { get; set; }

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
