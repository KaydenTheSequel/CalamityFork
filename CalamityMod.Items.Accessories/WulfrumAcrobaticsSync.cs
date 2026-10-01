using System.IO;
using CalamityMod.Packets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

internal sealed class WulfrumAcrobaticsSync : CalamityPacket
{
	public static WulfrumAcrobaticsSync Instance { get; private set; }

	public static void Send(Player player, WulfrumPackPlayer mPlayer, Projectile proj, int toClient = -1, int ignoreClient = -1)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		Send((byte)player.whoAmI, mPlayer.SwingLength, mPlayer.OldPosition, proj.Center, (byte)proj.whoAmI);
	}

	private static void Send(byte playerWhoAmI, float swingLength, Vector2 playerOldPos, Vector2 projCenter, byte projWhoAmI, int toClient = -1, int ignoreClient = -1)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		ModPacket modPacket = Instance.CreateBasePacket();
		modPacket.Write(playerWhoAmI);
		modPacket.Write(swingLength);
		modPacket.WriteVector2(playerOldPos);
		modPacket.WriteVector2(projCenter);
		modPacket.Write(projWhoAmI);
		modPacket.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		byte playerWhoAmI = packet.ReadByte();
		float swingLength = packet.ReadSingle();
		Vector2 playerOldPos = packet.ReadVector2();
		Vector2 projCenter = packet.ReadVector2();
		byte projWhoAmI = packet.ReadByte();
		if (Main.player[playerWhoAmI].TryGetModPlayer<WulfrumPackPlayer>(out var mPlayer))
		{
			mPlayer.SwingLength = swingLength;
			mPlayer.OldPosition = playerOldPos;
			mPlayer.SetSegments(projCenter);
			mPlayer.Grapple = projWhoAmI;
		}
		if (Main.netMode == 2)
		{
			Send(playerWhoAmI, swingLength, playerOldPos, projCenter, projWhoAmI, -1, sender);
		}
	}
}
