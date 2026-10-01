using CalamityMod.Tiles.FurnitureSilva;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureSilva;

public class SilvaClock : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureSilva.SilvaClock>());
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SilvaCrystal>(10).AddRecipeGroup("IronBar", 3).AddIngredient(170, 6)
			.AddTile(302)
			.Register();
	}
}
