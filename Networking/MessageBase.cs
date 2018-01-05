using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Networking
{
    [Serializable]
    /**
     * @brief
     * Base class for all future messages to inherit from.
     */
    public class MessageBase
    {
        [NonSerialized] public MessageHeader header;
    }

    
}
