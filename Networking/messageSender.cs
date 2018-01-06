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
        public MessageSender(MessageBase message, Socket socket)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    var headerBuffer = message.header.toByteArray();
                    stream.Write(headerBuffer, 0, headerBuffer.Length);

                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, message);

                    socket.Send(stream.ToArray());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to send message {message.header.messageID.ToString()}");
                Console.WriteLine($"The following error occurred: {ex.Message}");
            }
        }
    }
}
