using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class BlueCoralTreeEcho : BlueCoralTree
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/BlueCoralTree";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<CyanCoral>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<CyanCoral>(), base.Type, default(int));
	}
}
