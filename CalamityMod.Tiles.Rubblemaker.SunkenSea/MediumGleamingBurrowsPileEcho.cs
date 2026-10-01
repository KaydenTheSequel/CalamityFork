using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class MediumGleamingBurrowsPileEcho : MediumGleamingBurrowsPile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/MediumGleamingBurrowsPile";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<HardenedEutrophicSand>());
		FlexibleTileWand.RubblePlacementMedium.AddVariations(ModContent.ItemType<HardenedEutrophicSand>(), base.Type, 0, 1, 2, 3, 4, 5);
	}
}
