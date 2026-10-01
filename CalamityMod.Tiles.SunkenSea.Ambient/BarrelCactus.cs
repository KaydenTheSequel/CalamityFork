using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class BarrelCactus : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		Main.tileNoAttach[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleWrapLimit = 2;
		TileObjectData.newTile.RandomStyleRange = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(85, 143, 46));
		base.DustType = 225;
		base.SetStaticDefaults();
	}
}
