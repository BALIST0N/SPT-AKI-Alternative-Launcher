using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Linq;

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
			this.backendUrlLabel.Text = this.backendUrlLabel.Text + Globals.clientConfig.BackendUrl;
			LoadProfiles();
		}

		public void LoadProfiles()
		{
			if (!File.Exists(Globals.accountsFile))
			{
				MessageBox.Show("unable to find profiles, make sure the launcher is in Emutarkov server folder");
				return;
			}

			using (StreamReader r = new StreamReader(Globals.accountsFile))
			{
				Dictionary<string, Profile> dico = JsonConvert.DeserializeObject<Dictionary<string, Profile>>(r.ReadToEnd());

				if (dico != null)
				{
					if(dico.Count == 0)
					{
						profileEditButton.Text = "Create Profile";
						profileEditButton.Enabled = true;
					}
					else
					{
						foreach (string key in dico.Keys)
						{
							profilesListBox.Items.Add(dico[key].nickname);
							profiles.Add(dico[key]);
						}
					}
				}
				else
				{
					//account file is corrupted ..? 
					profileEditButton.Text = "Create Profile";
					profileEditButton.Enabled = true;
				}
			}
		}

		//check everything bafore enabling buttons.
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

		private void profilesListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(profilesListBox.SelectedIndex > 0) { validateValues(); }
		}

		private void profileEditButton_Click(object sender, EventArgs e)
		{
			if(profileEditButton.Text == "Create Profile")
			{
				if (Application.OpenForms.OfType<Views.CreateAccountForm>().Count() == 0)
				{
					Views.CreateAccountForm c = new Views.CreateAccountForm(this.Location);
					c.Show();
				}
			}
			else
			{
				string profileid = profiles[profilesListBox.SelectedIndex - 1].id;
				if (File.Exists(Path.Combine(Globals.profilesFolder, profileid + "/character.json")))
				{
					if (Application.OpenForms.OfType<EditProfileForm>().Count() == 0)
					{
						EditProfileForm edit = new EditProfileForm(profileid, this.Location);
						edit.Show();
					}
				}
				else
				{
					MessageBox.Show("this profile does't have data, launch the game for being able to edit your profile");
				}
			}
		}

		private string GenerateToken(string email, string password,string accountid)
		{
			LoginToken token = new LoginToken(email, password);
			string convertedStr = Convert.ToBase64String(Encoding.UTF8.GetBytes( JsonConvert.SerializeObject(token) )) + "=";
			return "-bC5vLmcuaS5u=" + convertedStr + " -token=" + accountid + " -config=" + JsonConvert.SerializeObject(Globals.clientConfig);

			//return "-bC5vLmcuaS5u=" + convertedStr + " -token=" + accountid;


		}

		//**************************************************//
		//					PROCESS FUNCTIONS				//
		//**************************************************//
		private void startButton_Click(object sender, EventArgs e)
		{
			int select = profilesListBox.SelectedIndex -1;
			
			if (startServerChackBox.Checked || startServerChackBox.Visible == false)
			{
				try
				{
					LaunchServer();
				}
				catch(Exception ex)
				{
					MessageBox.Show("something went wrong :" + ex.Message );
				}
			}

			// start game
			ProcessStartInfo startGame = new ProcessStartInfo(Path.Combine(Globals.gameFolder, "EscapeFromTarkov.exe"));
			startGame.Arguments = GenerateToken(profiles[select].email, profiles[select].password,profiles[select].id);
			startGame.UseShellExecute = false;
			startGame.WorkingDirectory = Globals.gameFolder;
			Process.Start(startGame);
			this.startButton.Enabled = false;
		}

		private void LaunchServer()
		{
			Process proc = new Process();
			proc.StartInfo.WorkingDirectory = Globals.serverFolder;

			if( File.Exists(Path.Combine(Globals.serverFolder, "EmuTarkov-Server.exe")))
			{
				proc.StartInfo.FileName = Path.Combine(Globals.serverFolder, "EmuTarkov-Server.exe");
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
			else if( File.Exists(Path.Combine(Globals.serverFolder, "start.bat")) )
			{
				proc.StartInfo.FileName =  Path.Combine(Globals.serverFolder, "start.bat");
				proc.Start();
			}
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
				res = res.Replace(@"(\\e\[[0-1];3[0-9])m", ""); // it should replace all \e[0;32m things
				res = res.Replace("[2J[0;0f", "").Replace("[0m", "").Replace("[36m", "").Replace("[37m", "").Replace("[40m", "").Replace("[41m", "").Replace("[42m", "").Replace("[43m", "");
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

        private void backendUrlLabel_Click(object sender, EventArgs e)
        {
			//popup window to allow change backend url
			MessageBox.Show("you will be able to change the back url soon");
        }
    }

    internal class Profile
	{
		public string id;
		public string nickname;
		public string email;
		public string password;
		public bool wipe;
		public string edition;

		public Profile(){}
		
	} 

	internal class LoginToken
	{
		public string email;
		public string password;
		public bool toggle;
		public long timestamp;

		public LoginToken(string email, string password)
		{
			this.email = email;
			this.password = password;
			this.toggle = true;
			this.timestamp = 132178097635361483;
		}
	}
}
