using System;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Diagnostics;
using System.Net.Sockets;

namespace Password_Manager_Server
{
    class Program
    {
        private readonly static string Header = new string ('-', Console.BufferWidth - 1);
        private static Server server;

        public static void Main()
        {
            Console.CancelKeyPress += OnCancelKeyPressed;

            // Print a fancy header
            var originalFgColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(Header);
            Console.WriteLine("Password Manager Server");
            Console.WriteLine("Press CTRL+C to stop the server");
            Console.WriteLine(Header);

            Console.ForegroundColor = originalFgColor;
#if DEBUG
            var consoleListener = new ConsoleTraceListener();
            Debug.Listeners.Add(consoleListener);
#endif
            bool isInitialized = databaseInitializer.intializeDatabase();

            var con = new SQLiteConnection();
            con = databaseInitializer.makeConnection(con);

            try
            {
                server = new Server();
                server.Start();
            }
            catch (SocketException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The server is already running.");
                Console.WriteLine("This instance will now close.");
                Console.ForegroundColor = originalFgColor;
            }
        }

        private static void OnCancelKeyPressed(object sender, ConsoleCancelEventArgs e)
        {
            Debug.WriteLine("Stopping the server...");
            server.Stop();

            // Don't immediately exit until all the Task that we've spawned are done.
            e.Cancel = true;
        }
    }
}
