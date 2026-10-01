using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;
using CalamityMod.UI.DialogueDisplay;
using CalamityMod.Utilities;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace CalamityMod.Dialogues;

internal class DialogueLoader : ModSystem
{
	[CompilerGenerated]
	private static class _003C_003EO
	{
		public static Manipulator _003C0_003E__ExtractDialogueFilesPatch;
	}

	private const string DialogueFilePrefix = "Dialogue.";

	private static readonly Dictionary<string, DialogueTextDataEntry> _DialogueLookup = new Dictionary<string, DialogueTextDataEntry>();

	private static readonly Dictionary<Mod, MainThreadedFileSystemWatcher> _Watchers = new Dictionary<Mod, MainThreadedFileSystemWatcher>();

	public override void Load()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		_DialogueLookup.Clear();
		MethodInfo method = typeof(LocalizationLoader).GetMethod("ExtractLocalizationFiles", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		if (method != null)
		{
			object obj = _003C_003EO._003C0_003E__ExtractDialogueFilesPatch;
			if (obj == null)
			{
				Manipulator val = ExtractDialogueFilesPatch;
				_003C_003EO._003C0_003E__ExtractDialogueFilesPatch = val;
				obj = (object)val;
			}
			MonoModHooks.Modify(method, (Manipulator)obj);
		}
	}

	public override void PostSetupContent()
	{
		Mod[] mods = ModLoader.Mods;
		foreach (Mod mod in mods)
		{
			if (mod.File == null)
			{
				continue;
			}
			string path = mod.SourceFolder;
			if (Directory.Exists(path))
			{
				MainThreadedFileSystemWatcher watcher = new MainThreadedFileSystemWatcher
				{
					Path = path,
					Filter = "*.json",
					FileNameFilter = CalamityDialogueFileRegex(),
					NotifyFilter = (NotifyFilters.FileName | NotifyFilters.LastWrite),
					IncludeSubdirectories = true
				};
				watcher.Changed += delegate(FileSystemEventArgs arg)
				{
					HandleFileUpdate(mod, arg.FullPath);
				};
				watcher.Renamed += delegate(RenamedEventArgs arg)
				{
					HandleFileUpdate(mod, arg.FullPath);
				};
				watcher.EnableRaisingEvents = true;
				_Watchers[mod] = watcher;
			}
		}
	}

	public override void Unload()
	{
		_DialogueLookup.Clear();
		foreach (MainThreadedFileSystemWatcher value in _Watchers.Values)
		{
			value.EnableRaisingEvents = false;
			value.Dispose();
		}
		_Watchers.Clear();
	}

