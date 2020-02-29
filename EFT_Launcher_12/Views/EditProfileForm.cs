using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace EFT_Launcher_12
{
    public partial class EditProfileForm : Form
    {
        string profilePath;
        ProfileExtended profileToEdit;
        List<HideoutUpgradesArea> hideoutLevels;

        public EditProfileForm(int id)
        {
            profilePath = Path.Combine(Globals.profilesFolder,"profiles/"+ id + "/character.json");
            hideoutLevels = new List<HideoutUpgradesArea>();

            #region hideoutlevel init
            hideoutLevels.Add(new HideoutUpgradesArea(0,  "Vents", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(1,  "Security", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(2,  "Lavatory", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(3,  "Stash", 4));
            hideoutLevels.Add(new HideoutUpgradesArea(4,  "Generator", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(5,  "Heating", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(6,  "Water Collector", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(7,  "MedStation", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(8,  "Nutrition Unit", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(9,  "Rest Space", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(10, "Workbench", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(11, "Intel Center", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(12, "Shooting Range", 1));
            hideoutLevels.Add(new HideoutUpgradesArea(13, "Library", 1));
            hideoutLevels.Add(new HideoutUpgradesArea(14, "Scav Case", 1));
            hideoutLevels.Add(new HideoutUpgradesArea(15, "Illumination", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(16, "?trophies rack?", 1));
            hideoutLevels.Add(new HideoutUpgradesArea(17, "Air Filter", 1));
            hideoutLevels.Add(new HideoutUpgradesArea(18, "Solar Power", 1));
            hideoutLevels.Add(new HideoutUpgradesArea(19, "Booze Generator", 1));
            hideoutLevels.Add(new HideoutUpgradesArea(20, "Bitcoin Farm", 3));
            hideoutLevels.Add(new HideoutUpgradesArea(21, "number 21", 1));
            #endregion
            InitializeComponent();
        }

        private void EditProfileForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader r = new StreamReader(profilePath))
                {
                    profileToEdit = JsonConvert.DeserializeObject<ProfileExtended>(r.ReadToEnd());
                    profileToEdit.hideout.areas = profileToEdit.hideout.areas.OrderBy(o => o.type).ToList();
                    SetInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("profile can't be loaded : " + ex.Message);
                Close();
            }

            foreach (HideoutUpgradesArea h in hideoutLevels)
            {
                hideoutAreaComboBox.Items.Add(h.areaName);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e) //saving the profile 
        {
            profileToEdit.info.nickname = nicknameTextBox.Text;
            profileToEdit.info.side = sideselectorComboBox.SelectedItem.ToString();
            profileToEdit.info.experience = Convert.ToInt32(experienceBox.Value);
            profileToEdit.info.gameVersion = gameVersionCombo.SelectedItem.ToString();

            SetSkillValue("Endurance", enduranceNumericBox.Value);
            SetSkillValue("Strength", strenghNumericBox.Value);
            SetSkillValue("Vitality", vitalityNumericBox.Value);
            SetSkillValue("Health", healthNumericBox.Value);
            SetSkillValue("StressResistance", stressNumericBox.Value);
            SetSkillValue("Metabolism", metabolismNumericBox.Value);
            SetSkillValue("Immunity", immunityNumericBox.Value);
            SetSkillValue("Perception", perceptionNumericBox.Value);
            SetSkillValue("Intellect", intelNumericBox.Value);
            SetSkillValue("Attention", attentionNumericBox.Value);
            SetSkillValue("Charisma", charismaNumericBox.Value);
            SetSkillValue("Memory", memoryNumericBox.Value);
            SetSkillValue("CovertMovement", covertNumericBox.Value);
            SetSkillValue("RecoilControl", recoilNumericBox.Value);
            SetSkillValue("Search", searchNumericBox.Value);
            SetSkillValue("MagDrills", magdrillsNumericBox.Value);

            try
            {
                /*
                using (StreamReader r = new StreamReader(profilePath))
                {
                    dynamic realProfile = JsonConvert.DeserializeObject(r.ReadToEnd());
                    
                    realProfile.Info.Nickname = profileToEdit.info.nickname;
                    realProfile.Info.Side = profileToEdit.info.side;
                    realProfile.Info.Experience = profileToEdit.info.experience;
                    realProfile.Info.GameVersion = profileToEdit.info.gameVersion;

                    realProfile.Skills.Common = new List<ProfileExtended.Skills.Skill>();
                    foreach(ProfileExtended.Skills.Skill cheval in profileToEdit.skills.common)
                    {
                        realProfile.Skills.Common.Add(cheval);
                    }

                    realProfile.Hideout.Areas = new List<ProfileExtended.Hideout.Area>();
                    foreach (ProfileExtended.Hideout.Area fromage in profileToEdit.hideout.areas)
                    {
                        realProfile.Hideout.Areas.Add(fromage);
                    }
                    

                    using (StreamWriter file = File.CreateText(profilePath))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, profileToEdit);
                    }
                    MessageBox.Show("profile succesfully saved");
                    
                }
                */
            }
            catch(Exception ex)
            {
                MessageBox.Show("something went wrong : " + ex.Message);
            }

        }

        private void SetInfo()
        {
            Text += profileToEdit.info.nickname;

            nicknameTextBox.Text = profileToEdit.info.nickname;
            sideselectorComboBox.SelectedItem = profileToEdit.info.side;
            experienceBox.Value = profileToEdit.info.experience;
            gameVersionCombo.SelectedItem = profileToEdit.info.gameVersion;

            #region INIT SKILLS numericBoxes
            enduranceNumericBox.Value = GetSkillValue("Endurance");
            strenghNumericBox.Value = GetSkillValue("Strength");
            vitalityNumericBox.Value = GetSkillValue("Vitality");
            healthNumericBox.Value = GetSkillValue("Health");
            stressNumericBox.Value = GetSkillValue("StressResistance");

            metabolismNumericBox.Value = GetSkillValue("Metabolism");
            immunityNumericBox.Value = GetSkillValue("Immunity");
            perceptionNumericBox.Value = GetSkillValue("Perception");
            intelNumericBox.Value = GetSkillValue("Intellect");
            attentionNumericBox.Value = GetSkillValue("Attention");
            charismaNumericBox.Value = GetSkillValue("Charisma");
            memoryNumericBox.Value = GetSkillValue("Memory");

            covertNumericBox.Value = GetSkillValue("CovertMovement");
            recoilNumericBox.Value = GetSkillValue("RecoilControl");
            searchNumericBox.Value = GetSkillValue("Search");
            magdrillsNumericBox.Value = GetSkillValue("MagDrills");
            #endregion
        }

        private decimal GetSkillValue(string skill)
        {
            //return profileToEdit.skills[profileToEdit.skills.listskills.FindIndex(x => x.id.Equals(skill))].progress;
            return profileToEdit.skills.common.Find(x => x.id.Equals(skill)).progress;
        }

        private void SetSkillValue(string skill, decimal newval)
        {
            profileToEdit.skills.common.Find(x => x.id.Equals(skill)).progress = newval;
        }

        private void HideoutAreaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ignore raising value changed event
            //super important it cause the hideout numeric to change values and save without user input
            hideoutLevelNumeric.ValueChanged -= new EventHandler(HideoutLevelNumeric_ValueChanged);

            hideoutLevelNumeric.Maximum = hideoutLevels[this.hideoutAreaComboBox.SelectedIndex].levelMax;
            hideoutLevelNumeric.Value = profileToEdit.hideout.areas[this.hideoutAreaComboBox.SelectedIndex].level;

            hideoutLevelNumeric.ValueChanged += new EventHandler(HideoutLevelNumeric_ValueChanged);//re-enable user input event ! 
        }

        private void HideoutLevelNumeric_ValueChanged(object sender, EventArgs e)
        {
            profileToEdit.hideout.areas[this.hideoutAreaComboBox.SelectedIndex].level = Convert.ToInt32(hideoutLevelNumeric.Value);
            //profileToEdit.hideout.areas.Find(x => x.type.Equals(this.hideoutAreaComboBox.SelectedIndex)).level = Convert.ToInt32(hideoutLevelNumeric.Value);
        }

        /// <summary>
        /// hideout upgrades level object
        /// </summary>
        internal class HideoutUpgradesArea
        {
            public int areaType;
            public string areaName;
            public int levelMax;

            public HideoutUpgradesArea(int t, string n, int u)
            {
                areaType = t;
                areaName = n;
                levelMax = u;
            }
        }
    }
}
