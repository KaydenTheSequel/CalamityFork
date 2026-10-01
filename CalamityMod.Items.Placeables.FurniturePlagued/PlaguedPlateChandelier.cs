using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurniturePlaguedPlate;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurniturePlagued;

public class PlaguedPlateChandelier : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurniturePlaguedPlate.PlaguedPlateChandelier>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PlaguedContainmentBrick>(4).AddIngredient(8, 4).AddIngredient(85)
			.AddTile<PlagueInfuser>()
			.Register();
	}
}
