using System;
using System.IO;

namespace EFT_Launcher_12
{
	public class Globals
	{
		public static string gameFolder;
		public static string serverFolder;
		public static string profilesFolder;
		public static ClientConfig clientConfig;
		
		static Globals()
		{
			//serverFolder = Environment.CurrentDirectory;
			serverFolder = "D:/tarkov/AKI-1.1.0";

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