using System;
using System.IO;

namespace EFT_Launcher_12
{
	public class Globals
	{
		public static string gameFolder;
		public static string serverFolder;
		public static string profilesFolder;
		public static bool launchServer;
		public static bool useServerPath;
		public static ClientConfig clientConfig;
		
		static Globals()
		{
			serverFolder = Environment.CurrentDirectory;
			profilesFolder = Path.Combine(Environment.CurrentDirectory, "user");

			//serverFolder = "Y:/tarkov/emutarkov git/EmuTarkov-Server";
			//profilesFolder = "Y:/tarkov/emutarkov git/EmuTarkov-Server/user";

			gameFolder = Properties.Settings.Default.gameFolder;
			launchServer = true;
			clientConfig = null;
		}
	}
}