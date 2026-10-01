using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureShellstone;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureShellstone;

public class ShellstoneSlab : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureShellstone.ShellstoneSlab>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Shellstone>().AddTile(283).Register();
		CreateRecipe().AddIngredient<ShellstoneSlabWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
