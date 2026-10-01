using CalamityMod.Items.Placeables.Crags;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureAncient;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAncient;

public class AncientAltar : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureAncient.AncientAltar>());
		base.Item.value = Item.sellPrice(0, 2);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BrimstoneSlag>(10).AddIngredient<ScorchedBone>(5).AddTile(ModContent.TileType<AshenAltar>())
			.Register();
	}
}
