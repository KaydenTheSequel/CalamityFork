using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureCosmilite;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureCosmilite;

public class CosmiliteBrick : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureCosmilite.CosmiliteBrick>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(200).AddRecipeGroup("AnyStoneBlock", 200).AddIngredient<CosmiliteBar>().AddTile<CosmicAnvil>()
			.Register();
		CreateRecipe().AddIngredient<CosmiliteBrickWall>(4).AddTile(18).DisableDecraft()
			.Register();
		CreateRecipe().AddIngredient<CosmilitePlatform>(2).DisableDecraft().Register();
	}
}
