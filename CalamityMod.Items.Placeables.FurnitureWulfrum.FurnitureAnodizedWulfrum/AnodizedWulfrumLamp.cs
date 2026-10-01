using CalamityMod.Tiles.FurnitureWulfrum.FurnitureAnodizedWulfrum;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureWulfrum.FurnitureAnodizedWulfrum;

public class AnodizedWulfrumLamp : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureWulfrum.FurnitureAnodizedWulfrum.AnodizedWulfrumLamp>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(8).AddIngredient<RoundedAnodizedWulfrumPanels>(3).AddTile(283)
			.Register();
	}
}
