﻿using System;
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
using Networking.Requests;

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
            NewApplicationForm newAppForm = new NewApplicationForm(rootNodes.Select(node => node.Text).ToArray(),M_secretkey);
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

        public bool doesUsernameExist(string category, string appName, string username)
        {
            foreach (TreeNode categoryNode in this.applicationTreeView.Nodes)
            {
                if (categoryNode.Text == category)
                {
                    foreach (TreeNode appNode in categoryNode.Nodes)
                    {
                        if (appNode.Text == appName)
                        {
                            foreach (TreeNode userNode in appNode.Nodes)
                            {
                                if (userNode.Text == username)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }
        
        public void populateTree(Shared.Application[] applications)
        {
            foreach (var app in applications)
            {
                addAppToTree(app);
            }
        }

        public void addAppToTree(Shared.Application application)
        {
            var rootNodes = getRootNodes();
            foreach(var appTypeNode in rootNodes)
            {
                if (application.Type == appTypeNode.Text)
                {
                    foreach(TreeNode appNode in appTypeNode.Nodes)
                    {
                        if (application.Name == appNode.Text)
                        {
                            appNode.Nodes.Add(application.Usernames[0].name);
                            return;
                        }
                    }
                    TreeNode newAppNode = new TreeNode(application.Name);
                    newAppNode.Nodes.Add(application.Usernames[0].name);
                    appTypeNode.Nodes.Add(newAppNode);
                    return;
                }
            }
            TreeNode newRootNode = new TreeNode(application.Type);
            TreeNode applicationNode = newRootNode.Nodes.Add(application.Name);
            applicationNode.Nodes.Add(application.Usernames[0].name);
            this.applicationTreeView.Nodes.Add(newRootNode);
            return;
        }

        

        public void fillPasswordBox(String password)
        {
            this.passwordTextBox.Text = password;
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
    public byte[] M_secretkey { get; private set; }

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
                        M_secretkey = secretkey;

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
                                M_secretkey = File.ReadAllBytes(filename);
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
                M_secretkey = File.ReadAllBytes("keyFile");
            }
            //TODO: send ApplicationsRequests
        }

        private void PasswordManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        async private void applicationTreeView_DoubleClick(object sender, EventArgs e)
        {
            if (this.applicationTreeView.SelectedNode.Nodes.Count == 0)
            {
                Shared.Application app = new Shared.Application();
                var applicationNode = this.applicationTreeView.SelectedNode.Parent;
                app.Name = applicationNode.Text;
                app.Usernames = new Shared.Username[] { new Shared.Username (this.applicationTreeView.SelectedNode.Text) };
                PasswordRequest request = new PasswordRequest(app);
                SocketManager manager = SocketManager.Instance;
                await manager.SendMessage(request);
            }
        }

        private void passwordCopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.passwordTextBox.Text);
        }
    }
}
