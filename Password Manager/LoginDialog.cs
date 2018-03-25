using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Networking;
using Networking.Requests;
using Networking.Responses;

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

            LoginResponse resp = await sktMngr.SendRequest<LoginResponse>(msg);
            if (resp == null)
            {
                using (var taskDialog = new TaskDialog()
                {
                    Caption = "Password Manager",
                    Icon = TaskDialogStandardIcon.Error,
                    InstructionText = "Unable to log in",
                    Text = "Make sure the username and password you've entered is correct, and then try again.",
                    StandardButtons = TaskDialogStandardButtons.Close
                })
                {
                    taskDialog.Show();
                }
                return;
            }
        }

        private void LoginDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
