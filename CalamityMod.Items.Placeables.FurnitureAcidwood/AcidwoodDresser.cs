using CalamityMod.Tiles.FurnitureAcidwood;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAcidwood;

public class AcidwoodDresser : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AcidwoodDresserTile>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Acidwood>(16).AddTile(106).Register();
	}
}
