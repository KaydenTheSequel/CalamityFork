using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class SmallPolypForestPileEcho : SmallPolypForestPile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/SmallPolypForestPile";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<PolypSand>());
		FlexibleTileWand.RubblePlacementSmall.AddVariations(ModContent.ItemType<PolypSand>(), base.Type, 0, 1, 2);
	}
}
