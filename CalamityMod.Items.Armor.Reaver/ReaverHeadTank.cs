using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Reaver;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "ReaverHelm" })]
public class ReaverHeadTank : ModItem, ILocalizedModType, IModType
{
	public static int MaxLifeBoost = 50;

	public static int RegenBoost = 8;

	public static int SetBonusAggroBoost = 600;

	public static float SetBonusDebuffDamageReduction = 0.2f;

	public static float SetBonusMobilityReduction = 0.3f;

	public static int ReaverRageDuration = CalamityUtils.SecondsToFrames(5);

	public static int ReaverRageDefenseBoost = 5;

	public static float ReaverRageDamageBoost = 0.1f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	internal static string HealOrbEntitySourceContext => "SetBonus_Calamity_ReaverTank";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxLifeBoost, RegenBoost.ToRegenPerSecond());

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 30;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.defense = 28;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<ReaverScaleMail>())
		{
			return legs.type == ModContent.ItemType<ReaverCuisses>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadowSubtle = true;
		player.armorEffectDrawOutlines = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		player.moveSpeed -= SetBonusMobilityReduction;
		player.aggro += SetBonusAggroBoost;
		calamityPlayer.reaverDefense = true;
		calamityPlayer.wearingRogueArmor = true;
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusDebuffDamageReduction.ToPercent(), SetBonusMobilityReduction.ToPercent(), ReaverRageDuration.FramesToSeconds());
	}

	public override void UpdateEquip(Player player)
	{
		player.statLifeMax2 += MaxLifeBoost;
		player.lifeRegen += RegenBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PerennialBar>(7).AddIngredient<LivingShard>().AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<ReaverHeadMobility>())
			.Register();
	}
}
