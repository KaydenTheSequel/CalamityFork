using System.IO;
using CalamityMod.Events;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class AcidRainSyncPacket : CalamityPacket
{
	public static AcidRainSyncPacket Instance { get; private set; }

	public static void Send(int toClient = -1, int ignoreClient = -1)
	{
		ModPacket modPacket = Instance.CreateBasePacket();
		modPacket.Write(AcidRainEvent.AcidRainEventIsOngoing);
		modPacket.Write(AcidRainEvent.AccumulatedKillPoints);
		modPacket.Write(AcidRainEvent.TimeSinceLastAcidRainKill);
		modPacket.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		AcidRainEvent.AcidRainEventIsOngoing = packet.ReadBoolean();
		AcidRainEvent.AccumulatedKillPoints = packet.ReadInt32();
		AcidRainEvent.TimeSinceLastAcidRainKill = packet.ReadInt32();
	}
}
