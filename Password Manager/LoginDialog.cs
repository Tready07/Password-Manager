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

namespace Password_Manager
{
    public partial class LoginDialog : Form
    {
        public LoginDialog()
        {
            InitializeComponent();
        }

        private void loginSubmitButton(object sender, EventArgs e)
        {
            Console.WriteLine("The Submit button was clicked!");
            String username = this.usernameTextBox.Text;
            String password = this.passwordTextBox.Text;
            Shared.Username user = new Shared.Username(username, password);
            LoginMessage msg = new LoginMessage(user);
            SocketManager sktMngr = SocketManager.Instance;
            sktMngr.connect(this.serverAddressTextBox.Text, (int)serverPort.Value);
            MessageSender msgSender = new MessageSender(sktMngr.socket);
            msgSender.send(msg);
        }

    }
}
