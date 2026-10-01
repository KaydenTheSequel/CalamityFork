using System;
using System.Collections.Generic;
using CalamityMod.Balancing;
using CalamityMod.CalPlayer;
using CalamityMod.World;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class StatMeter : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
	}

	public override void UpdateInventory(Player player)
	{
		if (Main.zenithWorld || (DateTime.Now.Month == 4 && DateTime.Now.Day == 1))
		{
			base.Item.SetNameOverride(this.GetLocalizedValue("FoolsName"));
		}
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		Player player = Main.LocalPlayer;
		if (player == null)
		{
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		Item heldItem = null;
		if (player.selectedItem >= 0 && player.selectedItem < 58)
		{
			heldItem = player.HeldItem;
		}
		int numRageBoosters = (modPlayer.rageBoostOne ? 1 : 0) + (modPlayer.rageBoostTwo ? 1 : 0) + (modPlayer.rageBoostThree ? 1 : 0);
		int rageDuration = BalancingConstants.DefaultRageDuration + numRageBoosters * BalancingConstants.RageDurationPerBooster;
		int numAdrenBoosters = (modPlayer.adrenalineBoostOne ? 1 : 0) + (modPlayer.adrenalineBoostTwo ? 1 : 0) + (modPlayer.adrenalineBoostThree ? 1 : 0);
		float adrenalineDR = BalancingConstants.FullAdrenalineDR + (float)numAdrenBoosters * BalancingConstants.AdrenalineDRPerBooster;
		string stats1 = ((!CalamityWorld.revenge) ? string.Empty : ("\n" + this.GetLocalization("RevStats").Format(OnePlace(100f * modPlayer.RageDamageBoost), rageDuration / 60, OnePlace(100f * modPlayer.GetAdrenalineDamage()), OnePlace(100f * adrenalineDR))));
		list.FindAndReplace("[REV]", stats1);
		string stats2 = string.Empty;
		if (heldItem != null && !heldItem.IsAir)
		{
			if (heldItem.createWall >= 0 || heldItem.createTile >= 0)
			{
				int extraBlockRangeX = player.blockRange + Player.tileRangeX - 5;
				int extraBlockRangeY = player.blockRange + Player.tileRangeY - 4;
				stats2 = stats2 + "\n" + this.GetLocalization("PlacementRange").Format(Sign(extraBlockRangeX) + extraBlockRangeX, Sign(extraBlockRangeY) + extraBlockRangeY);
				if (heldItem.createTile >= 0 && player.tileSpeed != 0f)
				{
					float tileSpeed = 1f / player.tileSpeed - 1f;
					stats2 += this.GetLocalization("TileSpeed").Format(Sign(tileSpeed) + OnePlace(100f * tileSpeed));
				}
				else if (heldItem.createWall >= 0 && player.wallSpeed != 0f)
				{
					float wallSpeed = 1f / player.wallSpeed - 1f;
					stats2 += this.GetLocalization("WallSpeed").Format(Sign(wallSpeed) + OnePlace(100f * wallSpeed));
				}
			}
			else if (heldItem.damage >= 0)
			{
				DamageClass dc = heldItem.DamageType;
				string damageClassLine = dc.DisplayName.ToString();
				bool displayCrit = true;
				bool displayAttackSpeed = true;
				if (dc == DamageClass.Summon)
				{
					displayCrit = false;
					displayAttackSpeed = false;
				}
				else if (dc == DamageClass.SummonMeleeSpeed)
				{
					damageClassLine += this.GetLocalizedValue("ClassNameOverride.SummonMeleeSpeed");
					displayCrit = false;
				}
				else if (dc == DamageClass.MeleeNoSpeed || dc == TrueMeleeNoSpeedDamageClass.Instance)
				{
					damageClassLine += this.GetLocalizedValue("ClassNameOverride.NoSpeed");
					displayAttackSpeed = false;
				}
				else if (dc == DamageClass.Generic)
				{
					damageClassLine = this.GetLocalizedValue("ClassNameOverride.Generic");
				}
				else if (dc == DamageClass.Default)
				{
					damageClassLine = this.GetLocalizedValue("ClassNameOverride.Default");
					displayCrit = false;
				}
				StatModifier currentStats = player.GetTotalDamage(dc);
				string damageStatLine = string.Empty;
				float baseFlatDamage = currentStats.Base;
				if (baseFlatDamage != 0f)
				{
					damageStatLine += this.GetLocalization("DamageBase").Format(Sign(baseFlatDamage) + OnePlace(baseFlatDamage));
				}
				float normalDamage = currentStats.Additive - 1f;
				damageStatLine += this.GetLocalization("DamageNormal").Format(TwoPlaces(100f * normalDamage));
				float multDamage = currentStats.Multiplicative;
				if (multDamage != 1f)
				{
					damageStatLine += this.GetLocalization("DamageMult").Format(TwoPlaces(multDamage));
				}
				float flatDamage = currentStats.Flat;
				if (flatDamage != 0f)
				{
					damageStatLine += this.GetLocalization("DamageFlat").Format(Sign(flatDamage) + OnePlace(flatDamage));
				}
				stats2 = stats2 + "\n" + this.GetLocalization("DamageStats").Format(damageClassLine, damageStatLine, OnePlace(player.GetTotalArmorPenetration(dc)));
				if (displayCrit)
				{
					stats2 += this.GetLocalization("Crit").Format(TwoPlaces(player.GetTotalCritChance(dc)));
				}
				if (displayAttackSpeed)
				{
					float attackSpeed = player.GetTotalAttackSpeed(dc) - 1f;
					stats2 = stats2 + "\n" + this.GetLocalization("AttackSpeed").Format(Sign(attackSpeed) + TwoPlaces(100f * attackSpeed));
					if (dc == DamageClass.SummonMeleeSpeed)
					{
						float meleeSpeed = player.GetAttackSpeed<MeleeDamageClass>() - 1f;
						stats2 += this.GetLocalization("WhipMeleeInheritance").Format(Sign(meleeSpeed) + TwoPlaces(100f * meleeSpeed));
					}
				}
				if (heldItem.useAmmo > 0)
				{
					stats2 = stats2 + "\n" + this.GetLocalization("AmmoStats").Format(OnePlace(100f * player.GetAmmoCostReduction()));
				}
				if (heldItem.mana > 0)
				{
					stats2 = stats2 + "\n" + this.GetLocalization("ManaStats").Format(OnePlace(100f * player.manaCost), player.manaRegen);
				}
				if (dc != DamageClass.SummonMeleeSpeed && (dc == DamageClass.Summon || dc.GetModifierInheritance(DamageClass.Summon).Equals(StatInheritanceData.Full)))
				{
					stats2 = stats2 + "\n" + this.GetLocalization("SummonStats").Format(player.maxMinions, player.maxTurrets);
				}
				float whipRange = player.whipRangeMultiplier - 1f;
				if (dc == DamageClass.SummonMeleeSpeed || dc.GetModifierInheritance(DamageClass.SummonMeleeSpeed).Equals(StatInheritanceData.Full))
				{
					stats2 = stats2 + "\n" + this.GetLocalization("WhipStats").Format(Sign(whipRange) + OnePlace(100f * whipRange));
				}
				float rogueVelocity = modPlayer.rogueVelocity - 1f;
				if (dc == DamageClass.Throwing || dc.GetModifierInheritance(DamageClass.Throwing).Equals(StatInheritanceData.Full))
				{
					stats2 = stats2 + "\n" + this.GetLocalization("RogueStats").Format((int)(100f * modPlayer.rogueStealthMax), TwoPlaces(60f * player.GetStandingStealthRegen()), TwoPlaces(60f * player.GetMovingStealthRegen()), Sign(rogueVelocity) + OnePlace(100f * rogueVelocity));
				}
				if (heldItem.pick > 0 || heldItem.axe > 0 || heldItem.hammer > 0)
				{
					int extraToolRangeX = Player.tileRangeX - 5;
					int extraToolRangeY = Player.tileRangeY - 4;
					stats2 = stats2 + "\n" + this.GetLocalization("ToolRange").Format(Sign(extraToolRangeX) + extraToolRangeX, Sign(extraToolRangeY) + extraToolRangeY);
					if (heldItem.pick > 0)
					{
						float pickSpeed = 1f - player.pickSpeed;
						stats2 += this.GetLocalization("MiningSpeed").Format(Sign(pickSpeed) + OnePlace(100f * pickSpeed));
					}
				}
			}
		}
		list.FindAndReplace("[ITEMS]", stats2);
		float moveSpeedBoost = (CalamityServerConfig.Instance.FasterBaseSpeed ? (player.moveSpeed / BalancingConstants.DefaultMoveSpeedBoost - 1f) : (player.moveSpeed - 1f));
		float wingFlightTime = player.wingTimeMax;
		float luck = player.luck;
		string stats3 = "\n" + this.GetLocalization("GenericStats").Format(player.GetCurrentDefense(), TwoPlaces(100f * player.endurance), OnePlace((float)player.lifeRegen / 2f), Sign(moveSpeedBoost) + TwoPlaces(100f * moveSpeedBoost), TwoPlaces(20f * player.GetJumpBoost()));
		if (wingFlightTime > 0f)
		{
			stats3 += this.GetLocalization("FlightTime").Format(TwoPlaces(wingFlightTime / 60f));
		}
		stats3 = stats3 + "\n" + this.GetLocalization("MiscStats").Format(player.aggro, Sign(luck) + TwoPlaces(100f * luck));
		list.FindAndReplace("[GENERIC]", stats3);
		string stats4 = "\n" + ((!modPlayer.ZoneAbyss) ? this.GetLocalizedValue("AbyssStatsHidden") : this.GetLocalization("AbyssStats").Format(modPlayer.abyssDarkness, modPlayer.abyssPlayerGlowMultiplier, modPlayer.abyssFlashlightWidthMultiplier, TwoPlaces(modPlayer.abyssBreathLossRateStat), modPlayer.abyssLifeLostAtZeroBreathStat, modPlayer.abyssDefenseLossStat));
		list.FindAndReplace("[ABYSS]", stats4);
		list.RemoveAll((TooltipLine l) => l.Mod == "Terraria" && (l.Name == "Favorite" || l.Name == "FavoriteDesc"));
		static string OnePlace(float f)
		{
			return f.ToString("n1");
		}
		static string Sign(float f)
		{
			if (!(f >= 0f))
			{
				return string.Empty;
			}
			return "+";
		}
		static string TwoPlaces(float f)
		{
			return f.ToString("n2");
		}
	}
}
