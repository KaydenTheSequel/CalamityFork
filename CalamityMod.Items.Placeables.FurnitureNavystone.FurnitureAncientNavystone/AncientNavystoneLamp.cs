using CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone;

[LegacyName(new string[] { "EutrophicLamp" })]
public class AncientNavystoneLamp : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone.AncientNavystoneLamp>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AncientSmoothNavystone>(3).AddIngredient(8).AddTile(18)
			.Register();
	}
}
