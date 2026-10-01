using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureProfaned;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureProfaned;

public class ProfanedSlab : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureProfaned.ProfanedSlab>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(5).AddIngredient<ProfanedRock>(5).AddTile(133).Register();
		CreateRecipe().AddIngredient<ProfanedSlabWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
