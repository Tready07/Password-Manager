﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
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

        public bool isAdmin { get; private set; } = false;
        public bool isLoginSuccess { get; private set; } = false;

        private async void loginSubmitButton(object sender, EventArgs e)
        { 
            Console.WriteLine("The Submit button was clicked!");
            String username = this.usernameTextBox.Text;
            String password = this.passwordTextBox.Text;
            Shared.Username user = new Shared.Username(username, password);
            LoginRequest msg = new LoginRequest(user);
            SocketManager sktMngr = SocketManager.Instance;

            try
            {
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
                }
                else
                {
                    this.isAdmin = resp.isAdmin;
                    this.isLoginSuccess = true;
                    this.Close();
                }
            }
            catch (SocketException ex)
            {
                using (var taskDialog = new TaskDialog()
                {
                    Caption = "Password Manager",
                    Icon = TaskDialogStandardIcon.Error,
                    InstructionText = "Unable to connect to the server",
                    Text = "Make sure that the server is running on the specified host address and port, and then try again.",
                    StandardButtons = TaskDialogStandardButtons.Close,
                    DetailsCollapsedLabel = "Show error",
                    DetailsExpandedLabel = "Hide error",
                    DetailsExpandedText = string.Format("{1} (0x{0:X8})", ex.ErrorCode, ex.Message)
                })
                {
                    taskDialog.Show();
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
