using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;

namespace Password_Manager_Server
{
    /**
     * @brief
     *      The databaseInitializer is intended to be used
     *      at the beginning of the program to ensure that
     *      the sqlite database is intialized
     */
    class databaseInitializer
    {
        /**
         * @brief
         *      InitializeDatabase checks for pwdatabase.sqlite file
         *      if it isn't there then it calls intializeTables() 
         */
        public static bool intializeDatabase()
        {
            bool Initialized = true;
            Console.WriteLine(Directory.GetCurrentDirectory());
            bool databaseExists = File.Exists("./pwdatabase.sqlite");
            Console.WriteLine("does database exist? {0}", databaseExists);
            if(!databaseExists)
            {
                try
                {
                    SQLiteConnection.CreateFile("pwdatabase.sqlite");
                    
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error in Initializing Database");
                    Console.Write(e.Message);
                    Initialized = false;
                }

                intializeTables();
            }
            return Initialized;
        }
        /**
         * @brief
         *      InitializeTables intializes the users and password tables
         */
        private static bool intializeTables()
        {
            bool tablesInitialzed = true;
            SQLiteConnection dbConn = new SQLiteConnection();
            dbConn = makeConnection(dbConn);
            String createUsersTable = "CREATE TABLE users (name VARCHAR(20) NOT NULL UNIQUE, password TEXT NOT NULL, isAdmin BOOL, salt TEXT)";
            String createApplicationsTable = "CREATE TABLE applications (name VARCHAR(20) NOT NULL, application CHAR(20), application_type CHAR(20) NOT NULL, username CHAR(40), password TEXT, UNIQUE(name, application, username), FOREIGN KEY(name) REFERENCES users(name))";
            try
            {
                SQLiteCommand command = new SQLiteCommand(createUsersTable, dbConn);
                command.ExecuteNonQuery();
                command = new SQLiteCommand(createApplicationsTable, dbConn);
                command.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                tablesInitialzed = false;
                Console.WriteLine("Error while creating tables");
                Console.Write(e.Message);
                System.Environment.Exit(1);
            }
            dbConn.Close();
            return tablesInitialzed;

        }


        /**
         * @brief
         *      getConnection makes a sqlite connection to pwdatabase.sqlite file
         *      and opens the connection then returns the sqliteconnection object it was passed
         */
        public static SQLiteConnection makeConnection(SQLiteConnection connection)
        {
            try
            {
                connection = new SQLiteConnection("Data Source=pwdatabase.sqlite;Version=3;");
                connection.Open();
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
                Console.WriteLine("Couldn't connect to database");
                System.Environment.Exit(1);
            }
            return connection;
                
        }
    }
}
