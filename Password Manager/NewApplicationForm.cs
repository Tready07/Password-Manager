using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Password_Manager
{
    public partial class NewApplicationForm : Form
    {
        public NewApplicationForm()
        {
            InitializeComponent();
            this.appTypeComboBox.SelectedIndex = 0;
        }

        public NewApplicationForm(String [] appTypes)
        {
            InitializeComponent();
            this.appTypeComboBox.SelectedIndex = 0;
            foreach (var appType in appTypes)
            {
                this.appTypeComboBox.Items.Insert(0, appType);
            }
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

        private void submitButton(object sender, EventArgs e)
        {
            Shared.Application app = new Shared.Application();
            String plainTextPw = pwTextBox.Text;
            var encryptedPw = Shared.CryptManager.encrypt(plainTextPw,secretKey.ToString());
            Shared.Username username = new Shared.Username(usernameTextBox.Text,encryptedPw);
            Shared.Username[] userName = new Shared.Username[] {username};
            app.Usernames = userName;
            app.Type = appTypeComboBox.SelectedItem.ToString();
            
        }
        byte[] secretKey { get; set; }
    }
}
