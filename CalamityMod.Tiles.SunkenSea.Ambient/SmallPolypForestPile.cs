using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class SmallPolypForestPile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileNoFail[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		AddMapEntry(new Color(162, 55, 196));
		base.DustType = 97;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
		TileObjectData.newTile.Height = 1;
		TileObjectData.newTile.CoordinateHeights = new int[1] { 16 };
		TileObjectData.newTile.Origin = new Point16(0, 0);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.addTile(base.Type);
		base.SetStaticDefaults();
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = 2;
	}
}
