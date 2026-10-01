using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureAbyss;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAbyss;

public class SmoothAbyssGravel : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureAbyss.SmoothAbyssGravel>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AbyssGravel>().AddTile(18).Register();
		CreateRecipe().AddIngredient<SmoothAbyssGravelWall>(4).AddTile(18).DisableDecraft()
			.Register();
		CreateRecipe().AddIngredient<SmoothAbyssGravelPlatform>(2).DisableDecraft().Register();
	}
}
