namespace Password_Manager
{
    partial class PasswordManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordManagerForm));
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.Label();
            this.passwordCopyButton = new System.Windows.Forms.Button();
            this.showpwCheckBox = new System.Windows.Forms.CheckBox();
            this.applicationTreeView = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.newUsernameButton = new System.Windows.Forms.ToolStripButton();
            this.accountToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.editButton = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(476, 34);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.ReadOnly = true;
            this.passwordTextBox.Size = new System.Drawing.Size(375, 20);
            this.passwordTextBox.TabIndex = 0;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // Password
            // 
            this.Password.AutoSize = true;
            this.Password.Location = new System.Drawing.Point(417, 37);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(53, 13);
            this.Password.TabIndex = 1;
            this.Password.Text = "Password";
            // 
            // passwordCopyButton
            // 
            this.passwordCopyButton.Enabled = false;
            this.passwordCopyButton.Location = new System.Drawing.Point(476, 84);
            this.passwordCopyButton.Name = "passwordCopyButton";
            this.passwordCopyButton.Size = new System.Drawing.Size(75, 23);
            this.passwordCopyButton.TabIndex = 2;
            this.passwordCopyButton.Text = "Copy";
            this.passwordCopyButton.UseVisualStyleBackColor = true;
            this.passwordCopyButton.Click += new System.EventHandler(this.passwordCopyButton_Click);
            // 
            // showpwCheckBox
            // 
            this.showpwCheckBox.AutoSize = true;
            this.showpwCheckBox.Location = new System.Drawing.Point(476, 61);
            this.showpwCheckBox.Name = "showpwCheckBox";
            this.showpwCheckBox.Size = new System.Drawing.Size(102, 17);
            this.showpwCheckBox.TabIndex = 4;
            this.showpwCheckBox.Text = "Show Password";
            this.showpwCheckBox.UseVisualStyleBackColor = true;
            this.showpwCheckBox.CheckedChanged += new System.EventHandler(this.showpwCheckBox_CheckedChanged);
            // 
            // applicationTreeView
            // 
            this.applicationTreeView.Location = new System.Drawing.Point(0, 28);
            this.applicationTreeView.Name = "applicationTreeView";
            this.applicationTreeView.Size = new System.Drawing.Size(411, 573);
            this.applicationTreeView.TabIndex = 5;
            this.applicationTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.applicationTreeView_AfterSelect);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.newUsernameButton,
            this.accountToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(863, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton1.Text = "New App";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // newUsernameButton
            // 
            this.newUsernameButton.Image = ((System.Drawing.Image)(resources.GetObject("newUsernameButton.Image")));
            this.newUsernameButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newUsernameButton.Name = "newUsernameButton";
            this.newUsernameButton.Size = new System.Drawing.Size(107, 22);
            this.newUsernameButton.Text = "New Username";
            this.newUsernameButton.Click += new System.EventHandler(this.newUsernameButton_Click);
            // 
            // accountToolStripButton
            // 
            this.accountToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("accountToolStripButton.Image")));
            this.accountToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.accountToolStripButton.Name = "accountToolStripButton";
            this.accountToolStripButton.Size = new System.Drawing.Size(117, 22);
            this.accountToolStripButton.Text = "Account Settings";
            this.accountToolStripButton.ToolTipText = "Account Settings";
            this.accountToolStripButton.Click += new System.EventHandler(this.accountToolStripButton_Click);
            // 
            // editButton
            // 
            this.editButton.Enabled = false;
            this.editButton.Location = new System.Drawing.Point(557, 84);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 7;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(638, 84);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 8;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // PasswordManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 600);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.applicationTreeView);
            this.Controls.Add(this.showpwCheckBox);
            this.Controls.Add(this.passwordCopyButton);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.passwordTextBox);
            this.Name = "PasswordManagerForm";
            this.Text = "Password Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PasswordManagerForm_FormClosed);
            this.Load += new System.EventHandler(this.PasswordManagerForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label Password;
        private System.Windows.Forms.Button passwordCopyButton;
        private System.Windows.Forms.CheckBox showpwCheckBox;
        private System.Windows.Forms.TreeView applicationTreeView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.ToolStripButton newUsernameButton;
        private System.Windows.Forms.ToolStripButton accountToolStripButton;
        private System.Windows.Forms.Button buttonDelete;
    }
}