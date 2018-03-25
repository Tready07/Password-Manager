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
            this.applicationTreeView = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.newUsernameButton = new System.Windows.Forms.ToolStripButton();
            this.accountToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.adminPanelButton = new System.Windows.Forms.ToolStripButton();
            this.splitcontainerMain = new System.Windows.Forms.SplitContainer();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.showpwCheckBox = new System.Windows.Forms.CheckBox();
            this.passwordCopyButton = new System.Windows.Forms.Button();
            this.labelPassword = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitcontainerMain)).BeginInit();
            this.splitcontainerMain.Panel1.SuspendLayout();
            this.splitcontainerMain.Panel2.SuspendLayout();
            this.splitcontainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // applicationTreeView
            // 
            this.applicationTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.applicationTreeView.Location = new System.Drawing.Point(0, 0);
            this.applicationTreeView.Name = "applicationTreeView";
            this.applicationTreeView.Size = new System.Drawing.Size(287, 575);
            this.applicationTreeView.TabIndex = 5;
            this.applicationTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.applicationTreeView_AfterSelect);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.newUsernameButton,
            this.accountToolStripButton,
            this.adminPanelButton});
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
            // adminPanelButton
            // 
            this.adminPanelButton.Image = ((System.Drawing.Image)(resources.GetObject("adminPanelButton.Image")));
            this.adminPanelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.adminPanelButton.Name = "adminPanelButton";
            this.adminPanelButton.Size = new System.Drawing.Size(95, 22);
            this.adminPanelButton.Text = "Admin Panel";
            this.adminPanelButton.ToolTipText = "Admin Panel";
            this.adminPanelButton.Click += new System.EventHandler(this.adminPanelButton_Click);
            // 
            // splitcontainerMain
            // 
            this.splitcontainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitcontainerMain.Location = new System.Drawing.Point(0, 25);
            this.splitcontainerMain.Name = "splitcontainerMain";
            // 
            // splitcontainerMain.Panel1
            // 
            this.splitcontainerMain.Panel1.Controls.Add(this.applicationTreeView);
            // 
            // splitcontainerMain.Panel2
            // 
            this.splitcontainerMain.Panel2.Controls.Add(this.buttonDelete);
            this.splitcontainerMain.Panel2.Controls.Add(this.editButton);
            this.splitcontainerMain.Panel2.Controls.Add(this.showpwCheckBox);
            this.splitcontainerMain.Panel2.Controls.Add(this.passwordCopyButton);
            this.splitcontainerMain.Panel2.Controls.Add(this.labelPassword);
            this.splitcontainerMain.Panel2.Controls.Add(this.passwordTextBox);
            this.splitcontainerMain.Size = new System.Drawing.Size(863, 575);
            this.splitcontainerMain.SplitterDistance = 287;
            this.splitcontainerMain.TabIndex = 9;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(477, 62);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 14;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // editButton
            // 
            this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editButton.Enabled = false;
            this.editButton.Location = new System.Drawing.Point(396, 62);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 13;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            // 
            // showpwCheckBox
            // 
            this.showpwCheckBox.AutoSize = true;
            this.showpwCheckBox.Enabled = false;
            this.showpwCheckBox.Location = new System.Drawing.Point(19, 66);
            this.showpwCheckBox.Name = "showpwCheckBox";
            this.showpwCheckBox.Size = new System.Drawing.Size(102, 17);
            this.showpwCheckBox.TabIndex = 12;
            this.showpwCheckBox.Text = "&Show Password";
            this.showpwCheckBox.UseVisualStyleBackColor = true;
            this.showpwCheckBox.CheckedChanged += new System.EventHandler(this.showpwCheckBox_CheckedChanged);
            // 
            // passwordCopyButton
            // 
            this.passwordCopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordCopyButton.Enabled = false;
            this.passwordCopyButton.Location = new System.Drawing.Point(315, 62);
            this.passwordCopyButton.Name = "passwordCopyButton";
            this.passwordCopyButton.Size = new System.Drawing.Size(75, 23);
            this.passwordCopyButton.TabIndex = 11;
            this.passwordCopyButton.Text = "Copy";
            this.passwordCopyButton.UseVisualStyleBackColor = true;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(16, 14);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(56, 13);
            this.labelPassword.TabIndex = 10;
            this.labelPassword.Text = "&Password:";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.Enabled = false;
            this.passwordTextBox.Location = new System.Drawing.Point(19, 35);
            this.passwordTextBox.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.ReadOnly = true;
            this.passwordTextBox.Size = new System.Drawing.Size(533, 20);
            this.passwordTextBox.TabIndex = 9;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // PasswordManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 600);
            this.Controls.Add(this.splitcontainerMain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "PasswordManagerForm";
            this.Text = "Password Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PasswordManagerForm_FormClosed);
            this.Load += new System.EventHandler(this.PasswordManagerForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitcontainerMain.Panel1.ResumeLayout(false);
            this.splitcontainerMain.Panel2.ResumeLayout(false);
            this.splitcontainerMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitcontainerMain)).EndInit();
            this.splitcontainerMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView applicationTreeView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton newUsernameButton;
        private System.Windows.Forms.ToolStripButton accountToolStripButton;
        private System.Windows.Forms.ToolStripButton adminPanelButton;
        private System.Windows.Forms.SplitContainer splitcontainerMain;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.CheckBox showpwCheckBox;
        private System.Windows.Forms.Button passwordCopyButton;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox passwordTextBox;
    }
}