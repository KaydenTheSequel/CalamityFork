using System.IO;
using CalamityMod.Events;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class BossRushStagePacket : CalamityPacket
{
	public static BossRushStagePacket Instance { get; private set; }

	public static void Send(int toClient = -1, int ignoreClient = -1)
	{
		ModPacket modPacket = Instance.CreateBasePacket();
		modPacket.Write(BossRushEvent.BossRushStage);
		modPacket.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		BossRushEvent.BossRushStage = packet.ReadInt32();
	}
}
