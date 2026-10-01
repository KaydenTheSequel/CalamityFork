using CalamityMod.Tiles.FurnitureNavystone;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureNavystone;

public class NavystonePlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureNavystone.NavystonePlatform>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<SmoothNavystone>().Register();
	}
}
