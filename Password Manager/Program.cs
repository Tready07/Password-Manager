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

            var dialog = new LoginDialog();
            dialog.FormClosed += (object s, FormClosedEventArgs a) =>
            {
                if (dialog.isLoginSuccess)
                {
                    var form = new PasswordManagerForm();
                    form.isAdmin = dialog.isAdmin;
                    form.Show();
                }
                else
                {
                    Application.Exit();
                }
            };
            dialog.Show();

            Application.Run();
        }
    }
}
