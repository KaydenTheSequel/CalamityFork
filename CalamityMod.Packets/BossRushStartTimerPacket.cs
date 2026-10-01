using System.IO;
using CalamityMod.Events;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class BossRushStartTimerPacket : CalamityPacket
{
	public static BossRushStartTimerPacket Instance { get; private set; }

	public static void Send(int toClient = -1, int ignoreClient = -1)
	{
		ModPacket modPacket = Instance.CreateBasePacket();
		modPacket.Write(BossRushEvent.StartTimer);
		modPacket.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		BossRushEvent.StartTimer = packet.ReadInt32();
	}
}
