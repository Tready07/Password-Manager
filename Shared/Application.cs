using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [Serializable]
    public class Application
    {
        public Application(String name, Username[] usernames, String type)
        {
            
            Usernames = usernames;

            
            Type = type;

            
            Name = name;
        }

        public Application()
        {

        }
        /**
          * @brief
          *      List of usernames for the given application
          */
        public Username [] Usernames { get; set; }

        /**
          * @brief
          *      Type of application e.g. social media, mail, other.
          */
        public String Type { get; set; }

        /**
          * @brief
          *      Name of the application
          */
        public String Name { get; set; }
    }
}
