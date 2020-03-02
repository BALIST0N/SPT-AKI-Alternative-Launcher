using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace EFT_Launcher_12
{
	public partial class MainWindow : Form
	{
		private Profile[] profiles = new Profile[10];
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
			LoadProfiles();

		}

		public void LoadProfiles()
		{
			if (!File.Exists(Path.Combine(Globals.profilesFolder, "profiles.config.json")))
			{
				MessageBox.Show("unable to find profiles, make sure the launcher is in Emutarkov server folder");
				return;
			}

			using (StreamReader r = new StreamReader(Path.Combine(Globals.profilesFolder, "profiles.config.json")))
			{
				Dictionary<string, Profile> dico = JsonConvert.DeserializeObject<Dictionary<string, Profile>>(r.ReadToEnd());

				foreach(string key in dico.Keys)
				{
					profilesListBox.Items.Add(dico[key].email);
					profiles[dico[key].id] = dico[key];
				}
			}
		}

		// we need to check absolutely everything.
		// if there is one thing we learned from the 0.8.0-alpha,
		// it's that people don't or can't read.
		//baliston : people can read, they are just lazy as F, so make the launcher as simple as possible
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

				if ( File.Exists(Path.Combine(gamePathTextBox.Text, "client.config.json")) == false )
				{
					backendUrlLabel.Text = "Backend URL : ?";
				}
				else
				{
					string json = File.ReadAllText(Path.Combine(Globals.gameFolder, "client.config.json"));
					Globals.clientConfig = JsonConvert.DeserializeObject<ClientConfig>(json);
					backendUrlLabel.Text = "Backend URL : " + Globals.clientConfig.BackendUrl;
				}
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

		private void backendUrlTextBox_TextChanged(object sender, EventArgs e)
		{
			validateValues();
		}

		private void profilesListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			validateValues();
		}

		private void profileEditButton_Click(object sender, EventArgs e)
		{
			int profileid = profiles[profilesListBox.SelectedIndex].id;
			if( File.Exists(Path.Combine(Globals.profilesFolder, "Profiles/" + profileid + @"\character.json")) )
			{
				EditProfileForm edit = new EditProfileForm(profileid);
				edit.Show();
			}		
			else
			{
				MessageBox.Show("this profile does't have data, launch the game for being able to edit your profile");
			}
		}

		private string GenerateToken(string email, string password)
		{
			LoginToken token = new LoginToken(email, password);
			string beginKey = "-bC5vLmcuaS5u=";
			string endKey = "=";

			// serialize login token
			string serialized = JsonConvert.SerializeObject(token);

			// encode login token to base64
			string result = Convert.ToBase64String(Encoding.UTF8.GetBytes(serialized));

			// add begin and end part of the token
			return beginKey + result + endKey;
		}


		//**************************************************//
		//					PROCESS FUNCTIONS				//
		//**************************************************//
		private void startButton_Click(object sender, EventArgs e)
		{
			int select = profilesListBox.SelectedIndex;
			
			if (Globals.launchServer)
			{
				try
				{
					LaunchServer();
					this.Height = 400;
					this.killServerButton.Show();
				}
				catch(Exception ex)
				{
					MessageBox.Show("something went wrong :" + ex.Message );
				}
			}

			// start game
			ProcessStartInfo startGame = new ProcessStartInfo(Path.Combine(Globals.gameFolder, "EscapeFromTarkov.exe"));
			startGame.Arguments = GenerateToken(profiles[select].email, profiles[select].password) + " -token=" + select + " -screenmode=fullscreen";
			startGame.UseShellExecute = false;
			startGame.WorkingDirectory = Globals.gameFolder;
			Process.Start(startGame);
			this.startButton.Enabled = false;
		}

		// package-private doesn't benefit from the optimizations that private has
		private void LaunchServer()
		{
			Process proc = new Process();

			// normal properties
			proc.StartInfo.CreateNoWindow = true;
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.WorkingDirectory = Globals.serverFolder;
			if( File.Exists(Path.Combine(Globals.serverFolder, "EmuTarkov-Server.exe")))
			{
				proc.StartInfo.FileName = Path.Combine(Globals.serverFolder, "EmuTarkov-Server.exe");
			}
			else
			{
				proc.StartInfo.FileName =  Path.Combine(Globals.serverFolder, "start.bat");
			}
			
			
			// stdout
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
		}

		private void killServer()
		{
			// what is the point of throw-catch if you're not going to use it?
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
				ResetLauncherCallback d = new ResetLauncherCallback(resetLauncherSize);
				Invoke(d, new object[] { });
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
		public int id;
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
