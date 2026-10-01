using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class ProtolithBangle : ModItem, ILocalizedModType, IModType
{
	public static int cooldown = 420;

	public static int damage = 60;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 34;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.protolithBangle = true;
		calamityPlayer.protolithBangleVisual = !hideVisual;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BlackGlassBand>().AddRecipeGroup("Boss2Material", 15).AddIngredient(3081, 45)
			.AddIngredient(178, 3)
			.AddIngredient(38, 5)
			.AddTile(16)
			.Register();
	}
}
