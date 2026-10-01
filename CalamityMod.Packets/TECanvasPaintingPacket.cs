using System.IO;
using CalamityMod.TileEntities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class TECanvasPaintingPacket : CalamityPacket
{
	public static TECanvasPaintingPacket Instance { get; private set; }

	public static void Send(TECanvasPainting painting, float posX, float posY, float scale, int toClient = -1, int ignoreClient = -1)
	{
		if (painting != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteTileEntityID(painting);
			modPacket.Write(posX);
			modPacket.Write(posY);
			modPacket.Write(scale);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		TECanvasPainting painting = packet.ReadTileEntity<TECanvasPainting>();
		float posX = packet.ReadSingle();
		float posY = packet.ReadSingle();
		float scale = packet.ReadSingle();
		if (painting != null)
		{
			painting.framePosition = new Vector2(posX, posY);
			painting.scale = scale;
			if (Main.dedServ)
			{
				Send(painting, posX, posY, scale);
			}
		}
	}
}
