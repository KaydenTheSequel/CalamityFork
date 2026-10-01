using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.Crags;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Crags;

public class BrimstoneSlab : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Crags.BrimstoneSlab>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BrimstoneSlag>().AddTile(283).Register();
		CreateRecipe().AddIngredient<BrimstoneSlabWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
