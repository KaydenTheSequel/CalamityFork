using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class RuinMedallion : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 28;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().stealthStrike75Cost = true;
		player.GetCritChance<ThrowingDamageClass>() += 6f;
		player.GetDamage<ThrowingDamageClass>() += 0.06f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CoinofDeceit>().AddIngredient<UnholyCore>(4).AddIngredient<EssenceofHavoc>(2)
			.AddTile(134)
			.Register();
	}
}
