using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Crags.Spike;

public class CragStalagmiteGiant1 : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		TileObjectData.newTile.Width = 4;
		TileObjectData.newTile.Height = 8;
		TileObjectData.newTile.Origin = new Point16(2, 7);
		TileObjectData.newTile.CoordinateHeights = new int[8] { 16, 16, 16, 16, 16, 16, 16, 16 };
		TileObjectData.newTile.StyleWrapLimit = 36;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.DrawYOffset = 3;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(43, 23, 46));
		base.DustType = 155;
		base.HitSound = SoundID.Tink;
	}
}
