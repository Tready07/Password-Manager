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

namespace Password_Manager
{
    public partial class NewApplicationForm : Form
    {
        public NewApplicationForm()
        {
            InitializeComponent();
            this.appTypeComboBox.SelectedIndex = 0;
        }

        public NewApplicationForm(String [] appTypes, byte [] key)
        {
            InitializeComponent();
            this.appTypeComboBox.SelectedIndex = 0;
            foreach (var appType in appTypes)
            {
                this.appTypeComboBox.Items.Insert(0, appType);
            }
            secretKey = key;
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
        }
        byte[] secretKey { get; set; }
    }
}
