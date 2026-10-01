using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Hydrothermic;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "AtaxiaHelm" })]
public class HydrothermicHeadMelee : ModItem, ILocalizedModType, IModType
{
	public static float MeleeDamageBoost = 0.12f;

	public static int MeleeCritBoost = 10;

	public static float MeleeSpeedBoost = 0.15f;

	public static int SetBonusAggroBoost = 700;

	public static int GeyserCountLimit = 3;

	public static double GeyserDamageRatio = 0.15;

	public static int GeyserDamageSoftcap = 45;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MeleeDamageBoost.ToPercent(), MeleeCritBoost, MeleeSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.defense = 33;
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
		calamityPlayer.ataxiaGeyser = true;
		player.aggro += SetBonusAggroBoost;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<MeleeDamageClass>() += MeleeDamageBoost;
		player.GetCritChance<MeleeDamageClass>() += MeleeCritBoost;
		player.GetAttackSpeed<MeleeDamageClass>() += MeleeSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ScoriaBar>(7).AddIngredient<EssenceofHavoc>().AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<HydrothermicHeadMagic>())
			.Register();
	}
}
