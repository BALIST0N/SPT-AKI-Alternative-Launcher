using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SPTAKI_Alt_Launcher
{
    public partial class EditProfileForm : Form
    {
        string profilePath;
        ProfileExtended profileToEdit;
        List<HideoutUpgradesArea> hideoutLevels;
        Dictionary<string, string> tradersNames;
        List<MasteringWeaponLevel> weaponMasteringLevels;

        public EditProfileForm(string id, Point location)
        {
            profilePath = Path.Combine(Globals.profilesFolder, id +".json");
            tradersNames = new Dictionary<string, string>();
            weaponMasteringLevels = new List<MasteringWeaponLevel>();
            hideoutLevels = new List<HideoutUpgradesArea>();
            this.StartPosition = FormStartPosition.Manual;
            location.X += 100;
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
            tradersNames.Add("638f541a29ffd1183d187f57", "Lighthousekeeper");
            tradersNames.Add("ragfair", "Ragfair");
            #endregion

            #region hideoutlevel init
            hideoutLevels.Add(new HideoutUpgradesArea(0,  "VENTS"           ,3));
            hideoutLevels.Add(new HideoutUpgradesArea(1,  "SECURITY"        ,3));
            hideoutLevels.Add(new HideoutUpgradesArea(2,  "LAVATORY"        ,3));
            hideoutLevels.Add(new HideoutUpgradesArea(3,  "STASH"           ,4));
            hideoutLevels.Add(new HideoutUpgradesArea(4,  "GENERATOR"       ,3));
            hideoutLevels.Add(new HideoutUpgradesArea(5,  "HEATING"         ,3));
            hideoutLevels.Add(new HideoutUpgradesArea(6,  "WATER COLLECTOR" ,3));
            hideoutLevels.Add(new HideoutUpgradesArea(7,  "MEDSTATION"      ,3));
            hideoutLevels.Add(new HideoutUpgradesArea(8,  "NUTRITION UNIT"  ,3));
            hideoutLevels.Add(new HideoutUpgradesArea(9,  "REST SPACE"      ,3));
            hideoutLevels.Add(new HideoutUpgradesArea(10, "WORKBENCH"       ,3));
            hideoutLevels.Add(new HideoutUpgradesArea(11, "INTEL CENTER"    ,3));
            hideoutLevels.Add(new HideoutUpgradesArea(12, "SHOOTING RANGE"  ,3));
            hideoutLevels.Add(new HideoutUpgradesArea(13, "LIBRARY"         ,1));
            hideoutLevels.Add(new HideoutUpgradesArea(14, "SCAV CASE"       ,1));
            hideoutLevels.Add(new HideoutUpgradesArea(15, "ILLUMINATION"    ,3));
            hideoutLevels.Add(new HideoutUpgradesArea(16, "PLACE OF FAME"   ,1));
            hideoutLevels.Add(new HideoutUpgradesArea(17, "AIR FILTERING"   ,1));
            hideoutLevels.Add(new HideoutUpgradesArea(18, "SOLAR POWER"     ,1));
            hideoutLevels.Add(new HideoutUpgradesArea(19, "BOOZE GENERATOR" ,1));
            hideoutLevels.Add(new HideoutUpgradesArea(20, "BITCOIN FARM"    ,3));
            hideoutLevels.Add(new HideoutUpgradesArea(21, "CHRISTMAS TREE"  ,1));
            hideoutLevels.Add(new HideoutUpgradesArea(22, "EMERGENCY WALL"  ,6));
            hideoutLevels.Add(new HideoutUpgradesArea(23, "GYM"             ,1));
            hideoutLevels.Add(new HideoutUpgradesArea(24, "WEAPON STAND"    ,3));
            #endregion
            InitializeComponent();
        }

        private void EditProfileForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader r = new StreamReader(profilePath))
                {
                    dynamic profileex = JObject.Parse(r.ReadToEnd());

                    string t1 = (string)profileex["characters"]["pmc"].ToString();
                    profileToEdit = JsonConvert.DeserializeObject<ProfileExtended>( t1 );
                    if(profileToEdit.hideout != null )
                    {
                        profileToEdit.hideout.areas = profileToEdit.hideout.areas.OrderBy(o => o.type).ToList();
                        this.wipeProfileCheckbox.Checked = (bool)profileex["info"]["wipe"];
                        SetInfo();
                    }
                    else
                    {
                        MessageBox.Show(" the profile "+ (string)profileex["info"]["username"] +" is not initialized, please start the game and choose your side ;)");
                        this.Close();
                    }                    
                }

                using (StreamReader r = new StreamReader(Globals.serverFolder + "/Aki_Data/Server/database/globals.json"))
                {
                    dynamic global = JObject.Parse(r.ReadToEnd());
                    weaponMasteringLevels = JsonConvert.DeserializeObject<List<MasteringWeaponLevel>>( (string)global["config"]["Mastering"].ToString() ); 
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

        private void SetInfo()
        {
            this.Text += profileToEdit.info.nickname;

            nicknameTextBox.Text = profileToEdit.info.nickname;
            sideselectorComboBox.SelectedItem = profileToEdit.info.side;
            ExperienceTrackBar.Value = profileToEdit.info.experience;
            gameVersionCombo.SelectedItem = profileToEdit.info.gameVersion;

            foreach(ProfileExtended.Skills.Mastering m in profileToEdit.skills.mastering)
            {
                masteringComboBox.Items.Add(m.id);
            }

            foreach(string tr in profileToEdit.TradersInfo.Keys)
            {
                if(tradersNames.ContainsKey(tr) == true )
                {
                    traderListComboBox.Items.Add(tradersNames[tr]);
                }
                else //if its a modded trader then add trader id...
                {
                    traderListComboBox.Items.Add(tr);
                }
            }
            this.traderListComboBox.SelectedIndex = 0;

            #region INIT SKILLS trackbars
            EnduranceTrackBar.Value = profileToEdit.GetSkillValue("Endurance");
            StrengthTrackBar.Value = profileToEdit.GetSkillValue("Strength");
            VitalityTrackBar.Value = profileToEdit.GetSkillValue("Vitality");
            HealthTrackBar.Value = profileToEdit.GetSkillValue("Health");
            StressTrackBar.Value = profileToEdit.GetSkillValue("StressResistance");

            MetabolismTrackbar.Value = profileToEdit.GetSkillValue("Metabolism");
            ImmunityTrackbar.Value = profileToEdit.GetSkillValue("Immunity");
            PerceptionTrackBar.Value = profileToEdit.GetSkillValue("Perception");
            IntelectTrackBar.Value = profileToEdit.GetSkillValue("Intellect");
            AttentionTrackBar.Value = profileToEdit.GetSkillValue("Attention");
            CharismaTrackBar.Value = profileToEdit.GetSkillValue("Charisma");
            MemoryTrackBar.Value = profileToEdit.GetSkillValue("Memory");

            CovertTrackBar.Value = profileToEdit.GetSkillValue("CovertMovement");
            RecoilTrackBar.Value = profileToEdit.GetSkillValue("RecoilControl");
            SearchingTrackBar.Value = profileToEdit.GetSkillValue("Search");
            MagdrillsTrackBar.Value = profileToEdit.GetSkillValue("MagDrills");

            AimTrackBar.Value = profileToEdit.GetSkillValue("AimDrills");
            SurgeryTrackBar.Value = profileToEdit.GetSkillValue("Surgery");
            ProneTrackBar.Value = profileToEdit.GetSkillValue("ProneMovement");
            CraftingTrackBar.Value = profileToEdit.GetSkillValue("Crafting");
            HideoutTrackBar.Value = profileToEdit.GetSkillValue("HideoutManagement");
            WeaponTreatmentTrackBar.Value = profileToEdit.GetSkillValue("WeaponTreatment");
            TroubleShootingTrackBar.Value = profileToEdit.GetSkillValue("TroubleShooting");

            LightVestsTrackBar.Value = profileToEdit.GetSkillValue("LightVests");
            HeavyVestsTrackBar.Value = profileToEdit.GetSkillValue("HeavyVests");
            //weapons skills
            PistolTrackBar.Value = profileToEdit.GetSkillValue("Pistol");
            SMGTrackBar.Value = profileToEdit.GetSkillValue("SMG");
            AssaultTrackBar.Value = profileToEdit.GetSkillValue("Assault");
            ShotgunTrackBar.Value = profileToEdit.GetSkillValue("Shotgun");
            SniperTrackBar.Value = profileToEdit.GetSkillValue("Sniper");

            LMGTrackBar.Value = profileToEdit.GetSkillValue("LMG");
            HMGTrackBar.Value = profileToEdit.GetSkillValue("HMG");
            DMRTrackBar.Value = profileToEdit.GetSkillValue("DMR");
            LauncherTrackBar.Value = profileToEdit.GetSkillValue("Launcher");
            ThrowingTrackBar.Value = profileToEdit.GetSkillValue("Throwing");
            MeleeTrackBar.Value = profileToEdit.GetSkillValue("Melee");
            RevolverTrackBar.Value = profileToEdit.GetSkillValue("Revolver");
            AttachedLauncherTrackBar.Value = profileToEdit.GetSkillValue("AttachedLauncher");

            #endregion
        }

        private void HideoutAreaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ignore raising value changed event
            //super important it cause the hideout numeric to change values and save without user input
            hideoutLevelNumeric.ValueChanged -= new EventHandler(HideoutLevelNumeric_ValueChanged);
            HideoutUpgradesArea h = hideoutLevels.Find(x => x.areaType == hideoutAreaComboBox.SelectedIndex);
            hideoutLevelNumeric.Maximum = h.levelMax;
            hideoutLevelNumeric.Value = profileToEdit.hideout.areas.Find(x => x.type == h.areaType).level;

            hideoutLevelNumeric.ValueChanged += new EventHandler(HideoutLevelNumeric_ValueChanged);//re-enable user input event ! 
        }

        private void HideoutLevelNumeric_ValueChanged(object sender, EventArgs e)
        {
            profileToEdit.hideout.areas[this.hideoutAreaComboBox.SelectedIndex].level = Convert.ToInt32(hideoutLevelNumeric.Value);
            //profileToEdit.hideout.areas.Find(x => x.type.Equals(this.hideoutAreaComboBox.SelectedIndex)).level = Convert.ToInt32(hideoutLevelNumeric.Value);
        }

        private void masteringComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //remove event (same like hideout values)
            MasteringWeaponLevel wml = weaponMasteringLevels.Find(x => x.Name == this.masteringComboBox.SelectedItem.ToString());
            WeaponMasteringTrackBar.ValueChanged -= new EventHandler(WeaponMasteringTrackBar_ValueChanged);
            WeaponMasteringTrackBar.Maximum = wml.Level2 + wml.Level3;

            int masterValue = (int)profileToEdit.skills.mastering.Find(x => x.id.Equals(this.masteringComboBox.SelectedItem.ToString())).progress;
            if(masterValue > WeaponMasteringTrackBar.Maximum) { masterValue = WeaponMasteringTrackBar.Maximum; }

            WeaponMasteringTrackBar.Value = masterValue;
            TrackBars_ValueChanged(WeaponMasteringTrackBar, e);
            WeaponMasteringTrackBar.ValueChanged += new EventHandler(WeaponMasteringTrackBar_ValueChanged); //re-add the event otherwise it will not be triggerd
        }

        private void WeaponMasteringTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if(masteringComboBox.SelectedIndex > -1 )
            {
                profileToEdit.skills.mastering.Find(x => x.id.Equals(this.masteringComboBox.SelectedItem.ToString())).progress = WeaponMasteringTrackBar.Value;
                TrackBars_ValueChanged(WeaponMasteringTrackBar, e);
            }
        }

        //when you move any skill trackbar value, it must change label value (don't forget to add this event to the control !)
        private void TrackBars_ValueChanged(object sender, EventArgs e)
        {
            TrackBar t = (TrackBar)sender;
            string name = t.Name.Replace("TrackBar", "").Replace("Trackbar", "") + "LevelLabel";
            try
            {
                this.Controls.Find(name, true).FirstOrDefault().Text = t.Value.ToString();
            }
            catch { }
        }



        //****************** TRADER WINDOW FUNCTIONS & EVENTS ************************// 
        private void traderListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string trader = getSelectedTrader();

            traderLevelNumericBox.Value = profileToEdit.TradersInfo[trader].loyaltyLevel;
            traderSalesNumericBox.Value = profileToEdit.TradersInfo[trader].salesSum;
            traderStandingNumericBox.Value = profileToEdit.TradersInfo[trader].standing;
            traderUnlockedCkeckBox.Checked = profileToEdit.TradersInfo[trader].unlocked;
        }

        private void traderUnlockedCkeckBox_CheckedChanged(object sender, EventArgs e)
        {
            profileToEdit.TradersInfo[getSelectedTrader()].unlocked = this.traderUnlockedCkeckBox.Checked;
        }

        private void traderStandingNumericBox_ValueChanged(object sender, EventArgs e)
        {
            profileToEdit.TradersInfo[getSelectedTrader()].standing = this.traderStandingNumericBox.Value;
        }

        private void traderSalesNumericBox_ValueChanged(object sender, EventArgs e)
        {
            profileToEdit.TradersInfo[getSelectedTrader()].salesSum = (int)Math.Round(this.traderSalesNumericBox.Value,0);
        }

        private void traderLevelNumericBox_ValueChanged(object sender, EventArgs e)
        {
            profileToEdit.TradersInfo[getSelectedTrader()].loyaltyLevel = (int)Math.Round(this.traderLevelNumericBox.Value,0);
        }

        private string getSelectedTrader()
        {
            string trader = this.traderListComboBox.SelectedItem.ToString();
            string traderid = tradersNames.FirstOrDefault(x => x.Value == trader).Key;

            if (traderid != null) { trader = traderid; }//if its not a modded trader, convert the name into the trader id
            return trader;
        }

        private void SaveButton_Click(object sender, EventArgs e) //saving the profile 
        {
            profileToEdit.info.nickname = nicknameTextBox.Text;
            profileToEdit.info.side = sideselectorComboBox.SelectedItem.ToString();
            profileToEdit.info.experience = ExperienceTrackBar.Value;
            profileToEdit.info.gameVersion = gameVersionCombo.SelectedItem.ToString();

            profileToEdit.SetSkillValue("Endurance", EnduranceTrackBar.Value);
            profileToEdit.SetSkillValue("Strength", StrengthTrackBar.Value);
            profileToEdit.SetSkillValue("Vitality", VitalityTrackBar.Value);
            profileToEdit.SetSkillValue("Health", HealthTrackBar.Value);
            profileToEdit.SetSkillValue("StressResistance", StressTrackBar.Value);
            profileToEdit.SetSkillValue("Metabolism", MetabolismTrackbar.Value);
            profileToEdit.SetSkillValue("Immunity", ImmunityTrackbar.Value);

            profileToEdit.SetSkillValue("Perception", PerceptionTrackBar.Value);
            profileToEdit.SetSkillValue("Intellect", IntelectTrackBar.Value);
            profileToEdit.SetSkillValue("Attention", AttentionTrackBar.Value);
            profileToEdit.SetSkillValue("Charisma", CharismaTrackBar.Value);
            profileToEdit.SetSkillValue("Memory", AttentionTrackBar.Value);

            profileToEdit.SetSkillValue("CovertMovement", CovertTrackBar.Value);
            profileToEdit.SetSkillValue("Search", SearchingTrackBar.Value);
            profileToEdit.SetSkillValue("MagDrills", MagdrillsTrackBar.Value);
            profileToEdit.SetSkillValue("AimDrills", AimTrackBar.Value);
            profileToEdit.SetSkillValue("Surgery", SurgeryTrackBar.Value);
            profileToEdit.SetSkillValue("ProneMovement", ProneTrackBar.Value);
            profileToEdit.SetSkillValue("Crafting", CraftingTrackBar.Value);
            profileToEdit.SetSkillValue("HideoutManagement", HideoutTrackBar.Value);

            profileToEdit.SetSkillValue("RecoilControl", RecoilTrackBar.Value);
            profileToEdit.SetSkillValue("WeaponTreatment", WeaponTreatmentTrackBar.Value);
            profileToEdit.SetSkillValue("TroubleShooting", TroubleShootingTrackBar.Value);

            profileToEdit.SetSkillValue("LightVests", LightVestsTrackBar.Value);
            profileToEdit.SetSkillValue("HeavyVests", HeavyVestsTrackBar.Value);


            //weapon skills
            profileToEdit.SetSkillValue("Melee", MeleeTrackBar.Value);
            profileToEdit.SetSkillValue("Pistol", PistolTrackBar.Value);
            profileToEdit.SetSkillValue("SMG", SMGTrackBar.Value);
            profileToEdit.SetSkillValue("Assault", AssaultTrackBar.Value);
            profileToEdit.SetSkillValue("Shotgun", ShotgunTrackBar.Value);
            profileToEdit.SetSkillValue("Sniper", SniperTrackBar.Value);
            profileToEdit.SetSkillValue("LMG", LMGTrackBar.Value);
            profileToEdit.SetSkillValue("HMG", HMGTrackBar.Value);
            profileToEdit.SetSkillValue("DMR", DMRTrackBar.Value);
            profileToEdit.SetSkillValue("Launcher", LauncherTrackBar.Value);
            profileToEdit.SetSkillValue("Throwing", ThrowingTrackBar.Value);
            profileToEdit.SetSkillValue("Revolver", RevolverTrackBar.Value);
            profileToEdit.SetSkillValue("AttachedLauncher", AttachedLauncherTrackBar.Value);         

            try
            {
                //using (StreamWriter file = File.CreateText( Path.Combine(Globals.profilesFolder, "profiles/1/TestSaveProfile.json") ))
                
                dynamic ProfileJSON = JObject.Parse( File.ReadAllText(profilePath) );

                dynamic characterPmcData = ProfileJSON["characters"]["pmc"];
                characterPmcData.SelectToken("Info")["Nickname"] = profileToEdit.info.nickname;
                characterPmcData.SelectToken("Info")["Side"] = profileToEdit.info.side;
                characterPmcData.SelectToken("Info")["Experience"] = profileToEdit.info.experience;
                characterPmcData.SelectToken("Info")["GameVersion"] = profileToEdit.info.gameVersion;

                for (int i = 0; i < profileToEdit.skills.common.Count; i++)
                {
                    characterPmcData.SelectToken("Skills").SelectToken("Common")[i]["Progress"] = profileToEdit.skills.common[i].progress;
                }

                for (int i = 0; i < profileToEdit.skills.mastering.Count; i++)
                {
                    characterPmcData.SelectToken("Skills").SelectToken("Mastering")[i]["Progress"] = profileToEdit.skills.mastering[i].progress;
                }

                foreach (ProfileExtended.Hideout.Area a in profileToEdit.hideout.areas)
                {
                    characterPmcData.SelectToken("Hideout").SelectToken("$.Areas[?(@.type == "+a.type+")]")["level"] = a.level; //some json LINQ magic i don't understand....
                }

                foreach(KeyValuePair<string, ProfileExtended.Trader> T in profileToEdit.TradersInfo )
                {   
                    if(T.Key.Contains('.') == false)
                    {
                        characterPmcData.SelectToken("TradersInfo." + T.Key)["loyaltyLevel"] = T.Value.loyaltyLevel;
                        characterPmcData.SelectToken("TradersInfo." + T.Key)["salesSum"] = T.Value.salesSum;
                        characterPmcData.SelectToken("TradersInfo." + T.Key)["standing"] = T.Value.standing;
                        characterPmcData.SelectToken("TradersInfo." + T.Key)["unlocked"] = T.Value.unlocked;
                    }
                                                              
                }
                
                ProfileJSON["characters"]["pmc"] = characterPmcData;
                ProfileJSON["info"]["wipe"] = this.wipeProfileCheckbox.Checked;

                using (StreamWriter file = File.CreateText(profilePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(file, ProfileJSON);
                }
                
                MessageBox.Show("profile succesfully saved");
                                
            }
            catch(Exception ex)
            {
                MessageBox.Show("something went wrong : " + ex.Message);
            }

        }

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

        internal class MasteringWeaponLevel
        {
            public string Name;
            public int Level2;
            public int Level3;

            public MasteringWeaponLevel(string name, int l2,int l3) 
            {
                Name = name;
                Level2 = l2;
                Level3 = l3;
            }
        }


    }
}
