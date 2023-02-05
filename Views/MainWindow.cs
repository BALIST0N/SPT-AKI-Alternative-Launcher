using Newtonsoft.Json.Linq;
using SPTAKI_Alt_Launcher.Properties;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace SPTAKI_Alt_Launcher
{
    public partial class MainWindow : Form
    {
        private List<Profile> profiles = new List<Profile>();
        private delegate void SetTextCallback(string text);
        private delegate void ResetLauncherCallback();
        private Process serverProcess;
        private int gameProcessId;

        public MainWindow()
        {
            InitializeComponent();
            FormClosing += MainWindow_FormClosing;
            startButton.Enabled = false;
            profileEditButton.Enabled = false;
            profilesListBox.SelectedIndex = 0;
            gamePathTextBox.Text = Globals.gameFolder;
            backendUrlTextBox.Text = Globals.backendUrl;
            backendUrlTextBox.TextChanged += backendUrlTextBox_TextChanged;
            profilesListBox.SelectedIndexChanged += profilesListBox_SelectedIndexChanged;
            gamePathTextBox.TextChanged += gamePathTextBox_TextChanged;
        }

        //**************************************************//
        //              MAIN WINDOW EVENTS                  //
        //**************************************************//

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            LoadProfiles();
            backendUrlTextBox.Width = TextRenderer.MeasureText(backendUrlTextBox.Text, backendUrlTextBox.Font).Width;
            serverOutputRichBox.Text = " "; //load someting on the RTB cuz sometimes font doesn't load properly..?
            serverOutputRichBox.Font = new Font("Consolas", 10);
        }

        private void gamePathTextBox_TextChanged(object sender, EventArgs e)
        {
            validateValues();
        }

        private void gamePathTextBox_Click(object sender, EventArgs e)
        {
            //event when click on the "game Location" textbox, open a folder dialog and set into the textbox
            if (startButton.Enabled == false)
            {
                if (GameLocationFolderBrowser.ShowDialog() == DialogResult.OK)
                {
                    gamePathTextBox.Text = GameLocationFolderBrowser.SelectedPath;
                    validateValues();
                }
            }
        }
        private void profilesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            validateValues();
        }

        private void profileEditButton_Click(object sender, EventArgs e)
        {
            string profileid = profiles.Find(x => x.username == profilesListBox.SelectedItem.ToString()).id;
            string path = Path.Combine(Globals.profilesFolder, profileid + ".json");

            if (Application.OpenForms.OfType<EditProfileForm>().Count() == 0 && File.Exists(path))
            {
                EditProfileForm edit = new EditProfileForm(profileid, this.Location);
                edit.Show();
            }
        }

        private void backendUrlTextBox_TextChanged(object sender, EventArgs e)
        {
            backendUrlTextBox.Width = TextRenderer.MeasureText(backendUrlTextBox.Text, backendUrlTextBox.Font).Width;
            Globals.backendUrl = backendUrlTextBox.Text;
            Properties.Settings.Default.backendURL = backendUrlTextBox.Text;
            Properties.Settings.Default.Save();

            /*
            bool httpStr = Regex.IsMatch(backendUrlTextBox.Text, "https://|http://", RegexOptions.IgnoreCase);
            string ip = Regex.Replace(backendUrlTextBox.Text, "https://|http://", "", RegexOptions.IgnoreCase);
            bool y = Regex.IsMatch(ip, @"/[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}:[0-9]{1,5}/");
            if (httpStr == true && y == true)
            {
                this.backendUrlTextBox.ForeColor = Color.Black;
            }
            else
            {
                this.backendUrlTextBox.ForeColor = Color.Red;
            }*/

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (startServerChackBox.Checked || startServerChackBox.Visible == false)
            {
                LaunchServer();
            }
        }


        //**************************************************//
        //					 FUNCTIONS      				//
        //**************************************************//
        public void LoadProfiles()
        {
            if (Directory.Exists(Globals.profilesFolder) == false) //if "profiles folder doesn't exist ?"
            {
                MessageBox.Show("unable to find profiles, make sure the launcher is in SPT-AKI SERVER folder");
            }
            else
            {
                var profilesFiles = Directory.GetFiles(Globals.profilesFolder);

                if (profilesFiles.Length == 0) //file count = 0
                {
                    //MessageBox.Show("There is no profiles actually ...? ");
                    CreateProfileForm C = new CreateProfileForm(this.Location);
                    C.Show();
                }
                else
                {
                    foreach (string filePath in profilesFiles) //for each file in the directory
                    {
                        using (StreamReader r = new StreamReader(filePath))
                        {
                            dynamic profileDATA = JObject.Parse(r.ReadToEnd());
                            //create a new profile Object from the json file
                            profiles.Add(new Profile()
                            {
                                id = (string)profileDATA["info"]["id"],
                                username = (string)profileDATA["info"]["username"],
                                password = (string)profileDATA["info"]["password"],
                                wipe = (bool)profileDATA["info"]["wipe"],
                                edition = (string)profileDATA["info"]["edition"]
                            });
                            //don't forget to add them into the profile selection
                            profilesListBox.Items.Add((string)profileDATA["info"]["username"]);
                        }
                    }
                }
            }
        }

        private void validateValues()
        {
            bool gameExists = false;
            bool profileExists = false;

            // game
            if (File.Exists(Path.Combine(gamePathTextBox.Text, "EscapeFromTarkov.exe")))
            {
                gameExists = true;
                gamePathTextBox.ForeColor = Color.White;
                Globals.gameFolder = gamePathTextBox.Text;
                Properties.Settings.Default.gameFolder = Globals.gameFolder;
                Properties.Settings.Default.Save();
                backendUrlTextBox.Visible = true;
            }
            else
            {
                gamePathTextBox.ForeColor = Color.Red; //signal to the user its wrong game folder
                backendUrlTextBox.Visible = false;
            }

            if (profilesListBox.SelectedIndex > 0) //if there is a profile selected
            {
                profileExists = true;
                profileEditButton.Enabled = true;
            }
            else
            {
                profileEditButton.Enabled = false;
            }

            if (gameExists && profileExists )
            {
                startButton.Enabled = true;

                //check if server is running
                if(this.serverProcess != null)
                {
                    foreach (Process theprocess in Process.GetProcesses())
                    {
                        if (theprocess.Id == this.serverProcess.Id)
                        {
                            startButton.Enabled = false;
                        }
                    }
                }
            }
            else
            {
                startButton.Enabled = false;
            }
        }
        private void resetLauncherSize()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (serverOutputRichBox.InvokeRequired)
            {
                Invoke(new ResetLauncherCallback(resetLauncherSize));
            }
            else
            {
                this.Height = 225;
                this.serverOutputRichBox.Clear();
                this.startButton.Enabled = true;
                this.killServerButton.Hide();
            }
        }


        //**************************************************//
        //              PROCESS FUNCTIONS & EVENTS          //
        //**************************************************//

        private void LaunchServer()
        {
            string serverExe =  Directory.GetFiles(Globals.serverFolder, "*Server.exe")[0];

            /* start the server exe silently and with a few parameters 
            receive all the console output into a rich textbox */
            this.serverProcess = new Process();
            this.serverProcess.StartInfo.WorkingDirectory = Globals.serverFolder;

            if (File.Exists(serverExe) )
            {
                this.serverProcess.StartInfo.FileName = serverExe;
                this.serverProcess.StartInfo.CreateNoWindow = true;
                this.serverProcess.StartInfo.UseShellExecute = false; //very important, tie the console window with the launcher so the application can read the console
                
                this.serverProcess.StartInfo.RedirectStandardError = true; //redirect everything otherwise nothing will be send
                this.serverProcess.StartInfo.RedirectStandardInput = true;
                this.serverProcess.StartInfo.RedirectStandardOutput = true;
                this.serverProcess.StartInfo.StandardOutputEncoding = Encoding.UTF8; //utf-8 because i didn't find better readable text
                this.serverProcess.EnableRaisingEvents = true; // /!\ enables handlers for reading info
                this.serverProcess.Exited += ServerTerminated;
                this.serverProcess.Start();
                
                this.serverProcess.BeginOutputReadLine();
                this.serverProcess.OutputDataReceived += proc_OutputDataReceived;

                this.Height = 450;
                this.killServerButton.Show();

            }
            else
            {
                MessageBox.Show("we didn't find server ?");
            }                                                                             
        }

        private void StartGame(string id)
        {
            string dll = Globals.gameFolder + "/EscapeFromTarkov_Data/Managed/Assembly-CSharp.dll";
            string bpf = Globals.serverFolder + "/Aki_Data/Launcher/Patches/aki-core/EscapeFromTarkov_Data/Managed/Assembly-CSharp.dll.bpf";

            Aki.Launcher.Helpers.FilePatcher.Patch(dll, bpf, false);

            ProcessStartInfo startGame = new ProcessStartInfo(Path.Combine(Globals.gameFolder, "EscapeFromTarkov.exe"))
            {
                //$"-force-gfx-jobs native -token={account.id} -config={Json.Serialize(new ClientConfig(server.backendUrl))}";
                Arguments = "-token=" + id + " -config={'BackendUrl':'" + Globals.backendUrl + "','Version':'live'}",
                UseShellExecute = false,
                WorkingDirectory = Globals.gameFolder
            };

            Process p = Process.Start(startGame);
            this.gameProcessId = p.Id;
            this.startButton.Enabled = false;
            
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            killServer();
        }

        private void killServerButton_Click(object sender, EventArgs e)
        {
            killServer();
        }

        private void killServer()
        {
            if(this.serverProcess != null) 
            {
                this.serverProcess.Kill(true);
                this.serverProcess.WaitForExitAsync();
            }
        }

        private void ServerTerminated(object sender, EventArgs e)
        {
            resetLauncherSize();
        }

        void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            string res = e.Data;
            if (res != null)
            {
                res = System.Text.RegularExpressions.Regex.Replace(res, @"\[[0-1];[0-9][a-z]|\[[0-9][0-9][a-z]|\[[0-9][a-z]|\[[0-9][A-Z]", String.Empty); //it should replace all [0;32m things
            }
            SetConsoleOutputText(res + "\n");
        }

        private void SetConsoleOutputText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.serverOutputRichBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetConsoleOutputText);
                Invoke(d, new object[] { text });
            }
            else
            {
                this.serverOutputRichBox.Text += text;
                this.serverOutputRichBox.SelectionStart = serverOutputRichBox.Text.Length;
                this.serverOutputRichBox.ScrollToCaret();

                if (text.Contains(Settings.Default.backendURL) == true )
                {
                    StartGame( profiles[profilesListBox.SelectedIndex - 1].id );
                }

            }
        }


    }

    internal class Profile
    {
        public string id;
        public string username;
        public string password;
        public bool wipe;
        public string edition;

        public Profile() { }       

    }
}
