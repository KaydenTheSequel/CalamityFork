using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class EnchantedPearl : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.fishingSkill += 10;
		player.Calamity().enchantedPearl = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(4412).AddIngredient<SeaPrism>(10).AddIngredient<SeaRemains>(3)
			.AddTile(16)
			.Register();
	}
}
