using CalamityMod.Balancing;
using CalamityMod.DataStructures;
using CalamityMod.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class ShellfishClaps : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 150f,
		NPCLifeRegenMethod = ShellfishStacking
	};

	public static void ShellfishStacking(NPC npc, int buffType, ref int buffIndex, ref int damage)
	{
		int projectileCount = 0;
		int owner = 255;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == ModContent.ProjectileType<Shellfish>() && p.ai[0] == 1f && p.ai[1] == (float)npc.whoAmI)
			{
				owner = p.owner;
				projectileCount++;
				if (projectileCount >= 5)
				{
					projectileCount = 5;
					break;
				}
			}
		}
		Item heldItem = Main.player[owner].HeldItem;
		int totalDamage = (int)Main.player[owner].GetTotalDamage<SummonDamageClass>().ApplyTo(debuffData.EnemyLostRegen);
		if (CalamityUtils.ShouldTriggerSummonPenalty(Main.player[owner], heldItem))
		{
			totalDamage = (int)((float)totalDamage * BalancingConstants.SummonerCrossClassNerf);
		}
		int totalDisplayedDamage = totalDamage / 5;
		npc.Calamity().ApplyDPSDebuff(projectileCount * totalDamage, projectileCount * totalDisplayedDamage, ref npc.lifeRegen, ref damage);
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
		npc.Calamity().shellfishStaffDebuff = true;
	}
}
