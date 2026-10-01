using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureAshen;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAshen;

public class AshenSlab : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureAshen.AshenSlab>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SmoothBrimstoneSlag>().AddTile<AshenAltar>().Register();
		CreateRecipe().AddIngredient<AshenSlabWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
