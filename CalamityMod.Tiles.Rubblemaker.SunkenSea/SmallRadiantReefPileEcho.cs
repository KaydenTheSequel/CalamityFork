using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class SmallRadiantReefPileEcho : SmallRadiantReefPile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/SmallRadiantReefPile";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<EutrophicSand>());
		FlexibleTileWand.RubblePlacementSmall.AddVariations(ModContent.ItemType<EutrophicSand>(), base.Type, 0, 1, 2);
	}
}
