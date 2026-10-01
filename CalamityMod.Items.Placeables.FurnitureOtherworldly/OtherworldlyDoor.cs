using CalamityMod.Tiles.FurnitureOtherworldly;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureOtherworldly;

[LegacyName(new string[] { "OccultDoor" })]
public class OtherworldlyDoor : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<OtherworldlyDoorClosed>());
		base.Item.value = Item.sellPrice(0, 0, 0, 40);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<OtherworldlyStone>(6).AddTile(18).Register();
	}
}
