using System.IO;
using CalamityMod.Systems;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class MusicEventSyncResponsePacket : CalamityPacket
{
	public static MusicEventSyncResponsePacket Instance { get; private set; }

	public static void Send(int toClient = -1, int ignoreClient = -1)
	{
		if (!Main.dedServ)
		{
			return;
		}
		ModPacket packet = Instance.CreateBasePacket();
		int trackCount = MusicEventSystem.PlayedEvents.Count;
		packet.Write(trackCount);
		foreach (string playedEvent in MusicEventSystem.PlayedEvents)
		{
			packet.Write(playedEvent);
		}
		packet.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		if (Main.netMode != 1)
		{
			int c = packet.ReadInt32();
			for (int i = 0; i < c; i++)
			{
				packet.ReadString();
			}
			return;
		}
		MusicEventSystem.PlayedEvents.Clear();
		int trackCount = packet.ReadInt32();
		for (int j = 0; j < trackCount; j++)
		{
			MusicEventSystem.PlayedEvents.Add(packet.ReadString());
		}
	}
}
