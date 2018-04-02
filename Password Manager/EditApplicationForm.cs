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
    public partial class EditApplicationForm : Form
    {
        public EditApplicationForm(string[] appTypes, Shared.Application app)
        {
            InitializeComponent();

            // Populate the application type combo box with all the types the user has so far
            foreach (var appType in appTypes)
            {
                if (!this.appTypeComboBox.Items.Contains(appType))
                {
                    this.appTypeComboBox.Items.Insert(0, appType);
                }

            }

            this.appNameTextBox.Text = app.Name;
            this.appTypeComboBox.Text = app.Type;
        }

        public string AppType
        {
            get => this.appTypeComboBox.Text;
        }

        public string AppName
        {
            get => this.appNameTextBox.Text;
        }

        private void appTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.appTypeComboBox.SelectedItem.ToString() == "Add Custom Type")
            {
                this.appTypeComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
            {
                this.appTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
