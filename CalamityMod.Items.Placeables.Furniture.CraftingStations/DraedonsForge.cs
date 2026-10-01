using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.CraftingStations;

public class DraedonsForge : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Furniture.CraftingStations.DraedonsForge>());
		base.Item.value = Item.sellPrice(4);
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.CraftingObjects;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmicAnvilItem>().AddRecipeGroup("HardmodeForge").AddIngredient(398)
			.AddIngredient(3549)
			.AddIngredient<AuricBar>(15)
			.AddIngredient<ExoPrism>(12)
			.AddIngredient<AscendantSpiritEssence>(15)
			.Register();
	}
}
