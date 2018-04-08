namespace Password_Manager
{
    partial class EditApplicationForm
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
            this.appTypeComboBox = new System.Windows.Forms.ComboBox();
            this.appTypeLabel = new System.Windows.Forms.Label();
            this.applicationNameLabel = new System.Windows.Forms.Label();
            this.appNameTextBox = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelMainInstruction
            // 
            this.labelMainInstruction.AutoSize = true;
            this.labelMainInstruction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMainInstruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.labelMainInstruction.Location = new System.Drawing.Point(5, 9);
            this.labelMainInstruction.Name = "labelMainInstruction";
            this.labelMainInstruction.Padding = new System.Windows.Forms.Padding(10);
            this.labelMainInstruction.Size = new System.Drawing.Size(407, 41);
            this.labelMainInstruction.TabIndex = 0;
            this.labelMainInstruction.Text = "Rename or move an application to a different category";
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
            this.appTypeComboBox.Location = new System.Drawing.Point(128, 58);
            this.appTypeComboBox.Name = "appTypeComboBox";
            this.appTypeComboBox.Size = new System.Drawing.Size(328, 23);
            this.appTypeComboBox.TabIndex = 2;
            this.appTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.appTypeComboBox_SelectedIndexChanged);
            // 
            // appTypeLabel
            // 
            this.appTypeLabel.AutoSize = true;
            this.appTypeLabel.Location = new System.Drawing.Point(13, 61);
            this.appTypeLabel.Name = "appTypeLabel";
            this.appTypeLabel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.appTypeLabel.Size = new System.Drawing.Size(109, 15);
            this.appTypeLabel.TabIndex = 1;
            this.appTypeLabel.Text = "Application &Type:";
            // 
            // applicationNameLabel
            // 
            this.applicationNameLabel.AutoSize = true;
            this.applicationNameLabel.Location = new System.Drawing.Point(6, 90);
            this.applicationNameLabel.Name = "applicationNameLabel";
            this.applicationNameLabel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.applicationNameLabel.Size = new System.Drawing.Size(116, 15);
            this.applicationNameLabel.TabIndex = 3;
            this.applicationNameLabel.Text = "Application &Name:";
            // 
            // appNameTextBox
            // 
            this.appNameTextBox.Location = new System.Drawing.Point(128, 87);
            this.appNameTextBox.Name = "appNameTextBox";
            this.appNameTextBox.Size = new System.Drawing.Size(328, 23);
            this.appNameTextBox.TabIndex = 4;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(369, 145);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(87, 27);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(276, 145);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(87, 27);
            this.buttonSubmit.TabIndex = 5;
            this.buttonSubmit.Text = "&Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // EditApplicationForm
            // 
            this.AcceptButton = this.buttonSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(470, 184);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.applicationNameLabel);
            this.Controls.Add(this.appNameTextBox);
            this.Controls.Add(this.appTypeComboBox);
            this.Controls.Add(this.appTypeLabel);
            this.Controls.Add(this.labelMainInstruction);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditApplicationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMainInstruction;
        private System.Windows.Forms.ComboBox appTypeComboBox;
        private System.Windows.Forms.Label appTypeLabel;
        private System.Windows.Forms.Label applicationNameLabel;
        private System.Windows.Forms.TextBox appNameTextBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button buttonSubmit;
    }
}