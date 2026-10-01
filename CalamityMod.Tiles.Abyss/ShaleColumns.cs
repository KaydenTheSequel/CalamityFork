using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Abyss;

public class ShaleColumns : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileSolidTop[base.Type] = true;
		TileObjectData.newTile.Width = 2;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.Origin = new Point16(1, 2);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.WaterDeath = false;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(37, 24, 63));
		base.DustType = 33;
		base.SetStaticDefaults();
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		Tile t = CalamityUtils.ParanoidTileRetrieval(i, j);
		Tile left = CalamityUtils.ParanoidTileRetrieval(i - 1, j);
		Tile right = CalamityUtils.ParanoidTileRetrieval(i + 1, j);
		if (t.TileFrameX % 36 == 0 && !right.HasTile)
		{
			WorldGen.KillTile(i, j);
		}
		if (t.TileFrameX % 36 == 18 && !left.HasTile)
		{
			WorldGen.KillTile(i, j);
		}
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		_ = Main.dedServ;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 2);
	}
}
