using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace EFT_Launcher_12
{
    public partial class EditProfileForm : Form
    {
        string profilePath;
        ProfileExtended profileToEdit;
        List<HideoutUpgradesArea> hideoutLevels;
        Dictionary<string, string> tradersNames;

        public EditProfileForm(int id, System.Drawing.Point location)
        {
            profilePath = Path.Combine(Globals.profilesFolder,"profiles/"+ id + "/character.json");
            tradersNames = new Dictionary<string, string>();
            hideoutLevels = new List<HideoutUpgradesArea>();
            this.StartPosition = FormStartPosition.Manual;
            location.X += 500;
            this.Location = location;
            
            #region init Traders Names
            tradersNames.Add("5a7c2eca46aef81a7ca2145d", "Mechanic");
            tradersNames.Add("5ac3b934156ae10c4430e83c", "Ragman");
            tradersNames.Add("5c0647fdd443bc2504c2d371", "Jaeger");
            tradersNames.Add("54cb50c76803fa8b248b4571", "Prapor");
            tradersNames.Add("54cb57776803fa99248b456e", "Therapist");
            tradersNames.Add("579dc571d53a0658a154fbec", "Fence");
            tradersNames.Add("5935c25fb3acc3127c3d8cd9", "Peacekeeper");
            tradersNames.Add("58330581ace78e27b8b10cee", "Skier");
            tradersNames.Add("ragfair", "Ragfair");
            #endregion

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
            hideoutLevels.Add(new HideoutUpgradesArea(21, "Christmas Tree", 1));
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

            profileToEdit.SetSkillValue("Endurance", enduranceNumericBox.Value);
            profileToEdit.SetSkillValue("Strength", strenghNumericBox.Value);
            profileToEdit.SetSkillValue("Vitality", vitalityNumericBox.Value);
            profileToEdit.SetSkillValue("Health", healthNumericBox.Value);
            profileToEdit.SetSkillValue("StressResistance", stressNumericBox.Value);
            profileToEdit.SetSkillValue("Metabolism", metabolismNumericBox.Value);
            profileToEdit.SetSkillValue("Immunity", immunityNumericBox.Value);
            profileToEdit.SetSkillValue("Perception", perceptionNumericBox.Value);
            profileToEdit.SetSkillValue("Intellect", intelNumericBox.Value);
            profileToEdit.SetSkillValue("Attention", attentionNumericBox.Value);
            profileToEdit.SetSkillValue("Charisma", charismaNumericBox.Value);
            profileToEdit.SetSkillValue("Memory", memoryNumericBox.Value);
            profileToEdit.SetSkillValue("CovertMovement", covertNumericBox.Value);
            profileToEdit.SetSkillValue("RecoilControl", recoilNumericBox.Value);
            profileToEdit.SetSkillValue("Search", searchNumericBox.Value);
            profileToEdit.SetSkillValue("MagDrills", magdrillsNumericBox.Value);
            
            //weapon skills
            profileToEdit.SetSkillValue("Pistol", pistolNumericBox.Value);
            profileToEdit.SetSkillValue("Revolver", revolverNumericBox.Value);
            profileToEdit.SetSkillValue("SMG", SMGNumericBox.Value);
            profileToEdit.SetSkillValue("Assault", assaultNumericBox.Value);
            profileToEdit.SetSkillValue("Shotgun", shotgunNumericBox.Value);
            profileToEdit.SetSkillValue("Sniper", sniperNumericBox.Value);
            profileToEdit.SetSkillValue("LMG", lmgNumericBox.Value);
            profileToEdit.SetSkillValue("HMG", hmgNumericBox.Value);
            profileToEdit.SetSkillValue("DMR", dmrNumericBox.Value);
            profileToEdit.SetSkillValue("Launcher", launcherNumericBox.Value);
            profileToEdit.SetSkillValue("AttachedLauncher", attachLauncherNumericBox.Value);
            profileToEdit.SetSkillValue("Throwing", throwNumericBox.Value);
            profileToEdit.SetSkillValue("Melee", meleeNumericBox.Value);

            try
            {
                string json = File.ReadAllText(profilePath);
                JObject realProfile = JObject.Parse(json);

                realProfile.SelectToken("Info")["Nickname"] = profileToEdit.info.nickname;
                realProfile.SelectToken("Info")["Side"] = profileToEdit.info.side;
                realProfile.SelectToken("Info")["Experience"] = profileToEdit.info.experience;
                realProfile.SelectToken("Info")["GameVersion"] = profileToEdit.info.gameVersion;

                for (int i = 0; i < profileToEdit.skills.common.Count; i++)
                {
                    realProfile.SelectToken("Skills").SelectToken("Common")[i]["Progress"] = profileToEdit.skills.common[i].progress;
                }

                for (int i = 0; i < profileToEdit.skills.mastering.Count; i++)
                {
                    realProfile.SelectToken("Skills").SelectToken("Mastering")[i]["Progress"] = profileToEdit.skills.mastering[i].progress;
                }

                foreach (ProfileExtended.Hideout.Area a in profileToEdit.hideout.areas)
                {
                    realProfile.SelectToken("Hideout").SelectToken("Areas")[a.type]["level"] = a.level;
                }

                //using (StreamWriter file = File.CreateText( Path.Combine(Globals.profilesFolder, "profiles/1/TestSaveProfile.json") ))
                using (StreamWriter file = File.CreateText(profilePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, realProfile);
                }
                MessageBox.Show("profile succesfully saved");
                                
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

            foreach(ProfileExtended.Skills.Mastering m in profileToEdit.skills.mastering)
            {
                masteringComboBox.Items.Add(m.id);
            }

            foreach(string tr in profileToEdit.traderStandings.Keys)
            {
                if(tradersNames.ContainsKey(tr) == true )
                {
                    traderListComboBox.Items.Add(tradersNames[tr]);
                }
                else
                {
                    traderListComboBox.Items.Add(tr);
                }
            }

            #region INIT SKILLS numericBoxes
            enduranceNumericBox.Value = profileToEdit.GetSkillValue("Endurance");
            strenghNumericBox.Value = profileToEdit.GetSkillValue("Strength");
            vitalityNumericBox.Value = profileToEdit.GetSkillValue("Vitality");
            healthNumericBox.Value = profileToEdit.GetSkillValue("Health");
            stressNumericBox.Value = profileToEdit.GetSkillValue("StressResistance");

            metabolismNumericBox.Value = profileToEdit.GetSkillValue("Metabolism");
            immunityNumericBox.Value = profileToEdit.GetSkillValue("Immunity");
            perceptionNumericBox.Value = profileToEdit.GetSkillValue("Perception");
            intelNumericBox.Value = profileToEdit.GetSkillValue("Intellect");
            attentionNumericBox.Value = profileToEdit.GetSkillValue("Attention");
            charismaNumericBox.Value = profileToEdit.GetSkillValue("Charisma");
            memoryNumericBox.Value = profileToEdit.GetSkillValue("Memory");

            covertNumericBox.Value = profileToEdit.GetSkillValue("CovertMovement");
            recoilNumericBox.Value = profileToEdit.GetSkillValue("RecoilControl");
            searchNumericBox.Value = profileToEdit.GetSkillValue("Search");
            magdrillsNumericBox.Value = profileToEdit.GetSkillValue("MagDrills");

            //weapons skills
            pistolNumericBox.Value = profileToEdit.GetSkillValue("Pistol");
            revolverNumericBox.Value = profileToEdit.GetSkillValue("Revolver");
            SMGNumericBox.Value = profileToEdit.GetSkillValue("SMG");
            assaultNumericBox.Value = profileToEdit.GetSkillValue("Assault");
            shotgunNumericBox.Value = profileToEdit.GetSkillValue("Shotgun");
            sniperNumericBox.Value = profileToEdit.GetSkillValue("Sniper");

            lmgNumericBox.Value = profileToEdit.GetSkillValue("LMG");
            hmgNumericBox.Value = profileToEdit.GetSkillValue("HMG");
            dmrNumericBox.Value = profileToEdit.GetSkillValue("DMR");
            launcherNumericBox.Value = profileToEdit.GetSkillValue("Launcher");
            attachLauncherNumericBox.Value = profileToEdit.GetSkillValue("AttachedLauncher");
            throwNumericBox.Value = profileToEdit.GetSkillValue("Throwing");
            meleeNumericBox.Value = profileToEdit.GetSkillValue("Melee");

            #endregion
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

        private void masteringComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            masteringProgressNumericBox.ValueChanged -= new EventHandler(masteringProgressNumericBox_ValueChanged);
            masteringProgressNumericBox.Value = profileToEdit.skills.mastering.Find( x => x.id.Equals(this.masteringComboBox.SelectedItem.ToString()) ).progress;
            masteringProgressNumericBox.ValueChanged += new EventHandler(masteringProgressNumericBox_ValueChanged);
        }

        private void masteringProgressNumericBox_ValueChanged(object sender, EventArgs e)
        {
            profileToEdit.skills.mastering.Find(x => x.id.Equals(this.masteringComboBox.SelectedItem.ToString())).progress = Convert.ToInt32( masteringProgressNumericBox.Value);
        }

        private void traderListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string trader = this.traderListComboBox.SelectedItem.ToString();
            string traderid = tradersNames.FirstOrDefault(x => x.Value == trader).Key;

            if(traderid != null ){ trader = traderid; }
            
            traderLevelNumericBox.Value = profileToEdit.traderStandings[trader].currentLevel;
            traderSalesNumericBox.Value = profileToEdit.traderStandings[trader].currentSalesSum;
            traderStandingNumericBox.Value = profileToEdit.traderStandings[trader].currentStanding;
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
