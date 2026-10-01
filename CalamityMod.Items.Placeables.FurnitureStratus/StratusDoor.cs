using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureStratus;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureStratus;

public class StratusDoor : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<StratusDoorClosed>());
		base.Item.value = Item.sellPrice(0, 0, 0, 40);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StratusBricks>(6).AddTile<VoidCondenser>().Register();
	}
}
