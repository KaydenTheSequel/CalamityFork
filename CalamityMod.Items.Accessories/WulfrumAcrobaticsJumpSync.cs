using System.IO;
using CalamityMod.Packets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

internal sealed class WulfrumAcrobaticsJumpSync : CalamityPacket
{
	public readonly record struct Data(byte PlayerWhoAmI, float SwingLength, bool CanJumpOffHook, Vector2 PlayerVelocity, int PlayerJump, bool SetControlUpFalse);

	public static WulfrumAcrobaticsJumpSync Instance { get; private set; }

	public static void Send(Data data, int toClient = -1, int ignoreClient = -1)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		ModPacket modPacket = Instance.CreateBasePacket();
		modPacket.Write(data.PlayerWhoAmI);
		modPacket.Write(data.SwingLength);
		modPacket.Write(data.CanJumpOffHook);
		modPacket.WriteVector2(data.PlayerVelocity);
		modPacket.Write(data.PlayerJump);
		modPacket.Write(data.SetControlUpFalse);
		modPacket.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		byte playerWhoAmI = packet.ReadByte();
		float swingLength = packet.ReadSingle();
		bool canJumpOffHook = packet.ReadBoolean();
		Vector2 playerVelocity = packet.ReadVector2();
		int playerJump = packet.ReadInt32();
		bool setControlUpFalse = packet.ReadBoolean();
		Player player = Main.player[playerWhoAmI];
		if (player.TryGetModPlayer<WulfrumPackPlayer>(out var mPlayer))
		{
			mPlayer.SwingLength = swingLength;
		}
		if (canJumpOffHook)
		{
			player.jump = playerJump;
			player.velocity = playerVelocity;
		}
		else
		{
			player.releaseJump = false;
		}
		if (setControlUpFalse)
		{
			player.controlUp = false;
		}
		if (Main.netMode == 2)
		{
			Send(new Data(playerWhoAmI, swingLength, canJumpOffHook, playerVelocity, playerJump, setControlUpFalse), -1, sender);
		}
	}
}
