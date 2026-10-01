using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Hydrothermic;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "AtaxiaMask" })]
public class HydrothermicHeadMagic : ModItem, ILocalizedModType, IModType
{
	public static int MaxManaBoost = 100;

	public static float ManaCostReduction = 0.15f;

	public static float MagicDamageBoost = 0.12f;

	public static int MagicCritBoost = 10;

	public static double OrbDamageRatio = 0.6;

	public static float OrbDamageCooldownMult = 0.5f;

	public static double OrbHealingRatio = 0.1;

	public static double OrbHealingRatioLossPerPierce = 0.05;

	public static float OrbHealingCooldownMult = 1.25f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaBoost, ManaCostReduction.ToPercent(), MagicDamageBoost.ToPercent(), MagicCritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.defense = 9;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<HydrothermicArmor>())
		{
			return legs.type == ModContent.ItemType<HydrothermicSubligar>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawOutlines = true;
		player.Calamity().hydrothermalSmoke = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = this.GetLocalization("SetBonus").Format(HydrothermicArmor.InfernoHealthThreshold.ToPercent());
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.ataxiaBlaze = true;
		calamityPlayer.ataxiaMage = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.statManaMax2 += MaxManaBoost;
		player.manaCost -= ManaCostReduction;
		player.GetDamage<MagicDamageClass>() += MagicDamageBoost;
		player.GetCritChance<MagicDamageClass>() += MagicCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ScoriaBar>(7).AddIngredient<EssenceofHavoc>().AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<HydrothermicArmor>())
			.Register();
	}
}
