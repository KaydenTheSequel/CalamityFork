using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class SyncVanillaNPCLocalAIArrayPacket : CalamityPacket
{
	public static SyncVanillaNPCLocalAIArrayPacket Instance { get; private set; }

	public static void Send(NPC npc, int toClient = -1, int ignoreClient = -1)
	{
		if (npc != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(npc);
			modPacket.Write(npc.localAI[0]);
			modPacket.Write(npc.localAI[1]);
			modPacket.Write(npc.localAI[2]);
			modPacket.Write(npc.localAI[3]);
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
			npc.localAI[0] = ai0;
			npc.localAI[1] = ai1;
			npc.localAI[2] = ai2;
			npc.localAI[3] = ai3;
		}
	}
}
