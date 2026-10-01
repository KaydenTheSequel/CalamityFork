using System.IO;
using CalamityMod.TileEntities;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Packets;

internal sealed class TEChargingStationItemChangePacket : CalamityPacket
{
	public static TEChargingStationItemChangePacket Instance { get; private set; }

	public static void Send(TEChargingStation chargingStn, Item pluggedItem, int toClient = -1, int ignoreClient = -1)
	{
		if (chargingStn != null)
		{
			ModPacket packet = Instance.CreateBasePacket();
			packet.WriteTileEntityID(chargingStn);
			ItemIO.Send(pluggedItem, packet, writeStack: true, writeFavorite: true);
			packet.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		TEChargingStation chargingStn = packet.ReadTileEntity<TEChargingStation>();
		Item thePlug = ItemIO.Receive(packet, readStack: true, readFavorite: true);
		if (chargingStn != null)
		{
			chargingStn.PluggedItem = thePlug;
			if (Main.dedServ)
			{
				Send(chargingStn, thePlug);
			}
		}
	}
}
