using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureAuric;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAuric;

public class ExoAuricPanel : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<ExoAuricPanelTile>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(400).AddIngredient<AuricPanel>(400).AddIngredient<ExoPrism>().AddTile<DraedonsForge>()
			.Register();
	}
}
