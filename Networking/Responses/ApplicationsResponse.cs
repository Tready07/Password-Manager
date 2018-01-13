using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Response
{
    [Serializable]
   public class ApplicationsResponse : MessageBase
    {
        public ApplicationsResponse(Shared.Application[] apps)
        {
            applications = apps;
        }

        Shared.Application[] applications;
    }
}
