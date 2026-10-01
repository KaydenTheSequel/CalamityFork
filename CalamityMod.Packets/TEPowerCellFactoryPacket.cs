using System.IO;
using CalamityMod.TileEntities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class TEPowerCellFactoryPacket : CalamityPacket
{
	public static TEPowerCellFactoryPacket Instance { get; private set; }

	public static void Send(TEPowerCellFactory cellFactory, long time, int stack, int toClient = -1, int ignoreClient = -1)
	{
		if (cellFactory != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteTileEntityID(cellFactory);
			modPacket.Write(time);
			modPacket.Write(stack);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		TEPowerCellFactory cellFactory = packet.ReadTileEntity<TEPowerCellFactory>();
		long time = packet.ReadInt64();
		short cellStack = packet.ReadInt16();
		if (cellFactory != null)
		{
			if (Main.netMode == 1)
			{
				cellFactory.Time = time;
			}
			cellFactory.Stack_Internal = cellStack;
			if (Main.dedServ)
			{
				Send(cellFactory, time, cellStack);
			}
		}
	}
}
