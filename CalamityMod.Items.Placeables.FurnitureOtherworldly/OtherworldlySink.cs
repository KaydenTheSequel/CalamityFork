using CalamityMod.Tiles.FurnitureOtherworldly;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureOtherworldly;

[LegacyName(new string[] { "OccultSink" })]
public class OtherworldlySink : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureOtherworldly.OtherworldlySink>());
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<OtherworldlyStone>(6).AddIngredient(206).AddTile(18)
			.Register();
	}
}
