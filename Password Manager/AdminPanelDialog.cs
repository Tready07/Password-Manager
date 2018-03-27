﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Networking.Requests;
using Networking.Responses;

namespace Password_Manager
{
    public partial class AdminPanelDialog : Form
    {
        public AdminPanelDialog()
        {
            InitializeComponent();
        }

        private async void AdminPanelDialog_Load(object sender, EventArgs e)
        {
            var response = await SocketManager.Instance.SendRequest<SendUsersResponse>(new SendUsersRequest());
            foreach (var user in response.users)
            {
                var userItem = new ListViewItem(user.name);
                userItem.SubItems.Add(new ListViewItem.ListViewSubItem()
                {
                    Text = user.isAdmin ? "Administrator" : "Standard"
                });

                this.listviewAccounts.Items.Add(userItem);
            }
        }

        private async void buttonAddUser_Click(object sender, EventArgs e)
        {
            var addUserDialog = new AddUserDialog();
            if (addUserDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            // TODO: Should we check isAdmin via Shared.Username instead of CreateNewUserRequest constructor?
            var username = new Shared.Username(addUserDialog.userName, addUserDialog.password);
            var response = await SocketManager.Instance.SendRequest<CreateNewUserResponse>(new CreateNewUserRequest(username, addUserDialog.isAdmin));
            if (response.isSuccessful)
            {
                var userItem = new ListViewItem(addUserDialog.userName);
                userItem.SubItems.Add(new ListViewItem.ListViewSubItem()
                {
                    Text = addUserDialog.isAdmin ? "Administrator" : "Standard"
                });

                this.listviewAccounts.Items.Add(userItem);
            }
        }

        private void listviewAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool hasSelection = this.listviewAccounts.SelectedItems.Count == 1;
            this.buttonChangeRole.Enabled = hasSelection;
            this.buttonDeleteUser.Enabled = hasSelection;
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            using (var confirmDialog = new TaskDialog()
            {
                Caption = "Delete Account",
                InstructionText = "Are you sure you want to delete this account?",
                Icon = TaskDialogStandardIcon.Warning
            })
            {
                var deleteButton = new TaskDialogButton("DeleteButton", "Delete user");
                deleteButton.Click += (object s, EventArgs a) =>
                {
                    confirmDialog.Close();
                };

                var cancelButton = new TaskDialogButton("CancelButton", "Don't delete");
                cancelButton.Click += (object s, EventArgs a) =>
                {
                    confirmDialog.Close();
                };

                confirmDialog.Controls.Add(deleteButton);
                confirmDialog.Controls.Add(cancelButton);
                confirmDialog.Show();
            }

        }

        private async void buttonChangeRole_Click(object sender, EventArgs e)
        {
            if (this.listviewAccounts.SelectedItems.Count != 1)
            {
                return;
            }

            var selectedItem = this.listviewAccounts.SelectedItems[0];

            var username = new Shared.Username(selectedItem.Text);
            bool isAdmin = selectedItem.SubItems[0].Text != "Administrator";
            var response = await SocketManager.Instance.SendRequest<ChangeAdminResponse>(new ChangeAdminRequest(username, isAdmin));
            selectedItem.SubItems[0].Text = response.username.isAdmin ? "Administrator" : "Standard";
        }
    }
}
