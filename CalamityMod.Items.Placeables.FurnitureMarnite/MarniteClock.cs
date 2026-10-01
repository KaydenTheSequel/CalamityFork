using CalamityMod.Tiles.FurnitureMarnite;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureMarnite;

public class MarniteClock : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureMarnite.MarniteClock>());
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PolishedMarniteBlock>(4).AddRecipeGroup("IronBar", 3).AddIngredient(170, 6)
			.AddTile(106)
			.Register();
	}
}
