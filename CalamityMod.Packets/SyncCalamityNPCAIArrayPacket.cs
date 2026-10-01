using System.IO;
using CalamityMod.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class SyncCalamityNPCAIArrayPacket : CalamityPacket
{
	public static SyncCalamityNPCAIArrayPacket Instance { get; private set; }

	public static void Send(NPC npc, int toClient = -1, int ignoreClient = -1)
	{
		if (npc != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(npc);
			CalamityGlobalNPC calNPC = npc.Calamity();
			modPacket.Write(calNPC.newAI[0]);
			modPacket.Write(calNPC.newAI[1]);
			modPacket.Write(calNPC.newAI[2]);
			modPacket.Write(calNPC.newAI[3]);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		NPC npc = packet.ReadNPC();
		float ai0 = packet.ReadSingle();
		float ai1 = packet.ReadSingle();
		float ai2 = packet.ReadSingle();
		float ai3 = packet.ReadSingle();
		if (npc != null)
		{
			CalamityGlobalNPC calamityGlobalNPC = npc.Calamity();
			calamityGlobalNPC.newAI[0] = ai0;
			calamityGlobalNPC.newAI[1] = ai1;
			calamityGlobalNPC.newAI[2] = ai2;
			calamityGlobalNPC.newAI[3] = ai3;
		}
	}
}
