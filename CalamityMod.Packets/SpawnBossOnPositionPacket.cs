using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class SpawnBossOnPositionPacket : CalamityPacket
{
	public static SpawnBossOnPositionPacket Instance { get; private set; }

	public static void Send(int x, int y, int npcType, Player target = null, int toClient = -1, int ignoreClient = -1)
	{
		ModPacket modPacket = Instance.CreateBasePacket();
		modPacket.Write(x);
		modPacket.Write(y);
		modPacket.Write(npcType);
		modPacket.Write((byte)(target?.whoAmI ?? 255));
		modPacket.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		int x = packet.ReadInt32();
		int y = packet.ReadInt32();
		int npcType = packet.ReadInt32();
		int targetIndex = packet.ReadPlayer()?.whoAmI ?? 255;
		if (!Main.dedServ)
		{
			return;
		}
		if (npcType == 113)
		{
			NPC.SpawnWOF(new Vector2((float)x, (float)y));
			return;
		}
		int spawnedNPCIdx = NPC.NewNPC(NPC.GetBossSpawnSource(targetIndex), x, y, npcType, 1);
		if (spawnedNPCIdx < Main.maxNPCs)
		{
			NPC obj = Main.npc[spawnedNPCIdx];
			obj.timeLeft *= 20;
			obj.target = targetIndex;
			CalamityUtils.BossAwakenMessage(spawnedNPCIdx);
			CalamityNetcode.SyncNPC(obj);
		}
	}
}
