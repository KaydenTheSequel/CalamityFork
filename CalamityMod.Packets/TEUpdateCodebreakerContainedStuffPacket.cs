using System.IO;
using CalamityMod.TileEntities;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class TEUpdateCodebreakerContainedStuffPacket : CalamityPacket
{
	public static TEUpdateCodebreakerContainedStuffPacket Instance { get; private set; }

	public static void Send(TECodebreaker codeBreaker, int toClient = -1, int ignoreClient = -1)
	{
		if (codeBreaker != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteTileEntityID(codeBreaker);
			modPacket.Write(codeBreaker.InputtedCellCount);
			modPacket.Write(codeBreaker.InitialCellCountBeforeDecrypting);
			modPacket.Write(codeBreaker.HeldSchematicID);
			modPacket.Write(codeBreaker.ContainsBloodyVein);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		TECodebreaker codeBreaker = packet.ReadTileEntity<TECodebreaker>();
		int cellCount = packet.ReadInt32();
		int cellCountBeforeDecrypting = packet.ReadInt32();
		int schematicID = packet.ReadInt32();
		bool containsBloodyVein = packet.ReadBoolean();
		if (codeBreaker != null)
		{
			codeBreaker.InputtedCellCount = cellCount;
			codeBreaker.InitialCellCountBeforeDecrypting = cellCountBeforeDecrypting;
			codeBreaker.HeldSchematicID = schematicID;
			codeBreaker.ContainsBloodyVein = containsBloodyVein;
		}
	}
}
