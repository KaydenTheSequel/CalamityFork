using CalamityMod.Items.Placeables.FurnitureOtherworldly;
using CalamityMod.Walls;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

[LegacyName(new string[] { "OccultStoneWall" })]
public class OtherworldlyStoneWall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<global::CalamityMod.Walls.OtherworldlyStoneWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<OtherworldlyStone>().AddTile(18).Register();
	}
}
