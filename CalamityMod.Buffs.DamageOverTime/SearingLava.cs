using System;
using CalamityMod.DataStructures;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

[LegacyName(new string[] { "CragsLava" })]
public class SearingLava : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 40f,
		HeatDebuffScaling = 2f,
		NPCLifeRegenMethod = CragsLavaScaling
	};

	public static void CragsLavaScaling(NPC npc, int buffType, ref int buffIndex, ref int damage)
	{
		StatModifier multiplier = npc.Calamity().HeatDebuffMultiplier;
		if (npc.drippingSlime || npc.drippingSparkleSlime)
		{
			multiplier += 1f;
		}
		int dotValue = (int)DebuffData.ApplyScalingToStatModifer(multiplier, debuffData.HeatDebuffScaling).ApplyTo(debuffData.EnemyLostRegen);
		npc.Calamity().ApplyDPSDebuff(dotValue, (int)Math.Max((float)dotValue * debuffData.MultiplierDamageTickSize, debuffData.MinimumDamageTickSize), ref npc.lifeRegen, ref damage);
	}

	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffDatasets.DebuffDataset[base.Type] = debuffData;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().searingLava = true;
	}
}
