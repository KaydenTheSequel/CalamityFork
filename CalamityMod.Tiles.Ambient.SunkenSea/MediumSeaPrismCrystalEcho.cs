using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Ambient.SunkenSea;

public class MediumSeaPrismCrystalEcho : MediumSeaPrismCrystal
{
	public override string Texture => "CalamityMod/Tiles/SunkenSea/MediumSeaPrismCrystal";

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		RegisterItemDrop(ModContent.ItemType<PrismShard>(), base.Type, 0);
		FlexibleTileWand.RubblePlacementMedium.AddVariations(ModContent.ItemType<PrismShard>(), base.Type, default(int));
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
	}
}
