using CalamityMod.DataStructures;
using CalamityMod.Projectiles.Rogue;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class SnapClamDebuff : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 15f,
		NPCLifeRegenMethod = ShellfishStacking
	};

	public static void ShellfishStacking(NPC npc, int buffType, ref int buffIndex, ref int damage)
	{
		int projectileCount = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.ai[0] == 1f && p.ai[1] == (float)npc.whoAmI)
			{
				if (p.type == ModContent.ProjectileType<SnapClamProj>())
				{
					projectileCount += 2;
				}
				if (p.type == ModContent.ProjectileType<SnapClamStealth>())
				{
					projectileCount++;
				}
			}
		}
		npc.Calamity().ApplyDPSDebuff((int)((float)projectileCount * debuffData.EnemyLostRegen), projectileCount * 3, ref npc.lifeRegen, ref damage);
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
		npc.Calamity().snapClamDebuff = true;
	}
}
