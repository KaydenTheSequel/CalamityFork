using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureProfaned;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureProfaned;

public class ProfanedCrystal : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureProfaned.ProfanedCrystal>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(50).AddIngredient(170, 50).AddIngredient<UnholyEssence>().AddTile(133)
			.Register();
		CreateRecipe().AddIngredient<ProfanedCrystalWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
