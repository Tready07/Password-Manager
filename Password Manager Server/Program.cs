using System;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Diagnostics;

namespace Password_Manager_Server
{
    class Program
    {
        private static Server server;

        public static async Task Main()
        {
#if DEBUG
            var consoleListener = new ConsoleTraceListener();
            Debug.Listeners.Add(consoleListener);
#endif
            bool isInitialized = databaseInitializer.intializeDatabase();

            var con = new SQLiteConnection();
            con = databaseInitializer.makeConnection(con);

            server = new Server();
            server.Start();
        }

        private static void OnCancelKeyPressed(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Stopping the server...");
            server.Stop();
        }
    }
}
