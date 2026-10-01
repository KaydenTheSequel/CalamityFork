using System.IO;
using Terraria;

namespace CalamityMod.Packets;

internal sealed class MusicEventSyncRequestPacket : CalamityPacket
{
	public static MusicEventSyncRequestPacket Instance { get; private set; }

	public static void Send(int toClient = -1, int ignoreClient = -1)
	{
		if (Main.netMode == 1)
		{
			Instance.CreateBasePacket().Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		if (Main.dedServ)
		{
			MusicEventSyncResponsePacket.Send(sender);
		}
	}
}
