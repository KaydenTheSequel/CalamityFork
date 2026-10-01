using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureAuric;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAuric;

public class AuricRepulserPanel : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AuricRepulserPanelTile>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(400).AddIngredient<ActivatedAuricPanel>(400).AddIngredient<AscendantSpiritEssence>().AddTile<CosmicAnvil>()
			.Register();
	}
}
