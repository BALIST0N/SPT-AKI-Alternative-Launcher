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
			serverFolder = "Y:/tarkov/emutarkov 12.4-R2";

			accountsFile = serverFolder + "/user/configs/accounts.json";
			profilesFolder = serverFolder + "/user/profiles";

			gameFolder = Properties.Settings.Default.gameFolder;
			clientConfig = new ClientConfig
			{
				BackendUrl = "https://127.0.0.1:443",
				Version = "live",
				BuildVersion = "000",
				LocalGame = false,
				AmmoPoolSize = -1,
				WeaponsPoolSize = -1,
				MagsPoolSize = -1,
				ItemsPoolSize = -1,
				PlayersPoolSize = 30,
				ObservedFix = 1,
				TargetFrameRate = -1,
				BotsCount = -1,
				ResetSettings = false,
				SaveResults = true,
				FixedFrameRate = 60
			};
		}
	}
}