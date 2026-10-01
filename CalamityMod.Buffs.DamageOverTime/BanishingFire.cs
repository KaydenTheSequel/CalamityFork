using CalamityMod.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class BanishingFire : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 4000f,
		HeatDebuffScaling = 1f,
		NPCLifeRegenMethod = BanishingFireNPCLifeRegen
	};

	public static void BanishingFireNPCLifeRegen(NPC npc, int buffType, ref int buffIndex, ref int damage)
	{
		int baseBanishingFireDoTValue = (int)npc.Calamity().ActiveHeatDebuffMultiplier.ApplyTo((npc.lifeMax >= 1000000) ? ((float)(npc.lifeMax / 500)) : debuffData.EnemyLostRegen);
		npc.Calamity().ApplyDPSDebuff(baseBanishingFireDoTValue, baseBanishingFireDoTValue / 5, ref npc.lifeRegen, ref damage);
	}

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.LongerExpertDebuff[base.Type] = true;
		BuffDatasets.DebuffDataset[base.Type] = debuffData;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().banishingFire = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().banishingFire = true;
	}
}
