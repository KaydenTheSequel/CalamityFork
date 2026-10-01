using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Hydrothermic;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "AtaxiaHeadgear" })]
public class HydrothermicHeadRanged : ModItem, ILocalizedModType, IModType
{
	public static float RangedDamageBoost = 0.12f;

	public static int RangedCritBoost = 10;

	public static float AmmoReduction = 0.75f;

	public static int FlareCooldown = CalamityUtils.SecondsToFrames(0.33f);

	public static double FlareDamageRatio = 0.25;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBoost.ToPercent(), RangedCritBoost, (1f - AmmoReduction).ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.defense = 15;
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
		player.setBonus = this.GetLocalization("SetBonus").Format(FlareCooldown.FramesToSeconds(), HydrothermicArmor.InfernoHealthThreshold.ToPercent());
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.ataxiaBlaze = true;
		calamityPlayer.ataxiaBolt = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().ammoCost *= AmmoReduction;
		player.GetDamage<RangedDamageClass>() += RangedDamageBoost;
		player.GetCritChance<RangedDamageClass>() += RangedCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ScoriaBar>(7).AddIngredient<EssenceofHavoc>().AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<HydrothermicHeadMagic>())
			.Register();
	}
}
