using System.IO;
using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class AdrenalineSyncPacket : CalamityPacket
{
	public static AdrenalineSyncPacket Instance { get; private set; }

	public static void Send(CalamityPlayer playerToSync, int toClient = -1, int ignoreClient = -1)
	{
		if (playerToSync != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(playerToSync);
			modPacket.Write(playerToSync.adrenaline);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		CalamityPlayer player = packet.ReadCalamityPlayer();
		float adrenaline = packet.ReadSingle();
		if (player != null)
		{
			player.adrenaline = adrenaline;
			if (Main.dedServ)
			{
				Send(player, -1, sender);
			}
		}
	}
}
