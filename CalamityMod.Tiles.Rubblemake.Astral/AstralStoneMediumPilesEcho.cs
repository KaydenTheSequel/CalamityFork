using CalamityMod.Dusts;
using CalamityMod.Items.Placeables.Astral;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Rubblemake.Astral;

public class AstralStoneMediumPilesEcho : ModTile
{
	public override string Texture => "CalamityMod/Tiles/Astral/AstralStoneMediumPiles";

	public override void SetStaticDefaults()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileObsidianKill[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
		TileObjectData.addTile(base.Type);
		base.DustType = ModContent.DustType<AstralBasic>();
		AddMapEntry(new Color(79, 61, 97));
		RegisterItemDrop(ModContent.ItemType<AstralDirt>());
		FlexibleTileWand.RubblePlacementMedium.AddVariations(ModContent.ItemType<AstralStone>(), base.Type, 0, 1, 2, 3, 4, 5);
		base.SetStaticDefaults();
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = 2;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 4);
	}
}
