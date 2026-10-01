using CalamityMod.Items.Placeables.Crags;
using CalamityMod.Items.Placeables.FurnitureAshen;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.CraftingStations;

public class AshenAltar : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Furniture.CraftingStations.AshenAltar>());
		base.Item.value = Item.sellPrice(0, 2);
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.CraftingObjects;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SmoothBrimstoneSlag>(10).AddIngredient<ScorchedBone>(5).AddTile(16)
			.Register();
	}
}
