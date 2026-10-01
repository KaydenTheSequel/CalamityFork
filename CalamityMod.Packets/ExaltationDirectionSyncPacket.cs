using System.IO;
using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class ExaltationDirectionSyncPacket : CalamityPacket
{
	public static ExaltationDirectionSyncPacket Instance { get; private set; }

	public static void Send(CalamityPlayer playerToSync, int toClient = -1, int ignoreClient = -1)
	{
		if (playerToSync != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(playerToSync);
			modPacket.Write(playerToSync.InvertExaltationLineRotationDirections);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		CalamityPlayer player = packet.ReadCalamityPlayer();
		bool invertDir = packet.ReadBoolean();
		if (player != null)
		{
			player.InvertExaltationLineRotationDirections = invertDir;
			if (Main.dedServ)
			{
				Send(player, -1, sender);
			}
		}
	}
}
