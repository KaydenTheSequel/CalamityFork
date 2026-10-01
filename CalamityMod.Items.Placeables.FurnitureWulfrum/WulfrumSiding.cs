using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureWulfrum;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureWulfrum;

public class WulfrumSiding : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureWulfrum.WulfrumSiding>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(25).AddRecipeGroup("AnyStoneBlock", 25).AddIngredient<AnodizedWulfrumMetal>().AddIngredient<WulfrumMetalScrap>()
			.AddTile(17)
			.Register();
		CreateRecipe().AddIngredient<WulfrumSidingWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
