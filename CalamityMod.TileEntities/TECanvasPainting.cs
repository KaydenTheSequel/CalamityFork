using System.IO;
using CalamityMod.Packets;
using CalamityMod.Tiles.Furniture.Paintings;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.TileEntities;

public class TECanvasPainting : ModTileEntity
{
	public Vector2 framePosition;

	public float scale;

	public override bool IsTileValidForEntity(int x, int y)
	{
		Tile tile = Main.tile[x, y];
		if (tile.HasTile && TileLoader.GetTile(tile.TileType) is BaseCanvasPainting && tile.TileFrameX == 0)
		{
			return tile.TileFrameY == 0;
		}
		return false;
	}

	public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
	{
		int iMinus = i - 1;
		int jMinus = j - 1;
		if (Main.netMode == 1)
		{
			NetMessage.SendTileSquare(Main.myPlayer, iMinus, jMinus, 5, 5);
			NetMessage.SendData(87, -1, -1, null, iMinus, jMinus, base.Type);
			return -1;
		}
		return Place(iMinus, jMinus);
	}

	public override void OnNetPlace()
	{
		NetMessage.SendData(86, -1, -1, null, ID, Position.X, Position.Y);
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(framePosition.X);
		writer.Write(framePosition.Y);
		writer.Write(scale);
	}

	public override void NetReceive(BinaryReader reader)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		framePosition = new Vector2(reader.ReadSingle(), reader.ReadSingle());
		scale = reader.ReadSingle();
	}

	public override void SaveData(TagCompound tag)
	{
		tag.Add("posX", framePosition.X);
		tag.Add("posY", framePosition.Y);
		tag.Add("scale", scale);
	}

	public override void LoadData(TagCompound tag)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		framePosition = new Vector2(tag.Get<float>("posX"), tag.Get<float>("posY"));
		scale = tag.Get<float>("scale");
	}

	public void SendSyncPacket()
	{
		if (Main.netMode != 0)
		{
			TECanvasPaintingPacket.Send(this, framePosition.X, framePosition.Y, scale);
		}
	}

	public TECanvasPainting()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		framePosition = new Vector2(0f, 0f);
		scale = 1f;
		base._002Ector();
	}
}
