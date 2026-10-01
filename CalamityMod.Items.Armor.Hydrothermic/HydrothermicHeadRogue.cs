using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Hydrothermic;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "AtaxiaHood" })]
public class HydrothermicHeadRogue : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.12f;

	public static int RogueCritBoost = 10;

	public static float MoveSpeedBoost = 0.05f;

	public static float SetBonusRogueStealth = 1.1f;

	public static int VolleyCooldown = CalamityUtils.SecondsToFrames(2);

	public static int VolleyDamage = 50;

	public static double VolleyDamageRatio = 0.15;

	public static int VolleyDamageSoftcap = 90;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), RogueCritBoost, MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.defense = 12;
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
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusRogueStealth.ToStealth(), VolleyCooldown.FramesToSeconds(), HydrothermicArmor.InfernoHealthThreshold.ToPercent());
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.ataxiaBlaze = true;
		calamityPlayer.ataxiaVolley = true;
		calamityPlayer.rogueStealthMax += SetBonusRogueStealth;
		calamityPlayer.wearingRogueArmor = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ScoriaBar>(7).AddIngredient<EssenceofHavoc>().AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<HydrothermicArmor>())
			.Register();
	}
}
