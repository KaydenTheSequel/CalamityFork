using CalamityMod.Tiles.FurnitureAcidwood;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAcidwood;

public class AcidwoodPiano : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AcidwoodPianoTile>());
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Acidwood>(15).AddIngredient(154, 4).AddIngredient(149)
			.AddTile(106)
			.Register();
	}
}
