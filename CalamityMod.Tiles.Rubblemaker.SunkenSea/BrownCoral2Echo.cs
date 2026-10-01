using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class BrownCoral2Echo : BrownCoral2
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/BrownCoral2";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<YellowCoral>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<YellowCoral>(), base.Type, default(int));
	}
}
