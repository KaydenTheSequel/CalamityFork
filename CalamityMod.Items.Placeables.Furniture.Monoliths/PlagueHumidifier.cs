using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.Monoliths;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.Monoliths;

public class PlagueHumidifier : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<PlagueHumidifierTile>());
		base.Item.value = Item.sellPrice(0, 12);
		base.Item.rare = 8;
		base.Item.accessory = true;
		base.Item.vanity = true;
	}

	public override void UpdateEquip(Player player)
	{
		if (player.whoAmI == Main.myPlayer)
		{
			player.Calamity().monolithPlagueShader = 30;
		}
	}

	public override void UpdateVanity(Player player)
	{
		if (player.whoAmI == Main.myPlayer)
		{
			player.Calamity().monolithPlagueShader = 30;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<InfectedArmorPlating>(15).AddTile(134).Register();
	}
}
