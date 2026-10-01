using CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone;

[LegacyName(new string[] { "EutrophicCandle" })]
public class AncientNavystoneCandle : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone.AncientNavystoneCandle>());
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AncientSmoothNavystone>(4).AddIngredient(8).AddTile(18)
			.Register();
	}
}