	private static void ExtractDialogueFilesPatch(ILContext il)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		int pathLdloc = -1;
		int modLdloc = -1;
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[4]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdloc(i, ref modLdloc),
			(Instruction i) => ILPatternMatchingExt.MatchLdloc(i, ref pathLdloc),
			(Instruction i) =>
			{
				MethodReference val = default(MethodReference);
				return ILPatternMatchingExt.MatchCallOrCallvirt(i, ref val);
			},
			(Instruction i) => ILPatternMatchingExt.MatchCallOrCallvirt(i, typeof(LocalizationLoader), "UpdateLocalizationFilesForMod")
		}))
		{
			CalamityMod.Log.ILFailure("Force Extract Dialogue Files", "Unable to locate UpdateLocalizationFilesForMod call");
		}
		if (modLdloc == -1)
		{
			CalamityMod.Log.ILFailure("Force Extract Dialogue Files", "Unable to locate ldloc index for mod");
		}
		if (pathLdloc == -1)
		{
			CalamityMod.Log.ILFailure("Force Extract Dialogue Files", "Unable to locate ldloc index for path");
		}
		cursor.EmitLdloc(modLdloc);
		cursor.EmitLdloc(pathLdloc);
		cursor.EmitDelegate<Action<Mod, string>>((Action<Mod, string>)delegate(Mod mod, string basePath)
		{
			foreach (DialogueTextDataEntry current in GetDialogueTextEntries(mod, GameCulture.DefaultCulture, skipDeserializeData: true))
			{
				try
				{
					string path = Path.Combine(basePath, current.FilePath);
					string directoryName = Path.GetDirectoryName(path);
					if (!Directory.Exists(directoryName))
					{
						Directory.CreateDirectory(directoryName);
					}
					using Stream stream = mod.File.GetStream(current.FilePath);
					using FileStream stream2 = File.OpenWrite(path);
					using StreamWriter streamWriter = new StreamWriter(stream2, Encoding.UTF8);
					using StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
					streamWriter.Write(streamReader.ReadToEnd());
				}
				catch (Exception value)
				{
					CalamityMod.Log.Error((object)$"Error while exporting DialogueTextData entry ({mod.Name}::{current.FilePath}): {value}");
				}
			}
		});
	}

	public static bool TryGetDialogue(string dialogueKey, out DialogueTextData data)
	{
		if (_DialogueLookup.TryGetValue(dialogueKey, out var entry))
		{
			data = entry.Data;
			return true;
		}
		data = null;
		return false;
	}

	public override void OnLocalizationsLoaded()
	{
		_DialogueLookup.Clear();
		foreach (DialogueTextDataEntry entry in GetDialogueTextEntiresForAllMods(GameCulture.DefaultCulture))
		{
			if (_DialogueLookup.TryGetValue(entry.DialogueKey, out var oldEntry) && entry.Data.Revision != oldEntry.Data.Revision)
			{
				CalamityMod.Log.Warn((object)$"Dialogue Localization was detected but revision mismatches. This will not be applied! : '{entry.ProviderMod.Name}::{entry.FilePath}'");
			}
			else
			{
				_DialogueLookup[entry.DialogueKey] = entry;
			}
		}
		GameCulture activeCulture = LanguageManager.Instance.ActiveCulture;
		if (activeCulture == GameCulture.DefaultCulture)
		{
			return;
		}
		foreach (DialogueTextDataEntry entry2 in GetDialogueTextEntiresForAllMods(activeCulture))
		{
			Mod mod = entry2.ProviderMod;
			if (!_DialogueLookup.TryGetValue(entry2.DialogueKey, out var oldEntry2))
			{
				CalamityMod.Log.Warn((object)$"Dialogue Localization was detected but original Dialogue file does not exist. This will not be applied! : '{mod.Name}::{entry2.FilePath}'");
			}
			else if (oldEntry2.ProviderMod != entry2.ProviderMod || !(oldEntry2.FilePath == entry2.FilePath))
			{
				if (oldEntry2.Data.Revision != entry2.Data.Revision)
				{
					CalamityMod.Log.Warn((object)$"Dialogue Localization was detected but revision mismatches. This will not be applied! : '{mod.Name}::{entry2.FilePath}'");
				}
				else
				{
					_DialogueLookup[entry2.DialogueKey] = entry2;
				}
			}
		}
	}

	private static void HandleFileUpdate(Mod mod, string filePath)
	{
		if (!TryGetDialogueFileInfo(filePath, out var _, out var _, out var dialogueKey) || !_DialogueLookup.TryGetValue(dialogueKey, out var existingEntry) || existingEntry.ProviderMod != mod)
		{
			return;
		}
		try
		{
			using StreamReader stream = new StreamReader(File.OpenRead(filePath), Encoding.UTF8);
			_DialogueLookup[dialogueKey] = existingEntry with
			{
				Data = JsonSerializer.Deserialize<DialogueTextData>(stream.BaseStream)
			};
			string hotreloadedMessage = $"Dialogue entry has been hot reloaded: '{dialogueKey}', from source: '{filePath}'";
			CalamityMod.Log.Info((object)hotreloadedMessage);
			if (!Main.gameMenu)
			{
				Main.NewText(hotreloadedMessage);
			}
		}
		catch (Exception value)
		{
			CalamityMod.Log.Error((object)$"Error while hot reloading DialogueTextData entry ({filePath}): {value}");
		}
	}

	private static IEnumerable<DialogueTextDataEntry> GetDialogueTextEntiresForAllMods(GameCulture targetCulture, bool skipDeserializeData = false)
	{
		return ModLoader.Mods.Where((Mod mod) => mod.File != null).SelectMany((Mod mod) => GetDialogueTextEntries(mod, targetCulture, skipDeserializeData));
	}

	private static IEnumerable<DialogueTextDataEntry> GetDialogueTextEntries(Mod mod, GameCulture targetCulture, bool skipDeserializeData = false)
	{
		if (mod == null || mod.File == null)
		{
			yield break;
		}
		foreach (TmodFile.FileEntry file in mod.File)
		{
			if (!TryGetDialogueFileInfo(file.Name, out var culture, out var _, out var dialogueKey) || culture != targetCulture)
			{
				continue;
			}
			DialogueTextData data = null;
			if (!skipDeserializeData)
			{
				try
				{
					using StreamReader stream = new StreamReader(mod.File.GetStream(file), Encoding.UTF8);
					data = JsonSerializer.Deserialize<DialogueTextData>(stream.BaseStream);
				}
				catch (Exception value)
				{
					CalamityMod.Log.Error((object)$"Error while reading DialogueTextData entry ({mod.Name}::{file.Name}): {value}");
				}
			}
			if ((data != null) | skipDeserializeData)
			{
				yield return new DialogueTextDataEntry(mod, file.Name, dialogueKey, data);
			}
		}
	}

	private static bool TryGetDialogueFileInfo(string filePath, out GameCulture culture, out string prefix, out string dialogueKey)
	{
		if (Path.GetExtension(filePath).Equals(".json", StringComparison.InvariantCultureIgnoreCase) && LocalizationLoader.TryGetCultureAndPrefixFromPath(filePath, out culture, out prefix))
		{
			string fileName = Path.GetFileNameWithoutExtension(filePath);
			if (fileName.StartsWith(prefix + "_Dialogue.", StringComparison.InvariantCultureIgnoreCase))
			{
				string text = fileName;
				int length = (prefix + "_Dialogue.").Length;
				dialogueKey = text.Substring(length, text.Length - length);
				return true;
			}
			if (fileName.StartsWith("Dialogue.", StringComparison.InvariantCultureIgnoreCase))
			{
				string text = fileName;
				int length = "Dialogue.".Length;
				dialogueKey = text.Substring(length, text.Length - length);
				return true;
			}
		}
		culture = null;
		prefix = null;
		dialogueKey = null;
		return false;
	}

	[GeneratedRegex("Dialogue\\..+?\\.jsonc?$", RegexOptions.IgnoreCase)]
	[GeneratedCode("System.Text.RegularExpressions.Generator", "8.0.14.7010")]
	private static Regex CalamityDialogueFileRegex()
	{
		return _003CRegexGenerator_g_003EF7EF839F8623C6609A374476407935905F891DC2675E6D6432167A284150137EE__CalamityDialogueFileRegex_0.Instance;
	}
}
