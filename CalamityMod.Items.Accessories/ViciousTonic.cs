using CalamityMod.Buffs.DamageOverTime;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "CrimsonFlask" })]
public class ViciousTonic : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.buffImmune[ModContent.BuffType<BurningBlood>()] = true;
		if (player.ZoneCrimson)
		{
			player.statDefense += 6;
			player.endurance += 0.04f;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2886, 15).AddIngredient(1330, 10).AddTile(16)
			.Register();
	}
}
