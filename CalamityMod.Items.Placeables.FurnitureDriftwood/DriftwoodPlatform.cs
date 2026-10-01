using CalamityMod.Tiles.FurnitureDriftwood;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureDriftwood;

public class DriftwoodPlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureDriftwood.DriftwoodPlatform>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<Driftwood>().Register();
	}
}
