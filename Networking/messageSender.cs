using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;
using System.IO;

namespace Networking
{
    public class MessageSender
    {
        public MessageSender(Socket skt)
        {
            socket = skt;             
        }

        public MessageSender()
        {

        }

        public void send(MessageBase message)
        {
            var formatter = new BinaryFormatter();
            var lengthformatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();            
            using (socket)
            {
                try
                {
                    MemoryStream payLoadStream = new MemoryStream();
                    lengthformatter.Serialize(payLoadStream, message);
                    message.header.messageSize = payLoadStream.Length;
                    var headerSize = BitConverter.GetBytes(message.header.toByteArray().Length);
                    stream.Write(headerSize, 0, headerSize.Length);
                    stream.Write(message.header.toByteArray(), 0, message.header.toByteArray().Length);
                    stream.Write(payLoadStream.ToArray(), 0,(int)payLoadStream.Length);
                    socket.Send(stream.ToArray());
                }
                catch (Exception e)
                {
                    Console.Write("Error sending message " + message.header.messageID);
                    Console.WriteLine(e.Message);
                }
            }
        }

        public Socket socket { get; set; }
    }
}
