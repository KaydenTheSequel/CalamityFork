using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets.Entities;

internal sealed class GlaiveShredPacket : CalamityPacket
{
	public static GlaiveShredPacket Instance { get; private set; }

	public static void Send(NPC npc, int toClient = -1, int ignoreClient = -1)
	{
		if (npc != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(npc);
			modPacket.Write(npc.Calamity().glaiveShredTimer);
			modPacket.Write(npc.Calamity().blazingStarShredTimer);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		NPC npc = packet.ReadNPC();
		int glaive = packet.ReadInt32();
		int blazingstar = packet.ReadInt32();
		if (npc != null)
		{
			npc.Calamity().glaiveShredTimer = glaive;
			npc.Calamity().blazingStarShredTimer = blazingstar;
		}
	}
}
