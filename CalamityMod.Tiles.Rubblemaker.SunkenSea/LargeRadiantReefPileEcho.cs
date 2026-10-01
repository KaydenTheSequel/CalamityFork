using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class LargeRadiantReefPileEcho : LargeRadiantReefPile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/LargeRadiantReefPile";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<EutrophicSand>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<EutrophicSand>(), base.Type, 0, 1, 2);
	}
}
