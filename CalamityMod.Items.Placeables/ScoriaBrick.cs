using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables;

[LegacyName(new string[] { "ChaoticBrick" })]
public class ScoriaBrick : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.ScoriaBrick>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(50).AddRecipeGroup("AnyStoneBlock", 50).AddIngredient<ScoriaOre>().AddTile(17)
			.Register();
		CreateRecipe().AddIngredient<ScoriaBrickWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
