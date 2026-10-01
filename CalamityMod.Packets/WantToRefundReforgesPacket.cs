using System.IO;
using CalamityMod.NPCs.TownNPCs;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class WantToRefundReforgesPacket : CalamityPacket
{
	public static WantToRefundReforgesPacket Instance { get; private set; }

	public static void Send(int toClient = -1, int ignoreClient = -1)
	{
		Instance.CreateBasePacket().Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		if (!Main.dedServ)
		{
			return;
		}
		int banditIdx = NPC.FindFirstNPC(ModContent.NPCType<Bandit>());
		if (banditIdx != -1)
		{
			NPC bandit = Main.npc[banditIdx];
			if (bandit != null && bandit.active)
			{
				Bandit.DoRefund(bandit);
			}
		}
	}
}
