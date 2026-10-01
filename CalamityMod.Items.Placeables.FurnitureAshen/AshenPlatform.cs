using CalamityMod.Tiles.FurnitureAshen;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAshen;

public class AshenPlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureAshen.AshenPlatform>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<SmoothBrimstoneSlag>().Register();
	}
}
