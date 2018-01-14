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
            // Print a fancy header
            var originalFgColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;

            string header = new string('-', Console.BufferWidth - 1);
            Console.WriteLine(header);
            Console.WriteLine("Password Manager Server");
            Console.WriteLine("Press CTRL+C to stop the server");
            Console.WriteLine(header);

            Console.ForegroundColor = originalFgColor;
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
