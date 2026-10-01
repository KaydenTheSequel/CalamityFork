using CalamityMod.Tiles.SunkenSea;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.SunkenSea;

public class PinkPearlPile : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.PinkPearlPile>());
		base.Item.rare = 1;
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(4414).AddTile(283).Register();
	}
}
