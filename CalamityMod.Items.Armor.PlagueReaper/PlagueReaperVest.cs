using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.PlagueReaper;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class PlagueReaperVest : ModItem, ILocalizedModType, IModType
{
	public static float RangedDamageBoost = 0.16f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.defense = 19;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<RangedDamageClass>() += RangedDamageBoost;
		player.buffImmune[ModContent.BuffType<Plague>()] = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(152).AddIngredient<PlagueCellCanister>(29).AddIngredient(1346, 19)
			.AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<PlagueReaperStriders>())
			.Register();
	}
}
