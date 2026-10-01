using CalamityMod.Tiles.FurnitureStatigel;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureStatigel;

public class StaticRefiner : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureStatigel.StaticRefiner>());
		base.Item.value = Item.sellPrice(0, 0, 5);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StatigelBlock>(10).AddTile(220).Register();
	}
}
