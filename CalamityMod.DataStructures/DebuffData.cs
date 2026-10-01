using System;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class DebuffData
{
	public enum DebuffBehavior
	{
		Default,
		Electric
	}

	public delegate void UpdatePlayerLifeRegen(Player player, int buffType, ref int buffIndex, ref int damage);

	public delegate void UpdateNPCLifeRegen(NPC npc, int buffType, ref int buffIndex, ref int damage);

	public float PlayerLostRegen;

	public float EnemyLostRegen;

	public int EnemyVanillaRegenToCancelOut;

	public int MinimumDamageTickSize = 1;

	public float MultiplierDamageTickSize = 0.25f;

	public float HeatDebuffScaling;

	public float SicknessDebuffScaling;

	public float ColdDebuffScaling;

	public float WaterDebuffScaling;

	public float ElectricDebuffScaling;

	public bool DrawAboveNPC = true;

	public bool GearCanModifyDebuff = true;

	public float AlcoholLevel;

	public UpdatePlayerLifeRegen PlayerUpdateMethod;

	public UpdateNPCLifeRegen NPCLifeRegenMethod;

	public static DebuffData OnFire = new DebuffData
	{
		EnemyLostRegen = 12f,
		EnemyVanillaRegenToCancelOut = 12,
		HeatDebuffScaling = 1f
	};

	public static DebuffData Hellfire = new DebuffData
	{
		EnemyLostRegen = 30f,
		EnemyVanillaRegenToCancelOut = 30,
		HeatDebuffScaling = 1f
	};

	public static DebuffData CursedInferno = new DebuffData
	{
		EnemyLostRegen = 48f,
		EnemyVanillaRegenToCancelOut = 48,
		HeatDebuffScaling = 1f
	};

	public static DebuffData Shadowflame = new DebuffData
	{
		EnemyLostRegen = 60f,
		EnemyVanillaRegenToCancelOut = 60,
		HeatDebuffScaling = 1f
	};

	public static DebuffData Daybroken = new DebuffData
	{
		EnemyLostRegen = 200f,
		EnemyVanillaRegenToCancelOut = 200,
		HeatDebuffScaling = 1f,
		NPCLifeRegenMethod = DaybrokenRegen
	};

	public static DebuffData Burning = new DebuffData
	{
		HeatDebuffScaling = 1f
	};

	public static DebuffData Frostburn = new DebuffData
	{
		EnemyLostRegen = 16f,
		EnemyVanillaRegenToCancelOut = 16,
		ColdDebuffScaling = 1f
	};

	public static DebuffData Frostbite = new DebuffData
	{
		EnemyLostRegen = 50f,
		EnemyVanillaRegenToCancelOut = 50,
		ColdDebuffScaling = 1f
	};

	public static DebuffData Poisoned = new DebuffData
	{
		EnemyLostRegen = 12f,
		EnemyVanillaRegenToCancelOut = 12,
		SicknessDebuffScaling = 1f
	};

	public static DebuffData AcidVenom = new DebuffData
	{
		EnemyLostRegen = 60f,
		EnemyVanillaRegenToCancelOut = 60,
		SicknessDebuffScaling = 1f
	};

	public static DebuffData Electrified = new DebuffData(DebuffBehavior.Electric)
	{
		EnemyLostRegen = 21f,
		EnemyVanillaRegenToCancelOut = 8,
		ElectricDebuffScaling = 1f
	};

	public static DebuffData Oiled = new DebuffData
	{
		EnemyLostRegen = 50f,
		EnemyVanillaRegenToCancelOut = 50,
		HeatDebuffScaling = 2f,
		NPCLifeRegenMethod = OiledNPCMethod
	};

	public static DebuffData DryadsBane = new DebuffData
	{
		EnemyLostRegen = 8f,
		NPCLifeRegenMethod = DryadsBaneNPCMethod
	};

	public DebuffData()
	{
		PlayerUpdateMethod = DefaultUpdateOnPlayer;
		NPCLifeRegenMethod = BaseUpdateNPCLifeRegen;
	}

	public DebuffData(DebuffBehavior behavior)
	{
		PlayerUpdateMethod = DefaultUpdateOnPlayer;
		if (behavior == DebuffBehavior.Electric)
		{
			NPCLifeRegenMethod = ElectricDebuffNPCLifeRegen;
		}
		else
		{
			NPCLifeRegenMethod = BaseUpdateNPCLifeRegen;
		}
	}

	public static StatModifier ApplyScalingToStatModifer(StatModifier Modifer, float scaling)
	{
		StatModifier output = new StatModifier();
		output += (Modifer.Additive - 1f) * scaling;
		output *= 1f + (Modifer.Multiplicative - 1f) * scaling;
		output.Base = Modifer.Base * scaling;
		output.Flat = Modifer.Flat * scaling;
		return output;
	}

	public static StatModifier ForceModifierPositiveWithScaling(StatModifier Modifer, float scaling)
	{
		StatModifier output = new StatModifier();
		output += MathHelper.Max((Modifer.Additive - 1f) * scaling, 0f);
		output *= MathHelper.Max(1f + (Modifer.Multiplicative - 1f) * scaling, 1f);
		output.Base = MathHelper.Max(Modifer.Base * scaling, 0f);
		output.Flat = MathHelper.Max(Modifer.Flat * scaling, 0f);
		return output;
	}

	public void DefaultUpdateOnPlayer(Player player, int buffType, ref int buffIndex, ref int damage)
	{
	}

	public void BaseUpdateNPCLifeRegen(NPC npc, int buffType, ref int buffIndex, ref int damage)
	{
		CalamityGlobalNPC cnpc = npc.Calamity();
		float totalDPS = EnemyLostRegen;
		StatModifier totalScaling = ((HeatDebuffScaling + ColdDebuffScaling + SicknessDebuffScaling + WaterDebuffScaling + ElectricDebuffScaling != 0f) ? ApplyScalingToStatModifer(cnpc.ActiveHeatDebuffMultiplier, HeatDebuffScaling).CombineWith(ApplyScalingToStatModifer(cnpc.ActiveColdDebuffMultiplier, ColdDebuffScaling).CombineWith(ApplyScalingToStatModifer(cnpc.ActiveSicknessDebuffMultiplier, SicknessDebuffScaling).CombineWith(ApplyScalingToStatModifer(cnpc.ActiveWaterDebuffMultiplier, WaterDebuffScaling).CombineWith(ApplyScalingToStatModifer(cnpc.ActiveElectricDebuffMultiplier, ElectricDebuffScaling))))) : cnpc.ActiveTypelessDebuffMultiplier);
		if (totalScaling.Multiplicative <= 0.25f)
		{
			totalScaling.Multiplicative = 0.25f;
		}
		if (totalScaling.Additive < 0.25f)
		{
			totalScaling.Additive = 0.25f;
		}
		totalDPS = totalScaling.ApplyTo(totalDPS);
		float totalDPSAdjusted = totalDPS - (float)EnemyVanillaRegenToCancelOut;
		npc.Calamity().ApplyDPSDebuff((int)totalDPSAdjusted, (int)Math.Max(totalDPS * MultiplierDamageTickSize, MinimumDamageTickSize), ref npc.lifeRegen, ref damage);
	}

	public void ElectricDebuffNPCLifeRegen(NPC npc, int buffType, ref int buffIndex, ref int damage)
	{
		CalamityGlobalNPC cnpc = npc.Calamity();
		float totalDPS = EnemyLostRegen;
		StatModifier totalScaling = ((HeatDebuffScaling + ColdDebuffScaling + SicknessDebuffScaling + WaterDebuffScaling + ElectricDebuffScaling != 0f) ? ApplyScalingToStatModifer(cnpc.ActiveHeatDebuffMultiplier, HeatDebuffScaling).CombineWith(ApplyScalingToStatModifer(cnpc.ActiveColdDebuffMultiplier, ColdDebuffScaling).CombineWith(ApplyScalingToStatModifer(cnpc.ActiveSicknessDebuffMultiplier, SicknessDebuffScaling).CombineWith(ApplyScalingToStatModifer(cnpc.ActiveWaterDebuffMultiplier, WaterDebuffScaling).CombineWith(ApplyScalingToStatModifer(cnpc.ActiveElectricDebuffMultiplier, ElectricDebuffScaling))))) : cnpc.ActiveTypelessDebuffMultiplier);
		if (totalScaling.Multiplicative <= 0.25f)
		{
			totalScaling.Multiplicative = 0.25f;
		}
		if (totalScaling.Additive < 0.25f)
		{
			totalScaling.Additive = 0.25f;
		}
		totalDPS = totalScaling.ApplyTo(totalDPS);
		totalDPS *= (float)((npc.velocity.X == 0f) ? 1 : 4);
		totalDPS -= (float)(EnemyVanillaRegenToCancelOut * ((npc.velocity.X == 0f) ? 1 : 5));
		npc.Calamity().ApplyDPSDebuff((int)totalDPS, (int)Math.Max(totalDPS * MultiplierDamageTickSize, MinimumDamageTickSize), ref npc.lifeRegen, ref damage);
	}

	public static void DaybrokenRegen(NPC npc, int buffType, ref int buffIndex, ref int damage)
	{
		npc.Calamity();
		int numImpaledSpears = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile k = enumerator.Current;
			if (k.type == 636 && k.ai[0] == 1f && k.ai[1] == (float)npc.whoAmI)
			{
				numImpaledSpears++;
			}
		}
		int adjustedSpears = Math.Max(1, numImpaledSpears);
		int baseDaybreakDoTValue = (int)(npc.Calamity().ActiveHeatDebuffMultiplier.ApplyTo(Daybroken.EnemyLostRegen) + Daybroken.EnemyLostRegen * (float)(adjustedSpears - 1));
		int totalDPSAdjusted = baseDaybreakDoTValue - Daybroken.EnemyVanillaRegenToCancelOut * numImpaledSpears;
		if (numImpaledSpears == 0)
		{
			totalDPSAdjusted -= Daybroken.EnemyVanillaRegenToCancelOut;
		}
		npc.Calamity().ApplyDPSDebuff(totalDPSAdjusted, (int)Math.Max((float)baseDaybreakDoTValue * Daybroken.MultiplierDamageTickSize, Daybroken.MinimumDamageTickSize), ref npc.lifeRegen, ref damage);
	}

	public static void OiledNPCMethod(NPC npc, int buffType, ref int buffIndex, ref int damage)
	{
		double totalDPS = ApplyScalingToStatModifer(npc.Calamity().ActiveHeatDebuffMultiplier, Oiled.HeatDebuffScaling).ApplyTo(Oiled.EnemyLostRegen);
		if (!(totalDPS <= 0.0))
		{
			npc.Calamity().ApplyDPSDebuff((int)totalDPS, damage + (int)Math.Max(totalDPS * (double)Oiled.MultiplierDamageTickSize, Oiled.MinimumDamageTickSize), ref npc.lifeRegen, ref damage);
		}
	}

	public static void DryadsBaneNPCMethod(NPC npc, int buffType, ref int buffIndex, ref int damage)
	{
		float buffedDryadsBaneMult = 0f + (NPC.downedMoonlord ? 0.6f : 0f) + (DownedBossSystem.downedProvidence ? 0.2f : 0f) + (DownedBossSystem.downedPolterghast ? 0.2f : 0f) + (DownedBossSystem.downedDoG ? 0.2f : 0f) + (DownedBossSystem.downedYharon ? 0.2f : 0f) + (DownedBossSystem.downedExoMechs ? 0.6f : 0f) + (DownedBossSystem.downedCalamitas ? 0.6f : 0f);
		if (Main.expertMode)
		{
			buffedDryadsBaneMult *= Main.GameModeInfo.TownNPCDamageMultiplier;
		}
		int buffedDryadsBaneDoTValue = 2 * (int)(4f * buffedDryadsBaneMult);
		npc.lifeRegen -= buffedDryadsBaneDoTValue;
		float vanillaDryadsBaneMult = 1f + (NPC.downedBoss1 ? 0.1f : 0f) + (NPC.downedBoss2 ? 0.1f : 0f) + (NPC.downedBoss3 ? 0.1f : 0f) + (NPC.downedQueenBee ? 0.1f : 0f) + (Main.hardMode ? 0.4f : 0f) + (NPC.downedMechBoss1 ? 0.15f : 0f) + (NPC.downedMechBoss1 ? 0.15f : 0f) + (NPC.downedMechBoss1 ? 0.15f : 0f) + (NPC.downedPlantBoss ? 0.15f : 0f) + (NPC.downedGolemBoss ? 0.15f : 0f) + (NPC.downedAncientCultist ? 0.15f : 0f);
		if (Main.expertMode)
		{
			vanillaDryadsBaneMult *= Main.GameModeInfo.TownNPCDamageMultiplier;
		}
		int totalDryadsBaneDoTValue = 2 * (int)(4f * vanillaDryadsBaneMult) + buffedDryadsBaneDoTValue;
		if (damage < totalDryadsBaneDoTValue / 6)
		{
			damage = totalDryadsBaneDoTValue / 6;
		}
	}
}
