using CalamityMod.Tiles.FurnitureAcidwood;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAcidwood;

public class AcidwoodBed : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AcidwoodBedTile>());
		base.Item.value = Item.sellPrice(0, 0, 4);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Acidwood>(15).AddIngredient(225, 5).AddTile(106)
			.Register();
	}
}
