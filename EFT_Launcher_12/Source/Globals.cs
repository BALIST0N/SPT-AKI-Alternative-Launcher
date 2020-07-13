using System;
using System.IO;

namespace EFT_Launcher_12
{
	public class Globals
	{
		public static string gameFolder;
		public static string serverFolder;
		public static string accountsFile;
		public static string profilesFolder;
		public static ClientConfig clientConfig;
		
		static Globals()
		{
			//serverFolder = Environment.CurrentDirectory;
			serverFolder = "D:/tarkov/Server";

			accountsFile = serverFolder + "/user/configs/accounts.json";
			profilesFolder = serverFolder + "/user/profiles";

			gameFolder = Properties.Settings.Default.gameFolder;
			clientConfig = new ClientConfig
			{
				BackendUrl = Properties.Settings.Default.backendURL,
				Version = "live"
			};
		}
	}
}