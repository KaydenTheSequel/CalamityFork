using System;
using CalamityMod.DataStructures;
using CalamityMod.NPCs;
using CalamityMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatDebuffs;

public class Irradiated : ModBuff
{
	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 20f,
		NPCLifeRegenMethod = IrradiatedNPCLifeRegen
	};

	public static void IrradiatedNPCLifeRegen(NPC npc, int buffType, ref int buffIndex, ref int damage)
	{
		CalamityGlobalNPC cnpc = npc.Calamity();
		int projectileCount = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == ModContent.ProjectileType<WaterLeechProj>() && p.ai[0] == 1f && p.ai[1] == (float)npc.whoAmI)
			{
				projectileCount++;
			}
		}
		int baseIrradiatedDoTValue = (int)(cnpc.scionsCurioEffected ? ((float)(int)(debuffData.EnemyLostRegen * 1.5f)) : debuffData.EnemyLostRegen);
		if (cnpc.scionsCurioEffected)
		{
			for (int playerIndex = 0; playerIndex < 255; playerIndex++)
			{
				Player player = Main.player[playerIndex];
				if (player.active && player.Calamity().scionsCurioDebuffDamage > (float)baseIrradiatedDoTValue && player.Calamity().scionsCurio)
				{
					baseIrradiatedDoTValue = (int)player.Calamity().scionsCurioDebuffDamage;
				}
			}
		}
		if (projectileCount > 0)
		{
			cnpc.ApplyDPSDebuff(projectileCount * baseIrradiatedDoTValue, projectileCount * 4, ref npc.lifeRegen, ref damage);
		}
		else
		{
			cnpc.ApplyDPSDebuff(baseIrradiatedDoTValue, Math.Max((int)((float)baseIrradiatedDoTValue * debuffData.MultiplierDamageTickSize), debuffData.MinimumDamageTickSize), ref npc.lifeRegen, ref damage);
		}
	}

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;
		BuffID.Sets.LongerExpertDebuff[base.Type] = true;
		BuffDatasets.DebuffDataset[base.Type] = debuffData;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().irradiated = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().irradiated = true;
	}
}
