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
using Networking.Requests;
using Networking.Responses;
using System.Security.Cryptography;

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
            NewApplicationForm newAppForm = new NewApplicationForm(rootNodes.Select(node => node.Text).ToArray(),M_secretkey, this);
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

            TreeNode theAppTypeNode = null;
            TreeNode theAppNode = null;

            // First, search for the existing app type node and the app node in the TreeView.
            foreach (var appTypeNode in rootNodes)
            {
                if (application.Type == appTypeNode.Text)
                {
                    theAppTypeNode = appTypeNode;

                    foreach (TreeNode appNode in appTypeNode.Nodes)
                    {
                        if (application.Name == appNode.Text)
                        {
                            theAppNode = appNode;
                            break;
                        }
                    }
                }
            }

            // Create the root node (i.e., the app type node) if our search ended up fruitless
            if (theAppTypeNode == null)
            {
                theAppTypeNode = new TreeNode(application.Type);
                theAppTypeNode.ImageKey = "AppType";
                theAppTypeNode.SelectedImageKey = "AppType";
                this.applicationTreeView.Nodes.Add(theAppTypeNode);
            }

            // Create the app node if our search ended up fruitless
            if (theAppNode == null)
            {
                theAppNode = new TreeNode(application.Name);
                theAppNode.ImageKey = "AppName";
                theAppNode.SelectedImageKey = "AppName";
                theAppTypeNode.Nodes.Add(theAppNode);
            }

            // Now add the username underneath theAppNode
            foreach (var username in application.Usernames)
            {
                var userNode = theAppNode.Nodes.Add(username.name);
                userNode.ImageKey = "AppUser";
                userNode.SelectedImageKey = "AppUser";
            }
        }

        public void fillPasswordBox(String password)
        {
            this.passwordTextBox.Text = password;
        }

        public void deleteUsername(Shared.Application appUser)
        {
            var rootNodes = getRootNodes();
            foreach (var appTypeNode in rootNodes)
            {
                if (appUser.Type == appTypeNode.Text)
                {
                    foreach (TreeNode appNode in appTypeNode.Nodes)
                    {
                        if (appUser.Name == appNode.Text)
                        {
                            foreach (TreeNode userNode in appNode.Nodes)
                            {
                                if (appUser.Usernames[0].name == userNode.Text)
                                {
                                    userNode.Remove();

                                    // While we're add it, delete the app and the app type if this
                                    // was the only username and app remaining.
                                    if (appNode.Nodes.Count == 0)
                                    {
                                        appNode.Remove();
                                    }

                                    if (appTypeNode.Nodes.Count == 0)
                                    {
                                        appTypeNode.Remove();
                                    }

                                    break;
                                }
                            }
                        }
                    }
                }
            }

            // Reset the selection, in case the username we've deleted is the currently selected node
            if (this.applicationTreeView.SelectedNode == null)
            {
                this.passwordTextBox.Enabled = false;
                this.showpwCheckBox.Enabled = false;
                this.buttonDelete.Enabled = false;
                this.passwordCopyButton.Enabled = false;
                this.editButton.Enabled = false;
                this.passwordTextBox.Text = string.Empty;
            }
        }

        private async void editButton_Click(object sender, EventArgs e)
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

                // Assumption: The selected node is capable of having a parent (since it's
                // the app node).
                var selectedNode = this.applicationTreeView.SelectedNode;
                var username = selectedNode.Text;
                var appName = selectedNode.Parent.Text;
                var appType = selectedNode.Parent.Parent.Text;

                var encryptedPw = Shared.CryptManager.encrypt(this.passwordTextBox.Text, M_secretkey);

                var app = new Shared.Application(appName, new Shared.Username[] { new Shared.Username(username, encryptedPw) }, appType);
                var request = new PasswordRequest(app);
                request.updatePassword = true;

                var response = await SocketManager.Instance.SendRequest<PasswordResponse>(request);
                var password = Shared.CryptManager.decrypt(response.application.Usernames[0].password, M_secretkey);
                this.fillPasswordBox(password);
            }
        }

        public byte[] M_secretkey { get; set; }

        private Shared.Username loggedInUser;
        public Shared.Username LoggedInUser
        {
            get => this.loggedInUser;
            set
            {
                this.loggedInUser = value;
                this.adminPanelButton.Visible = this.loggedInUser.isAdmin;
            }
        }

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

        private async void PasswordManagerForm_Load(object sender, EventArgs e)
        {
            ApplicationsRequest request = new ApplicationsRequest();
            var response = await SocketManager.Instance.SendRequest<ApplicationsResponse>(request);
            this.populateTree(response.applications);
        }

        private void PasswordManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void passwordCopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.passwordTextBox.Text);
        }

        private void newUsernameButton_Click(object sender, EventArgs e)
        {
            string appType = null;
            string appName = null;
            var selectedNode = this.applicationTreeView.SelectedNode;

            List<TreeNode> rootNodes = getRootNodes();
            var newAppForm = new NewApplicationForm(rootNodes.Select(node => node.Text).ToArray(),
                                                    M_secretkey, this);

            if (selectedNode != null)
            {
                switch (selectedNode.Level)
                {
                    case 0:
                        appType = selectedNode.Text;
                        break;
                    case 1:
                        appType = selectedNode.Parent.Text;
                        appName = selectedNode.Text;
                        break;
                    case 2:
                        appType = selectedNode.Parent.Parent.Text;
                        appName = selectedNode.Parent.Text;
                        break;
                }

                //TODO: Fix names
                newAppForm.appName = appName;
                newAppForm.appType = appType;
            }

            newAppForm.Show();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            using (var dialog = new TaskDialog()
            {
                Caption = "Delete Account",
                InstructionText = "Are you sure you want to delete this account?",
                Icon = TaskDialogStandardIcon.Warning
            })
            {
                var buttonDelete = new TaskDialogButton("DeleteButton", "Delete user account")
                {
                    Default = true,
                };
                buttonDelete.Click += async (object s, EventArgs ea) =>
                {
                    // Assumption: The selected node is capable of having a parent (since it's
                    // the app node).
                    var selectedNode = this.applicationTreeView.SelectedNode;
                    var username = selectedNode.Text;
                    var appName = selectedNode.Parent.Text;
                    var appType = selectedNode.Parent.Parent.Text;

                    Shared.Application app = new Shared.Application();
                    var applicationNode = this.applicationTreeView.SelectedNode.Parent;
                    app.Name = appName;
                    app.Type = appType;
                    app.Usernames = new Shared.Username[] { new Shared.Username(username) };

                    DeleteUsernameRequest request = new DeleteUsernameRequest(app);
                    var response = await SocketManager.Instance.SendRequest<DeleteUsernameResponse>(request);
                    this.deleteUsername(response.application);

                    dialog.Close();
                };

                var buttonCancel = new TaskDialogButton("DontDeleteButton", "Don't delete");
                buttonCancel.Click += (object s, EventArgs ea) =>
                {
                    dialog.Close();
                };

                dialog.Controls.Add(buttonDelete);
                dialog.Controls.Add(buttonCancel);
                dialog.Show();
            }
        }

        private async void applicationTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool isAppSelected = e.Node.Level == 1;
            bool isUserSelected = e.Node.Level == 2;

            this.newUsernameButton.Enabled = isAppSelected;
            this.toolstripEditButton.Enabled = isAppSelected;

            if (isUserSelected)
            {
                Shared.Application app = new Shared.Application();
                var applicationNode = this.applicationTreeView.SelectedNode.Parent;
                app.Name = applicationNode.Text;
                app.Usernames = new Shared.Username[] { new Shared.Username(this.applicationTreeView.SelectedNode.Text) };

                try
                {
                    PasswordRequest request = new PasswordRequest(app);
                    var response = await SocketManager.Instance.SendRequest<PasswordResponse>(request);
                    var password = Shared.CryptManager.decrypt(response.application.Usernames[0].password, M_secretkey);
                    this.fillPasswordBox(password);

                    this.passwordTextBox.Enabled = true;
                    this.showpwCheckBox.Enabled = true;
                    this.buttonDelete.Enabled = true;
                    this.editButton.Enabled = true;
                    this.passwordCopyButton.Enabled = true;
                }
                catch (CryptographicException ex)
                {
                    using (var dialog = new TaskDialog()
                    {
                        Caption = "Cannot Decrypt Password",
                        Icon = TaskDialogStandardIcon.Error,
                        InstructionText = "Unable to decrypt the password for this account",
                        Text = "Make sure that the key file currently in use is the same key " +
                               "used to encrypt your passwords previously.",
                        StandardButtons = TaskDialogStandardButtons.Close,
                        DetailsExpandedText = ex.Message,
                        DetailsExpandedLabel = "Hide error",
                        DetailsCollapsedLabel = "Show error",
                        OwnerWindowHandle = this.Handle
                    })
                    {
                        dialog.Show();
                    }
                }
            }
            else
            {
                this.passwordTextBox.Enabled = false;
                this.showpwCheckBox.Enabled = false;
                this.buttonDelete.Enabled = false;
                this.editButton.Enabled = false;
                this.passwordCopyButton.Enabled = false;
                this.passwordTextBox.Text = string.Empty;
            }
        }

        private void accountToolStripButton_Click(object sender, EventArgs e)
        {
            var accountSettingsDialog = new AccountSettingsDialog();
            accountSettingsDialog.ShowDialog(this);
        }

        private void adminPanelButton_Click(object sender, EventArgs e)
        {
            var adminPanelDialog = new AdminPanelDialog();
            adminPanelDialog.ShowDialog(this);
        }

        private void applicationTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var draggedNode = (TreeNode)e.Item;
            if (draggedNode.Level == 1)
            {
                //it is an application
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void applicationTreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private async void applicationTreeView_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode treenode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            Point targetPoint = applicationTreeView.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = applicationTreeView.GetNodeAt(targetPoint);
            if (treenode.Level - 1 == targetNode.Level)
            {
                var appType = targetNode.Text;
                var appName = treenode.Text;
                var applicationNames = new List<Shared.Username>(treenode.Nodes.Count);
                foreach (TreeNode appNode in treenode.Nodes)
                {
                    applicationNames.Add(new Shared.Username(appNode.Text));
                }

                var app = new Shared.Application(appName, applicationNames.ToArray(), appType);
                var response = await SocketManager.Instance.SendRequest<ChangeAppTypeResponse>(new ChangeAppTypeRequest(app));
                if (response.isSuccess)
                {
                    // Remove the parent node if there's no longer any other app beneath it
                    var parentNode = treenode.Parent;
                    treenode.Remove();
                    if (parentNode.Nodes.Count == 0)
                    {
                        parentNode.Remove();
                    }

                    // Before we add this tree node to the target app type node, check to see if
                    // the target has an existing app beneath it, because if so, we're just going
                    // to merge the two nodes together.
                    var appNode = targetNode.Nodes.Cast<TreeNode>().FirstOrDefault(x => x.Text == treenode.Text);
                    if (appNode == null)
                    {
                        targetNode.Nodes.Add(treenode);
                    }
                    else
                    {
                        // Take my babies.
                        appNode.Nodes.AddRange(treenode.Nodes.Cast<TreeNode>().ToArray());
                    }
                }
                else
                {
                    using (var dialog = new TaskDialog()
                    {
                        Caption = "Cannot Change App Type",
                        InstructionText = "Unable to change the type for this application",
                        Text = "An existing application already exists under this type.",
                        Icon = TaskDialogStandardIcon.Error,
                        StandardButtons = TaskDialogStandardButtons.Close
                    })
                    {
                        dialog.Show();
                    }
                }
            }
        }

        private void applicationTreeView_DragOver(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the mouse position.
            Point targetPoint = applicationTreeView.PointToClient(new Point(e.X, e.Y));

            // Select the node at the mouse position.
            if(applicationTreeView.GetNodeAt(targetPoint).Level == 0)
            {
                applicationTreeView.SelectedNode = applicationTreeView.GetNodeAt(targetPoint);
            }
            
        }

        private async void toolstripEditButton_Click(object sender, EventArgs e)
        {
            // ASSUMPTION: SelectedNode.Level == 1, which should be true since toolstripEditButton
            //             is only enabled if SelectedNode.Level == 1.
            var selectedNode = this.applicationTreeView.SelectedNode;
            var appName = selectedNode.Text;
            var appType = selectedNode.Parent.Text;
            var appUsernames = new Shared.Username[selectedNode.Nodes.Count];
            for (int i = 0; i < appUsernames.Length; i++)
            {
                appUsernames[i] = new Shared.Username(selectedNode.Nodes[i].Text);
            }

            List<TreeNode> rootNodes = getRootNodes();
            var app = new Shared.Application(appName, appUsernames, appType);
            var editDialog = new EditApplicationForm(rootNodes.Select(node => node.Text).ToArray(), app);
            if (editDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var request = new EditApplicationRequest(app);
                    request.NewAppName = editDialog.AppName;
                    request.NewAppType = editDialog.AppType;
                    var response = await SocketManager.Instance.SendRequest<EditApplicationResponse>(request);
                    if (!response.isSuccess)
                    {
                        using (var dialog = new TaskDialog()
                        {
                            Caption = "Cannot Change App Properties",
                            InstructionText = "Unable to change the properties for this app",
                            Text = "Something went wrong. Try again later.",
                            StandardButtons = TaskDialogStandardButtons.Close,
                            Icon = TaskDialogStandardIcon.Error,
                            OwnerWindowHandle = this.Handle
                        })
                        {
                            dialog.Show();
                        }
                    }
                    else
                    {
                        // Clean up the parent app type node if there's no longer any app type there
                        var appTypeNode = selectedNode.Parent;
                        selectedNode.Remove();
                        if (appTypeNode.Nodes.Count == 0)
                        {
                            appTypeNode.Remove();
                        }


                        app.Name = editDialog.AppName;
                        app.Type = editDialog.AppType;
                        this.addAppToTree(app);
                    }
                }
                catch (ResponseException ex)
                {
                    using (var dialog = new TaskDialog()
                    {
                        Caption = "Cannot Change App Properties",
                        InstructionText = "Unable to change the properties for this app",
                        Text = ex.Message,
                        StandardButtons = TaskDialogStandardButtons.Close,
                        Icon = TaskDialogStandardIcon.Error,
                        OwnerWindowHandle = this.Handle
                    })
                    {
                        dialog.Show();
                    }
                }
            }
        }

        private void applicationTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
        }

        private async void applicationTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            
            if(!String.IsNullOrEmpty(e.Label))
            {
                if (e.Node.Level == 0)
                {
                    //GET ALL APPS AND USER NAMES AND SEND CHANGE APPTYPE REQ
                    var appNodes = e.Node.Nodes;
                    List<Shared.Application> apps = new List<Shared.Application>();
                    foreach(TreeNode appNode in appNodes)
                    {
                        Shared.Application app = new Shared.Application();
                        List<Shared.Username> usernames = new List<Shared.Username>();
                        foreach(TreeNode usernameNode in appNode.Nodes)
                        {
                            Shared.Username username = new Shared.Username(usernameNode.Text);
                            usernames.Add(username);
                        }
                        app.Name = appNode.Text;
                        app.Type = e.Label;
                        app.Usernames = usernames.ToArray();
                        apps.Add(app);
                    }
                    var request = new ChangeAppTypeRequest(apps.ToArray());
                    var response = await SocketManager.Instance.SendRequest<ChangeAppTypeResponse>(request);
                    if(response.isSuccess)
                    {
                        e.Node.EndEdit(false);
                        applicationTreeView.LabelEdit = false;
                    }
                }

                else if (e.Node.Level ==1)
                {
                    // SEND EDITAPP REQUEST

                    var usernameNodes = e.Node.Nodes;
                    Shared.Application app = new Shared.Application();
                    List<Shared.Username> usernames = new List<Shared.Username>();
                    foreach(TreeNode usernameNode in usernameNodes)
                    {
                        Shared.Username username = new Shared.Username(usernameNode.Text);
                        usernames.Add(username);
                    }
                    app.Usernames = usernames.ToArray();
                    app.Type = e.Node.Parent.Text;
                    app.Name = e.Node.Text;
                    var request = new EditApplicationRequest(app);
                    request.NewAppName = e.Label;
                    request.NewAppType = app.Type;
                    var response = await SocketManager.Instance.SendRequest<EditApplicationResponse>(request);
                    if(response.isSuccess)
                    {
                        e.Node.EndEdit(false);
                        applicationTreeView.LabelEdit = false;
                    }
                }

                else
                {
                    var oldUsername = e.Node.Text;
                    Shared.Application app = new Shared.Application();
                    Shared.Username[] usernames = new Shared.Username[] { new Shared.Username(e.Node.Text) };
                    app.Usernames = usernames;
                    app.Name = e.Node.Parent.Text;
                    var request = new EditUsernameRequest(app);
                    request.NewUsername = e.Label;

                    try
                    {
                        var response = await SocketManager.Instance.SendRequest<EditUsernameResponse>(request);
                        if (response.isSuccess)
                        {
                            e.Node.EndEdit(false);
                            applicationTreeView.LabelEdit = false;
                        }
                    }

                    catch (ResponseException ex)
                    {
                        e.Node.Text = oldUsername;
                        applicationTreeView.LabelEdit = false;
                        using (var dialog = new TaskDialog()
                        {
                            
                            Caption = "Cannot Rename the Username",
                            InstructionText = "Unable to rename the username. Could the username already exist?",
                            Text = ex.Message,
                            StandardButtons = TaskDialogStandardButtons.Close,
                            Icon = TaskDialogStandardIcon.Error,
                            OwnerWindowHandle = this.Handle
                        })
                        {
                            dialog.Show();

                        }
                    }

                }

            }
        }

        private DateTime? appTreeViewLastClick;
        private TreeNode appTreeViewLastNodeClicked;
        private void applicationTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var selectedNode = applicationTreeView.GetNodeAt(e.X, e.Y);
            if (selectedNode.Level != 2)
            {
                // Is this the first click?
                if (!appTreeViewLastClick.HasValue)
                {
                    appTreeViewLastClick = DateTime.Now;
                    appTreeViewLastNodeClicked = selectedNode;
                }
                else
                {
                    // In order for us to consider the second click to be a rename action, the user:
                    // - Must have done the second click so slow such that Windows wouldn't interpret it as a double-click
                    // - Must be clicking the same node for the second time
                    // - Must have clicked on a TreeNode's label
                    // - Must not be editing the selected node already
                    var delta = (DateTime.Now - appTreeViewLastClick.Value).Duration().TotalMilliseconds;
                    if (delta > SystemInformation.DoubleClickTime && 
                        selectedNode == appTreeViewLastNodeClicked &&
                        !selectedNode.IsEditing &&
                        applicationTreeView.HitTest(e.Location).Location == TreeViewHitTestLocations.Label)
                    {
                        applicationTreeView.LabelEdit = true;
                        selectedNode.BeginEdit();
                    }

                    appTreeViewLastClick = null;
                    appTreeViewLastNodeClicked = null;
                }
            }
        }
    }
    
}
