using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Brimflame;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "BrimflameScowl" })]
public class BrimflameCowl : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ActivationSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/BrimflameAbility");

	public static int MaxManaBoost = 80;

	public static float ManaCostReduction = 0.1f;

	public static float MagicDamageBoost = 0.1f;

	public static int MagicCritBoost = 10;

	public static float SetBonusMagicDamageBoost = 0.08f;

	public static int SetBonusMagicCritBoost = 8;

	public static float FrenzyMagicDamageBoost = 0.4f;

	public static int FrenzyDuration = CalamityUtils.SecondsToFrames(10);

	public static int FrenzyCooldown = CalamityUtils.SecondsToFrames(30);

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaBoost, ManaCostReduction.ToPercent(), MagicDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.defense = 12;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<MagicDamageClass>() += MagicDamageBoost;
		player.GetCritChance<MagicDamageClass>() += MagicCritBoost;
		player.statManaMax2 += MaxManaBoost;
		player.manaCost -= ManaCostReduction;
		player.buffImmune[ModContent.BuffType<BrimstoneFlames>()] = true;
		player.buffImmune[24] = true;
		player.buffImmune[44] = true;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<BrimflameRobes>())
		{
			return legs.type == ModContent.ItemType<BrimflameBoots>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadowSubtle = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().brimflameSet = true;
		player.GetDamage<MagicDamageClass>() += SetBonusMagicDamageBoost;
		player.GetCritChance<MagicDamageClass>() += SetBonusMagicCritBoost;
		Color AbilityBriefColor = Color.Lerp(new Color(250, 202, 140), new Color(227, 79, 79), 0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 3f));
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusMagicDamageBoost.ToPercent(), AbilityBriefColor.Hex3(), CalamityUtils.GetArmorSetBonusKey(), FrenzyDuration.FramesToSeconds(), FrenzyMagicDamageBoost.ToPercent(), FrenzyCooldown.FramesToSeconds());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AshesofCalamity>(4).AddIngredient<UnholyCore>(2).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<BrimflameBoots>())
			.Register();
	}
}
