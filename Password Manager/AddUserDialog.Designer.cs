namespace Password_Manager
{
    partial class AddUserDialog
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
            this.labelUsername = new System.Windows.Forms.Label();
            this.textboxUsername = new System.Windows.Forms.TextBox();
            this.textboxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.checkboxIsAdmin = new System.Windows.Forms.CheckBox();
            this.checkboxShowPassword = new System.Windows.Forms.CheckBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelMainInstruction
            // 
            this.labelMainInstruction.AutoSize = true;
            this.labelMainInstruction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMainInstruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.labelMainInstruction.Location = new System.Drawing.Point(14, 10);
            this.labelMainInstruction.Name = "labelMainInstruction";
            this.labelMainInstruction.Padding = new System.Windows.Forms.Padding(0, 12, 12, 12);
            this.labelMainInstruction.Size = new System.Drawing.Size(187, 45);
            this.labelMainInstruction.TabIndex = 0;
            this.labelMainInstruction.Text = "Add a new user account";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(15, 67);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(63, 15);
            this.labelUsername.TabIndex = 1;
            this.labelUsername.Text = "&Username:";
            // 
            // textboxUsername
            // 
            this.textboxUsername.Location = new System.Drawing.Point(84, 64);
            this.textboxUsername.Name = "textboxUsername";
            this.textboxUsername.Size = new System.Drawing.Size(378, 23);
            this.textboxUsername.TabIndex = 2;
            // 
            // textboxPassword
            // 
            this.textboxPassword.Location = new System.Drawing.Point(84, 93);
            this.textboxPassword.Name = "textboxPassword";
            this.textboxPassword.Size = new System.Drawing.Size(378, 23);
            this.textboxPassword.TabIndex = 4;
            this.textboxPassword.UseSystemPasswordChar = true;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(15, 96);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(60, 15);
            this.labelPassword.TabIndex = 3;
            this.labelPassword.Text = "&Password:";
            // 
            // checkboxIsAdmin
            // 
            this.checkboxIsAdmin.AutoSize = true;
            this.checkboxIsAdmin.Location = new System.Drawing.Point(84, 147);
            this.checkboxIsAdmin.Name = "checkboxIsAdmin";
            this.checkboxIsAdmin.Size = new System.Drawing.Size(174, 19);
            this.checkboxIsAdmin.TabIndex = 6;
            this.checkboxIsAdmin.Text = "This user is an &administrator";
            this.checkboxIsAdmin.UseVisualStyleBackColor = true;
            // 
            // checkboxShowPassword
            // 
            this.checkboxShowPassword.AutoSize = true;
            this.checkboxShowPassword.Location = new System.Drawing.Point(84, 122);
            this.checkboxShowPassword.Name = "checkboxShowPassword";
            this.checkboxShowPassword.Size = new System.Drawing.Size(108, 19);
            this.checkboxShowPassword.TabIndex = 5;
            this.checkboxShowPassword.Text = "&Show password";
            this.checkboxShowPassword.UseVisualStyleBackColor = true;
            this.checkboxShowPassword.CheckedChanged += new System.EventHandler(this.checkboxShowPassword_CheckedChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(306, 187);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 7;
            this.buttonAdd.Text = "&Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(387, 187);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // AddUserDialog
            // 
            this.AcceptButton = this.buttonAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(474, 221);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.checkboxShowPassword);
            this.Controls.Add(this.checkboxIsAdmin);
            this.Controls.Add(this.textboxPassword);
            this.Controls.Add(this.textboxUsername);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.labelMainInstruction);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddUserDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMainInstruction;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textboxUsername;
        private System.Windows.Forms.TextBox textboxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.CheckBox checkboxIsAdmin;
        private System.Windows.Forms.CheckBox checkboxShowPassword;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonCancel;
    }
}