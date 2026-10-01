using CalamityMod.Items.Critters;
using CalamityMod.Tiles.Furniture;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class BabyGhostBellJar : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<BabyGhostBellJarTile>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BabyGhostBellItem>().AddIngredient(126).Register();
	}
}
