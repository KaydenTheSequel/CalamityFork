using System;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class SkeletronAI : VanillaAIOverride
{
	public class SkeletronHandAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0163: Unknown result type (might be due to invalid IL or missing references)
			//IL_0191: Unknown result type (might be due to invalid IL or missing references)
			//IL_0197: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0204: Unknown result type (might be due to invalid IL or missing references)
			//IL_0209: Unknown result type (might be due to invalid IL or missing references)
			//IL_0528: Unknown result type (might be due to invalid IL or missing references)
			//IL_0546: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c77: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c7c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c92: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cb0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cce: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cde: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_0910: Unknown result type (might be due to invalid IL or missing references)
			//IL_092e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0674: Unknown result type (might be due to invalid IL or missing references)
			//IL_0692: Unknown result type (might be due to invalid IL or missing references)
			//IL_106d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1072: Unknown result type (might be due to invalid IL or missing references)
			//IL_1088: Unknown result type (might be due to invalid IL or missing references)
			//IL_10a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_10c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_10d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ea6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0eb1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0da6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dc4: Unknown result type (might be due to invalid IL or missing references)
			//IL_09b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0721: Unknown result type (might be due to invalid IL or missing references)
			//IL_073f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e14: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e1f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e24: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e29: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e2e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e35: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e3a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a7a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bbd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bf1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c0f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c1f: Unknown result type (might be due to invalid IL or missing references)
			//IL_12e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_12ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_11c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b09: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b27: Unknown result type (might be due to invalid IL or missing references)
			//IL_124f: Unknown result type (might be due to invalid IL or missing references)
			//IL_125a: Unknown result type (might be due to invalid IL or missing references)
			//IL_125f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1264: Unknown result type (might be due to invalid IL or missing references)
			//IL_1269: Unknown result type (might be due to invalid IL or missing references)
			//IL_1270: Unknown result type (might be due to invalid IL or missing references)
			//IL_1275: Unknown result type (might be due to invalid IL or missing references)
			//IL_11e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f06: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f1e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1341: Unknown result type (might be due to invalid IL or missing references)
			//IL_1359: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f83: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f8e: Unknown result type (might be due to invalid IL or missing references)
			//IL_13be: Unknown result type (might be due to invalid IL or missing references)
			//IL_13c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fbd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fd3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fd8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fdf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fe4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fff: Unknown result type (might be due to invalid IL or missing references)
			//IL_1004: Unknown result type (might be due to invalid IL or missing references)
			//IL_13f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_140e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1413: Unknown result type (might be due to invalid IL or missing references)
			//IL_141a: Unknown result type (might be due to invalid IL or missing references)
			//IL_141f: Unknown result type (might be due to invalid IL or missing references)
			//IL_143a: Unknown result type (might be due to invalid IL or missing references)
			//IL_143f: Unknown result type (might be due to invalid IL or missing references)
			CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
			}
			float yMultiplier = 1f;
			if (calamityGlobalNPC.newAI[0] != 0f)
			{
				yMultiplier = calamityGlobalNPC.newAI[0];
			}
			if (death)
			{
				yMultiplier *= 1.3f;
			}
			if (calamityGlobalNPC.newAI[1] < 180f)
			{
				calamityGlobalNPC.newAI[1]++;
				if (calamityGlobalNPC.newAI[1] % 15f == 0f)
				{
					base.NPC.SyncExtraAI();
				}
				base.NPC.damage = 0;
			}
			else
			{
				base.NPC.damage = base.NPC.defDamage;
			}
			base.NPC.spriteDirection = -(int)base.NPC.ai[0];
			if (Main.npc[(int)base.NPC.ai[1]].ai[3] == -60f && Main.netMode != 1)
			{
				for (int m = 0; m < 10; m++)
				{
					int teleportDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 91, 0f, 0f, 200, default(Color), 3f);
					Main.dust[teleportDust].noGravity = true;
					Main.dust[teleportDust].velocity.X *= 2f;
				}
				base.NPC.Center = Main.npc[(int)base.NPC.ai[1]].Center;
				base.NPC.velocity = Vector2.Zero;
				base.NPC.netUpdate = true;
			}
			float skeletronLifeRatio = 1f;
			if (!Main.npc[(int)base.NPC.ai[1]].active || Main.npc[(int)base.NPC.ai[1]].aiStyle != 11)
			{
				base.NPC.ai[2] += 10f;
				if (base.NPC.ai[2] > 50f || !Main.dedServ)
				{
					base.NPC.life = -1;
					base.NPC.HitEffect();
					base.NPC.active = false;
				}
			}
			else
			{
				skeletronLifeRatio = (float)Main.npc[(int)base.NPC.ai[1]].life / (float)Main.npc[(int)base.NPC.ai[1]].lifeMax;
			}
			bool cancelSlap = Main.npc[(int)base.NPC.ai[1]].ai[2] >= 600f;
			bool phase2 = skeletronLifeRatio < 0.5f;
			bool phase3 = skeletronLifeRatio < 0.3f;
			float velocityMultiplier = MathHelper.Lerp(death ? 0.6f : 0.7f, 1f, skeletronLifeRatio);
			float velocityIncrement = MathHelper.Lerp(0.2f, death ? 0.4f : 0.3f, 1f - skeletronLifeRatio);
			float handSwipeVelocity = MathHelper.Lerp(16f, death ? 24f : 20f, 1f - skeletronLifeRatio);
			float deceleration = (Main.getGoodWorld ? 0.78f : (death ? 0.82f : 0.86f));
			if (death)
			{
				velocityMultiplier *= 0.75f;
				velocityIncrement *= 1.5f;
				handSwipeVelocity *= 1.35f;
				deceleration *= 0.75f;
			}
			float handSwipeDistance = (death ? 1280f : 960f);
			float handSwipeDuration = handSwipeDistance / handSwipeVelocity;
			float slapGateValue = 300f;
			float slapTimerIncrement = MathHelper.Lerp(death ? 1.5f : 1f, death ? 3f : 2f, 1f - skeletronLifeRatio);
			if (phase3)
			{
				slapTimerIncrement *= (death ? 2.5f : 2f);
			}
			else if (phase2)
			{
				slapTimerIncrement *= (death ? 2f : 1.5f);
			}
			if (base.NPC.ai[2] == 0f || base.NPC.ai[2] == 3f)
			{
				if (Main.npc[(int)base.NPC.ai[1]].ai[1] == 3f && base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
				}
				if ((Main.npc[(int)base.NPC.ai[1]].ai[1] != 0f) | cancelSlap)
				{
					deceleration *= 0.75f;
					velocityIncrement *= 1.5f;
					float maxX = velocityIncrement * 100f * velocityMultiplier;
					float maxY = velocityIncrement * 100f * velocityMultiplier;
					if (base.NPC.Top.Y > Main.npc[(int)base.NPC.ai[1]].Top.Y - 100f * yMultiplier)
					{
						if (base.NPC.velocity.Y > 0f)
						{
							base.NPC.velocity.Y *= deceleration;
						}
						base.NPC.velocity.Y -= velocityIncrement;
						if (base.NPC.velocity.Y > maxY)
						{
							base.NPC.velocity.Y = maxY;
						}
					}
					else if (base.NPC.Top.Y < Main.npc[(int)base.NPC.ai[1]].Top.Y - 100f * yMultiplier)
					{
						if (base.NPC.velocity.Y < 0f)
						{
							base.NPC.velocity.Y *= deceleration;
						}
						base.NPC.velocity.Y += velocityIncrement;
						if (base.NPC.velocity.Y < 0f - maxY)
						{
							base.NPC.velocity.Y = 0f - maxY;
						}
					}
					if (base.NPC.Center.X > Main.npc[(int)base.NPC.ai[1]].Center.X - 120f * base.NPC.ai[0])
					{
						if (base.NPC.velocity.X > 0f)
						{
							base.NPC.velocity.X *= deceleration;
						}
						base.NPC.velocity.X -= velocityIncrement;
						if (base.NPC.velocity.X > maxX)
						{
							base.NPC.velocity.X = maxX;
						}
					}
					if (base.NPC.Center.X < Main.npc[(int)base.NPC.ai[1]].Center.X - 120f * base.NPC.ai[0])
					{
						if (base.NPC.velocity.X < 0f)
						{
							base.NPC.velocity.X *= deceleration;
						}
						base.NPC.velocity.X += velocityIncrement;
						if (base.NPC.velocity.X < 0f - maxX)
						{
							base.NPC.velocity.X = 0f - maxX;
						}
					}
				}
				else
				{
					if (calamityGlobalNPC.newAI[3] == 1f)
					{
						calamityGlobalNPC.newAI[2] += slapTimerIncrement;
						base.NPC.ai[3] += slapTimerIncrement;
						if (base.NPC.ai[3] >= slapGateValue)
						{
							base.NPC.target = Main.npc[(int)base.NPC.ai[1]].target;
							base.NPC.ai[2]++;
							base.NPC.ai[3] = (calamityGlobalNPC.newAI[2] = slapGateValue);
							calamityGlobalNPC.newAI[3] = 0f;
							base.NPC.netUpdate = true;
							base.NPC.SyncExtraAI();
						}
					}
					else
					{
						calamityGlobalNPC.newAI[2] -= slapTimerIncrement * 2f;
						if (calamityGlobalNPC.newAI[2] <= 0f)
						{
							calamityGlobalNPC.newAI[2] = 0f;
							calamityGlobalNPC.newAI[3] = 1f;
							base.NPC.SyncExtraAI();
						}
					}
					float maxX2 = velocityIncrement * 100f * velocityMultiplier;
					float maxY2 = velocityIncrement * 100f * velocityMultiplier;
					if (base.NPC.Top.Y > Main.npc[(int)base.NPC.ai[1]].Top.Y + 230f * yMultiplier)
					{
						if (base.NPC.velocity.Y > 0f)
						{
							base.NPC.velocity.Y *= deceleration;
						}
						base.NPC.velocity.Y -= velocityIncrement;
						if (base.NPC.velocity.Y > maxY2)
						{
							base.NPC.velocity.Y = maxY2;
						}
					}
					else if (base.NPC.Top.Y < Main.npc[(int)base.NPC.ai[1]].Top.Y + 230f * yMultiplier)
					{
						if (base.NPC.velocity.Y < 0f)
						{
							base.NPC.velocity.Y *= deceleration;
						}
						base.NPC.velocity.Y += velocityIncrement;
						if (base.NPC.velocity.Y < 0f - maxY2)
						{
							base.NPC.velocity.Y = 0f - maxY2;
						}
					}
					if (base.NPC.Center.X > Main.npc[(int)base.NPC.ai[1]].Center.X - 200f * base.NPC.ai[0])
					{
						if (base.NPC.velocity.X > 0f)
						{
							base.NPC.velocity.X *= deceleration;
						}
						base.NPC.velocity.X -= velocityIncrement;
						if (base.NPC.velocity.X > maxX2)
						{
							base.NPC.velocity.X = maxX2;
						}
					}
					if (base.NPC.Center.X < Main.npc[(int)base.NPC.ai[1]].Center.X - 200f * base.NPC.ai[0])
					{
						if (base.NPC.velocity.X < 0f)
						{
							base.NPC.velocity.X *= deceleration;
						}
						base.NPC.velocity.X += velocityIncrement;
						if (base.NPC.velocity.X < 0f - maxX2)
						{
							base.NPC.velocity.X = 0f - maxX2;
						}
					}
				}
				Vector2 handCurrentPos = base.NPC.Center;
				float handIdleXPos = Main.npc[(int)base.NPC.ai[1]].Center.X - 200f * base.NPC.ai[0] - handCurrentPos.X;
				float handIdleYPos = Main.npc[(int)base.NPC.ai[1]].Top.Y + 230f - handCurrentPos.Y;
				Math.Sqrt(handIdleXPos * handIdleXPos + handIdleYPos * handIdleYPos);
				base.NPC.rotation = (float)Math.Atan2(handIdleYPos, handIdleXPos) + (float)Math.PI / 2f;
				return false;
			}
			if (base.NPC.ai[2] == 1f)
			{
				Vector2 handCurrentPosition = base.NPC.Center;
				float handDrawbackXPos = Main.npc[(int)base.NPC.ai[1]].Center.X - 200f * base.NPC.ai[0] - handCurrentPosition.X;
				float handDrawbackYPos = Main.npc[(int)base.NPC.ai[1]].Top.Y + 230f - handCurrentPosition.Y;
				Math.Sqrt(handDrawbackXPos * handDrawbackXPos + handDrawbackYPos * handDrawbackYPos);
				base.NPC.rotation = (float)Math.Atan2(handDrawbackYPos, handDrawbackXPos) + (float)Math.PI / 2f;
				base.NPC.velocity.X *= 0.95f;
				base.NPC.velocity.Y -= velocityIncrement;
				if (base.NPC.velocity.Y < -14f)
				{
					base.NPC.velocity.Y = -14f;
				}
				else if (base.NPC.velocity.Y > 10f)
				{
					base.NPC.velocity.Y = 10f;
				}
				if (base.NPC.Top.Y < Main.npc[(int)base.NPC.ai[1]].Top.Y - 200f)
				{
					base.NPC.ai[2] = 2f;
					base.NPC.ai[3] = 0f;
					base.NPC.velocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * handSwipeVelocity;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[2] == 2f)
			{
				base.NPC.ai[3]++;
				if ((base.NPC.ai[3] >= handSwipeDuration || Vector2.Distance(Main.npc[(int)base.NPC.ai[1]].Center, base.NPC.Center) > handSwipeDistance) | cancelSlap)
				{
					base.NPC.ai[2] = 3f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
					if (death && Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) && !cancelSlap && Main.netMode != 1 && phase2 && Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 160f)
					{
						float skullProjSpeed = handSwipeVelocity * (phase3 ? 0.6f : 0.2f);
						Vector2 initialProjectileVelocity = base.NPC.Center.DirectionTo(Main.player[base.NPC.target].Center) * skullProjSpeed;
						int type = 270;
						int skullProjectile = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, initialProjectileVelocity, type, SkullDamage, 0f, Main.myPlayer, 0f - (phase3 ? 2f : 1f));
						Main.projectile[skullProjectile].timeLeft = 600;
					}
				}
			}
			else if (base.NPC.ai[2] == 4f)
			{
				Vector2 handStrikeCurrentPos = base.NPC.Center;
				float handStrikeXPos = Main.npc[(int)base.NPC.ai[1]].Center.X - 200f * base.NPC.ai[0] - handStrikeCurrentPos.X;
				float handStrikeYPos = Main.npc[(int)base.NPC.ai[1]].Top.Y + 230f - handStrikeCurrentPos.Y;
				Math.Sqrt(handStrikeXPos * handStrikeXPos + handStrikeYPos * handStrikeYPos);
				base.NPC.rotation = (float)Math.Atan2(handStrikeYPos, handStrikeXPos) + (float)Math.PI / 2f;
				base.NPC.velocity.Y *= 0.95f;
				base.NPC.velocity.X += velocityIncrement * (0f - base.NPC.ai[0]);
				if (base.NPC.velocity.X < -10f)
				{
					base.NPC.velocity.X = -10f;
				}
				else if (base.NPC.velocity.X > 14f)
				{
					base.NPC.velocity.X = 14f;
				}
				if (base.NPC.Center.X < Main.npc[(int)base.NPC.ai[1]].Center.X - 500f || base.NPC.Center.X > Main.npc[(int)base.NPC.ai[1]].Center.X + 500f)
				{
					base.NPC.ai[2] = 5f;
					base.NPC.ai[3] = 0f;
					base.NPC.velocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * handSwipeVelocity;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[2] == 5f)
			{
				base.NPC.ai[3]++;
				if ((base.NPC.ai[3] >= handSwipeDuration || Vector2.Distance(Main.npc[(int)base.NPC.ai[1]].Center, base.NPC.Center) > handSwipeDistance) | cancelSlap)
				{
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
					if (death && Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) && !cancelSlap && Main.netMode != 1 && phase2 && Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 160f)
					{
						float skullProjSpeed2 = handSwipeVelocity * (phase3 ? 0.6f : 0.2f);
						Vector2 initialProjectileVelocity2 = base.NPC.Center.DirectionTo(Main.player[base.NPC.target].Center) * skullProjSpeed2;
						int type2 = 270;
						int skullProjectile2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, initialProjectileVelocity2, type2, SkullDamage, 0f, Main.myPlayer, 0f - (phase3 ? 2f : 1f));
						Main.projectile[skullProjectile2].timeLeft = 600;
					}
				}
			}
			return false;
		}

		public override void PostDraw(Mod mod, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0117: Unknown result type (might be due to invalid IL or missing references)
			CalamityGlobalNPC calNPC = base.NPC.GetGlobalNPC<CalamityGlobalNPC>();
			float beginTelegraphGateValue = 180f;
			if (calNPC.newAI[2] > beginTelegraphGateValue)
			{
				float colorScale = MathHelper.Clamp((calNPC.newAI[2] - beginTelegraphGateValue) / 120f, 0f, 1f);
				Color drawColor2 = new Color(150, 150, 150, 0) * colorScale;
				Vector2 halfSize = base.NPC.frame.Size() / 2f;
				SpriteEffects spriteEffects = (SpriteEffects)0;
				if (base.NPC.spriteDirection == 1)
				{
					spriteEffects = (SpriteEffects)1;
				}
				Vector2 glowOffset = Vector2.UnitY * 8f;
				for (int i = 0; i < 2; i++)
				{
					spriteBatch.Draw(TextureAssets.Npc[base.NPC.type].Value, base.NPC.Center - screenPos + new Vector2(0f, base.NPC.gfxOffY) - glowOffset, (Rectangle?)base.NPC.frame, drawColor2, base.NPC.rotation, halfSize, base.NPC.scale, spriteEffects, 0f);
				}
			}
		}
	}

	public class DungeonGuardianAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			return true;
		}

		public override void PostAI(Mod mod)
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0112: Unknown result type (might be due to invalid IL or missing references)
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			//IL_016b: Unknown result type (might be due to invalid IL or missing references)
			//IL_018e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01da: Unknown result type (might be due to invalid IL or missing references)
			//IL_01df: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_020b: Unknown result type (might be due to invalid IL or missing references)
			//IL_020d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0214: Unknown result type (might be due to invalid IL or missing references)
			//IL_0219: Unknown result type (might be due to invalid IL or missing references)
			//IL_021e: Unknown result type (might be due to invalid IL or missing references)
			//IL_022c: Unknown result type (might be due to invalid IL or missing references)
			//IL_022e: Unknown result type (might be due to invalid IL or missing references)
			Player target = Main.player[base.NPC.target];
			if (base.NPC.ai[1] == 3f)
			{
				return;
			}
			Vector2 targetVector = target.Center - base.NPC.Center;
			float targetDist = ((Vector2)(ref targetVector)).Length();
			targetDist = 12f / targetDist;
			base.NPC.velocity.X = targetVector.X * targetDist;
			base.NPC.velocity.Y = targetVector.Y * targetDist;
			if (Main.netMode != 1 && base.NPC.localAI[1]++ % 60f == 59f)
			{
				Vector2 source = base.NPC.Center;
				if (Collision.CanHit(source, 1, 1, target.Center, target.width, target.height))
				{
					float speed = 5f;
					float xDist = target.Center.X - source.X + (float)Main.rand.Next(-20, 21);
					float yDist = target.Center.Y - source.Y + (float)Main.rand.Next(-20, 21);
					Vector2 velocity = default(Vector2);
					((Vector2)(ref velocity))._002Ector(xDist, yDist);
					float distTarget = ((Vector2)(ref velocity)).Length();
					distTarget = speed / distTarget;
					velocity.X *= distTarget;
					velocity.Y *= distTarget;
					Vector2 offset = Utils.SafeNormalize(new Vector2(velocity.X * 1f + (float)Main.rand.Next(-50, 51) * 0.01f, velocity.Y * 1f + (float)Main.rand.Next(-50, 51) * 0.01f), Vector2.UnitY);
					offset *= speed;
					offset += base.NPC.velocity;
					velocity.X = offset.X;
					velocity.Y = offset.Y;
					int damage = 2500;
					int projType = 270;
					source += offset * 5f;
					int skull = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), source, velocity, projType, damage, 0f, Main.myPlayer, -1f);
					Main.projectile[skull].timeLeft = 600;
					Main.projectile[skull].tileCollide = false;
				}
			}
		}
	}

	public const float ChargeGateValue = 600f;

	public const float ChargeTelegraphTime = 120f;

	public const float HandSlapGateValue = 300f;

	public const float HandSlapTelegraphTime = 120f;

	public const float HandSwipeDistance = 960f;

	public const float HandSwipeDistance_Master = 1280f;

	public static float SpinDamageMult = 1.3f;

	public static int SkullDamage = 17;

	public override void SetDefaults(Mod mod)
	{
		base.DisableMultiplayerSmoothing = true;
	}

	public override bool AI(Mod mod)
	{
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0725: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_073c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0754: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f93: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f98: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fab: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fda: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0910: Unknown result type (might be due to invalid IL or missing references)
		//IL_0916: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0803: Unknown result type (might be due to invalid IL or missing references)
		//IL_0873: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_087c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0882: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2176: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_21e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a83: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_17af: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a66: Unknown result type (might be due to invalid IL or missing references)
		//IL_22d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_230f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2315: Unknown result type (might be due to invalid IL or missing references)
		//IL_233a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2345: Unknown result type (might be due to invalid IL or missing references)
		//IL_234a: Unknown result type (might be due to invalid IL or missing references)
		//IL_234f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d21: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d44: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_18cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1008: Unknown result type (might be due to invalid IL or missing references)
		//IL_100d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1012: Unknown result type (might be due to invalid IL or missing references)
		//IL_1017: Unknown result type (might be due to invalid IL or missing references)
		//IL_1019: Unknown result type (might be due to invalid IL or missing references)
		//IL_1023: Unknown result type (might be due to invalid IL or missing references)
		//IL_1028: Unknown result type (might be due to invalid IL or missing references)
		//IL_1036: Unknown result type (might be due to invalid IL or missing references)
		//IL_1038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b28: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b63: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b83: Unknown result type (might be due to invalid IL or missing references)
		//IL_156b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1586: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e21: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e26: Unknown result type (might be due to invalid IL or missing references)
		//IL_160c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1627: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f16: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f18: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1467: Unknown result type (might be due to invalid IL or missing references)
		//IL_1472: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e69: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eac: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ede: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ee8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eed: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef2: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		float phase2LifeRatio = (death ? 1f : 0.85f);
		float phase3LifeRatio = (death ? 0.9f : 0.7f);
		float respawnHandsLifeRatio = 0.5f;
		float phase4LifeRatio = (death ? 0.4f : 0.3f);
		float phase5LifeRatio = (death ? 0.15f : 0.1f);
		bool phase2 = lifeRatio < phase2LifeRatio;
		bool phase3 = lifeRatio < phase3LifeRatio;
		bool respawnHands = lifeRatio < respawnHandsLifeRatio;
		bool phase4 = lifeRatio < phase4LifeRatio;
		bool phase5 = lifeRatio < phase5LifeRatio;
		base.NPC.defense = base.NPC.defDefense;
		base.NPC.damage = base.NPC.defDamage;
		base.NPC.reflectsProjectiles = false;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
		}
		if (Main.netMode != 1)
		{
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.ai[0] = 1f;
				SpawnHands();
				base.NPC.netUpdate = true;
			}
			if (respawnHands && calamityGlobalNPC.newAI[0] == 0f && Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 160f)
			{
				calamityGlobalNPC.newAI[0] = 1f;
				SoundStyle style = SoundID.ForceRoar with
				{
					Pitch = SoundID.ForceRoar.Pitch - 0.25f
				};
				SoundEngine.PlaySound(in style, base.NPC.Center);
				SpawnHands();
				base.NPC.netUpdate = true;
				base.NPC.SyncExtraAI();
			}
		}
		if (base.NPC.ai[1] != 3f)
		{
			int despawnDistanceInTiles = 500;
			if (Main.player[base.NPC.target].dead || Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) / 16f > (float)despawnDistanceInTiles)
			{
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
				if (Main.player[base.NPC.target].dead || Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) / 16f > (float)despawnDistanceInTiles)
				{
					base.NPC.ai[1] = 3f;
				}
			}
			else if (base.NPC.timeLeft < 1800)
			{
				base.NPC.timeLeft = 1800;
			}
		}
		if (Main.IsItDay() && !BossRushEvent.BossRushActive && base.NPC.ai[1] != 3f && base.NPC.ai[1] != 2f)
		{
			base.NPC.ai[1] = 2f;
			SoundEngine.PlaySound(in SoundID.ForceRoar, base.NPC.Center);
		}
		int numHandsAlive = 0;
		for (int i = 0; i < Main.maxNPCs; i++)
		{
			if (Main.npc[i].active && Main.npc[i].type == 36)
			{
				numHandsAlive++;
			}
		}
		bool handsDead = numHandsAlive == 0;
		int numProj = (Main.getGoodWorld ? 22 : (death ? 5 : 3));
		float spread = (Main.getGoodWorld ? 180 : 60);
		float headSpinVelocityMult = (phase3 ? 12f : 4.5f);
		switch (numHandsAlive)
		{
		case 0:
			numProj = (Main.getGoodWorld ? 36 : (death ? 9 : 7));
			spread = (Main.getGoodWorld ? 180 : (death ? 90 : 82));
			headSpinVelocityMult = (phase3 ? 12f : 6f);
			break;
		case 1:
			numProj = (Main.getGoodWorld ? 27 : (death ? 7 : 5));
			spread = (Main.getGoodWorld ? 150 : (death ? 76 : 68));
			headSpinVelocityMult = (phase3 ? 11.5f : 5f);
			break;
		case 2:
			numProj = (Main.getGoodWorld ? 18 : (death ? 6 : 4));
			spread = (Main.getGoodWorld ? 140 : (death ? 70 : 62));
			headSpinVelocityMult = (phase3 ? 11f : 4.5f);
			break;
		case 3:
			numProj = (Main.getGoodWorld ? 15 : (death ? 5 : 3));
			spread = (Main.getGoodWorld ? 130 : (death ? 64 : 56));
			headSpinVelocityMult = (phase3 ? 10.5f : 4f);
			break;
		case 4:
			numProj = (Main.getGoodWorld ? 12 : (death ? 4 : 3));
			spread = (Main.getGoodWorld ? 120 : 56);
			headSpinVelocityMult = (phase3 ? 10f : 3.5f);
			break;
		}
		if (!death && numProj > 3)
		{
			if (phase4)
			{
				numProj--;
			}
			if (phase5 && numProj > 3)
			{
				numProj--;
			}
		}
		if (death)
		{
			headSpinVelocityMult *= 1.08f;
		}
		float moveAwayVelocity = headSpinVelocityMult;
		if (!phase3)
		{
			moveAwayVelocity *= 2f;
		}
		base.NPC.chaseable = handsDead;
		float minDR = 0f;
		float maxDR = 0.9999f;
		calamityGlobalNPC.DR = ((!handsDead) ? ((float)Math.Sqrt(MathHelper.Lerp(minDR, maxDR, respawnHands ? ((respawnHandsLifeRatio - lifeRatio) / respawnHandsLifeRatio) : (2f - lifeRatio / respawnHandsLifeRatio)))) : minDR);
		calamityGlobalNPC.unbreakableDR = !handsDead;
		calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = !handsDead;
		int teleportGateValue = (phase5 ? 180 : 300);
		if (base.NPC.ai[3] <= 60f)
		{
			_ = 1;
		}
		else
			_ = base.NPC.ai[3] > (float)teleportGateValue + 60f;
		if (base.NPC.ai[1] != 3f)
		{
			int dustType = 91;
			if (base.NPC.ai[3] == -60f)
			{
				base.NPC.ai[3] = 0f;
				SoundEngine.PlaySound(in SoundID.Item66, base.NPC.Center);
				if (Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) && Main.netMode != 1)
				{
					int type = 270;
					Vector2 baseVel = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * (death ? 6f : 5f);
					Vector2 firingPos = base.NPC.Center + baseVel * 5f;
					float centralCount = 0.5f * ((float)numProj - 1f);
					for (int j = 0; j < numProj; j++)
					{
						float offset = MathHelper.ToRadians(MathHelper.Lerp((0f - spread) * 0.5f, spread * 0.5f, (float)j / ((float)numProj - 1f)));
						float velocityMult = MathHelper.Lerp(0.5f, 1.5f, MathF.Abs(centralCount - (float)j) / centralCount);
						Projectile.NewProjectileDirect(base.NPC.GetSource_FromAI(), firingPos, baseVel.RotatedBy(offset) * velocityMult, type, SkullDamage, 0f, Main.myPlayer, -2f).timeLeft = 600;
					}
					base.NPC.netUpdate = true;
				}
				for (int m = 0; m < 30; m++)
				{
					int teleportDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dustType, 0f, 0f, 100, default(Color), 3f);
					Main.dust[teleportDust].noGravity = true;
					Main.dust[teleportDust].velocity.X *= 2f;
				}
			}
			base.NPC.ai[3] += 1f + (((phase2 & handsDead) | phase4) ? 0.5f : 0f) - (handsDead ? 0f : 0.5f);
			_ = base.NPC.ai[3];
			if (base.NPC.localAI[0] == 1f && calamityGlobalNPC.newAI[2] == 0f && calamityGlobalNPC.newAI[3] == 0f && Main.netMode != 1)
			{
				Vector2 skullFaceDirection = base.NPC.Center + new Vector2((float)(base.NPC.direction * 20), 6f);
				Vector2 skullTargetDirection = Main.player[base.NPC.target].Center - skullFaceDirection;
				Point skullTileCoords = base.NPC.Center.ToTileCoordinates();
				Point targetTileCoords = Main.player[base.NPC.target].Center.ToTileCoordinates();
				int randomTeleportOffset = 20 - (int)Math.Ceiling(MathHelper.Lerp(0f, 10f, 1f - lifeRatio));
				int skullPositionOffset = 4;
				int targetPositionOffset = randomTeleportOffset - 4;
				int teleportTries = 0;
				bool targetTooFar = false;
				if (((Vector2)(ref skullTargetDirection)).Length() > 2000f)
				{
					targetTooFar = true;
				}
				while (!targetTooFar && teleportTries < 100)
				{
					teleportTries++;
					int teleportTileX = Main.rand.Next(targetTileCoords.X - randomTeleportOffset, targetTileCoords.X + randomTeleportOffset + 1);
					int teleportTileY = Main.rand.Next(targetTileCoords.Y - randomTeleportOffset, targetTileCoords.Y + randomTeleportOffset + 1);
					if ((teleportTileY < targetTileCoords.Y - targetPositionOffset || teleportTileY > targetTileCoords.Y + targetPositionOffset || teleportTileX < targetTileCoords.X - targetPositionOffset || teleportTileX > targetTileCoords.X + targetPositionOffset) && (teleportTileY < skullTileCoords.Y - skullPositionOffset || teleportTileY > skullTileCoords.Y + skullPositionOffset || teleportTileX < skullTileCoords.X - skullPositionOffset || teleportTileX > skullTileCoords.X + skullPositionOffset) && !Main.tile[teleportTileX, teleportTileY].HasUnactuatedTile)
					{
						calamityGlobalNPC.newAI[2] = teleportTileX * 16 - base.NPC.width / 2;
						calamityGlobalNPC.newAI[3] = teleportTileY * 16 - base.NPC.height;
						base.NPC.SyncExtraAI();
						break;
					}
				}
			}
			if (calamityGlobalNPC.newAI[2] != 0f && calamityGlobalNPC.newAI[3] != 0f)
			{
				for (int k = 0; k < 5; k++)
				{
					int teleportDust2 = Dust.NewDust(new Vector2(calamityGlobalNPC.newAI[2], calamityGlobalNPC.newAI[3]), base.NPC.width, base.NPC.height, dustType, 0f, 0f, 100, default(Color), 2f);
					Main.dust[teleportDust2].noGravity = true;
				}
			}
			if (Main.netMode != 1 && base.NPC.localAI[0] == 0f && base.NPC.ai[1] != 1f && calamityGlobalNPC.newAI[2] != 0f && calamityGlobalNPC.newAI[3] != 0f)
			{
				for (int l = 0; l < 30; l++)
				{
					int teleportDust3 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dustType, 0f, 0f, 100, default(Color), 3f);
					Main.dust[teleportDust3].noGravity = true;
					Main.dust[teleportDust3].velocity.X *= 2f;
				}
				base.NPC.Center = new Vector2(calamityGlobalNPC.newAI[2], calamityGlobalNPC.newAI[3]);
				base.NPC.velocity = Vector2.Zero;
				base.NPC.ai[3] = -60f;
				calamityGlobalNPC.newAI[2] = (calamityGlobalNPC.newAI[3] = 0f);
				base.NPC.SyncExtraAI();
				base.NPC.netUpdate = true;
			}
		}
		if (handsDead && base.NPC.ai[1] == 0f && !phase4)
		{
			float skullProjFrequency = (phase2 ? (48f - (death ? (10f * (1f - lifeRatio)) : 0f)) : 60f);
			if (Main.getGoodWorld)
			{
				skullProjFrequency *= 0.8f;
			}
			skullProjFrequency = (float)Math.Ceiling(skullProjFrequency);
			if (Main.netMode != 1 && calamityGlobalNPC.newAI[1] % skullProjFrequency == 0f && calamityGlobalNPC.newAI[1] > 45f)
			{
				Vector2 skullFiringPos = base.NPC.Center;
				float skullProjTargetX = Main.player[base.NPC.target].Center.X - skullFiringPos.X;
				float skullProjTargetY = Main.player[base.NPC.target].Center.Y - skullFiringPos.Y;
				if (Collision.CanHit(skullFiringPos, 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					float skullProjSpeed = (phase2 ? (5f + (death ? (1f * (1f - lifeRatio)) : 0f)) : 4f);
					int spread2 = 50;
					Vector2 skullProjDirection = Utils.SafeNormalize(new Vector2(skullProjTargetX + (float)Main.rand.Next(-spread2, spread2 + 1) * 0.01f, skullProjTargetY + (float)Main.rand.Next(-spread2, spread2 + 1) * 0.01f), Vector2.UnitY);
					skullProjDirection *= skullProjSpeed;
					skullProjDirection += base.NPC.velocity;
					skullFiringPos += skullProjDirection * 5f;
					int type2 = 270;
					int skullProjectile = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), skullFiringPos, skullProjDirection, type2, SkullDamage, 0f, Main.myPlayer, -1f);
					Main.projectile[skullProjectile].timeLeft = 600;
					if (death & handsDead)
					{
						skullProjDirection = Utils.SafeNormalize(new Vector2(skullProjTargetX, skullProjTargetY), Vector2.UnitY);
						skullProjDirection *= skullProjSpeed * 2f;
						int skullProjectile2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), skullFiringPos, skullProjDirection, type2, SkullDamage, 0f, Main.myPlayer, -2f);
						Main.projectile[skullProjectile2].timeLeft = 600;
					}
					base.NPC.netUpdate = true;
				}
			}
		}
		if (base.NPC.ai[1] == 0f)
		{
			calamityGlobalNPC.newAI[1]++;
			float chargePhaseChangeRateBoost = ((!phase5) ? ((!phase4) ? ((death ? 4.5f : 3f) * ((1f - lifeRatio) / (1f - phase4LifeRatio))) : (death ? 6f : 4f)) : (death ? 24f : 8f));
			if (!handsDead)
			{
				chargePhaseChangeRateBoost *= 0.25f;
			}
			float chargePhaseChangeRate = chargePhaseChangeRateBoost + 1f;
			base.NPC.ai[2] += chargePhaseChangeRate;
			base.NPC.localAI[1] += chargePhaseChangeRate;
			float chargePhaseGateValue = 600f;
			if (base.NPC.localAI[1] > chargePhaseGateValue)
			{
				base.NPC.localAI[1] = chargePhaseGateValue;
			}
			float forcedMoveAwayTime = (death ? 30f : 45f);
			float canChargeDistance = 320f;
			bool hasMovedForcedDistance = base.NPC.localAI[2] >= forcedMoveAwayTime;
			bool canCharge = Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) >= canChargeDistance;
			bool num = (base.NPC.ai[2] >= chargePhaseGateValue) & canCharge;
			bool forceCharge = base.NPC.ai[2] > chargePhaseGateValue + 120f;
			if (num | forceCharge)
			{
				base.NPC.localAI[2]++;
				if ((hasMovedForcedDistance || !phase3) && Main.netMode != 1)
				{
					base.NPC.ai[2] = 0f;
					base.NPC.ai[1] = 1f;
					base.NPC.localAI[0] = 1f;
					base.NPC.localAI[1] = chargePhaseGateValue;
					base.NPC.localAI[2] = 0f;
					calamityGlobalNPC.newAI[1] = 0f;
					base.NPC.SyncExtraAI();
					base.NPC.SyncVanillaLocalAI();
					base.NPC.netUpdate = true;
				}
			}
			float headYAcceleration = (Main.getGoodWorld ? 0.07f : (death ? (0.06f + 0.04f * (1f - lifeRatio)) : 0.04f));
			float headYTopSpeed = headYAcceleration * 100f;
			float headXAcceleration = (Main.getGoodWorld ? 0.21f : (death ? (0.16f + 0.08f * (1f - lifeRatio)) : 0.08f));
			float headXTopSpeed = headXAcceleration * 100f;
			if (!Main.getGoodWorld)
			{
				_ = death;
			}
			float moveAwayGateValue = chargePhaseGateValue - (5f + chargePhaseChangeRate);
			if (base.NPC.ai[2] >= moveAwayGateValue)
			{
				if (!canCharge || !hasMovedForcedDistance)
				{
					float phase5Multiplier = 1.2f;
					float maxVelocity = (base.NPC.ai[2] - moveAwayGateValue) * (moveAwayVelocity * 0.002f);
					if (phase5)
					{
						maxVelocity *= phase5Multiplier;
					}
					float maxVelocityCap = moveAwayVelocity;
					if (phase5)
					{
						maxVelocityCap *= phase5Multiplier;
					}
					if (maxVelocity > maxVelocityCap)
					{
						maxVelocity = maxVelocityCap;
					}
					base.NPC.velocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * (0f - maxVelocity);
				}
				if (phase3)
				{
					base.NPC.rotation += (float)base.NPC.direction * 0.3f;
					if (base.NPC.localAI[0] == 0f)
					{
						base.NPC.localAI[0] = 1f;
						base.NPC.SyncVanillaLocalAI();
						SoundEngine.PlaySound(in SoundID.ForceRoar, base.NPC.Center);
					}
				}
				else
				{
					base.NPC.rotation = base.NPC.velocity.X / 15f;
				}
				return false;
			}
			base.NPC.rotation = base.NPC.velocity.X / 15f;
			if (base.NPC.Top.Y > Main.player[base.NPC.target].Top.Y - 250f)
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y *= 0.98f;
				}
				base.NPC.velocity.Y -= headYAcceleration;
				if (base.NPC.velocity.Y > headYTopSpeed)
				{
					base.NPC.velocity.Y = headYTopSpeed;
				}
			}
			else if (base.NPC.Top.Y < Main.player[base.NPC.target].Top.Y - 250f)
			{
				if (base.NPC.velocity.Y < 0f)
				{
					base.NPC.velocity.Y *= 0.98f;
				}
				base.NPC.velocity.Y += headYAcceleration;
				if (base.NPC.velocity.Y < 0f - headYTopSpeed)
				{
					base.NPC.velocity.Y = 0f - headYTopSpeed;
				}
			}
			if (base.NPC.Center.X > Main.player[base.NPC.target].Center.X)
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X *= 0.98f;
				}
				base.NPC.velocity.X -= headXAcceleration;
				if (base.NPC.velocity.X > headXTopSpeed)
				{
					base.NPC.velocity.X = headXTopSpeed;
				}
			}
			if (base.NPC.Center.X < Main.player[base.NPC.target].Center.X)
			{
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X *= 0.98f;
				}
				base.NPC.velocity.X += headXAcceleration;
				if (base.NPC.velocity.X < 0f - headXTopSpeed)
				{
					base.NPC.velocity.X = 0f - headXTopSpeed;
				}
			}
		}
		else if (base.NPC.ai[1] == 1f)
		{
			if (Main.getGoodWorld)
			{
				base.NPC.reflectsProjectiles = true;
				if (Main.netMode != 1 && base.NPC.ai[2] == 0f)
				{
					if (NPC.CountNPCS(32) < 6)
					{
						for (int n = 0; n < 1000; n++)
						{
							int headYAcceleration2 = (int)(base.NPC.Center.X / 16f) + Main.rand.Next(-50, 51);
							int num2;
							for (num2 = (int)(base.NPC.Center.Y / 16f) + Main.rand.Next(-50, 51); num2 < Main.maxTilesY - 10 && !WorldGen.SolidTile(headYAcceleration2, num2); num2++)
							{
							}
							num2--;
							if (!WorldGen.SolidTile(headYAcceleration2, num2))
							{
								int headXAcceleration2 = NPC.NewNPC(base.NPC.GetSource_FromAI(), headYAcceleration2 * 16 + 8, num2 * 16, 32);
								if (Main.dedServ && headXAcceleration2 < Main.maxNPCs)
								{
									NetMessage.SendData(23, -1, -1, null, headXAcceleration2);
								}
								break;
							}
						}
					}
					if (Main.zenithWorld && !NPC.AnyNPCs(286))
					{
						for (int num3 = 0; num3 < 1000; num3++)
						{
							int headYAcceleration3 = (int)(base.NPC.Center.X / 16f) + Main.rand.Next(-50, 51);
							int num4;
							for (num4 = (int)(base.NPC.Center.Y / 16f) + Main.rand.Next(-50, 51); num4 < Main.maxTilesY - 10 && !WorldGen.SolidTile(headYAcceleration3, num4); num4++)
							{
							}
							num4--;
							if (!WorldGen.SolidTile(headYAcceleration3, num4))
							{
								int headXAcceleration3 = NPC.NewNPC(base.NPC.GetSource_FromAI(), headYAcceleration3 * 16 + 8, num4 * 16, 286);
								if (Main.dedServ && headXAcceleration3 < Main.maxNPCs)
								{
									NetMessage.SendData(23, -1, -1, null, headXAcceleration3);
								}
								break;
							}
						}
					}
				}
			}
			base.NPC.defense = base.NPC.defDefense - 10;
			base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * SpinDamageMult);
			float phaseChangeRateBoost = (phase3 ? 0f : (1f - (lifeRatio - phase3LifeRatio) / (1f - phase3LifeRatio)));
			base.NPC.ai[2] += 1f + phaseChangeRateBoost;
			calamityGlobalNPC.newAI[1]++;
			if (calamityGlobalNPC.newAI[1] == 2f)
			{
				SoundEngine.PlaySound(phase3 ? SoundID.ForceRoarPitched : SoundID.ForceRoar, base.NPC.Center);
			}
			if (base.NPC.localAI[1] > 0f)
			{
				base.NPC.localAI[1] -= 2f;
				if (base.NPC.localAI[1] <= 0f)
				{
					base.NPC.localAI[1] = 0f;
					base.NPC.SyncVanillaLocalAI();
				}
			}
			bool dontGoMach10 = false;
			float dashPhaseTime = (death ? 210f : 300f);
			if (base.NPC.ai[2] >= dashPhaseTime && Main.netMode != 1)
			{
				if (Main.getGoodWorld && Main.netMode != 1 && NPC.CountNPCS(32) < 6)
				{
					for (int num5 = 0; num5 < 1000; num5++)
					{
						int headYAcceleration4 = (int)(base.NPC.Center.X / 16f) + Main.rand.Next(-50, 51);
						int num6;
						for (num6 = (int)(base.NPC.Center.Y / 16f) + Main.rand.Next(-50, 51); num6 < Main.maxTilesY - 10 && !WorldGen.SolidTile(headYAcceleration4, num6); num6++)
						{
						}
						num6--;
						if (!WorldGen.SolidTile(headYAcceleration4, num6))
						{
							int headXAcceleration4 = NPC.NewNPC(base.NPC.GetSource_FromAI(), headYAcceleration4 * 16 + 8, num6 * 16, 32);
							if (Main.dedServ && headXAcceleration4 < Main.maxNPCs)
							{
								NetMessage.SendData(23, -1, -1, null, headXAcceleration4);
							}
							break;
						}
					}
				}
				base.NPC.ai[2] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = 0f;
				calamityGlobalNPC.newAI[1] = 0f;
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
				base.NPC.SyncVanillaLocalAI();
				base.NPC.SyncExtraAI();
				base.NPC.netUpdate = true;
				dontGoMach10 = true;
			}
			base.NPC.rotation += (float)base.NPC.direction * 0.3f;
			Vector2 headSpinPos = base.NPC.Center;
			float num7 = Main.player[base.NPC.target].Center.X - headSpinPos.X;
			float headSpinTargetY = Main.player[base.NPC.target].Center.Y - headSpinPos.Y;
			float headSpinTargetDist = (float)Math.Sqrt(num7 * num7 + headSpinTargetY * headSpinTargetY);
			if (!phase3)
			{
				float velocityBoost = MathHelper.Lerp(0f, 3f, (1f - lifeRatio) / (1f - phase3LifeRatio));
				if (handsDead)
				{
					headSpinVelocityMult += velocityBoost;
				}
			}
			float altDashStopDistance = (death ? 320f : 400f);
			float headSpeedIncreaseDist = (phase3 ? altDashStopDistance : 160f);
			if (headSpinTargetDist > headSpeedIncreaseDist)
			{
				float baseDistanceVelocityMult = 1f + MathHelper.Clamp((headSpinTargetDist - headSpeedIncreaseDist) * 0.0015f, 0.05f, death ? 2f : 1.5f);
				headSpinVelocityMult *= baseDistanceVelocityMult;
			}
			if (Main.getGoodWorld)
			{
				headSpinVelocityMult *= 1.3f;
			}
			headSpinTargetDist = headSpinVelocityMult / headSpinTargetDist;
			Vector2 headSpinVelocity = new Vector2(num7, headSpinTargetY) * headSpinTargetDist;
			if (!dontGoMach10)
			{
				if (phase3)
				{
					float altDashPhaseTime = dashPhaseTime * (death ? 0.9f : 0.85f);
					if (base.NPC.ai[2] < altDashPhaseTime)
					{
						if (base.NPC.Center.Distance(Main.player[base.NPC.target].Center) > altDashStopDistance || base.NPC.ai[2] == 1f + phaseChangeRateBoost)
						{
							base.NPC.velocity = headSpinVelocity.SafeNormalize(Vector2.UnitY) * headSpinVelocityMult + base.NPC.Center.DirectionTo(Main.player[base.NPC.target].Center) * 2f;
						}
						else
						{
							base.NPC.ai[2] = altDashPhaseTime;
						}
					}
				}
				else
				{
					base.NPC.velocity = headSpinVelocity;
				}
			}
		}
		else if (base.NPC.ai[1] == 2f)
		{
			base.NPC.damage = 1000;
			calamityGlobalNPC.DR = 0.9999f;
			calamityGlobalNPC.unbreakableDR = true;
			calamityGlobalNPC.CurrentlyEnraged = true;
			calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = true;
			base.NPC.rotation += (float)base.NPC.direction * 0.3f;
			Vector2 enrageSpinPos = base.NPC.Center;
			float enrageSpinTargetX = Main.player[base.NPC.target].Center.X - enrageSpinPos.X;
			float enrageSpinTargetY = Main.player[base.NPC.target].Center.Y - enrageSpinPos.Y;
			float enrageSpinTargetDist = (float)Math.Sqrt(enrageSpinTargetX * enrageSpinTargetX + enrageSpinTargetY * enrageSpinTargetY);
			enrageSpinTargetDist = 8f / enrageSpinTargetDist;
			base.NPC.velocity.X = enrageSpinTargetX * enrageSpinTargetDist;
			base.NPC.velocity.Y = enrageSpinTargetY * enrageSpinTargetDist;
		}
		else if (base.NPC.ai[1] == 3f)
		{
			if (base.NPC.ai[3] != 0f || calamityGlobalNPC.newAI[2] != 0f || calamityGlobalNPC.newAI[3] != 0f)
			{
				base.NPC.ai[3] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				base.NPC.SyncExtraAI();
				base.NPC.netUpdate = true;
			}
			base.NPC.velocity.Y += 0.1f;
			if (base.NPC.velocity.Y < 0f)
			{
				base.NPC.velocity.Y *= 0.95f;
			}
			base.NPC.velocity.X *= 0.95f;
			if (base.NPC.timeLeft > 50)
			{
				base.NPC.timeLeft = 50;
			}
		}
		if (base.NPC.ai[1] != 2f && base.NPC.ai[1] != 3f && numHandsAlive != 0)
		{
			int idleDust = Dust.NewDust(new Vector2(base.NPC.Center.X - 15f - base.NPC.velocity.X * 5f, base.NPC.position.Y + (float)base.NPC.height - 2f), 30, 10, 5, (0f - base.NPC.velocity.X) * 0.2f, 3f, 0, default(Color), 2f);
			Main.dust[idleDust].noGravity = true;
			Main.dust[idleDust].velocity.X = Main.dust[idleDust].velocity.X * 1.3f;
			Main.dust[idleDust].velocity.X = Main.dust[idleDust].velocity.X + base.NPC.velocity.X * 0.4f;
			Main.dust[idleDust].velocity.Y = Main.dust[idleDust].velocity.Y + (2f + base.NPC.velocity.Y);
			for (int num8 = 0; num8 < 2; num8++)
			{
				idleDust = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y + 120f), base.NPC.width, 60, 5, base.NPC.velocity.X, base.NPC.velocity.Y, 0, default(Color), 2f);
				Main.dust[idleDust].noGravity = true;
				Dust obj = Main.dust[idleDust];
				obj.velocity -= base.NPC.velocity;
				Main.dust[idleDust].velocity.Y = Main.dust[idleDust].velocity.Y + 5f;
			}
		}
		return false;
		void SpawnHands()
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
			int skeletronHand = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 36, base.NPC.whoAmI);
			Main.npc[skeletronHand].ai[0] = (death ? (-1.3f) : (-1f));
			Main.npc[skeletronHand].ai[1] = base.NPC.whoAmI;
			Main.npc[skeletronHand].target = base.NPC.target;
			Main.npc[skeletronHand].netUpdate = true;
			skeletronHand = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 36, base.NPC.whoAmI);
			Main.npc[skeletronHand].ai[0] = (death ? 1.3f : 1f);
			Main.npc[skeletronHand].ai[1] = base.NPC.whoAmI;
			Main.npc[skeletronHand].ai[3] = 150f;
			Main.npc[skeletronHand].Calamity().newAI[2] = 150f;
			Main.npc[skeletronHand].target = base.NPC.target;
			Main.npc[skeletronHand].netUpdate = true;
			if (death)
			{
				skeletronHand = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 36, base.NPC.whoAmI);
				Main.npc[skeletronHand].ai[0] = -1.3f;
				Main.npc[skeletronHand].Calamity().newAI[0] = -1f;
				Main.npc[skeletronHand].ai[1] = base.NPC.whoAmI;
				Main.npc[skeletronHand].ai[3] = (respawnHands ? (-75f) : 0f);
				Main.npc[skeletronHand].Calamity().newAI[2] = (respawnHands ? (-75f) : 0f);
				Main.npc[skeletronHand].target = base.NPC.target;
				Main.npc[skeletronHand].netUpdate = true;
				skeletronHand = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 36, base.NPC.whoAmI);
				Main.npc[skeletronHand].ai[0] = 1.3f;
				Main.npc[skeletronHand].Calamity().newAI[0] = -1f;
				Main.npc[skeletronHand].ai[1] = base.NPC.whoAmI;
				Main.npc[skeletronHand].ai[3] = (respawnHands ? 75f : 150f);
				Main.npc[skeletronHand].Calamity().newAI[2] = (respawnHands ? 75f : 150f);
				Main.npc[skeletronHand].target = base.NPC.target;
				Main.npc[skeletronHand].netUpdate = true;
			}
		}
	}

	public override void PostDraw(Mod mod, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		float beginTelegraphGateValue = 480f;
		if (base.NPC.localAI[1] > beginTelegraphGateValue)
		{
			float colorScale = MathHelper.Clamp((base.NPC.localAI[1] - beginTelegraphGateValue) / 120f, 0f, 1f);
			Color drawColor2 = new Color(150, 150, 150, 0) * colorScale;
			Vector2 halfSize = base.NPC.frame.Size() / 2f;
			SpriteEffects spriteEffects = (SpriteEffects)(base.NPC.spriteDirection == 1);
			for (int i = 0; i < 2; i++)
			{
				spriteBatch.Draw(TextureAssets.Npc[base.NPC.type].Value, base.NPC.Center - screenPos + new Vector2(0f, base.NPC.gfxOffY), (Rectangle?)base.NPC.frame, drawColor2, base.NPC.rotation, halfSize, base.NPC.scale, spriteEffects, 0f);
			}
		}
	}
}
