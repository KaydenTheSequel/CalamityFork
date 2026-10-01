using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureProfaned;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureProfaned;

public class RunicProfanedBrick : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureProfaned.RunicProfanedBrick>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(5).AddIngredient<ProfanedRock>(4).AddIngredient<ProfanedCrystal>().AddTile(133)
			.Register();
		CreateRecipe().AddIngredient<RunicProfanedBrickWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
