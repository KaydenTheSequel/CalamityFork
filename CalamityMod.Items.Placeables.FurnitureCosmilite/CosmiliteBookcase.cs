using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureCosmilite;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureCosmilite;

public class CosmiliteBookcase : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureCosmilite.CosmiliteBookcase>());
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmiliteBrick>(20).AddIngredient(149, 10).AddTile<CosmicAnvil>()
			.Register();
	}
}
