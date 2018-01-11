using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Password_Manager_Server
{
    class DatabaseQuerier
    {
        public DatabaseQuerier(SQLiteConnection conn)
        {
            dbConnection = conn;
        }

        private SQLiteConnection dbConnection;

        /**
         * @brief
         *      gets all the applications for a specific user
         */
        public Shared.Application [] getApplications(String user)
        {
            String[] applicationNames = getApplicationNames(user);
            List<Shared.Application> applications = new List<Shared.Application>();
            foreach(var name in applicationNames)
            {
                String appType = getAppType(name, user);
                Shared.Username[] usernames = getUserNames(name, user);
                Shared.Application app = new Shared.Application(name, usernames, appType);
                applications.Add(app);
            }

            return applications.ToArray();
        }

        /**
         * @brief
         *      Returns an array of application names for
         *      a specific user
         */
        private string[] getApplicationNames(String user)
        {
            List<string> applicationNames = new List<string>();
            String sqlString = "SELECT DISTINCT application FROM applications WHERE name = @name";
            SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
            command.Parameters.AddWithValue("@name", user);
            SQLiteDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                applicationNames.Add(reader["application"].ToString());
            }
            return applicationNames.ToArray();
        }

        /**
         * @brief
         *      Gets the list of usernames the 'user' has for the given application
         */
        private Shared.Username [] getUserNames(String appName, String user)
        {
            List<Shared.Username> usernames = new List<Shared.Username>();
            String sqlString = "SELECT username FROM applications WHERE application = @application AND name = @name";
            SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
            command.Parameters.AddWithValue("@name", user);
            command.Parameters.AddWithValue("@application", appName);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Shared.Username username = new Shared.Username(reader["username"].ToString());
                usernames.Add(username);
            }
            return usernames.ToArray();
        }

        /**
         * @brief
         *      gets the application type that the 'user' chose for
         *      the given 'appName'
         */
        private string getAppType(String appName, String user)
        {
            String appType = "";
            String sqlString = "SELECT DISTINCT application_type FROM applications WHERE application = @application AND name = @name";
            SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
            command.Parameters.AddWithValue("@name", user);
            command.Parameters.AddWithValue("@application", appName);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                appType =reader["application_type"].ToString();
            }
            return appType;
        }

        /**
         * @brief
         *      Returns a string array of distinct application types for
         *      that given user
         */      
        public string[] getAppTypes(String user)
        {
            List<String> appTypes = new List<string>();
            String sqlString = "SELECT DISTINCT application_type FROM applications WHERE name = @name";
            SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
            command.Parameters.AddWithValue("@name", user);
            SQLiteDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                var appType = reader["application_type"].ToString();
                appTypes.Add(appType);
            }
            return appTypes.ToArray();
        }

        /**
         * @brief
         *      gets the password for the given 'username'
         *      for the specified (appName) for the specific
         *      'user' who is logged in on the client
         *      NOTE: password will be an encrypted string when returned.
         */      
        public string getPassword(String appName, String username, String user)
        {
            String password = "";
            String sqlString = "SELECT password FROM applications WHERE application = @appName AND username = @username AND name = @user ";
            SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
            command.Parameters.AddWithValue("@name", user);
            command.Parameters.AddWithValue("@appName", appName);
            command.Parameters.AddWithValue("@username", username);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                password = reader["password"].ToString();
            }
            return password;
        }
        

        /**
         * @brief
         *      sets the password for a specific username under the specific user
         *      NOTE: the password should be encrypted
         */      
        public void setPassword(String appName, String username, String user, String password)
        {
            String sqlString = "UPDATE applications SET password = @password WHERE application = @appName AND username = @username AND name = @user";
            SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
            command.Parameters.AddWithValue("@name", user);
            command.Parameters.AddWithValue("@appName", appName);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.ExecuteNonQuery();
        }

        /**
         * @brief
         *      sets the password for a specific username under the specific user
         *      NOTE: the password should be encrypted
         */
        public void setPassword(Shared.Application app, String user)
        {
            String sqlString = "UPDATE applications SET password = @password WHERE application = @appName AND username = @username AND name = @user";
            SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
            command.Parameters.AddWithValue("@name", user);
            command.Parameters.AddWithValue("@appName", app.Name);
            command.Parameters.AddWithValue("@username", app.Usernames[0].name);
            command.Parameters.AddWithValue("@password", app.Usernames[0].password);
            command.ExecuteNonQuery();
        }

        /**
         * @brief
         *      adds a new username to the database fields in all of the parameters
         *      in the applications table
         */      
        public void addUsername(Shared.Application app, String user)
        {
            String sqlString = "INSERT INTO applications (name, application, application_type, username, password) " +
                "VALUES (@name, @appName, @appType, @username, @password)";
            SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
            command.Parameters.AddWithValue("@name", user);
            command.Parameters.AddWithValue("@appName", app.Name);
            command.Parameters.AddWithValue("@appType", app.Type);
            command.Parameters.AddWithValue("@username", app.Usernames[0].name);
            command.Parameters.AddWithValue("@password", app.Usernames[0].password);
            command.ExecuteNonQuery();
        }

        /**
         * @brief
         *      removes a username and his info from the database.
         *      
         */
        public void removeUsername(Shared.Application app, String user)
        {
            String sqlString = "DELETE FROM applications WHERE name = @name AND application = @appname AND username = @username";
            SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
            command.Parameters.AddWithValue("@name", user);
            command.Parameters.AddWithValue("@appName", app.Name);
            command.Parameters.AddWithValue("@username", app.Usernames[0].name);
            command.ExecuteNonQuery();
        }

    }
}
