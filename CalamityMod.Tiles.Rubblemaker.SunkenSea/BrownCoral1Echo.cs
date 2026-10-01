using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class BrownCoral1Echo : BrownCoral1
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/BrownCoral1";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<YellowCoral>());
		FlexibleTileWand.RubblePlacementMedium.AddVariations(ModContent.ItemType<YellowCoral>(), base.Type, default(int));
	}
}
