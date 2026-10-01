using CalamityMod.Tiles.FurnitureExo;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureExo;

public class ExoPlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<ExoPlatformTile>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<ExoPlating>().Register();
	}
}
