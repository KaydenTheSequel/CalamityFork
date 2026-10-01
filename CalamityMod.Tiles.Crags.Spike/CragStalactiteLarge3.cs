using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Crags.Spike;

public class CragStalactiteLarge3 : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		TileObjectData.newTile.Width = 2;
		TileObjectData.newTile.Height = 4;
		TileObjectData.newTile.Origin = new Point16(1, 1);
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 16 };
		TileObjectData.newTile.StyleWrapLimit = 36;
		TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.AnchorBottom = default(AnchorData);
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.DrawYOffset = -3;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(43, 23, 46));
		base.DustType = 155;
		base.HitSound = SoundID.Tink;
	}
}
