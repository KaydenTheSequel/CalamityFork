using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class TallDigitateCoralEcho : TallDigitateCoral
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/TallDigitateCoral";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<LimeCoral>(), base.Type, 0);
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<LimeCoral>(), base.Type, default(int));
	}
}
