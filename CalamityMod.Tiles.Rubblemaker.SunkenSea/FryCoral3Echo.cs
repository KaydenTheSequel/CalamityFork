using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class FryCoral3Echo : FryCoral3
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/FryCoral3";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<OrangeCoral>(), base.Type, 0);
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<OrangeCoral>(), base.Type, default(int));
	}
}
