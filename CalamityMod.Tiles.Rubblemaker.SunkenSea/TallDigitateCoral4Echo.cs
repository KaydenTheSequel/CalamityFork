using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class TallDigitateCoral4Echo : TallDigitateCoral4
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/TallDigitateCoral4";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<LimeCoral>(), base.Type, 0);
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<LimeCoral>(), base.Type, default(int));
	}
}
