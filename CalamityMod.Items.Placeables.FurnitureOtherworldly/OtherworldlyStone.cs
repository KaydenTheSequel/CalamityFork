using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureOtherworldly;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureOtherworldly;

[LegacyName(new string[] { "OccultStone" })]
public class OtherworldlyStone : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureOtherworldly.OtherworldlyStone>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(200).AddRecipeGroup("AnyStoneBlock", 200).AddIngredient<DarkPlasma>().AddIngredient<ArmoredShell>()
			.AddIngredient<TwistingNether>()
			.AddIngredient(225, 10)
			.AddTile(133)
			.Register();
		CreateRecipe().AddIngredient<OtherworldlyStoneWall>(4).AddTile(18).DisableDecraft()
			.Register();
		CreateRecipe().AddIngredient<OtherworldlyPlatform>(2).DisableDecraft().Register();
	}
}
