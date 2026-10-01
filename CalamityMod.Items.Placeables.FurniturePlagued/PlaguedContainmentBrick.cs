using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurniturePlaguedPlate;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurniturePlagued;

[LegacyName(new string[] { "PlaguedPlate" })]
public class PlaguedContainmentBrick : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<PlaguedPlate>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(50).AddRecipeGroup("AnyStoneBlock", 50).AddIngredient<PlagueCellCanister>().AddTile<PlagueInfuser>()
			.Register();
		CreateRecipe().AddIngredient<PlaguedPlateWall>(4).AddTile(18).DisableDecraft()
			.Register();
		CreateRecipe().AddIngredient<PlaguedPlatePlatform>(2).DisableDecraft().Register();
	}
}
