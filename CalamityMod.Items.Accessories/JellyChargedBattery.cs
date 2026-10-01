using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class JellyChargedBattery : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 22;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = 4;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().voltaicJelly = true;
		player.Calamity().jellyChargedBattery = true;
		player.GetDamage<SummonDamageClass>() += 0.07f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WulfrumBattery>().AddIngredient<VoltaicJelly>().AddIngredient<PurifiedGel>(10)
			.AddIngredient<StormlionMandible>(2)
			.AddTile(16)
			.Register();
	}
}
