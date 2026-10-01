using System;
using CalamityMod.CalPlayer;
using CalamityMod.Cooldowns;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.PlagueReaper;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class PlagueReaperMask : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ActivationSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/PlagueReaperAbility");

	public static float RangedDamageBoost = 0.1f;

	public static int RangedCritBoost = 8;

	public static float AmmoReduction = 0.75f;

	public static float SetBonusFlightTimeBoost = 0.05f;

	public static float SetBonusPlaguedRangedDamageMult = 1.1f;

	public static float BlackoutRangedDamageBoost = 0.6f;

	public static int BlackoutRangedCritBoost = 20;

	public static int BlackoutDuration = CalamityUtils.SecondsToFrames(5);

	public static int BlackoutCooldown = CalamityUtils.SecondsToFrames(25);

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBoost.ToPercent(), RangedCritBoost, (1f - AmmoReduction).ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.defense = 11;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<PlagueReaperVest>())
		{
			return legs.type == ModContent.ItemType<PlagueReaperStriders>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawOutlines = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		Color AbilityBriefColor = Color.Lerp(new Color(79, 100, 95), new Color(109, 209, 130), 0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 3f));
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusFlightTimeBoost.ToPercent(), SetBonusPlaguedRangedDamageMult, AbilityBriefColor.Hex3(), CalamityUtils.GetArmorSetBonusKey(), BlackoutRangedDamageBoost.ToPercent(), BlackoutRangedCritBoost, BlackoutDuration.FramesToSeconds(), BlackoutCooldown.FramesToSeconds());
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.plagueReaper = true;
		if (calamityPlayer.cooldowns.TryGetValue(PlagueBlackout.ID, out var cd) && cd.timeLeft > BlackoutCooldown)
		{
			player.blind = true;
			player.headcovered = true;
			player.blackout = true;
			player.GetDamage<RangedDamageClass>() += BlackoutRangedDamageBoost;
			player.GetCritChance<RangedDamageClass>() += BlackoutRangedCritBoost;
		}
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().ammoCost *= AmmoReduction;
		player.GetDamage<RangedDamageClass>() += RangedDamageBoost;
		player.GetCritChance<RangedDamageClass>() += RangedCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(151).AddIngredient<PlagueCellCanister>(15).AddIngredient(1346, 11)
			.AddTile(134)
			.Register();
		CreateRecipe().AddIngredient(959).AddIngredient<PlagueCellCanister>(15).AddIngredient(1346, 11)
			.AddTile(134)
			.Register();
	}
}
