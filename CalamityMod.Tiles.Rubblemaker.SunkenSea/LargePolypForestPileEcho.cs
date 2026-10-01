using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class LargePolypForestPileEcho : LargePolypForestPile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/LargePolypForestPile";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<PolypSand>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<PolypSand>(), base.Type, 0, 1, 2);
	}
}
