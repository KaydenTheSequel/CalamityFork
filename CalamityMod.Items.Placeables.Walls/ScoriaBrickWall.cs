using CalamityMod.Walls;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

[LegacyName(new string[] { "ChaoticBrickWall" })]
public class ScoriaBrickWall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<global::CalamityMod.Walls.ScoriaBrickWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<ScoriaBrick>().AddTile(18).Register();
	}
}
