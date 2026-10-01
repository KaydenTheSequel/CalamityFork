using System.IO;
using CalamityMod.TileEntities;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class TELabHologramProjectorPacket : CalamityPacket
{
	public static TELabHologramProjectorPacket Instance { get; private set; }

	public static void Send(TELabHologramProjector projector, bool poppingUp, int toClient = -1, int ignoreClient = -1)
	{
		if (projector != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteTileEntityID(projector);
			modPacket.Write(poppingUp);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		TELabHologramProjector projector = packet.ReadTileEntity<TELabHologramProjector>();
		bool pop = packet.ReadBoolean();
		if (projector != null)
		{
			projector.PoppingUp = pop;
		}
	}
}
