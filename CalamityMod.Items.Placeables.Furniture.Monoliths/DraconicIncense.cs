using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.Furniture.Monoliths;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.Monoliths;

public class DraconicIncense : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<DraconicIncenseTile>());
		base.Item.value = Item.sellPrice(0, 75);
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.accessory = true;
		base.Item.vanity = true;
	}

	public override void UpdateEquip(Player player)
	{
		if (player.whoAmI == Main.myPlayer)
		{
			player.Calamity().monolithYharonShader = 30;
		}
	}

	public override void UpdateVanity(Player player)
	{
		if (player.whoAmI == Main.myPlayer)
		{
			player.Calamity().monolithYharonShader = 30;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<YharonSoulFragment>(15).AddTile<CosmicAnvil>().Register();
	}
}
