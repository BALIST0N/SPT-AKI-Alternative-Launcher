using System;
using System.IO;

namespace EFT_Launcher_12
{
	public class Globals
	{
		public static string gameFolder;
		public static string serverFolder;
		public static string profilesFolder;
		public static string backendUrl;
		
		static Globals()
		{
			
			//serverFolder = Environment.CurrentDirectory;
			serverFolder = "D:/tarkov/EFT2";

			profilesFolder = serverFolder + "/user/profiles";

			gameFolder = Properties.Settings.Default.gameFolder;
			backendUrl = Properties.Settings.Default.backendURL;
				
		}
	}
}