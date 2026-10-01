using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class LargeGleamingBurrowsPileEcho : LargeGleamingBurrowsPile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/LargeGleamingBurrowsPile";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<HardenedEutrophicSand>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<HardenedEutrophicSand>(), base.Type, 0, 1, 2, 3, 4, 5);
	}
}
