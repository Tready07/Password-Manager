using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [Serializable]
    public class Username
    {
        public Username(String username)
        {
            name = username;
        }
        public Username(String username, String pw)
        {
            name = username;
            password = pw;
        }

        /**
         * @brief
         *      Name of the user
         */
      public String name { get; set; }

        /**
         * @brief
         *      password of the user
         *      this should be encrypted CryptManager
         *      using clientside symmetric key
         */
        public String password { get; set; } = "";
    }
}
