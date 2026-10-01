using CalamityMod.Buffs.DamageOverTime;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "CorruptFlask" })]
public class UnholyTonic : ModItem, ILocalizedModType, IModType
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
		player.buffImmune[ModContent.BuffType<BrainRot>()] = true;
		if (player.ZoneCorrupt)
		{
			player.statDefense += 6;
			player.endurance += 0.04f;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(67, 15).AddIngredient(68, 10).AddTile(16)
			.Register();
	}
}
