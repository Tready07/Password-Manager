namespace Password_Manager
{
    partial class LoginDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginDialog));
            this.submitButton = new System.Windows.Forms.Button();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.serverPort = new System.Windows.Forms.NumericUpDown();
            this.labelServer = new System.Windows.Forms.Label();
            this.labelMainInstruction = new System.Windows.Forms.Label();
            this.labelInstruction = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelKeyFile = new System.Windows.Forms.Label();
            this.textboxKeyFilePath = new System.Windows.Forms.TextBox();
            this.buttonBrowseKeyFile = new System.Windows.Forms.Button();
            this.serverAddressTextBox = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.serverPort)).BeginInit();
            this.SuspendLayout();
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(263, 238);
            this.submitButton.Margin = new System.Windows.Forms.Padding(3, 20, 3, 10);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(84, 25);
            this.submitButton.TabIndex = 9;
            this.submitButton.Text = "&Login";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.loginSubmitButton);
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Password_Manager.Properties.Settings.Default, "Username", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.usernameTextBox.Location = new System.Drawing.Point(87, 128);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(341, 23);
            this.usernameTextBox.TabIndex = 6;
            this.usernameTextBox.Text = global::Password_Manager.Properties.Settings.Default.Username;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(87, 157);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(341, 23);
            this.passwordTextBox.TabIndex = 8;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(8, 131);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.usernameLabel.Size = new System.Drawing.Size(73, 15);
            this.usernameLabel.TabIndex = 5;
            this.usernameLabel.Text = "&Username:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(11, 160);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.passwordLabel.Size = new System.Drawing.Size(70, 15);
            this.passwordLabel.TabIndex = 7;
            this.passwordLabel.Text = "&Password:";
            // 
            // serverPort
            // 
            this.serverPort.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Password_Manager.Properties.Settings.Default, "Port", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.serverPort.Location = new System.Drawing.Point(371, 99);
            this.serverPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.serverPort.Name = "serverPort";
            this.serverPort.Size = new System.Drawing.Size(57, 23);
            this.serverPort.TabIndex = 4;
            this.serverPort.Value = global::Password_Manager.Properties.Settings.Default.Port;
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(29, 102);
            this.labelServer.Name = "labelServer";
            this.labelServer.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labelServer.Size = new System.Drawing.Size(52, 15);
            this.labelServer.TabIndex = 2;
            this.labelServer.Text = "&Server:";
            // 
            // labelMainInstruction
            // 
            this.labelMainInstruction.AutoSize = true;
            this.labelMainInstruction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMainInstruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.labelMainInstruction.Location = new System.Drawing.Point(7, 9);
            this.labelMainInstruction.Name = "labelMainInstruction";
            this.labelMainInstruction.Padding = new System.Windows.Forms.Padding(10);
            this.labelMainInstruction.Size = new System.Drawing.Size(288, 41);
            this.labelMainInstruction.TabIndex = 0;
            this.labelMainInstruction.Text = "Log in to Password Manager to begin";
            // 
            // labelInstruction
            // 
            this.labelInstruction.Location = new System.Drawing.Point(8, 50);
            this.labelInstruction.Name = "labelInstruction";
            this.labelInstruction.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.labelInstruction.Size = new System.Drawing.Size(420, 38);
            this.labelInstruction.TabIndex = 1;
            this.labelInstruction.Text = "Connect to a server, enter your username and your password, and press Login to co" +
    "ntinue.";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(353, 238);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 25);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "E&xit";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // labelKeyFile
            // 
            this.labelKeyFile.AutoSize = true;
            this.labelKeyFile.Location = new System.Drawing.Point(31, 189);
            this.labelKeyFile.Name = "labelKeyFile";
            this.labelKeyFile.Size = new System.Drawing.Size(50, 15);
            this.labelKeyFile.TabIndex = 11;
            this.labelKeyFile.Text = "Key File:";
            // 
            // textboxKeyFilePath
            // 
            this.textboxKeyFilePath.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textboxKeyFilePath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Password_Manager.Properties.Settings.Default, "KeyFilePath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textboxKeyFilePath.Location = new System.Drawing.Point(87, 186);
            this.textboxKeyFilePath.Name = "textboxKeyFilePath";
            this.textboxKeyFilePath.ReadOnly = true;
            this.textboxKeyFilePath.Size = new System.Drawing.Size(179, 23);
            this.textboxKeyFilePath.TabIndex = 12;
            this.textboxKeyFilePath.Text = global::Password_Manager.Properties.Settings.Default.KeyFilePath;
            // 
            // buttonBrowseKeyFile
            // 
            this.buttonBrowseKeyFile.Location = new System.Drawing.Point(272, 185);
            this.buttonBrowseKeyFile.Name = "buttonBrowseKeyFile";
            this.buttonBrowseKeyFile.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseKeyFile.TabIndex = 13;
            this.buttonBrowseKeyFile.Text = "&Browse...";
            this.buttonBrowseKeyFile.UseVisualStyleBackColor = true;
            this.buttonBrowseKeyFile.Click += new System.EventHandler(this.buttonBrowseKeyFile_Click);
            // 
            // serverAddressTextBox
            // 
            this.serverAddressTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Password_Manager.Properties.Settings.Default, "Host", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.serverAddressTextBox.Location = new System.Drawing.Point(87, 99);
            this.serverAddressTextBox.Name = "serverAddressTextBox";
            this.serverAddressTextBox.Size = new System.Drawing.Size(278, 23);
            this.serverAddressTextBox.TabIndex = 3;
            this.serverAddressTextBox.Text = global::Password_Manager.Properties.Settings.Default.Host;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(353, 186);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 14;
            this.buttonClear.Text = "&Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // LoginDialog
            // 
            this.AcceptButton = this.submitButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(440, 275);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonBrowseKeyFile);
            this.Controls.Add(this.textboxKeyFilePath);
            this.Controls.Add(this.labelKeyFile);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.labelInstruction);
            this.Controls.Add(this.labelMainInstruction);
            this.Controls.Add(this.serverPort);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.serverAddressTextBox);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.submitButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.LoginDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.serverPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox serverAddressTextBox;
        private System.Windows.Forms.NumericUpDown serverPort;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label labelMainInstruction;
        private System.Windows.Forms.Label labelInstruction;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelKeyFile;
        private System.Windows.Forms.TextBox textboxKeyFilePath;
        private System.Windows.Forms.Button buttonBrowseKeyFile;
        private System.Windows.Forms.Button buttonClear;
    }
}

