using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Systems;
using CalamityMod.Tiles.FurnitureMarnite;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureMarnite;

public class PolishedMarniteBlock : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureMarnite.PolishedMarniteBlock>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(3081, 2).AddIngredient(3086, 2).AddRecipeGroup(RecipeSystem.AnyGoldOre)
			.AddTile(18)
			.Register();
		CreateRecipe().AddIngredient<PolishedMarniteWall>(4).AddTile(18).DisableDecraft()
			.Register();
		CreateRecipe().AddIngredient<PolishedMarnitePlatform>(2).DisableDecraft().Register();
	}
}
