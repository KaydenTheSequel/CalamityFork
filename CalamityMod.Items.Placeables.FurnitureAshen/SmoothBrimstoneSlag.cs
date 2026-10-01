using CalamityMod.Items.Placeables.Crags;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureAshen;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAshen;

public class SmoothBrimstoneSlag : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureAshen.SmoothBrimstoneSlag>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BrimstoneSlag>().AddTile(18).Register();
		CreateRecipe().AddIngredient<SmoothBrimstoneSlagWall>(4).AddTile(18).DisableDecraft()
			.Register();
		CreateRecipe().AddIngredient<AshenPlatform>(2).DisableDecraft().Register();
	}
}
