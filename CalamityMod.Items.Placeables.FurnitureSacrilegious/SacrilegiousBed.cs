using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureSacrilegious;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureSacrilegious;

public class SacrilegiousBed : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<SacrilegiousBedTile>());
		base.Item.value = Item.sellPrice(0, 0, 4);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<OccultBrickItem>(15).AddIngredient(225, 5).AddTile<SCalAltar>()
			.Register();
	}
}
