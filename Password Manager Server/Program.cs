using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isInitialized = databaseInitializer.intializeDatabase();
            Console.ReadKey();
            
        }
    }
}
