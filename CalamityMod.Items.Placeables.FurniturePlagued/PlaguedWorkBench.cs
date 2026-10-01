using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurniturePlaguedPlate;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurniturePlagued;

[LegacyName(new string[] { "PlaguedPlateWorkbench" })]
public class PlaguedWorkBench : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<PlaguedPlateWorkBenchTile>());
		base.Item.value = Item.sellPrice(0, 0, 0, 30);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PlaguedContainmentBrick>(10).AddTile<PlagueInfuser>().Register();
	}
}
