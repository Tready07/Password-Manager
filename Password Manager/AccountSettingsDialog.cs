using System;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Networking.Requests;
using Networking.Responses;

namespace Password_Manager
{
    public partial class AccountSettingsDialog : Form
    {
        public AccountSettingsDialog()
        {
            InitializeComponent();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            using (var dialog = new TaskDialog()
            {
                Caption = "Delete Account",
                InstructionText = "Are you sure you want to delete your account?",
                Icon = TaskDialogStandardIcon.Warning
            })
            {
                var deleteButton = new TaskDialogButton("DeleteButton", "Delete account");
                deleteButton.Click += (object s, EventArgs ea) =>
                {
                    // TODO: Delete account request.
                    dialog.Close();
                };

                var cancelButton = new TaskDialogButton("CancelButton", "Don't delete");
                cancelButton.Default = true;
                cancelButton.Click += (object s, EventArgs ea) =>
                {
                    dialog.Close();
                };

                dialog.Controls.Add(deleteButton);
                dialog.Controls.Add(cancelButton);
                dialog.Show();
            }
        }

        private async void buttonUpdatePassword_Click(object sender, EventArgs e)
        {
            var request = new ChangeUserPasswordRequest(this.textboxPassword.Text);
            var resp = await SocketManager.Instance.SendRequest<ChangeUserPasswordResponse>(request);

            string mainInstruction = null;
            string text = null;
            TaskDialogStandardIcon icon = TaskDialogStandardIcon.None;

            if (resp.isSuccessful)
            {
                mainInstruction = "Your account password has been changed";
                icon = TaskDialogStandardIcon.Information;
            }
            else
            {
                mainInstruction = "Your account password could not be changed";
                text = "Please try again later.";
                icon = TaskDialogStandardIcon.Error;
            }

            using (var dialog = new TaskDialog()
            {
                Caption = "Change Password",
                InstructionText = mainInstruction,
                Text = text,
                Icon = icon
            })
            {
                dialog.Show();
            }
        }
    }
}
