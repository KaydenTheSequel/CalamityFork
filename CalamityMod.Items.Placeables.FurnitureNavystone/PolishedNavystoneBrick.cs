using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.FurnitureNavystone;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureNavystone;

public class PolishedNavystoneBrick : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureNavystone.PolishedNavystoneBrick>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Navystone>(2).AddTile(17).Register();
	}
}
