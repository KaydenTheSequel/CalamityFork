using CalamityMod.Tiles.FurnitureAcidwood;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAcidwood;

public class AcidwoodClock : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AcidwoodClockTile>());
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Acidwood>(10).AddRecipeGroup("IronBar", 3).AddIngredient(170, 6)
			.AddTile(106)
			.Register();
	}
}
