using System.IO;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class MousePositionSyncPacket : CalamityPacket
{
	public static MousePositionSyncPacket Instance { get; private set; }

	public static void Send(CalamityPlayer player, int toClient = -1, int ignoreClient = -1)
	{
		if (player != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(player);
			modPacket.Write((short)player.mouseWorldDeltaFromPlayer.X);
			modPacket.Write((short)player.mouseWorldDeltaFromPlayer.Y);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer player = packet.ReadCalamityPlayer();
		short deltaX = packet.ReadInt16();
		short deltaY = packet.ReadInt16();
		if (player != null)
		{
			Vector2 delta = default(Vector2);
			((Vector2)(ref delta))._002Ector((float)deltaX, (float)deltaY);
			player.mouseWorldDeltaFromPlayer = delta;
			player.mouseRotationFromPlayer = delta.ToRotation();
			if (Main.dedServ)
			{
				Send(player, -1, sender);
			}
		}
	}
}
