using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Rubblemaker.SulphurousSea;

public class SulphurousRib1Echo : ModTile
{
	public override string Texture => "CalamityMod/Tiles/Abyss/SulphurousRib1";

	public override void SetStaticDefaults()
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		TileObjectData.newTile.Width = 1;
		TileObjectData.newTile.Height = 4;
		TileObjectData.newTile.Origin = new Point16(0, 3);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.WaterDeath = false;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(57, 48, 83), CalamityUtils.GetText("Tiles.Ribs"));
		base.DustType = 75;
		RegisterItemDrop(ModContent.ItemType<CorrodedFossil>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<CorrodedFossil>(), base.Type, default(int));
		base.SetStaticDefaults();
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 2);
	}
}
