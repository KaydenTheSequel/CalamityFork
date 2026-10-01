using System.IO;
using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class RageSyncPacket : CalamityPacket
{
	public static RageSyncPacket Instance { get; private set; }

	public static void Send(CalamityPlayer playerToSync, int toClient = -1, int ignoreClient = -1)
	{
		if (playerToSync != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(playerToSync);
			modPacket.Write(playerToSync.rage);
			modPacket.Write(playerToSync.rageCombatFrames);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		CalamityPlayer player = packet.ReadCalamityPlayer();
		float rage = packet.ReadSingle();
		int rageCombatFrames = packet.ReadInt32();
		if (player != null)
		{
			player.rage = rage;
			player.rageCombatFrames = rageCombatFrames;
			if (Main.dedServ)
			{
				Send(player, -1, sender);
			}
		}
	}
}
