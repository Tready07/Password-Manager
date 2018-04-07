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
using System.Data.SQLite;

namespace Password_Manager_Server
{
    public class MessageHandler
    {
        Func<ClientSession, bool>[] functions = {handleLogin,handleApplications,handleNewApp,
            handlePassword, handleDeleteUsername, handleChangeUserPassword, handleCreateNewUser, handleChangeAdmin, handleSendUsers, handleDeleteUser, handleChangeAppType, handleEditApp, handleEditUsername};

        static MessageHandler()
        {
            con = databaseInitializer.makeConnection();
            db = new DatabaseQuerier(con);
        }

        private static SQLiteConnection con;
        private static DatabaseQuerier db;

        public bool handleMessage(ClientSession session)
        {
            int id = session.Reader.Header.Value.ID;
            bool isComplete = functions[id](session);
            return isComplete;
        }

        private static bool handleLogin(ClientSession session)
        {
            LoginRequest msg = (LoginRequest)session.Reader.GetMessage();
            if (db.checkLoginInfo(msg.username.name,msg.username.password))
            {
                session.loginUsername.name = msg.username.name;
                session.loginUsername.isAdmin = db.isAdmin(msg.username.name);
                LoginResponse loginResponse = new LoginResponse(session.loginUsername.isAdmin);
                byte[] loginPayLoad = MessageUtils.SerializeMessage(loginResponse).GetAwaiter().GetResult();
                session.Client.Client.Send(loginPayLoad);
                return true;
            }
            return false;
        }

        private static bool handleApplications(ClientSession session)
        {
            DatabaseQuerier db = new DatabaseQuerier(con);
            ApplicationsResponse resp = new ApplicationsResponse(db.getApplications(session.loginUsername.name));
            byte[] payLoad = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
            session.Client.Client.Send(payLoad);
            return true;
        }

        private static bool handleNewApp(ClientSession session)
        {
            NewAppRequest request = (NewAppRequest)session.Reader.GetMessage();
            Console.WriteLine(request.application.Usernames[0].name);
            Console.WriteLine(request.application.Usernames[0].password);            
            if(db.addUsername(request.application, session.loginUsername.name))
            {
                NewAppResponse resp = new NewAppResponse(request.application);
                byte[] payLoad = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                session.Client.Client.Send(payLoad);
                return true;
            }
            else
            {
                ErrorResponse resp = new ErrorResponse(NewAppResponse.MessageID,
                    "A username for this application already exists");
                byte[] payload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                session.Client.Client.Send(payload);
                return false;
            }
        }

        private static bool handlePassword(ClientSession session)
        {
            PasswordRequest request = (PasswordRequest)session.Reader.GetMessage();
            Console.WriteLine(request.application.Usernames[0].name); 
            if(request.updatePassword)
            {
                db.setPassword(request.application, session.loginUsername.name);
            }      
            var encryptedPw = db.getPassword(request.application.Name,
                request.application.Usernames[0].name, session.loginUsername.name);
            request.application.Usernames[0].password = encryptedPw;
            PasswordResponse resp = new PasswordResponse(request.application);
            byte[] payLoad = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
            session.Client.Client.Send(payLoad);
            return true;
        }

        private static bool handleDeleteUsername(ClientSession session)
        {
            DeleteUsernameRequest request = (DeleteUsernameRequest)session.Reader.GetMessage();
            var app = request.application;

            const string TraceFormat = @"DeleteUsernameRequest received:
- Application: {0}
- Category: {1}
- Username: {2}";
            Trace.WriteLine(string.Format(TraceFormat, app.Name, app.Type, app.Usernames[0].name), "Server");

            db.removeUsername(app, session.loginUsername.name);

            var response = new DeleteUsernameResponse(app);
            byte[] payload = MessageUtils.SerializeMessage(response).GetAwaiter().GetResult();
            session.Client.Client.Send(payload);
            return false;
        }

        private static bool handleChangeUserPassword(ClientSession session)
        {
            ChangeUserPasswordRequest request = (ChangeUserPasswordRequest)session.Reader.GetMessage();
            var plainTextPw = request.plainTextPassword;
            bool success = db.changeUserPassword(session.loginUsername.name, request.plainTextPassword);
            ChangeUserPasswordResponse response = new ChangeUserPasswordResponse(success);
            byte[] payload = MessageUtils.SerializeMessage(response).GetAwaiter().GetResult();
            session.Client.Client.Send(payload);
            return true;
        }

        private static bool handleCreateNewUser(ClientSession session)
        {
            bool success = false;
            CreateNewUserRequest request = (CreateNewUserRequest)session.Reader.GetMessage();
            var userInfo = request.username;
            if(session.loginUsername.isAdmin)
            {
                success = db.createNewUser(userInfo.name, userInfo.password, request.makeAdmin);
                if (!success)
                {
                    ErrorResponse resp = new ErrorResponse(CreateNewUserResponse.MessageID,
                        "Another user with this username already exists");
                    byte[] respPayload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                    session.Client.Client.Send(respPayload);
                    return false;
                }
            }
            CreateNewUserResponse response = new CreateNewUserResponse(success);
            byte[] payload = MessageUtils.SerializeMessage(response).GetAwaiter().GetResult();
            session.Client.Client.Send(payload);
            return success;
        }

