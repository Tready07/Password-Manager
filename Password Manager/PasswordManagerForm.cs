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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Password_Manager
{
    public partial class PasswordManagerForm : Form
    {
        public PasswordManagerForm()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //TODO: Fix names
            List<TreeNode> rootNodes = getRootNodes();
            NewApplicationForm newAppForm = new NewApplicationForm(rootNodes.Select(node => node.Text).ToArray(),m_secretkey);
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

        private void applicationTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void PasswordManagerForm_Load(object sender, EventArgs e)
        {
            var secretKeyIsPresent = File.Exists("keyFile");
            if (!secretKeyIsPresent)
            {
                using (var dialog = new TaskDialog()
                {
                    Caption = "Browse for Secret Key",
                    InstructionText = "Would you like to browse for your secret key, or generate a new one?",
                    Text = "This key is needed in order to decrypt the passwords you've stored in Password Manager properly.\n\n" +
                           "If you choose to browse for a key file, make sure that this is the same key file that you've " +
                           "used with Password Manager earlier, or your passwords will look funny.",
                    OwnerWindowHandle = this.Handle,
                })
                {
                    var buttonGenerate = new TaskDialogButton("GenerateButton", "Generate new key")
                    {
                        Default = true,
                    };
                    buttonGenerate.Click += (object s, EventArgs ea) =>
                    {
                        var secretkey = Shared.CryptManager.generateKey();
                        File.WriteAllBytes("keyFile", secretkey);
                        m_secretkey = secretkey;

                        dialog.Close();
                    };

                    var buttonBrowse = new TaskDialogButton("BrowseButton", "Browse for key");
                    buttonBrowse.Click += (object s, EventArgs ea) =>
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

                        dialog.Close();
                    };

                    dialog.Controls.Add(buttonGenerate);
                    dialog.Controls.Add(buttonBrowse);
                    dialog.Show();
                }
            }
            else
            {
                m_secretkey = File.ReadAllBytes("keyFile");
            }
            //TODO: send ApplicationsRequests
        }
    }
}
