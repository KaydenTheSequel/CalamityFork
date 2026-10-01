using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureBotanic;
using CalamityMod.Items.Placeables.FurnitureCosmilite;
using CalamityMod.Items.Placeables.FurnitureSilva;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureAuric;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAuric;

public class AuricToilet : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AuricToiletTile>());
		base.Item.value = Item.sellPrice(0, 40);
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BotanicChair>().AddIngredient<CosmiliteChair>().AddIngredient<SilvaChair>()
			.AddIngredient<AuricBar>(5)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
