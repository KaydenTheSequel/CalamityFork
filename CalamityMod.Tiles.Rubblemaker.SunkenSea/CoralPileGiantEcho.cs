using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class CoralPileGiantEcho : CoralPileGiant
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/CoralPileGiant";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<EutrophicSand>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<EutrophicSand>(), base.Type, default(int));
	}
}
