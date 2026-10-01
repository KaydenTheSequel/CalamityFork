using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.NPCs;

public sealed class CalamityNetImportantNPC : GlobalNPC
{
	private static Dictionary<int, int> typesToUpdate;

	public override void Load()
	{
		typesToUpdate = new Dictionary<int, int>();
	}

	public override void Unload()
	{
		typesToUpdate?.Clear();
		typesToUpdate = null;
	}

	public override void SetStaticDefaults()
	{
		int uniqueNetOffsetID = 0;
		MarkNPCToLongDistanceSync(13, uniqueNetOffsetID);
		MarkNPCToLongDistanceSync(14, uniqueNetOffsetID);
		MarkNPCToLongDistanceSync(15, uniqueNetOffsetID);
		uniqueNetOffsetID++;
		MarkNPCToLongDistanceSync(134, uniqueNetOffsetID);
		MarkNPCToLongDistanceSync(135, uniqueNetOffsetID);
		MarkNPCToLongDistanceSync(136, uniqueNetOffsetID);
		uniqueNetOffsetID++;
		IEnumerable<ModNPC> content = ModContent.GetContent<ModNPC>();
		Dictionary<Type, int> netOffsetTable = new Dictionary<Type, int>();
		foreach (ModNPC npc in content)
		{
			try
			{
				Type type = npc.GetType();
				LongDistanceNetSyncAttribute longDistSync = type.GetCustomAttribute<LongDistanceNetSyncAttribute>();
				if (longDistSync != null)
				{
					int type2 = npc.Type;
					int netOffset = uniqueNetOffsetID;
					Type typeToCheck = longDistSync.SyncWith ?? type;
					if (netOffsetTable.TryGetValue(typeToCheck, out var savedUniqueID))
					{
						netOffset = savedUniqueID;
					}
					else
					{
						netOffsetTable[typeToCheck] = netOffset;
						uniqueNetOffsetID++;
					}
					MarkNPCToLongDistanceSync(type2, netOffset);
				}
			}
			catch (Exception value)
			{
				CalamityMod.Log.Error((object)$"Exception thrown while evaluating type \"{npc.FullName}\": {value}");
			}
		}
		netOffsetTable?.Clear();
	}

	public override void PostAI(NPC npc)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ || !npc.active || !typesToUpdate.TryGetValue(npc.type, out var netUpdateTickOffset) || (Main.GameUpdateCount + netUpdateTickOffset) % 45 != 0L)
		{
			return;
		}
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (!(enumerator.Current.position.ManhattanDistance(npc.position) <= 1499f))
			{
				npc.SyncNPCPosAndRotOnly();
			}
		}
	}

	private static void MarkNPCToLongDistanceSync<NPCType>(int netUpdateTickOffset = 0) where NPCType : ModNPC
	{
		MarkNPCToLongDistanceSync(ModContent.NPCType<NPCType>(), netUpdateTickOffset);
	}

	private static void MarkNPCToLongDistanceSync(int npcType, int netUpdateTickOffset = 0)
	{
		typesToUpdate[npcType] = netUpdateTickOffset;
	}
}
