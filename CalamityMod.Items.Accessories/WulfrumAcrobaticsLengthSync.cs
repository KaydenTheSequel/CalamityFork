using System.IO;
using CalamityMod.Packets;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

internal sealed class WulfrumAcrobaticsLengthSync : CalamityPacket
{
	public readonly record struct Data(byte PlayerWhoAmI, float SwingLength, bool SetControlUpFalse);

	public static WulfrumAcrobaticsLengthSync Instance { get; private set; }

	public static void Send(Data data, int toClient = -1, int ignoreClient = -1)
	{
		ModPacket modPacket = Instance.CreateBasePacket();
		modPacket.Write(data.PlayerWhoAmI);
		modPacket.Write(data.SwingLength);
		modPacket.Write(data.SetControlUpFalse);
		modPacket.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		byte playerWhoAmI = packet.ReadByte();
		float swingLength = packet.ReadSingle();
		bool setControlUpFalse = packet.ReadBoolean();
		Player player = Main.player[playerWhoAmI];
		if (player.TryGetModPlayer<WulfrumPackPlayer>(out var mPlayer))
		{
			mPlayer.SwingLength = swingLength;
		}
		if (setControlUpFalse)
		{
			player.controlUp = false;
		}
		if (Main.netMode == 2)
		{
			Send(new Data(playerWhoAmI, swingLength, setControlUpFalse), -1, sender);
		}
	}
}
