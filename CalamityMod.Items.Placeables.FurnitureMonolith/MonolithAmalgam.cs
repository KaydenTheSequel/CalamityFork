using CalamityMod.Tiles.FurnitureMonolith;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureMonolith;

[LegacyName(new string[] { "MonolithCrafting" })]
public class MonolithAmalgam : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureMonolith.MonolithAmalgam>());
		base.Item.value = Item.sellPrice(0, 2);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralMonolith>(20).AddTile(106).Register();
	}
}
