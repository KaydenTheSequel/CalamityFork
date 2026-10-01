using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureAshen;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAshen;

public class AshenDoor : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AshenDoorClosed>());
		base.Item.value = Item.sellPrice(0, 0, 0, 40);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SmoothBrimstoneSlag>(6).AddTile<AshenAltar>().Register();
	}
}
