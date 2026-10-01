using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class PlaceAltCritterPacket : CalamityPacket
{
	public static PlaceAltCritterPacket Instance { get; private set; }

	public static void Send(Player placer, int x, int y, Item critterItem, int colorType, int toClient = -1, int ignoreClient = -1)
	{
		if (critterItem != null)
		{
			Send(placer, x, y, critterItem.makeNPC, critterItem.type, colorType, toClient, ignoreClient);
		}
	}

	public static void Send(Player placer, int x, int y, int critterNPCType, int itemType, int colorType, int toClient = -1, int ignoreClient = -1)
	{
		if (placer != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(placer);
			modPacket.Write(x);
			modPacket.Write(y);
			modPacket.Write(critterNPCType);
			modPacket.Write(itemType);
			modPacket.Write(colorType);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		Player placerplayer = packet.ReadPlayer();
		int posX = packet.ReadInt32();
		int posY = packet.ReadInt32();
		int type = packet.ReadInt32();
		int itemType = packet.ReadInt32();
		float color = packet.ReadInt32();
		if (Main.dedServ && placerplayer != null)
		{
			int newNPCIndex = NPC.NewNPC(placerplayer.GetSource_ReleaseEntity(), posX, posY, type, 0, 0f, color);
			if (newNPCIndex < Main.maxNPCs)
			{
				NPC obj = Main.npc[newNPCIndex];
				obj.catchItem = itemType;
				obj.releaseOwner = (short)placerplayer.whoAmI;
				CalamityNetcode.SyncNPC(obj);
			}
		}
	}
}
