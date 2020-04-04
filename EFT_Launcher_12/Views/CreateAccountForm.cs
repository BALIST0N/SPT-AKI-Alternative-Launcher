using System;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace EFT_Launcher_12.Views
{
    public partial class CreateAccountForm : Form
    {
        public CreateAccountForm(Point location)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            location.X += 100;
            this.Location = location;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if( CheckInputText() == true)
            {
                Profile profileToAdd = new Profile();
                profileToAdd.id = "1";
                profileToAdd.nickname = nicknameTextBox.Text;
                profileToAdd.email = emailTextBox.Text;
                profileToAdd.password = passwordTextbox.Text;
                profileToAdd.wipe = true;
                switch (gameVersionCombo.SelectedIndex)
                {
                    case 0:
                        profileToAdd.edition = "Standard";
                        break;
                    case 1:
                        profileToAdd.edition = "Left Behind";
                        break;
                    case 2:
                        profileToAdd.edition = "Prepare To Escape";
                        break;
                    case 3:
                        profileToAdd.edition = "Edge Of Darkness";
                        break;
                }

                try
                {
                    using (StreamWriter file = File.CreateText(Globals.accountsFile))
                    {
                        Dictionary<string, Profile> temp = new Dictionary<string, Profile>();
                        temp.Add(profileToAdd.id, profileToAdd);
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Formatting = Formatting.Indented;
                        serializer.Serialize(file, temp);
                    }
                    MessageBox.Show("profile succesfully created, the launcher will now restart : ) ");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("something wrong happened " + ex.Message);
                }

                Process.Start(Path.Combine(Environment.CurrentDirectory, "EmuTarkov-Launcher.exe"));
                Application.Exit();
            }
        }

        private bool CheckInputText()
        {
            bool ok = true;
            if (nicknameTextBox.TextLength > 0)
            {
                if (Regex.IsMatch(nicknameTextBox.Text.Trim(), @"^[a-zA-Z0-9_]*$") == false)
                {
                    nicknameTextBox.ForeColor = Color.Red;
                    ok = false;
                }
                else
                {
                    nicknameTextBox.ForeColor = Color.White;
                }
            }
            else
            {
                ok = false;
            }

            if (emailTextBox.TextLength > 0)
            {
                if (Regex.IsMatch(emailTextBox.Text.Trim(), @"^[a-zA-Z0-9_@.-]*$") == false)
                {
                    emailTextBox.ForeColor = Color.Red;
                    ok = false;
                }
                else
                {
                    emailTextBox.ForeColor = Color.White;
                }
            }
            else
            {
                ok = false;
            }

            if (passwordTextbox.TextLength > 0)
            {
                if (Regex.IsMatch(passwordTextbox.Text.Trim(), @"^[a-zA-Z0-9_@.-]*$") == false)
                {
                    passwordTextbox.ForeColor = Color.Red;
                    ok = false;
                }
                else
                {
                    passwordTextbox.ForeColor = Color.White;
                }
            }
            else
            {
                ok = false;
            }

            if (gameVersionCombo.SelectedIndex == -1)
            {
                label7.ForeColor = Color.Red;
                ok = false;
            }
            else
            {
                label7.ForeColor = Color.White;
            }

            return ok;
        }
    }
}
