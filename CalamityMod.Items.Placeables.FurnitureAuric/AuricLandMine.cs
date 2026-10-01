using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureAuric;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAuric;

public class AuricLandMine : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
		ItemID.Sets.CanBePlacedOnWeaponRacks[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AuricLandMineTile>());
		base.Item.value = Item.sellPrice(0, 1);
		base.Item.mech = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe(50).AddIngredient(937, 50).AddIngredient<AuricBar>().AddTile<CosmicAnvil>()
			.Register();
	}
}
