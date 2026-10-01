using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class SmallGleamingBurrowsPileEcho : SmallGleamingBurrowsPile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/SmallGleamingBurrowsPile";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<HardenedEutrophicSand>());
		FlexibleTileWand.RubblePlacementSmall.AddVariations(ModContent.ItemType<HardenedEutrophicSand>(), base.Type, 0, 1, 2);
	}
}
