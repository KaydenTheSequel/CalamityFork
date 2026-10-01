using System;
using CalamityMod.Buffs.Pets;
using CalamityMod.CalPlayer;
using CalamityMod.Cooldowns;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class DaawnlightSpiritOrigin : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public const int BullseyeIdleLifetime = 600;

	public const int BullseyeHitLifetime = 90;

	public static readonly int CritDecayThreshold = 60;

	public static readonly int CritDecayEchelon = 10;

	public static readonly int CritDecayBaseRate = 4;

	internal const float RegularEnemyBullseyeRadius = 8f;

	internal const float BossBullseyeRadius = 18f;

	public static readonly float RicoshotSearchDistance = 2800f;

	public static readonly int CritHardCap = 100;

	public new string LocalizationCategory => "Items.Accessories";

	public bool HidesNormalTooltip => true;

	public bool HasFlavorTooltip => true;

	public Color? FlavorTooltipColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(149, 28, 235);
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 38;
		base.Item.accessory = true;
		base.Item.rare = 11;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.Calamity().donorItem = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.Calamity();
		modPlayer.spiritOrigin = true;
		if (hideVisual)
		{
			if (player.FindBuffIndex(ModContent.BuffType<ArcherofLunamoon>()) != -1)
			{
				player.ClearBuff(ModContent.BuffType<ArcherofLunamoon>());
			}
		}
		else if (player.whoAmI == Main.myPlayer && player.FindBuffIndex(ModContent.BuffType<ArcherofLunamoon>()) == -1)
		{
			player.AddBuff(ModContent.BuffType<ArcherofLunamoon>(), 18000);
		}
		int currentCritBoost = modPlayer.spiritOriginCritBoost;
		int decayRateEchelons = 0;
		if (currentCritBoost >= CritDecayThreshold)
		{
			decayRateEchelons = 1 + (currentCritBoost - CritDecayThreshold) / CritDecayEchelon;
		}
		int decayRate = Math.Max(1, CritDecayBaseRate - decayRateEchelons);
		int percentToDecay = 1;
		if (decayRateEchelons >= CritDecayBaseRate)
		{
			percentToDecay += decayRateEchelons - CritDecayBaseRate;
		}
		if (player.miscCounter % decayRate == 0 && currentCritBoost > 0)
		{
			currentCritBoost -= percentToDecay;
		}
		modPlayer.spiritOriginCritBoost = currentCritBoost;
		player.GetCritChance<RangedDamageClass>() += Math.Min(modPlayer.spiritOriginCritBoost, CritHardCap);
		if (modPlayer.cooldowns.TryGetValue(DaawnlightSpiritOriginExtraCrit.ID, out var cooldown))
		{
			int displayedCritOnCooldown = Math.Max(0, CritDecayThreshold - currentCritBoost);
			cooldown.timeLeft = displayedCritOnCooldown;
		}
		else
		{
			player.AddCooldown(DaawnlightSpiritOriginExtraCrit.ID, CritDecayThreshold);
		}
	}

	public override void UpdateVanity(Player player)
	{
		player.Calamity().spiritOriginVanity = true;
		if (player.whoAmI == Main.myPlayer && player.FindBuffIndex(ModContent.BuffType<ArcherofLunamoon>()) == -1)
		{
			player.AddBuff(ModContent.BuffType<ArcherofLunamoon>(), 18000);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<DeadshotBrooch>().AddIngredient<MysteriousCircuitry>(15).AddIngredient<DubiousPlating>(15)
			.AddIngredient(3467, 10)
			.AddIngredient<GalacticaSingularity>(4)
			.AddTile(134)
			.Register();
	}
}
