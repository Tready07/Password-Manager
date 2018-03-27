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
    public partial class AddUserDialog : Form
    {
        public AddUserDialog()
        {
            InitializeComponent();
        }

        public bool isAdmin
        {
            get => this.checkboxIsAdmin.Checked;
            set => this.checkboxIsAdmin.Checked = value;
        }

        public string userName
        {
            get => this.textboxUsername.Text;
            set => this.textboxUsername.Text = value;
        }

        public string password
        {
            get => this.textboxPassword.Text;
            set => this.textboxPassword.Text = value;
        }

        private void checkboxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            this.textboxPassword.UseSystemPasswordChar = !this.checkboxShowPassword.Checked;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
