using System;
using CalamityMod.Events;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class PlanteraAI : VanillaAIOverride
{
	public class HookAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_033e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0367: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_012d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0166: Unknown result type (might be due to invalid IL or missing references)
			//IL_0587: Unknown result type (might be due to invalid IL or missing references)
			//IL_0597: Unknown result type (might be due to invalid IL or missing references)
			//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_06fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0705: Unknown result type (might be due to invalid IL or missing references)
			//IL_071a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0724: Unknown result type (might be due to invalid IL or missing references)
			//IL_0647: Unknown result type (might be due to invalid IL or missing references)
			bool enrage = false;
			bool despawn = false;
			bool death = CalamityWorld.death | enrage;
			if (NPC.plantBoss < 0)
			{
				if (Main.netMode != 1)
				{
					base.NPC.StrikeInstantKill();
				}
				return false;
			}
			float lifeRatio = (float)Main.npc[NPC.plantBoss].life / (float)Main.npc[NPC.plantBoss].lifeMax;
			if (Main.player[Main.npc[NPC.plantBoss].target].dead && !enrage)
			{
				despawn = true;
			}
			if (!enrage && !BossRushEvent.BossRushActive && (((double)Main.player[Main.npc[NPC.plantBoss].target].position.Y < Main.worldSurface * 16.0 || Main.player[Main.npc[NPC.plantBoss].target].position.Y > (float)(Main.UnderworldLayer * 16)) | despawn))
			{
				base.NPC.localAI[0] -= 4f;
				enrage = true;
			}
			if (Main.netMode == 1)
			{
				if (base.NPC.ai[0] == 0f)
				{
					base.NPC.ai[0] = (int)(base.NPC.Center.X / 16f);
				}
				if (base.NPC.ai[1] == 0f)
				{
					base.NPC.ai[1] = (int)(base.NPC.Center.X / 16f);
				}
			}
			if (Main.netMode != 1)
			{
				if (base.NPC.ai[0] == 0f || base.NPC.ai[1] == 0f)
				{
					base.NPC.localAI[0] = 0f;
				}
				float moveBoost = (death ? (4f * (1f - lifeRatio)) : (2f * (1f - lifeRatio)));
				base.NPC.localAI[0] -= 1f + moveBoost;
				if (enrage)
				{
					base.NPC.localAI[0] -= 6f;
				}
				if (!despawn && base.NPC.localAI[0] <= 0f && base.NPC.ai[0] != 0f)
				{
					ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
					while (enumerator.MoveNext())
					{
						NPC n = enumerator.Current;
						if (n.whoAmI != base.NPC.whoAmI && n.type == base.NPC.type && (n.velocity.X != 0f || n.velocity.Y != 0f))
						{
							base.NPC.localAI[0] = Main.rand.Next(60, 301);
						}
					}
				}
				if (base.NPC.localAI[0] <= 0f)
				{
					base.NPC.localAI[0] = Main.rand.Next(300, 601);
					bool hookCanMove = false;
					int hookMoveTries = 0;
					while (!hookCanMove && hookMoveTries <= 1000)
					{
						hookMoveTries++;
						int targetTilePosX = (int)(Main.player[Main.npc[NPC.plantBoss].target].Center.X / 16f);
						int targetTilePosY = (int)(Main.player[Main.npc[NPC.plantBoss].target].Center.Y / 16f);
						if (base.NPC.ai[0] == 0f)
						{
							targetTilePosX = (int)((Main.player[Main.npc[NPC.plantBoss].target].Center.X + Main.npc[NPC.plantBoss].Center.X) / 32f);
							targetTilePosY = (int)((Main.player[Main.npc[NPC.plantBoss].target].Center.Y + Main.npc[NPC.plantBoss].Center.Y) / 32f);
						}
						if (despawn)
						{
							targetTilePosX = (int)Main.npc[NPC.plantBoss].position.X / 16;
							targetTilePosY = (int)(Main.npc[NPC.plantBoss].position.Y + 400f) / 16;
						}
						int hookTileOffset = 20;
						hookTileOffset += (int)(100f * ((float)hookMoveTries / 1000f));
						int hookTileX = targetTilePosX + Main.rand.Next(-hookTileOffset, hookTileOffset + 1);
						int hookTileY = targetTilePosY + Main.rand.Next(-hookTileOffset, hookTileOffset + 1);
						try
						{
							if (WorldGen.SolidTile(hookTileX, hookTileY) || (Main.tile[hookTileX, hookTileY].WallType > 0 && (hookMoveTries > 500 || lifeRatio < 0.5f)))
							{
								hookCanMove = true;
								base.NPC.ai[0] = hookTileX;
								base.NPC.ai[1] = hookTileY;
								base.NPC.netUpdate = true;
							}
						}
						catch
						{
						}
					}
				}
			}
			if (base.NPC.ai[0] > 0f && base.NPC.ai[1] > 0f)
			{
				float velocityBoost = (death ? (6f * (1f - lifeRatio)) : (3f * (1f - lifeRatio)));
				float velocity = 7f + velocityBoost;
				if (enrage)
				{
					velocity *= 2f;
				}
				if (despawn)
				{
					velocity *= 2f;
				}
				Vector2 hookCenter = default(Vector2);
				((Vector2)(ref hookCenter))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
				float hookMoveX = base.NPC.ai[0] * 16f - 8f - hookCenter.X;
				float hookMoveY = base.NPC.ai[1] * 16f - 8f - hookCenter.Y;
				float hookMoveDistance = (float)Math.Sqrt(hookMoveX * hookMoveX + hookMoveY * hookMoveY);
				if (hookMoveDistance < 12f + velocity)
				{
					if (Main.netMode != 1 && Main.getGoodWorld && base.NPC.localAI[3] == 1f)
					{
						base.NPC.localAI[3] = 0f;
						WorldGen.SpawnPlanteraThorns(base.NPC.Center);
					}
					base.NPC.velocity.X = hookMoveX;
					base.NPC.velocity.Y = hookMoveY;
				}
				else
				{
					if (Main.netMode != 1 && Main.getGoodWorld)
					{
						base.NPC.localAI[3] = 1f;
					}
					hookMoveDistance = velocity / hookMoveDistance;
					base.NPC.velocity.X = hookMoveX * hookMoveDistance;
					base.NPC.velocity.Y = hookMoveY * hookMoveDistance;
				}
				Vector2 hookCenterRotation = default(Vector2);
				((Vector2)(ref hookCenterRotation))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
				float plantXDirection = Main.npc[NPC.plantBoss].Center.X - hookCenterRotation.X;
				float plantYDirection = Main.npc[NPC.plantBoss].Center.Y - hookCenterRotation.Y;
				base.NPC.rotation = (float)Math.Atan2(plantYDirection, plantXDirection) - (float)Math.PI / 2f;
			}
			return false;
		}
	}

	public class TentacleAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_0349: Unknown result type (might be due to invalid IL or missing references)
			//IL_034e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0350: Unknown result type (might be due to invalid IL or missing references)
			//IL_0367: Unknown result type (might be due to invalid IL or missing references)
			//IL_037e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0388: Unknown result type (might be due to invalid IL or missing references)
			//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_043b: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0513: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.Calamity();
			Lighting.AddLight((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f), 0.2f, 0.4f, 0.1f);
			if (Main.rand.NextBool(10))
			{
				Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 44, 0f, 0f, 250, default(Color), 0.4f).fadeIn = 0.7f;
			}
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			if (Main.getGoodWorld)
			{
				if (Main.rand.NextBool(5))
				{
					base.NPC.reflectsProjectiles = true;
				}
				else
				{
					base.NPC.reflectsProjectiles = false;
				}
			}
			if (NPC.plantBoss < 0)
			{
				if (Main.netMode != 1)
				{
					base.NPC.StrikeInstantKill();
				}
				return false;
			}
			int plantBoss = NPC.plantBoss;
			bool planteraIsCharging = Main.npc[plantBoss].ai[3] <= -2f;
			if (Main.npc[plantBoss].ai[2] == -1f && base.NPC.ai[2] != 1f)
			{
				if (Main.netMode != 1)
				{
					base.NPC.StrikeInstantKill();
				}
				return false;
			}
			float extendTime = 180f;
			if (planteraIsCharging)
			{
				if (base.NPC.localAI[0] > 0f)
				{
					base.NPC.localAI[0] = 0f;
					base.NPC.SyncExtraAI();
				}
			}
			else if (base.NPC.localAI[0] < extendTime)
			{
				base.NPC.localAI[0]++;
				if (base.NPC.localAI[0] >= extendTime)
				{
					base.NPC.SyncExtraAI();
				}
			}
			int maxOffset = 100;
			if (Main.netMode != 1 && (base.NPC.ai[0] == 0f || base.NPC.ai[1] == 0f))
			{
				base.NPC.ai[0] = Main.rand.Next(-maxOffset, maxOffset + 1);
				base.NPC.ai[1] = Main.rand.Next(-maxOffset, maxOffset + 1);
				base.NPC.netUpdate = true;
			}
			float tentacleAcceleration = 1.6f;
			float extendedDistanceFromPlantera = Math.Abs(base.NPC.ai[0] + base.NPC.ai[1]) / (float)maxOffset;
			float tentacleDistance = MathHelper.Lerp(50f, 100f + extendedDistanceFromPlantera * 300f, base.NPC.localAI[0] / extendTime);
			float deceleration = (death ? 0.5f : 0.8f) / (1f + extendedDistanceFromPlantera);
			if (death)
			{
				tentacleAcceleration *= 1.2f;
				extendedDistanceFromPlantera *= 1.1f;
				tentacleDistance *= 1.2f;
				deceleration *= 0.75f;
			}
			if (Main.getGoodWorld)
			{
				tentacleAcceleration += 4f;
			}
			if (planteraIsCharging)
			{
				tentacleAcceleration *= 2f;
				deceleration *= 0.5f;
			}
			if (!Main.npc[plantBoss].active)
			{
				base.NPC.active = false;
				return false;
			}
			Vector2 planteraCenter = Main.npc[plantBoss].Center;
			float plantXOffset = planteraCenter.X + base.NPC.ai[0];
			float num = planteraCenter.Y + base.NPC.ai[1];
			float plantXDist = plantXOffset - planteraCenter.X;
			float plantYDist = num - planteraCenter.Y;
			float plantTotalDist = (float)Math.Sqrt(plantXDist * plantXDist + plantYDist * plantYDist);
			plantTotalDist = tentacleDistance / plantTotalDist;
			plantXDist *= plantTotalDist;
			plantYDist *= plantTotalDist;
			if (base.NPC.position.X < planteraCenter.X + plantXDist)
			{
				base.NPC.velocity.X += tentacleAcceleration;
				if (base.NPC.velocity.X < 0f && plantXDist > 0f)
				{
					base.NPC.velocity.X *= deceleration;
				}
			}
			else if (base.NPC.position.X > planteraCenter.X + plantXDist)
			{
				base.NPC.velocity.X -= tentacleAcceleration;
				if (base.NPC.velocity.X > 0f && plantXDist < 0f)
				{
					base.NPC.velocity.X *= deceleration;
				}
			}
			if (base.NPC.position.Y < planteraCenter.Y + plantYDist)
			{
				base.NPC.velocity.Y += tentacleAcceleration;
				if (base.NPC.velocity.Y < 0f && plantYDist > 0f)
				{
					base.NPC.velocity.Y *= deceleration;
				}
			}
			else if (base.NPC.position.Y > planteraCenter.Y + plantYDist)
			{
				base.NPC.velocity.Y -= tentacleAcceleration;
				if (base.NPC.velocity.Y > 0f && plantYDist < 0f)
				{
					base.NPC.velocity.Y *= deceleration;
				}
			}
			float velocityLimit = 12f + 6f * extendedDistanceFromPlantera;
			if (planteraIsCharging)
			{
				velocityLimit *= 1.5f;
			}
			if (base.NPC.velocity.X > velocityLimit)
			{
				base.NPC.velocity.X = velocityLimit;
			}
			if (base.NPC.velocity.X < 0f - velocityLimit)
			{
				base.NPC.velocity.X = 0f - velocityLimit;
			}
			if (base.NPC.velocity.Y > velocityLimit)
			{
				base.NPC.velocity.Y = velocityLimit;
			}
			if (base.NPC.velocity.Y < 0f - velocityLimit)
			{
				base.NPC.velocity.Y = 0f - velocityLimit;
			}
			if (plantXDist > 0f)
			{
				base.NPC.spriteDirection = 1;
				base.NPC.rotation = (float)Math.Atan2(plantYDist, plantXDist);
			}
			if (plantXDist < 0f)
			{
				base.NPC.spriteDirection = -1;
				base.NPC.rotation = (float)Math.Atan2(plantYDist, plantXDist) + (float)Math.PI;
			}
			return false;
		}
	}

	public const float SeedGatlingGateValue = 600f;

	public const float SeedGatlingDuration = 300f;

	public const float SeedGatlingColorChangeDuration = 180f;

	public const float SeedGatlingStopValue = 900f;

	public const float SeedGatlingColorChangeGateValue = 720f;

	public const float TentaclePhaseSlowDuration = 1200f;

	public const float ChargePhaseGateValue = 900f;

	public const float ChargeTelegraphColorChangeGateValue = 720f;

	public const float ReduceSpeedForChargeDistance = 480f;

	public const float BeginChargeGateValue = -120f;

	public const float BeginChargeSlowDownGateValue = -165f;

	public const float StopChargeGateValue = -195f;

	public const float MovementVelocityMultiplierForSlowAttacks = 0.5f;

	public static float Phase2ContactDamageMult = 1.4f;

	public static int PinkSeedDamage = 19;

	public static int PoisonSeedDamage = 24;

	public static int ThornBallDamage = 27;

	public static int ContactDamageCorrection = (Main.masterMode ? 150 : 100);

	public static int ThornBallSpikeDamage = 22;

	public static int GasBulbDamage = 27;

	public static int PinkCloudDamage = 22;

	public static int GreenCloudDamage = 24;

	public static float DashDamageMult = 1.25f;

	public override bool AI(Mod mod)
	{
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1178: Unknown result type (might be due to invalid IL or missing references)
		//IL_117f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1184: Unknown result type (might be due to invalid IL or missing references)
		//IL_152e: Unknown result type (might be due to invalid IL or missing references)
		//IL_154a: Unknown result type (might be due to invalid IL or missing references)
		//IL_154f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1554: Unknown result type (might be due to invalid IL or missing references)
		//IL_1556: Unknown result type (might be due to invalid IL or missing references)
		//IL_155e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1563: Unknown result type (might be due to invalid IL or missing references)
		//IL_156e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1573: Unknown result type (might be due to invalid IL or missing references)
		//IL_1578: Unknown result type (might be due to invalid IL or missing references)
		//IL_157d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1584: Unknown result type (might be due to invalid IL or missing references)
		//IL_1589: Unknown result type (might be due to invalid IL or missing references)
		//IL_1591: Unknown result type (might be due to invalid IL or missing references)
		//IL_1596: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_14fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1501: Unknown result type (might be due to invalid IL or missing references)
		//IL_1052: Unknown result type (might be due to invalid IL or missing references)
		//IL_105d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1062: Unknown result type (might be due to invalid IL or missing references)
		//IL_1067: Unknown result type (might be due to invalid IL or missing references)
		//IL_106c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1071: Unknown result type (might be due to invalid IL or missing references)
		//IL_1079: Unknown result type (might be due to invalid IL or missing references)
		//IL_107e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1085: Unknown result type (might be due to invalid IL or missing references)
		//IL_108a: Unknown result type (might be due to invalid IL or missing references)
		//IL_108f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1095: Unknown result type (might be due to invalid IL or missing references)
		//IL_1099: Unknown result type (might be due to invalid IL or missing references)
		//IL_109e: Unknown result type (might be due to invalid IL or missing references)
		//IL_15bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_15aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aba: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_10be: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_162f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1659: Unknown result type (might be due to invalid IL or missing references)
		//IL_165f: Unknown result type (might be due to invalid IL or missing references)
		//IL_168b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1690: Unknown result type (might be due to invalid IL or missing references)
		//IL_1695: Unknown result type (might be due to invalid IL or missing references)
		//IL_169a: Unknown result type (might be due to invalid IL or missing references)
		//IL_169c: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_16cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_16de: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_16fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2110: Unknown result type (might be due to invalid IL or missing references)
		//IL_213e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df1: Unknown result type (might be due to invalid IL or missing references)
		//IL_111f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1115: Unknown result type (might be due to invalid IL or missing references)
		//IL_138c: Unknown result type (might be due to invalid IL or missing references)
		//IL_139a: Unknown result type (might be due to invalid IL or missing references)
		//IL_139f: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1124: Unknown result type (might be due to invalid IL or missing references)
		//IL_1128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e17: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_177a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1785: Unknown result type (might be due to invalid IL or missing references)
		//IL_178a: Unknown result type (might be due to invalid IL or missing references)
		//IL_178f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1794: Unknown result type (might be due to invalid IL or missing references)
		//IL_179b: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_13db: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_293a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2968: Unknown result type (might be due to invalid IL or missing references)
		//IL_296e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2993: Unknown result type (might be due to invalid IL or missing references)
		//IL_299d: Unknown result type (might be due to invalid IL or missing references)
		//IL_29a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_29d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_29dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_29e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_29e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_29e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_29ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a04: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a10: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a15: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a23: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a44: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a46: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e68: Unknown result type (might be due to invalid IL or missing references)
		//IL_092b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0936: Unknown result type (might be due to invalid IL or missing references)
		//IL_093b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0940: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Unknown result type (might be due to invalid IL or missing references)
		//IL_094a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0952: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_095e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0963: Unknown result type (might be due to invalid IL or missing references)
		//IL_0968: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d00: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d10: Unknown result type (might be due to invalid IL or missing references)
		//IL_1447: Unknown result type (might be due to invalid IL or missing references)
		//IL_1449: Unknown result type (might be due to invalid IL or missing references)
		//IL_2433: Unknown result type (might be due to invalid IL or missing references)
		//IL_243e: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_24be: Unknown result type (might be due to invalid IL or missing references)
		//IL_2533: Unknown result type (might be due to invalid IL or missing references)
		//IL_253e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1800: Unknown result type (might be due to invalid IL or missing references)
		//IL_1807: Unknown result type (might be due to invalid IL or missing references)
		//IL_1815: Unknown result type (might be due to invalid IL or missing references)
		//IL_181b: Unknown result type (might be due to invalid IL or missing references)
		//IL_21db: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_25d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_25da: Unknown result type (might be due to invalid IL or missing references)
		//IL_25df: Unknown result type (might be due to invalid IL or missing references)
		//IL_25e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_25e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09db: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c69: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c74: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c83: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1886: Unknown result type (might be due to invalid IL or missing references)
		//IL_1891: Unknown result type (might be due to invalid IL or missing references)
		//IL_1897: Unknown result type (might be due to invalid IL or missing references)
		//IL_1899: Unknown result type (might be due to invalid IL or missing references)
		//IL_189e: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_18bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2651: Unknown result type (might be due to invalid IL or missing references)
		//IL_2669: Unknown result type (might be due to invalid IL or missing references)
		//IL_266f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2671: Unknown result type (might be due to invalid IL or missing references)
		//IL_2676: Unknown result type (might be due to invalid IL or missing references)
		//IL_267e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2683: Unknown result type (might be due to invalid IL or missing references)
		//IL_268a: Unknown result type (might be due to invalid IL or missing references)
		//IL_268f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2694: Unknown result type (might be due to invalid IL or missing references)
		//IL_227f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a25: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_18fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1900: Unknown result type (might be due to invalid IL or missing references)
		//IL_2862: Unknown result type (might be due to invalid IL or missing references)
		//IL_2866: Unknown result type (might be due to invalid IL or missing references)
		//IL_286b: Unknown result type (might be due to invalid IL or missing references)
		//IL_287f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2890: Unknown result type (might be due to invalid IL or missing references)
		//IL_28ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_28e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_129a: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a78: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_27d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_27d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_27de: Unknown result type (might be due to invalid IL or missing references)
		//IL_27e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_27e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f45: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f50: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f55: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f64: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f71: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f78: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f82: Unknown result type (might be due to invalid IL or missing references)
		//IL_1907: Unknown result type (might be due to invalid IL or missing references)
		//IL_190f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1916: Unknown result type (might be due to invalid IL or missing references)
		//IL_1920: Unknown result type (might be due to invalid IL or missing references)
		//IL_1926: Unknown result type (might be due to invalid IL or missing references)
		//IL_1973: Unknown result type (might be due to invalid IL or missing references)
		//IL_1975: Unknown result type (might be due to invalid IL or missing references)
		//IL_1979: Unknown result type (might be due to invalid IL or missing references)
		//IL_26b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_26b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_26bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_26c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_26cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b82: Unknown result type (might be due to invalid IL or missing references)
		//IL_271c: Unknown result type (might be due to invalid IL or missing references)
		//IL_271e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2722: Unknown result type (might be due to invalid IL or missing references)
		//IL_272c: Unknown result type (might be due to invalid IL or missing references)
		//IL_281f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2821: Unknown result type (might be due to invalid IL or missing references)
		//IL_2825: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b95: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fa7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fab: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2035: Unknown result type (might be due to invalid IL or missing references)
		//IL_202b: Unknown result type (might be due to invalid IL or missing references)
		//IL_203a: Unknown result type (might be due to invalid IL or missing references)
		//IL_203e: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		float phase2LifeRatio = 0.5f;
		bool addThornBallsToGatlingAttack = lifeRatio < 0.85f;
		bool addSporeGasBlastToGatlingAttack = lifeRatio < 0.75f;
		bool useNewGatlingAttackVariant = addSporeGasBlastToGatlingAttack & death;
		bool phase2 = lifeRatio <= phase2LifeRatio;
		bool phase3 = lifeRatio < 0.35f;
		bool phase4 = lifeRatio < 0.2f;
		base.NPC.damage = (int)Math.Round((float)ContactDamageCorrection * (phase2 ? Phase2ContactDamageMult : 1f));
		bool enrage = false;
		bool despawn = false;
		bool surface = !BossRushEvent.BossRushActive && (double)Main.player[base.NPC.target].position.Y < Main.worldSurface * 16.0;
		int maxTentaclesAfterFirstTentaclePhase = (death ? 4 : 2);
		int maxFreeTentaclesAfterFirstTentaclePhase = maxTentaclesAfterFirstTentaclePhase * 2;
		float speedUpDistance = 480f;
		Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center);
		if (Main.player[base.NPC.target].dead)
		{
			despawn = true;
			enrage = true;
		}
		if (Main.netMode != 1 && Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 6000f)
		{
			base.NPC.active = false;
			base.NPC.life = 0;
			if (Main.dedServ)
			{
				NetMessage.SendData(23, -1, -1, null, base.NPC.whoAmI);
			}
		}
		NPC.plantBoss = base.NPC.whoAmI;
		if (base.NPC.localAI[0] == 0f && Main.netMode != 1)
		{
			base.NPC.localAI[0] = 1f;
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 263, base.NPC.whoAmI);
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 263, base.NPC.whoAmI);
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 263, base.NPC.whoAmI);
		}
		int maxHooks = 3;
		int[] hookArray = new int[maxHooks];
		float hookPositionX = 0f;
		float hookPositionY = 0f;
		int numHooksSpawned = 0;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.aiStyle == 52)
			{
				hookPositionX += n.Center.X;
				hookPositionY += n.Center.Y;
				hookArray[numHooksSpawned] = n.whoAmI;
				numHooksSpawned++;
				if (numHooksSpawned >= maxHooks)
				{
					break;
				}
			}
		}
		hookPositionX /= (float)numHooksSpawned;
		hookPositionY /= (float)numHooksSpawned;
		float velocity = (phase4 ? 7f : (phase3 ? 6.5f : (phase2 ? 6f : 4f)));
		float acceleration = (phase3 ? 0.06f : 0.04f);
		float chargeLineUpVelocity = (phase4 ? 12f : (phase3 ? 10f : 8f));
		float chargeLineUpAcceleration = (phase4 ? 0.6f : (phase3 ? 0.5f : 0.4f));
		float chargeVelocity = (phase4 ? 22f : (phase3 ? 20f : 18f));
		float chargeDeceleration = (phase4 ? 0.92f : (phase3 ? 0.95f : 0.96f));
		if (!BossRushEvent.BossRushActive && (surface || Main.player[base.NPC.target].position.Y > (float)(Main.UnderworldLayer * 16)))
		{
			enrage = true;
			velocity += 8f;
			acceleration = 0.15f;
		}
		base.NPC.Calamity().CurrentlyEnraged = enrage;
		Vector2 npcCenterAccountingForHooks = default(Vector2);
		((Vector2)(ref npcCenterAccountingForHooks))._002Ector(hookPositionX, hookPositionY);
		float maxVelocityX = Main.player[base.NPC.target].Center.X - npcCenterAccountingForHooks.X;
		float maxVelocityY = Main.player[base.NPC.target].Center.Y - npcCenterAccountingForHooks.Y;
		bool phase1MoveAway = !phase2 && Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) < 240f && Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
		bool adjustProjectileShootLocation = Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) < 80f;
		if (despawn)
		{
			maxVelocityY *= -1f;
			maxVelocityX *= -1f;
			velocity += 8f;
		}
		else if (phase1MoveAway)
		{
			maxVelocityY *= -1f;
			maxVelocityX *= -1f;
			velocity *= 1.5f;
			acceleration *= 1.5f;
		}
		float distanceFromTarget = (float)Math.Sqrt(maxVelocityX * maxVelocityX + maxVelocityY * maxVelocityY);
		if (death)
		{
			velocity += velocity * 0.35f * ((1f - lifeRatio) / 2f);
			acceleration += acceleration * 0.35f * ((1f - lifeRatio) / 2f);
			if (phase2)
			{
				float aggressionScale = (phase2LifeRatio - lifeRatio) / phase2LifeRatio;
				chargeLineUpVelocity += chargeLineUpVelocity * 0.15f * aggressionScale;
				chargeLineUpAcceleration += chargeLineUpAcceleration * 0.15f * aggressionScale;
				chargeVelocity += chargeVelocity * 0.15f * aggressionScale;
				chargeDeceleration -= 0.05f * aggressionScale;
			}
		}
		if (Main.getGoodWorld)
		{
			velocity *= 1.15f;
			acceleration *= 1.15f;
		}
		bool usingSeedGatling = base.NPC.ai[1] > 600f;
		bool slowedDuringTentaclePhase = base.NPC.ai[2] > 0f;
		bool doneWithTentaclePhase = base.NPC.ai[2] == -1f;
		bool charging = base.NPC.ai[3] <= -2f;
		bool secondCharge = calamityGlobalNPC.newAI[2] == 1f;
		if (!phase2)
		{
			base.NPC.ai[1]++;
			if (usingSeedGatling)
			{
				float currentSeedGatlingTime = base.NPC.ai[1] - 600f;
				velocity *= MathHelper.Lerp(0.5f, 1f, (float)Math.Pow(currentSeedGatlingTime / 300f, 2.0));
				float shootProjectileGateValue = (useNewGatlingAttackVariant ? 45f : 30f);
				if (currentSeedGatlingTime >= 240f)
				{
					shootProjectileGateValue = (useNewGatlingAttackVariant ? 9f : 3f);
				}
				else if (currentSeedGatlingTime >= 180f)
				{
					shootProjectileGateValue = (useNewGatlingAttackVariant ? 15f : 5f);
				}
				else if (currentSeedGatlingTime >= 120f)
				{
					shootProjectileGateValue = (useNewGatlingAttackVariant ? 18f : 9f);
				}
				else if (currentSeedGatlingTime >= 60f)
				{
					shootProjectileGateValue = (useNewGatlingAttackVariant ? 30f : 15f);
				}
				if (base.NPC.ai[1] % shootProjectileGateValue == 0f)
				{
					bool shootThornBall = ((base.NPC.ai[1] % 90f == 0f) & addThornBallsToGatlingAttack) && !useNewGatlingAttackVariant;
					bool shootPoisonSeed = base.NPC.ai[1] % 9f == 0f && !shootThornBall;
					float projectileSpeed = 14f;
					int projectileType = (shootThornBall ? 277 : (shootPoisonSeed ? 276 : 275));
					int damage = (shootThornBall ? ThornBallDamage : (shootPoisonSeed ? PoisonSeedDamage : PinkSeedDamage));
					Vector2 projectileVelocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
					Vector2 spawnOffset = base.NPC.Center + projectileVelocity * 70f;
					if (useNewGatlingAttackVariant)
					{
						int spread = 8;
						if (currentSeedGatlingTime >= 240f)
						{
							spread = 16;
						}
						else if (currentSeedGatlingTime >= 180f)
						{
							spread = 14;
						}
						else if (currentSeedGatlingTime >= 120f)
						{
							spread = 12;
						}
						else if (currentSeedGatlingTime >= 60f)
						{
							spread = 10;
						}
						float rotation = MathHelper.ToRadians((float)spread);
						int numProj = 3;
						for (int i = 0; i < numProj; i++)
						{
							Vector2 perturbedSpeed = projectileVelocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)i / (float)(numProj - 1)));
							int dustType = (shootPoisonSeed ? 74 : 73);
							Vector2 dustVelocity = perturbedSpeed * projectileSpeed;
							for (int k = 0; k < 5; k++)
							{
								int dust = Dust.NewDust(spawnOffset, 14, 14, dustType, dustVelocity.X, dustVelocity.Y);
								Main.dust[dust].noGravity = true;
								Main.dust[dust].scale = 1.4f;
							}
							if (Main.netMode != 1)
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnOffset, perturbedSpeed * projectileSpeed, projectileType, damage, 0f, Main.myPlayer);
							}
						}
					}
					else
					{
						int dustType2 = (shootPoisonSeed ? 74 : 73);
						int dustSpawnBoxSize = (shootThornBall ? 38 : 14);
						int dustAmount = (shootThornBall ? 15 : 5);
						Vector2 dustVelocity2 = projectileVelocity * projectileSpeed;
						for (int j = 0; j < dustAmount; j++)
						{
							int dust2 = Dust.NewDust(spawnOffset, dustSpawnBoxSize, dustSpawnBoxSize, dustType2, dustVelocity2.X, dustVelocity2.Y);
							Main.dust[dust2].noGravity = true;
							Main.dust[dust2].scale = 1.4f;
						}
						if (Main.netMode != 1)
						{
							float ai2 = ((projectileType == 277 && (Main.rand.NextBool() || !Main.zenithWorld)) ? 1f : 0f);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), adjustProjectileShootLocation ? base.NPC.Center : spawnOffset, projectileVelocity * projectileSpeed, projectileType, damage, 0f, Main.myPlayer, 0f, 0f, ai2);
						}
					}
				}
			}
			if (addSporeGasBlastToGatlingAttack && base.NPC.ai[1] > 720f)
			{
				float dustEmitAmount = base.NPC.ai[1] - 720f;
				int dustInXChanceMax = 8;
				int dustChance = (int)Math.Round(MathHelper.Lerp(2f, (float)dustInXChanceMax, 1f - dustEmitAmount / 180f));
				if (Main.rand.NextBool(dustChance))
				{
					int dust3 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 74, 0f, 0f, 0, default(Color), 1.4f);
					Vector2 vector = Utils.SafeNormalize(new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101)), Vector2.UnitY);
					vector *= (float)Main.rand.Next(50, 100) * 0.04f;
					Main.dust[dust3].velocity = vector;
					vector = vector.SafeNormalize(Vector2.UnitY);
					vector *= 86f;
					Main.dust[dust3].position = base.NPC.Center - vector;
				}
			}
			if (base.NPC.ai[1] >= 900f)
			{
				if (addSporeGasBlastToGatlingAttack)
				{
					SoundEngine.PlaySound(in SoundID.Item74, base.NPC.Center);
					int totalProjectiles = (death ? 36 : 30);
					float radians = (float)Math.PI * 2f / (float)totalProjectiles;
					int type = ModContent.ProjectileType<SporeGasPlantera>();
					float velocity2 = (Main.getGoodWorld ? 10f : 5f);
					Vector2 spinningPoint = default(Vector2);
					((Vector2)(ref spinningPoint))._002Ector(0f, 0f - velocity2);
					for (int l = 0; l < totalProjectiles; l++)
					{
						Vector2 projectileVelocity2 = spinningPoint.RotatedBy(radians * (float)l);
						Vector2 spawnOffset2 = base.NPC.Center + projectileVelocity2.SafeNormalize(Vector2.UnitY) * 50f;
						float randomSpeed = Main.rand.NextFloat(0.8f, death ? 1.5f : 1.2f);
						int dustType3 = 74;
						Vector2 dustVelocity3 = projectileVelocity2 * randomSpeed;
						for (int m = 0; m < 5; m++)
						{
							int dust4 = Dust.NewDust(spawnOffset2, 32, 32, dustType3, dustVelocity3.X, dustVelocity3.Y);
							Main.dust[dust4].scale = 1.4f;
						}
						float ai3 = Main.rand.Next(3);
						if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), adjustProjectileShootLocation ? base.NPC.Center : spawnOffset2, projectileVelocity2 * randomSpeed, type, GreenCloudDamage, 0f, Main.myPlayer, ai3);
						}
					}
				}
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
				base.NPC.ai[1] = -300f;
			}
		}
		else
		{
			base.NPC.ai[1] = 0f;
			if (slowedDuringTentaclePhase)
			{
				velocity *= MathHelper.Lerp(0.5f, 1f, (float)Math.Pow(1f - base.NPC.ai[2] / 1200f, 2.0));
			}
			if (doneWithTentaclePhase && !charging && !despawn)
			{
				float timeToChargeIncrement = (phase4 ? 2f : (phase3 ? 1.5f : 1f));
				if (death)
				{
					timeToChargeIncrement *= 2f;
				}
				base.NPC.ai[3] += timeToChargeIncrement;
				if (base.NPC.ai[3] >= 900f)
				{
					base.NPC.ai[3] = -2f;
				}
			}
		}
		bool slowedAfterGatlingAttack = base.NPC.ai[1] < 0f && !phase2;
		if (slowedAfterGatlingAttack)
		{
			float absValueOfTimer = Math.Abs(base.NPC.ai[1]);
			velocity *= MathHelper.Lerp(0.5f, 1f, (float)Math.Pow(absValueOfTimer / 300f, 2.0));
			float shootBulbGateValue = (death ? 150f : 120f);
			if (addSporeGasBlastToGatlingAttack)
			{
				shootBulbGateValue *= 0.8f;
			}
			if (absValueOfTimer % shootBulbGateValue == 0f)
			{
				float projectileSpeed2 = 9f;
				int projectileType2 = ModContent.ProjectileType<HomingGasBulb>();
				Vector2 projectileVelocity3 = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
				Vector2 spawnOffset3 = base.NPC.Center + projectileVelocity3 * 70f;
				int dustType4 = 73;
				Vector2 dustVelocity4 = projectileVelocity3 * projectileSpeed2;
				for (int num = 0; num < 5; num++)
				{
					int dust5 = Dust.NewDust(spawnOffset3, 18, 18, dustType4, dustVelocity4.X, dustVelocity4.Y);
					Main.dust[dust5].noGravity = true;
					Main.dust[dust5].scale = 1.4f;
				}
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), adjustProjectileShootLocation ? base.NPC.Center : spawnOffset3, projectileVelocity3 * projectileSpeed2, projectileType2, GasBulbDamage, 0f, Main.myPlayer);
				}
			}
		}
		if (charging)
		{
			if (base.NPC.ai[3] <= -165f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= chargeDeceleration;
				float timeToDecelerateDecrement = (phase4 ? 1.5f : 1f);
				base.NPC.ai[3] -= timeToDecelerateDecrement;
				if (base.NPC.ai[3] <= -195f)
				{
					bool chargeAgain = ((phase4 || (phase3 && Main.rand.NextBool())) & death) && calamityGlobalNPC.newAI[2] == 0f;
					base.NPC.ai[3] = (chargeAgain ? (-2f) : 0f);
					calamityGlobalNPC.newAI[2] = (((death && calamityGlobalNPC.newAI[2] == 0f) & chargeAgain) ? 1f : 0f);
					base.NPC.SyncExtraAI();
					if (!secondCharge && Main.netMode != 1 && NPC.CountNPCS(264) < maxTentaclesAfterFirstTentaclePhase && NPC.CountNPCS(ModContent.NPCType<PlanterasFreeTentacle>()) < maxFreeTentaclesAfterFirstTentaclePhase)
					{
						for (int num2 = 0; num2 < maxTentaclesAfterFirstTentaclePhase; num2++)
						{
							NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 264, base.NPC.whoAmI, 0f, 0f, 1f);
						}
					}
				}
			}
			else if (base.NPC.ai[3] <= -120f)
			{
				base.NPC.damage = (int)Math.Round((float)ContactDamageCorrection * Phase2ContactDamageMult * DashDamageMult);
				float sporeGasDashGateValue = (death ? 6f : 9f);
				if (phase3 && base.NPC.ai[3] % sporeGasDashGateValue == 0f)
				{
					int projectileType3 = ModContent.ProjectileType<SporeGasPlantera>();
					float randomVelocityMultiplier = (secondCharge ? 0.05f : (death ? 0.3f : 0.2f));
					Vector2 projectileVelocity4 = base.NPC.velocity * Main.rand.NextVector2CircularEdge(randomVelocityMultiplier, randomVelocityMultiplier);
					Vector2 spawnOffset4 = base.NPC.Center + projectileVelocity4.SafeNormalize(Vector2.UnitY) * 30f;
					int dustType5 = 74;
					Vector2 dustVelocity5 = projectileVelocity4;
					for (int num3 = 0; num3 < 5; num3++)
					{
						int dust6 = Dust.NewDust(spawnOffset4, 32, 32, dustType5, dustVelocity5.X, dustVelocity5.Y);
						Main.dust[dust6].scale = 1.4f;
					}
					float ai4 = Main.rand.Next(3);
					if (Main.netMode != 1)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnOffset4, projectileVelocity4, projectileType3, GasBulbDamage, 0f, Main.myPlayer, ai4);
					}
				}
				base.NPC.ai[3]--;
				if (base.NPC.ai[3] <= -165f)
				{
					base.NPC.ai[3] = -165f;
				}
			}
			else
			{
				if (base.NPC.Calamity().newAI[0] == 0f)
				{
					base.NPC.Calamity().newAI[0] = Math.Sign((base.NPC.Center - Main.player[base.NPC.target].Center).X);
					base.NPC.SyncExtraAI();
				}
				Vector2 destination = Main.player[base.NPC.target].Center + new Vector2(base.NPC.Calamity().newAI[0], 0f);
				Vector2 desiredVelocity = (destination - base.NPC.Center - base.NPC.velocity).SafeNormalize(Vector2.UnitY) * chargeLineUpVelocity;
				if (Vector2.Distance(base.NPC.Center, destination) > 480f)
				{
					base.NPC.SimpleFlyMovement(desiredVelocity, chargeLineUpAcceleration);
				}
				else
				{
					NPC nPC2 = base.NPC;
					nPC2.velocity *= 0.98f;
				}
				float dustEmitAmount2 = Math.Abs(-120f) - Math.Abs(base.NPC.ai[3]);
				int dustInXChanceMax2 = 8;
				int dustChance2 = (int)Math.Round(MathHelper.Lerp(2f, (float)dustInXChanceMax2, 1f - dustEmitAmount2 / Math.Abs(-120f)));
				if (Main.rand.NextBool(dustChance2))
				{
					int dust7 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 74, 0f, 0f, 0, default(Color), 1.4f);
					Vector2 vector2 = Utils.SafeNormalize(new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101)), Vector2.UnitY);
					vector2 *= (float)Main.rand.Next(50, 100) * 0.04f;
					Main.dust[dust7].velocity = vector2;
					vector2 = vector2.SafeNormalize(Vector2.UnitY);
					vector2 *= 86f;
					Main.dust[dust7].position = base.NPC.Center - vector2;
				}
				float timeToLineUpChargeDecrement = (phase4 ? 2f : 1f);
				if (death)
				{
					timeToLineUpChargeDecrement *= 2f;
				}
				base.NPC.ai[3] -= timeToLineUpChargeDecrement;
				if (base.NPC.ai[3] <= -120f)
				{
					base.NPC.ai[3] = -120f;
					base.NPC.velocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * chargeVelocity;
					SoundEngine.PlaySound(in SoundID.Item74, base.NPC.Center);
					Vector2 dustVelocity6 = base.NPC.velocity * -0.25f;
					for (int num4 = 0; num4 < 30; num4++)
					{
						Dust.NewDustDirect(base.NPC.Center, base.NPC.width, base.NPC.height, 44, dustVelocity6.X, dustVelocity6.Y, 250, default(Color), 0.8f).fadeIn = 0.7f;
					}
					int totalProjectiles2 = (secondCharge ? 6 : 12);
					float radians2 = (float)Math.PI * 2f / (float)totalProjectiles2;
					int type2 = ModContent.ProjectileType<SporeGasPlantera>();
					float velocity3 = (Main.getGoodWorld ? 10f : 5f);
					Vector2 spinningPoint2 = default(Vector2);
					((Vector2)(ref spinningPoint2))._002Ector(0f, 0f - velocity3);
					for (int num5 = 0; num5 < totalProjectiles2; num5++)
					{
						Vector2 projectileVelocity5 = spinningPoint2.RotatedBy(radians2 * (float)num5);
						Vector2 spawnOffset5 = base.NPC.Center + projectileVelocity5.SafeNormalize(Vector2.UnitY) * 50f;
						float randomSpeed2 = Main.rand.NextFloat(0.8f, secondCharge ? 1f : (death ? 1.5f : 1.2f));
						int dustType6 = 74;
						Vector2 dustVelocity7 = projectileVelocity5 * randomSpeed2;
						for (int num6 = 0; num6 < 5; num6++)
						{
							int dust8 = Dust.NewDust(spawnOffset5, 32, 32, dustType6, dustVelocity7.X, dustVelocity7.Y);
							Main.dust[dust8].scale = 1.4f;
						}
						float ai5 = Main.rand.Next(3);
						if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnOffset5, projectileVelocity5 * randomSpeed2, type2, GasBulbDamage, 0f, Main.myPlayer, ai5);
						}
					}
				}
				float rotationX = Main.player[base.NPC.target].Center.X - base.NPC.Center.X;
				float rotationY = Main.player[base.NPC.target].Center.Y - base.NPC.Center.Y;
				base.NPC.rotation = (float)Math.Atan2(rotationY, rotationX) + (float)Math.PI / 2f;
			}
		}
		else
		{
			float maxDistanceFromHooks = (enrage ? 1000f : 600f);
			if (phase3)
			{
				maxDistanceFromHooks += 150f;
			}
			if (death)
			{
				maxDistanceFromHooks += maxDistanceFromHooks * 0.2f * ((1f - lifeRatio) / 2f);
				maxDistanceFromHooks += 200f;
			}
			if (distanceFromTarget >= maxDistanceFromHooks)
			{
				distanceFromTarget = maxDistanceFromHooks / distanceFromTarget;
				maxVelocityX *= distanceFromTarget;
				maxVelocityY *= distanceFromTarget;
			}
			hookPositionX += maxVelocityX;
			hookPositionY += maxVelocityY;
			npcCenterAccountingForHooks = base.NPC.Center;
			maxVelocityX = hookPositionX - npcCenterAccountingForHooks.X;
			maxVelocityY = hookPositionY - npcCenterAccountingForHooks.Y;
			distanceFromTarget = (float)Math.Sqrt(maxVelocityX * maxVelocityX + maxVelocityY * maxVelocityY);
			if (distanceFromTarget < velocity)
			{
				maxVelocityX = base.NPC.velocity.X;
				maxVelocityY = base.NPC.velocity.Y;
			}
			else
			{
				distanceFromTarget = velocity / distanceFromTarget;
				maxVelocityX *= distanceFromTarget;
				maxVelocityY *= distanceFromTarget;
			}
			if (base.NPC.velocity.X < maxVelocityX)
			{
				base.NPC.velocity.X += acceleration;
				if (base.NPC.velocity.X < 0f && maxVelocityX > 0f)
				{
					base.NPC.velocity.X += acceleration * 2f;
				}
			}
			else if (base.NPC.velocity.X > maxVelocityX)
			{
				base.NPC.velocity.X -= acceleration;
				if (base.NPC.velocity.X > 0f && maxVelocityX < 0f)
				{
					base.NPC.velocity.X -= acceleration * 2f;
				}
			}
			if (base.NPC.velocity.Y < maxVelocityY)
			{
				base.NPC.velocity.Y += acceleration;
				if (base.NPC.velocity.Y < 0f && maxVelocityY > 0f)
				{
					base.NPC.velocity.Y += acceleration * 2f;
				}
			}
			else if (base.NPC.velocity.Y > maxVelocityY)
			{
				base.NPC.velocity.Y -= acceleration;
				if (base.NPC.velocity.Y > 0f && maxVelocityY < 0f)
				{
					base.NPC.velocity.Y -= acceleration * 2f;
				}
			}
			float rotationX2 = Main.player[base.NPC.target].Center.X - base.NPC.Center.X;
			float rotationY2 = Main.player[base.NPC.target].Center.Y - base.NPC.Center.Y;
			base.NPC.rotation = (float)Math.Atan2(rotationY2, rotationX2) + (float)Math.PI / 2f;
		}
		if (!phase2)
		{
			Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0.8f, 0.2f, 0.4f);
			calamityGlobalNPC.DR = 0.15f;
			calamityGlobalNPC.unbreakableDR = false;
			base.NPC.defense = 32;
			if (!usingSeedGatling && !slowedAfterGatlingAttack)
			{
				float shootBoost = 2f * (1f - lifeRatio);
				base.NPC.localAI[1] += 1f + shootBoost;
				if (enrage)
				{
					base.NPC.localAI[1] += 2f;
				}
				if (Main.getGoodWorld)
				{
					base.NPC.localAI[1]++;
				}
				float shootProjectileGateValue2 = (death ? 40f : 60f);
				if (base.NPC.localAI[1] >= shootProjectileGateValue2)
				{
					base.NPC.localAI[1] = 0f;
					bool shootThornBall2 = false;
					if (useNewGatlingAttackVariant)
					{
						int numThornBalls = 0;
						int thornBallLimit = 3;
						for (int num7 = 0; num7 < Main.maxProjectiles; num7++)
						{
							if (Main.projectile[num7].active && Main.projectile[num7].type == 277)
							{
								numThornBalls++;
								if (numThornBalls >= thornBallLimit)
								{
									shootThornBall2 = false;
									break;
								}
							}
						}
					}
					bool shootPoisonSeed2 = (Main.getGoodWorld || Main.rand.NextBool(death ? 2 : 4)) && !shootThornBall2;
					int projectileType4 = (shootThornBall2 ? 277 : (shootPoisonSeed2 ? 276 : 275));
					float projectileSpeed3 = (death ? 16f : 14f);
					int damage2 = (shootThornBall2 ? ThornBallDamage : (shootPoisonSeed2 ? PoisonSeedDamage : PinkSeedDamage));
					Vector2 projectileVelocity6 = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
					Vector2 spawnOffset6 = base.NPC.Center + projectileVelocity6 * 70f;
					int dustType7 = (shootPoisonSeed2 ? 74 : 73);
					int dustSpawnBoxSize2 = (shootThornBall2 ? 38 : 14);
					int dustAmount2 = (shootThornBall2 ? 15 : 5);
					Vector2 dustVelocity8 = projectileVelocity6 * projectileSpeed3;
					for (int num8 = 0; num8 < dustAmount2; num8++)
					{
						int dust9 = Dust.NewDust(spawnOffset6, dustSpawnBoxSize2, dustSpawnBoxSize2, dustType7, dustVelocity8.X, dustVelocity8.Y);
						Main.dust[dust9].noGravity = true;
						Main.dust[dust9].scale = 1.4f;
					}
					if (Main.netMode != 1)
					{
						int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), adjustProjectileShootLocation ? base.NPC.Center : spawnOffset6, projectileVelocity6 * projectileSpeed3, projectileType4, damage2, 0f, Main.myPlayer);
						if (projectileType4 == 277 && (Main.rand.NextBool() || !Main.zenithWorld))
						{
							Main.projectile[proj].tileCollide = false;
						}
					}
				}
			}
		}
		else
		{
			Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0.4f, 0.8f, 0.2f);
			if (Main.rand.NextBool(10))
			{
				Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 44, 0f, 0f, 250, default(Color), 0.6f).fadeIn = 0.7f;
			}
			calamityGlobalNPC.DR = 0.15f;
			calamityGlobalNPC.unbreakableDR = false;
			base.NPC.defense = 10;
			if (Main.netMode != 1 && base.NPC.localAI[0] == 1f)
			{
				base.NPC.localAI[0] = 2f;
				int totalTentacles = (death ? 11 : 8);
				if (Main.getGoodWorld)
				{
					totalTentacles *= 2;
				}
				for (int num9 = 0; num9 < totalTentacles; num9++)
				{
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 264, base.NPC.whoAmI);
				}
				if (Main.getGoodWorld)
				{
					ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						NPC n2 = enumerator2.Current;
						if (n2.aiStyle == 52)
						{
							for (int num10 = 0; num10 < totalTentacles / 2 - 1; num10++)
							{
								int hookIndex = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 264, base.NPC.whoAmI);
								Main.npc[hookIndex].ai[3] = n2.whoAmI + 1;
							}
						}
					}
				}
			}
			if (base.NPC.ai[2] == 0f)
			{
				base.NPC.ai[2] = 1200f;
			}
			if (slowedDuringTentaclePhase)
			{
				bool noAttachedTentacles = !NPC.AnyNPCs(264);
				bool noFreeTentacles = !NPC.AnyNPCs(ModContent.NPCType<PlanterasFreeTentacle>());
				float tentacleIdleTimerDecrement = ((noAttachedTentacles & noFreeTentacles) ? 4f : (noAttachedTentacles ? 2f : 1f));
				if (death)
				{
					tentacleIdleTimerDecrement *= 2f;
				}
				base.NPC.ai[2] -= tentacleIdleTimerDecrement;
				if (base.NPC.ai[2] <= 0f)
				{
					base.NPC.ai[2] = -1f;
				}
			}
			if (base.NPC.localAI[2] == 0f)
			{
				if (!Main.dedServ)
				{
					Gore.NewGore(base.NPC.GetSource_FromAI(), new Vector2(base.NPC.position.X + (float)Main.rand.Next(base.NPC.width), base.NPC.position.Y + (float)Main.rand.Next(base.NPC.height)), base.NPC.velocity, 378, base.NPC.scale);
					Gore.NewGore(base.NPC.GetSource_FromAI(), new Vector2(base.NPC.position.X + (float)Main.rand.Next(base.NPC.width), base.NPC.position.Y + (float)Main.rand.Next(base.NPC.height)), base.NPC.velocity, 379, base.NPC.scale);
					Gore.NewGore(base.NPC.GetSource_FromAI(), new Vector2(base.NPC.position.X + (float)Main.rand.Next(base.NPC.width), base.NPC.position.Y + (float)Main.rand.Next(base.NPC.height)), base.NPC.velocity, 380, base.NPC.scale);
				}
				base.NPC.localAI[2] = 1f;
			}
			if (!charging)
			{
				base.NPC.localAI[3]++;
				float shootProjectileGateValue3 = (slowedDuringTentaclePhase ? 120f : 90f);
				if (base.NPC.localAI[3] >= shootProjectileGateValue3)
				{
					float projectileSpeed4 = 14f;
					Vector2 projectileVelocity7 = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
					int num11 = 8 + (int)Math.Round((0.5f - lifeRatio) * 16f);
					int numProj2 = num11 / 2;
					if (numProj2 % 2 == 0)
					{
						numProj2++;
					}
					int type3 = 276;
					int damage3 = PoisonSeedDamage;
					float rotation2 = MathHelper.ToRadians((float)num11);
					for (int num12 = 0; num12 < numProj2; num12++)
					{
						bool num13 = num12 % 2 == 0;
						if (num13)
						{
							type3 = 275;
							damage3 = PinkSeedDamage;
						}
						else
						{
							type3 = 276;
						}
						Vector2 perturbedSpeed2 = projectileVelocity7.RotatedBy(MathHelper.Lerp(0f - rotation2, rotation2, (float)num12 / (float)(numProj2 - 1)));
						Vector2 spawnOffset7 = base.NPC.Center + perturbedSpeed2 * 50f;
						int dustType8 = (num13 ? 73 : 74);
						Vector2 dustVelocity9 = perturbedSpeed2 * projectileSpeed4;
						for (int num14 = 0; num14 < 5; num14++)
						{
							int dust10 = Dust.NewDust(spawnOffset7, 14, 14, dustType8, dustVelocity9.X, dustVelocity9.Y);
							Main.dust[dust10].noGravity = true;
							Main.dust[dust10].scale = 1.4f;
						}
						if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnOffset7, perturbedSpeed2 * projectileSpeed4 * 0.5f, type3, damage3, 0f, Main.myPlayer, 0f, 0f, projectileSpeed4);
						}
					}
					if (death)
					{
						bool shootThornBall3 = true;
						int numThornBalls2 = 0;
						int thornBallLimit2 = 3;
						for (int num15 = 0; num15 < Main.maxProjectiles; num15++)
						{
							if (Main.projectile[num15].active && Main.projectile[num15].type == 277)
							{
								numThornBalls2++;
								if (numThornBalls2 >= thornBallLimit2)
								{
									shootThornBall3 = false;
									break;
								}
							}
						}
						if (shootThornBall3)
						{
							type3 = 277;
							damage3 = ThornBallDamage;
							Vector2 spawnOffset8 = base.NPC.Center + projectileVelocity7 * 50f;
							if (Main.netMode != 1)
							{
								float ai6 = 0f;
								if (Main.rand.NextBool() || !Main.zenithWorld)
								{
									ai6 = 1f;
								}
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnOffset8, projectileVelocity7 * projectileSpeed4, type3, damage3, 0f, Main.myPlayer, 0f, 0f, ai6);
							}
						}
					}
					if (death && Main.netMode != 1)
					{
						float sporeSpeed = 12f;
						Vector2 sporeVelocity = projectileVelocity7 * sporeSpeed;
						int spore = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 265);
						Main.npc[spore].velocity.X = sporeVelocity.X;
						Main.npc[spore].velocity.Y = sporeVelocity.Y;
						Main.npc[spore].netUpdate = true;
					}
					base.NPC.localAI[3] = 0f;
				}
			}
		}
		if (surface)
		{
			if (Main.rand.NextBool(Main.IsItDay() ? 3 : 6))
			{
				int dust11 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 55, 0f, 0f, 200, default(Color), 0.5f);
				Main.dust[dust11].noGravity = true;
				Dust obj = Main.dust[dust11];
				obj.velocity *= 0.75f;
				Main.dust[dust11].fadeIn = 1.3f;
				Vector2 vector3 = Utils.SafeNormalize(new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101)), Vector2.UnitY);
				vector3 *= (float)Main.rand.Next(50, 100) * 0.04f;
				Main.dust[dust11].velocity = vector3;
				vector3 = vector3.SafeNormalize(Vector2.UnitY);
				vector3 *= 86f;
				Main.dust[dust11].position = base.NPC.Center - vector3;
			}
			calamityGlobalNPC.newAI[1]++;
			if (calamityGlobalNPC.newAI[1] >= (Main.IsItDay() ? 30f : 60f))
			{
				calamityGlobalNPC.newAI[1] = 0f;
				base.NPC.SyncExtraAI();
				if (Main.netMode != 1)
				{
					int healAmt = base.NPC.lifeMax / 100;
					if (healAmt > base.NPC.lifeMax - base.NPC.life)
					{
						healAmt = base.NPC.lifeMax - base.NPC.life;
					}
					if (healAmt > 0)
					{
						base.NPC.life += healAmt;
						base.NPC.HealEffect(healAmt);
						base.NPC.netUpdate = true;
					}
				}
			}
		}
		if (base.NPC.ai[0] == 0f && base.NPC.life > 0)
		{
			base.NPC.ai[0] = base.NPC.lifeMax;
		}
		if (base.NPC.life > 0 && Main.netMode != 1)
		{
			int healthInterval = (death ? ((int)((double)base.NPC.lifeMax * 0.03)) : ((int)((double)base.NPC.lifeMax * 0.04)));
			if ((float)(base.NPC.life + healthInterval) < base.NPC.ai[0])
			{
				base.NPC.ai[0] = base.NPC.life;
				if (phase2)
				{
					int spore2 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 265, base.NPC.whoAmI);
					float sporeSpeed2 = (death ? 8f : 6f);
					Vector2 sporeVelocity2 = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * sporeSpeed2;
					Main.npc[spore2].velocity.X = sporeVelocity2.X;
					Main.npc[spore2].velocity.Y = sporeVelocity2.Y;
					Main.npc[spore2].netUpdate = true;
				}
			}
		}
		return false;
	}

	public override void PostDraw(Mod mod, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		float num = (float)base.NPC.life / (float)base.NPC.lifeMax;
		Texture2D npcTexture = TextureAssets.Npc[base.NPC.type].Value;
		SpriteEffects spriteEffects = (SpriteEffects)(base.NPC.spriteDirection == 1);
		Color originalColor = base.NPC.GetAlpha(drawColor);
		Color newColor = default(Color);
		((Color)(ref newColor))._002Ector(100, 255, 100, 255);
		Vector2 glowOffset = Vector2.UnitY * -4f;
		Vector2 drawPosition = base.NPC.Center - screenPos + new Vector2(0f, base.NPC.gfxOffY) + glowOffset;
		Vector2 origin = base.NPC.frame.Size() / 2f;
		if (!(num <= 0.5f))
		{
			float telegraphTimer = Math.Abs(base.NPC.ai[1]);
			bool num2 = base.NPC.ai[1] > 720f;
			bool endSeedGatlingSporeGasTelegraph = base.NPC.ai[1] < -120f;
			if (num2)
			{
				float telegraphScalar = MathHelper.Clamp((telegraphTimer - 720f) / 180f, 0f, 1f);
				Color telegraphColor = Color.Lerp(originalColor, newColor, telegraphScalar);
				spriteBatch.Draw(npcTexture, drawPosition, (Rectangle?)base.NPC.frame, telegraphColor, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
			}
			else if (endSeedGatlingSporeGasTelegraph)
			{
				float telegraphScalar2 = MathHelper.Clamp((telegraphTimer - 120f) / 180f, 0f, 1f);
				Color telegraphColor2 = Color.Lerp(originalColor, newColor, telegraphScalar2);
				spriteBatch.Draw(npcTexture, drawPosition, (Rectangle?)base.NPC.frame, telegraphColor2, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
			}
			return;
		}
		float telegraphTimer2 = Math.Abs(base.NPC.ai[3]);
		bool num3 = base.NPC.ai[3] > 720f;
		bool endChargeTelegraph = base.NPC.ai[3] <= -2f;
		if (num3)
		{
			float telegraphScalar3 = MathHelper.Clamp((telegraphTimer2 - 720f) / 180f, 0f, 1f);
			Color telegraphColor3 = Color.Lerp(originalColor, newColor, telegraphScalar3);
			spriteBatch.Draw(npcTexture, drawPosition, (Rectangle?)base.NPC.frame, telegraphColor3, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
		}
		else
		{
			if (!endChargeTelegraph)
			{
				return;
			}
			float telegraphScalar4 = MathHelper.Clamp((Math.Abs(-195f) - telegraphTimer2) / Math.Abs(-195f), 0f, 1f);
			Color telegraphColor4 = Color.Lerp(originalColor, newColor, telegraphScalar4);
			if (CalamityClientConfig.Instance.Afterimages)
			{
				int afterimageAmount = 10;
				int afterImageIncrement = 2;
				for (int j = 0; j < afterimageAmount; j += afterImageIncrement)
				{
					Color afterimageColor = telegraphColor4;
					afterimageColor = Color.Lerp(afterimageColor, originalColor, 0.5f);
					afterimageColor = base.NPC.GetAlpha(afterimageColor);
					afterimageColor *= (float)(afterimageAmount - j) / 15f;
					Vector2 afterimagePos = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
					afterimagePos -= new Vector2((float)npcTexture.Width, (float)(npcTexture.Height / Main.npcFrameCount[base.NPC.type])) * base.NPC.scale / 2f;
					afterimagePos += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY) + glowOffset;
					spriteBatch.Draw(npcTexture, afterimagePos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
				}
			}
			spriteBatch.Draw(npcTexture, drawPosition, (Rectangle?)base.NPC.frame, telegraphColor4, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
		}
	}
}
