using System;
using CalamityMod.Events;
using CalamityMod.ExtraTextures;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class WallOfFleshAI : VanillaAIOverride
{
	public class HungryAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_030d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0791: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_0606: Unknown result type (might be due to invalid IL or missing references)
			//IL_061b: Unknown result type (might be due to invalid IL or missing references)
			if (base.NPC.justHit)
			{
				base.NPC.ai[1] = 10f;
			}
			if (Main.wofNPCIndex < 0)
			{
				base.NPC.active = false;
				return false;
			}
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
			float acceleration = (death ? 0.15f : 0.12f);
			float distanceFromWall = 300f;
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.defense = base.NPC.defDefense;
			if ((double)Main.npc[Main.wofNPCIndex].life < (double)Main.npc[Main.wofNPCIndex].lifeMax * 0.5)
			{
				base.NPC.damage = base.NPC.defDamage * 2;
				base.NPC.defense = 30;
				acceleration += (death ? 0.1f : 0.08f);
			}
			else if ((double)Main.npc[Main.wofNPCIndex].life < (double)Main.npc[Main.wofNPCIndex].lifeMax * 0.75)
			{
				base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * 1.5f);
				base.NPC.defense = 20;
				acceleration += (death ? 0.05f : 0.04f);
			}
			if (base.NPC.whoAmI % 4 == 0)
			{
				distanceFromWall *= 1.75f;
			}
			if (base.NPC.whoAmI % 4 == 1)
			{
				distanceFromWall *= 1.5f;
			}
			if (base.NPC.whoAmI % 4 == 2)
			{
				distanceFromWall *= 1.25f;
			}
			if (base.NPC.whoAmI % 3 == 0)
			{
				distanceFromWall *= 1.5f;
			}
			if (base.NPC.whoAmI % 3 == 1)
			{
				distanceFromWall *= 1.25f;
			}
			distanceFromWall *= 0.75f;
			float num404 = Main.npc[Main.wofNPCIndex].Center.X;
			float y3 = Main.npc[Main.wofNPCIndex].position.Y;
			float num405 = Main.wofDrawAreaBottom - Main.wofDrawAreaTop;
			y3 = (float)Main.wofDrawAreaTop + num405 * base.NPC.ai[0];
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] > 100f)
			{
				distanceFromWall = (int)(distanceFromWall * 1.3f);
				if (base.NPC.ai[2] > 200f)
				{
					base.NPC.ai[2] = 0f;
				}
			}
			Vector2 vector40 = default(Vector2);
			((Vector2)(ref vector40))._002Ector(num404, y3);
			float num406 = Main.player[base.NPC.target].Center.X - (float)(base.NPC.width / 2) - vector40.X;
			float num407 = Main.player[base.NPC.target].Center.Y - (float)(base.NPC.height / 2) - vector40.Y;
			float num408 = (float)Math.Sqrt(num406 * num406 + num407 * num407);
			if (base.NPC.ai[1] == 0f)
			{
				if (num408 > distanceFromWall)
				{
					num408 = distanceFromWall / num408;
					num406 *= num408;
					num407 *= num408;
				}
				if (base.NPC.position.X < num404 + num406)
				{
					base.NPC.velocity.X += acceleration;
					if (base.NPC.velocity.X < 0f && num406 > 0f)
					{
						base.NPC.velocity.X += acceleration * 2.5f;
					}
				}
				else if (base.NPC.position.X > num404 + num406)
				{
					base.NPC.velocity.X -= acceleration;
					if (base.NPC.velocity.X > 0f && num406 < 0f)
					{
						base.NPC.velocity.X -= acceleration * 2.5f;
					}
				}
				if (base.NPC.position.Y < y3 + num407)
				{
					base.NPC.velocity.Y += acceleration;
					if (base.NPC.velocity.Y < 0f && num407 > 0f)
					{
						base.NPC.velocity.Y += acceleration * 2.5f;
					}
				}
				else if (base.NPC.position.Y > y3 + num407)
				{
					base.NPC.velocity.Y -= acceleration;
					if (base.NPC.velocity.Y > 0f && num407 < 0f)
					{
						base.NPC.velocity.Y -= acceleration * 2.5f;
					}
				}
				float maxVelocity = 4f;
				if (Main.wofNPCIndex >= 0)
				{
					float velocityBoost = 1.5f;
					float num409 = (float)Main.npc[Main.wofNPCIndex].life / (float)Main.npc[Main.wofNPCIndex].lifeMax;
					if (num409 < 0.75f)
					{
						velocityBoost += 0.7f;
					}
					if (num409 < 0.5f)
					{
						velocityBoost += 0.7f;
					}
					if (num409 < 0.25f)
					{
						velocityBoost += 0.9f;
					}
					if (num409 < 0.1f)
					{
						velocityBoost += 0.9f;
					}
					velocityBoost *= (death ? 1.4f : 1.25f);
					velocityBoost += 0.3f;
					maxVelocity += velocityBoost * 0.35f;
					if (base.NPC.Center.X < Main.npc[Main.wofNPCIndex].Center.X && Main.npc[Main.wofNPCIndex].velocity.X > 0f)
					{
						maxVelocity += 6f;
					}
					if (base.NPC.Center.X > Main.npc[Main.wofNPCIndex].Center.X && Main.npc[Main.wofNPCIndex].velocity.X < 0f)
					{
						maxVelocity += 6f;
					}
				}
				if (base.NPC.velocity.X > maxVelocity)
				{
					base.NPC.velocity.X = maxVelocity;
				}
				if (base.NPC.velocity.X < 0f - maxVelocity)
				{
					base.NPC.velocity.X = 0f - maxVelocity;
				}
				if (base.NPC.velocity.Y > maxVelocity)
				{
					base.NPC.velocity.Y = maxVelocity;
				}
				if (base.NPC.velocity.Y < 0f - maxVelocity)
				{
					base.NPC.velocity.Y = 0f - maxVelocity;
				}
			}
			else if (base.NPC.ai[1] > 0f)
			{
				base.NPC.ai[1]--;
			}
			else
			{
				base.NPC.ai[1] = 0f;
			}
			if (num406 > 0f)
			{
				base.NPC.spriteDirection = 1;
				base.NPC.rotation = (float)Math.Atan2(num407, num406);
			}
			if (num406 < 0f)
			{
				base.NPC.spriteDirection = -1;
				base.NPC.rotation = (float)Math.Atan2(num407, num406) + (float)Math.PI;
			}
			Lighting.AddLight(base.NPC.Center, 0.3f, 0.2f, 0.1f);
			return false;
		}
	}

	public class EyeAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0121: Unknown result type (might be due to invalid IL or missing references)
			//IL_014d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0373: Unknown result type (might be due to invalid IL or missing references)
			//IL_0383: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0799: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0733: Unknown result type (might be due to invalid IL or missing references)
			//IL_0743: Unknown result type (might be due to invalid IL or missing references)
			//IL_040a: Unknown result type (might be due to invalid IL or missing references)
			//IL_040f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0416: Unknown result type (might be due to invalid IL or missing references)
			//IL_041b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0420: Unknown result type (might be due to invalid IL or missing references)
			//IL_0427: Unknown result type (might be due to invalid IL or missing references)
			//IL_0439: Unknown result type (might be due to invalid IL or missing references)
			//IL_043e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0443: Unknown result type (might be due to invalid IL or missing references)
			//IL_0493: Unknown result type (might be due to invalid IL or missing references)
			//IL_044d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0459: Unknown result type (might be due to invalid IL or missing references)
			//IL_0463: Unknown result type (might be due to invalid IL or missing references)
			//IL_0468: Unknown result type (might be due to invalid IL or missing references)
			//IL_0470: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0547: Unknown result type (might be due to invalid IL or missing references)
			//IL_0552: Unknown result type (might be due to invalid IL or missing references)
			//IL_0566: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0601: Unknown result type (might be due to invalid IL or missing references)
			//IL_09f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a18: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a20: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a25: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a2f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a36: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a3b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a43: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a48: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a4a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a4f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a56: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a5b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a60: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a6e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a70: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.Calamity();
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			base.NPC.damage = 0;
			if (Main.wofNPCIndex < 0)
			{
				base.NPC.active = false;
				return false;
			}
			base.NPC.realLife = Main.wofNPCIndex;
			if (Main.npc[Main.wofNPCIndex].life > 0)
			{
				base.NPC.life = Main.npc[Main.wofNPCIndex].life;
			}
			float lifeRatio = (float)Main.npc[Main.wofNPCIndex].life / (float)Main.npc[Main.wofNPCIndex].lifeMax;
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				base.NPC.target = Main.npc[Main.wofNPCIndex].target;
			}
			bool shouldFireLasers = true;
			float phase2LifeRatio = 0.4f;
			bool deathModeDetach = (lifeRatio < phase2LifeRatio) & death;
			bool canHit = Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
			if (!deathModeDetach)
			{
				base.NPC.position.X = Main.npc[Main.wofNPCIndex].position.X;
				base.NPC.direction = Main.npc[Main.wofNPCIndex].direction;
				base.NPC.spriteDirection = base.NPC.direction;
				float expectedPosition = (Main.wofDrawAreaBottom + Main.wofDrawAreaTop) / 2;
				expectedPosition = ((!(base.NPC.ai[0] > 0f)) ? ((expectedPosition + (float)Main.wofDrawAreaBottom) / 2f) : ((expectedPosition + (float)Main.wofDrawAreaTop) / 2f));
				expectedPosition -= (float)(base.NPC.height / 2);
				bool num = base.NPC.position.Y > expectedPosition + 1f;
				bool aboveExpectedPosition = base.NPC.position.Y < expectedPosition - 1f;
				if (num)
				{
					float movementVelocity = MathHelper.Clamp((base.NPC.position.Y - expectedPosition + 1f) * (1f / 32f), 1f, 5f);
					base.NPC.velocity.Y = 0f - movementVelocity;
				}
				else if (aboveExpectedPosition)
				{
					float movementVelocity2 = MathHelper.Clamp((expectedPosition - 1f - base.NPC.position.Y) * (1f / 32f), 1f, 5f);
					base.NPC.velocity.Y = movementVelocity2;
				}
				else
				{
					base.NPC.velocity.Y = 0f;
					base.NPC.position.Y = expectedPosition;
				}
			}
			else
			{
				float distanceAboveTarget = (canHit ? 240f : 120f) * base.NPC.ai[0];
				float distanceAwayFromTargetX = 560f;
				float distanceAwayFromTargetXLeeway = 40f;
				float distanceAwayFromTargetY = Main.player[base.NPC.target].Center.Y - base.NPC.Center.Y;
				float distanceAwayFromTargetYLeeway = 40f;
				float absoluteDistanceX = Math.Abs(Main.player[base.NPC.target].Center.X - base.NPC.Center.X);
				bool num2 = absoluteDistanceX > distanceAwayFromTargetX + distanceAwayFromTargetXLeeway || absoluteDistanceX < distanceAwayFromTargetX - distanceAwayFromTargetXLeeway;
				bool tooFarY = distanceAwayFromTargetY > distanceAboveTarget + distanceAwayFromTargetYLeeway || distanceAwayFromTargetY < distanceAboveTarget - distanceAwayFromTargetYLeeway;
				bool num3 = num2 | tooFarY;
				Vector2 hoverDestination = Main.player[base.NPC.target].Center - Vector2.UnitY * distanceAboveTarget + Vector2.UnitX * distanceAwayFromTargetX * base.NPC.ai[3];
				if (num3)
				{
					Vector2 idealVelocity = base.NPC.SafeDirectionTo(hoverDestination) * 16f;
					base.NPC.SimpleFlyMovement(idealVelocity, 0.36f);
				}
				if (base.NPC.Distance(Main.player[base.NPC.target].Center) < distanceAwayFromTargetX || base.NPC.Distance(hoverDestination) > 120f)
				{
					shouldFireLasers = false;
				}
				float playerLocation = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
				base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
				if (base.NPC.ai[1] == 0f)
				{
					base.NPC.ai[1] = 1f;
					SoundEngine.PlaySound(in SoundID.NPCDeath12, base.NPC.Center);
					for (int i = 0; i < 100; i++)
					{
						int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, base.NPC.velocity.X, base.NPC.velocity.Y);
						Main.dust[dust].scale = Main.rand.NextFloat(1.5f, 4f);
						Dust obj = Main.dust[dust];
						obj.velocity *= Main.rand.NextFloat(0.5f, 1.5f);
					}
				}
				float eyePositionRandomChangeGateValue = MathHelper.Lerp(death ? 180f : 240f, death ? 480f : 720f, lifeRatio / phase2LifeRatio);
				if (base.NPC.ai[2] >= eyePositionRandomChangeGateValue)
				{
					base.NPC.ai[2] = 0f;
					base.NPC.ai[0] = (Main.rand.NextBool() ? 1f : (-1f));
					base.NPC.netUpdate = true;
				}
				base.NPC.ai[2]++;
			}
			Vector2 eyeLocation = base.NPC.Center;
			Vector2 lookAt = Main.player[base.NPC.target].Center;
			float eyeTargetX = lookAt.X - eyeLocation.X;
			float eyeTargetY = lookAt.Y - eyeLocation.Y;
			float wallVelocity = (float)Math.Sqrt(eyeTargetX * eyeTargetX + eyeTargetY * eyeTargetY);
			eyeTargetX *= wallVelocity;
			eyeTargetY *= wallVelocity;
			if (base.NPC.direction > 0)
			{
				if (Main.player[base.NPC.target].Center.X > base.NPC.Center.X)
				{
					base.NPC.rotation = (float)Math.Atan2(0f - eyeTargetY, 0f - eyeTargetX) + (float)Math.PI;
				}
				else
				{
					base.NPC.rotation = 0f;
					if (!deathModeDetach)
					{
						shouldFireLasers = false;
					}
				}
			}
			else if (Main.player[base.NPC.target].Center.X < base.NPC.Center.X)
			{
				base.NPC.rotation = (float)Math.Atan2(eyeTargetY, eyeTargetX) + (float)Math.PI;
			}
			else
			{
				base.NPC.rotation = 0f;
				if (!deathModeDetach)
				{
					shouldFireLasers = false;
				}
			}
			if (Main.netMode != 1)
			{
				bool charging = Main.npc[Main.wofNPCIndex].ai[3] == 1f;
				float enragedLaserTimer = 300f;
				if (charging)
				{
					base.NPC.localAI[3] = enragedLaserTimer;
				}
				bool fireEnragedLasers = base.NPC.localAI[3] > 0f && base.NPC.localAI[3] < enragedLaserTimer;
				if (base.NPC.localAI[3] > 0f)
				{
					base.NPC.localAI[3]--;
					if (base.NPC.localAI[3] == 0f)
					{
						base.NPC.localAI[1] = 0f;
					}
				}
				float shootBoost = ((!fireEnragedLasers) ? (death ? 3f : (3f * (1f - lifeRatio))) : (death ? 5f : 4f));
				base.NPC.localAI[1] += 1f + shootBoost;
				if (base.NPC.localAI[2] == 0f)
				{
					if (base.NPC.localAI[1] > 400f)
					{
						base.NPC.localAI[2] = 1f;
						base.NPC.localAI[1] = 0f;
					}
				}
				else if (base.NPC.localAI[1] > 45f && (canHit | deathModeDetach) && !charging)
				{
					base.NPC.localAI[1] = 0f;
					base.NPC.localAI[2]++;
					if (base.NPC.localAI[2] >= 4f)
					{
						base.NPC.localAI[2] = 0f;
					}
					if (shouldFireLasers)
					{
						float velocity = (fireEnragedLasers ? 3f : 4f) + shootBoost;
						int projectileType = 83;
						float projectileOffset = ((base.NPC.Distance(Main.player[base.NPC.target].Center) < 160f) ? 60f : 150f);
						Vector2 projectileVelocity = (lookAt - base.NPC.Center).SafeNormalize(Vector2.UnitY) * velocity;
						Vector2 projectileSpawn = base.NPC.Center + projectileVelocity.SafeNormalize(Vector2.UnitY) * projectileOffset;
						int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawn, projectileVelocity, projectileType, LaserDamage, 0f, Main.myPlayer, 1f);
						Main.projectile[proj].timeLeft = 900;
						if (!canHit)
						{
							Main.projectile[proj].tileCollide = false;
						}
					}
				}
			}
			return false;
		}

		public override void PostDraw(Mod mod, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0136: Unknown result type (might be due to invalid IL or missing references)
			//IL_013d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0142: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_0156: Unknown result type (might be due to invalid IL or missing references)
			//IL_0157: Unknown result type (might be due to invalid IL or missing references)
			//IL_016c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0171: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Unknown result type (might be due to invalid IL or missing references)
			//IL_0193: Unknown result type (might be due to invalid IL or missing references)
			//IL_019f: Unknown result type (might be due to invalid IL or missing references)
			bool enraged = base.NPC.localAI[3] > 0f;
			float eyeTelegraphGateValue = 200f;
			if ((base.NPC.localAI[1] > eyeTelegraphGateValue || base.NPC.localAI[2] > 0f) | enraged)
			{
				Texture2D glowTexture = (CalamityClientConfig.Instance.EnableVanillaTextureEdits ? ExtraTextureRefs.WallOfFleshEyeGlowmask.Value : TextureAssets.Npc[base.NPC.type].Value);
				Vector2 halfSize = base.NPC.frame.Size() / 2f;
				SpriteEffects spriteEffects = (SpriteEffects)(base.NPC.spriteDirection == 1);
				float colorScale = (enraged ? MathHelper.Clamp(base.NPC.localAI[3] / 300f, 0f, 1f) : ((base.NPC.localAI[2] > 0f) ? (1f - (base.NPC.localAI[2] - 1f) / 3f) : MathHelper.Clamp((base.NPC.localAI[1] - eyeTelegraphGateValue) / 200f, 0f, 1f)));
				Color drawColor2 = new Color(100, 0, 200, 192) * colorScale;
				for (int i = 0; i < 2; i++)
				{
					spriteBatch.Draw(glowTexture, base.NPC.Center - screenPos + new Vector2(0f, base.NPC.gfxOffY), (Rectangle?)base.NPC.frame, drawColor2, base.NPC.rotation, halfSize, base.NPC.scale, spriteEffects, 0f);
				}
			}
		}
	}

	public const float LaserShootGateValue = 400f;

	public const float LaserShootTelegraphTime = 200f;

	public const float TotalLasersPerBarrage = 3f;

	public const float EnragedLaserFiringDuration = 300f;

	public static int LaserDamage = 15;

	public static int SickleDamage = 22;

	public override bool AI(Mod mod)
	{
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_084d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_093c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0947: Unknown result type (might be due to invalid IL or missing references)
		//IL_0981: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e26: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (base.NPC.position.X < 160f || base.NPC.position.X > (float)((Main.maxTilesX - 10) * 16))
		{
			base.NPC.active = false;
		}
		if (base.NPC.localAI[0] == 0f)
		{
			base.NPC.localAI[0] = 1f;
			Main.wofDrawAreaBottom = -1;
			Main.wofDrawAreaTop = -1;
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		lifeRatio = MathHelper.Clamp(lifeRatio, 0f, 1f);
		bool phase2 = lifeRatio < 0.66f;
		bool phase3 = lifeRatio < 0.33f;
		if (Main.getGoodWorld && Main.netMode != 1 && Main.rand.NextBool(180) && NPC.CountNPCS(24) < 4)
		{
			for (int i = 0; i < 1000; i++)
			{
				int targetTileX = (int)(base.NPC.Center.X / 16f);
				int targetTileY = (int)(base.NPC.Center.Y / 16f);
				if (base.NPC.target >= 0)
				{
					targetTileX = (int)(Main.player[base.NPC.target].Center.X / 16f);
					targetTileY = (int)(Main.player[base.NPC.target].Center.Y / 16f);
				}
				targetTileX += Main.rand.Next(-50, 51);
				for (targetTileY += Main.rand.Next(-50, 51); targetTileY < Main.maxTilesY - 10 && !WorldGen.SolidTile(targetTileX, targetTileY); targetTileY++)
				{
				}
				targetTileY--;
				if (!WorldGen.SolidTile(targetTileX, targetTileY))
				{
					int impSpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), targetTileX * 16 + 8, targetTileY * 16, 24);
					if (Main.dedServ && impSpawn < Main.maxNPCs)
					{
						NetMessage.SendData(23, -1, -1, null, impSpawn);
					}
					break;
				}
			}
		}
		base.NPC.ai[1]++;
		if (base.NPC.ai[2] == 0f)
		{
			if (death)
			{
				base.NPC.ai[1]++;
			}
			if (phase2)
			{
				base.NPC.ai[1]++;
			}
			if (phase3)
			{
				base.NPC.ai[1]++;
			}
			if (Main.getGoodWorld)
			{
				base.NPC.ai[1] += 9f;
			}
			if (base.NPC.ai[1] > 2700f)
			{
				base.NPC.ai[2] = 1f;
			}
		}
		if (base.NPC.ai[2] > 0f && base.NPC.ai[1] > 60f)
		{
			int leechAmt = (phase3 ? 3 : 2);
			base.NPC.ai[2]++;
			base.NPC.ai[1] = 0f;
			if (base.NPC.ai[2] > (float)leechAmt)
			{
				base.NPC.ai[2] = 0f;
			}
			if (Main.netMode != 1)
			{
				if (NPC.CountNPCS(117) < 10)
				{
					int leechSpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)(base.NPC.Center.Y + 20f), 117, 1);
					int leechVelocity = (death ? 12 : 9);
					Main.npc[leechSpawn].velocity.X = base.NPC.direction * leechVelocity;
				}
				if (phase2 | death)
				{
					Vector2 projectileVelocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * ((Vector2)(ref base.NPC.velocity)).Length();
					Vector2 projectileSpawn = base.NPC.Center + projectileVelocity.SafeNormalize(Vector2.UnitY) * 50f;
					int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawn, projectileVelocity, 44, SickleDamage, 0f, Main.myPlayer, 0f, ((Vector2)(ref projectileVelocity)).Length() * 3f);
					Main.projectile[proj].timeLeft = 600;
					Main.projectile[proj].tileCollide = false;
				}
			}
		}
		base.NPC.localAI[3]++;
		if (base.NPC.localAI[3] >= (float)(600 + Main.rand.Next(1000)))
		{
			base.NPC.localAI[3] = -Main.rand.Next(200);
			SoundEngine.PlaySound(in SoundID.NPCDeath10, base.NPC.Center);
		}
		Main.wofNPCIndex = base.NPC.whoAmI;
		int currentEyeTileCenterX = (int)(base.NPC.position.X / 16f);
		int currentEyeTileWidthX = (int)((base.NPC.position.X + (float)base.NPC.width) / 16f);
		int currentEyeTileHeightY = (int)(base.NPC.Center.Y / 16f);
		int eyeMovementTries = 0;
		int eyeMovementTileY = currentEyeTileHeightY + 7;
		while (eyeMovementTries < 15 && eyeMovementTileY > Main.UnderworldLayer)
		{
			eyeMovementTileY++;
			for (int eyeMovementTileX = currentEyeTileCenterX; eyeMovementTileX <= currentEyeTileWidthX; eyeMovementTileX++)
			{
				try
				{
					if (WorldGen.SolidTile(eyeMovementTileX, eyeMovementTileY) || Main.tile[eyeMovementTileX, eyeMovementTileY].LiquidAmount > 0)
					{
						eyeMovementTries++;
					}
				}
				catch
				{
					eyeMovementTries += 15;
				}
			}
		}
		eyeMovementTileY += 4;
		if (Main.wofDrawAreaBottom == -1)
		{
			Main.wofDrawAreaBottom = eyeMovementTileY * 16;
		}
		else if (Main.wofDrawAreaBottom > eyeMovementTileY * 16)
		{
			Main.wofDrawAreaBottom--;
			if (Main.wofDrawAreaBottom < eyeMovementTileY * 16)
			{
				Main.wofDrawAreaBottom = eyeMovementTileY * 16;
			}
		}
		else if (Main.wofDrawAreaBottom < eyeMovementTileY * 16)
		{
			Main.wofDrawAreaBottom++;
			if (Main.wofDrawAreaBottom > eyeMovementTileY * 16)
			{
				Main.wofDrawAreaBottom = eyeMovementTileY * 16;
			}
		}
		eyeMovementTries = 0;
		eyeMovementTileY = currentEyeTileHeightY - 7;
		while (eyeMovementTries < 15 && eyeMovementTileY < Main.maxTilesY - 10)
		{
			eyeMovementTileY--;
			for (int j = currentEyeTileCenterX; j <= currentEyeTileWidthX; j++)
			{
				try
				{
					if (WorldGen.SolidTile(j, eyeMovementTileY) || Main.tile[j, eyeMovementTileY].LiquidAmount > 0)
					{
						eyeMovementTries++;
					}
				}
				catch
				{
					eyeMovementTries += 15;
				}
			}
		}
		eyeMovementTileY -= 4;
		if (Main.wofDrawAreaTop == -1)
		{
			Main.wofDrawAreaTop = eyeMovementTileY * 16;
		}
		else if (Main.wofDrawAreaTop > eyeMovementTileY * 16)
		{
			Main.wofDrawAreaTop--;
			if (Main.wofDrawAreaTop < eyeMovementTileY * 16)
			{
				Main.wofDrawAreaTop = eyeMovementTileY * 16;
			}
		}
		else if (Main.wofDrawAreaTop < eyeMovementTileY * 16)
		{
			Main.wofDrawAreaTop++;
			if (Main.wofDrawAreaTop > eyeMovementTileY * 16)
			{
				Main.wofDrawAreaTop = eyeMovementTileY * 16;
			}
		}
		float mouthYPosition = (Main.wofDrawAreaBottom + Main.wofDrawAreaTop) / 2 - base.NPC.height / 2;
		int worldBottomTileY = (Main.maxTilesY - 180) * 16;
		if (mouthYPosition < (float)worldBottomTileY)
		{
			mouthYPosition = worldBottomTileY;
		}
		base.NPC.position.Y = mouthYPosition;
		float targetPosition = Main.player[base.NPC.target].Center.X;
		float npcPosition = base.NPC.Center.X;
		float distanceFromTarget = ((!(base.NPC.velocity.X < 0f)) ? (targetPosition - npcPosition) : (npcPosition - targetPosition));
		float halfAverageScreenWidth = 960f;
		float distanceBeforeSlowingDown = 640f;
		float timeBeforeEnrage = (death ? (150f - 130f * (1f - lifeRatio)) : 600f);
		float speedMult = 1f;
		if (calamityGlobalNPC.newAI[0] < timeBeforeEnrage)
		{
			if (distanceFromTarget > halfAverageScreenWidth)
			{
				speedMult += (distanceFromTarget - halfAverageScreenWidth) * 0.001f;
				calamityGlobalNPC.newAI[0]++;
				if (calamityGlobalNPC.newAI[0] >= timeBeforeEnrage)
				{
					calamityGlobalNPC.newAI[1] = 1f;
					base.NPC.ai[3] = 1f;
					if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 2800f)
					{
						SoundStyle style = SoundID.NPCDeath10 with
						{
							Pitch = SoundID.NPCDeath10.Pitch - 0.25f
						};
						SoundEngine.PlaySound(in style, Main.LocalPlayer.Center);
					}
				}
			}
			else if (distanceFromTarget < distanceBeforeSlowingDown)
			{
				speedMult += (distanceFromTarget - distanceBeforeSlowingDown) * 0.002f;
			}
			if (distanceFromTarget < halfAverageScreenWidth && calamityGlobalNPC.newAI[0] > 0f)
			{
				calamityGlobalNPC.newAI[0]--;
			}
			speedMult = MathHelper.Clamp(speedMult, 0.4f, 2f);
		}
		if (calamityGlobalNPC.newAI[1] == 1f)
		{
			speedMult = 3.25f;
			if (distanceFromTarget < distanceBeforeSlowingDown)
			{
				calamityGlobalNPC.newAI[0] = 0f;
				calamityGlobalNPC.newAI[1] = 0f;
				base.NPC.ai[3] = 0f;
			}
		}
		calamityGlobalNPC.CurrentlyEnraged = distanceFromTarget > halfAverageScreenWidth || base.NPC.ai[3] == 1f;
		float deathModeVelocityBoost = 0f;
		if (death)
		{
			float velocityBoostStartDistance = distanceBeforeSlowingDown;
			float velocityBoostMaxDistance = velocityBoostStartDistance * 1.5f;
			float lerpAmount = MathHelper.Clamp((Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) - velocityBoostStartDistance) / velocityBoostMaxDistance, 0f, 1f);
			deathModeVelocityBoost = MathHelper.Lerp(0f, 4f, lerpAmount);
		}
		float velocityBoost = 4f * (1f - lifeRatio);
		float velocityX = 2f + deathModeVelocityBoost + velocityBoost;
		velocityX *= speedMult;
		if (death)
		{
			velocityX *= 1.1f;
		}
		if (Main.getGoodWorld)
		{
			velocityX *= 1.1f;
			velocityX += 0.1f;
		}
		if (base.NPC.velocity.X == 0f)
		{
			base.NPC.TargetClosest();
			if (Main.player[base.NPC.target].dead)
			{
				float wallVelocity = float.PositiveInfinity;
				int wallDirection = 0;
				for (int k = 0; k < 255; k++)
				{
					Player player = Main.player[base.NPC.target];
					if (player.active)
					{
						float playerDist = base.NPC.Distance(player.Center);
						if (wallVelocity > playerDist)
						{
							wallVelocity = playerDist;
							wallDirection = ((base.NPC.Center.X < player.Center.X) ? 1 : (-1));
						}
					}
				}
				base.NPC.direction = wallDirection;
			}
			base.NPC.velocity.X = base.NPC.direction;
		}
		if (base.NPC.velocity.X < 0f)
		{
			base.NPC.velocity.X = 0f - velocityX;
			base.NPC.direction = -1;
		}
		else
		{
			base.NPC.velocity.X = velocityX;
			base.NPC.direction = 1;
		}
		if (Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].gross)
		{
			base.NPC.TargetClosest_WOF();
		}
		if (Main.player[base.NPC.target].dead)
		{
			base.NPC.localAI[1] += 1f / 180f;
			if (base.NPC.localAI[1] >= 1f)
			{
				SoundEngine.PlaySound(in SoundID.NPCDeath10, base.NPC.Center);
				base.NPC.life = 0;
				base.NPC.active = false;
				if (Main.netMode != 1)
				{
					NetMessage.SendData(28, -1, -1, null, base.NPC.whoAmI, -1f);
				}
				return false;
			}
		}
		else
		{
			base.NPC.localAI[1] = MathHelper.Clamp(base.NPC.localAI[1] - 1f / 30f, 0f, 1f);
		}
		base.NPC.spriteDirection = base.NPC.direction;
		Vector2 mouthLocation = base.NPC.Center;
		float mouthTargetX = Main.player[base.NPC.target].Center.X - mouthLocation.X;
		float mouthTargetY = Main.player[base.NPC.target].Center.Y - mouthLocation.Y;
		float mouthTargetDist = (float)Math.Sqrt(mouthTargetX * mouthTargetX + mouthTargetY * mouthTargetY);
		mouthTargetX *= mouthTargetDist;
		mouthTargetY *= mouthTargetDist;
		if (base.NPC.direction > 0)
		{
			if (Main.player[base.NPC.target].Center.X > base.NPC.Center.X)
			{
				base.NPC.rotation = (float)Math.Atan2(0f - mouthTargetY, 0f - mouthTargetX) + (float)Math.PI;
			}
			else
			{
				base.NPC.rotation = 0f;
			}
		}
		else if (Main.player[base.NPC.target].Center.X < base.NPC.Center.X)
		{
			base.NPC.rotation = (float)Math.Atan2(mouthTargetY, mouthTargetX) + (float)Math.PI;
		}
		else
		{
			base.NPC.rotation = 0f;
		}
		if (Main.netMode != 1)
		{
			float spawnBoost = (death ? 1f : ((float)Math.Ceiling(lifeRatio * 10f)));
			int chance = (int)(1f + spawnBoost);
			chance *= chance;
			chance = (chance * 19 + 400) / 20;
			if (chance < 60)
			{
				chance = (chance * 3 + 60) / 4;
			}
			chance *= 2;
			if (death)
			{
				chance /= 2;
			}
			if (chance < 2)
			{
				chance = 2;
			}
			if (Main.rand.NextBool(chance))
			{
				int maxHungriesBasedOnHP = (int)Math.Round(MathHelper.Lerp(death ? 2f : 1f, death ? 6f : 4f, (float)base.NPC.life / (float)base.NPC.lifeMax));
				if (NPC.CountNPCS(115) < maxHungriesBasedOnHP)
				{
					int hungryAmt = 0;
					int maxHungries = 10;
					float[] array = new float[maxHungries];
					for (int l = 0; l < Main.maxNPCs; l++)
					{
						if (hungryAmt < maxHungries && Main.npc[l].active && Main.npc[l].type == 115)
						{
							array[hungryAmt] = Main.npc[l].ai[0];
							hungryAmt++;
						}
					}
					int maxValue = 1 + hungryAmt * 2;
					if (death)
					{
						maxValue /= 2;
					}
					if (maxValue < 2)
					{
						maxValue = 2;
					}
					if (hungryAmt < maxHungries && Main.rand.Next(maxValue) <= 1)
					{
						int spawnHungryControl = -1;
						for (int m = 0; m < 1000; m++)
						{
							int randomHungrySpawnValue = Main.rand.Next(maxHungries);
							float hungryArrayValue = (float)randomHungrySpawnValue * 0.1f - 0.05f;
							bool shouldRespawnHungry = true;
							for (int n = 0; n < hungryAmt; n++)
							{
								if (hungryArrayValue == array[n])
								{
									shouldRespawnHungry = false;
									break;
								}
							}
							if (shouldRespawnHungry)
							{
								spawnHungryControl = randomHungrySpawnValue;
								break;
							}
						}
						if (spawnHungryControl >= 0)
						{
							int hungryRespawns = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X, (int)mouthYPosition, 115, base.NPC.whoAmI);
							Main.npc[hungryRespawns].ai[0] = (float)spawnHungryControl * 0.1f - 0.05f;
						}
					}
				}
			}
		}
		if (base.NPC.localAI[0] == 1f && Main.netMode != 1)
		{
			base.NPC.localAI[0] = 2f;
			mouthYPosition = (Main.wofDrawAreaBottom + Main.wofDrawAreaTop) / 2;
			mouthYPosition = (mouthYPosition + (float)Main.wofDrawAreaTop) / 2f;
			int eyeSpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X, (int)mouthYPosition, 114, base.NPC.whoAmI);
			Main.npc[eyeSpawn].ai[0] = 1f;
			if (death)
			{
				Main.npc[eyeSpawn].ai[3] = 1f;
			}
			mouthYPosition = (Main.wofDrawAreaBottom + Main.wofDrawAreaTop) / 2;
			mouthYPosition = (mouthYPosition + (float)Main.wofDrawAreaBottom) / 2f;
			eyeSpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X, (int)mouthYPosition, 114, base.NPC.whoAmI);
			Main.npc[eyeSpawn].ai[0] = -1f;
			if (death)
			{
				Main.npc[eyeSpawn].ai[3] = -1f;
			}
			mouthYPosition = (Main.wofDrawAreaBottom + Main.wofDrawAreaTop) / 2;
			mouthYPosition = (mouthYPosition + (float)Main.wofDrawAreaBottom) / 2f;
			int maxHungries2 = (death ? 14 : 11);
			float maxOffset = (death ? (1f / 15f) : 0.1f);
			for (int num = 0; num < maxHungries2; num++)
			{
				int hungrySpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X, (int)mouthYPosition, 115, base.NPC.whoAmI);
				Main.npc[hungrySpawn].ai[0] = (float)num * maxOffset - 0.05f;
			}
		}
		return false;
	}
}
