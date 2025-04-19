using System;
using System.IO;
using Server;

namespace MultiplayerSFS.Server
{
    public class Program
    {
	    private const string CONFIG_FILENAME = "Multiplayer.cfg";
		public static void Main()
		{
			try
			{
				ServerUI.RefreshUI();
				ServerSettings settings;
				if (!File.Exists(CONFIG_FILENAME))
				{
					Logger.Info($"'{CONFIG_FILENAME}' not found, running with default settings...", true);
					settings = new ServerSettings();
					File.WriteAllText(CONFIG_FILENAME, settings.Serialize());
				}
				else
				{
					Logger.Info($"Loading '{CONFIG_FILENAME}'...", true);
					settings = ServerSettings.Deserialize(File.ReadAllText(CONFIG_FILENAME));
				}
				Server.Initialize(settings);
				Server.Run();
			}
			catch (Exception e)
			{
				Logger.Error(e);
			}
		}
	}
}
