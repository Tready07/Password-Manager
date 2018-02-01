using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Networking;
using Networking.Request;

namespace Password_Manager
{
    public partial class LoginDialog : Form
    {
        public LoginDialog()
        {
            InitializeComponent();
        }

        private async void loginSubmitButton(object sender, EventArgs e)
        { 
            Console.WriteLine("The Submit button was clicked!");
            String username = this.usernameTextBox.Text;
            String password = this.passwordTextBox.Text;
            Shared.Username user = new Shared.Username(username, password);
            LoginRequest msg = new LoginRequest(user);
            SocketManager sktMngr = SocketManager.Instance;
            sktMngr.connect(this.serverAddressTextBox.Text, (int)serverPort.Value);

            // If we get disconnected from the server, then that means we entered some bad login
            // information.
            EventHandler badAuthentication = null;
            badAuthentication = (s, ea) =>
            {
                // Disconnect from the Disconnected event so that we're not emitted if we get
                // disconnected again.
                sktMngr.Disconnected -= badAuthentication;

                MessageBox.Show(
                    "Unable to sign in\n\nThe username or password is incorrect.", 
                    "Can't Sign In", 
                    MessageBoxButtons.OK);
            };
            sktMngr.Disconnected += badAuthentication;

            await sktMngr.SendMessage(msg);
        }
    }
}
