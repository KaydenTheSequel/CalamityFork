using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.CraftingStations;

public class CosmicAnvilItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<CosmicAnvil>());
		base.Item.value = Item.sellPrice(0, 50);
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.CraftingObjects;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("HardmodeAnvil").AddIngredient<CosmiliteBar>(10).AddIngredient(3467, 10)
			.AddIngredient<GalacticaSingularity>(12)
			.AddIngredient<ExodiumCluster>(20)
			.AddTile(412)
			.Register();
	}
}
