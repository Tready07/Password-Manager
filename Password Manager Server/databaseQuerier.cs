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
            command.Parameters.AddWithValue("@user", user);
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
        public bool setPassword(String appName, String username, String user, String password)
        {
            try{
                String sqlString = "UPDATE applications SET password = @password WHERE application = @appName AND username = @username AND name = @user";
                SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
                command.Parameters.AddWithValue("@name", user);
                command.Parameters.AddWithValue("@appName", appName);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.ExecuteNonQuery();
                return true;
                }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /**
         * @brief
         *      sets the password for a specific username under the specific user
         *      NOTE: the password should be encrypted
         */
        public bool setPassword(Shared.Application app, String user)
        {
            try{
                String sqlString = "UPDATE applications SET password = @password WHERE application = @appName AND username = @username AND name = @user";
                SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@appName", app.Name);
                command.Parameters.AddWithValue("@username", app.Usernames[0].name);
                command.Parameters.AddWithValue("@password", app.Usernames[0].password);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /**
         * @brief
         *      adds a new username to the database fields in all of the parameters
         *      in the applications table
         */      
        public bool addUsername(Shared.Application app, String user)
        {
            try
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
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }

        /**
         * @brief
         *      removes a username and his info from the database.
         *      
         */
        public bool removeUsername(Shared.Application app, String user)
        {
            try{
                String sqlString = "DELETE FROM applications WHERE name = @name AND application = @appname AND username = @username";
                SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
                command.Parameters.AddWithValue("@name", user);
                command.Parameters.AddWithValue("@appName", app.Name);
                command.Parameters.AddWithValue("@username", app.Usernames[0].name);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public String getSalt(String user)
        {
            String sqlString = "SELECT salt FROM users WHERE name = @name";
            SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
            command.Parameters.AddWithValue("@name", user);
            SQLiteDataReader reader = command.ExecuteReader();
            String salt = "";
            while (reader.Read())
            {
                salt = reader["salt"].ToString();
            }
            return salt;
        }

        private String getLoginPassword(String user)
        {

            String sqlString = "SELECT password FROM users WHERE name = @name";
            SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
            command.Parameters.AddWithValue("@name", user);
            SQLiteDataReader reader = command.ExecuteReader();
            String password = "";
            while (reader.Read())
            {
                password = reader["password"].ToString();
            }
            return password;
        }

        public bool checkLoginInfo(String user, String password)
        {
            bool isCorrect = false;
            String salt = getSalt(user);
            String encryptedAttemptedPW = Shared.CryptManager.hash(password + salt);
            String encryptedStoredPW = getLoginPassword(user);

            if(encryptedAttemptedPW == encryptedStoredPW)
            {
                isCorrect = true;
            }

            return isCorrect;
        }

        public bool changeUserPassword(String user, String newPassword)
        {
            bool success= false;
            String salt = getSalt(user);
            String encryptedNewPassword = Shared.CryptManager.hash(newPassword + salt);
            try
            {
                String sqlString = "UPDATE users SET password = @password WHERE name = @user";
                SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
                command.Parameters.AddWithValue("@password", encryptedNewPassword);
                command.Parameters.AddWithValue("@user", user);
                int effectedRows = command.ExecuteNonQuery();
                success = (effectedRows > 0) ? true : false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }


            return success;
        }

        public bool createNewUser(String username, String password, bool isAdmin = false)
        {
            String salt = Shared.CryptManager.generateSalt(8);
            String encryptedPassword = Shared.CryptManager.hash(password + salt);
            try
            {
                String sqlString = "INSERT INTO users VALUES(@name,@password,@isAdmin,0,@salt)";
                SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
                command.Parameters.AddWithValue("@name",username);
                command.Parameters.AddWithValue("@password",encryptedPassword);
                command.Parameters.AddWithValue("@isAdmin",isAdmin);
                command.Parameters.AddWithValue("@salt",salt);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool isAdmin(string username)
        {
            try
            {
                String sqlString = "SELECT isadmin FROM users WHERE name = @name";
                SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
                command.Parameters.AddWithValue("@name", username);
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                bool isAdmin = (bool)reader["isadmin"];
                return isAdmin;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Shared.Username [] getUsers()
        {
             String sqlString = "SELECT name, isadmin FROM users";
             SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
             SQLiteDataReader reader = command.ExecuteReader();
             List<Shared.Username> users = new List<Shared.Username> ();
             while(reader.Read())
             {
                 Shared.Username username = new Shared.Username(reader["name"].ToString());
                 username.isAdmin = (bool)reader["isadmin"];
                 users.Add(username);
             }
             return users.ToArray();
        }

        public bool changeAdminPrivileges(string username, bool admin)
        {
            String sqlString = "UPDATE users SET isadmin = @admin WHERE name = @name";
            SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
            command.Parameters.AddWithValue("@admin", admin);
            command.Parameters.AddWithValue("@name", username);
            return (command.ExecuteNonQuery() > 0);
        }

        public bool isSuperAdmin(string username)
        {
            try
            {
                String sqlString = "SELECT issuperadmin FROM users WHERE name = @name";
                SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
                command.Parameters.AddWithValue("@name", username);
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                bool isAdmin = (bool)reader["issuperadmin"];
                return isAdmin;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool deleteUser(string user)
        {
            try
            {
                String sqlString = "DELETE FROM users WHERE name = @name";
                SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
                command.Parameters.AddWithValue("@name", user);
                return (command.ExecuteNonQuery() > 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        
        public bool changeAppType(Shared.Application app, string user)
        {
            try
            {
                String sqlString = "UPDATE APPLICATIONS set application_type = @application_type WHERE application = @application AND name = @name AND username = @username";
                SQLiteCommand command = new SQLiteCommand(sqlString,dbConnection);
                command.Parameters.AddWithValue("@application", app.Name);
                command.Parameters.AddWithValue("application_type", app.Type);
                command.Parameters.AddWithValue("@name", user);
                foreach(var username in app.Usernames)
                {
                    command.Parameters.AddWithValue("@username",username.name);
                    command.ExecuteNonQuery();
                }
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool editApp(Shared.Application appToEdit, string newName, string newAppType, string user)
        {
            try
            {
                String sqlString = "UPDATE APPLICATIONS set application_type = @newAppType, application = @newAppName" +
                    " WHERE name = @user AND username = @username AND application = @oldAppName";
                SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
                command.Parameters.AddWithValue("@newAppType", newAppType);
                command.Parameters.AddWithValue("@newAppName", newName);
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@oldAppName", appToEdit.Name);
                var usernames = appToEdit.Usernames;
                foreach(var name in usernames)
                {
                    command.Parameters.AddWithValue("@username", name.name);
                    command.ExecuteNonQuery();
                }
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool changeUsername(Shared.Application app, string newUsername, string user)
        {
            try
            {
                string sqlString = "UPDATE APPLICATIONS set username = @newUsername " +
                    " WHERE name = @user AND application = @appname AND username = @username";
                SQLiteCommand command = new SQLiteCommand(sqlString, dbConnection);
                command.Parameters.AddWithValue("@newUsername", newUsername);
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@appname", app.Name);
                command.Parameters.AddWithValue("@username", app.Usernames[0].name);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }
}
