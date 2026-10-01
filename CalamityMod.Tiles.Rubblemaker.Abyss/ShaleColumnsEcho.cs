using CalamityMod.Items.Placeables.Abyss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Rubblemaker.Abyss;

public class ShaleColumnsEcho : ModTile
{
	public override string Texture => "CalamityMod/Tiles/Abyss/ShaleColumns";

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
		RegisterItemDrop(ModContent.ItemType<SulphurousShale>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<SulphurousShale>(), base.Type, default(int));
		base.SetStaticDefaults();
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
