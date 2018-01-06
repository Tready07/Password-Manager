using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Password_Manager_Server
{
    public class ClientSession
    {
        public ClientSession(TcpClient tcpclient,String name)
        {
            user = name;
            client = tcpclient;
        }

        public String user { get; set; }
        public TcpClient client;
    }
}
