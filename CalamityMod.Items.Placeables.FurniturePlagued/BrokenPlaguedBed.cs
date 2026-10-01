using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurniturePlaguedPlate;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurniturePlagued;

[LegacyName(new string[] { "PlaguedPlateBasin" })]
public class BrokenPlaguedBed : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<PlaguedPlateBed>();
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<PlaguedPlateBasin>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PlaguedContainmentBrick>(10).AddTile<PlagueInfuser>().Register();
	}
}
