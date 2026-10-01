using CalamityMod.Tiles.FurnitureStratus;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureStratus;

public class StratusStarPlatformItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<StratusStarPlatform>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<StratusBricks>().Register();
	}
}
