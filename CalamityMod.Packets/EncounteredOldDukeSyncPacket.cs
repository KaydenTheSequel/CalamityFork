using System.IO;
using CalamityMod.Events;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class EncounteredOldDukeSyncPacket : CalamityPacket
{
	public static EncounteredOldDukeSyncPacket Instance { get; private set; }

	public static void Send(int toClient = -1, int ignoreClient = -1)
	{
		ModPacket modPacket = Instance.CreateBasePacket();
		modPacket.Write(AcidRainEvent.OldDukeHasBeenEncountered);
		modPacket.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		AcidRainEvent.OldDukeHasBeenEncountered = packet.ReadBoolean();
	}
}
