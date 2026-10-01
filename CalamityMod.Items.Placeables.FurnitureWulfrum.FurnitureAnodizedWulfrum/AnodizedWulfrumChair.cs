using CalamityMod.Tiles.FurnitureWulfrum.FurnitureAnodizedWulfrum;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureWulfrum.FurnitureAnodizedWulfrum;

public class AnodizedWulfrumChair : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureWulfrum.FurnitureAnodizedWulfrum.AnodizedWulfrumChair>());
		base.Item.value = Item.sellPrice(0, 0, 0, 30);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<RoundedAnodizedWulfrumPanels>(4).AddTile(283).Register();
	}
}
