using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureStratus;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureStratus;

public class StratusCandelabra : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureStratus.StratusCandelabra>());
		base.Item.value = Item.sellPrice(0, 0, 3);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StratusBricks>(5).AddIngredient(8, 3).AddTile<VoidCondenser>()
			.Register();
	}
}
