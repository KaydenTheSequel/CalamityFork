using CalamityMod.Tiles.FurnitureMonolith;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureMonolith;

public class MonolithDoor : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<MonolithDoorClosed>());
		base.Item.value = Item.sellPrice(0, 0, 0, 40);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralMonolith>(6).AddTile(18).Register();
	}
}
