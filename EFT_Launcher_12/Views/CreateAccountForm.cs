using System;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace EFT_Launcher_12.Views
{
    public partial class CreateAccountForm : Form
    {
        public CreateAccountForm(System.Drawing.Point location)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            location.X += 100;
            this.Location = location;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (nicknameTextBox.TextLength == 0)
            {

            }

            if (emailTextBox.TextLength == 0)
            {

            }

            if (passwordTextbox.TextLength == 0)
            {

            }

            if (gameVersionCombo.SelectedIndex == 0)
            {

            }

            Profile profileToAdd = new Profile();
            profileToAdd.id = "1";
            profileToAdd.nickname = nicknameTextBox.Text;
            profileToAdd.email = emailTextBox.Text;
            profileToAdd.password = passwordTextbox.Text;
            switch(gameVersionCombo.SelectedIndex)
            {
                case 1:
                    profileToAdd.edition = "Standard";
                    break;
                case 2:
                    profileToAdd.edition = "Left Behind";
                    break;
                case 3:
                    profileToAdd.edition = "Prepare To Escape";
                    break;
                case 4:
                    profileToAdd.edition = "Edga Of Darkness";
                    break;
            }

            //save profile 
            //start launcher
            //close this launcher

        }
    }
}
