using CalamityMod.Items.Placeables.FurnitureProfaned;
using CalamityMod.Walls;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

public class ProfanedCrystalWall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<global::CalamityMod.Walls.ProfanedCrystalWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<ProfanedCrystal>().AddTile(18).Register();
	}
}
