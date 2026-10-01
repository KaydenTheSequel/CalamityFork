using System;
using CalamityMod.Events;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class GolemAI : VanillaAIOverride
{
	public class FistAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_0128: Unknown result type (might be due to invalid IL or missing references)
			//IL_0191: Unknown result type (might be due to invalid IL or missing references)
			//IL_0197: Unknown result type (might be due to invalid IL or missing references)
			//IL_019c: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0200: Unknown result type (might be due to invalid IL or missing references)
			//IL_0205: Unknown result type (might be due to invalid IL or missing references)
			//IL_0554: Unknown result type (might be due to invalid IL or missing references)
			//IL_0571: Unknown result type (might be due to invalid IL or missing references)
			//IL_0576: Unknown result type (might be due to invalid IL or missing references)
			//IL_0466: Unknown result type (might be due to invalid IL or missing references)
			//IL_0480: Unknown result type (might be due to invalid IL or missing references)
			//IL_0297: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c3c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c41: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c44: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c4e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c59: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c63: Unknown result type (might be due to invalid IL or missing references)
			//IL_0968: Unknown result type (might be due to invalid IL or missing references)
			//IL_0973: Unknown result type (might be due to invalid IL or missing references)
			//IL_06df: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0706: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0603: Unknown result type (might be due to invalid IL or missing references)
			//IL_060d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0613: Unknown result type (might be due to invalid IL or missing references)
			//IL_0314: Unknown result type (might be due to invalid IL or missing references)
			//IL_0325: Unknown result type (might be due to invalid IL or missing references)
			//IL_02df: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_098b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0995: Unknown result type (might be due to invalid IL or missing references)
			//IL_099a: Unknown result type (might be due to invalid IL or missing references)
			//IL_09ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_09b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_09b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_09bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_09bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_09c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_09c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ad3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a3d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a48: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b08: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b13: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a80: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a8b: Unknown result type (might be due to invalid IL or missing references)
			if (NPC.golemBoss < 0)
			{
				if (Main.netMode != 1)
				{
					base.NPC.StrikeInstantKill();
				}
				return false;
			}
			if (base.NPC.alpha > 0)
			{
				base.NPC.alpha -= 10;
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
			}
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
			}
			NPC golem = Main.npc[NPC.golemBoss];
			Player player = Main.player[base.NPC.target];
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			bool enrage = !BossRushEvent.BossRushActive;
			bool turboEnrage = false;
			if ((double)player.Center.Y > Main.worldSurface * 16.0 && !BossRushEvent.BossRushActive)
			{
				int i = (int)player.Center.X / 16;
				int targetTilePosY = (int)player.Center.Y / 16;
				if (Framing.GetTileSafely(i, targetTilePosY).WallType == 87)
				{
					enrage = false;
				}
				else
				{
					turboEnrage = Main.getGoodWorld;
				}
			}
			else
			{
				turboEnrage = Main.getGoodWorld;
			}
			if (Main.getGoodWorld)
			{
				enrage = true;
			}
			float aggression = (turboEnrage ? 3f : (enrage ? 2f : (death ? 1.7f : 1f)));
			Vector2 fistCenter = golem.Center + golem.velocity + new Vector2(0f, -9f * base.NPC.scale);
			fistCenter.X += (float)((base.NPC.type == 247) ? (-84) : 78) * base.NPC.scale;
			Vector2 distanceFromFistCenter = fistCenter - base.NPC.Center;
			float distanceFromRestPosition = ((Vector2)(ref distanceFromFistCenter)).Length();
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.damage = 0;
				base.NPC.noTileCollide = true;
				float fistSpeed = 28f;
				fistSpeed *= (aggression + 3f) / 4f;
				if (fistSpeed > 48f)
				{
					fistSpeed = 48f;
				}
				float fistRestDistance = distanceFromRestPosition;
				if (fistRestDistance < 12f + fistSpeed)
				{
					base.NPC.rotation = 0f;
					base.NPC.velocity.X = distanceFromFistCenter.X;
					base.NPC.velocity.Y = distanceFromFistCenter.Y;
					bool canPunch = (base.NPC.alpha == 0 && base.NPC.type == 247 && base.NPC.Center.X + 100f > player.Center.X) || (base.NPC.type == 248 && base.NPC.Center.X - 100f < player.Center.X);
					if (canPunch)
					{
						float fistShootSpeed = (death ? Main.rand.NextFloat(aggression * 0.5f, aggression * 2f) : aggression);
						base.NPC.ai[1] += fistShootSpeed;
						if (base.NPC.life < base.NPC.lifeMax / 2)
						{
							base.NPC.ai[1] += fistShootSpeed;
						}
						if (base.NPC.life < base.NPC.lifeMax / 4)
						{
							base.NPC.ai[1] += fistShootSpeed;
						}
					}
					float fistPunchGateValue = (death ? 120f : 40f);
					if (base.NPC.ai[1] >= fistPunchGateValue)
					{
						if (canPunch)
						{
							base.NPC.ai[1] = 0f;
							base.NPC.ai[0] = 1f;
						}
						else
						{
							base.NPC.ai[1] = 0f;
						}
						if (death)
						{
							base.NPC.ForceNetUpdate();
						}
					}
				}
				else
				{
					fistRestDistance = fistSpeed / fistRestDistance;
					base.NPC.velocity.X = distanceFromFistCenter.X * fistRestDistance;
					base.NPC.velocity.Y = distanceFromFistCenter.Y * fistRestDistance;
					base.NPC.rotation = (float)Math.Atan2(0f - base.NPC.velocity.Y, 0f - base.NPC.velocity.X);
					if (base.NPC.type == 247)
					{
						base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
					}
				}
			}
			else if (base.NPC.ai[0] == 1f)
			{
				base.NPC.damage = 0;
				base.NPC.ai[1]++;
				base.NPC.Center = fistCenter;
				base.NPC.rotation = 0f;
				base.NPC.velocity = Vector2.Zero;
				if (base.NPC.ai[1] <= 15f)
				{
					for (int j = 0; j < 1; j++)
					{
						Vector2 largeRandDustRadius = Main.rand.NextVector2Circular(80f, 80f);
						Vector2 largeRandDustRecoil = largeRandDustRadius * -1f * 0.05f;
						Vector2 smallRandDustRadius = Main.rand.NextVector2Circular(20f, 20f);
						Dust dust = Dust.NewDustPerfect(base.NPC.Center + largeRandDustRecoil + largeRandDustRadius + smallRandDustRadius, 228, largeRandDustRecoil);
						dust.fadeIn = 1.5f;
						dust.scale = 0.5f;
						if (Main.getGoodWorld)
						{
							dust.noLight = true;
						}
						dust.noGravity = true;
					}
				}
				if (base.NPC.ai[1] >= 30f)
				{
					base.NPC.damage = base.NPC.defDamage;
					base.NPC.noTileCollide = true;
					base.NPC.collideX = false;
					base.NPC.collideY = false;
					float fistReturnSpeed = 24f;
					fistReturnSpeed *= (aggression + 3f) / 4f;
					if (fistReturnSpeed > 48f)
					{
						fistReturnSpeed = 48f;
					}
					Vector2 fistCent = base.NPC.Center;
					float fistTargetXDist = player.Center.X - fistCent.X;
					float fistTargetYDist = player.Center.Y - fistCent.Y;
					float fistTargetDistance = (float)Math.Sqrt(fistTargetXDist * fistTargetXDist + fistTargetYDist * fistTargetYDist);
					fistTargetDistance = fistReturnSpeed / fistTargetDistance;
					base.NPC.velocity.X = fistTargetXDist * fistTargetDistance;
					base.NPC.velocity.Y = fistTargetYDist * fistTargetDistance;
					base.NPC.ai[0] = 2f;
					base.NPC.ai[1] = 0f;
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
					if (base.NPC.type == 247)
					{
						base.NPC.rotation = (float)Math.Atan2(0f - base.NPC.velocity.Y, 0f - base.NPC.velocity.X);
					}
				}
			}
			else if (base.NPC.ai[0] == 2f)
			{
				base.NPC.damage = base.NPC.defDamage;
				if (Main.netMode != 1 && Main.getGoodWorld)
				{
					for (int k = (int)(base.NPC.position.X / 16f) - 1; (float)k < (base.NPC.position.X + (float)base.NPC.width) / 16f + 1f; k++)
					{
						for (int l = (int)(base.NPC.position.Y / 16f) - 1; (float)l < (base.NPC.position.Y + (float)base.NPC.width) / 16f + 1f; l++)
						{
							if (Main.tile[k, l].TileType == 4)
							{
								Main.tile[k, l].Get<TileWallWireStateData>().HasTile = false;
								if (Main.dedServ)
								{
									NetMessage.SendTileSquare(-1, k, l);
								}
							}
						}
					}
				}
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] == 1f)
				{
					SoundEngine.PlaySound(in SoundID.Item14, base.NPC.Center);
				}
				if (Main.rand.NextBool())
				{
					Vector2 halfVelocityDust = base.NPC.velocity * 0.5f;
					Vector2 randDustRadius = Main.rand.NextVector2Circular(20f, 20f);
					Dust.NewDustPerfect(base.NPC.Center + halfVelocityDust + randDustRadius, 306, halfVelocityDust, 0, Main.OurFavoriteColor).scale = 2f;
				}
				if (Math.Abs(base.NPC.velocity.X) > Math.Abs(base.NPC.velocity.Y))
				{
					if (base.NPC.velocity.X > 0f && base.NPC.Center.X > player.Center.X)
					{
						base.NPC.noTileCollide = false;
					}
					if (base.NPC.velocity.X < 0f && base.NPC.Center.X < player.Center.X)
					{
						base.NPC.noTileCollide = false;
					}
				}
				else
				{
					if (base.NPC.velocity.Y > 0f && base.NPC.Center.Y > player.Center.Y)
					{
						base.NPC.noTileCollide = false;
					}
					if (base.NPC.velocity.Y < 0f && base.NPC.Center.Y < player.Center.Y)
					{
						base.NPC.noTileCollide = false;
					}
				}
				float maxPunchDistance = 700f;
				if (death)
				{
					if (base.NPC.life < base.NPC.lifeMax / 2)
					{
						maxPunchDistance += MathHelper.Lerp(-175f, 75f, Main.rand.NextFloat());
					}
					if (base.NPC.life < base.NPC.lifeMax / 4)
					{
						maxPunchDistance += MathHelper.Lerp(-175f, 75f, Main.rand.NextFloat());
					}
				}
				if (distanceFromRestPosition > maxPunchDistance || base.NPC.collideX || base.NPC.collideY)
				{
					base.NPC.damage = 0;
					base.NPC.noTileCollide = true;
					base.NPC.ai[0] = 0f;
				}
			}
			else
			{
				if (base.NPC.ai[0] != 3f)
				{
					return false;
				}
				base.NPC.damage = base.NPC.defDamage;
				base.NPC.noTileCollide = true;
				float fistAcceleration = 0.4f;
				Vector2 returningFistCenter = base.NPC.Center;
				float returningTargetX = player.Center.X - returningFistCenter.X;
				float returningTargetY = player.Center.Y - returningFistCenter.Y;
				float returningTargetDist = (float)Math.Sqrt(returningTargetX * returningTargetX + returningTargetY * returningTargetY);
				returningTargetDist = 12f / returningTargetDist;
				returningTargetX *= returningTargetDist;
				returningTargetY *= returningTargetDist;
				if (base.NPC.velocity.X < returningTargetX)
				{
					base.NPC.velocity.X += fistAcceleration;
					if (base.NPC.velocity.X < 0f && returningTargetX > 0f)
					{
						base.NPC.velocity.X += fistAcceleration * 2f;
					}
				}
				else if (base.NPC.velocity.X > returningTargetX)
				{
					base.NPC.velocity.X -= fistAcceleration;
					if (base.NPC.velocity.X > 0f && returningTargetX < 0f)
					{
						base.NPC.velocity.X -= fistAcceleration * 2f;
					}
				}
				if (base.NPC.velocity.Y < returningTargetY)
				{
					base.NPC.velocity.Y += fistAcceleration;
					if (base.NPC.velocity.Y < 0f && returningTargetY > 0f)
					{
						base.NPC.velocity.Y += fistAcceleration * 2f;
					}
				}
				else if (base.NPC.velocity.Y > returningTargetY)
				{
					base.NPC.velocity.Y -= fistAcceleration;
					if (base.NPC.velocity.Y > 0f && returningTargetY < 0f)
					{
						base.NPC.velocity.Y -= fistAcceleration * 2f;
					}
				}
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
				if (base.NPC.type == 247)
				{
					base.NPC.rotation = (float)Math.Atan2(0f - base.NPC.velocity.Y, 0f - base.NPC.velocity.X);
				}
			}
			return false;
		}
	}

	public class HeadAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			//IL_0110: Unknown result type (might be due to invalid IL or missing references)
			//IL_0120: Unknown result type (might be due to invalid IL or missing references)
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_0140: Unknown result type (might be due to invalid IL or missing references)
			//IL_0145: Unknown result type (might be due to invalid IL or missing references)
			//IL_0168: Unknown result type (might be due to invalid IL or missing references)
			//IL_019c: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_048d: Unknown result type (might be due to invalid IL or missing references)
			//IL_049d: Unknown result type (might be due to invalid IL or missing references)
			//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_04df: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d51: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d81: Unknown result type (might be due to invalid IL or missing references)
			//IL_0539: Unknown result type (might be due to invalid IL or missing references)
			//IL_0549: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dbd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dc2: Unknown result type (might be due to invalid IL or missing references)
			//IL_067d: Unknown result type (might be due to invalid IL or missing references)
			//IL_068d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e7b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e83: Unknown result type (might be due to invalid IL or missing references)
			//IL_032a: Unknown result type (might be due to invalid IL or missing references)
			//IL_033a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0eb7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ed0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ed6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ed8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0387: Unknown result type (might be due to invalid IL or missing references)
			//IL_0391: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0eec: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ef1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f0a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f0c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f0e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f13: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f1d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f22: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f27: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_07de: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0803: Unknown result type (might be due to invalid IL or missing references)
			//IL_041a: Unknown result type (might be due to invalid IL or missing references)
			//IL_041c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0429: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b57: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b67: Unknown result type (might be due to invalid IL or missing references)
			//IL_098f: Unknown result type (might be due to invalid IL or missing references)
			//IL_099f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0867: Unknown result type (might be due to invalid IL or missing references)
			//IL_0869: Unknown result type (might be due to invalid IL or missing references)
			//IL_0876: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c0a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c14: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c2f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c39: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c8e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c90: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c95: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c9f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a1e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a28: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a43: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a4d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0abd: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.noTileCollide = true;
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				CalamityTargetingParameters options = CalamityTargetingParameters.BossDefaults;
				options.aggroRatio = -1f;
				options.finishThemOff = true;
				base.NPC.CalamityTargeting(options);
			}
			if (NPC.golemBoss < 0)
			{
				if (Main.netMode != 1)
				{
					base.NPC.StrikeInstantKill();
				}
				return false;
			}
			float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			bool leftFistAlive = NPC.AnyNPCs(247);
			bool rightFistAlive = NPC.AnyNPCs(248);
			base.NPC.dontTakeDamage = leftFistAlive | rightFistAlive;
			base.NPC.Center = Main.npc[NPC.golemBoss].Center - new Vector2(3f, 57f) * base.NPC.scale;
			base.NPC.velocity = Main.npc[NPC.golemBoss].velocity;
			bool enrage = !BossRushEvent.BossRushActive;
			bool turboEnrage = false;
			if ((double)Main.player[base.NPC.target].Center.Y > Main.worldSurface * 16.0 && !BossRushEvent.BossRushActive)
			{
				int i = (int)Main.player[base.NPC.target].Center.X / 16;
				int targetTilePosY = (int)Main.player[base.NPC.target].Center.Y / 16;
				if (Framing.GetTileSafely(i, targetTilePosY).WallType == 87)
				{
					enrage = false;
				}
				else
				{
					turboEnrage = Main.getGoodWorld;
				}
			}
			else
			{
				turboEnrage = Main.getGoodWorld;
			}
			if (Main.getGoodWorld)
			{
				enrage = true;
			}
			if (base.NPC.alpha > 0)
			{
				base.NPC.alpha -= 10;
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
				base.NPC.ai[1] = 30f;
			}
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.ai[1]++;
				float openMouthGateValue = ((!rightFistAlive || !leftFistAlive) ? 10f : 20f);
				float shootFireballGateValue = ((!rightFistAlive || !leftFistAlive) ? 60f : 120f);
				if (base.NPC.ai[1] < openMouthGateValue || base.NPC.ai[1] > shootFireballGateValue - openMouthGateValue)
				{
					base.NPC.localAI[0] = 1f;
				}
				else
				{
					base.NPC.localAI[0] = 0f;
				}
				if (Main.netMode != 1 && base.NPC.ai[1] >= shootFireballGateValue)
				{
					base.NPC.ai[1] = 0f;
					Vector2 headCent = default(Vector2);
					((Vector2)(ref headCent))._002Ector(base.NPC.Center.X, base.NPC.Center.Y + 10f * base.NPC.scale);
					float num = (turboEnrage ? 24f : (enrage ? 18f : 9f));
					float headFireballTargetX = Main.player[base.NPC.target].Center.X - headCent.X;
					float headFireballTargetY = Main.player[base.NPC.target].Center.Y - headCent.Y;
					float headFireballTargetDist = (float)Math.Sqrt(headFireballTargetX * headFireballTargetX + headFireballTargetY * headFireballTargetY);
					headFireballTargetDist = num / headFireballTargetDist;
					headFireballTargetX *= headFireballTargetDist;
					headFireballTargetY *= headFireballTargetDist;
					int type = 258;
					int damage = FireballDamage;
					int fireballAmount = ((!death) ? 1 : 2);
					Vector2 fireballVelocity = default(Vector2);
					((Vector2)(ref fireballVelocity))._002Ector(headFireballTargetX, headFireballTargetY);
					for (int j = 0; j < fireballAmount; j++)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), headCent, fireballVelocity * (1f / (float)(j + 1)), type, damage, 0f, Main.myPlayer);
					}
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[0] == 1f)
			{
				Vector2 projectileFirePos = default(Vector2);
				((Vector2)(ref projectileFirePos))._002Ector(base.NPC.Center.X, base.NPC.Center.Y + 10f * base.NPC.scale);
				if (Main.player[base.NPC.target].Center.X < base.NPC.Center.X - (float)base.NPC.width)
				{
					base.NPC.localAI[1] = -1f;
					projectileFirePos.X -= 40f * base.NPC.scale;
				}
				else if (Main.player[base.NPC.target].Center.X > base.NPC.Center.X + (float)base.NPC.width)
				{
					base.NPC.localAI[1] = 1f;
					projectileFirePos.X += 40f * base.NPC.scale;
				}
				else
				{
					base.NPC.localAI[1] = 0f;
				}
				base.NPC.ai[3]++;
				if (base.NPC.ai[3] >= 600f && Main.npc[NPC.golemBoss].velocity.Y == 0f && MathF.Abs(Main.npc[NPC.golemBoss].velocity.X) < 0.5f)
				{
					base.NPC.ai[0] = 2f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.localAI[1] = (Main.player[base.NPC.target].Center.X > base.NPC.Center.X).ToDirectionInt();
					base.NPC.netUpdate = true;
				}
				base.NPC.ai[1]++;
				float openMouthGateValue2 = 20f - (death ? (15f * (1f - lifeRatio / 2f)) : (10f * (1f - lifeRatio / 2f)));
				float shootFireballGateValue2 = 120f - (death ? (75f * (1f - lifeRatio / 2f)) : (50f * (1f - lifeRatio / 2f)));
				if (base.NPC.ai[1] < openMouthGateValue2 || base.NPC.ai[1] > shootFireballGateValue2 - openMouthGateValue2)
				{
					base.NPC.localAI[0] = 1f;
				}
				else
				{
					base.NPC.localAI[0] = 0f;
				}
				if (Main.netMode != 1 && base.NPC.ai[1] >= shootFireballGateValue2)
				{
					base.NPC.ai[1] = 0f;
					float num2 = (turboEnrage ? 28f : (enrage ? 21f : 10.5f));
					float fireballFistsDedTargetX = Main.player[base.NPC.target].Center.X - projectileFirePos.X;
					float fireballFistsDedTargetY = Main.player[base.NPC.target].Center.Y - projectileFirePos.Y;
					float fireballFistsDedTargetDist = (float)Math.Sqrt(fireballFistsDedTargetX * fireballFistsDedTargetX + fireballFistsDedTargetY * fireballFistsDedTargetY);
					fireballFistsDedTargetDist = num2 / fireballFistsDedTargetDist;
					fireballFistsDedTargetX *= fireballFistsDedTargetDist;
					fireballFistsDedTargetY *= fireballFistsDedTargetDist;
					int type2 = 258;
					int damage2 = FireballDamage;
					int fireballAmount2 = ((!death) ? 1 : 2);
					Vector2 fireballVelocity2 = default(Vector2);
					((Vector2)(ref fireballVelocity2))._002Ector(fireballFistsDedTargetX, fireballFistsDedTargetY);
					for (int k = 0; k < fireballAmount2; k++)
					{
						int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileFirePos, fireballVelocity2 * (1f / (float)(k + 1)), type2, damage2, 0f, Main.myPlayer);
						Main.projectile[proj].timeLeft = 225;
					}
					base.NPC.netUpdate = true;
				}
				float shootBoost2 = (death ? (4.5f * (1f - lifeRatio / 2f)) : (2.75f * (1f - lifeRatio / 2f)));
				base.NPC.ai[2] += 1f + shootBoost2;
				if (enrage)
				{
					base.NPC.ai[2] += 4f;
				}
				if (base.NPC.ai[2] >= 300f)
				{
					base.NPC.ai[2] = 0f;
					int projType = 259;
					int dmg = LaserDamage;
					if (base.NPC.localAI[1] == 0f)
					{
						Vector2 laserVelocity = default(Vector2);
						for (int l = 0; l < 2; l++)
						{
							((Vector2)(ref projectileFirePos))._002Ector(base.NPC.Center.X, base.NPC.Center.Y - 22f * base.NPC.scale);
							if (l == 0)
							{
								projectileFirePos.X -= 18f * base.NPC.scale;
							}
							else
							{
								projectileFirePos.X += 18f * base.NPC.scale;
							}
							float num3 = (death ? 15f : 12f);
							float laserTargetXDist = Main.player[base.NPC.target].Center.X - projectileFirePos.X;
							float laserTargetYDist = Main.player[base.NPC.target].Center.Y - projectileFirePos.Y;
							float laserTargetDistance = (float)Math.Sqrt(laserTargetXDist * laserTargetXDist + laserTargetYDist * laserTargetYDist);
							laserTargetDistance = num3 / laserTargetDistance;
							laserTargetXDist *= laserTargetDistance;
							laserTargetYDist *= laserTargetDistance;
							((Vector2)(ref laserVelocity))._002Ector(laserTargetXDist, laserTargetYDist);
							if (Main.netMode != 1)
							{
								int bodyLaser = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileFirePos + laserVelocity.SafeNormalize(Vector2.UnitY) * 40f, laserVelocity, projType, dmg, 0f, Main.myPlayer);
								Main.projectile[bodyLaser].timeLeft = (enrage ? 600 : 300);
								if (turboEnrage)
								{
									Main.projectile[bodyLaser].extraUpdates++;
								}
								base.NPC.netUpdate = true;
							}
						}
					}
					else if (base.NPC.localAI[1] != 0f)
					{
						((Vector2)(ref projectileFirePos))._002Ector(base.NPC.Center.X, base.NPC.Center.Y - 22f * base.NPC.scale);
						if (base.NPC.localAI[1] == -1f)
						{
							projectileFirePos.X -= 30f * base.NPC.scale;
						}
						else if (base.NPC.localAI[1] == 1f)
						{
							projectileFirePos.X += 30f * base.NPC.scale;
						}
						float num4 = (death ? 15f : 12f);
						float extraLaserTargetX = Main.player[base.NPC.target].Center.X - projectileFirePos.X;
						float extraLaserTargetY = Main.player[base.NPC.target].Center.Y - projectileFirePos.Y;
						float extraLaserTargetDist = (float)Math.Sqrt(extraLaserTargetX * extraLaserTargetX + extraLaserTargetY * extraLaserTargetY);
						extraLaserTargetDist = num4 / extraLaserTargetDist;
						extraLaserTargetX *= extraLaserTargetDist;
						extraLaserTargetY *= extraLaserTargetDist;
						Vector2 laserVelocity2 = default(Vector2);
						((Vector2)(ref laserVelocity2))._002Ector(extraLaserTargetX, extraLaserTargetY);
						if (Main.netMode != 1)
						{
							int extraLasers = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileFirePos + laserVelocity2.SafeNormalize(Vector2.UnitY) * 40f, laserVelocity2, projType, dmg, 0f, Main.myPlayer);
							Main.projectile[extraLasers].timeLeft = (enrage ? 600 : 300);
							if (turboEnrage)
							{
								Main.projectile[extraLasers].extraUpdates++;
							}
							base.NPC.netUpdate = true;
						}
					}
				}
			}
			else if (base.NPC.ai[0] == 2f || base.NPC.ai[0] == 3f)
			{
				int telegraphTime = 60;
				int endTime = 120;
				Vector2 spawnLocation = default(Vector2);
				((Vector2)(ref spawnLocation))._002Ector(base.NPC.Center.X + 30f * base.NPC.scale * base.NPC.localAI[1], base.NPC.Center.Y - 22f * base.NPC.scale);
				if (base.NPC.ai[1] == 1f)
				{
					GeneralParticleHandler.SpawnParticle(new SparkleParticle(spawnLocation, Vector2.Zero, Color.Yellow, Color.White, 1.25f * base.NPC.scale, telegraphTime, (float)Math.PI / 50f, 1f, AddativeBlend: true, needed: true));
				}
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] >= (float)telegraphTime && base.NPC.ai[1] < (float)endTime && base.NPC.ai[1] % 2f == 0f)
				{
					if (base.NPC.ai[1] % 10f == 0f)
					{
						SoundEngine.PlaySound(in SoundID.Item33, spawnLocation);
					}
					float laserFireAngle = MathHelper.ToRadians((base.NPC.ai[1] - (float)telegraphTime + 20f) * (death ? 2.45f : 2f));
					Vector2 laserVelocity3 = Vector2.UnitY.RotatedBy(laserFireAngle * (0f - base.NPC.localAI[1])) * (death ? 15f : 12f);
					if (Main.netMode != 1)
					{
						int extraLasers2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnLocation + laserVelocity3.SafeNormalize(Vector2.UnitY) * 40f, laserVelocity3, 259, LaserDamage, 0f, Main.myPlayer, 0f, 1f);
						Main.projectile[extraLasers2].timeLeft = (enrage ? 600 : 300);
						if (turboEnrage)
						{
							Main.projectile[extraLasers2].extraUpdates++;
						}
						base.NPC.netUpdate = true;
					}
				}
				if (death && base.NPC.ai[0] == 2f && base.NPC.ai[1] >= (float)endTime)
				{
					base.NPC.ai[0] = 3f;
					base.NPC.ai[1] = 0f;
					base.NPC.localAI[1] = 0f - base.NPC.localAI[1];
					base.NPC.netUpdate = true;
				}
				if (base.NPC.ai[1] >= (float)(endTime + 30))
				{
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			if (((!leftFistAlive && !rightFistAlive) | death) || Main.getGoodWorld)
			{
				if (base.NPC.ai[0] <= 1f)
				{
					base.NPC.ai[0] = 1f;
				}
			}
			else
			{
				base.NPC.ai[0] = 0f;
			}
			return false;
		}

		public override void FindFrame(Mod mod, int frameHeight)
		{
			if (base.NPC.ai[0] == 2f || base.NPC.ai[0] == 3f)
			{
				if (base.NPC.localAI[1] == 1f)
				{
					base.NPC.frame.Y = frameHeight * 2;
				}
				else
				{
					base.NPC.frame.Y = frameHeight * 4;
				}
			}
		}
	}

	public class HeadFreeAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_010e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0142: Unknown result type (might be due to invalid IL or missing references)
			//IL_0161: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0508: Unknown result type (might be due to invalid IL or missing references)
			//IL_050d: Unknown result type (might be due to invalid IL or missing references)
			//IL_05be: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_05db: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_065b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0644: Unknown result type (might be due to invalid IL or missing references)
			//IL_0649: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c5a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c65: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c6f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cb3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cb8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ce2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cf7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d04: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d0a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d18: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d23: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d28: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d2d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d81: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d86: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d90: Unknown result type (might be due to invalid IL or missing references)
			//IL_0da5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0df3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dfe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e03: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e08: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e14: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e1f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e24: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e29: Unknown result type (might be due to invalid IL or missing references)
			//IL_0913: Unknown result type (might be due to invalid IL or missing references)
			//IL_0918: Unknown result type (might be due to invalid IL or missing references)
			//IL_0922: Unknown result type (might be due to invalid IL or missing references)
			//IL_0932: Unknown result type (might be due to invalid IL or missing references)
			//IL_0937: Unknown result type (might be due to invalid IL or missing references)
			//IL_093c: Unknown result type (might be due to invalid IL or missing references)
			//IL_093e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0951: Unknown result type (might be due to invalid IL or missing references)
			//IL_0956: Unknown result type (might be due to invalid IL or missing references)
			//IL_073b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0746: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b05: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b15: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b84: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b89: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b90: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b92: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b94: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b99: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ba0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ba5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bc1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bc3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bc5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bca: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bde: Unknown result type (might be due to invalid IL or missing references)
			//IL_0975: Unknown result type (might be due to invalid IL or missing references)
			//IL_097a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0996: Unknown result type (might be due to invalid IL or missing references)
			//IL_0998: Unknown result type (might be due to invalid IL or missing references)
			//IL_09b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_075c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0782: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_07df: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_0800: Unknown result type (might be due to invalid IL or missing references)
			//IL_0805: Unknown result type (might be due to invalid IL or missing references)
			//IL_081e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0820: Unknown result type (might be due to invalid IL or missing references)
			//IL_0822: Unknown result type (might be due to invalid IL or missing references)
			//IL_0827: Unknown result type (might be due to invalid IL or missing references)
			//IL_0831: Unknown result type (might be due to invalid IL or missing references)
			//IL_0836: Unknown result type (might be due to invalid IL or missing references)
			//IL_083b: Unknown result type (might be due to invalid IL or missing references)
			CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				CalamityTargetingParameters options = CalamityTargetingParameters.BossDefaults;
				options.aggroRatio = -1f;
				options.finishThemOff = true;
				base.NPC.CalamityTargeting(options);
			}
			if (NPC.golemBoss < 0)
			{
				if (Main.netMode != 1)
				{
					base.NPC.StrikeInstantKill();
				}
				return false;
			}
			float golemLifeRatio = (float)Main.npc[NPC.golemBoss].life / (float)Main.npc[NPC.golemBoss].lifeMax;
			float PosDelay = 270f;
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			bool phase2 = golemLifeRatio < 0.66f;
			bool phase3 = golemLifeRatio < 0.33f;
			bool enrage = !BossRushEvent.BossRushActive;
			bool turboEnrage = false;
			if ((double)Main.player[base.NPC.target].Center.Y > Main.worldSurface * 16.0 && !BossRushEvent.BossRushActive)
			{
				int i = (int)Main.player[base.NPC.target].Center.X / 16;
				int targetTilePosY = (int)Main.player[base.NPC.target].Center.Y / 16;
				if (Framing.GetTileSafely(i, targetTilePosY).WallType == 87)
				{
					enrage = false;
				}
				else
				{
					turboEnrage = Main.getGoodWorld;
				}
			}
			else
			{
				turboEnrage = Main.getGoodWorld;
			}
			if (Main.getGoodWorld)
			{
				enrage = true;
			}
			base.NPC.noTileCollide = !Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1) | phase2 | turboEnrage;
			if ((phase2 && base.NPC.ai[0] == 0f) || (phase3 && base.NPC.ai[0] == 1f))
			{
				base.NPC.ai[0] = 3f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = PosDelay;
				calamityGlobalNPC.newAI[3] = 0f;
				base.NPC.netUpdate = true;
				base.NPC.SyncExtraAI();
			}
			float maxDistanceDiagonal = 360f;
			float maxDistanceStraight = 480f;
			if (base.NPC.ai[3] <= 0f)
			{
				base.NPC.ai[3] = PosDelay;
				calamityGlobalNPC.newAI[3] += (phase2 ? 1 : 0);
				if (calamityGlobalNPC.newAI[3] >= 7f)
				{
					calamityGlobalNPC.newAI[3] = 0f;
					base.NPC.ai[0] = 3f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[3] = PosDelay;
				}
				if (!(phase3 | turboEnrage))
				{
					if (phase2)
					{
						switch ((int)(calamityGlobalNPC.newAI[3] % 4f))
						{
						case 0:
							calamityGlobalNPC.newAI[0] = 0f - maxDistanceDiagonal;
							calamityGlobalNPC.newAI[1] = 0f - maxDistanceDiagonal;
							break;
						case 1:
							calamityGlobalNPC.newAI[0] = 0f - maxDistanceDiagonal;
							calamityGlobalNPC.newAI[1] = maxDistanceDiagonal;
							break;
						case 2:
							calamityGlobalNPC.newAI[0] = maxDistanceDiagonal;
							calamityGlobalNPC.newAI[1] = maxDistanceDiagonal;
							break;
						case 3:
							calamityGlobalNPC.newAI[0] = maxDistanceDiagonal;
							calamityGlobalNPC.newAI[1] = 0f - maxDistanceDiagonal;
							break;
						}
					}
					else
					{
						calamityGlobalNPC.newAI[0] = Main.rand.NextFloat(-150f, 150f);
						calamityGlobalNPC.newAI[1] = (0f - maxDistanceStraight) * 0.85f;
					}
				}
				base.NPC.netSpam = 5;
				base.NPC.SyncExtraAI();
				base.NPC.ForceNetUpdate();
			}
			float positioningInc = (enrage ? 6f : (phase3 ? 2.5f : (phase2 ? 1.8f : 1f)));
			base.NPC.ai[3] -= positioningInc;
			if (phase3 | turboEnrage)
			{
				float spinSpeedMult = (enrage ? 2.5f : (death ? 1.35f : 1.2f));
				calamityGlobalNPC.newAI[0] = maxDistanceStraight * MathF.Sin(MathHelper.ToRadians(base.NPC.ai[2] * spinSpeedMult));
				calamityGlobalNPC.newAI[1] = maxDistanceStraight * MathF.Cos(MathHelper.ToRadians(base.NPC.ai[2] * spinSpeedMult));
			}
			if (base.NPC.ai[0] == 3f)
			{
				calamityGlobalNPC.newAI[0] = 0f;
				calamityGlobalNPC.newAI[1] = (0f - maxDistanceStraight) * 0.8f;
			}
			float offsetX = calamityGlobalNPC.newAI[0];
			float offsetY = calamityGlobalNPC.newAI[1];
			Vector2 val = Main.player[base.NPC.target].Center + new Vector2(offsetX, offsetY);
			float velocity = (turboEnrage ? 15f : 10f) + (turboEnrage ? 7.5f : (phase2 ? 10f : 0f)) + (turboEnrage ? 7.5f : (phase3 ? 15f : 0f));
			if (enrage)
			{
				velocity = ((phase3 | turboEnrage) ? 35f : 25f);
			}
			float acceleration = ((base.NPC.ai[0] == 3f) ? 1.5f : (phase2 ? 0f : (turboEnrage ? 5f : (enrage ? 3f : 0.3f))));
			Vector2 distanceFromDestination = val - base.NPC.Center;
			bool canFireProjectiles = (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 160f && base.NPC.ai[0] != 3f) | enrage;
			if (base.NPC.ai[0] == 3f && base.NPC.ai[1] >= 60f)
			{
				base.NPC.velocity = Vector2.Zero;
			}
			else
			{
				CalamityUtils.SmoothMovement(base.NPC, 80f, distanceFromDestination, velocity, acceleration, !phase2 || base.NPC.ai[0] == 3f);
			}
			if (base.NPC.ai[0] == 3f)
			{
				int telegraphTime = 60;
				int laserEndTime = 120;
				int fireballStartTime = 160;
				base.NPC.ai[1]++;
				base.NPC.ai[3] = PosDelay;
				if (base.NPC.ai[1] >= (float)telegraphTime && base.NPC.ai[1] < (float)laserEndTime && base.NPC.ai[1] % 2f == 0f)
				{
					if (base.NPC.ai[1] % 10f == 0f)
					{
						SoundEngine.PlaySound(in SoundID.Item33, base.NPC.Center);
					}
					Vector2 spawnLocation = default(Vector2);
					for (int j = -1; j <= 1; j += 2)
					{
						((Vector2)(ref spawnLocation))._002Ector(base.NPC.Center.X + 14f * base.NPC.scale * (float)j, base.NPC.Center.Y - 20f * base.NPC.scale);
						float laserFireAngle = MathHelper.ToRadians((base.NPC.ai[1] - (float)telegraphTime + 20f) * (death ? 2.1f : 2f));
						Vector2 laserVelocity = -Vector2.UnitY.RotatedBy(laserFireAngle * (float)j) * (death ? 15f : 12f);
						if (Main.netMode != 1)
						{
							int spreadLasers = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnLocation + laserVelocity.SafeNormalize(Vector2.UnitY) * 40f, laserVelocity, 259, LaserDamage, 0f, Main.myPlayer, 0f, 1f);
							Main.projectile[spreadLasers].timeLeft = (enrage ? 600 : 300);
							if (turboEnrage)
							{
								Main.projectile[spreadLasers].extraUpdates++;
							}
							base.NPC.netUpdate = true;
						}
					}
				}
				if (base.NPC.ai[1] > (float)laserEndTime)
				{
					base.NPC.localAI[0] = 1f;
				}
				if (base.NPC.ai[1] >= (float)fireballStartTime && base.NPC.ai[1] % 30f == 10f)
				{
					Vector2 spawnLocation2 = base.NPC.Center + Vector2.UnitY * 20f * base.NPC.scale;
					Vector2 fireBoltVelocity = spawnLocation2.DirectionTo(Main.player[base.NPC.target].Center) * (enrage ? 30f : (death ? 16f : 12f));
					int type = ModContent.ProjectileType<GolemInfernoBolt>();
					int damage = InfernoBoltDamage;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnLocation2, fireBoltVelocity, type, damage, 0f, Main.myPlayer, Main.player[base.NPC.target].Center.X, Main.player[base.NPC.target].Center.Y);
				}
				if (base.NPC.ai[1] >= (float)(fireballStartTime + (death ? 40 : 10)))
				{
					base.NPC.ai[0] = (phase3 ? 2f : 1f);
					base.NPC.ai[1] = 0f;
					if (phase3)
					{
						calamityGlobalNPC.newAI[2] = Main.rand.Next(120);
					}
					base.NPC.ai[2] = calamityGlobalNPC.newAI[2];
					base.NPC.localAI[0] = 0f;
					calamityGlobalNPC.newAI[0] = 0f - maxDistanceDiagonal;
					base.NPC.netUpdate = true;
					base.NPC.SyncExtraAI();
				}
			}
			base.NPC.ai[2]++;
			int laserGateValue = (int)(PosDelay / positioningInc);
			if (canFireProjectiles && Main.netMode != 1 && (base.NPC.ai[2] - calamityGlobalNPC.newAI[2]) % (float)(laserGateValue / 2) == 0f)
			{
				int numLasers = 2;
				Vector2 freeHeadProjSpawn = default(Vector2);
				for (int k = 0; k < numLasers; k++)
				{
					((Vector2)(ref freeHeadProjSpawn))._002Ector(base.NPC.Center.X, base.NPC.Center.Y - 20f * base.NPC.scale);
					freeHeadProjSpawn.X += 14f * base.NPC.scale * (float)(k == 1).ToDirectionInt();
					float freeHeadProjSpeed = 7f + 5f * (1f - golemLifeRatio);
					Vector2 laserVelocity2 = Main.player[base.NPC.target].Center - freeHeadProjSpawn;
					laserVelocity2 = laserVelocity2.SafeNormalize(Vector2.UnitY) * freeHeadProjSpeed;
					int type2 = 259;
					int damage2 = LaserDamage;
					int freeHeadLaser = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), freeHeadProjSpawn + laserVelocity2.SafeNormalize(Vector2.UnitY) * 40f, laserVelocity2, type2, damage2, 0f, Main.myPlayer);
					Main.projectile[freeHeadLaser].timeLeft = (enrage ? 600 : 300);
					if (turboEnrage)
					{
						Main.projectile[freeHeadLaser].extraUpdates++;
					}
				}
			}
			if (!Main.getGoodWorld)
			{
				NPC nPC = base.NPC;
				nPC.position += base.NPC.netOffset;
				int randDustOffset = Main.rand.Next(2) * 2 - 1;
				Dust dust = Dust.NewDustPerfect(base.NPC.Bottom + new Vector2((float)(randDustOffset * 22) * base.NPC.scale, -22f * base.NPC.scale), 228, ((float)Math.PI / 2f + -(float)Math.PI / 2f * (float)randDustOffset + Main.rand.NextFloatDirection() * ((float)Math.PI / 4f)).ToRotationVector2() * (2f + Main.rand.NextFloat()));
				dust.velocity += base.NPC.velocity;
				dust.noGravity = true;
				Dust dust2 = Dust.NewDustPerfect(base.NPC.Bottom + new Vector2(Main.rand.NextFloatDirection() * 6f * base.NPC.scale, (Main.rand.NextFloat() * -4f - 8f) * base.NPC.scale), 228, Vector2.UnitY * (2f + Main.rand.NextFloat()));
				dust2.fadeIn = 0f;
				dust2.scale = 0.7f + Main.rand.NextFloat() * 0.5f;
				dust2.noGravity = true;
				dust2.velocity += base.NPC.velocity;
				NPC nPC2 = base.NPC;
				nPC2.position -= base.NPC.netOffset;
			}
			return false;
		}

		public override bool PreDraw(Mod mod, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_0105: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_014d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0152: Unknown result type (might be due to invalid IL or missing references)
			//IL_015d: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Unknown result type (might be due to invalid IL or missing references)
			//IL_017e: Unknown result type (might be due to invalid IL or missing references)
			//IL_018e: Unknown result type (might be due to invalid IL or missing references)
			//IL_018f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0196: Unknown result type (might be due to invalid IL or missing references)
			//IL_019c: Unknown result type (might be due to invalid IL or missing references)
			//IL_019e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_026a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0279: Unknown result type (might be due to invalid IL or missing references)
			//IL_0289: Unknown result type (might be due to invalid IL or missing references)
			//IL_028e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0293: Unknown result type (might be due to invalid IL or missing references)
			//IL_0295: Unknown result type (might be due to invalid IL or missing references)
			//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
			Texture2D golemHeadTexture = TextureAssets.Npc[base.NPC.type].Value;
			Vector2 headDrawPosition = base.NPC.Center - screenPos;
			spriteBatch.Draw(golemHeadTexture, headDrawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), 0f, base.NPC.frame.Size() * 0.5f, base.NPC.scale, (SpriteEffects)0, 0f);
			Color eyeColor = default(Color);
			((Color)(ref eyeColor))._002Ector((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, 0);
			Vector2 eyesDrawPosition = headDrawPosition - base.NPC.scale * new Vector2(1f, 12f);
			Rectangle eyesFrame = default(Rectangle);
			((Rectangle)(ref eyesFrame))._002Ector(0, 0, TextureAssets.Golem[1].Value.Width, TextureAssets.Golem[1].Value.Height / 2);
			spriteBatch.Draw(TextureAssets.Golem[1].Value, eyesDrawPosition, (Rectangle?)eyesFrame, eyeColor, 0f, eyesFrame.Size() * 0.5f, base.NPC.scale, (SpriteEffects)0, 0f);
			int frameCounter = (int)base.NPC.frameCounter / 4;
			Rectangle frame = TextureAssets.Extra[106].Value.Frame(1, 8);
			frame.Y += frame.Height * 2 * frameCounter + base.NPC.frame.Y;
			Rectangle glowFrame = frame;
			spriteBatch.Draw(TextureAssets.Extra[106].Value, eyesDrawPosition, (Rectangle?)glowFrame, eyeColor, 0f, glowFrame.Size() * 0.5f, base.NPC.scale, (SpriteEffects)0, 0f);
			frame = base.NPC.frame;
			Rectangle glowFrame2 = frame;
			spriteBatch.Draw(TextureAssets.Extra[107].Value, eyesDrawPosition, (Rectangle?)glowFrame2, eyeColor, 0f, glowFrame2.Size() * 0.5f, base.NPC.scale, (SpriteEffects)0, 0f);
			if (base.NPC.ai[0] == 3f && base.NPC.ai[1] <= 60f)
			{
				spriteBatch.SetBlendState(BlendState.Additive);
				for (int i = -1; i <= 1; i += 2)
				{
					Texture2D sparkle = ModContent.Request<Texture2D>("CalamityMod/Particles/Sparkle2", (AssetRequestMode)2).Value;
					Vector2 sparkleDraw = headDrawPosition + new Vector2(14f * (float)i, -15f) * base.NPC.scale;
					Color drawFade = Color.Yellow * Utils.GetLerpValue(0f, 30f, 60f - base.NPC.ai[1], clamped: true);
					spriteBatch.Draw(sparkle, sparkleDraw, (Rectangle?)null, drawFade, (float)Math.PI / 50f * base.NPC.ai[1] * (float)i, sparkle.Size() / 2f, 1.25f * base.NPC.scale, (SpriteEffects)0, 0f);
				}
				spriteBatch.SetBlendState(BlendState.AlphaBlend);
			}
			return false;
		}
	}

	public static int FireballDamage = 24;

	public static int LaserDamage = 29;

	public static int InfernoBoltDamage = 35;

	public override bool AI(Mod mod)
	{
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_074d: Unknown result type (might be due to invalid IL or missing references)
		//IL_077e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0788: Unknown result type (might be due to invalid IL or missing references)
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_092a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0956: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_090a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_0821: Unknown result type (might be due to invalid IL or missing references)
		//IL_0836: Unknown result type (might be due to invalid IL or missing references)
		//IL_083c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ceb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_096a: Unknown result type (might be due to invalid IL or missing references)
		//IL_166d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1688: Unknown result type (might be due to invalid IL or missing references)
		//IL_169e: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_1708: Unknown result type (might be due to invalid IL or missing references)
		//IL_171e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1739: Unknown result type (might be due to invalid IL or missing references)
		//IL_1371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1023: Unknown result type (might be due to invalid IL or missing references)
		//IL_104d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1053: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1104: Unknown result type (might be due to invalid IL or missing references)
		//IL_1109: Unknown result type (might be due to invalid IL or missing references)
		//IL_110e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1117: Unknown result type (might be due to invalid IL or missing references)
		//IL_111b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1120: Unknown result type (might be due to invalid IL or missing references)
		//IL_153d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1558: Unknown result type (might be due to invalid IL or missing references)
		//IL_1167: Unknown result type (might be due to invalid IL or missing references)
		//IL_117f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1185: Unknown result type (might be due to invalid IL or missing references)
		//IL_1187: Unknown result type (might be due to invalid IL or missing references)
		//IL_118c: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11be: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11da: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1202: Unknown result type (might be due to invalid IL or missing references)
		//IL_1207: Unknown result type (might be due to invalid IL or missing references)
		GolemAI golemAI = this;
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		NPC.golemBoss = base.NPC.whoAmI;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (base.NPC.localAI[0] == 0f && Main.netMode != 1)
		{
			base.NPC.localAI[0] = 1f;
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X - 84, (int)base.NPC.Center.Y - 9, 247);
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X + 78, (int)base.NPC.Center.Y - 9, 248);
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X - 3, (int)base.NPC.Center.Y - 57, 246);
		}
		if (base.NPC.target >= 0 && Main.player[base.NPC.target].dead)
		{
			base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
			if (Main.player[base.NPC.target].dead)
			{
				base.NPC.noTileCollide = true;
			}
		}
		bool enrage = !BossRushEvent.BossRushActive;
		bool turboEnrage = false;
		if ((double)Main.player[base.NPC.target].Center.Y > Main.worldSurface * 16.0)
		{
			int i = (int)Main.player[base.NPC.target].Center.X / 16;
			int targetTilePosY = (int)Main.player[base.NPC.target].Center.Y / 16;
			if (Framing.GetTileSafely(i, targetTilePosY).WallType == 87)
			{
				enrage = false;
			}
			else
			{
				turboEnrage = Main.getGoodWorld;
			}
		}
		else
		{
			turboEnrage = Main.getGoodWorld;
		}
		if (Main.getGoodWorld)
		{
			enrage = true;
		}
		base.NPC.Calamity().CurrentlyEnraged = !BossRushEvent.BossRushActive && (enrage | turboEnrage);
		bool reduceFallSpeed = base.NPC.velocity.Y > 0f && Collision.SolidCollision(base.NPC.position + Vector2.UnitY * 1.1f * base.NPC.velocity.Y, base.NPC.width, base.NPC.height);
		if (base.NPC.alpha > 0)
		{
			base.NPC.alpha -= 10;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
			base.NPC.ai[1] = 0f;
		}
		bool headAlive = NPC.AnyNPCs(246);
		bool leftFistAlive = NPC.AnyNPCs(247);
		bool rightFistAlive = NPC.AnyNPCs(248);
		base.NPC.dontTakeDamage = headAlive | leftFistAlive | rightFistAlive;
		int despawnDistance = (turboEnrage ? 7500 : (enrage ? 6000 : 4500));
		if (Main.netMode != 1 && Main.getGoodWorld && base.NPC.velocity.Y > 0f)
		{
			for (int j = (int)(base.NPC.position.X / 16f); (float)j < (base.NPC.position.X + (float)base.NPC.width) / 16f; j++)
			{
				for (int k = (int)(base.NPC.position.Y / 16f); (float)k < (base.NPC.position.Y + (float)base.NPC.width) / 16f; k++)
				{
					if (Main.tile[j, k].TileType == 4)
					{
						Main.tile[j, k].Get<TileWallWireStateData>().HasTile = false;
						if (Main.dedServ)
						{
							NetMessage.SendTileSquare(-1, j, k);
						}
					}
				}
			}
		}
		if (!Main.getGoodWorld)
		{
			if (!leftFistAlive)
			{
				int lostLeftFistDust = Dust.NewDust(new Vector2(base.NPC.Center.X - 80f * base.NPC.scale, base.NPC.Center.Y - 9f), 8, 8, 31, 0f, 0f, 100);
				Dust obj = Main.dust[lostLeftFistDust];
				obj.alpha += Main.rand.Next(100);
				obj.velocity *= 0.2f;
				obj.velocity.Y -= 0.5f + (float)Main.rand.Next(10) * 0.1f;
				obj.fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
				if (Main.rand.NextBool(10))
				{
					lostLeftFistDust = Dust.NewDust(new Vector2(base.NPC.Center.X - 80f * base.NPC.scale, base.NPC.Center.Y - 9f), 8, 8, 6);
					if (!Main.rand.NextBool(20))
					{
						Main.dust[lostLeftFistDust].noGravity = true;
						Dust obj2 = Main.dust[lostLeftFistDust];
						obj2.scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
						obj2.velocity.Y--;
					}
				}
			}
			if (!rightFistAlive)
			{
				int lostRightFistDust = Dust.NewDust(new Vector2(base.NPC.Center.X + 62f * base.NPC.scale, base.NPC.Center.Y - 9f), 8, 8, 31, 0f, 0f, 100);
				Dust obj3 = Main.dust[lostRightFistDust];
				obj3.alpha += Main.rand.Next(100);
				obj3.velocity *= 0.2f;
				obj3.velocity.Y -= 0.5f + (float)Main.rand.Next(10) * 0.1f;
				obj3.fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
				if (Main.rand.NextBool(10))
				{
					lostRightFistDust = Dust.NewDust(new Vector2(base.NPC.Center.X + 62f * base.NPC.scale, base.NPC.Center.Y - 9f), 8, 8, 6);
					if (!Main.rand.NextBool(20))
					{
						Main.dust[lostRightFistDust].noGravity = true;
						Dust obj4 = Main.dust[lostRightFistDust];
						obj4.scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
						obj4.velocity.Y--;
					}
				}
			}
		}
		if (base.NPC.noTileCollide && !Main.player[base.NPC.target].dead)
		{
			if (base.NPC.velocity.Y > 0f && base.NPC.Bottom.Y > Main.player[base.NPC.target].Top.Y)
			{
				base.NPC.noTileCollide = false;
			}
			else if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].Center, 1, 1) && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.noTileCollide = false;
			}
		}
		bool canJump;
		float[] ai;
		float num2;
		if (base.NPC.ai[0] == 0f)
		{
			if (base.NPC.velocity.Y == 0f || base.NPC.ai[2] > 0f)
			{
				base.NPC.damage = 0;
				if (base.NPC.ai[2] == 0f)
				{
					base.NPC.velocity.X *= 0.8f;
					base.NPC.ai[1]++;
				}
				if (base.NPC.ai[1] > 0f)
				{
					base.NPC.ai[1] += (death ? 1.5f : 1f);
					if (Main.getGoodWorld)
					{
						base.NPC.ai[1] += 100f;
					}
					if (enrage | death)
					{
						base.NPC.ai[1] += 18f;
					}
					else
					{
						if (!leftFistAlive)
						{
							base.NPC.ai[1] += 6f;
						}
						if (!rightFistAlive)
						{
							base.NPC.ai[1] += 6f;
						}
					}
				}
				canJump = (!headAlive || Main.npc[NPC.FindFirstNPC(246)].ai[0] <= 1f) && (!NPC.AnyNPCs(249) || Main.npc[NPC.FindFirstNPC(249)].ai[0] != 3f);
				if ((base.NPC.ai[1] >= 300f) & canJump)
				{
					base.NPC.ai[1] = -20f;
					base.NPC.frameCounter = 0.0;
				}
				else if (base.NPC.ai[1] == -1f)
				{
					if (!headAlive)
					{
						base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
					}
					base.NPC.damage = base.NPC.defDamage;
					if (base.NPC.ai[3] == 0f)
					{
						ai = base.NPC.ai;
						bool num;
						if (!death)
						{
							num = !headAlive;
						}
						else
						{
							if (leftFistAlive)
							{
								goto IL_0c22;
							}
							num = !rightFistAlive;
						}
						if (!num)
						{
							goto IL_0c22;
						}
						num2 = Main.rand.Next(1, 3);
						goto IL_0c36;
					}
					goto IL_0c37;
				}
			}
			goto IL_0c67;
		}
		if (base.NPC.ai[0] == 1f)
		{
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				SoundEngine.PlaySound(in SoundID.Item14, base.NPC.Center);
				base.NPC.ai[0] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				calamityGlobalNPC.newAI[1] = 0f;
				base.NPC.SyncExtraAI();
				for (int l = (int)base.NPC.position.X - 20; l < (int)base.NPC.position.X + base.NPC.width + 40; l += 20)
				{
					for (int m = 0; m < 4; m++)
					{
						int fallDust = Dust.NewDust(new Vector2(base.NPC.position.X - 20f, base.NPC.position.Y + (float)base.NPC.height), base.NPC.width + 20, 4, 31, 0f, 0f, 100, default(Color), 1.5f);
						Dust obj5 = Main.dust[fallDust];
						obj5.velocity *= 0.2f;
					}
					if (!Main.dedServ)
					{
						int fallGore = Gore.NewGore(base.NPC.GetSource_FromAI(), new Vector2((float)(l - 20), base.NPC.position.Y + (float)base.NPC.height - 8f), default(Vector2), Main.rand.Next(61, 64));
						Gore obj6 = Main.gore[fallGore];
						obj6.velocity *= 0.4f;
					}
				}
				if (Main.netMode != 1 && (!headAlive | turboEnrage))
				{
					for (int n = 0; n < 10; n++)
					{
						int fiery = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, 0f, 0f, 100, default(Color), 2f);
						Main.dust[fiery].velocity.Y *= 6f;
						Main.dust[fiery].velocity.X *= 3f;
						if (Main.rand.NextBool())
						{
							Main.dust[fiery].scale = 0.5f;
							Main.dust[fiery].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}
					for (int num3 = 0; num3 < 20; num3++)
					{
						int fiery2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, 0f, 0f, 100, default(Color), 3f);
						Main.dust[fiery2].noGravity = true;
						Main.dust[fiery2].velocity.Y *= 10f;
						fiery2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, 0f, 0f, 100, default(Color), 2f);
						Main.dust[fiery2].velocity.X *= 2f;
					}
					float projectileVelocity = (death ? 7.5f : 4.75f);
					if (enrage)
					{
						projectileVelocity *= 1.5f;
					}
					if (turboEnrage)
					{
						projectileVelocity *= 1.25f;
					}
					int type = 258;
					int damage = FireballDamage;
					Vector2 destination = new Vector2(base.NPC.Center.X, base.NPC.Center.Y - 100f) - base.NPC.Center;
					((Vector2)(ref destination)).Normalize();
					destination *= projectileVelocity;
					int totalFireballsPerSide = 3;
					int totalIterations = (turboEnrage ? 11 : (death ? 40 : 60));
					float rotation = MathHelper.ToRadians(90f);
					for (int num4 = 0; num4 < totalIterations; num4++)
					{
						if (num4 < totalFireballsPerSide || num4 >= totalIterations - totalFireballsPerSide)
						{
							Vector2 perturbedSpeed = destination.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)num4 / (float)(totalIterations - 1)));
							int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.UnitY * ((float)(base.NPC.height / 2) * 0.8f) * base.NPC.scale + Vector2.Normalize(perturbedSpeed) * (float)(base.NPC.width / 3) * base.NPC.scale, perturbedSpeed, type, damage, 0f, Main.myPlayer);
							Main.projectile[proj].timeLeft = (enrage ? 480 : 150);
							if (turboEnrage)
							{
								Main.projectile[proj].extraUpdates++;
							}
						}
					}
					base.NPC.netUpdate = true;
				}
			}
			else
			{
				base.NPC.damage = base.NPC.defDamage;
				if ((base.NPC.position.X < Main.player[base.NPC.target].position.X && base.NPC.position.X + (float)base.NPC.width > Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width) || base.NPC.ai[2] == 1f)
				{
					base.NPC.velocity.X *= ((base.NPC.ai[2] == 1f) ? 0.5f : 0.8f);
					if (base.NPC.Bottom.Y < Main.player[base.NPC.target].position.Y || base.NPC.ai[2] == 1f)
					{
						float fallSpeedBoost = (death ? (0.9f * (1f - lifeRatio / 2f)) : (0.75f * (1f - lifeRatio / 2f)));
						float fallSpeed = (death ? 0.3f : 0.2f) + fallSpeedBoost;
						if (enrage)
						{
							fallSpeed *= 2f;
						}
						base.NPC.velocity.Y += fallSpeed;
					}
				}
				else
				{
					float velocityChangeBoost = (death ? (0.16f * (1f - lifeRatio / 2f)) : (0.12f * (1f - lifeRatio / 2f)));
					float velocityXChange = (death ? 0.285f : 0.2f) + velocityChangeBoost;
					if (base.NPC.direction < 0)
					{
						base.NPC.velocity.X -= velocityXChange;
					}
					else if (base.NPC.direction > 0)
					{
						base.NPC.velocity.X += velocityXChange;
					}
					float velocityBoost = (death ? (5.75f * (1f - lifeRatio / 2f)) : (4f * (1f - lifeRatio / 2f)));
					float velocityXCap = (death ? 6f : 4f) + velocityBoost;
					if (enrage)
					{
						velocityXCap *= 3f;
					}
					if ((float)((base.NPC.Center.X - Main.player[base.NPC.target].Center.X < 0f) ? 1 : (-1)) != calamityGlobalNPC.newAI[1])
					{
						velocityXCap *= (enrage ? 0.2f : 0.5f);
					}
					if (base.NPC.velocity.X < 0f - velocityXCap)
					{
						base.NPC.velocity.X = 0f - velocityXCap;
					}
					if (base.NPC.velocity.X > velocityXCap)
					{
						base.NPC.velocity.X = velocityXCap;
					}
				}
				CustomGravity(base.NPC.ai[2] == 1f);
			}
		}
		goto IL_1606;
		IL_1606:
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
		}
		if (Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) + Math.Abs(base.NPC.Center.Y - Main.player[base.NPC.target].Center.Y) > (float)despawnDistance)
		{
			base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
			if (Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) + Math.Abs(base.NPC.Center.Y - Main.player[base.NPC.target].Center.Y) > (float)despawnDistance)
			{
				base.NPC.active = false;
				base.NPC.netUpdate = true;
			}
		}
		return false;
		IL_0c36:
		ai[3] = num2;
		goto IL_0c37;
		IL_0c37:
		int num5 = (int)base.NPC.ai[3];
		if ((uint)num5 <= 1u || num5 != 2)
		{
			NormalJump(canJump);
		}
		else
		{
			SlamJump(canJump);
		}
		goto IL_0c67;
		IL_0c67:
		if (base.NPC.ai[0] != 1f && base.NPC.ai[2] == 0f)
		{
			CustomGravity(isSlamming: false);
		}
		goto IL_1606;
		IL_0c22:
		num2 = 1f;
		goto IL_0c36;
		void CustomGravity(bool isSlamming)
		{
			float gravity = (turboEnrage ? 0.85f : (enrage ? 0.75f : ((!leftFistAlive && !rightFistAlive) ? 0.45f : 0.3f)));
			float maxFallSpeed = (reduceFallSpeed ? 12f : (turboEnrage ? 30f : (enrage ? 25f : ((!leftFistAlive && !rightFistAlive) ? 15f : 10f))));
			if (isSlamming && !reduceFallSpeed)
			{
				gravity *= 4f;
				maxFallSpeed *= 2f;
			}
			base.NPC.velocity.Y += gravity;
			if (base.NPC.velocity.Y > maxFallSpeed)
			{
				base.NPC.velocity.Y = maxFallSpeed;
			}
		}
		void NormalJump(bool jump)
		{
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
			if (jump)
			{
				float velocityBoost2 = (death ? 5f : 3.8f) * (1f - lifeRatio / 2f);
				float velocityX = (death ? 6f : 4f) + velocityBoost2;
				if (enrage)
				{
					velocityX *= 1.5f;
				}
				float playerLocation = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
				base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
				calamityGlobalNPC.newAI[1] = base.NPC.direction;
				base.NPC.velocity.X = velocityX * (float)base.NPC.direction;
				float distanceBelowTarget = base.NPC.position.Y - (Main.player[base.NPC.target].position.Y + 80f);
				float speedMult = 1f;
				float multiplier = (turboEnrage ? 0.00275f : (enrage ? 0.0025f : 0.00175f));
				if (distanceBelowTarget > 0f && ((!leftFistAlive && !rightFistAlive) | turboEnrage))
				{
					speedMult += distanceBelowTarget * multiplier;
				}
				float speedMultLimit = (turboEnrage ? 3.25f : (enrage ? 3f : 2.5f));
				if (speedMult > speedMultLimit)
				{
					speedMult = speedMultLimit;
				}
				if (Main.player[base.NPC.target].position.Y < base.NPC.Bottom.Y)
				{
					base.NPC.velocity.Y = ((turboEnrage ? (-15.5f) : (-11.75f)) + (enrage ? (-4f) : 0f)) * speedMult;
				}
				else
				{
					base.NPC.velocity.Y = 1f;
				}
				base.NPC.noTileCollide = true;
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
				base.NPC.SyncExtraAI();
			}
		}
		void SlamJump(bool jump)
		{
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_011f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0202: Unknown result type (might be due to invalid IL or missing references)
			//IL_0207: Unknown result type (might be due to invalid IL or missing references)
			//IL_0213: Unknown result type (might be due to invalid IL or missing references)
			//IL_0219: Unknown result type (might be due to invalid IL or missing references)
			//IL_021e: Unknown result type (might be due to invalid IL or missing references)
			//IL_015b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0176: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0303: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.noTileCollide = true;
			base.NPC.ai[2]++;
			float jumpVelocity = (death ? 26f : 21f);
			if (enrage)
			{
				jumpVelocity *= 1.25f;
			}
			if (turboEnrage)
			{
				jumpVelocity *= 1.25f;
			}
			float minJumpTime = 15f;
			float maxJumpTime = 45f;
			if ((base.NPC.ai[2] >= minJumpTime && Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) <= jumpVelocity) || base.NPC.ai[2] >= maxJumpTime || !jump)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 1f;
				base.NPC.velocity.Y = -3f;
				base.NPC.netUpdate = true;
			}
			if (jump)
			{
				Vector2 center = base.NPC.Center;
				if (!Main.player[base.NPC.target].dead && Main.player[base.NPC.target].active && Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) <= (float)despawnDistance)
				{
					center = Main.player[base.NPC.target].Center;
				}
				center.Y -= 480f;
				if (base.NPC.velocity.Y == 0f)
				{
					base.NPC.velocity = center - base.NPC.Center;
					base.NPC.velocity = base.NPC.velocity.SafeNormalize(Vector2.Zero);
					NPC nPC = base.NPC;
					nPC.velocity *= jumpVelocity;
					float distanceBelowTarget = base.NPC.position.Y - (Main.player[base.NPC.target].position.Y + 80f);
					float speedMult = 1f;
					float multiplier = (turboEnrage ? 0.0025f : (enrage ? 0.002f : 0.0015f));
					if (distanceBelowTarget > 0f && ((!leftFistAlive && !rightFistAlive) | turboEnrage))
					{
						speedMult += distanceBelowTarget * multiplier;
					}
					float speedMultLimit = (turboEnrage ? 3.25f : (enrage ? 3f : 2.5f));
					if (speedMult > speedMultLimit)
					{
						speedMult = speedMultLimit;
					}
					if (Main.player[base.NPC.target].position.Y < base.NPC.Bottom.Y)
					{
						base.NPC.velocity.Y *= speedMult;
					}
				}
				else
				{
					base.NPC.velocity.Y *= 0.95f;
				}
			}
		}
	}
}
