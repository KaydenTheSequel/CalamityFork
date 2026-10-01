using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class LargeMossyStonePileEcho : LargeMossyStonePile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/LargeMossyStonePile";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<MossyStone>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<MossyStone>(), base.Type, 0, 1, 2);
	}
}
