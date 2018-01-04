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
            var secretKeyIsPresent = checkForSecretKey();
            DialogResult result = MessageBox.Show("Please select if you already have a secretkey file you would like to use or if you want it to be generated for you" + 
                 " and stored locally", "Secret Key",MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                var secretkey = Shared.CryptManager.generateKey();
                File.WriteAllBytes("keyFile",secretkey);
            }
            else
            {
                //TODO: generate windows explorer have them point us to secret key read in file as secretkey
            }


        }

        bool checkForSecretKey()
        {
            //TODO: Check for secret key
            return false;
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
                this.editButton.Text = "Edit";
            }
            else
            {
                this.passwordTextBox.ReadOnly = true;
                this.editButton.Text = "Edit";
            }
        }
    }
}
