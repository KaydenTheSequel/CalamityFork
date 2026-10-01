using System.IO;
using CalamityMod.TileEntities;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class TEUpdateCodebreakerDecryptCountdownPacket : CalamityPacket
{
	public static TEUpdateCodebreakerDecryptCountdownPacket Instance { get; private set; }

	public static void Send(TECodebreaker codeBreaker, int toClient = -1, int ignoreClient = -1)
	{
		if (codeBreaker != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteTileEntityID(codeBreaker);
			modPacket.Write(codeBreaker.DecryptionCountdown);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		TECodebreaker codeBreaker = packet.ReadTileEntity<TECodebreaker>();
		int countdown = packet.ReadInt32();
		if (codeBreaker != null)
		{
			codeBreaker.DecryptionCountdown = countdown;
		}
	}
}
