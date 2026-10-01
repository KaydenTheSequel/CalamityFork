using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurniturePlaguedPlate;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurniturePlagued;

public class PlaguedPlateTable : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurniturePlaguedPlate.PlaguedPlateTable>());
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PlaguedContainmentBrick>(8).AddTile<PlagueInfuser>().Register();
	}
}
