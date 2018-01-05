using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Password_Manager
{
    public partial class PasswordManagerForm : Form
    {
        public PasswordManagerForm()
        {
            InitializeComponent();
            var secretKeyIsPresent = File.Exists("keyFile");
            if (!secretKeyIsPresent)
            {   //TODO: Handle case where the file is encrypted.        
                DialogResult result = MessageBox.Show("Please select YES if you already have a secretkey file you would like to use or" +
                 " select no if you want it to be generated for you and stored locally", "Secret Key", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    fileDialog.FilterIndex = 2;
                    fileDialog.RestoreDirectory = true;
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            string filename = fileDialog.FileName;
                            m_secretkey = File.ReadAllBytes(filename);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    var secretkey = Shared.CryptManager.generateKey();
                    File.WriteAllBytes("keyFile", secretkey);
                    m_secretkey = secretkey;
                }
            }
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            List<TreeNode> rootNodes = getRootNodes();
            NewApplicationForm newAppForm = new NewApplicationForm(rootNodes.Select(node => node.Text).ToArray());
            newAppForm.Show();
        }

        public List<TreeNode> getRootNodes()
        {
            List<TreeNode> rootNodes = new List<TreeNode>();
            foreach(TreeNode node in this.applicationTreeView.Nodes)
            {
                rootNodes.Add(node);
            }

            return rootNodes;
        }
        
        public void addRootNodes(String[] nodes)
        {
            foreach(var node in nodes)
            {
                this.applicationTreeView.Nodes.Add(node);
            }
        }
        
        public void populateTree(Shared.Application[] applications)
        {
            var rootNodes = getRootNodes();
            foreach (var app in applications)
            {
                foreach (var node in rootNodes)
                {
                    if (app.Type == node.Text)
                    {
                        var appNode = node.Nodes.Add(app.Name);
                        appNode.Nodes.AddRange(app.Usernames.Select(username => new TreeNode(username.name)).ToArray());
                        break;
                    }
                }
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if(this.passwordTextBox.ReadOnly)
            {
                this.passwordTextBox.ReadOnly = false;
                this.editButton.Text = "Submit";
            }
            else
            {
                this.passwordTextBox.ReadOnly = true;
                this.editButton.Text = "Edit";
            }
        }
    byte[] m_secretkey;

        private void showpwCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(this.passwordTextBox.UseSystemPasswordChar)
            {
                this.passwordTextBox.UseSystemPasswordChar = false;
            }
            else
            {
                this.passwordTextBox.UseSystemPasswordChar = true;
            }           
        }
    }
}
