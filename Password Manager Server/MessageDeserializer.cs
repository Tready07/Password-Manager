using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Password_Manager_Server
{
    class MessageDeserializer
    {
        /// <summary>
        /// Feed in a byte array that contains the whole message including the header
        /// to the Messasge Deserializer
        /// </summary>
        /// <param name="message"></param>
        public MessageDeserializer(byte[] message)
        {
            mBuffer = message;
        }

        /// <summary>
        /// returns the message ID
        /// </summary>
        /// <returns></returns>
        public int getID()
        {
            return BitConverter.ToInt32(mBuffer, 4);
        }


        /// <summary>
        /// returns the deserialzed object that is the derived class
        /// from message base
        /// </summary>
        /// <returns></returns>
        public object getMessage()
        {
            if (mBuffer == null)
            {
                return null;
            }
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                var msgSize = getMessageSize();
                memStream.Write(mBuffer, 13, msgSize);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        /// <summary>
        /// returns the message size (used in get message)
        /// </summary>
        /// <returns></returns>
        public int getMessageSize()
        {
            return BitConverter.ToInt32(mBuffer, 9);
        }
        public byte[] mBuffer;
    }
}

