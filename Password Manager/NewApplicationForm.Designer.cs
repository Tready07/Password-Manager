namespace Password_Manager
{
    partial class NewApplicationForm
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
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.appNameTextBox = new System.Windows.Forms.TextBox();
            this.applicationNameLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.pwTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.appTypeComboBox = new System.Windows.Forms.ComboBox();
            this.appTypeLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(107, 102);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(186, 20);
            this.usernameTextBox.TabIndex = 3;
            // 
            // appNameTextBox
            // 
            this.appNameTextBox.Location = new System.Drawing.Point(107, 60);
            this.appNameTextBox.Name = "appNameTextBox";
            this.appNameTextBox.Size = new System.Drawing.Size(186, 20);
            this.appNameTextBox.TabIndex = 1;
            // 
            // applicationNameLabel
            // 
            this.applicationNameLabel.AutoSize = true;
            this.applicationNameLabel.Location = new System.Drawing.Point(11, 63);
            this.applicationNameLabel.Name = "applicationNameLabel";
            this.applicationNameLabel.Size = new System.Drawing.Size(90, 13);
            this.applicationNameLabel.TabIndex = 0;
            this.applicationNameLabel.Text = "Application Name";
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(15, 105);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(55, 13);
            this.usernameLabel.TabIndex = 2;
            this.usernameLabel.Text = "Username";
            // 
            // pwTextBox
            // 
            this.pwTextBox.Location = new System.Drawing.Point(107, 142);
            this.pwTextBox.Name = "pwTextBox";
            this.pwTextBox.Size = new System.Drawing.Size(186, 20);
            this.pwTextBox.TabIndex = 5;
            this.pwTextBox.UseSystemPasswordChar = true;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(15, 145);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.TabIndex = 4;
            this.passwordLabel.Text = "Password";
            // 
            // appTypeComboBox
            // 
            this.appTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.appTypeComboBox.FormattingEnabled = true;
            this.appTypeComboBox.Items.AddRange(new object[] {
            "Social Media",
            "Email",
            "Banking",
            "Shopping",
            "Other",
            "Add Custom Type"});
            this.appTypeComboBox.Location = new System.Drawing.Point(107, 183);
            this.appTypeComboBox.Name = "appTypeComboBox";
            this.appTypeComboBox.Size = new System.Drawing.Size(117, 21);
            this.appTypeComboBox.TabIndex = 7;
            this.appTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.appTypeComboBox_SelectedIndexChanged);
            // 
            // appTypeLabel
            // 
            this.appTypeLabel.AutoSize = true;
            this.appTypeLabel.Location = new System.Drawing.Point(15, 186);
            this.appTypeLabel.Name = "appTypeLabel";
            this.appTypeLabel.Size = new System.Drawing.Size(86, 13);
            this.appTypeLabel.TabIndex = 6;
            this.appTypeLabel.Text = "Application Type";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(218, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.submitButton);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(107, 222);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // NewApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 300);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.appTypeComboBox);
            this.Controls.Add(this.appTypeLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.applicationNameLabel);
            this.Controls.Add(this.appNameTextBox);
            this.Controls.Add(this.pwTextBox);
            this.Controls.Add(this.usernameTextBox);
            this.Name = "NewApplicationForm";
            this.Text = "New Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox appNameTextBox;
        private System.Windows.Forms.Label applicationNameLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox pwTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.ComboBox appTypeComboBox;
        private System.Windows.Forms.Label appTypeLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cancelButton;
    }
}