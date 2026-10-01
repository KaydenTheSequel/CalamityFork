using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture.Paintings;

public class ThankYouPaintingTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileSpelunker[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
		TileObjectData.newTile.Width = 6;
		TileObjectData.newTile.Height = 4;
		TileObjectData.newTile.Origin = new Point16(2, 2);
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 16 };
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.addTile(base.Type);
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		TileID.Sets.FramesOnKillWall[base.Type] = true;
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		base.DustType = 7;
		AddMapEntry(new Color(99, 50, 30), Language.GetText("MapObject.Painting"));
	}
}
