using System.IO;
using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class RightClickSyncPacket : CalamityPacket
{
	public static RightClickSyncPacket Instance { get; private set; }

	public static void Send(CalamityPlayer player, int toClient = -1, int ignoreClient = -1)
	{
		if (player != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(player);
			modPacket.Write(player.mouseRight);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		CalamityPlayer player = packet.ReadCalamityPlayer();
		bool rightClick = packet.ReadBoolean();
		if (player != null)
		{
			player.mouseRight = rightClick;
			if (Main.dedServ)
			{
				Send(player, -1, sender);
			}
		}
	}
}
