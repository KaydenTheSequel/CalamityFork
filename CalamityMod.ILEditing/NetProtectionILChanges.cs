using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.ILEditing;

public sealed class NetProtectionILChanges : ModSystem
{
	private static string[] _ModNames;

	public override void OnModLoad()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		_ModNames = ModLoader.Mods.Select((Mod mod) => mod.Name).ToArray();
		On_NPC.NewNPC += new hook_NewNPC(NewNPCRule);
	}

	public override void OnModUnload()
	{
		_ModNames = null;
	}

	private int NewNPCRule(orig_NewNPC orig, IEntitySource source, int X, int Y, int Type, int Start, float ai0, float ai1, float ai2, float ai3, int Target)
	{
		if (Main.netMode == 1)
		{
			CalamityMod.Log.Error((object)"NETCODE SAFETY VIOLATION DETECTED: NewNPC was called from a Multiplayer Client");
			CalamityMod.Log.Error((object)GetSimplifiedStackTrace());
			return Main.maxNPCs;
		}
		return orig.Invoke(source, X, Y, Type, Start, ai0, ai1, ai2, ai3, Target);
	}

	public override bool HijackSendData(int whoAmI, int msgType, int remoteClient, int ignoreClient, NetworkText text, int number, float number2, float number3, float number4, int number5, int number6, int number7)
	{
		if (msgType == 23 && Main.netMode == 1)
		{
			CalamityMod.Log.Error((object)"NETCODE SAFETY VIOLATION DETECTED: SendData (SyncNPC) was called from a Multiplayer Client");
			CalamityMod.Log.Error((object)GetSimplifiedStackTrace());
			return true;
		}
		return base.HijackSendData(whoAmI, msgType, remoteClient, ignoreClient, text, number, number2, number3, number4, number5, number6, number7);
	}

	private static bool ContainsAnyModName(string str)
	{
		return _ModNames?.Any(str.Contains) ?? false;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private static string GetSimplifiedStackTrace()
	{
		StringBuilder stringBuilder = new StringBuilder();
		try
		{
			stringBuilder.AppendLine("STACKTRACE:");
			StackFrame[] frames = new StackTrace(1).GetFrames();
			bool didPrintSomethingUselessLastTime = false;
			bool didEverPrintSomethingUseful = false;
			StackFrame[] array = frames;
			for (int i = 0; i < array.Length; i++)
			{
				MethodBase? method = array[i].GetMethod();
				string methodName = method?.ToString() ?? "UNKNOWN_METHOD";
				string typeName = method?.DeclaringType?.FullName ?? "UNKNOWN_TYPE";
				if (ContainsAnyModName(typeName) && !typeName.Contains("NetProtectionILChanges"))
				{
					didPrintSomethingUselessLastTime = false;
					didEverPrintSomethingUseful = true;
					stringBuilder.AppendFormat(" at {0}::{1}", typeName, methodName);
					stringBuilder.AppendLine();
				}
				else if (!didPrintSomethingUselessLastTime & didEverPrintSomethingUseful)
				{
					stringBuilder.AppendLine(" ...");
					didPrintSomethingUselessLastTime = true;
				}
			}
			stringBuilder.AppendLine("END OF STACKTRACE");
			return stringBuilder.ToString();
		}
		catch
		{
			return "";
		}
		finally
		{
			stringBuilder?.Clear();
		}
	}
}