        private static bool handleChangeAdmin(ClientSession session)
        {
            ChangeAdminRequest request = (ChangeAdminRequest)session.Reader.GetMessage();
            var userInfo = request.username;
            if(db.isSuperAdmin(userInfo.name))
            {
                ErrorResponse resp = new ErrorResponse(DeleteUsernameResponse.MessageID,
                    "You can't demote yourself, silly.");
                byte[] respPayload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                session.Client.Client.Send(respPayload);
                return false;
            }
            if(db.isAdmin(userInfo.name))
            {
                if(db.isSuperAdmin(session.loginUsername.name))
                {
                    db.changeAdminPrivileges(userInfo.name, request.makeAdmin);
                }
                else
                {
                    ErrorResponse resp = new ErrorResponse(DeleteUsernameResponse.MessageID,
                        "You don't have permission to change this user's role.");
                    byte[] respPayload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                    session.Client.Client.Send(respPayload);
                    return false;
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

        private static bool handleSendUsers(ClientSession session)
        {
            bool success = false;
            SendUsersRequest request = (SendUsersRequest)session.Reader.GetMessage();
            if (session.loginUsername.isAdmin)
            {
                var users = db.getUsers();
                SendUsersResponse resp = new SendUsersResponse(users);
                byte[] payload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                session.Client.Client.Send(payload);
                success = true;

            }
            return success;
        }

        private static bool handleDeleteUser(ClientSession session)
        {
            bool success = false;
            DeleteUserRequest request = (DeleteUserRequest)session.Reader.GetMessage();
            if (db.isSuperAdmin(request.username))
            {
                ErrorResponse resp = new ErrorResponse(DeleteUsernameResponse.MessageID,
                    "You do not have permission to delete this user");
                byte[] payload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                session.Client.Client.Send(payload);
                return false;
            }

            if (db.isAdmin(session.loginUsername.name))
            {
                if(!db.isAdmin(request.username))
                {
                    db.deleteUser(request.username);
                    DeleteUserResponse resp = new DeleteUserResponse(request.username);
                    byte[] payload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                    session.Client.Client.Send(payload);
                    success = true;
                }
                else if (db.isSuperAdmin(session.loginUsername.name))
                {
                    db.deleteUser(request.username);
                    DeleteUserResponse resp = new DeleteUserResponse(request.username);
                    byte[] payload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                    session.Client.Client.Send(payload);
                    success = true;
                }
                else
                {
                    ErrorResponse resp = new ErrorResponse(DeleteUsernameResponse.MessageID,
                        "You do not have permission to delete this user");
                    byte[] payload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                    session.Client.Client.Send(payload);
                    success = false;
                }
            }
            return success;
        }

        private static bool handleChangeAppType(ClientSession session)
        {
            bool success = true;
            ChangeAppTypeRequest request = (ChangeAppTypeRequest)session.Reader.GetMessage();
            var applications = request.apps;
            foreach(var app in applications)
            {
                success &= db.changeAppType(app, session.loginUsername.name);
            }
            //TODO: check success and throw error?
            ChangeAppTypeResponse resp = new ChangeAppTypeResponse(success);
            byte[] payload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
            session.Client.Client.Send(payload);

            return success;
        }

        private static bool handleEditApp(ClientSession session)
        {
            EditApplicationRequest request = (EditApplicationRequest)session.Reader.GetMessage();
            if (db.editApp(request.AppToEdit, request.NewAppName, request.NewAppType, session.loginUsername.name))
            {
                EditApplicationResponse resp = new EditApplicationResponse(true);
                byte[] payload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                session.Client.Client.Send(payload);
                return true;
            }
            else
            {
                ErrorResponse resp = new ErrorResponse(DeleteUsernameResponse.MessageID,
                    "Unknown error");
                byte[] payload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                session.Client.Client.Send(payload);
                return false;
            }
         
        }

        private static bool handleEditUsername(ClientSession session)
        {
            EditUsernameRequest request = (EditUsernameRequest)session.Reader.GetMessage();
            if(db.changeUsername(request.app,request.NewUsername,session.loginUsername.name))
            {
                EditUsernameResponse resp = new EditUsernameResponse(true);
                byte[] payload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                session.Client.Client.Send(payload);
                return true;
            }
            else
            {
                ErrorResponse resp = new ErrorResponse(EditUsernameResponse.MessageID, "Error in changing username. Does it already exist?");
                byte[] payload = MessageUtils.SerializeMessage(resp).GetAwaiter().GetResult();
                session.Client.Client.Send(payload);
                return false;
            }
        }
    }
}
