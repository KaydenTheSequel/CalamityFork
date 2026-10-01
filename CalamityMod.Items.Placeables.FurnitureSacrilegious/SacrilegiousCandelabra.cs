using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureSacrilegious;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureSacrilegious;

public class SacrilegiousCandelabra : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<SacrilegiousCandelabraTile>());
		base.Item.value = Item.sellPrice(0, 0, 3);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<OccultBrickItem>(5).AddIngredient(8, 3).AddTile<SCalAltar>()
			.Register();
	}
}
