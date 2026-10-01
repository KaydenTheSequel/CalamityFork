using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureProfaned;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureProfaned;

public class ProfanedTable : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureProfaned.ProfanedTable>());
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ProfanedRock>(8).AddTile<ProfanedCrucible>().Register();
	}
}
