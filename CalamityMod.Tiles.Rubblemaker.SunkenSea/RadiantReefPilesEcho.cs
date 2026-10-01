using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class RadiantReefPilesEcho : MediumRadiantReefPile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/MediumRadiantReefPile";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<EutrophicSand>());
		FlexibleTileWand.RubblePlacementMedium.AddVariations(ModContent.ItemType<EutrophicSand>(), base.Type, 0, 1, 2, 3, 4, 5, 6, 7, 8);
	}
}
