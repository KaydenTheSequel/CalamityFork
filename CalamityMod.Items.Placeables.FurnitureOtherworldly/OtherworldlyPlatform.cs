using CalamityMod.Tiles.FurnitureOtherworldly;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureOtherworldly;

[LegacyName(new string[] { "OccultPlatform" })]
public class OtherworldlyPlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureOtherworldly.OtherworldlyPlatform>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<OtherworldlyStone>().Register();
	}
}
