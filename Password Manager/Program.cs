using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            SocketManager.Instance.Disconnected += (object s, EventArgs a) =>
            {
                ShowLoginDialog();
            };

            ShowLoginDialog();

            Application.Run();
        }

        static void ShowLoginDialog()
        {
            var dialog = new LoginDialog();
            dialog.FormClosed += (object s, FormClosedEventArgs a) =>
            {
                if (dialog.isLoginSuccess)
                {
                    var userName = new Shared.Username(dialog.userName);
                    userName.isAdmin = dialog.isAdmin;

                    var form = new PasswordManagerForm();
                    form.LoggedInUser = userName;
                    form.Show();
                }
                else
                {
                    Application.Exit();
                }
            };
            dialog.Show();
        }
    }
}
