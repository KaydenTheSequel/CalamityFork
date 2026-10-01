using CalamityMod.Items.Placeables.Walls.DraedonStructures;
using CalamityMod.Tiles.DraedonStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures;

public class LaboratoryPlating : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.DraedonStructures.LaboratoryPlating>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(25).AddRecipeGroup("AnyStoneBlock", 25).AddRecipeGroup("IronBar").AddTile(283)
			.Register();
		CreateRecipe().AddIngredient<RustedPlating>().AddTile(16).Register();
		CreateRecipe().AddIngredient<LaboratoryShelf>(2).Register();
		CreateRecipe().AddIngredient<LaboratoryPlatingWall>(4).AddTile(18).DisableDecraft()
			.Register();
		CreateRecipe().AddIngredient<LaboratoryPlateBeam>(4).AddTile(18).Register();
		CreateRecipe().AddIngredient<LaboratoryPlatePillar>(4).AddTile(18).Register();
	}
}
