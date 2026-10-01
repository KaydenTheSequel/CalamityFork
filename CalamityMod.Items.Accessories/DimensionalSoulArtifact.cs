using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Items.Placeables.Plates;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class DimensionalSoulArtifact : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 28;
		base.Item.accessory = true;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().dArtifact = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmiliteBar>(5).AddIngredient<Elumplate>(25).AddIngredient<ExodiumCluster>(25)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
