using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Networking.Requests;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Password_Manager
{
    public partial class NewApplicationForm : Form
    {
        private PasswordManagerForm managerForm;

        public NewApplicationForm()
        {
            InitializeComponent();
            this.appTypeComboBox.SelectedIndex = 0;
        }

        public NewApplicationForm(String [] appTypes, byte [] key, PasswordManagerForm managerForm)
        {
            InitializeComponent();
            this.managerForm = managerForm;
            this.appTypeComboBox.SelectedIndex = 0;
            foreach (var appType in appTypes)
            {
                this.appTypeComboBox.Items.Insert(0, appType);
            }
            secretKey = key;
        }

        public string appType
        {
            get { return this.appTypeComboBox.Text; }
            set { this.appTypeComboBox.Text = value; }
        }

        public string appName
        {
            get { return this.appNameTextBox.Text; }
            set { this.appNameTextBox.Text = value; }
        }
        
        private void appTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.Write(this.appTypeComboBox.SelectedItem.ToString());
            if (this.appTypeComboBox.SelectedItem.ToString() == "Add Custom Type")
            {
                this.appTypeComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
            {
                this.appTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private async void submitButton(object sender, EventArgs e)
        {
            if (this.managerForm.doesUsernameExist(this.appTypeComboBox.Text,
                                                   this.appNameTextBox.Text,
                                                   this.usernameTextBox.Text))
            {
                using (var dialog = new TaskDialog()
                {
                    Caption = "Cannot Add Account",
                    InstructionText = "Unable to add this user",
                    Text = "This user already exists in the database. Please enter a different username for this application.",
                    StandardButtons = TaskDialogStandardButtons.Close,
                    Icon = TaskDialogStandardIcon.Error
                })
                {
                    dialog.Show();
                    return;
                }
            }


            Shared.Application app = new Shared.Application();
            String plainTextPw = pwTextBox.Text;
            var encryptedPw = Shared.CryptManager.encrypt(plainTextPw,secretKey);
            Shared.Username username = new Shared.Username(usernameTextBox.Text,encryptedPw);
            Shared.Username[] userName = new Shared.Username[] {username};
            app.Usernames = userName;
            app.Type = appTypeComboBox.Text;
            app.Name = appNameTextBox.Text;
            NewAppRequest request = new NewAppRequest(app);
            SocketManager manager = SocketManager.Instance;
            await manager.SendMessage(request);
            this.Close();
        }

        byte[] secretKey { get; set; }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
