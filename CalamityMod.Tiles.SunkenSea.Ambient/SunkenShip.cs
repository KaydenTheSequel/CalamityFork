using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class SunkenShip : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileBlockLight[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		TileObjectData.newTile.Width = 14;
		TileObjectData.newTile.Height = 8;
		TileObjectData.newTile.Origin = new Point16(7, 7);
		TileObjectData.newTile.CoordinateHeights = new int[8] { 16, 16, 16, 16, 16, 16, 16, 16 };
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, 14, 0);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(100, 80, 70), Language.GetText("Sunken Ship"));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
