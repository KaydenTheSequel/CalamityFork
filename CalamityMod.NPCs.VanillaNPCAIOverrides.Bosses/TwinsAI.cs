using System;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public static class TwinsAI
{
	public class RetinazerAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0171: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Unknown result type (might be due to invalid IL or missing references)
			//IL_025f: Unknown result type (might be due to invalid IL or missing references)
			//IL_029b: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_040c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0411: Unknown result type (might be due to invalid IL or missing references)
			//IL_0424: Unknown result type (might be due to invalid IL or missing references)
			//IL_042b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0430: Unknown result type (might be due to invalid IL or missing references)
			//IL_0444: Unknown result type (might be due to invalid IL or missing references)
			//IL_0446: Unknown result type (might be due to invalid IL or missing references)
			//IL_0448: Unknown result type (might be due to invalid IL or missing references)
			//IL_044d: Unknown result type (might be due to invalid IL or missing references)
			//IL_044f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0454: Unknown result type (might be due to invalid IL or missing references)
			//IL_0456: Unknown result type (might be due to invalid IL or missing references)
			//IL_045b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1050: Unknown result type (might be due to invalid IL or missing references)
			//IL_1099: Unknown result type (might be due to invalid IL or missing references)
			//IL_109f: Unknown result type (might be due to invalid IL or missing references)
			//IL_10b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_10bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_10c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0935: Unknown result type (might be due to invalid IL or missing references)
			//IL_0940: Unknown result type (might be due to invalid IL or missing references)
			//IL_0945: Unknown result type (might be due to invalid IL or missing references)
			//IL_094a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0952: Unknown result type (might be due to invalid IL or missing references)
			//IL_0957: Unknown result type (might be due to invalid IL or missing references)
			//IL_095e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0963: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e90: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e9b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d80: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0567: Unknown result type (might be due to invalid IL or missing references)
			//IL_056c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0573: Unknown result type (might be due to invalid IL or missing references)
			//IL_057b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0580: Unknown result type (might be due to invalid IL or missing references)
			//IL_0585: Unknown result type (might be due to invalid IL or missing references)
			//IL_058c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0591: Unknown result type (might be due to invalid IL or missing references)
			//IL_0596: Unknown result type (might be due to invalid IL or missing references)
			//IL_059d: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0da8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dd2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dd7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dd9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0de0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0de5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dea: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aba: Unknown result type (might be due to invalid IL or missing references)
			//IL_0675: Unknown result type (might be due to invalid IL or missing references)
			//IL_0679: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_060b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0610: Unknown result type (might be due to invalid IL or missing references)
			//IL_0615: Unknown result type (might be due to invalid IL or missing references)
			//IL_061a: Unknown result type (might be due to invalid IL or missing references)
			//IL_061f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0648: Unknown result type (might be due to invalid IL or missing references)
			//IL_0655: Unknown result type (might be due to invalid IL or missing references)
			//IL_065a: Unknown result type (might be due to invalid IL or missing references)
			//IL_065c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0663: Unknown result type (might be due to invalid IL or missing references)
			//IL_0668: Unknown result type (might be due to invalid IL or missing references)
			//IL_0627: Unknown result type (might be due to invalid IL or missing references)
			//IL_062e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0633: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fc6: Unknown result type (might be due to invalid IL or missing references)
			//IL_100f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1015: Unknown result type (might be due to invalid IL or missing references)
			//IL_1039: Unknown result type (might be due to invalid IL or missing references)
			//IL_1044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ec5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ef4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f1b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f4a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f6d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f9c: Unknown result type (might be due to invalid IL or missing references)
			//IL_12aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_12af: Unknown result type (might be due to invalid IL or missing references)
			//IL_12b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_12bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_12c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_12c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_12cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_12d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_12e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_12e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_12ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_12f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_12f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_12fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_16af: Unknown result type (might be due to invalid IL or missing references)
			//IL_16b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_16bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_16c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_16c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_16cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_16d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_16d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_16de: Unknown result type (might be due to invalid IL or missing references)
			//IL_16ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_16f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_16f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1700: Unknown result type (might be due to invalid IL or missing references)
			//IL_1705: Unknown result type (might be due to invalid IL or missing references)
			//IL_170a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1712: Unknown result type (might be due to invalid IL or missing references)
			//IL_1716: Unknown result type (might be due to invalid IL or missing references)
			//IL_1739: Unknown result type (might be due to invalid IL or missing references)
			//IL_1744: Unknown result type (might be due to invalid IL or missing references)
			//IL_1749: Unknown result type (might be due to invalid IL or missing references)
			//IL_139f: Unknown result type (might be due to invalid IL or missing references)
			//IL_13a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1311: Unknown result type (might be due to invalid IL or missing references)
			//IL_1313: Unknown result type (might be due to invalid IL or missing references)
			//IL_131a: Unknown result type (might be due to invalid IL or missing references)
			//IL_131f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1324: Unknown result type (might be due to invalid IL or missing references)
			//IL_1335: Unknown result type (might be due to invalid IL or missing references)
			//IL_133a: Unknown result type (might be due to invalid IL or missing references)
			//IL_133f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1344: Unknown result type (might be due to invalid IL or missing references)
			//IL_1349: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a2f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a36: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a3b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1372: Unknown result type (might be due to invalid IL or missing references)
			//IL_137f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1384: Unknown result type (might be due to invalid IL or missing references)
			//IL_1386: Unknown result type (might be due to invalid IL or missing references)
			//IL_138d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1392: Unknown result type (might be due to invalid IL or missing references)
			//IL_1351: Unknown result type (might be due to invalid IL or missing references)
			//IL_1358: Unknown result type (might be due to invalid IL or missing references)
			//IL_135d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a42: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a4d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a52: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a57: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a5c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a63: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a68: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bcf: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b3b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b45: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b4a: Unknown result type (might be due to invalid IL or missing references)
			//IL_17c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_17ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_148b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1496: Unknown result type (might be due to invalid IL or missing references)
			//IL_149b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1871: Unknown result type (might be due to invalid IL or missing references)
			//IL_187c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1881: Unknown result type (might be due to invalid IL or missing references)
			//IL_1886: Unknown result type (might be due to invalid IL or missing references)
			//IL_188b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1892: Unknown result type (might be due to invalid IL or missing references)
			//IL_1897: Unknown result type (might be due to invalid IL or missing references)
			//IL_18ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_18b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_18b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_18b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_18c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_18c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_18cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f68: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f6d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f74: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f7c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f81: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f91: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f96: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f9e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fa3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1faa: Unknown result type (might be due to invalid IL or missing references)
			//IL_1faf: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fb7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c37: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c42: Unknown result type (might be due to invalid IL or missing references)
			//IL_0839: Unknown result type (might be due to invalid IL or missing references)
			//IL_0844: Unknown result type (might be due to invalid IL or missing references)
			//IL_0849: Unknown result type (might be due to invalid IL or missing references)
			//IL_084e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0853: Unknown result type (might be due to invalid IL or missing references)
			//IL_085a: Unknown result type (might be due to invalid IL or missing references)
			//IL_085f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0873: Unknown result type (might be due to invalid IL or missing references)
			//IL_0878: Unknown result type (might be due to invalid IL or missing references)
			//IL_087a: Unknown result type (might be due to invalid IL or missing references)
			//IL_087f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0889: Unknown result type (might be due to invalid IL or missing references)
			//IL_088e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0893: Unknown result type (might be due to invalid IL or missing references)
			//IL_1514: Unknown result type (might be due to invalid IL or missing references)
			//IL_1540: Unknown result type (might be due to invalid IL or missing references)
			//IL_15a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_15b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_15b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_15be: Unknown result type (might be due to invalid IL or missing references)
			//IL_15c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_15ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_15cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_15e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_15e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_15ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_15ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_15f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_15fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_1603: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d06: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d11: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d16: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d1b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d20: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d27: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d2c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d40: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d4b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d50: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d55: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d5f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d64: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d69: Unknown result type (might be due to invalid IL or missing references)
			CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				CalamityTargetingParameters options = CalamityTargetingParameters.BossDefaults;
				options.aggroRatio = -1f;
				base.NPC.CalamityTargeting(options);
			}
			float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
			CalamityGlobalNPC.laserEye = base.NPC.whoAmI;
			bool spazAlive = false;
			if (CalamityGlobalNPC.fireEye != -1)
			{
				spazAlive = Main.npc[CalamityGlobalNPC.fireEye].active;
			}
			Vector2 v = new Vector2(base.NPC.Center.X - Main.player[base.NPC.target].position.X - (float)(Main.player[base.NPC.target].width / 2), base.NPC.position.Y + (float)base.NPC.height - 59f - Main.player[base.NPC.target].position.Y - (float)(Main.player[base.NPC.target].height / 2));
			int direction = ((!(base.NPC.Center.X < Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width)) ? 1 : (-1));
			float hoverRotation = Utils.ToRotation(v) + (float)Math.PI / 2f;
			if (hoverRotation < 0f)
			{
				hoverRotation += (float)Math.PI * 2f;
			}
			else if (hoverRotation > (float)Math.PI * 2f)
			{
				hoverRotation -= (float)Math.PI * 2f;
			}
			float rotationRate = 0.15f;
			base.NPC.rotation = base.NPC.rotation.AngleTowards(hoverRotation, rotationRate);
			if (Main.rand.NextBool(5))
			{
				int retiDust = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y + (float)base.NPC.height * 0.25f), base.NPC.width, (int)((float)base.NPC.height * 0.5f), 5, base.NPC.velocity.X, 2f);
				Dust obj = Main.dust[retiDust];
				obj.velocity.X *= 0.5f;
				obj.velocity.Y *= 0.1f;
			}
			if (Main.netMode != 1 && !Main.player[base.NPC.target].dead && base.NPC.timeLeft < 10)
			{
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					if (i != base.NPC.whoAmI && Main.npc[i].active && (Main.npc[i].type == 125 || Main.npc[i].type == 126) && Main.npc[i].timeLeft - 1 > base.NPC.timeLeft)
					{
						base.NPC.timeLeft = Main.npc[i].timeLeft - 1;
					}
				}
			}
			float phase2LifeRatio = (death ? 0.85f : 0.7f);
			float finalPhaseLifeRatio = (death ? 0.4f : 0.25f);
			float phase1MaxSpeedIncrease = 1f;
			float phase1MaxChargeSpeedIncrease = 1.5f;
			float phase1MaxLaserPhaseDurationDecrease = (death ? 120f : 300f);
			bool phase2 = lifeRatio < phase2LifeRatio;
			bool finalPhase = lifeRatio < finalPhaseLifeRatio;
			Vector2 mechQueenSpacing = Vector2.Zero;
			if (NPC.IsMechQueenUp)
			{
				NPC obj2 = Main.npc[NPC.mechQueen];
				Vector2 mechQueenCenter = obj2.GetMechQueenCenter();
				Vector2 eyePosition = default(Vector2);
				((Vector2)(ref eyePosition))._002Ector(-150f, -250f);
				eyePosition *= 0.75f;
				float mechdusaRotation = obj2.velocity.X * 0.025f;
				mechQueenSpacing = mechQueenCenter + eyePosition;
				mechQueenSpacing = mechQueenSpacing.RotatedBy(mechdusaRotation, mechQueenCenter);
			}
			base.NPC.reflectsProjectiles = false;
			Vector2 val2;
			if ((Main.player[base.NPC.target].dead || Main.IsItDay()) && !BossRushEvent.BossRushActive)
			{
				base.NPC.velocity.Y -= 0.04f;
				if (base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
					return false;
				}
			}
			else if (base.NPC.ai[0] == 0f)
			{
				if (base.NPC.ai[1] == 0f)
				{
					float maxVelocity = (death ? 9.25f : 8.25f);
					float acceleration = (death ? 0.13f : 0.115f);
					if (death)
					{
						maxVelocity += phase1MaxSpeedIncrease * ((1f - lifeRatio) / (1f - phase2LifeRatio));
					}
					if (Main.getGoodWorld)
					{
						maxVelocity *= 1.15f;
						acceleration *= 1.15f;
					}
					float distanceFromTarget = 300f;
					Vector2 val = Main.player[base.NPC.target].Center + Vector2.UnitX * distanceFromTarget * (float)direction - Vector2.UnitY * distanceFromTarget;
					val2 = val - base.NPC.Center;
					float distanceFromDestination = ((Vector2)(ref val2)).Length();
					Vector2 idealVelocity = (val - base.NPC.Center).SafeNormalize(Vector2.UnitX * (float)direction);
					if (NPC.IsMechQueenUp)
					{
						maxVelocity = 14f;
						Vector2 val3 = mechQueenSpacing;
						val2 = val3 - base.NPC.Center;
						distanceFromDestination = ((Vector2)(ref val2)).Length();
						idealVelocity = (val3 - base.NPC.Center).SafeNormalize(Vector2.UnitY);
						if (distanceFromDestination > maxVelocity)
						{
							idealVelocity *= maxVelocity / distanceFromDestination;
						}
						float inertia = 60f;
						base.NPC.velocity = (base.NPC.velocity * (inertia - 1f) + idealVelocity) / inertia;
					}
					else
					{
						base.NPC.SimpleFlyMovement(idealVelocity * maxVelocity, acceleration);
					}
					float phaseGateValue = (death ? (300f - phase1MaxLaserPhaseDurationDecrease * ((1f - lifeRatio) / (1f - phase2LifeRatio))) : 450f);
					float laserGateValue = 30f;
					if (NPC.IsMechQueenUp)
					{
						phaseGateValue = 900f;
						laserGateValue = ((!NPC.npcsFoundForCheckActive[135]) ? 60f : 90f);
					}
					base.NPC.ai[2]++;
					if (base.NPC.ai[2] >= phaseGateValue)
					{
						base.NPC.ai[1] = 1f;
						base.NPC.ai[2] = 0f;
						base.NPC.ai[3] = 0f;
						CalamityTargetingParameters options2 = CalamityTargetingParameters.BossDefaults;
						options2.aggroRatio = -1f;
						base.NPC.CalamityTargeting(options2);
						base.NPC.netUpdate = true;
					}
					else if (distanceFromDestination < (death ? 960f : 800f))
					{
						if (!Main.player[base.NPC.target].dead)
						{
							base.NPC.ai[3]++;
							if (Main.getGoodWorld)
							{
								base.NPC.ai[3] += 0.5f;
							}
						}
						if (base.NPC.ai[3] >= laserGateValue)
						{
							base.NPC.ai[3] = 0f;
							if (Main.netMode != 1)
							{
								float laserSpeed = (death ? 11.25f : 10.5f);
								int type = 83;
								int damage = LaserDamage.CalculateMechDamage();
								Vector2 laserVelocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * laserSpeed;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + laserVelocity.SafeNormalize(Vector2.UnitY) * 90f, laserVelocity, type, damage, 0f, Main.myPlayer);
							}
						}
					}
				}
				else if (base.NPC.ai[1] == 1f)
				{
					base.NPC.rotation = hoverRotation;
					float chargeSpeed = (death ? 17.5f : 15f);
					if (death)
					{
						chargeSpeed += phase1MaxChargeSpeedIncrease * ((1f - lifeRatio) / (1f - phase2LifeRatio));
					}
					if (Main.getGoodWorld)
					{
						chargeSpeed += 2f;
					}
					base.NPC.velocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitX * (float)direction) * chargeSpeed;
					base.NPC.ai[1] = 2f;
				}
				else if (base.NPC.ai[1] == 2f)
				{
					base.NPC.ai[2]++;
					float decelerateGateValue = (death ? 36f : 32f) + (death ? (4f * ((1f - lifeRatio) / (1f - phase2LifeRatio))) : 0f);
					if (base.NPC.ai[2] >= decelerateGateValue)
					{
						float decelerationMultiplier = (death ? 0.84f : 0.92f) - (death ? (0.16f * ((1f - lifeRatio) / (1f - phase2LifeRatio))) : 0f);
						NPC nPC = base.NPC;
						nPC.velocity *= decelerationMultiplier;
						if ((double)Math.Abs(base.NPC.velocity.X) < 0.1)
						{
							base.NPC.velocity.X = 0f;
						}
						if ((double)Math.Abs(base.NPC.velocity.Y) < 0.1)
						{
							base.NPC.velocity.Y = 0f;
						}
					}
					else
					{
						base.NPC.rotation = base.NPC.velocity.ToRotation() - (float)Math.PI / 2f;
					}
					float delayBeforeChargingAgain = (death ? 48f : 56f);
					if (base.NPC.ai[2] >= delayBeforeChargingAgain)
					{
						base.NPC.ai[3]++;
						base.NPC.ai[2] = 0f;
						base.NPC.rotation = hoverRotation;
						float totalCharges = (death ? 6f : 5f);
						if (base.NPC.ai[3] >= totalCharges)
						{
							base.NPC.ai[1] = 0f;
							base.NPC.ai[3] = 0f;
							CalamityTargetingParameters options3 = CalamityTargetingParameters.BossDefaults;
							options3.aggroRatio = -1f;
							base.NPC.CalamityTargeting(options3);
						}
						else
						{
							base.NPC.ai[1] = 1f;
						}
					}
				}
				if (phase2)
				{
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					CalamityTargetingParameters options4 = CalamityTargetingParameters.BossDefaults;
					options4.aggroRatio = -1f;
					base.NPC.CalamityTargeting(options4);
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[0] == 1f || base.NPC.ai[0] == 2f)
			{
				if (NPC.IsMechQueenUp)
				{
					base.NPC.reflectsProjectiles = true;
				}
				if (base.NPC.ai[0] == 1f)
				{
					base.NPC.ai[2] += 0.005f;
					if ((double)base.NPC.ai[2] > 0.5)
					{
						base.NPC.ai[2] = 0.5f;
					}
				}
				else
				{
					base.NPC.ai[2] -= 0.005f;
					if (base.NPC.ai[2] < 0f)
					{
						base.NPC.ai[2] = 0f;
					}
				}
				base.NPC.rotation += base.NPC.ai[2];
				base.NPC.ai[1]++;
				if (death && base.NPC.ai[2] >= 0.2f && base.NPC.ai[1] % 10f == 0f)
				{
					SoundEngine.PlaySound(in SoundID.Item33, base.NPC.Center);
					if (Main.netMode != 1)
					{
						int type2 = 100;
						Vector2 projectileVelocity = base.NPC.rotation.ToRotationVector2() * 7f;
						float offset = 90f;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.Normalize(projectileVelocity) * offset, projectileVelocity, type2, LaserDamage.CalculateMechDamage(), 0f, Main.myPlayer);
					}
				}
				if (base.NPC.ai[1] == 100f)
				{
					base.NPC.ai[0]++;
					base.NPC.ai[1] = 0f;
					if (base.NPC.ai[0] == 3f)
					{
						base.NPC.ai[2] = 0f;
					}
					else
					{
						SoundEngine.PlaySound(in SoundID.NPCHit1, base.NPC.Center);
						if (!Main.dedServ)
						{
							for (int j = 0; j < 2; j++)
							{
								Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 143);
								Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7);
								Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6);
							}
						}
						for (int k = 0; k < 20; k++)
						{
							Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
						}
						SoundEngine.PlaySound(in SoundID.ForceRoar, base.NPC.Center);
					}
				}
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.98f;
				if ((double)Math.Abs(base.NPC.velocity.X) < 0.1)
				{
					base.NPC.velocity.X = 0f;
				}
				if ((double)Math.Abs(base.NPC.velocity.Y) < 0.1)
				{
					base.NPC.velocity.Y = 0f;
				}
			}
			else
			{
				bool spazInPhase1 = false;
				if (CalamityGlobalNPC.fireEye != -1 && Main.npc[CalamityGlobalNPC.fireEye].active)
				{
					spazInPhase1 = Main.npc[CalamityGlobalNPC.fireEye].ai[0] == 1f || Main.npc[CalamityGlobalNPC.fireEye].ai[0] == 2f || Main.npc[CalamityGlobalNPC.fireEye].ai[0] == 0f;
				}
				base.NPC.chaseable = !spazInPhase1;
				base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * RetinazerPhase2ContactDamageMult);
				base.NPC.defense = base.NPC.defDefense + 10;
				calamityGlobalNPC.DR = (spazInPhase1 ? 0.9999f : 0.2f);
				calamityGlobalNPC.unbreakableDR = spazInPhase1;
				calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = spazInPhase1;
				base.NPC.HitSound = SoundID.NPCHit4;
				if (base.NPC.ai[1] == 0f)
				{
					float maxVelocity2 = (death ? (10.65f + 1.5f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 9.5f);
					float acceleration2 = (death ? 0.1975f : 0.175f);
					if (Main.getGoodWorld)
					{
						maxVelocity2 *= 1.15f;
						acceleration2 *= 1.15f;
					}
					float distanceFromTarget2 = 420f;
					Vector2 val4 = Main.player[base.NPC.target].Center - Vector2.UnitY * distanceFromTarget2;
					val2 = val4 - base.NPC.Center;
					float distanceFromDestination2 = ((Vector2)(ref val2)).Length();
					Vector2 idealVelocity2 = (val4 - base.NPC.Center).SafeNormalize(Vector2.UnitX * (float)direction);
					if (NPC.IsMechQueenUp)
					{
						maxVelocity2 = 14f;
						Vector2 val5 = mechQueenSpacing;
						val2 = val5 - base.NPC.Center;
						distanceFromDestination2 = ((Vector2)(ref val2)).Length();
						idealVelocity2 = (val5 - base.NPC.Center).SafeNormalize(Vector2.UnitY);
						if (distanceFromDestination2 > maxVelocity2)
						{
							idealVelocity2 *= maxVelocity2 / distanceFromDestination2;
						}
						float inertia2 = 5f;
						base.NPC.velocity = (base.NPC.velocity * (inertia2 - 1f) + idealVelocity2) / inertia2;
					}
					else
					{
						base.NPC.SimpleFlyMovement(idealVelocity2 * maxVelocity2, acceleration2);
					}
					base.NPC.ai[2] += (spazAlive ? 1f : 1.5f);
					float phaseGateValue2 = (NPC.IsMechQueenUp ? 900f : (300f - (death ? (80f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f)));
					if (base.NPC.ai[2] >= phaseGateValue2)
					{
						base.NPC.ai[1] = 1f;
						base.NPC.ai[2] = 0f;
						base.NPC.ai[3] = 0f;
						CalamityTargetingParameters options5 = CalamityTargetingParameters.BossDefaults;
						options5.aggroRatio = -1f;
						base.NPC.CalamityTargeting(options5);
						base.NPC.netUpdate = true;
					}
					base.NPC.rotation = (Main.player[base.NPC.target].Center - base.NPC.Center).ToRotation() - (float)Math.PI / 2f;
					if (Main.netMode != 1)
					{
						base.NPC.localAI[1] += 1f + (death ? ((phase2LifeRatio - lifeRatio) / phase2LifeRatio / 2f) : 0f);
						if (base.NPC.localAI[1] >= (spazAlive ? 52f : 26f) && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
						{
							base.NPC.localAI[1] = 0f;
							float laserSpeed2 = 10f;
							int type3 = 100;
							Vector2 laserVelocity2 = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * laserSpeed2;
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + laserVelocity2.SafeNormalize(Vector2.UnitY) * 120f, laserVelocity2, type3, RedLaserDamage.CalculateMechDamage(), 0f, Main.myPlayer);
						}
					}
				}
				else if (base.NPC.ai[1] == 1f)
				{
					float maxVelocity3 = (death ? (10.65f + 1.5f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 9.5f);
					float acceleration3 = (death ? 0.295f : 0.25f);
					if (Main.getGoodWorld)
					{
						maxVelocity3 *= 1.15f;
						acceleration3 *= 1.15f;
					}
					float distanceFromTarget3 = 420f;
					Vector2 val6 = Main.player[base.NPC.target].Center + Vector2.UnitX * distanceFromTarget3 * (float)direction;
					val2 = val6 - base.NPC.Center;
					((Vector2)(ref val2)).Length();
					Vector2 idealVelocity3 = (val6 - base.NPC.Center).SafeNormalize(Vector2.UnitX * (float)direction);
					base.NPC.SimpleFlyMovement(idealVelocity3 * maxVelocity3, acceleration3);
					base.NPC.rotation = (Main.player[base.NPC.target].Center - base.NPC.Center).ToRotation() - (float)Math.PI / 2f;
					if (Main.netMode != 1)
					{
						base.NPC.localAI[1] += 1f + (death ? ((phase2LifeRatio - lifeRatio) / phase2LifeRatio / 2f) : 0f);
						if (base.NPC.localAI[1] > (spazAlive ? 20f : 10f) && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
						{
							base.NPC.localAI[1] = 0f;
							float laserSpeed3 = 9f;
							int type4 = 100;
							int damage2 = (int)Math.Round((float)RedLaserDamage.CalculateMechDamage() * RapidFireDamageMult);
							Vector2 laserVelocity3 = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * laserSpeed3;
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + laserVelocity3.SafeNormalize(Vector2.UnitY) * 120f, laserVelocity3, type4, damage2, 0f, Main.myPlayer);
						}
					}
					base.NPC.ai[2] += (spazAlive ? 1f : 1.5f);
					if (base.NPC.ai[2] >= (death ? 150f : 180f) - (death ? (25f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f))
					{
						base.NPC.ai[1] = (finalPhase ? 4f : 0f);
						base.NPC.ai[2] = 0f;
						base.NPC.ai[3] = 0f;
						CalamityTargetingParameters options6 = CalamityTargetingParameters.BossDefaults;
						options6.aggroRatio = -1f;
						base.NPC.CalamityTargeting(options6);
						base.NPC.netUpdate = true;
					}
				}
				else if (base.NPC.ai[1] == 2f)
				{
					base.NPC.rotation = hoverRotation;
					float chargeSpeed2 = (death ? 25f : 22f) + (death ? (3f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f);
					if (!spazAlive)
					{
						chargeSpeed2 += 2f;
					}
					if (Main.getGoodWorld)
					{
						chargeSpeed2 += 2f;
					}
					base.NPC.velocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * chargeSpeed2;
					base.NPC.ai[1] = 3f;
				}
				else if (base.NPC.ai[1] == 3f)
				{
					base.NPC.ai[2]++;
					float chargeTime = (spazAlive ? 45f : 30f);
					if (base.NPC.ai[3] % 3f == 0f)
					{
						chargeTime = (spazAlive ? 90f : 60f);
					}
					if (death)
					{
						chargeTime -= chargeTime * 0.1f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio);
					}
					chargeTime -= chargeTime * (death ? 0.06f : 0f);
					if (base.NPC.ai[2] >= chargeTime)
					{
						NPC nPC3 = base.NPC;
						nPC3.velocity *= 0.93f;
						if ((double)Math.Abs(base.NPC.velocity.X) < 0.1)
						{
							base.NPC.velocity.X = 0f;
						}
						if ((double)Math.Abs(base.NPC.velocity.Y) < 0.1)
						{
							base.NPC.velocity.Y = 0f;
						}
					}
					else
					{
						base.NPC.rotation = base.NPC.velocity.ToRotation() - (float)Math.PI / 2f;
						if (base.NPC.ai[3] % 3f == 0f)
						{
							float fireRate = (spazAlive ? 13f : 9f);
							if (base.NPC.ai[2] % fireRate == 0f)
							{
								SoundEngine.PlaySound(in SoundID.Item33, base.NPC.Center);
								if (Main.netMode != 1)
								{
									float laserDartSpeed = (death ? 7.25f : 6f) * (spazAlive ? 1f : 1.5f);
									int type5 = ModContent.ProjectileType<HomingLaserDart>();
									int damage3 = HomingDartDamage.CalculateMechDamage();
									if (CalamityServerConfig.Instance.EarlyHardmodeProgressionRework && !BossRushEvent.BossRushActive)
									{
										double firstMechMultiplier = 0.9;
										double secondMechMultiplier = 0.95;
										if (!NPC.downedMechBossAny)
										{
											damage3 = (int)((double)damage3 * firstMechMultiplier);
										}
										else if ((!NPC.downedMechBoss1 && !NPC.downedMechBoss2) || (!NPC.downedMechBoss2 && !NPC.downedMechBoss3) || (!NPC.downedMechBoss3 && !NPC.downedMechBoss1))
										{
											damage3 = (int)((double)damage3 * secondMechMultiplier);
										}
									}
									Vector2 laserDartVelocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * laserDartSpeed;
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + base.NPC.velocity.SafeNormalize(Vector2.UnitY) * 60f, laserDartVelocity, type5, damage3, 0f, Main.myPlayer);
								}
							}
						}
					}
					float chargeGateValue = 30f;
					chargeGateValue -= chargeGateValue * (death ? 0.07f : 0f);
					if (base.NPC.ai[2] >= chargeTime + chargeGateValue)
					{
						base.NPC.ai[2] = 0f;
						float chargeIncrement = 1f;
						if (death && Main.rand.NextBool() && base.NPC.ai[3] < (spazAlive ? 1f : 3f))
						{
							chargeIncrement = 2f;
							base.NPC.netUpdate = true;
						}
						base.NPC.rotation = hoverRotation;
						base.NPC.ai[3] += chargeIncrement;
						float maxChargeAmt = (spazAlive ? 2f : 4f);
						if (base.NPC.ai[3] >= maxChargeAmt)
						{
							base.NPC.ai[1] = 0f;
							base.NPC.ai[3] = 0f;
							CalamityTargetingParameters options7 = CalamityTargetingParameters.BossDefaults;
							options7.aggroRatio = -1f;
							base.NPC.CalamityTargeting(options7);
						}
						else
						{
							base.NPC.ai[1] = 4f;
						}
					}
				}
				else if (base.NPC.ai[1] == 4f)
				{
					float chargeLineUpDistance = (spazAlive ? 600f : 500f);
					float chargeSpeed3 = (death ? 20f : 18f) + (death ? (1.5f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f);
					float chargeAcceleration = (death ? 0.495f : 0.45f);
					if (spazAlive)
					{
						chargeSpeed3 *= 0.75f;
						chargeAcceleration *= 0.75f;
					}
					if (Main.getGoodWorld)
					{
						chargeSpeed3 *= 1.15f;
						chargeAcceleration *= 1.15f;
					}
					Vector2 idealVelocity4 = (Main.player[base.NPC.target].Center + Vector2.UnitX * chargeLineUpDistance * (float)direction - base.NPC.Center).SafeNormalize(Vector2.UnitX * (float)direction) * chargeSpeed3;
					base.NPC.SimpleFlyMovement(idealVelocity4, chargeAcceleration);
					base.NPC.ai[2]++;
					if (base.NPC.ai[2] >= (spazAlive ? 75f : 60f) - (death ? (5f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f))
					{
						base.NPC.ai[1] = 2f;
						base.NPC.ai[2] = 0f;
						base.NPC.netUpdate = true;
					}
				}
			}
			return false;
		}
	}

	public class SpazmatismAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_016d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0178: Unknown result type (might be due to invalid IL or missing references)
			//IL_0294: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_042e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0433: Unknown result type (might be due to invalid IL or missing references)
			//IL_0448: Unknown result type (might be due to invalid IL or missing references)
			//IL_044d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0460: Unknown result type (might be due to invalid IL or missing references)
			//IL_0467: Unknown result type (might be due to invalid IL or missing references)
			//IL_046c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0480: Unknown result type (might be due to invalid IL or missing references)
			//IL_0482: Unknown result type (might be due to invalid IL or missing references)
			//IL_0484: Unknown result type (might be due to invalid IL or missing references)
			//IL_0489: Unknown result type (might be due to invalid IL or missing references)
			//IL_048b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0490: Unknown result type (might be due to invalid IL or missing references)
			//IL_0492: Unknown result type (might be due to invalid IL or missing references)
			//IL_0497: Unknown result type (might be due to invalid IL or missing references)
			//IL_0931: Unknown result type (might be due to invalid IL or missing references)
			//IL_093c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0941: Unknown result type (might be due to invalid IL or missing references)
			//IL_0946: Unknown result type (might be due to invalid IL or missing references)
			//IL_094e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0953: Unknown result type (might be due to invalid IL or missing references)
			//IL_095a: Unknown result type (might be due to invalid IL or missing references)
			//IL_095f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1040: Unknown result type (might be due to invalid IL or missing references)
			//IL_1089: Unknown result type (might be due to invalid IL or missing references)
			//IL_108f: Unknown result type (might be due to invalid IL or missing references)
			//IL_10a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_10ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_10b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a66: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e80: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1809: Unknown result type (might be due to invalid IL or missing references)
			//IL_1814: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_05af: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0612: Unknown result type (might be due to invalid IL or missing references)
			//IL_0614: Unknown result type (might be due to invalid IL or missing references)
			//IL_061b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0620: Unknown result type (might be due to invalid IL or missing references)
			//IL_0625: Unknown result type (might be due to invalid IL or missing references)
			//IL_0636: Unknown result type (might be due to invalid IL or missing references)
			//IL_063b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0640: Unknown result type (might be due to invalid IL or missing references)
			//IL_0645: Unknown result type (might be due to invalid IL or missing references)
			//IL_064a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0673: Unknown result type (might be due to invalid IL or missing references)
			//IL_0680: Unknown result type (might be due to invalid IL or missing references)
			//IL_0685: Unknown result type (might be due to invalid IL or missing references)
			//IL_0687: Unknown result type (might be due to invalid IL or missing references)
			//IL_068e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0693: Unknown result type (might be due to invalid IL or missing references)
			//IL_0652: Unknown result type (might be due to invalid IL or missing references)
			//IL_0659: Unknown result type (might be due to invalid IL or missing references)
			//IL_065e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fb6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fff: Unknown result type (might be due to invalid IL or missing references)
			//IL_1005: Unknown result type (might be due to invalid IL or missing references)
			//IL_1029: Unknown result type (might be due to invalid IL or missing references)
			//IL_1034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0eb5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ee4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f0b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f3a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f5d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d55: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d60: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d65: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d6f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d79: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d8d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d92: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d97: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dab: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dc1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dc6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dcb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1874: Unknown result type (might be due to invalid IL or missing references)
			//IL_187f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1884: Unknown result type (might be due to invalid IL or missing references)
			//IL_1889: Unknown result type (might be due to invalid IL or missing references)
			//IL_1891: Unknown result type (might be due to invalid IL or missing references)
			//IL_1896: Unknown result type (might be due to invalid IL or missing references)
			//IL_189d: Unknown result type (might be due to invalid IL or missing references)
			//IL_18a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_128d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1292: Unknown result type (might be due to invalid IL or missing references)
			//IL_1299: Unknown result type (might be due to invalid IL or missing references)
			//IL_12a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_12a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_12ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_12b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_12b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_12bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_12cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_12d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_12d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_12df: Unknown result type (might be due to invalid IL or missing references)
			//IL_12e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_12e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e96: Unknown result type (might be due to invalid IL or missing references)
			//IL_19da: Unknown result type (might be due to invalid IL or missing references)
			//IL_1347: Unknown result type (might be due to invalid IL or missing references)
			//IL_134b: Unknown result type (might be due to invalid IL or missing references)
			//IL_194f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1956: Unknown result type (might be due to invalid IL or missing references)
			//IL_195b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f2f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f45: Unknown result type (might be due to invalid IL or missing references)
			//IL_140c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1438: Unknown result type (might be due to invalid IL or missing references)
			//IL_2023: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f95: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f9f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fa4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0802: Unknown result type (might be due to invalid IL or missing references)
			//IL_080d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0812: Unknown result type (might be due to invalid IL or missing references)
			//IL_0817: Unknown result type (might be due to invalid IL or missing references)
			//IL_081c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0823: Unknown result type (might be due to invalid IL or missing references)
			//IL_0846: Unknown result type (might be due to invalid IL or missing references)
			//IL_0850: Unknown result type (might be due to invalid IL or missing references)
			//IL_0855: Unknown result type (might be due to invalid IL or missing references)
			//IL_085a: Unknown result type (might be due to invalid IL or missing references)
			//IL_086e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0873: Unknown result type (might be due to invalid IL or missing references)
			//IL_0875: Unknown result type (might be due to invalid IL or missing references)
			//IL_087a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0884: Unknown result type (might be due to invalid IL or missing references)
			//IL_0889: Unknown result type (might be due to invalid IL or missing references)
			//IL_088e: Unknown result type (might be due to invalid IL or missing references)
			//IL_175c: Unknown result type (might be due to invalid IL or missing references)
			//IL_175e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1765: Unknown result type (might be due to invalid IL or missing references)
			//IL_176a: Unknown result type (might be due to invalid IL or missing references)
			//IL_176f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1780: Unknown result type (might be due to invalid IL or missing references)
			//IL_1785: Unknown result type (might be due to invalid IL or missing references)
			//IL_178a: Unknown result type (might be due to invalid IL or missing references)
			//IL_178f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1794: Unknown result type (might be due to invalid IL or missing references)
			//IL_14be: Unknown result type (might be due to invalid IL or missing references)
			//IL_14c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_17bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_17ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_17cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_17d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_17d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_17dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_179c: Unknown result type (might be due to invalid IL or missing references)
			//IL_17a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_17a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bd2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bdd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1be2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1be7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bef: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bf1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bf6: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bfd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c02: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c24: Unknown result type (might be due to invalid IL or missing references)
			//IL_219a: Unknown result type (might be due to invalid IL or missing references)
			//IL_219f: Unknown result type (might be due to invalid IL or missing references)
			//IL_21a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_21ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_21b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_21bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_21c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_21c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_21cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_21d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_21d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_21e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c9b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cb6: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d60: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d6b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d70: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d75: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d7a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d81: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d86: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cd2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ced: Unknown result type (might be due to invalid IL or missing references)
			//IL_15b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_15c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_15c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_15ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_15d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_15da: Unknown result type (might be due to invalid IL or missing references)
			//IL_15e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_15ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_15f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_15f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d0d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d12: Unknown result type (might be due to invalid IL or missing references)
			//IL_164d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1652: Unknown result type (might be due to invalid IL or missing references)
			//IL_1654: Unknown result type (might be due to invalid IL or missing references)
			//IL_1659: Unknown result type (might be due to invalid IL or missing references)
			//IL_1663: Unknown result type (might be due to invalid IL or missing references)
			//IL_1668: Unknown result type (might be due to invalid IL or missing references)
			//IL_166d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1613: Unknown result type (might be due to invalid IL or missing references)
			//IL_161a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1625: Unknown result type (might be due to invalid IL or missing references)
			//IL_162f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1634: Unknown result type (might be due to invalid IL or missing references)
			//IL_1639: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d9d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1db5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dbb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dbd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dc2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dd6: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ddb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ddd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1de2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dec: Unknown result type (might be due to invalid IL or missing references)
			//IL_1df1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1df6: Unknown result type (might be due to invalid IL or missing references)
			//IL_2291: Unknown result type (might be due to invalid IL or missing references)
			//IL_229c: Unknown result type (might be due to invalid IL or missing references)
			//IL_22a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_22a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_22ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_22b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_22b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_22cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_22d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_22d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_22d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_22e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_22e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_22eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_16f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_16f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_16fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1700: Unknown result type (might be due to invalid IL or missing references)
			//IL_170a: Unknown result type (might be due to invalid IL or missing references)
			//IL_170f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1714: Unknown result type (might be due to invalid IL or missing references)
			//IL_171b: Unknown result type (might be due to invalid IL or missing references)
			CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				CalamityTargetingParameters options = CalamityTargetingParameters.BossDefaults;
				options.finishThemOff = true;
				base.NPC.CalamityTargeting(options);
			}
			float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
			CalamityGlobalNPC.fireEye = base.NPC.whoAmI;
			bool retAlive = false;
			if (CalamityGlobalNPC.laserEye != -1)
			{
				retAlive = Main.npc[CalamityGlobalNPC.laserEye].active;
			}
			Vector2 v = new Vector2(base.NPC.Center.X - Main.player[base.NPC.target].position.X - (float)(Main.player[base.NPC.target].width / 2), base.NPC.position.Y + (float)base.NPC.height - 59f - Main.player[base.NPC.target].position.Y - (float)(Main.player[base.NPC.target].height / 2));
			int direction = ((!(base.NPC.Center.X < Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width)) ? 1 : (-1));
			float hoverRotation = Utils.ToRotation(v) + (float)Math.PI / 2f;
			if (hoverRotation < 0f)
			{
				hoverRotation += (float)Math.PI * 2f;
			}
			else if (hoverRotation > (float)Math.PI * 2f)
			{
				hoverRotation -= (float)Math.PI * 2f;
			}
			float rotationRate = 0.15f;
			if (NPC.IsMechQueenUp && base.NPC.ai[0] == 3f && base.NPC.ai[1] == 0f)
			{
				rotationRate *= 0.25f;
			}
			base.NPC.rotation = base.NPC.rotation.AngleTowards(hoverRotation, rotationRate);
			if (Main.rand.NextBool(5))
			{
				int spazDust = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y + (float)base.NPC.height * 0.25f), base.NPC.width, (int)((float)base.NPC.height * 0.5f), 5, base.NPC.velocity.X, 2f);
				Dust obj = Main.dust[spazDust];
				obj.velocity.X *= 0.5f;
				obj.velocity.Y *= 0.1f;
			}
			if (Main.netMode != 1 && !Main.player[base.NPC.target].dead && base.NPC.timeLeft < 10)
			{
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					if (i != base.NPC.whoAmI && Main.npc[i].active && (Main.npc[i].type == 125 || Main.npc[i].type == 126) && Main.npc[i].timeLeft - 1 > base.NPC.timeLeft)
					{
						base.NPC.timeLeft = Main.npc[i].timeLeft - 1;
					}
				}
			}
			float phase2LifeRatio = (death ? 0.85f : 0.7f);
			float finalPhaseLifeRatio = (death ? 0.4f : 0.25f);
			float phase1MaxSpeedIncrease = 1.125f;
			float phase1MaxChargeSpeedIncrease = 1.4f;
			float phase1MaxCursedFlamePhaseDurationDecrease = (death ? 80f : 200f);
			float phase1MaxChargesDecrease = 2f;
			bool phase2 = lifeRatio < phase2LifeRatio;
			bool finalPhase = lifeRatio < finalPhaseLifeRatio;
			Vector2 mechQueenSpacing = Vector2.Zero;
			if (NPC.IsMechQueenUp)
			{
				NPC obj2 = Main.npc[NPC.mechQueen];
				Vector2 mechQueenCenter2 = obj2.GetMechQueenCenter();
				Vector2 mechdusaSpacingVector = default(Vector2);
				((Vector2)(ref mechdusaSpacingVector))._002Ector(150f, -250f);
				mechdusaSpacingVector *= 0.75f;
				float mechdusaSpacingVel = obj2.velocity.X * 0.025f;
				mechQueenSpacing = mechQueenCenter2 + mechdusaSpacingVector;
				mechQueenSpacing = mechQueenSpacing.RotatedBy(mechdusaSpacingVel, mechQueenCenter2);
			}
			base.NPC.reflectsProjectiles = false;
			Vector2 center;
			if ((Main.player[base.NPC.target].dead || Main.IsItDay()) && !BossRushEvent.BossRushActive)
			{
				base.NPC.velocity.Y -= 0.04f;
				if (base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
					return false;
				}
			}
			else if (base.NPC.ai[0] == 0f)
			{
				if (base.NPC.ai[1] == 0f)
				{
					float maxVelocity = (death ? 13.5f : 12f);
					float acceleration = (death ? 0.46f : 0.4f);
					if (death)
					{
						maxVelocity += phase1MaxSpeedIncrease * ((1f - lifeRatio) / (1f - phase2LifeRatio));
					}
					if (Main.getGoodWorld)
					{
						maxVelocity *= 1.15f;
						acceleration *= 1.15f;
					}
					float distanceFromTarget = 400f;
					Vector2 val = Main.player[base.NPC.target].Center + Vector2.UnitX * distanceFromTarget * (float)direction;
					center = val - base.NPC.Center;
					float distanceFromDestination = ((Vector2)(ref center)).Length();
					Vector2 idealVelocity = (val - base.NPC.Center).SafeNormalize(Vector2.UnitX * (float)direction);
					if (NPC.IsMechQueenUp)
					{
						maxVelocity = 14f;
						Vector2 val2 = mechQueenSpacing;
						center = val2 - base.NPC.Center;
						distanceFromDestination = ((Vector2)(ref center)).Length();
						idealVelocity = (val2 - base.NPC.Center).SafeNormalize(Vector2.UnitY);
						if (distanceFromDestination > maxVelocity)
						{
							idealVelocity *= maxVelocity / distanceFromDestination;
						}
						float inertia = 5f;
						base.NPC.velocity = (base.NPC.velocity * (inertia - 1f) + idealVelocity) / inertia;
					}
					else
					{
						base.NPC.SimpleFlyMovement(idealVelocity * maxVelocity, acceleration);
					}
					base.NPC.ai[2]++;
					float phaseGateValue = (NPC.IsMechQueenUp ? 900f : (300f - (death ? (phase1MaxCursedFlamePhaseDurationDecrease * ((1f - lifeRatio) / (1f - phase2LifeRatio))) : 0f)));
					if (base.NPC.ai[2] >= phaseGateValue)
					{
						base.NPC.ai[1] = 1f;
						base.NPC.ai[2] = 0f;
						base.NPC.ai[3] = 0f;
						base.NPC.netUpdate = true;
					}
					else
					{
						if (!Main.player[base.NPC.target].dead)
						{
							base.NPC.ai[3]++;
							if (Main.getGoodWorld)
							{
								base.NPC.ai[3] += 0.4f;
							}
						}
						if (base.NPC.ai[3] >= 30f)
						{
							base.NPC.ai[3] = 0f;
							if (Main.netMode != 1)
							{
								float cursedFireballSpeed = (death ? 15.9f : 15f);
								int type = 96;
								Vector2 fireballVelocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * cursedFireballSpeed + new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11)) * 0.05f;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + fireballVelocity.SafeNormalize(Vector2.UnitY) * 50f, fireballVelocity, type, FireballDamage.CalculateMechDamage(), 0f, Main.myPlayer);
							}
						}
					}
				}
				else if (base.NPC.ai[1] == 1f)
				{
					base.NPC.rotation = hoverRotation;
					float chargeSpeed = (death ? (19.25f + phase1MaxChargeSpeedIncrease * ((1f - lifeRatio) / (1f - phase2LifeRatio))) : 18f);
					if (Main.getGoodWorld)
					{
						chargeSpeed *= 1.2f;
					}
					base.NPC.velocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitX * (float)direction) * chargeSpeed;
					base.NPC.ai[1] = 2f;
				}
				else if (base.NPC.ai[1] == 2f)
				{
					base.NPC.ai[2]++;
					float timeBeforeSlowDown = (death ? 14f : 10f);
					if (base.NPC.ai[2] >= timeBeforeSlowDown)
					{
						NPC nPC = base.NPC;
						nPC.velocity *= 0.8f;
						if ((double)Math.Abs(base.NPC.velocity.X) < 0.1)
						{
							base.NPC.velocity.X = 0f;
						}
						if ((double)Math.Abs(base.NPC.velocity.Y) < 0.1)
						{
							base.NPC.velocity.Y = 0f;
						}
					}
					else
					{
						base.NPC.rotation = base.NPC.velocity.ToRotation() - (float)Math.PI / 2f;
					}
					float chargeTime = (death ? 35f : 25f);
					if (base.NPC.ai[2] >= chargeTime)
					{
						base.NPC.ai[3]++;
						base.NPC.ai[2] = 0f;
						base.NPC.rotation = hoverRotation;
						float totalCharges = 8f;
						if (death)
						{
							totalCharges -= (float)Math.Round(phase1MaxChargesDecrease * ((1f - lifeRatio) / (1f - phase2LifeRatio)));
						}
						if (base.NPC.ai[3] >= totalCharges)
						{
							base.NPC.ai[1] = 0f;
							base.NPC.ai[3] = 0f;
						}
						else
						{
							base.NPC.ai[1] = 1f;
						}
					}
				}
				if (phase2)
				{
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					CalamityTargetingParameters options2 = CalamityTargetingParameters.BossDefaults;
					options2.finishThemOff = true;
					base.NPC.CalamityTargeting(options2);
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[0] == 1f || base.NPC.ai[0] == 2f)
			{
				if (NPC.IsMechQueenUp)
				{
					base.NPC.reflectsProjectiles = true;
				}
				if (base.NPC.ai[0] == 1f)
				{
					base.NPC.ai[2] += 0.005f;
					if ((double)base.NPC.ai[2] > 0.5)
					{
						base.NPC.ai[2] = 0.5f;
					}
				}
				else
				{
					base.NPC.ai[2] -= 0.005f;
					if (base.NPC.ai[2] < 0f)
					{
						base.NPC.ai[2] = 0f;
					}
				}
				base.NPC.rotation += base.NPC.ai[2];
				base.NPC.ai[1]++;
				if (death && base.NPC.ai[2] >= 0.2f && base.NPC.ai[1] % 10f == 0f && Main.netMode != 1)
				{
					int type2 = ((base.NPC.ai[1] % 20f == 0f) ? 96 : ModContent.ProjectileType<ShadowflameFireball>());
					Vector2 projectileVelocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * 16f + Main.rand.NextVector2CircularEdge(3f, 3f);
					int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + projectileVelocity.SafeNormalize(Vector2.UnitY) * 50f, projectileVelocity, type2, FireballDamage.CalculateMechDamage(), 0f, Main.myPlayer, 0f, 1f);
					Main.projectile[proj].tileCollide = false;
				}
				if (base.NPC.ai[1] == 100f)
				{
					base.NPC.ai[0]++;
					base.NPC.ai[1] = 0f;
					if (base.NPC.ai[0] == 3f)
					{
						base.NPC.ai[2] = 0f;
					}
					else
					{
						SoundEngine.PlaySound(in SoundID.NPCHit1, base.NPC.Center);
						if (!Main.dedServ)
						{
							for (int j = 0; j < 2; j++)
							{
								Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 144);
								Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7);
								Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6);
							}
						}
						for (int k = 0; k < 20; k++)
						{
							Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
						}
						SoundEngine.PlaySound(in SoundID.ForceRoar, base.NPC.Center);
					}
				}
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.98f;
				if ((double)Math.Abs(base.NPC.velocity.X) < 0.1)
				{
					base.NPC.velocity.X = 0f;
				}
				if ((double)Math.Abs(base.NPC.velocity.Y) < 0.1)
				{
					base.NPC.velocity.Y = 0f;
				}
			}
			else
			{
				bool retInPhase1 = false;
				if (CalamityGlobalNPC.laserEye != -1 && Main.npc[CalamityGlobalNPC.laserEye].active)
				{
					retInPhase1 = Main.npc[CalamityGlobalNPC.laserEye].ai[0] == 1f || Main.npc[CalamityGlobalNPC.laserEye].ai[0] == 2f || Main.npc[CalamityGlobalNPC.laserEye].ai[0] == 0f;
				}
				base.NPC.chaseable = !retInPhase1;
				base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * SpazmatismPhase2ContactDamageMult);
				base.NPC.defense = base.NPC.defDefense + 18;
				calamityGlobalNPC.DR = (retInPhase1 ? 0.9999f : 0.2f);
				calamityGlobalNPC.unbreakableDR = retInPhase1;
				calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = retInPhase1;
				base.NPC.HitSound = SoundID.NPCHit4;
				if (base.NPC.ai[1] == 0f)
				{
					float maxVelocity2 = 7.1f + (death ? (0.7f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f);
					float acceleration2 = 0.118f + (death ? (0.0175f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f);
					float distanceFromTarget2 = 180f;
					Vector2 val3 = Main.player[base.NPC.target].Center + Vector2.UnitX * distanceFromTarget2 * (float)direction;
					center = val3 - base.NPC.Center;
					float distanceFromDestination2 = ((Vector2)(ref center)).Length();
					Vector2 idealVelocity2 = (val3 - base.NPC.Center).SafeNormalize(Vector2.UnitX * (float)direction);
					if (!NPC.IsMechQueenUp)
					{
						if (distanceFromDestination2 > distanceFromTarget2)
						{
							maxVelocity2 += MathHelper.Lerp(0f, 6f, MathHelper.Clamp((distanceFromDestination2 - distanceFromTarget2) / 1000f, 0f, 1f));
						}
						if (Main.getGoodWorld)
						{
							maxVelocity2 *= 1.15f;
							acceleration2 *= 1.15f;
						}
						base.NPC.SimpleFlyMovement(idealVelocity2 * maxVelocity2, acceleration2);
					}
					base.NPC.ai[2] += (retAlive ? 1f : 2f);
					float phaseGateValue2 = (NPC.IsMechQueenUp ? 900f : (180f - (death ? (30f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f)));
					if (base.NPC.ai[2] >= phaseGateValue2)
					{
						base.NPC.ai[1] = (finalPhase ? 5f : 1f);
						base.NPC.ai[2] = 0f;
						base.NPC.ai[3] = 0f;
						base.NPC.netUpdate = true;
					}
					if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						base.NPC.localAI[2]++;
						if (base.NPC.localAI[2] > 22f)
						{
							base.NPC.localAI[2] = 0f;
							SoundEngine.PlaySound(in SoundID.Item34, base.NPC.Center);
						}
						if (Main.netMode != 1)
						{
							base.NPC.localAI[1]++;
							if (base.NPC.localAI[1] > 2f)
							{
								base.NPC.ai[3]++;
								base.NPC.localAI[1] = 0f;
								float flamethrowerSpeed = (death ? 6.9f : 6f);
								float timeForFlamethrowerToReachMaxVelocity = 60f;
								float flamethrowerSpeedScalar = MathHelper.Clamp(base.NPC.ai[2] / timeForFlamethrowerToReachMaxVelocity, 0f, 1f);
								flamethrowerSpeed = MathHelper.Lerp(0.1f, flamethrowerSpeed, flamethrowerSpeedScalar);
								int type3 = ((base.NPC.ai[3] % 2f == 0f) ? ModContent.ProjectileType<CursedFire>() : ModContent.ProjectileType<Shadowflamethrower>());
								Vector2 flamethrowerVelocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * flamethrowerSpeed + base.NPC.velocity * 0.5f;
								if (NPC.IsMechQueenUp)
								{
									flamethrowerVelocity = (base.NPC.rotation + (float)Math.PI / 2f).ToRotationVector2() * flamethrowerSpeed + base.NPC.velocity * 0.5f;
								}
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + flamethrowerVelocity.SafeNormalize(Vector2.UnitY) * 25f, flamethrowerVelocity, type3, FlamethrowerDamage.CalculateMechDamage(), 0f, Main.myPlayer);
								if (death && base.NPC.ai[3] % 30f == 0f)
								{
									type3 = ((base.NPC.ai[3] % 60f == 0f) ? ModContent.ProjectileType<ShadowflameFireball>() : 96);
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + flamethrowerVelocity.SafeNormalize(Vector2.UnitY) * 50f, flamethrowerVelocity * 2f, type3, FireballDamage.CalculateMechDamage(), 0f, Main.myPlayer);
								}
							}
						}
					}
					if (NPC.IsMechQueenUp)
					{
						maxVelocity2 = 14f;
						Vector2 val4 = mechQueenSpacing;
						center = val4 - base.NPC.Center;
						distanceFromDestination2 = ((Vector2)(ref center)).Length();
						idealVelocity2 = (val4 - base.NPC.Center).SafeNormalize(Vector2.UnitY);
						if (distanceFromDestination2 > maxVelocity2)
						{
							idealVelocity2 *= maxVelocity2 / distanceFromDestination2;
						}
						float inertia2 = 60f;
						base.NPC.velocity = (base.NPC.velocity * (inertia2 - 1f) + idealVelocity2) / inertia2;
					}
				}
				else
				{
					if (base.NPC.ai[1] == 1f)
					{
						SoundEngine.PlaySound(in SoundID.ForceRoar, base.NPC.Center);
						base.NPC.rotation = hoverRotation;
						float chargeSpeed2 = (death ? (19.25f + phase1MaxChargeSpeedIncrease * ((1f - lifeRatio) / (1f - phase2LifeRatio))) : 18f);
						if (Main.getGoodWorld)
						{
							chargeSpeed2 *= 1.2f;
						}
						base.NPC.velocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitX * (float)direction) * chargeSpeed2;
						base.NPC.ai[1] = 2f;
						return false;
					}
					if (base.NPC.ai[1] == 2f)
					{
						base.NPC.ai[2] += (retAlive ? 1f : 1.25f);
						float chargeTime2 = 30f - (death ? (3f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f);
						if (base.NPC.ai[2] >= chargeTime2)
						{
							float deceleration = 0.85f - (death ? (0.1f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f);
							NPC nPC3 = base.NPC;
							nPC3.velocity *= deceleration;
							if ((double)Math.Abs(base.NPC.velocity.X) < 0.1)
							{
								base.NPC.velocity.X = 0f;
							}
							if ((double)Math.Abs(base.NPC.velocity.Y) < 0.1)
							{
								base.NPC.velocity.Y = 0f;
							}
						}
						else
						{
							base.NPC.rotation = base.NPC.velocity.ToRotation() - (float)Math.PI / 2f;
						}
						if (base.NPC.ai[2] >= chargeTime2 * 1.6f)
						{
							base.NPC.ai[3]++;
							base.NPC.ai[2] = 0f;
							base.NPC.rotation = hoverRotation;
							float totalCharges2 = 5f;
							if (base.NPC.ai[3] >= totalCharges2)
							{
								base.NPC.ai[1] = 0f;
								base.NPC.ai[3] = 0f;
								return false;
							}
							base.NPC.ai[1] = 1f;
						}
					}
					else if (base.NPC.ai[1] == 3f)
					{
						float secondFastCharge = 4f;
						if (base.NPC.ai[3] >= (retAlive ? secondFastCharge : (secondFastCharge + 1f)))
						{
							base.NPC.ai[1] = (retAlive ? 0f : 5f);
							base.NPC.ai[2] = 0f;
							base.NPC.ai[3] = 0f;
							if (base.NPC.ai[1] == 0f)
							{
								base.NPC.localAI[1] = -20f;
							}
							base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
						}
						else if (Main.netMode != 1)
						{
							float spazmatismPhase3ChargeSpeed = (death ? 22.5f : 20f) + (death ? (3f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f);
							if (base.NPC.ai[2] == -1f || (!retAlive && base.NPC.ai[3] == secondFastCharge))
							{
								spazmatismPhase3ChargeSpeed *= 1.3f;
							}
							if (Main.getGoodWorld)
							{
								spazmatismPhase3ChargeSpeed *= 1.2f;
							}
							Vector2 distanceVector = Main.player[base.NPC.target].Center - base.NPC.Center;
							base.NPC.velocity = distanceVector.SafeNormalize(Vector2.UnitY) * spazmatismPhase3ChargeSpeed;
							if (retAlive && base.NPC.Distance(Main.player[base.NPC.target].Center) < 100f && Math.Abs(base.NPC.velocity.X) > Math.Abs(base.NPC.velocity.Y))
							{
								float absoluteSpazXVel = Math.Abs(base.NPC.velocity.X);
								float absoluteSpazYVel = Math.Abs(base.NPC.velocity.Y);
								if (base.NPC.Center.X > Main.player[base.NPC.target].Center.X)
								{
									absoluteSpazYVel *= -1f;
								}
								if (base.NPC.Center.Y > Main.player[base.NPC.target].Center.Y)
								{
									absoluteSpazXVel *= -1f;
								}
								base.NPC.velocity = new Vector2(absoluteSpazYVel, absoluteSpazXVel);
							}
							if (death)
							{
								float projectileSpeed = spazmatismPhase3ChargeSpeed * 0.5f;
								int type4 = ((!retAlive && base.NPC.ai[3] % 2f == 0f) ? ModContent.ProjectileType<ShadowflameFireball>() : 96);
								Vector2 projectileVelocity2 = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * projectileSpeed;
								int numProj = 3;
								float rotation = MathHelper.ToRadians(15f);
								for (int l = 0; l < numProj; l++)
								{
									double radians = MathHelper.Lerp(0f - rotation, rotation, (float)l / (float)(numProj - 1));
									center = default(Vector2);
									Vector2 perturbedSpeed = projectileVelocity2.RotatedBy(radians, center);
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + perturbedSpeed.SafeNormalize(Vector2.UnitY) * 25f, perturbedSpeed, type4, FireballDamage.CalculateMechDamage(), 0f, Main.myPlayer);
								}
							}
							base.NPC.ai[1] = 4f;
							base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
						}
					}
					else if (base.NPC.ai[1] == 4f)
					{
						if (base.NPC.ai[2] == 0f)
						{
							SoundEngine.PlaySound(in SoundID.ForceRoar, base.NPC.Center);
						}
						float spazmatismRetDeadChargeSpeed = ((!retAlive && base.NPC.ai[3] == 4f) ? 75f : 50f) - (float)Math.Round(death ? (((!retAlive && base.NPC.ai[3] == 4f) ? 15f : 10f) * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f);
						base.NPC.ai[2]++;
						if (base.NPC.ai[2] == spazmatismRetDeadChargeSpeed && Vector2.Distance(base.NPC.position, Main.player[base.NPC.target].position) < (retAlive ? 200f : 150f))
						{
							base.NPC.ai[2]--;
						}
						if (base.NPC.ai[2] >= spazmatismRetDeadChargeSpeed)
						{
							NPC nPC4 = base.NPC;
							nPC4.velocity *= 0.93f;
							if ((double)Math.Abs(base.NPC.velocity.X) < 0.1)
							{
								base.NPC.velocity.X = 0f;
							}
							if ((double)Math.Abs(base.NPC.velocity.Y) < 0.1)
							{
								base.NPC.velocity.Y = 0f;
							}
						}
						else
						{
							base.NPC.rotation = base.NPC.velocity.ToRotation() - (float)Math.PI / 2f;
						}
						float spazmatismRetDeadChargeTimer = spazmatismRetDeadChargeSpeed + 25f;
						if (base.NPC.ai[2] >= spazmatismRetDeadChargeTimer)
						{
							base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
							float chargeIncrement = 1f;
							if (death && Main.rand.NextBool() && base.NPC.ai[3] < (retAlive ? 2f : 3f))
							{
								chargeIncrement = 2f;
							}
							base.NPC.ai[3] += chargeIncrement;
							base.NPC.ai[2] = 0f;
							base.NPC.ai[1] = 3f;
						}
					}
					else if (base.NPC.ai[1] == 5f)
					{
						float chargeLineUpDistance = (retAlive ? 600f : 500f);
						float chargeSpeed3 = (death ? 17.5f : 16f) + (death ? (5f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f);
						float chargeAcceleration = (death ? 0.44f : 0.4f) + (death ? (0.1f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f);
						if (retAlive)
						{
							chargeSpeed3 *= 0.75f;
							chargeAcceleration *= 0.75f;
						}
						if (Main.getGoodWorld)
						{
							chargeSpeed3 *= 1.15f;
							chargeAcceleration *= 1.15f;
						}
						Vector2 idealVelocity3 = (Main.player[base.NPC.target].Center + Vector2.UnitY * chargeLineUpDistance - base.NPC.Center).SafeNormalize(Vector2.UnitX * (float)direction) * chargeSpeed3;
						base.NPC.SimpleFlyMovement(idealVelocity3, chargeAcceleration);
						base.NPC.ai[2]++;
						float fireRate = (retAlive ? 30f : 20f);
						if (base.NPC.ai[2] % fireRate == 0f)
						{
							base.NPC.ai[3]++;
							if (Main.netMode != 1)
							{
								float projectileSpeed2 = 16f;
								int type5 = ((base.NPC.ai[3] % 2f == 0f) ? 96 : ModContent.ProjectileType<ShadowflameFireball>());
								Vector2 projectileVelocity3 = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * projectileSpeed2;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + projectileVelocity3.SafeNormalize(Vector2.UnitY) * 25f, projectileVelocity3, type5, FireballDamage.CalculateMechDamage(), 0f, Main.myPlayer, 0f, retAlive ? 0f : 1f);
							}
						}
						if (base.NPC.ai[2] >= (retAlive ? 180f : 135f) - (death ? (45f * ((phase2LifeRatio - lifeRatio) / phase2LifeRatio)) : 0f))
						{
							base.NPC.ai[1] = 3f;
							base.NPC.ai[2] = -1f;
							base.NPC.ai[3] = 0f;
						}
						base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
				}
			}
			return false;
		}
	}

	public static float RapidFireDamageMult = 0.75f;

	public static float RetinazerPhase2ContactDamageMult = 1.5f;

	public static float SpazmatismPhase2ContactDamageMult = 1.5f;

	public static int FlamethrowerDamage = 27;

	public static int FireballDamage = 22;

	public static int LaserDamage = 21;

	public static int RedLaserDamage = 27;

	public static int HomingDartDamage = 27;

	public static int CalculateMechDamage(this int damage)
	{
		if (CalamityServerConfig.Instance.EarlyHardmodeProgressionRework && !BossRushEvent.BossRushActive)
		{
			double firstMechMultiplier = 0.9;
			double secondMechMultiplier = 0.95;
			if (!NPC.downedMechBossAny)
			{
				damage = (int)((double)damage * firstMechMultiplier);
			}
			else if ((!NPC.downedMechBoss1 && !NPC.downedMechBoss2) || (!NPC.downedMechBoss2 && !NPC.downedMechBoss3) || (!NPC.downedMechBoss3 && !NPC.downedMechBoss1))
			{
				damage = (int)((double)damage * secondMechMultiplier);
			}
		}
		return damage;
	}
}
