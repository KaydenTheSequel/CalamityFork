using System.IO;
using CalamityMod.NPCs.Abyss;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class SyncSlabCrabAIPacket : CalamityPacket
{
	public static SyncSlabCrabAIPacket Instance { get; private set; }

	public static void Send(SlabCrab crab, int phase = -1, int toClient = -1, int ignoreClient = -1)
	{
		if (crab != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(crab);
			modPacket.Write((phase != -1) ? phase : ((int)crab.NPC.ai[0]));
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		SlabCrab crab = packet.ReadModNPC<SlabCrab>();
		int phase = packet.ReadInt32();
		if (crab != null && Main.dedServ)
		{
			crab.ChangePhase(phase);
		}
	}
}
