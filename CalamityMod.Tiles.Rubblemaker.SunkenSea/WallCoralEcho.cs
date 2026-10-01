using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class WallCoralEcho : WallCoral
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/WallCoral";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<Shellstone>(), base.Type);
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<Shellstone>(), base.Type, 0, 1, 2, 3);
	}
}
