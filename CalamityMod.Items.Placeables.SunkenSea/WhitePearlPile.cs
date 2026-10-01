using CalamityMod.Tiles.SunkenSea;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.SunkenSea;

public class WhitePearlPile : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.WhitePearlPile>());
		base.Item.rare = 1;
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(4412).AddTile(283).Register();
	}
}
