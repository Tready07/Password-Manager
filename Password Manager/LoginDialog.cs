﻿using System;
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
            await sktMngr.SendMessage(msg);
        }

    }
}
