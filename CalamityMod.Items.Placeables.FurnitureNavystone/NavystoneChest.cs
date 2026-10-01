using CalamityMod.Tiles.FurnitureNavystone;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureNavystone;

public class NavystoneChest : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureNavystone.NavystoneChest>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SmoothNavystone>(8).AddRecipeGroup("IronBar", 2).AddTile(18)
			.Register();
	}
}
