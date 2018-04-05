using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Password_Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += OnApplicationExit;

            ShowLoginDialog();

            Application.Run();
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        static void ShowLoginDialog()
        {
            var dialog = new LoginDialog();
            dialog.FormClosed += (object s, FormClosedEventArgs a) =>
            {
                if (dialog.isLoginSuccess)
                {
                    var userName = new Shared.Username(dialog.userName);
                    userName.isAdmin = dialog.isAdmin;

                    if (dialog.keyData == null)
                    {
                        using (var d = new TaskDialog()
                        {
                            Caption = "Generate Key File",
                            InstructionText = "Would you like to generate a key file?",
                            Text = "A key file is needed in order to encrypt your password. " +
                                   "You'll be asked to save the key file somewhere on your PC. " ,
                            Icon = TaskDialogStandardIcon.None,
                        })
                        {
                            var buttonSave = new TaskDialogButton("SaveButton", "Generate key file");
                            buttonSave.Click += (object sender, EventArgs args) =>
                            {
                                dialog.Close();

                                var saveKeyDialog = new SaveFileDialog()
                                {
                                    Title = "Save Key File",
                                    Filter = "Key File (*.key)|*.key",
                                };
                                if (saveKeyDialog.ShowDialog() == DialogResult.OK)
                                {
                                    var secretkey = Shared.CryptManager.generateKey();
                                    File.WriteAllBytes(saveKeyDialog.FileName, secretkey);

                                    var form = new PasswordManagerForm();
                                    form.M_secretkey = secretkey;
                                    form.LoggedInUser = userName;
                                    form.Show();
                                }
                                else
                                {
                                    ShowLoginDialog();
                                }
                            };
                            buttonSave.Default = true;

                            var buttonClose = new TaskDialogButton("CloseButton", "Cancel");
                            buttonClose.Click += (object sender, EventArgs args) =>
                            {
                                d.Close();

                                ShowLoginDialog();
                            };

                            d.Controls.Add(buttonSave);
                            d.Controls.Add(buttonClose);
                            d.Show();
                        }
                    }
                    else
                    {
                        var form = new PasswordManagerForm();
                        form.M_secretkey = dialog.keyData;
                        form.LoggedInUser = userName;
                        form.Show();
                    }
                }
                else
                {
                    Application.Exit();
                }
            };
            dialog.Show();
        }
    }
}
