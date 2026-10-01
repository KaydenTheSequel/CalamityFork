using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureStratus;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureStratus;

public class StratusBricks : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureStratus.StratusBricks>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(200).AddRecipeGroup("AnyStoneBlock", 200).AddIngredient<Lumenyl>(3).AddIngredient<RuinousSoul>()
			.AddIngredient<ExodiumCluster>()
			.AddTile<VoidCondenser>()
			.Register();
		CreateRecipe().AddIngredient<StratusWall>(4).AddTile(18).DisableDecraft()
			.Register();
		CreateRecipe().AddIngredient<StratusPlatform>(2).DisableDecraft().Register();
		CreateRecipe().AddIngredient<StratusStarPlatformItem>(2).DisableDecraft().Register();
	}
}
