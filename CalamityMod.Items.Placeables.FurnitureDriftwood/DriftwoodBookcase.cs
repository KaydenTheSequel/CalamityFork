using CalamityMod.Tiles.FurnitureDriftwood;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureDriftwood;

public class DriftwoodBookcase : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureDriftwood.DriftwoodBookcase>());
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Driftwood>(20).AddIngredient(149, 10).AddTile(106)
			.Register();
	}
}
