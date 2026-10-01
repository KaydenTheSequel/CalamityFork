using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class SmallMossyStonePileEcho : SmallMossyStonePile
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/SmallMossyStonePile";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<MossyStone>());
		FlexibleTileWand.RubblePlacementSmall.AddVariations(ModContent.ItemType<MossyStone>(), base.Type, 0, 1, 2, 3, 4, 5);
	}
}
