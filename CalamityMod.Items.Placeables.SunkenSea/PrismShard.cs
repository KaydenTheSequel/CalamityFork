using CalamityMod.Tiles.SunkenSea;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.SunkenSea;

public class PrismShard : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<SeaPrismCrystals>());
		base.Item.value = Item.sellPrice(0, 0, 1);
		base.Item.rare = 2;
	}
}
