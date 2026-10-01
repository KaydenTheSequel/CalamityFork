using CalamityMod.DataStructures;
using CalamityMod.Systems.Collections;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class SagePoison : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 50f,
		SicknessDebuffScaling = 1f,
		NPCLifeRegenMethod = SagePoisonPower
	};

	public static float ViridVanguardPoisonMultiplier => 2f;

	public static void SagePoisonPower(NPC npc, int buffType, ref int buffIndex, ref int damage)
	{
		StatModifier multiplier = npc.Calamity().SicknessDebuffMultiplier;
		if (npc.Calamity().irradiated)
		{
			multiplier += (npc.Calamity().scionsCurioEffected ? 1.75f : 1f);
		}
		bool wormBoss = CalamityNPCTypeSets.DesertScourge.Contains(npc.type) || CalamityNPCTypeSets.EaterOfWorlds.Contains(npc.type) || CalamityNPCTypeSets.Perforators.Contains(npc.type) || CalamityNPCTypeSets.AquaticScourge.Contains(npc.type) || CalamityNPCTypeSets.AstrumDeus.Contains(npc.type) || CalamityNPCTypeSets.StormWeaver.Contains(npc.type);
		float NewWeaknessEffectiveness = 1.25f;
		float NewWeaknessEffectivenessWorm = 1.125f;
		float NewResistanceEffectiveness = 0.875f;
		if (npc.Calamity().VulnerableToSickness.HasValue)
		{
			if (npc.Calamity().VulnerableToSickness.Value)
			{
				multiplier *= (wormBoss ? NewWeaknessEffectivenessWorm : NewWeaknessEffectiveness);
			}
			else
			{
				multiplier *= NewResistanceEffectiveness;
			}
		}
		int baseSagePoisonDoTValue = (int)multiplier.ApplyTo(npc.Calamity().sagePoisonDamage);
		npc.Calamity().ApplyDPSDebuff(baseSagePoisonDoTValue, baseSagePoisonDoTValue / 5, ref npc.lifeRegen, ref damage);
	}

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffDatasets.DebuffDataset[base.Type] = debuffData;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().sagePoison = true;
	}
}
