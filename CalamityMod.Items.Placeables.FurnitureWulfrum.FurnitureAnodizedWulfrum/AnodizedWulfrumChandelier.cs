using CalamityMod.Tiles.FurnitureWulfrum.FurnitureAnodizedWulfrum;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureWulfrum.FurnitureAnodizedWulfrum;

public class AnodizedWulfrumChandelier : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureWulfrum.FurnitureAnodizedWulfrum.AnodizedWulfrumChandelier>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<RoundedAnodizedWulfrumPanels>(4).AddIngredient(8, 4).AddIngredient(85)
			.AddTile(283)
			.Register();
	}
}
