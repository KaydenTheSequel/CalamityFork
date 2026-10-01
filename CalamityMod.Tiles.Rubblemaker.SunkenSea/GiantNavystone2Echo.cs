using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class GiantNavystone2Echo : GiantNavystone2
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/GiantNavystone2";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<Navystone>(), base.Type, 0);
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<Navystone>(), base.Type, default(int));
	}
}
