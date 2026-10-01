using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.CraftingStations;

public class PlagueInfuser : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Furniture.CraftingStations.PlagueInfuser>());
		base.Item.value = Item.sellPrice(0, 2);
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.CraftingObjects;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PlagueCellCanister>(20).AddIngredient<MysteriousCircuitry>(6).AddTile(16)
			.Register();
	}
}
