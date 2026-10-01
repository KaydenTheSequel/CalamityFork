using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class BatholithBangle : ModItem, ILocalizedModType, IModType
{
	public static int cooldown = 600;

	public static int damage = 100;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 28;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.batholithBangle = true;
		calamityPlayer.batholithBangleVisual = !hideVisual;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BlackGlassBand>().AddRecipeGroup("Boss2Material", 15).AddIngredient(3086, 45)
			.AddIngredient(999, 3)
			.AddIngredient(75, 5)
			.AddTile(16)
			.Register();
	}
}
