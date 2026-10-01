using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Events;
using CalamityMod.NPCs.PlagueEnemies;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class QueenBeeAI : VanillaAIOverride
{
	public static int StingerDamage = 11;

	public override bool AI(Mod mod)
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_1580: Unknown result type (might be due to invalid IL or missing references)
		//IL_159b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d55: Unknown result type (might be due to invalid IL or missing references)
		//IL_2359: Unknown result type (might be due to invalid IL or missing references)
		//IL_2364: Unknown result type (might be due to invalid IL or missing references)
		//IL_2369: Unknown result type (might be due to invalid IL or missing references)
		//IL_236e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2373: Unknown result type (might be due to invalid IL or missing references)
		//IL_2378: Unknown result type (might be due to invalid IL or missing references)
		//IL_237a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2381: Unknown result type (might be due to invalid IL or missing references)
		//IL_2386: Unknown result type (might be due to invalid IL or missing references)
		//IL_2394: Unknown result type (might be due to invalid IL or missing references)
		//IL_239b: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_23af: Unknown result type (might be due to invalid IL or missing references)
		//IL_23b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_245e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2479: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_1816: Unknown result type (might be due to invalid IL or missing references)
		//IL_181d: Unknown result type (might be due to invalid IL or missing references)
		//IL_182a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1842: Unknown result type (might be due to invalid IL or missing references)
		//IL_188b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1890: Unknown result type (might be due to invalid IL or missing references)
		//IL_160a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1622: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_18aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_18af: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_18cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_167d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1682: Unknown result type (might be due to invalid IL or missing references)
		//IL_1689: Unknown result type (might be due to invalid IL or missing references)
		//IL_168e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1693: Unknown result type (might be due to invalid IL or missing references)
		//IL_169b: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e06: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e59: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e66: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ecc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f32: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1efc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f01: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f15: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f21: Unknown result type (might be due to invalid IL or missing references)
		//IL_1917: Unknown result type (might be due to invalid IL or missing references)
		//IL_1924: Unknown result type (might be due to invalid IL or missing references)
		//IL_1929: Unknown result type (might be due to invalid IL or missing references)
		//IL_192e: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1751: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f74: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c48: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c63: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c24: Unknown result type (might be due to invalid IL or missing references)
		//IL_1004: Unknown result type (might be due to invalid IL or missing references)
		//IL_13aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c38: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b23: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b74: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b79: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b88: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b98: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ba2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ba7: Unknown result type (might be due to invalid IL or missing references)
		//IL_102d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1032: Unknown result type (might be due to invalid IL or missing references)
		//IL_25b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_25fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2603: Unknown result type (might be due to invalid IL or missing references)
		//IL_2610: Unknown result type (might be due to invalid IL or missing references)
		//IL_2628: Unknown result type (might be due to invalid IL or missing references)
		//IL_2671: Unknown result type (might be due to invalid IL or missing references)
		//IL_2676: Unknown result type (might be due to invalid IL or missing references)
		//IL_267d: Unknown result type (might be due to invalid IL or missing references)
		//IL_226b: Unknown result type (might be due to invalid IL or missing references)
		//IL_226d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1419: Unknown result type (might be due to invalid IL or missing references)
		//IL_2285: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2691: Unknown result type (might be due to invalid IL or missing references)
		//IL_2696: Unknown result type (might be due to invalid IL or missing references)
		//IL_269b: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_26ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_26b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_26c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_26c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_26cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_26ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_26e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2023: Unknown result type (might be due to invalid IL or missing references)
		//IL_202b: Unknown result type (might be due to invalid IL or missing references)
		//IL_142d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1453: Unknown result type (might be due to invalid IL or missing references)
		//IL_145d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1462: Unknown result type (might be due to invalid IL or missing references)
		//IL_1146: Unknown result type (might be due to invalid IL or missing references)
		//IL_115e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ab0: Unknown result type (might be due to invalid IL or missing references)
		//IL_29bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_206f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2079: Unknown result type (might be due to invalid IL or missing references)
		//IL_2094: Unknown result type (might be due to invalid IL or missing references)
		//IL_209e: Unknown result type (might be due to invalid IL or missing references)
		//IL_147c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1486: Unknown result type (might be due to invalid IL or missing references)
		//IL_148b: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_29e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_29f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_29fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a03: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a12: Unknown result type (might be due to invalid IL or missing references)
		//IL_14aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2108: Unknown result type (might be due to invalid IL or missing references)
		//IL_210a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2780: Unknown result type (might be due to invalid IL or missing references)
		//IL_2788: Unknown result type (might be due to invalid IL or missing references)
		//IL_129b: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_12eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1300: Unknown result type (might be due to invalid IL or missing references)
		//IL_1305: Unknown result type (might be due to invalid IL or missing references)
		//IL_130a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1314: Unknown result type (might be due to invalid IL or missing references)
		//IL_1316: Unknown result type (might be due to invalid IL or missing references)
		//IL_1324: Unknown result type (might be due to invalid IL or missing references)
		//IL_132e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e65: Unknown result type (might be due to invalid IL or missing references)
		//IL_27cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_27d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_27dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_27e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_27e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_27ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_27f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_21bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_21e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2886: Unknown result type (might be due to invalid IL or missing references)
		//IL_289e: Unknown result type (might be due to invalid IL or missing references)
		//IL_28a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_28a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_28ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_28d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_28d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_28db: Unknown result type (might be due to invalid IL or missing references)
		//IL_28e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_28ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_28ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_28f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_28bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_28c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_28c9: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
		}
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool enrage = !BossRushEvent.BossRushActive;
		int i = (int)Main.player[base.NPC.target].Center.X / 16;
		int targetTileY = (int)Main.player[base.NPC.target].Center.Y / 16;
		if (Framing.GetTileSafely(i, targetTileY).WallType == 86)
		{
			enrage = false;
		}
		float maxEnrageScale = 2f;
		float enrageScale = (death ? 0.5f : 0f);
		if (((double)(base.NPC.position.Y / 16f) < Main.worldSurface) & enrage)
		{
			calamityGlobalNPC.CurrentlyEnraged = true;
			enrageScale++;
		}
		if (!Main.player[base.NPC.target].ZoneJungle & enrage)
		{
			calamityGlobalNPC.CurrentlyEnraged = true;
			enrageScale++;
		}
		if (Main.getGoodWorld)
		{
			enrageScale += 0.5f;
		}
		if (enrageScale > maxEnrageScale)
		{
			enrageScale = maxEnrageScale;
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		int beeLimit = (death ? 9 : 15);
		int totalBees = 0;
		bool beeLimitReached = false;
		for (int j = 0; j < Main.maxNPCs; j++)
		{
			NPC bee = Main.npc[j];
			bool isQueenBeeBee = bee.ai[3] == 1f;
			if ((bee.active && (bee.type == 210 || bee.type == 211)) & isQueenBeeBee)
			{
				totalBees++;
				if (totalBees >= beeLimit)
				{
					beeLimitReached = true;
					break;
				}
			}
		}
		int hornetLimit = 2;
		bool hornetLimitReached = false;
		if (death)
		{
			int totalHornets = 0;
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC hornet = Main.npc[k];
				bool isQueenBeeHornet = hornet.ai[3] == 1f;
				if ((hornet.active && (hornet.type == -58 || hornet.type == 232 || hornet.type == -59)) & isQueenBeeHornet)
				{
					int hornetCountIncrement = ((hornet.type == -59) ? 3 : ((hornet.type != 232) ? 1 : 2));
					totalHornets += hornetCountIncrement;
					if (totalHornets >= hornetLimit)
					{
						hornetLimitReached = true;
						break;
					}
				}
			}
		}
		else
		{
			hornetLimitReached = true;
		}
		bool phase2 = lifeRatio < 0.85f;
		bool phase3 = lifeRatio < 0.7f;
		bool phase4 = lifeRatio < 0.5f;
		bool phase5 = lifeRatio < 0.3f;
		bool phase6 = lifeRatio < 0.1f;
		float distanceFromTarget = Vector2.Distance(base.NPC.Center, Main.player[base.NPC.target].Center);
		if (base.NPC.ai[0] != 7f)
		{
			if (base.NPC.timeLeft < 60)
			{
				base.NPC.timeLeft = 60;
			}
			if (distanceFromTarget > 3000f)
			{
				base.NPC.ai[0] = 4f;
			}
		}
		if (Main.player[base.NPC.target].dead)
		{
			base.NPC.ai[0] = 7f;
		}
		bool immuneToSlowingDebuffs = base.NPC.ai[0] == 0f;
		base.NPC.buffImmune[ModContent.BuffType<GlacialState>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TemporalSadness>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Eutrophication>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TimeDistortion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<GalvanicCorrosion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Vaporfied>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[149] = immuneToSlowingDebuffs;
		if (calamityGlobalNPC.newAI[3] == 0f)
		{
			calamityGlobalNPC.newAI[3] = 1f;
			base.NPC.ai[0] = 2f;
			base.NPC.netUpdate = true;
			base.NPC.SyncExtraAI();
		}
		Vector2 center;
		if (base.NPC.ai[0] == 7f)
		{
			base.NPC.damage = 0;
			base.NPC.velocity.Y *= 0.98f;
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			if (base.NPC.position.X < (float)(Main.maxTilesX * 8))
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X *= 0.98f;
				}
				else
				{
					base.NPC.localAI[0] = 1f;
				}
				base.NPC.velocity.X -= 0.08f;
			}
			else
			{
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X *= 0.98f;
				}
				else
				{
					base.NPC.localAI[0] = 1f;
				}
				base.NPC.velocity.X += 0.08f;
			}
			if (base.NPC.timeLeft > 10)
			{
				base.NPC.timeLeft = 10;
			}
		}
		else if (base.NPC.ai[0] == -1f)
		{
			if (Main.netMode != 1)
			{
				int maxRandom = ((!phase4) ? 4 : (death ? 5 : 4));
				int phase7;
				do
				{
					phase7 = Main.rand.Next(maxRandom);
				}
				while ((float)phase7 == base.NPC.ai[1] || phase7 == 1 || ((phase7 == 2) & phase4) || ((death & phase6) && phase7 == 3));
				bool charging = phase7 == 0;
				if (phase7 == 4)
				{
					phase7 = 5;
				}
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
				base.NPC.ai[0] = phase7;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = ((!((phase7 == 5) & phase5)) ? ((phase7 == 5) ? 1f : 0f) : (Main.rand.NextBool() ? 1f : (-1f)));
				if (death)
				{
					base.NPC.ai[3] = (charging ? ((phase6 ? 27f : (phase5 ? 16f : (phase4 ? 27f : (phase2 ? 22f : 17f)))) + 3f * enrageScale) : 0f);
				}
				else
				{
					base.NPC.ai[3] = (charging ? ((phase6 ? 25f : (phase5 ? 14f : (phase4 ? 25f : (phase2 ? 20f : 15f)))) + 3f * enrageScale) : 0f);
				}
				if (death)
				{
					calamityGlobalNPC.newAI[1] = (charging ? ((phase6 ? 700f : (phase5 ? 300f : (phase4 ? 600f : (phase2 ? 500f : 400f)))) - 50f * enrageScale) : 0f);
				}
				else
				{
					calamityGlobalNPC.newAI[1] = (charging ? ((phase6 ? 750f : (phase5 ? 350f : (phase4 ? 650f : (phase2 ? 550f : 450f)))) - 50f * enrageScale) : 0f);
				}
				base.NPC.SyncExtraAI();
			}
		}
		else if (base.NPC.ai[0] == 0f)
		{
			int chargeDistanceX = (int)calamityGlobalNPC.newAI[1];
			int chargeAmt = (int)Math.Ceiling((phase6 ? 2f : (phase5 ? 4f : (phase4 ? 3f : 2f))) + enrageScale);
			if (death)
			{
				chargeAmt = (phase6 ? 1 : (phase5 ? 3 : ((!phase4) ? 1 : 2)));
			}
			int deathChargeLimit = (phase5 ? 3 : 2);
			if (death && chargeAmt > deathChargeLimit)
			{
				chargeAmt = deathChargeLimit;
			}
			if (base.NPC.ai[1] > (float)(2 * chargeAmt) && base.NPC.ai[1] % 2f == 0f)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				return false;
			}
			float velocity = base.NPC.ai[3];
			if (base.NPC.ai[1] % 2f == 0f)
			{
				base.NPC.damage = 0;
				float chargeDistanceY = (phase6 ? 100f : (phase4 ? 50f : 20f));
				chargeDistanceY += 50f * enrageScale;
				if (death)
				{
					chargeDistanceY += MathHelper.Lerp(0f, 100f, 1f - lifeRatio / 2f);
					chargeDistanceY *= 2f;
				}
				float distanceFromTargetX = Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X);
				if (Math.Abs(base.NPC.Center.Y - Main.player[base.NPC.target].Center.Y) < chargeDistanceY && distanceFromTargetX >= (float)chargeDistanceX)
				{
					base.NPC.damage = base.NPC.defDamage;
					base.NPC.localAI[0] = 1f;
					base.NPC.ai[1]++;
					base.NPC.ai[2] = 0f;
					Vector2 beeLocation = base.NPC.Center;
					float targetXDist = Main.player[base.NPC.target].Center.X - beeLocation.X;
					float targetYDist = Main.player[base.NPC.target].Center.Y - beeLocation.Y;
					float targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
					targetDistance = velocity / targetDistance;
					base.NPC.velocity.X = targetXDist * targetDistance;
					base.NPC.velocity.Y = targetYDist * targetDistance;
					float playerLocation = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
					base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
					base.NPC.spriteDirection = base.NPC.direction;
					SoundEngine.PlaySound(in SoundID.Zombie125, base.NPC.Center);
					return false;
				}
				base.NPC.localAI[0] = 0f;
				float chargeVelocityX = (phase4 ? 24f : (phase2 ? 20f : 16f)) + 8f * enrageScale;
				float chargeVelocityY = (phase4 ? 18f : (phase2 ? 15f : 12f)) + 6f * enrageScale;
				float chargeAccelerationX = (phase4 ? 0.7f : (phase2 ? 0.6f : 0.5f)) + 0.25f * enrageScale;
				float chargeAccelerationY = (phase4 ? 0.35f : (phase2 ? 0.3f : 0.25f)) + 0.125f * enrageScale;
				if (death)
				{
					chargeVelocityX++;
					chargeVelocityY += 2f;
					chargeAccelerationX += 0.1f;
					chargeAccelerationY += 0.2f;
				}
				if (base.NPC.Center.Y < Main.player[base.NPC.target].Center.Y - chargeDistanceY)
				{
					base.NPC.velocity.Y += chargeAccelerationY;
				}
				else if (base.NPC.Center.Y > Main.player[base.NPC.target].Center.Y + chargeDistanceY)
				{
					base.NPC.velocity.Y -= chargeAccelerationY;
				}
				else
				{
					base.NPC.velocity.Y *= 0.7f;
				}
				if (base.NPC.velocity.Y < 0f - chargeVelocityY)
				{
					base.NPC.velocity.Y = 0f - chargeVelocityY;
				}
				if (base.NPC.velocity.Y > chargeVelocityY)
				{
					base.NPC.velocity.Y = chargeVelocityY;
				}
				float distanceXMax = 100f;
				float distanceXMin = 20f;
				if (distanceFromTargetX > (float)chargeDistanceX + distanceXMax)
				{
					base.NPC.velocity.X += chargeAccelerationX * (float)base.NPC.direction;
				}
				else if (distanceFromTargetX < (float)chargeDistanceX + distanceXMin)
				{
					base.NPC.velocity.X -= chargeAccelerationX * (float)base.NPC.direction;
				}
				else
				{
					base.NPC.velocity.X *= 0.7f;
				}
				if (base.NPC.velocity.X < 0f - chargeVelocityX)
				{
					base.NPC.velocity.X = 0f - chargeVelocityX;
				}
				if (base.NPC.velocity.X > chargeVelocityX)
				{
					base.NPC.velocity.X = chargeVelocityX;
				}
				float playerLocation2 = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
				base.NPC.direction = ((playerLocation2 < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
			else
			{
				base.NPC.damage = base.NPC.defDamage;
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.direction = -1;
				}
				else
				{
					base.NPC.direction = 1;
				}
				base.NPC.spriteDirection = base.NPC.direction;
				int chargeDirection = 1;
				if (base.NPC.Center.X < Main.player[base.NPC.target].Center.X)
				{
					chargeDirection = -1;
				}
				bool shouldCharge = false;
				if (base.NPC.direction == chargeDirection && Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) > (float)chargeDistanceX)
				{
					base.NPC.ai[2] = 1f;
					shouldCharge = true;
				}
				if (Math.Abs(base.NPC.Center.Y - Main.player[base.NPC.target].Center.Y) > (float)chargeDistanceX * 1.5f)
				{
					base.NPC.ai[2] = 1f;
					shouldCharge = true;
				}
				if ((enrageScale > 0f) & shouldCharge)
				{
					NPC nPC = base.NPC;
					nPC.velocity *= MathHelper.Lerp(0.5f, death ? 0.9f : 1f, 1f - enrageScale / maxEnrageScale);
				}
				if (base.NPC.ai[2] != 1f)
				{
					if (((Vector2)(ref base.NPC.velocity)).Length() < velocity)
					{
						base.NPC.velocity.X = velocity * (float)base.NPC.direction;
					}
					float accelerateGateValue = (phase6 ? 30f : (phase5 ? 10f : 90f));
					if (enrageScale > 0f)
					{
						accelerateGateValue *= 0.75f;
					}
					calamityGlobalNPC.newAI[0]++;
					if (calamityGlobalNPC.newAI[0] > accelerateGateValue)
					{
						base.NPC.SyncExtraAI();
						float velocityXLimit = velocity * 2f;
						if (Math.Abs(base.NPC.velocity.X) < velocityXLimit)
						{
							base.NPC.velocity.X *= (death ? 1.02f : 1.01f);
						}
					}
					float beeSpawnGateValue = 20f;
					if (phase4 && calamityGlobalNPC.newAI[0] % beeSpawnGateValue == 0f && Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						SoundEngine.PlaySound(in SoundID.NPCHit18, base.NPC.Center);
						if (Main.netMode != 1)
						{
							int spawnType = Main.rand.Next(210, 212);
							if (Main.zenithWorld)
							{
								spawnType = ((!phase3) ? 60 : (Main.rand.NextBool(3) ? ModContent.NPCType<PlagueChargerLarge>() : ModContent.NPCType<PlagueCharger>()));
							}
							else if (death)
							{
								switch ((!hornetLimitReached) ? (beeLimitReached ? Main.rand.Next(6, 12) : Main.rand.Next(12)) : 0)
								{
								case 6:
								case 7:
								case 8:
									spawnType = -58;
									break;
								case 9:
								case 10:
									spawnType = 232;
									break;
								case 11:
									spawnType = -59;
									break;
								}
							}
							if (!beeLimitReached || !hornetLimitReached)
							{
								int spawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, spawnType);
								Vector2 beeVelocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
								Main.npc[spawn].velocity = beeVelocity;
								NPC obj = Main.npc[spawn];
								obj.velocity *= 5f;
								if (!Main.zenithWorld)
								{
									Main.npc[spawn].ai[2] = enrageScale;
									Main.npc[spawn].ai[3] = 1f;
								}
								Main.npc[spawn].timeLeft = 600;
								Main.npc[spawn].netUpdate = true;
							}
						}
					}
					base.NPC.localAI[0] = 1f;
					return false;
				}
				base.NPC.damage = 0;
				float playerLocation3 = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
				base.NPC.direction = ((playerLocation3 < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
				base.NPC.localAI[0] = 0f;
				NPC nPC2 = base.NPC;
				nPC2.velocity *= (death ? 0.8f : 0.9f);
				float chargeDeceleration = (death ? 0.2f : 0.1f);
				if (phase2)
				{
					NPC nPC3 = base.NPC;
					nPC3.velocity *= 0.9f;
					chargeDeceleration += 0.05f;
				}
				if (phase4)
				{
					NPC nPC4 = base.NPC;
					nPC4.velocity *= 0.8f;
					chargeDeceleration += 0.1f;
				}
				if (enrageScale > 0f)
				{
					NPC nPC5 = base.NPC;
					nPC5.velocity *= MathHelper.Lerp(0.7f, 1f, 1f - enrageScale / maxEnrageScale);
				}
				if (Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y) < chargeDeceleration)
				{
					base.NPC.ai[2] = 0f;
					base.NPC.ai[1]++;
					calamityGlobalNPC.newAI[0] = 0f;
					base.NPC.SyncExtraAI();
				}
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.damage = 0;
			float playerLocation4 = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
			base.NPC.direction = ((playerLocation4 < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			float beeAttackAccel = (death ? 0.48f : 0.24f);
			float beeAttackSpeed = 12f + enrageScale * 3f;
			if (death)
			{
				beeAttackSpeed *= 1.35f;
			}
			bool canHitTarget = Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
			float distanceAboveTarget = ((!canHitTarget) ? 0f : 320f);
			Vector2 hoverDestination = Main.player[base.NPC.target].Center - Vector2.UnitY * distanceAboveTarget;
			Vector2 idealVelocity = base.NPC.SafeDirectionTo(hoverDestination) * beeAttackSpeed;
			calamityGlobalNPC.newAI[0]++;
			if (((Vector2.Distance(base.NPC.Center, hoverDestination) < 400f) & canHitTarget) || calamityGlobalNPC.newAI[0] >= (death ? 90f : 180f))
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				calamityGlobalNPC.newAI[0] = 0f;
				base.NPC.netUpdate = true;
				base.NPC.SyncExtraAI();
				return false;
			}
			base.NPC.SimpleFlyMovement(idealVelocity, beeAttackAccel);
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			base.NPC.localAI[0] = 0f;
			float beeAttackHoverSpeed = 16f + enrageScale * 4f;
			float beeAttackHoverAccel = (death ? 0.6f : 0.3f);
			if (death)
			{
				beeAttackHoverSpeed *= 1.35f;
			}
			Vector2 beeSpawnLocation = default(Vector2);
			((Vector2)(ref beeSpawnLocation))._002Ector(base.NPC.Center.X + (float)(Main.rand.Next(20) * base.NPC.direction), base.NPC.position.Y + (float)base.NPC.height * 0.8f);
			bool canHitTarget2 = Collision.CanHit(new Vector2(beeSpawnLocation.X, beeSpawnLocation.Y - 30f), 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
			Vector2 hoverDestination2 = Main.player[base.NPC.target].Center - Vector2.UnitY * ((!canHitTarget2) ? 0f : 320f);
			Vector2 idealVelocity2 = base.NPC.SafeDirectionTo(hoverDestination2) * beeAttackHoverSpeed;
			base.NPC.ai[1]++;
			int beeSpawnTimer = 0;
			for (int l = 0; l < 255; l++)
			{
				if (Main.player[l].active && !Main.player[l].dead)
				{
					center = base.NPC.Center - Main.player[l].Center;
					if (((Vector2)(ref center)).Length() < 1000f)
					{
						beeSpawnTimer++;
					}
				}
			}
			base.NPC.ai[1] += beeSpawnTimer / 2;
			if (phase2)
			{
				base.NPC.ai[1]++;
			}
			bool spawnBee = false;
			float beeSpawnCheck = 9f * enrageScale;
			if (base.NPC.ai[1] > beeSpawnCheck)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2]++;
				spawnBee = true;
			}
			if ((Collision.CanHit(beeSpawnLocation, 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) & spawnBee) && (!beeLimitReached || !hornetLimitReached))
			{
				SoundEngine.PlaySound(in SoundID.NPCHit18, beeSpawnLocation);
				if (Main.netMode != 1)
				{
					int spawnType2 = Main.rand.Next(210, 212);
					if (Main.zenithWorld)
					{
						spawnType2 = ((!phase3) ? 60 : (Main.rand.NextBool(3) ? ModContent.NPCType<PlagueChargerLarge>() : ModContent.NPCType<PlagueCharger>()));
					}
					else if (death)
					{
						switch ((!hornetLimitReached) ? (beeLimitReached ? Main.rand.Next(6, 12) : Main.rand.Next(12)) : 0)
						{
						case 6:
						case 7:
						case 8:
							spawnType2 = -58;
							break;
						case 9:
						case 10:
							spawnType2 = 232;
							break;
						case 11:
							spawnType2 = -59;
							break;
						}
					}
					int spawn2 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)beeSpawnLocation.X, (int)beeSpawnLocation.Y, spawnType2);
					Vector2 beeVelocity2 = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
					Main.npc[spawn2].velocity = beeVelocity2;
					NPC obj2 = Main.npc[spawn2];
					obj2.velocity *= 5f;
					if (!Main.zenithWorld)
					{
						Main.npc[spawn2].ai[2] = enrageScale;
						Main.npc[spawn2].ai[3] = 1f;
					}
					Main.npc[spawn2].timeLeft = 600;
					Main.npc[spawn2].netUpdate = true;
				}
			}
			if (Vector2.Distance(beeSpawnLocation, hoverDestination2) > 400f || !canHitTarget2)
			{
				base.NPC.SimpleFlyMovement(idealVelocity2, beeAttackHoverAccel);
			}
			else
			{
				NPC nPC6 = base.NPC;
				nPC6.velocity *= (death ? 0.8f : 0.85f);
			}
			float playerLocation5 = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
			base.NPC.direction = ((playerLocation5 < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			float numSpawns = (death ? 3f : 5f);
			if (base.NPC.ai[2] > numSpawns || (beeLimitReached & hornetLimitReached))
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 2f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.damage = 0;
			float playerLocation6 = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
			base.NPC.direction = ((playerLocation6 < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			float stingerAttackSpeed = 16f + enrageScale * 4f;
			float stingerAttackAccel = (phase6 ? 0.16f : 0.12f);
			if (enrageScale > 0f)
			{
				stingerAttackAccel = MathHelper.Lerp(phase6 ? 0.3f : 0.24f, phase6 ? 0.6f : 0.48f, enrageScale / maxEnrageScale);
			}
			if (death)
			{
				stingerAttackSpeed *= 1.08f;
				stingerAttackAccel *= 1.18f;
			}
			Vector2 stingerSpawnLocation = default(Vector2);
			((Vector2)(ref stingerSpawnLocation))._002Ector(base.NPC.Center.X + (float)(Main.rand.Next(20) * base.NPC.direction), base.NPC.position.Y + (float)base.NPC.height * 0.8f);
			bool canHitTarget3 = Collision.CanHit(new Vector2(stingerSpawnLocation.X, stingerSpawnLocation.Y - 30f), 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
			Vector2 hoverDestination3 = Main.player[base.NPC.target].Center - Vector2.UnitY * ((!canHitTarget3) ? 0f : (phase4 ? 400f : (phase2 ? 360f : 320f)));
			Vector2 idealVelocity3 = base.NPC.SafeDirectionTo(hoverDestination3) * stingerAttackSpeed;
			base.NPC.ai[1]++;
			int stingerAttackTimer = (phase6 ? 40 : (phase2 ? 30 : 20));
			stingerAttackTimer -= (int)Math.Ceiling((phase6 ? 16f : (phase2 ? 12f : 8f)) * enrageScale);
			if (stingerAttackTimer < 5)
			{
				stingerAttackTimer = 5;
			}
			if (base.NPC.ai[1] % (float)stingerAttackTimer == (float)(stingerAttackTimer - 1) && base.NPC.Bottom.Y < Main.player[base.NPC.target].Top.Y && Collision.CanHit(stingerSpawnLocation, 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
			{
				SoundEngine.PlaySound(in SoundID.Item17, stingerSpawnLocation);
				if (Main.netMode != 1)
				{
					float stingerSpeed = (phase3 ? 6f : 5f) + enrageScale;
					if (death)
					{
						stingerSpeed++;
					}
					float stingerTargetX = Main.player[base.NPC.target].Center.X - stingerSpawnLocation.X;
					float stingerTargetY = Main.player[base.NPC.target].Center.Y - stingerSpawnLocation.Y;
					float stingerTargetDist = (float)Math.Sqrt(stingerTargetX * stingerTargetX + stingerTargetY * stingerTargetY);
					stingerTargetDist = stingerSpeed / stingerTargetDist;
					stingerTargetX *= stingerTargetDist;
					stingerTargetY *= stingerTargetDist;
					Vector2 stingerVelocity = default(Vector2);
					((Vector2)(ref stingerVelocity))._002Ector(stingerTargetX, stingerTargetY);
					int type = ((!Main.zenithWorld) ? 719 : (phase3 ? ModContent.ProjectileType<PlagueStingerGoliathV2>() : 325));
					int projectile = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), stingerSpawnLocation, stingerVelocity, type, StingerDamage, 0f, Main.myPlayer, 0f, (Main.zenithWorld & phase3) ? Main.player[base.NPC.target].position.Y : 0f);
					Main.projectile[projectile].timeLeft = 1200;
					Main.projectile[projectile].extraUpdates = 1;
					if (phase2)
					{
						int numExtraStingers = ((!death) ? ((!phase6) ? 1 : 2) : (phase6 ? 4 : 2));
						for (int m = 0; m < numExtraStingers; m++)
						{
							projectile = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), stingerSpawnLocation + Main.rand.NextVector2CircularEdge(16f, 16f) * (float)(m + 1), stingerVelocity * MathHelper.Lerp(0.75f, 1f, (float)m / (float)numExtraStingers), type, StingerDamage, 0f, Main.myPlayer, 0f, (Main.zenithWorld & phase3) ? Main.player[base.NPC.target].position.Y : 0f);
							Main.projectile[projectile].timeLeft = 1200;
							Main.projectile[projectile].extraUpdates = 1;
						}
					}
				}
			}
			if (Vector2.Distance(stingerSpawnLocation, hoverDestination3) > 40f || !canHitTarget3)
			{
				base.NPC.SimpleFlyMovement(idealVelocity3, stingerAttackAccel);
			}
			float numStingerShots = (phase6 ? 5f : (phase2 ? 8f : 15f));
			if (death)
			{
				numStingerShots = (float)Math.Round(numStingerShots * 0.5f);
			}
			if (base.NPC.ai[1] > (float)stingerAttackTimer * numStingerShots)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 3f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 4f)
		{
			base.NPC.damage = 0;
			base.NPC.localAI[0] = 1f;
			float despawnVelMult = 14f;
			Vector2 despawnTargetDist = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
			despawnTargetDist *= 14f;
			base.NPC.velocity = (base.NPC.velocity * despawnVelMult + despawnTargetDist) / (despawnVelMult + 1f);
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			if (distanceFromTarget < 2000f)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.localAI[0] = 0f;
			}
		}
		else if (base.NPC.ai[0] == 5f)
		{
			base.NPC.damage = 0;
			float playerLocation7 = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
			base.NPC.direction = ((playerLocation7 < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			float stingerAttackSpeed2 = 20f + enrageScale * 4f;
			float stingerAttackAccel2 = (phase6 ? 0.7f : 0.5f);
			if (enrageScale > 0f)
			{
				stingerAttackAccel2 = MathHelper.Lerp(phase6 ? 0.9f : 0.7f, phase6 ? 2.4f : 1.8f, enrageScale / maxEnrageScale);
			}
			if (death)
			{
				stingerAttackSpeed2 *= 1.1f;
				stingerAttackAccel2 *= 1.2f;
			}
			int numStingerArcs = (phase6 ? 3 : ((!phase5) ? 1 : 2));
			if (death)
			{
				numStingerArcs++;
			}
			float phaseLimit = (phase6 ? 180f : (phase5 ? 150f : 120f));
			if (death)
			{
				phaseLimit *= 1.5f;
			}
			float stingerAttackTimer2 = (float)Math.Ceiling(phaseLimit / (float)(numStingerArcs + 1));
			float maxDistance = 480f;
			float xLocationScale = MathHelper.Lerp(0f - maxDistance, maxDistance, base.NPC.ai[1] / phaseLimit) * base.NPC.ai[2];
			Vector2 stingerSpawnLocation2 = default(Vector2);
			((Vector2)(ref stingerSpawnLocation2))._002Ector(base.NPC.Center.X + (float)(Main.rand.Next(20) * base.NPC.direction), base.NPC.position.Y + (float)base.NPC.height * 0.8f);
			bool canHitTarget4 = Collision.CanHit(new Vector2(stingerSpawnLocation2.X, stingerSpawnLocation2.Y - 30f), 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
			Vector2 hoverDestination4 = Main.player[base.NPC.target].Center + Vector2.UnitX * xLocationScale * (death ? 1.35f : 1.25f) - Vector2.UnitY * maxDistance;
			Vector2 idealVelocity4 = base.NPC.SafeDirectionTo(hoverDestination4) * stingerAttackSpeed2;
			if ((stingerSpawnLocation2.Y < Main.player[base.NPC.target].Top.Y - maxDistance * 0.8f || !canHitTarget4) && base.NPC.ai[1] < phaseLimit)
			{
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] % stingerAttackTimer2 == 0f && base.NPC.ai[1] != 0f && base.NPC.ai[1] != phaseLimit)
				{
					SoundEngine.PlaySound(in SoundID.Item17, stingerSpawnLocation2);
					if (Main.netMode != 1)
					{
						float stingerSpeed2 = (phase6 ? 5f : 4f) + enrageScale;
						if (death)
						{
							stingerSpeed2++;
						}
						Vector2 projectileVelocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * stingerSpeed2;
						int type2 = (Main.zenithWorld ? ModContent.ProjectileType<PlagueStingerGoliathV2>() : 719);
						int numProj = ((!death) ? (phase6 ? 5 : (phase5 ? 9 : 13)) : (phase6 ? 7 : (phase5 ? 11 : 15)));
						int spread = (phase6 ? 30 : (phase5 ? 50 : 60));
						if (death)
						{
							numProj += (phase6 ? 2 : (phase5 ? 4 : 6));
							spread += (phase6 ? 10 : (phase5 ? 15 : 20));
						}
						float rotation = MathHelper.ToRadians((float)spread);
						for (int n = 0; n < numProj; n++)
						{
							double radians = MathHelper.Lerp(0f - rotation, rotation, (float)n / (float)(numProj - 1));
							center = default(Vector2);
							Vector2 perturbedSpeed = projectileVelocity.RotatedBy(radians, center);
							if ((float)n % 2f != 0f)
							{
								perturbedSpeed *= 0.8f;
							}
							int projectile2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), stingerSpawnLocation2 + perturbedSpeed.SafeNormalize(Vector2.UnitY) * 10f, perturbedSpeed, type2, StingerDamage, 0f, Main.myPlayer, 0f, Main.player[base.NPC.target].position.Y);
							Main.projectile[projectile2].timeLeft = 1200;
							Main.projectile[projectile2].extraUpdates = 1;
							if (!Main.zenithWorld)
							{
								Main.projectile[projectile2].tileCollide = false;
							}
						}
					}
				}
			}
			if (base.NPC.ai[1] >= phaseLimit)
			{
				base.NPC.ai[1]++;
				if (base.NPC.Distance(Main.player[base.NPC.target].Center) > 400f || !canHitTarget4)
				{
					idealVelocity4 = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * stingerAttackSpeed2;
					base.NPC.SimpleFlyMovement(idealVelocity4 * 0.5f, stingerAttackAccel2 * 0.5f);
				}
				else
				{
					NPC nPC7 = base.NPC;
					nPC7.velocity *= 0.8f;
				}
				float idleTime = (death ? 140f : 180f);
				if (base.NPC.ai[1] >= phaseLimit + idleTime)
				{
					base.NPC.ai[0] = -1f;
					base.NPC.ai[1] = 4f;
					base.NPC.ai[2] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else
			{
				base.NPC.SimpleFlyMovement(idealVelocity4, stingerAttackAccel2);
			}
		}
		if (Main.dedServ)
		{
			base.NPC.ForceNetUpdate();
		}
		return false;
	}
}
