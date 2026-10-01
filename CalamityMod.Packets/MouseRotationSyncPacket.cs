using System;
using System.IO;
using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class MouseRotationSyncPacket : CalamityPacket
{
	public static MouseRotationSyncPacket Instance { get; private set; }

	public static void Send(CalamityPlayer player, int toClient = -1, int ignoreClient = -1)
	{
		if (player != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(player);
			modPacket.Write((Half)player.mouseRotationFromPlayer);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer player = packet.ReadCalamityPlayer();
		float rotation = (float)packet.ReadHalf();
		if (player != null)
		{
			player.mouseRotationFromPlayer = rotation;
			player.mouseWorldDeltaFromPlayer = rotation.ToRotationVector2();
			if (Main.dedServ)
			{
				Send(player, -1, sender);
			}
		}
	}
}
