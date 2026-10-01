using CalamityMod.Tiles.FurnitureOtherworldly;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureOtherworldly;

[LegacyName(new string[] { "OccultLamp" })]
public class OtherworldlyLamp : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureOtherworldly.OtherworldlyLamp>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<OtherworldlyStone>(3).AddIngredient(8).AddTile(18)
			.Register();
	}
}
