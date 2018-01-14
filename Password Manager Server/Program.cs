using System;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Diagnostics;

namespace Password_Manager_Server
{
    class Program
    {
        static async Task Main()
        {
#if DEBUG
            var consoleListener = new ConsoleTraceListener();
            Debug.Listeners.Add(consoleListener);
#endif

            bool isInitialized = databaseInitializer.intializeDatabase();
            var con = new SQLiteConnection();
            con = databaseInitializer.makeConnection(con);
            Server server = new Server();
            await server.Start();
        }
    }
}
