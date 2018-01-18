using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager_Server
{
    public class ReadyClient
    {
        public ReadyClient(Task<int> tsk, Socket skt )
        {
            task = tsk;
            socket = skt;
        }
        public Task<int> task { get; set;}
        public Socket socket { get; set; }
    }
}
