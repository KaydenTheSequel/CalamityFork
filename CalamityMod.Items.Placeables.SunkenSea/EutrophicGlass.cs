using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.SunkenSea;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.SunkenSea;

public class EutrophicGlass : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.EutrophicGlass>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<EutrophicSand>(2).AddTile(17).Register();
		CreateRecipe().AddIngredient<EutrophicGlassWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
