using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;
using Networking.Requests;
using Networking.Responses;
using System.Net.Sockets;
using System.Diagnostics;

namespace Password_Manager_Server
{
    public class MessageHandler
    {
        Func<byte [],ClientSession, bool>[] functions = {handleLogin,handleApplications,handleNewApp,
            handlePassword, handleDeleteUsername, handleChangeUserPassword, handleCreateNewUser, handleChangeAdmin, handleSendUsers};
        public MessageHandler()
        {

        }

        public bool handleMessage(byte [] message, ClientSession session, MessageHeader header)
        {
            bool isComplete = false;
            MessageDeserializer ds = new MessageDeserializer(message);
            int id = header.ID;
            isComplete = functions[id](message,session);
            return isComplete;
        }

        private static bool handleLogin(byte [] message,ClientSession session)
        {
            MessageDeserializer ds = new MessageDeserializer(message);
            LoginRequest msg =(LoginRequest) ds.getMessage();
            Console.WriteLine(msg.username.name);
            Console.WriteLine(msg.username.password);
            var con = databaseInitializer.makeConnection();
            DatabaseQuerier db = new DatabaseQuerier(con);
            if(db.checkLoginInfo(msg.username.name,msg.username.password))
            {
                Console.Write("The info is True!");
                session.loginUsername.name = msg.username.name;
                session.loginUsername.isAdmin = db.isAdmin(msg.username.name);
                LoginResponse loginResponse = new LoginResponse(session.loginUsername.isAdmin);
                byte[] loginPayLoad = MessageUtils.SerializeMessage(loginResponse).GetAwaiter().GetResult();
                session.Client.Client.Send(loginPayLoad);
                return true;
            }
            return false;
        }

        private static bool handleApplications(byte [] message,ClientSession session)
        {
            var con = databaseInitializer.makeConnection();
            DatabaseQuerier db = new DatabaseQuerier(con);
            ApplicationsResponse resp = new ApplicationsResponse(db.getApplications(session.loginUsername.name));
            byte[] payLoad = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
            session.Client.Client.Send(payLoad);
            return true;
        }

        private static bool handleNewApp(byte [] message, ClientSession session)
        {
            MessageDeserializer ds = new MessageDeserializer(message);
            NewAppRequest request = (NewAppRequest)ds.getMessage();
            Console.WriteLine(request.application.Usernames[0].name);
            Console.WriteLine(request.application.Usernames[0].password);
            var con = databaseInitializer.makeConnection();
            DatabaseQuerier db = new DatabaseQuerier(con);
            db.addUsername(request.application, session.loginUsername.name);
            NewAppResponse resp = new NewAppResponse(request.application);
            byte[] payLoad = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
            session.Client.Client.Send(payLoad);
            return true;
        }

        private static bool handlePassword(byte [] message, ClientSession session)
        {
            MessageDeserializer ds = new MessageDeserializer(message);
            PasswordRequest request = (PasswordRequest)ds.getMessage();
            Console.WriteLine(request.application.Usernames[0].name);
            var con = databaseInitializer.makeConnection();
            DatabaseQuerier db = new DatabaseQuerier(con);
            var encryptedPw = db.getPassword(request.application.Name,
                request.application.Usernames[0].name, session.loginUsername.name);
            request.application.Usernames[0].password = encryptedPw;
            PasswordResponse resp = new PasswordResponse(request.application);
            byte[] payLoad = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
            session.Client.Client.Send(payLoad);
            return true;
        }

        private static bool handleDeleteUsername(byte[] message, ClientSession session)
        {
            MessageDeserializer ds = new MessageDeserializer(message);
            DeleteUsernameRequest request = (DeleteUsernameRequest)ds.getMessage();
            var app = request.application;

            const string TraceFormat = @"DeleteUsernameRequest received:
- Application: {0}
- Category: {1}
- Username: {2}";
            Trace.WriteLine(string.Format(TraceFormat, app.Name, app.Type, app.Usernames[0].name), "Server");

            var con = databaseInitializer.makeConnection();
            DatabaseQuerier db = new DatabaseQuerier(con);
            db.removeUsername(app, session.loginUsername.name);

            var response = new DeleteUsernameResponse(app);
            byte[] payload = MessageUtils.SerializeMessage(response).GetAwaiter().GetResult();
            session.Client.Client.Send(payload);
            return false;
        }

        private static bool handleChangeUserPassword(byte[] message, ClientSession session)
        {
            MessageDeserializer ds = new MessageDeserializer(message);
            ChangeUserPasswordRequest request = (ChangeUserPasswordRequest)ds.getMessage();
            var plainTextPw = request.plainTextPassword;
            var con = databaseInitializer.makeConnection();
            DatabaseQuerier db = new DatabaseQuerier(con);
            bool success = db.changeUserPassword(session.loginUsername.name, request.plainTextPassword);
            ChangeUserPasswordResponse response = new ChangeUserPasswordResponse(success);
            byte[] payload = MessageUtils.SerializeMessage(response).GetAwaiter().GetResult();
            session.Client.Client.Send(payload);
            return true;
        }

        private static bool handleCreateNewUser(byte[] message, ClientSession session)
        {
            bool success = false;
            MessageDeserializer ds = new MessageDeserializer(message);
            CreateNewUserRequest request = (CreateNewUserRequest)ds.getMessage();
            var userInfo = request.username;
            var con = databaseInitializer.makeConnection();
            DatabaseQuerier db = new DatabaseQuerier(con);
            if(session.loginUsername.isAdmin)
            {
                success = db.createNewUser(userInfo.name, userInfo.password, request.makeAdmin);
            }
            CreateNewUserResponse response = new CreateNewUserResponse(success);
            byte[] payload = MessageUtils.SerializeMessage(response).GetAwaiter().GetResult();
            session.Client.Client.Send(payload);
            return success;
        }

        private static bool handleChangeAdmin(byte [] message, ClientSession session)
        {
            MessageDeserializer ds = new MessageDeserializer(message);
            ChangeAdminRequest request = (ChangeAdminRequest)ds.getMessage();
            var userInfo = request.username;
            var con = databaseInitializer.makeConnection();
            DatabaseQuerier db = new DatabaseQuerier(con);
            if(db.isAdmin(userInfo.name))
            {
                if(db.isSuperAdmin(session.loginUsername.name))
                {
                    db.changeAdminPrivileges(userInfo.name, request.makeAdmin);
                }
            }
            if(session.loginUsername.isAdmin)
            {
                db.changeAdminPrivileges(userInfo.name, request.makeAdmin);
            }
            userInfo.isAdmin = db.isAdmin(userInfo.name);
            ChangeAdminResponse response = new ChangeAdminResponse(userInfo);
            byte[] payload = MessageUtils.SerializeMessage(response).GetAwaiter().GetResult();
            session.Client.Client.Send(payload);
            return true;
        }

        private static bool handleSendUsers(byte [] message, ClientSession session)
        {
            bool success = false;
            MessageDeserializer ds = new MessageDeserializer(message);
            SendUsersRequest request = (SendUsersRequest)ds.getMessage();
            var con = databaseInitializer.makeConnection();
            DatabaseQuerier db = new DatabaseQuerier(con);
            if(session.loginUsername.isAdmin)
            {
                var users = db.getUsers();
                SendUsersResponse resp = new SendUsersResponse(users);
                byte[] payload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                session.Client.Client.Send(payload);
                success = true;

            }
            return success;
        }
    }
}
