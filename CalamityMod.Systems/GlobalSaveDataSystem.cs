using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class GlobalSaveDataSystem : ModSystem
{
	internal static List<string> GlobalSaveDataKeys = new List<string>();

	internal static string GlobalSaveDataDirectoryPath;

	internal static string GlobalSaveDataKeysPath;

	internal static string READMEPath;

	public override void OnModLoad()
	{
		string savePathByOS = (OperatingSystem.IsLinux() ? "~/.local/share/Terraria/tModloader/" : Main.SavePath);
		GlobalSaveDataDirectoryPath = savePathByOS + Path.DirectorySeparatorChar + "CalamityModGlobalSaveData";
		GlobalSaveDataKeysPath = savePathByOS + Path.DirectorySeparatorChar + "CalamityModGlobalSaveData" + Path.DirectorySeparatorChar + "GlobalSaveDataKeys.txt";
		READMEPath = savePathByOS + Path.DirectorySeparatorChar + "CalamityModGlobalSaveData" + Path.DirectorySeparatorChar + "README.txt";
		if (!File.Exists(GlobalSaveDataKeysPath))
		{
			if (!Directory.Exists(GlobalSaveDataDirectoryPath))
			{
				Directory.CreateDirectory(GlobalSaveDataDirectoryPath);
			}
			if (!File.Exists(READMEPath))
			{
				using StreamWriter readme = File.CreateText(READMEPath);
				readme.WriteLine("Hi! You're probably wondering what this directory is and why it exists if you've found it.");
				readme.WriteLine("This is how the Calamity Mod currently manages data that is saved globally across all worlds. You will find all keys managing globally saved data in the other text file within this folder. You are free to remove any of those keys if you wish to reapply the lock on whatever in-game content they unlock. If you have no interest in doing so then it is recommended you simply leave the file as is.");
			}
			File.CreateText(GlobalSaveDataKeysPath);
		}
		else
		{
			GlobalSaveDataKeys = File.ReadAllLines(GlobalSaveDataKeysPath).ToList();
		}
	}

	public static bool IsKeyAlreadySaved(string key)
	{
		return GlobalSaveDataKeys.Contains(key);
	}

	public static void SaveKey(string key)
	{
		if (GlobalSaveDataKeys.Contains(key))
		{
			CalamityMod.Log.Error((object)("WARNING! A global save data key \"" + key + "\" which has already been registered is attempting to be registered again. Please report this to the Calamity Mod Team if you see this!"));
			return;
		}
		File.AppendAllLines(GlobalSaveDataKeysPath, new _003C_003Ez__ReadOnlySingleElementList<string>(key));
		GlobalSaveDataKeys.Add(key);
	}
}
