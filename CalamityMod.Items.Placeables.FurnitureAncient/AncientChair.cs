using CalamityMod.Items.Placeables.Crags;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureAncient;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAncient;

public class AncientChair : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureAncient.AncientChair>());
		base.Item.value = Item.sellPrice(0, 0, 0, 30);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BrimstoneSlag>(4).AddTile<AshenAltar>().Register();
	}
}
