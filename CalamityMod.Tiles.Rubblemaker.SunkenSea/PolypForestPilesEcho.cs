using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class PolypForestPilesEcho : MediumPolypForestPile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/MediumPolypForestPile";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<PolypSand>());
		FlexibleTileWand.RubblePlacementMedium.AddVariations(ModContent.ItemType<PolypSand>(), base.Type, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
	}
}
