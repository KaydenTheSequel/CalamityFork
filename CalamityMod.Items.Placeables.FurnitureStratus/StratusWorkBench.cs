using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureStratus;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureStratus;

[LegacyName(new string[] { "StratusWorkbench" })]
public class StratusWorkBench : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<StratusWorkBenchTile>());
		base.Item.value = Item.sellPrice(0, 0, 0, 30);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StratusBricks>(10).AddTile<VoidCondenser>().Register();
	}
}
