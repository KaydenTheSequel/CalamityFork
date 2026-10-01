using CalamityMod.Tiles.FurnitureAcidwood;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAcidwood;

public class AcidwoodCandelabra : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AcidwoodCandelabraTile>());
		base.Item.value = Item.sellPrice(0, 0, 3);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Acidwood>(5).AddIngredient(8, 3).AddTile(18)
			.Register();
	}
}
