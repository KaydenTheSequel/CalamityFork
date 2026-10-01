using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureWulfrum;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureWulfrum;

public class WulfrumPanels : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureWulfrum.WulfrumPanels>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(25).AddRecipeGroup("AnyStoneBlock", 25).AddIngredient<WulfrumMetalScrap>().AddTile(17)
			.Register();
		CreateRecipe().AddIngredient<WulfrumPanelWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
