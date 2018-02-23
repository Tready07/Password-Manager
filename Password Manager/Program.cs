using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Password_Manager
{
    static class Program
    {
        public static PasswordManagerForm passwordForm;
        public static LoginDialog loginDialog;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            passwordForm = new PasswordManagerForm();

            loginDialog = new LoginDialog();
            loginDialog.Show();

            Application.Run();
        }
    }
}
