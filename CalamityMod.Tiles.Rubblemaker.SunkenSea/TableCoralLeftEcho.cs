using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.SunkenSea;

public class TableCoralLeftEcho : TableCoralLeft
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/Ambient/TableCoralLeft";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<Shellstone>());
		FlexibleTileWand.RubblePlacementMedium.AddVariations(ModContent.ItemType<Shellstone>(), base.Type, 0, 1, 2);
	}
}
