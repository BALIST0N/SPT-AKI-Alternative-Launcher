﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Newtonsoft.Json.Linq;
using EFT_Launcher_12.Views;
using System.Reflection;

namespace EFT_Launcher_12
{
    public partial class MainWindow : Form
    {
        private List<Profile> profiles = new List<Profile>();
        private delegate void SetTextCallback(string text);
        private delegate void ResetLauncherCallback();
        private string serverProcessName;

        public MainWindow()
        {
            InitializeComponent();
            this.FormClosing += MainWindow_FormClosing;
            startButton.Enabled = false;
            profileEditButton.Enabled = false;
            profilesListBox.SelectedIndex = 0;
            this.gamePathTextBox.Text = Globals.gameFolder;
            this.backendUrlTextBox.Text = Globals.backendUrl;
            this.backendUrlTextBox.TextChanged += backendUrlTextBox_TextChanged;
            this.profilesListBox.SelectedIndexChanged += profilesListBox_SelectedIndexChanged;
            this.gamePathTextBox.TextChanged += gamePathTextBox_TextChanged;
        }

        //**************************************************//
        //              MAIN WINDOW EVENTS                  //
        //**************************************************//

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            LoadProfiles();
            backendUrlTextBox.Width = TextRenderer.MeasureText(backendUrlTextBox.Text, backendUrlTextBox.Font).Width;
        }

        private void gamePathTextBox_TextChanged(object sender, EventArgs e)
        {
            validateValues();
        }

        private void gamePathTextBox_Click(object sender, EventArgs e)
        {   
            //event when click on the "game Location" textbox, open a folder dialog and set into the textbox
            if(startButton.Enabled == false)
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
            int select = profilesListBox.SelectedIndex - 1;

            if (startServerChackBox.Checked || startServerChackBox.Visible == false)
            {
                try
                {
                    if (LaunchServer() == true)
                    {
                        ApplyDllPatch();
                        StartGame(profiles[select].id);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("something went wrong :" + ex.Message);
                }
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


        public void ApplyDllPatch()
        {
            string dll = Globals.gameFolder + "/EscapeFromTarkov_Data/Managed/Assembly-CSharp.dll";
            if (File.Exists(dll + ".bak") == false )
            {
                File.Move(dll, dll + ".bak");
            }

            Stream ResourceFile = Assembly.GetExecutingAssembly().GetManifestResourceStream("EFT_Launcher_12.Resources.Assembly-CSharp.dll");
            using ( var fileStream = File.Create(dll) )
            {
                ResourceFile.Seek(0, SeekOrigin.Begin);
                ResourceFile.CopyTo(fileStream);
            }
            ResourceFile.Dispose();
            ResourceFile.Close();
        }

        public void CleanGameFiles()
        {
            var files = new string[]
            {
                Path.Combine(Globals.gameFolder, "BattlEye"),
                Path.Combine(Globals.gameFolder, "Logs"),
                Path.Combine(Globals.gameFolder, "ConsistencyInfo"),
                Path.Combine(Globals.gameFolder, "EscapeFromTarkov_BE.exe"),
                Path.Combine(Globals.gameFolder, "Uninstall.exe"),
                Path.Combine(Globals.gameFolder, "UnityCrashHandler64.exe"),
                Path.Combine(Globals.gameFolder, "WinPixEventRuntime.dll")
            };

            foreach (var file in files)
            {
                if (Directory.Exists(file)) { RemoveFilesRecurse( new DirectoryInfo(file) ); }
                if (File.Exists(file)) { File.Delete(file); }
            }

            //***************** registry *****************
            try
            {
                var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Battlestate Games\EscapeFromTarkov", true);
                foreach (var value in key.GetValueNames()) { key.DeleteValue(value); }
            }
            catch{}


            //**************** temp files ********************
            var rootdir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), @"Battlestate Games\EscapeFromTarkov"));
            if (rootdir.Exists) { RemoveFilesRecurse(rootdir); }
           
        }


        public void RemoveFilesRecurse(DirectoryInfo basedir)
        {
            if (basedir.Exists)
            {
                try
                {
                    foreach (var dir in basedir.EnumerateDirectories()) { RemoveFilesRecurse(dir); } // remove subdirectories
                    var files = basedir.GetFiles(); // remove files

                    foreach (var file in files)
                    {
                        file.IsReadOnly = false;
                        file.Delete();
                    }
                    basedir.Delete();
                }
                catch (Exception) {}
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

            if (gameExists && profileExists && serverProcessName == null)
            {
                startButton.Enabled = true;
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
                this.Height = 190;
                serverOutputRichBox.Text = "";
                this.startButton.Enabled = true;
                this.killServerButton.Hide();
            }
        }


        //**************************************************//
        //              PROCESS FUNCTIONS & EVENTS          //
        //**************************************************//

        private bool LaunchServer()
        {
            /* start the server exe silently and with a few parameters 
            receive all the console output into a rich textbox */
            Process proc = new Process();
            proc.StartInfo.WorkingDirectory = Globals.serverFolder;

            if (File.Exists(Path.Combine(Globals.serverFolder, "Server.exe")))
            {
                proc.StartInfo.FileName = Path.Combine(Globals.serverFolder, "Server.exe");
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false; //very important, tie the console window with the launcher so the application can read the console

                proc.StartInfo.RedirectStandardError = true; //redirect everything otherwise nothing will be send
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.StandardOutputEncoding = Encoding.UTF8; //utf-8 because i didn't find better readable text
                proc.EnableRaisingEvents = true; // /!\ enables handlers for reading info
                proc.Exited += ServerTerminated;

                proc.Start();

                proc.BeginOutputReadLine(); 
                proc.OutputDataReceived += proc_OutputDataReceived;
                serverProcessName = proc.ProcessName;

                this.Height = 400;
                this.killServerButton.Show();
                return true;
            }
            else
            {
                MessageBox.Show("we didn't find server ?");
                return false;
            }
        }

        private void StartGame(string id)
        {
            ProcessStartInfo startGame = new ProcessStartInfo(Path.Combine(Globals.gameFolder, "EscapeFromTarkov.exe"))
            {
                //$"-force-gfx-jobs native -token={account.id} -config={Json.Serialize(new ClientConfig(server.backendUrl))}";
                Arguments = "-token=" + id + " -config={'BackendUrl':'" + Globals.backendUrl + "','Version':'live'}",
                UseShellExecute = false,
                WorkingDirectory = Globals.gameFolder
            };

            Process.Start(startGame);
            this.startButton.Enabled = false;
        }

        private void killServer()
        {
            Process[] procs = Process.GetProcessesByName(serverProcessName);

            if (procs.Length > 0)
            {
                procs[0].Kill();
            }
            serverProcessName = null;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            killServer();
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
            if (serverOutputRichBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetConsoleOutputText);
                Invoke(d, new object[] { text });
            }
            else
            {
                serverOutputRichBox.Text += text;
                serverOutputRichBox.SelectionStart = serverOutputRichBox.Text.Length;
                serverOutputRichBox.ScrollToCaret();
            }
        }

        private void killServerButton_Click(object sender, EventArgs e)
        {
            killServer();
        }

        private void ServerTerminated(object sender, EventArgs e)
        {
            resetLauncherSize();
        }
    }

	internal class Profile
	{
		public string id;
		public string username;
		public string password;
		public bool wipe;
		public string edition;

		public Profile(){}
		
	} 
}
