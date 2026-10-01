using CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureNavystone;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureNavystone;

public class SmoothNavystone : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<AncientSmoothNavystone>();
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureNavystone.SmoothNavystone>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Navystone>().AddTile(18).Register();
		CreateRecipe().AddIngredient<SmoothNavystoneWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
