using CalamityMod.Tiles.FurnitureCosmilite;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureCosmilite;

public class CosmilitePlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureCosmilite.CosmilitePlatform>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<CosmiliteBrick>().Register();
	}
}
