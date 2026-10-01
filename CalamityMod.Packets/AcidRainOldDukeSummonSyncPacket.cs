using System.IO;
using CalamityMod.Events;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class AcidRainOldDukeSummonSyncPacket : CalamityPacket
{
	public static AcidRainOldDukeSummonSyncPacket Instance { get; private set; }

	public static void Send(int toClient = -1, int ignoreClient = -1)
	{
		ModPacket modPacket = Instance.CreateBasePacket();
		modPacket.Write(AcidRainEvent.HasTriedToSummonOldDuke);
		modPacket.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		AcidRainEvent.HasTriedToSummonOldDuke = packet.ReadBoolean();
	}
}
