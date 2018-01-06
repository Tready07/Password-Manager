using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Password_Manager_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isInitialized = databaseInitializer.intializeDatabase();
            var con = new SQLiteConnection();
            con = databaseInitializer.makeConnection(con);
            Server server = new Server();
            server.start(con);
            Console.ReadKey();
        }
    }
}
