using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables;

public class PerennialBrick : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.PerennialBrick>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(50).AddRecipeGroup("AnyStoneBlock", 50).AddIngredient<PerennialOre>().AddTile(17)
			.Register();
		CreateRecipe().AddIngredient<PerennialBrickWall>(4).AddTile(18).DisableDecraft()
			.Register();
		CreateRecipe().AddIngredient<FloralWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
