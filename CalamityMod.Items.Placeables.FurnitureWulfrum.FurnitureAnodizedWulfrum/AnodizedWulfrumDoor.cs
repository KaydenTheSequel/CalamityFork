using CalamityMod.Tiles.FurnitureWulfrum.FurnitureAnodizedWulfrum;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureWulfrum.FurnitureAnodizedWulfrum;

public class AnodizedWulfrumDoor : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AnodizedWulfrumDoorClosed>());
		base.Item.value = Item.sellPrice(0, 0, 0, 40);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<RoundedAnodizedWulfrumPanels>(6).AddTile(283).Register();
	}
}
