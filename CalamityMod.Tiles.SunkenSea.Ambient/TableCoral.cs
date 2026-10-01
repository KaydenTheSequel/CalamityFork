using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class TableCoral : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileSolidTop[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleWrapLimit = 3;
		TileObjectData.newTile.RandomStyleRange = 3;
		TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
		TileObjectData.newTile.AnchorRight = new AnchorData(AnchorType.SolidTile, 2, 0);
		TileObjectData.newTile.DrawXOffset = 2;
		TileObjectData.addTile(base.Type);
		base.DustType = 253;
		AddMapEntry(new Color(23, 56, 75));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
