using CalamityMod.Items.Critters;
using CalamityMod.Tiles.Furniture;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class SeaMinnowJar : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<SeaMinnowJarTile>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaMinnowItem>().AddIngredient(126).Register();
	}
}
