using System;
using Microsoft.CSharp.RuntimeBinder;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;

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

			this.profilesListBox.SelectedIndexChanged += profilesListBox_SelectedIndexChanged;
            this.gamePathTextBox.TextChanged += gamePathTextBox_TextChanged;

			this.backendUrlLabel.Text = "Backend URL : " + Globals.backendUrl;
        }

        private void MainWindow_Shown(object sender, EventArgs e)
		{
			LoadProfiles();
		}

		public void LoadProfiles()
		{
			if( Directory.Exists(Globals.profilesFolder) == false )
            {
				MessageBox.Show("unable to find profiles, make sure the launcher is in SPT-AKI SERVER folder");
			}
			else
            {
				var profilesFiles = Directory.GetFiles(Globals.profilesFolder);
				
				if (profilesFiles.Length == 0)
                {
					MessageBox.Show("There is no profiles actually ...? ");
                }
                else
				{
					foreach (string filePath in profilesFiles)
					{
						using (StreamReader r = new StreamReader(filePath))
						{
							dynamic profileDATA = JObject.Parse(r.ReadToEnd());

							profiles.Add(new Profile()
							{
								id = (string)profileDATA["info"]["id"],
								username = (string)profileDATA["info"]["username"],
								password = (string)profileDATA["info"]["password"],
								wipe = (bool)profileDATA["info"]["wipe"],
								edition = (string)profileDATA["info"]["edition"]
							});

							profilesListBox.Items.Add((string)profileDATA["info"]["username"]);
						}
					}

				}
            }
		}

		//check everything before enabling buttons.
		private void validateValues()
		{
			bool gameExists = false;
			bool profileExists = false;

			// game
			if ( File.Exists(Path.Combine(gamePathTextBox.Text, "EscapeFromTarkov.exe")) )
			{
				gameExists = true;
				gamePathTextBox.ForeColor = Color.White;
				Globals.gameFolder = gamePathTextBox.Text;
				Properties.Settings.Default.gameFolder = Globals.gameFolder;
				Properties.Settings.Default.Save();
			}
			else
			{
				gamePathTextBox.ForeColor = Color.Red;
				backendUrlLabel.Text = "Backend URL : ?";
			}

			// profile
			if (profilesListBox.SelectedIndex > 0)
			{
				profileExists = true;
				profileEditButton.Enabled = true;
			}
			else
			{
				profileEditButton.Enabled = false;
			}
			
			// start button
			if (gameExists && profileExists && serverProcessName == null)
			{
				startButton.Enabled = true;
			}
			else
			{
				startButton.Enabled = false;
			}
		}

		private void gamePathTextBox_TextChanged(object sender, EventArgs e)
		{
			validateValues();
		}
		private void gamePathTextBox_Click(object sender, EventArgs e)
		{
			if (GameLocationFolderBrowser.ShowDialog() == DialogResult.OK )
			{
                gamePathTextBox.Text = GameLocationFolderBrowser.SelectedPath;
				validateValues();
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

            if (Application.OpenForms.OfType<EditProfileForm>().Count() == 0 && File.Exists(path) )
            {
                EditProfileForm edit = new EditProfileForm(profileid, this.Location);
                edit.Show();
            }


        }

        private void backendUrlLabel_Click(object sender, EventArgs e)
        {
			//do something to change backend url

			/*
			bool httpStr = Regex.IsMatch(this.backendURLTextBox.Text, "https://", RegexOptions.IgnoreCase);
			string ip = Regex.Replace(this.backendURLTextBox.Text, "https://", "", RegexOptions.IgnoreCase);
			bool y = Regex.IsMatch(ip, "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

            if (httpStr == true && y == true)
            {
                this.backendURLTextBox.ForeColor = Color.White;
            }
            else
            {
                this.backendURLTextBox.ForeColor = Color.Red;
            }*/

		}

		private void startButton_Click(object sender, EventArgs e)
		{
			int select = profilesListBox.SelectedIndex - 1;

			if (startServerChackBox.Checked || startServerChackBox.Visible == false)
			{
				try
				{
                    LaunchServer();
					StartGame(profiles[select].id);
				}
				catch (Exception ex)
				{
					MessageBox.Show("something went wrong :" + ex.Message);
				}
			}
		}

        //**************************************************//
        //					PROCESS FUNCTIONS				//
        //**************************************************//


        private void LaunchServer()
        {
            Process proc = new Process();
            proc.StartInfo.WorkingDirectory = Globals.serverFolder;

            if (File.Exists(Path.Combine(Globals.serverFolder, "Server.exe")))
            {
                proc.StartInfo.FileName = Path.Combine(Globals.serverFolder, "Server.exe");
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;

                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                proc.EnableRaisingEvents = true;
                proc.Exited += ServerTerminated;

                proc.Start();

                proc.BeginOutputReadLine();
                proc.OutputDataReceived += proc_OutputDataReceived;
                serverProcessName = proc.ProcessName;

                this.Height = 400;
                this.killServerButton.Show();
            }
            else if (File.Exists(Path.Combine(Globals.serverFolder, "start.bat")))
            {
                proc.StartInfo.FileName = Path.Combine(Globals.serverFolder, "start.bat");
                proc.Start();
            }
        }

		private void StartGame(string id)
        {
			ProcessStartInfo startGame = new ProcessStartInfo(Path.Combine(Globals.gameFolder, "EscapeFromTarkov.exe"))
			{
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

		private void ServerTerminated(object sender, EventArgs e)
		{
			resetLauncherSize();
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			killServer();
		}

		void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			string res = e.Data;
			// get line color here
			if(res != null )
			{
				res = System.Text.RegularExpressions.Regex.Replace(res, @"\[[0-1];[0-9][a-z]|\[[0-9][0-9][a-z]|\[[0-9][a-z]|\[[0-9][A-Z]",String.Empty); //it should replace all [0;32m things
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

		private void killServerButton_Click(object sender, EventArgs e)
		{
			killServer();
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
