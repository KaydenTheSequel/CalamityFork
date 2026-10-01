using CalamityMod.Tiles.Crags.Tree;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Crags;

public class SpineSapling : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Crags.Tree.SpineSapling>());
		base.Item.value = Item.sellPrice(0, 0, 0, 50);
	}
}
