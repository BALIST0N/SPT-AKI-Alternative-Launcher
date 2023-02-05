using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SPTAKI_Alt_Launcher
{
    public partial class CreateProfileForm : Form
    {
        string baseProfile;
        public CreateProfileForm(Point location)
        {
            this.StartPosition = FormStartPosition.Manual;
            location.X += 250;
            location.Y += 5;
            this.Location = location;
            InitializeComponent();
            baseProfile = "{\"info\":{\"id\":\"\",\"username\":\"\",\"password\":\"\",\"wipe\":true,\"edition\":\"\"},\"characters\":{\"pmc\":{\"UnlockedInfo\":{\"unlockedProductionRecipe\":[]}},\"scav\":{}},\"vitality\":{\"health\":{\"Hydration\":0,\"Energy\":0,\"Temperature\":0,\"Head\":0,\"Chest\":0,\"Stomach\":0,\"LeftArm\":0,\"RightArm\":0,\"LeftLeg\":0,\"RightLeg\":0},\"effects\":{\"Head\":{},\"Chest\":{},\"Stomach\":{},\"LeftArm\":{},\"RightArm\":{},\"LeftLeg\":{},\"RightLeg\":{}}},\"inraid\":{\"location\":\"none\",\"character\":\"none\"},\"insurance\":[]}";

        }

        private void CreateProfileForm_Load(object sender, EventArgs e)
        {
            editionListBox.SelectedIndex = 0;
        }

        private void profileEditButton_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text.Length > 0)
            {
                dynamic profileDATA = JObject.Parse(baseProfile);
                profileDATA["info"]["id"] = GenerateRandomID();
                string id = (string)profileDATA["info"]["id"];
                profileDATA["info"]["username"] = usernameTextBox.Text;
                profileDATA["info"]["edition"] = editionListBox.SelectedItem.ToString();

                try
                {
                    using StreamWriter file = File.CreateText( Path.Combine(Globals.profilesFolder, id + ".json") );
                    JsonSerializer serializer = new()
                    {
                        Formatting = Formatting.Indented
                    };
                    serializer.Serialize(file, profileDATA);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.Dispose();
            Application.Restart();
            
        }

        private void CreateProfileForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (e.CloseReason == CloseReason.UserClosing);
        }

        private string GenerateRandomID()
        {
            string res = "";
            Random R = new Random();
            for (int i = 0; i < 8;i++)
            {
                res += R.Next(0, 16).ToString("X").ToLower();
            }
            return res;
        }

        private void usernameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if( !char.IsLetter(e.KeyChar) && !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) )
            {
                e.Handled = true;
            }
        }
    }
}
