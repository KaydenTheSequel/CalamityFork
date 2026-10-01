using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureAuric;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAuric;

public class AuricPanel : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AuricPanelTile>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(400).AddRecipeGroup("AnyStoneBlock", 400).AddIngredient<AuricOre>().AddTile<CosmicAnvil>()
			.Register();
		CreateRecipe().AddIngredient<AuricPlatform>(2).DisableDecraft().Register();
	}
}
