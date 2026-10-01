using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class SpawnNPCOnPlayerPacket : CalamityPacket
{
	public static SpawnNPCOnPlayerPacket Instance { get; private set; }

	public static void Send(Player player, int x, int y, int npcType, int toClient = -1, int ignoreClient = -1)
	{
		if (player != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(player);
			modPacket.Write(x);
			modPacket.Write(y);
			modPacket.Write(npcType);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		Player player = packet.ReadPlayer();
		int x = packet.ReadInt32();
		int y = packet.ReadInt32();
		int npcType = packet.ReadInt32();
		if (player != null && Main.dedServ)
		{
			int spawnedNPC = NPC.NewNPC(new EntitySource_WorldEvent(), x, y, npcType, 0, 0f, 0f, 0f, 0f, player.whoAmI);
			if (spawnedNPC < Main.maxNPCs)
			{
				CalamityNetcode.SyncNPC(spawnedNPC);
			}
		}
	}
}
