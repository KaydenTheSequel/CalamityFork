using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone;

[LegacyName(new string[] { "EutrophicCrafting" })]
public class EutrophicShelf : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone.EutrophicShelf>());
		base.Item.value = Item.sellPrice(0, 0, 50);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AncientSmoothNavystone>(10).AddIngredient<SeaPrism>(5).AddIngredient<PrismShard>(5)
			.AddIngredient<PearlShard>(3)
			.AddTile(106)
			.Register();
	}
}
