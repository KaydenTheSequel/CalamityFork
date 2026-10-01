using CalamityMod.Tiles.FurnitureDriftwood;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureDriftwood;

public class DriftwoodToilet : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureDriftwood.DriftwoodToilet>());
		base.Item.value = Item.sellPrice(0, 0, 0, 30);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Driftwood>(4).AddTile(106).Register();
	}
}
