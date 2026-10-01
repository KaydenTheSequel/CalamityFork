using System.IO;
using CalamityMod.Items;
using CalamityMod.TileEntities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class TEChargingStationStandardPacket : CalamityPacket
{
	public static TEChargingStationStandardPacket Instance { get; private set; }

	public static void Send(TEChargingStation chargingStn, short timer, short cellStack, float chargeOrNaN, int toClient = -1, int ignoreClient = -1)
	{
		if (chargingStn != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteTileEntityID(chargingStn);
			modPacket.Write(timer);
			modPacket.Write(cellStack);
			modPacket.Write(chargeOrNaN);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		TEChargingStation chargingStn = packet.ReadTileEntity<TEChargingStation>();
		short timer = packet.ReadInt16();
		short cellStack = packet.ReadInt16();
		float chargeOrNaN = packet.ReadSingle();
		if (chargingStn == null)
		{
			return;
		}
		if (Main.netMode == 1)
		{
			chargingStn.Internal_ChargingTimer = timer;
		}
		chargingStn.Internal_Stack = cellStack;
		if (!float.IsNaN(chargeOrNaN))
		{
			CalamityGlobalItem modItem = ((chargingStn.PluggedItem != null && !chargingStn.PluggedItem.IsAir) ? chargingStn.PluggedItem.Calamity() : null);
			if (modItem != null && modItem.UsesCharge)
			{
				if (modItem.Charge != chargeOrNaN && Main.netMode == 1)
				{
					chargingStn.ClientChargingDust = true;
				}
				modItem.Charge = chargeOrNaN;
			}
		}
		if (Main.dedServ)
		{
			Send(chargingStn, timer, cellStack, chargeOrNaN);
		}
	}
}
