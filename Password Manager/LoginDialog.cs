using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        public bool IsLoginSuccessful { get; private set; } = false;

        public byte[] keyData { get; private set; } = null;
        public string userName { get { return this.usernameTextBox.Text; } }
        public bool isAdmin { get; private set; } = false;

        public event EventHandler LoginSucceeded;

        private async void loginSubmitButton(object sender, EventArgs e)
        {
            if (keyData == null)
            {
                // Make sure the user has loaded a keyData file. If they haven't, we should ask if they
                // would like us to generate one.
                using (var confirmGenerate = new TaskDialog()
                {
                    Caption = "Generate Key File",
                    InstructionText = "Would you like us to generate a key file for you?",
                    Text = "A key file is needed in order to encrypt your passwords. You'll be " +
                           "asked to save this file somewhere on your PC.\n\n" +
                           "If you already generated a key file, press Browse in the Login window " +
                           "to locate your key file.",
                    OwnerWindowHandle = this.Handle
                })
                {
                    var buttonGenerate = new TaskDialogButton("GenerateButton", "Generate key file")
                    {
                        Default = true
                    };
                    buttonGenerate.Click += async (object theSender, EventArgs theArgs) =>
                    {
                        confirmGenerate.Close();

                        var saveDialog = new SaveFileDialog()
                        {
                            Title = "Save Key File",
                            Filter = "Key File (*.key)|*.key",
                        };
                        if (saveDialog.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }

                        keyData = Shared.CryptManager.generateKey();
                        using (var stream = saveDialog.OpenFile())
                        {
                            await stream.WriteAsync(keyData, 0, keyData.Length);
                        }

                        await SendLoginRequest();
                    };

                    var buttonCancel = new TaskDialogButton("CancelButton", "Cancel");
                    buttonCancel.Click += (object theSender, EventArgs theArgs) =>
                    {
                        confirmGenerate.Close();
                    };

                    confirmGenerate.Controls.Add(buttonGenerate);
                    confirmGenerate.Controls.Add(buttonCancel);
                    confirmGenerate.Show();
                }
            }
            else
            {
                await SendLoginRequest();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonBrowseKeyFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Title = "Browse for Key File",
                Filter = "Key File (*.key)|*.key",
                Multiselect = false
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textboxKeyFilePath.Text = dialog.FileName;
                this.keyData = File.ReadAllBytes(dialog.FileName);
            }
        }

        private void LoginDialog_Load(object sender, EventArgs e)
        {
            string keyFilePath = Properties.Settings.Default.KeyFilePath;
            try
            {
                this.keyData = File.ReadAllBytes(keyFilePath);
            }
            catch (Exception ex)
            {
                this.textboxKeyFilePath.Text = string.Empty;
                Trace.WriteLine(string.Format("Failed to load key file {0}:\n{1}", keyFilePath, ex));
            }
        }

        private async Task SendLoginRequest()
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
                        OwnerWindowHandle = this.Handle,
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
                    this.IsLoginSuccessful = true;
                    this.Close();

                    this.LoginSucceeded?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (SocketException ex)
            {
                using (var taskDialog = new TaskDialog()
                {
                    Caption = "Password Manager",
                    Icon = TaskDialogStandardIcon.Error,
                    InstructionText = "Unable to connect to the server",
                    Text = "Make sure that the server is running, and then try again.",
                    StandardButtons = TaskDialogStandardButtons.Close,
                    OwnerWindowHandle = this.Handle,
                    DetailsCollapsedLabel = "Show error",
                    DetailsExpandedLabel = "Hide error",
                    DetailsExpandedText = string.Format("{1} (0x{0:X8})", ex.ErrorCode, ex.Message)
                })
                {
                    taskDialog.Show();
                }
            }
        }
    }
}
