using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureSacrilegious;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureSacrilegious;

public class LargeRitualCandle : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<LargeRitualCandleTile>());
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.placeStyle = 1;
		}
		else
		{
			base.Item.placeStyle = 0;
		}
		return base.CanUseItem(player);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<OccultBrickItem>(6).AddIngredient(8).AddTile<SCalAltar>()
			.Register();
	}
}
