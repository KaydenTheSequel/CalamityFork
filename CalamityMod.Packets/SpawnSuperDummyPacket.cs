using System.IO;
using CalamityMod.NPCs.NormalNPCs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class SpawnSuperDummyPacket : CalamityPacket
{
	public static SpawnSuperDummyPacket Instance { get; private set; }

	public static void Send(int x, int y, int toClient = -1, int ignoreClient = -1)
	{
		ModPacket modPacket = Instance.CreateBasePacket();
		modPacket.Write(x);
		modPacket.Write(y);
		modPacket.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		int x = packet.ReadInt32();
		int y = packet.ReadInt32();
		if (Main.dedServ)
		{
			NPC.NewNPC(new EntitySource_WorldEvent(), x, y, ModContent.NPCType<SuperDummyNPC>());
		}
	}
}
