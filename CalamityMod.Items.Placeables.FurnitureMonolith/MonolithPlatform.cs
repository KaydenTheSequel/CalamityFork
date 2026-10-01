using CalamityMod.Tiles.FurnitureMonolith;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureMonolith;

public class MonolithPlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureMonolith.MonolithPlatform>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<AstralMonolith>().Register();
	}
}
