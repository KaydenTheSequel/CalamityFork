using CalamityMod.Items.Placeables.Astral;
using CalamityMod.Walls;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

public class AstralBrickWall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<global::CalamityMod.Walls.AstralBrickWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<AstralBrick>().AddTile(18).Register();
	}
}
