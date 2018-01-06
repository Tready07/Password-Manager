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

        public void send(MessageBase message)
        {
            var formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            using (socket)
            {
                try
                {
                    formatter.Serialize(stream, message);
                    socket.Send(message.header.toByteArray());
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
