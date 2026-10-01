using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets.Entities;

internal sealed class ManaBurnSyncPacket : CalamityPacket
{
	public static ManaBurnSyncPacket Instance { get; private set; }

	public static void Send(NPC npc, int toClient = -1, int ignoreClient = -1)
	{
		if (npc != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(npc);
			modPacket.Write(npc.Calamity().manaBurn);
			modPacket.Write(npc.Calamity().manaBurnPeak);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		NPC npc = packet.ReadNPC();
		float burn = packet.ReadSingle();
		float burnPeak = packet.ReadSingle();
		if (npc != null)
		{
			npc.Calamity().manaBurn = burn;
			npc.Calamity().manaBurnPeak = burnPeak;
		}
	}
}
