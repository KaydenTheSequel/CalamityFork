using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureProfaned;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.CraftingStations;

[LegacyName(new string[] { "ProfanedBasin" })]
public class ProfanedCrucible : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Furniture.CraftingStations.ProfanedCrucible>());
		base.Item.value = Item.sellPrice(0, 2);
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.CraftingObjects;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ProfanedRock>(10).AddIngredient<UnholyEssence>(5).AddTile(134)
			.Register();
	}
}
