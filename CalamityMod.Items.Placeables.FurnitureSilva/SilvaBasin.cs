using CalamityMod.Tiles.FurnitureSilva;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureSilva;

[LegacyName(new string[] { "EffulgentManipulator" })]
public class SilvaBasin : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureSilva.SilvaBasin>());
		base.Item.value = Item.sellPrice(0, 2);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SilvaCrystal>(10).AddRecipeGroup("AnyGoldBar", 5).AddTile(302)
			.Register();
	}
}
