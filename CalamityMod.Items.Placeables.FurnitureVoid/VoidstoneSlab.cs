using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureVoid;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureVoid;

public class VoidstoneSlab : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureVoid.VoidstoneSlab>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SmoothVoidstone>().AddTile<VoidCondenser>().Register();
		CreateRecipe().AddIngredient<VoidstoneSlabWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
