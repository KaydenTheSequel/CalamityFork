using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureVoid;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureVoid;

[LegacyName(new string[] { "VoidClock" })]
public class VoidObelisk : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<VoidClock>());
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SmoothVoidstone>(10).AddRecipeGroup("IronBar", 3).AddIngredient(170, 6)
			.AddTile<VoidCondenser>()
			.Register();
	}
}
