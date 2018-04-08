namespace Password_Manager
{
    partial class AccountSettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountSettingsDialog));
            this.labelMainInstruction = new System.Windows.Forms.Label();
            this.groupboxChangePassword = new System.Windows.Forms.GroupBox();
            this.buttonUpdatePassword = new System.Windows.Forms.Button();
            this.textboxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelDeleteAccount = new System.Windows.Forms.Label();
            this.groupboxDeleteAccount = new System.Windows.Forms.GroupBox();
            this.groupboxChangePassword.SuspendLayout();
            this.groupboxDeleteAccount.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelMainInstruction
            // 
            this.labelMainInstruction.AutoSize = true;
            this.labelMainInstruction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMainInstruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.labelMainInstruction.Location = new System.Drawing.Point(8, 9);
            this.labelMainInstruction.Name = "labelMainInstruction";
            this.labelMainInstruction.Padding = new System.Windows.Forms.Padding(8, 10, 10, 10);
            this.labelMainInstruction.Size = new System.Drawing.Size(230, 41);
            this.labelMainInstruction.TabIndex = 1;
            this.labelMainInstruction.Text = "Update your account settings";
            // 
            // groupboxChangePassword
            // 
            this.groupboxChangePassword.Controls.Add(this.buttonUpdatePassword);
            this.groupboxChangePassword.Controls.Add(this.textboxPassword);
            this.groupboxChangePassword.Controls.Add(this.labelPassword);
            this.groupboxChangePassword.Location = new System.Drawing.Point(12, 53);
            this.groupboxChangePassword.Name = "groupboxChangePassword";
            this.groupboxChangePassword.Size = new System.Drawing.Size(419, 105);
            this.groupboxChangePassword.TabIndex = 2;
            this.groupboxChangePassword.TabStop = false;
            this.groupboxChangePassword.Text = "Change account password";
            // 
            // buttonUpdatePassword
            // 
            this.buttonUpdatePassword.Location = new System.Drawing.Point(293, 72);
            this.buttonUpdatePassword.Name = "buttonUpdatePassword";
            this.buttonUpdatePassword.Size = new System.Drawing.Size(120, 23);
            this.buttonUpdatePassword.TabIndex = 4;
            this.buttonUpdatePassword.Text = "Update password";
            this.buttonUpdatePassword.UseVisualStyleBackColor = true;
            this.buttonUpdatePassword.Click += new System.EventHandler(this.buttonUpdatePassword_Click);
            // 
            // textboxPassword
            // 
            this.textboxPassword.Location = new System.Drawing.Point(9, 43);
            this.textboxPassword.Name = "textboxPassword";
            this.textboxPassword.Size = new System.Drawing.Size(404, 23);
            this.textboxPassword.TabIndex = 3;
            this.textboxPassword.UseSystemPasswordChar = true;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(6, 25);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(87, 15);
            this.labelPassword.TabIndex = 0;
            this.labelPassword.Text = "New password:";
            // 
            // labelDeleteAccount
            // 
            this.labelDeleteAccount.Location = new System.Drawing.Point(6, 19);
            this.labelDeleteAccount.Name = "labelDeleteAccount";
            this.labelDeleteAccount.Size = new System.Drawing.Size(407, 81);
            this.labelDeleteAccount.TabIndex = 0;
            this.labelDeleteAccount.Text = resources.GetString("labelDeleteAccount.Text");
            // 
            // groupboxDeleteAccount
            // 
            this.groupboxDeleteAccount.Controls.Add(this.labelDeleteAccount);
            this.groupboxDeleteAccount.Location = new System.Drawing.Point(12, 164);
            this.groupboxDeleteAccount.Name = "groupboxDeleteAccount";
            this.groupboxDeleteAccount.Size = new System.Drawing.Size(419, 110);
            this.groupboxDeleteAccount.TabIndex = 3;
            this.groupboxDeleteAccount.TabStop = false;
            this.groupboxDeleteAccount.Text = "Delete your account";
            // 
            // AccountSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(441, 286);
            this.Controls.Add(this.groupboxDeleteAccount);
            this.Controls.Add(this.groupboxChangePassword);
            this.Controls.Add(this.labelMainInstruction);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountSettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Account Settings";
            this.groupboxChangePassword.ResumeLayout(false);
            this.groupboxChangePassword.PerformLayout();
            this.groupboxDeleteAccount.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMainInstruction;
        private System.Windows.Forms.GroupBox groupboxChangePassword;
        private System.Windows.Forms.Button buttonUpdatePassword;
        private System.Windows.Forms.TextBox textboxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelDeleteAccount;
        private System.Windows.Forms.GroupBox groupboxDeleteAccount;
    }
}