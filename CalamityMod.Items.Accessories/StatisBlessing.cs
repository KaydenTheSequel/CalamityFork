using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Neck })]
public class StatisBlessing : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().holyMinions = true;
		player.GetKnockback<SummonDamageClass>() += 2.5f;
		player.GetDamage<SummonDamageClass>() += 0.12f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1864).AddIngredient(1158).AddIngredient(2998)
			.AddIngredient(422, 30)
			.AddIngredient<EssenceofSunlight>(5)
			.AddTile(134)
			.Register();
	}
}
