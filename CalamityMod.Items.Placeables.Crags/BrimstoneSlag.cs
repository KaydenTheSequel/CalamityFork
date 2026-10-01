using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.Crags;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Crags;

public class BrimstoneSlag : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Crags.BrimstoneSlag>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BrimstoneSlagWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
