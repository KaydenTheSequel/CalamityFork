using CalamityMod.Items.Placeables.FurnitureNavystone;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.SunkenSea;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.SunkenSea;

public class Navystone : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<Runestone>();
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.Navystone>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<NavystoneWall>(4).AddTile(18).DisableDecraft()
			.Register();
		CreateRecipe().AddIngredient<NavystonePlatform>(2).DisableDecraft().Register();
	}
}
