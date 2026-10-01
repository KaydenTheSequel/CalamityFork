using System.IO;
using CalamityMod.World;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class BanditStolenMoneySyncPacket : CalamityPacket
{
	public static BanditStolenMoneySyncPacket Instance { get; private set; }

	public static void Send(int amountStolenByBandit, int toClient = -1, int ignoreClient = -1)
	{
		ModPacket modPacket = Instance.CreateBasePacket();
		modPacket.Write7BitEncodedInt(amountStolenByBandit);
		modPacket.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		int amountStolenByBandit = packet.Read7BitEncodedInt();
		CalamityWorld.MoneyStolenByBandit += amountStolenByBandit;
		CalamityWorld.Reforges++;
		if (Main.dedServ)
		{
			Send(amountStolenByBandit, -1, sender);
		}
	}
}
