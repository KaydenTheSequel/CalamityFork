using CalamityMod.Tiles.FurnitureOtherworldly;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureOtherworldly;

[LegacyName(new string[] { "OccultCandelabra" })]
public class OtherworldlyCandelabra : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureOtherworldly.OtherworldlyCandelabra>());
		base.Item.value = Item.sellPrice(0, 0, 3);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<OtherworldlyStone>(5).AddIngredient(8, 3).AddTile(18)
			.Register();
	}
}
