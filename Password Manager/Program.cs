using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Password_Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += OnApplicationExit;

            ShowLoginDialog();

            Application.Run();
        }

        private static void OnDisconnect(object s, EventArgs e)
        {
            using (var dialog = new TaskDialog()
            {
                Caption = "Password Manager",
                InstructionText = "You have been disconnected from the server",
                Text = "You'll need to reconnect from the server. The password manager will now close.",
                Icon = TaskDialogStandardIcon.Error
            })
            {
                dialog.Show();

                Application.Exit();
            }
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        static void ShowLoginDialog()
        {
            var loginDialog = new LoginDialog();
            loginDialog.LoginSucceeded += (object sender, EventArgs e) =>
            {
                var passwordManagerForm = new PasswordManagerForm()
                {
                    M_secretkey = loginDialog.keyData,
                    LoggedInUser = new Shared.Username(loginDialog.userName)
                    {
                        isAdmin = loginDialog.isAdmin
                    }
                };
                SocketManager.Instance.Disconnected += OnDisconnect;
                passwordManagerForm.Show();
            };
            loginDialog.FormClosed += (object sender, FormClosedEventArgs e) =>
            {
                if (!loginDialog.IsLoginSuccessful)
                {
                    // Assume that if we didn't log in that we're attempting to close the program.
                    Application.Exit();
                }
            };
            loginDialog.Show();
        }
    }
}
