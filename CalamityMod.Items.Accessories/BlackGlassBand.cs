using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class BlackGlassBand : ModItem, ILocalizedModType, IModType
{
	public static int cooldown = 300;

	public static int damage = 20;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 23;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.bGlassBand = true;
		calamityPlayer.bGlassBandVisual = !hideVisual;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyGoldBar", 8).AddIngredient(182, 3).AddIngredient(173, 35)
			.AddTile(16)
			.Register();
	}
}
