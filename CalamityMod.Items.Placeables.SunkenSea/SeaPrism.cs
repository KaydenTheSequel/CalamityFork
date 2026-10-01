using CalamityMod.Tiles.SunkenSea;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.SunkenSea;

public class SeaPrism : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.SeaPrism>());
		base.Item.value = Item.sellPrice(0, 0, 5);
		base.Item.rare = 2;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PrismShard>(5).AddTile(16).Register();
	}
}
