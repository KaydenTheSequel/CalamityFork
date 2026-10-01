using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureVoid;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureVoid;

public class SmoothVoidstone : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureVoid.SmoothVoidstone>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Voidstone>().AddTile(18).Register();
		CreateRecipe().AddIngredient<SmoothVoidstoneWall>(4).AddTile(18).DisableDecraft()
			.Register();
		CreateRecipe().AddIngredient<SmoothVoidstonePlatform>(2).DisableDecraft().Register();
	}
}
