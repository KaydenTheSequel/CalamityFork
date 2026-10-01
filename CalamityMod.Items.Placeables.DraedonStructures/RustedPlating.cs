using CalamityMod.Items.Placeables.Walls.DraedonStructures;
using CalamityMod.Tiles.DraedonStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures;

public class RustedPlating : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.DraedonStructures.RustedPlating>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(25).AddRecipeGroup("AnyStoneBlock", 25).AddRecipeGroup("IronBar").AddTile(283)
			.Register();
		CreateRecipe().AddIngredient<LaboratoryPlating>().AddTile(16).Register();
		CreateRecipe().AddIngredient<RustedShelf>(2).Register();
		CreateRecipe().AddIngredient<RustedPlatingWall>(4).AddTile(18).DisableDecraft()
			.Register();
		CreateRecipe().AddIngredient<RustedPlateBeam>(4).AddTile(18).Register();
		CreateRecipe().AddIngredient<RustedPlatePillar>(4).AddTile(18).Register();
	}
}
