using CalamityMod.Items.Critters;
using CalamityMod.Tiles.Furniture;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class TwinklerInABottle : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<TwinklerInABottleTile>());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(31).AddIngredient<TwinklerItem>().Register();
	}
}
