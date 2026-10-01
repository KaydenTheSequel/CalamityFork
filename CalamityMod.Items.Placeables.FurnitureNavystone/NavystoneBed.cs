using CalamityMod.Tiles.FurnitureNavystone;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureNavystone;

public class NavystoneBed : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureNavystone.NavystoneBed>());
		base.Item.value = Item.sellPrice(0, 0, 4);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SmoothNavystone>(15).AddIngredient(225, 5).AddTile(106)
			.Register();
	}
}
