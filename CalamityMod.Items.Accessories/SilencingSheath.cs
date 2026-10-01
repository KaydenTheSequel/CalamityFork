using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class SilencingSheath : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 34;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.rogueStealthMax += 0.1f;
		calamityPlayer.stealthGenStandstill += 0.04f;
		calamityPlayer.stealthGenMoving += 0.04f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyEvilBar", 8).AddIngredient(225, 10).AddRecipeGroup("Boss2Material", 3)
			.AddTile(114)
			.Register();
	}
}
