using CalamityMod.Items.Placeables.Crags;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureAncient;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAncient;

[LegacyName(new string[] { "AncientPiano" })]
public class AncientPipeOrgan : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureAncient.AncientPipeOrgan>());
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BrimstoneSlag>(15).AddIngredient(154, 4).AddIngredient(149)
			.AddTile<AshenAltar>()
			.Register();
	}
}
