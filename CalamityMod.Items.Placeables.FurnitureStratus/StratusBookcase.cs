using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureStratus;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureStratus;

public class StratusBookcase : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureStratus.StratusBookcase>());
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StratusBricks>(20).AddIngredient(149, 10).AddTile<VoidCondenser>()
			.Register();
	}
}
