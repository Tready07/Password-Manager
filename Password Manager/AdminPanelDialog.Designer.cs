namespace Password_Manager
{
    partial class AdminPanelDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelMainInstruction = new System.Windows.Forms.Label();
            this.listviewAccounts = new System.Windows.Forms.ListView();
            this.columnUsername = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnRole = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonAddUser = new System.Windows.Forms.Button();
            this.buttonDeleteUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelMainInstruction
            // 
            this.labelMainInstruction.AutoSize = true;
            this.labelMainInstruction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMainInstruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.labelMainInstruction.Location = new System.Drawing.Point(12, 9);
            this.labelMainInstruction.Name = "labelMainInstruction";
            this.labelMainInstruction.Padding = new System.Windows.Forms.Padding(0, 10, 10, 10);
            this.labelMainInstruction.Size = new System.Drawing.Size(270, 41);
            this.labelMainInstruction.TabIndex = 2;
            this.labelMainInstruction.Text = "Manage user accounts on the server";
            // 
            // listviewAccounts
            // 
            this.listviewAccounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnUsername,
            this.columnRole});
            this.listviewAccounts.Location = new System.Drawing.Point(16, 82);
            this.listviewAccounts.Name = "listviewAccounts";
            this.listviewAccounts.Size = new System.Drawing.Size(684, 425);
            this.listviewAccounts.TabIndex = 3;
            this.listviewAccounts.UseCompatibleStateImageBehavior = false;
            this.listviewAccounts.View = System.Windows.Forms.View.Details;
            // 
            // columnUsername
            // 
            this.columnUsername.Text = "Username";
            this.columnUsername.Width = 200;
            // 
            // columnRole
            // 
            this.columnRole.Text = "Role";
            this.columnRole.Width = 100;
            // 
            // buttonAddUser
            // 
            this.buttonAddUser.Location = new System.Drawing.Point(16, 53);
            this.buttonAddUser.Name = "buttonAddUser";
            this.buttonAddUser.Size = new System.Drawing.Size(93, 23);
            this.buttonAddUser.TabIndex = 4;
            this.buttonAddUser.Text = "&Add...";
            this.buttonAddUser.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteUser
            // 
            this.buttonDeleteUser.Location = new System.Drawing.Point(115, 53);
            this.buttonDeleteUser.Name = "buttonDeleteUser";
            this.buttonDeleteUser.Size = new System.Drawing.Size(93, 23);
            this.buttonDeleteUser.TabIndex = 5;
            this.buttonDeleteUser.Text = "&Remove";
            this.buttonDeleteUser.UseVisualStyleBackColor = true;
            // 
            // AdminPanelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(712, 519);
            this.Controls.Add(this.buttonDeleteUser);
            this.Controls.Add(this.buttonAddUser);
            this.Controls.Add(this.listviewAccounts);
            this.Controls.Add(this.labelMainInstruction);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AdminPanelDialog";
            this.Text = "Admin Panel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMainInstruction;
        private System.Windows.Forms.ListView listviewAccounts;
        private System.Windows.Forms.ColumnHeader columnUsername;
        private System.Windows.Forms.ColumnHeader columnRole;
        private System.Windows.Forms.Button buttonAddUser;
        private System.Windows.Forms.Button buttonDeleteUser;
    }
}