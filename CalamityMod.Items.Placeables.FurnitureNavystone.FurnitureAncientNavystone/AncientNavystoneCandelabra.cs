using CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone;

[LegacyName(new string[] { "EutrophicCandelabra" })]
public class AncientNavystoneCandelabra : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone.AncientNavystoneCandelabra>());
		base.Item.value = Item.sellPrice(0, 0, 3);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AncientSmoothNavystone>(5).AddIngredient(8, 3).AddTile(18)
			.Register();
	}
}
