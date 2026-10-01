using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class WallCoral : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileSolidTop[base.Type] = false;
		Main.tileLighted[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.Origin = new Point16(1, 1);
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
		TileObjectData.newTile.AnchorRight = new AnchorData(AnchorType.SolidTile, 3, 0);
		TileObjectData.newTile.DrawXOffset = 2;
		TileObjectData.addTile(base.Type);
		base.DustType = 253;
		AddMapEntry(new Color(54, 69, 72));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
