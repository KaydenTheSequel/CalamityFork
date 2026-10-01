using System;
using System.Collections.Generic;
using CalamityMod.NPCs.Astral;
using CalamityMod.NPCs.Crags;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.PlagueEnemies;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.RegularEnemies;

public static class RevengeanceAndDeathAI
{
	public class AncientVisionAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00da: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0121: Unknown result type (might be due to invalid IL or missing references)
			//IL_0127: Unknown result type (might be due to invalid IL or missing references)
			//IL_012c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0134: Unknown result type (might be due to invalid IL or missing references)
			//IL_0139: Unknown result type (might be due to invalid IL or missing references)
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_013f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_014b: Unknown result type (might be due to invalid IL or missing references)
			//IL_014c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0520: Unknown result type (might be due to invalid IL or missing references)
			//IL_052b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c8f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c9a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c9f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0caf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cb7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cbc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cf4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cfa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d2b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d36: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d3b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d40: Unknown result type (might be due to invalid IL or missing references)
			//IL_0216: Unknown result type (might be due to invalid IL or missing references)
			//IL_0221: Unknown result type (might be due to invalid IL or missing references)
			//IL_0226: Unknown result type (might be due to invalid IL or missing references)
			//IL_022b: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0611: Unknown result type (might be due to invalid IL or missing references)
			//IL_0617: Unknown result type (might be due to invalid IL or missing references)
			//IL_0648: Unknown result type (might be due to invalid IL or missing references)
			//IL_0653: Unknown result type (might be due to invalid IL or missing references)
			//IL_0658: Unknown result type (might be due to invalid IL or missing references)
			//IL_065d: Unknown result type (might be due to invalid IL or missing references)
			//IL_03df: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0429: Unknown result type (might be due to invalid IL or missing references)
			//IL_042f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0455: Unknown result type (might be due to invalid IL or missing references)
			//IL_045f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0464: Unknown result type (might be due to invalid IL or missing references)
			//IL_0472: Unknown result type (might be due to invalid IL or missing references)
			//IL_047d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0487: Unknown result type (might be due to invalid IL or missing references)
			//IL_048c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0491: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_023e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0243: Unknown result type (might be due to invalid IL or missing references)
			//IL_0281: Unknown result type (might be due to invalid IL or missing references)
			//IL_0297: Unknown result type (might be due to invalid IL or missing references)
			//IL_029c: Unknown result type (might be due to invalid IL or missing references)
			//IL_029e: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0309: Unknown result type (might be due to invalid IL or missing references)
			//IL_030e: Unknown result type (might be due to invalid IL or missing references)
			//IL_036b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0376: Unknown result type (might be due to invalid IL or missing references)
			//IL_037b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0380: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b89: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ba4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a4e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a55: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a5a: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0800: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bf9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c00: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c05: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_08b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0900: Unknown result type (might be due to invalid IL or missing references)
			//IL_093c: Unknown result type (might be due to invalid IL or missing references)
			//IL_095d: Unknown result type (might be due to invalid IL or missing references)
			if (base.NPC.alpha > 0)
			{
				base.NPC.alpha -= 30;
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
			}
			base.NPC.noGravity = true;
			base.NPC.noTileCollide = true;
			base.NPC.knockBackResist = 0f;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.whoAmI == base.NPC.whoAmI || n.type != base.NPC.type)
				{
					continue;
				}
				Vector2 targetDirection = n.Center - base.NPC.Center;
				if (!(((Vector2)(ref targetDirection)).Length() < 50f))
				{
					continue;
				}
				((Vector2)(ref targetDirection)).Normalize();
				if (targetDirection.X == 0f && targetDirection.Y == 0f)
				{
					if (n.whoAmI > base.NPC.whoAmI)
					{
						targetDirection.X = 1f;
					}
					else
					{
						targetDirection.X = -1f;
					}
				}
				targetDirection *= 0.4f;
				NPC nPC = base.NPC;
				nPC.velocity -= targetDirection;
				n.velocity += targetDirection;
			}
			if (base.NPC.type == 472 && base.NPC.localAI[0] < 120f)
			{
				if (base.NPC.localAI[0] == 0f)
				{
					SoundEngine.PlaySound(in SoundID.Item8, base.NPC.Center);
					base.NPC.TargetClosest();
					if (base.NPC.direction > 0)
					{
						base.NPC.velocity.X += 2f;
					}
					else
					{
						base.NPC.velocity.X -= 2f;
					}
					NPC nPC2 = base.NPC;
					nPC2.position += base.NPC.netOffset;
					Vector2 apparitionRandVelocity = default(Vector2);
					for (int j = 0; j < 20; j++)
					{
						Vector2 apparitionCenter = base.NPC.Center;
						apparitionCenter.Y -= 18f;
						((Vector2)(ref apparitionRandVelocity))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
						((Vector2)(ref apparitionRandVelocity)).Normalize();
						apparitionRandVelocity *= (float)Main.rand.Next(0, 100) * 0.1f;
						apparitionCenter += apparitionRandVelocity;
						((Vector2)(ref apparitionRandVelocity)).Normalize();
						apparitionRandVelocity *= (float)Main.rand.Next(50, 90) * 0.2f;
						int shadowflameDust = Dust.NewDust(apparitionCenter, 1, 1, 27);
						Main.dust[shadowflameDust].velocity = -apparitionRandVelocity * 0.3f;
						Main.dust[shadowflameDust].alpha = 100;
						if (Main.rand.NextBool())
						{
							Main.dust[shadowflameDust].noGravity = true;
							Main.dust[shadowflameDust].scale += 0.3f;
						}
					}
					NPC nPC3 = base.NPC;
					nPC3.position -= base.NPC.netOffset;
				}
				base.NPC.localAI[0]++;
				float dustAmt = (1f - base.NPC.localAI[0] / 120f) * 20f;
				for (int k = 0; (float)k < dustAmt; k++)
				{
					if (Main.rand.NextBool(5))
					{
						NPC nPC4 = base.NPC;
						nPC4.position += base.NPC.netOffset;
						int idleShadowflameDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 27);
						Main.dust[idleShadowflameDust].alpha = 100;
						Dust obj = Main.dust[idleShadowflameDust];
						obj.velocity *= 0.3f;
						Dust obj2 = Main.dust[idleShadowflameDust];
						obj2.velocity += base.NPC.velocity * 0.75f;
						Main.dust[idleShadowflameDust].noGravity = true;
						NPC nPC5 = base.NPC;
						nPC5.position -= base.NPC.netOffset;
					}
				}
			}
			if (base.NPC.type == 521 && base.NPC.localAI[0] < 120f)
			{
				if (base.NPC.localAI[0] == 0f)
				{
					SoundEngine.PlaySound(in SoundID.Item8, base.NPC.Center);
					base.NPC.TargetClosest();
					if (base.NPC.direction > 0)
					{
						base.NPC.velocity.X += 2f;
					}
					else
					{
						base.NPC.velocity.X -= 2f;
					}
				}
				base.NPC.localAI[0]++;
				int dustPosition = 10;
				for (int l = 0; l < 2; l++)
				{
					NPC nPC6 = base.NPC;
					nPC6.position += base.NPC.netOffset;
					int visionDust = Dust.NewDust(base.NPC.position - new Vector2((float)dustPosition), base.NPC.width + dustPosition * 2, base.NPC.height + dustPosition * 2, 228, 0f, 0f, 100, default(Color), 2f);
					Main.dust[visionDust].noGravity = true;
					Main.dust[visionDust].noLight = true;
					NPC nPC7 = base.NPC;
					nPC7.position -= base.NPC.netOffset;
				}
			}
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.TargetClosest();
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = base.NPC.direction;
			}
			else if (base.NPC.ai[0] == 1f)
			{
				base.NPC.TargetClosest();
				float xVelocityMult1 = 0.5f;
				float maxXVelocity1 = 10f;
				float maxYVelocity1 = 4f;
				float turnAroundDist1 = 550f;
				float yVelocityMult1 = 3f;
				if (base.NPC.type == 521)
				{
					xVelocityMult1 = 0.8f;
					maxXVelocity1 = 16f;
					turnAroundDist1 = 440f;
					maxYVelocity1 = 6f;
				}
				if (CalamityWorld.death)
				{
					xVelocityMult1 *= 1.25f;
					maxXVelocity1 *= 1.25f;
					turnAroundDist1 *= 0.9f;
					yVelocityMult1--;
				}
				base.NPC.velocity.X += base.NPC.ai[1] * xVelocityMult1;
				if (base.NPC.velocity.X > maxXVelocity1)
				{
					base.NPC.velocity.X = maxXVelocity1;
				}
				if (base.NPC.velocity.X < 0f - maxXVelocity1)
				{
					base.NPC.velocity.X = 0f - maxXVelocity1;
				}
				float targetYDist1 = Main.player[base.NPC.target].Center.Y - base.NPC.Center.Y;
				if (Math.Abs(targetYDist1) > maxYVelocity1)
				{
					yVelocityMult1 = (CalamityWorld.death ? 10f : 12f);
				}
				if (targetYDist1 > maxYVelocity1)
				{
					targetYDist1 = maxYVelocity1;
				}
				else if (targetYDist1 < 0f - maxYVelocity1)
				{
					targetYDist1 = 0f - maxYVelocity1;
				}
				base.NPC.velocity.Y = (base.NPC.velocity.Y * (yVelocityMult1 - 1f) + targetYDist1) / yVelocityMult1;
				if ((base.NPC.ai[1] > 0f && Main.player[base.NPC.target].Center.X - base.NPC.Center.X < 0f - turnAroundDist1) || (base.NPC.ai[1] < 0f && Main.player[base.NPC.target].Center.X - base.NPC.Center.X > turnAroundDist1))
				{
					base.NPC.ai[0] = 2f;
					base.NPC.ai[1] = 0f;
					if (base.NPC.Center.Y + 20f > Main.player[base.NPC.target].Center.Y)
					{
						base.NPC.ai[1] = -1f;
					}
					else
					{
						base.NPC.ai[1] = 1f;
					}
				}
			}
			else if (base.NPC.ai[0] == 2f)
			{
				float decelYVelocityMult = 0.6f;
				float deceleration = 0.93f;
				float decelerationDist = 7f;
				if (base.NPC.type == 521)
				{
					decelYVelocityMult = 0.45f;
					decelerationDist = 10f;
					deceleration = 0.87f;
				}
				if (CalamityWorld.death)
				{
					decelYVelocityMult *= 1.25f;
					deceleration *= 0.9f;
					decelerationDist *= 1.25f;
				}
				base.NPC.velocity.Y += base.NPC.ai[1] * decelYVelocityMult;
				if (((Vector2)(ref base.NPC.velocity)).Length() > decelerationDist)
				{
					NPC nPC8 = base.NPC;
					nPC8.velocity *= deceleration;
				}
				if (base.NPC.velocity.X > -1f && base.NPC.velocity.X < 1f)
				{
					base.NPC.TargetClosest();
					base.NPC.ai[0] = 3f;
					base.NPC.ai[1] = base.NPC.direction;
				}
			}
			else if (base.NPC.ai[0] == 3f)
			{
				float xVelocityMult3 = 0.6f;
				float yAlignSpeed = 0.3f;
				float decelerationDist3 = 7f;
				float deceleration3 = 0.93f;
				if (base.NPC.type == 521)
				{
					xVelocityMult3 = 0.8f;
					yAlignSpeed = 0.45f;
					decelerationDist3 = 9f;
					deceleration3 = 0.87f;
				}
				if (CalamityWorld.death)
				{
					xVelocityMult3 *= 1.25f;
					yAlignSpeed *= 1.25f;
					decelerationDist3 *= 1.25f;
					deceleration3 *= 0.9f;
				}
				base.NPC.velocity.X += base.NPC.ai[1] * xVelocityMult3;
				if (base.NPC.Center.Y > Main.player[base.NPC.target].Center.Y)
				{
					base.NPC.velocity.Y -= yAlignSpeed;
				}
				else
				{
					base.NPC.velocity.Y += yAlignSpeed;
				}
				if (((Vector2)(ref base.NPC.velocity)).Length() > decelerationDist3)
				{
					NPC nPC9 = base.NPC;
					nPC9.velocity *= deceleration3;
				}
				if (base.NPC.velocity.Y > -1f && base.NPC.velocity.Y < 1f)
				{
					base.NPC.TargetClosest();
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = base.NPC.direction;
				}
			}
			if (base.NPC.type == 521)
			{
				int squidDustVelocity = 10;
				NPC nPC10 = base.NPC;
				nPC10.position += base.NPC.netOffset;
				int squidDust = Dust.NewDust(base.NPC.position - new Vector2((float)squidDustVelocity), base.NPC.width + squidDustVelocity * 2, base.NPC.height + squidDustVelocity * 2, 228, 0f, 0f, 100, default(Color), 2f);
				Main.dust[squidDust].noGravity = true;
				Main.dust[squidDust].noLight = true;
				NPC nPC11 = base.NPC;
				nPC11.position -= base.NPC.netOffset;
			}
			return false;
		}
	}

	public class AngryNimbusAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_014b: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			//IL_022b: Unknown result type (might be due to invalid IL or missing references)
			//IL_017f: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_025f: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_0305: Unknown result type (might be due to invalid IL or missing references)
			//IL_0335: Unknown result type (might be due to invalid IL or missing references)
			//IL_0361: Unknown result type (might be due to invalid IL or missing references)
			//IL_0403: Unknown result type (might be due to invalid IL or missing references)
			//IL_0439: Unknown result type (might be due to invalid IL or missing references)
			//IL_043e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0443: Unknown result type (might be due to invalid IL or missing references)
			//IL_0451: Unknown result type (might be due to invalid IL or missing references)
			//IL_0453: Unknown result type (might be due to invalid IL or missing references)
			//IL_045d: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.noGravity = true;
			base.NPC.TargetClosest();
			float speed = (CalamityWorld.death ? 9f : 6f);
			float acceleration = (CalamityWorld.death ? 0.3f : 0.25f);
			Vector2 idealVelocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center - 300f * Vector2.UnitY) * speed;
			if (base.NPC.Distance(Main.player[base.NPC.target].Center) < 20f)
			{
				idealVelocity = base.NPC.velocity;
			}
			if (base.NPC.velocity.X < idealVelocity.X)
			{
				base.NPC.velocity.X += acceleration;
				if (base.NPC.velocity.X < 0f && idealVelocity.X > 0f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + acceleration * 2f;
				}
			}
			else if (base.NPC.velocity.X > idealVelocity.X)
			{
				base.NPC.velocity.X -= acceleration;
				if (base.NPC.velocity.X > 0f && idealVelocity.X < 0f)
				{
					base.NPC.velocity.X -= acceleration * 2f;
				}
			}
			if (base.NPC.velocity.Y < idealVelocity.Y)
			{
				base.NPC.velocity.Y += acceleration;
				if (base.NPC.velocity.Y < 0f && idealVelocity.Y > 0f)
				{
					base.NPC.velocity.Y += acceleration * 2f;
				}
			}
			else if (base.NPC.velocity.Y > idealVelocity.Y)
			{
				base.NPC.velocity.Y -= acceleration;
				if (base.NPC.velocity.Y > 0f && idealVelocity.Y < 0f)
				{
					base.NPC.velocity.Y -= acceleration * 2f;
				}
			}
			float minXRainDistance = (CalamityWorld.death ? 200f : 150f);
			if (base.NPC.Center.X > Main.player[base.NPC.target].position.X - minXRainDistance && base.NPC.position.X < Main.player[base.NPC.target].Center.X + minXRainDistance && base.NPC.Center.Y < Main.player[base.NPC.target].position.Y && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) && Main.netMode != 1)
			{
				if (base.NPC.justHit)
				{
					base.NPC.ai[0] = 0f;
				}
				base.NPC.ai[0]++;
				if (base.NPC.ai[0] % 8f == 0f)
				{
					Vector2 rainSpawnPosition = base.NPC.position + new Vector2(10f + (float)Main.rand.Next(base.NPC.width - 20), (float)base.NPC.height + 4f);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), rainSpawnPosition, Vector2.UnitY * 5f, 264, 20, 0f, Main.myPlayer);
					if (base.NPC.ai[0] % 16f == 0f)
					{
						float speedX = Main.rand.NextFloat(CalamityWorld.death ? (-6f) : (-3f), CalamityWorld.death ? 6f : 3f) * (Main.rand.NextFloat() - 0.5f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), rainSpawnPosition, new Vector2(speedX, 5f), 349, 20, 0f, Main.myPlayer);
					}
				}
				if (base.NPC.ai[0] >= 607f)
				{
					base.NPC.ai[0] = 0f;
				}
			}
			return false;
		}
	}

	public class AntlionAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0197: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0299: Unknown result type (might be due to invalid IL or missing references)
			//IL_029e: Unknown result type (might be due to invalid IL or missing references)
			//IL_029f: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_0301: Unknown result type (might be due to invalid IL or missing references)
			//IL_030b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0310: Unknown result type (might be due to invalid IL or missing references)
			//IL_0251: Unknown result type (might be due to invalid IL or missing references)
			//IL_025c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0484: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0621: Unknown result type (might be due to invalid IL or missing references)
			//IL_0655: Unknown result type (might be due to invalid IL or missing references)
			//IL_065b: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.TargetClosest();
			float xVel = Main.player[base.NPC.target].Center.X - base.NPC.Center.X;
			float yVel = Main.player[base.NPC.target].position.Y - base.NPC.Center.Y;
			Vector2 velocity = default(Vector2);
			((Vector2)(ref velocity))._002Ector(xVel, yVel);
			float targetDist = ((Vector2)(ref velocity)).Length();
			targetDist = 12f / targetDist;
			velocity.X *= targetDist;
			velocity.Y *= targetDist;
			bool canShoot = false;
			if (base.NPC.directionY < 0)
			{
				base.NPC.rotation = velocity.ToRotation() + (float)Math.PI / 2f;
				canShoot = Math.Abs(base.NPC.rotation) <= 1.2f;
				if (base.NPC.rotation < -0.8f)
				{
					base.NPC.rotation = -0.8f;
				}
				else if (base.NPC.rotation > 0.8f)
				{
					base.NPC.rotation = 0.8f;
				}
				if (base.NPC.velocity.X != 0f)
				{
					base.NPC.velocity.X *= 0.9f;
					if (Math.Abs(base.NPC.velocity.X) < 0.1f)
					{
						base.NPC.netUpdate = true;
						base.NPC.velocity.X = 0f;
					}
				}
			}
			bool lineofSight = Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
			if (base.NPC.justHit || !lineofSight)
			{
				base.NPC.ai[0] = 199f;
			}
			if (base.NPC.ai[0] > 0f)
			{
				if (base.NPC.ai[0] == 200f)
				{
					SoundEngine.PlaySound(in SoundID.NPCDeath13, base.NPC.Center);
				}
				base.NPC.ai[0]--;
			}
			if (base.NPC.ai[0] <= 30f)
			{
				Dust dust = Dust.NewDustDirect(base.NPC.Center + velocity.SafeNormalize(-Vector2.UnitY) * 16f + Main.rand.NextVector2CircularEdge(6f, 6f), 1, 1, 32, 0f, 0f, 0, default(Color), 1.5f);
				dust.noGravity = true;
				dust.velocity *= 0f;
			}
			if ((((Main.netMode != 1) & canShoot) && base.NPC.ai[0] == 0f) & lineofSight)
			{
				base.NPC.ai[0] = 200f;
				int damage = 10;
				int projType = 31;
				int projAmt = (Main.zenithWorld ? 100 : ((!Main.getGoodWorld) ? 1 : 8));
				for (int i = 0; i < projAmt; i++)
				{
					velocity.X += (float)Main.rand.Next(-30, 31) * 0.05f;
					velocity.Y += (float)Main.rand.Next(-30, 31) * 0.05f;
					int sandBall = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity, projType, damage, 0f, Main.myPlayer);
					Main.projectile[sandBall].ai[0] = 2f;
					Main.projectile[sandBall].timeLeft = 300;
					Main.projectile[sandBall].friendly = false;
					NetMessage.SendData(27, -1, -1, null, sandBall);
				}
				base.NPC.netUpdate = true;
			}
			try
			{
				int xLeft = (int)base.NPC.position.X / 16;
				int xCenter = (int)base.NPC.Center.X / 16;
				int xRight = (int)(base.NPC.position.X + (float)base.NPC.width) / 16;
				int y = (int)(base.NPC.position.Y + (float)base.NPC.height) / 16;
				bool tileClimbing = false;
				if ((Main.tile[xLeft, y].HasUnactuatedTile && Main.tileSolid[Main.tile[xLeft, y].TileType]) || (Main.tile[xCenter, y].HasUnactuatedTile && Main.tileSolid[Main.tile[xCenter, y].TileType]) || (Main.tile[xRight, y].HasUnactuatedTile && Main.tileSolid[Main.tile[xRight, y].TileType]))
				{
					tileClimbing = true;
				}
				if (tileClimbing)
				{
					base.NPC.noGravity = true;
					base.NPC.noTileCollide = true;
					base.NPC.velocity.Y = -0.2f;
				}
				else
				{
					base.NPC.noGravity = false;
					base.NPC.noTileCollide = false;
					if (Main.rand.NextBool())
					{
						int sand = Dust.NewDust(new Vector2(base.NPC.position.X - 4f, base.NPC.position.Y + (float)base.NPC.height - 8f), base.NPC.width + 8, 24, 32, 0f, base.NPC.velocity.Y / 2f);
						Dust dust2 = Main.dust[sand];
						dust2.velocity.X *= 0.4f;
						dust2.velocity.Y *= -1f;
						if (Main.rand.NextBool())
						{
							dust2.noGravity = true;
							dust2.scale += 0.2f;
						}
					}
				}
			}
			catch
			{
			}
			return false;
		}
	}

	public class BatAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0114: Unknown result type (might be due to invalid IL or missing references)
			//IL_0139: Unknown result type (might be due to invalid IL or missing references)
			//IL_0143: Unknown result type (might be due to invalid IL or missing references)
			//IL_0148: Unknown result type (might be due to invalid IL or missing references)
			//IL_035c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0388: Unknown result type (might be due to invalid IL or missing references)
			//IL_0922: Unknown result type (might be due to invalid IL or missing references)
			//IL_093c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0957: Unknown result type (might be due to invalid IL or missing references)
			//IL_0969: Unknown result type (might be due to invalid IL or missing references)
			//IL_0995: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a66: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a92: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d2c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d40: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d45: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d5e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d64: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d78: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d82: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d87: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dcc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0df7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dfd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e11: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e1b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e20: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e56: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e81: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e87: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e9b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ea5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0eaa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ef4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f20: Unknown result type (might be due to invalid IL or missing references)
			//IL_117f: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_13f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_141c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fe9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1015: Unknown result type (might be due to invalid IL or missing references)
			//IL_1274: Unknown result type (might be due to invalid IL or missing references)
			//IL_12a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_14e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1511: Unknown result type (might be due to invalid IL or missing references)
			//IL_106f: Unknown result type (might be due to invalid IL or missing references)
			//IL_107e: Unknown result type (might be due to invalid IL or missing references)
			//IL_12fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_1309: Unknown result type (might be due to invalid IL or missing references)
			//IL_1552: Unknown result type (might be due to invalid IL or missing references)
			//IL_1557: Unknown result type (might be due to invalid IL or missing references)
			//IL_1096: Unknown result type (might be due to invalid IL or missing references)
			//IL_109b: Unknown result type (might be due to invalid IL or missing references)
			//IL_10af: Unknown result type (might be due to invalid IL or missing references)
			//IL_10b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1321: Unknown result type (might be due to invalid IL or missing references)
			//IL_1326: Unknown result type (might be due to invalid IL or missing references)
			//IL_133a: Unknown result type (might be due to invalid IL or missing references)
			//IL_133f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1585: Unknown result type (might be due to invalid IL or missing references)
			//IL_1594: Unknown result type (might be due to invalid IL or missing references)
			//IL_159b: Unknown result type (might be due to invalid IL or missing references)
			//IL_15a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_15a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_15aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_15b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_15b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_15be: Unknown result type (might be due to invalid IL or missing references)
			//IL_15d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_15d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_15dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_15e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_15e7: Unknown result type (might be due to invalid IL or missing references)
			if (base.NPC.type == 60 || base.NPC.type == 151)
			{
				int lavaDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, base.NPC.velocity.X * 0.2f, base.NPC.velocity.Y * 0.2f, 100, default(Color), 2f);
				Main.dust[lavaDust].noGravity = true;
			}
			if (base.NPC.type == 150 && Main.rand.NextBool(10))
			{
				int iceDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 67, base.NPC.velocity.X * 0.5f, base.NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
				Main.dust[iceDust].noGravity = true;
				Dust obj = Main.dust[iceDust];
				obj.velocity *= 0.2f;
				Main.dust[iceDust].noLight = true;
			}
			base.NPC.noGravity = true;
			if (base.NPC.collideX)
			{
				base.NPC.velocity.X = base.NPC.oldVelocity.X * -0.5f;
				if (base.NPC.direction == -1 && base.NPC.velocity.X > 0f && base.NPC.velocity.X < 2f)
				{
					base.NPC.velocity.X = 2f;
				}
				if (base.NPC.direction == 1 && base.NPC.velocity.X < 0f && base.NPC.velocity.X > -2f)
				{
					base.NPC.velocity.X = -2f;
				}
			}
			if (base.NPC.collideY)
			{
				base.NPC.velocity.Y = base.NPC.oldVelocity.Y * -0.5f;
				if (base.NPC.velocity.Y > 0f && base.NPC.velocity.Y < 1f)
				{
					base.NPC.velocity.Y = 1f;
				}
				if (base.NPC.velocity.Y < 0f && base.NPC.velocity.Y > -1f)
				{
					base.NPC.velocity.Y = -1f;
				}
			}
			if (base.NPC.type == 226)
			{
				int direction = 1;
				int directionY = 1;
				if (base.NPC.velocity.X < 0f)
				{
					direction = -1;
				}
				if (base.NPC.velocity.Y < 0f)
				{
					directionY = -1;
				}
				base.NPC.TargetClosest();
				if (!Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					base.NPC.direction = direction;
					base.NPC.directionY = directionY;
				}
			}
			else
			{
				base.NPC.TargetClosest();
			}
			float maxSpeedX = (CalamityWorld.death ? 6f : (CalamityWorld.revenge ? 5f : 4f));
			float maxSpeedY = (CalamityWorld.death ? 2.5f : (CalamityWorld.revenge ? 2f : 1.5f));
			float xAccel = (CalamityWorld.revenge ? 0.12f : 0.1f);
			float xAccelBoost1 = (CalamityWorld.revenge ? 0.12f : 0.1f);
			float xAccelBoost2 = (CalamityWorld.revenge ? 0.06f : 0.04f);
			float yAccel = (CalamityWorld.revenge ? 0.06f : 0.04f);
			float yAccelBoost1 = (CalamityWorld.revenge ? 0.07f : 0.05f);
			float yAccelBoost2 = (CalamityWorld.revenge ? 0.05f : 0.03f);
			if (base.NPC.type == 158)
			{
				if ((double)base.NPC.position.Y < Main.worldSurface * 16.0 && Main.dayTime && !Main.eclipse)
				{
					base.NPC.directionY = -1;
					base.NPC.direction *= -1;
				}
				maxSpeedX = (maxSpeedY = (CalamityWorld.death ? 11f : 9f));
				xAccel = (yAccel = 0.3f);
				xAccelBoost1 = (yAccelBoost1 = 0.12f);
				xAccelBoost2 = (yAccelBoost2 = 0.07f);
			}
			else if (base.NPC.type == 226)
			{
				maxSpeedX = (CalamityWorld.death ? 9f : 6f);
				maxSpeedY = (CalamityWorld.death ? 5f : 3.5f);
				xAccel = 0.3f;
				xAccelBoost1 = 0.12f;
				xAccelBoost2 = 0.07f;
				yAccel = 0.12f;
				yAccelBoost1 = 0.07f;
				yAccelBoost2 = 0.05f;
			}
			DemonEyeAI.DemonEyeBatMovement(base.NPC, maxSpeedX, maxSpeedY, xAccel, xAccelBoost1, xAccelBoost2, yAccel, yAccelBoost1, yAccelBoost2);
			if (base.NPC.type == 49 || base.NPC.type == 51 || base.NPC.type == 60 || base.NPC.type == 62 || base.NPC.type == 66 || base.NPC.type == 93 || base.NPC.type == 137 || base.NPC.type == 150 || base.NPC.type == 151 || base.NPC.type == 152 || base.NPC.type == ModContent.NPCType<Melter>())
			{
				maxSpeedX = (CalamityWorld.death ? 6f : (CalamityWorld.revenge ? 5f : 4f));
				maxSpeedY = (CalamityWorld.death ? 2.5f : (CalamityWorld.revenge ? 2f : 1.5f));
				if (base.NPC.wet)
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y *= 0.95f;
					}
					base.NPC.velocity.Y -= (CalamityWorld.revenge ? 0.6f : 0.5f);
					if (base.NPC.velocity.Y < -5f)
					{
						base.NPC.velocity.Y = -5f;
					}
					base.NPC.TargetClosest();
				}
				if (base.NPC.type == 60)
				{
					xAccel = 0.12f;
					xAccelBoost1 = 0.09f;
					xAccelBoost2 = 0.05f;
					yAccel = 0.06f;
					yAccelBoost1 = 0.05f;
					yAccelBoost2 = 0.03f;
				}
				else
				{
					xAccel = (CalamityWorld.revenge ? 0.12f : 0.1f);
					xAccelBoost1 = (CalamityWorld.revenge ? 0.12f : 0.1f);
					xAccelBoost2 = (CalamityWorld.revenge ? 0.07f : 0.05f);
					yAccel = (CalamityWorld.revenge ? 0.06f : 0.04f);
					yAccelBoost1 = (CalamityWorld.revenge ? 0.07f : 0.05f);
					yAccelBoost2 = (CalamityWorld.revenge ? 0.05f : 0.03f);
				}
				DemonEyeAI.DemonEyeBatMovement(base.NPC, maxSpeedX, maxSpeedY, xAccel, xAccelBoost1, xAccelBoost2, yAccel, yAccelBoost1, yAccelBoost2);
			}
			if (base.NPC.type == 48 && base.NPC.wet)
			{
				base.NPC.ai[0] = 0f;
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y *= 0.95f;
				}
				base.NPC.velocity.Y -= (CalamityWorld.revenge ? 0.6f : 0.5f);
				if (base.NPC.velocity.Y < -5f)
				{
					base.NPC.velocity.Y = -5f;
				}
				base.NPC.TargetClosest();
			}
			if (base.NPC.type == 158 && Main.netMode != 1 && base.NPC.Distance(Main.player[base.NPC.target].Center) < 200f && base.NPC.Center.Y < Main.player[base.NPC.target].Center.Y && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
			{
				base.NPC.Transform(159);
			}
			base.NPC.ai[1] += (CalamityWorld.revenge ? 2f : 1f);
			if (base.NPC.type == 158)
			{
				base.NPC.ai[1]++;
			}
			if (base.NPC.ai[1] > 200f)
			{
				if (!Main.player[base.NPC.target].wet && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					base.NPC.ai[1] = 0f;
				}
				xAccel = (CalamityWorld.revenge ? 0.25f : 0.2f);
				yAccel = (CalamityWorld.revenge ? 0.12f : 0.1f);
				float maxVelocityX = (CalamityWorld.revenge ? 4.6f : 4f);
				float maxVelocityY = (CalamityWorld.revenge ? 1.8f : 1.5f);
				if (base.NPC.type == 48 || base.NPC.type == 62 || base.NPC.type == 66)
				{
					xAccel = (CalamityWorld.revenge ? 0.15f : 0.12f);
					yAccel = (CalamityWorld.revenge ? 0.1f : 0.07f);
					maxVelocityX = (CalamityWorld.revenge ? 3.5f : 3f);
					maxVelocityY = (CalamityWorld.revenge ? 1.5f : 1.25f);
				}
				if (base.NPC.ai[1] > 1000f)
				{
					base.NPC.ai[1] = 0f;
				}
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] > 0f)
				{
					if (base.NPC.velocity.Y < maxVelocityY)
					{
						base.NPC.velocity.Y += yAccel;
					}
				}
				else if (base.NPC.velocity.Y > 0f - maxVelocityY)
				{
					base.NPC.velocity.Y -= yAccel;
				}
				if (base.NPC.ai[2] < -150f || base.NPC.ai[2] > 150f)
				{
					if (base.NPC.velocity.X < maxVelocityX)
					{
						base.NPC.velocity.X += xAccel;
					}
				}
				else if (base.NPC.velocity.X > 0f - maxVelocityX)
				{
					base.NPC.velocity.X -= xAccel;
				}
				if (base.NPC.ai[2] > 300f)
				{
					base.NPC.ai[2] = -300f;
				}
			}
			if (base.NPC.type == 48)
			{
				if (base.NPC.ai[0] > 570f)
				{
					Dust dust = Dust.NewDustDirect(base.NPC.Center + Main.rand.NextVector2CircularEdge(5f, 5f), 1, 1, 172, 0f, 0f, 0, default(Color), 1.5f);
					dust.noGravity = true;
					dust.velocity *= 0f;
				}
			}
			else if (base.NPC.type == 62 || base.NPC.type == 66)
			{
				if (base.NPC.ai[0] > 420f)
				{
					Dust dust2 = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 27, 0f, 0f, 100, default(Color), 3f);
					dust2.noGravity = true;
					dust2.velocity *= 0f;
				}
			}
			else if (base.NPC.type == 156 && base.NPC.ai[0] > 345f)
			{
				Dust dust3 = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 27, 0f, 0f, 100, default(Color), 3f);
				dust3.noGravity = true;
				dust3.velocity *= 0f;
			}
			if (Main.netMode != 1)
			{
				if (base.NPC.type == 48)
				{
					float featherShootCutOffValue = (CalamityWorld.revenge ? 90f : 60f);
					if (base.NPC.justHit || !Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						base.NPC.ai[0] = featherShootCutOffValue + 1f;
					}
					if (base.NPC.ai[0] >= 600f)
					{
						base.NPC.ai[0] = 0f;
					}
					else if (base.NPC.ai[0] % 30f == 0f && base.NPC.ai[0] <= featherShootCutOffValue)
					{
						base.NPC.ai[0]++;
						if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
						{
							int damage = 15;
							int type = 38;
							Vector2 featherVelocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * (CalamityWorld.death ? 4f : 6f);
							int feather = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, featherVelocity, type, damage, 0f, Main.myPlayer);
							Main.projectile[feather].timeLeft = 300;
							if (CalamityWorld.death)
							{
								Main.projectile[feather].extraUpdates++;
								Main.projectile[feather].timeLeft = 600;
							}
						}
					}
					else
					{
						base.NPC.ai[0]++;
					}
				}
				if (base.NPC.type == 62 || base.NPC.type == 66)
				{
					float scytheShootCutOffValue = (CalamityWorld.revenge ? 80f : 60f);
					if (base.NPC.justHit || !Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						base.NPC.ai[0] = scytheShootCutOffValue + 1f;
					}
					if (base.NPC.ai[0] >= 450f)
					{
						base.NPC.ai[0] = 0f;
					}
					else if (base.NPC.ai[0] % 20f == 0f && base.NPC.ai[0] <= scytheShootCutOffValue)
					{
						base.NPC.ai[0]++;
						if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
						{
							int damage2 = 21;
							int type2 = 44;
							Vector2 sickleVelocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * (CalamityWorld.death ? 0.15f : 0.2f);
							int sickle = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, sickleVelocity, type2, damage2, 0f, Main.myPlayer);
							Main.projectile[sickle].timeLeft = 300;
							if (CalamityWorld.death)
							{
								Main.projectile[sickle].extraUpdates++;
								Main.projectile[sickle].timeLeft = 600;
							}
						}
					}
					else
					{
						base.NPC.ai[0]++;
					}
				}
				if (base.NPC.type == 156)
				{
					float tridentShootCutOffValue = 80f;
					if (base.NPC.justHit || !Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						base.NPC.ai[0] = tridentShootCutOffValue + 1f;
					}
					if (base.NPC.ai[0] >= 375f)
					{
						base.NPC.ai[0] = 0f;
					}
					else if (base.NPC.ai[0] % 20f == 0f && base.NPC.ai[0] <= tridentShootCutOffValue)
					{
						base.NPC.ai[0]++;
						if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
						{
							Vector2 spawnPosition = base.NPC.Center;
							float tridentSpeed = (CalamityWorld.death ? 0.15f : 0.2f);
							Vector2 tridentVelocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * tridentSpeed;
							spawnPosition += base.NPC.velocity * 5f;
							int damage3 = 80;
							int type3 = 115;
							int trident = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPosition + tridentVelocity * 100f, tridentVelocity, type3, damage3, 3f, Main.myPlayer);
							Main.projectile[trident].timeLeft = 300;
							if (CalamityWorld.death)
							{
								Main.projectile[trident].extraUpdates++;
								Main.projectile[trident].timeLeft = 600;
							}
						}
					}
					else
					{
						base.NPC.ai[0]++;
					}
				}
			}
			return false;
		}
	}

	public class BigMimicAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_013f: Unknown result type (might be due to invalid IL or missing references)
			//IL_014a: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0154: Unknown result type (might be due to invalid IL or missing references)
			//IL_028e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0299: Unknown result type (might be due to invalid IL or missing references)
			//IL_029e: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b40: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b58: Unknown result type (might be due to invalid IL or missing references)
			//IL_08ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_08cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b6c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a01: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a1c: Unknown result type (might be due to invalid IL or missing references)
			//IL_091a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0925: Unknown result type (might be due to invalid IL or missing references)
			//IL_092a: Unknown result type (might be due to invalid IL or missing references)
			//IL_092f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0938: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e0a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e15: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e1a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e1f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aad: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a39: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a54: Unknown result type (might be due to invalid IL or missing references)
			//IL_1028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c8a: Unknown result type (might be due to invalid IL or missing references)
			//IL_094d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0952: Unknown result type (might be due to invalid IL or missing references)
			//IL_0960: Unknown result type (might be due to invalid IL or missing references)
			//IL_096a: Unknown result type (might be due to invalid IL or missing references)
			//IL_096f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0971: Unknown result type (might be due to invalid IL or missing references)
			//IL_097b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0980: Unknown result type (might be due to invalid IL or missing references)
			//IL_1086: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f03: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f0d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f12: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f14: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f1e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f23: Unknown result type (might be due to invalid IL or missing references)
			//IL_0edb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e50: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ad5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0adf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af5: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0409: Unknown result type (might be due to invalid IL or missing references)
			//IL_10ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_114e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ef0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ef5: Unknown result type (might be due to invalid IL or missing references)
			//IL_11b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1216: Unknown result type (might be due to invalid IL or missing references)
			//IL_164a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1659: Unknown result type (might be due to invalid IL or missing references)
			//IL_165e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1663: Unknown result type (might be due to invalid IL or missing references)
			//IL_1669: Unknown result type (might be due to invalid IL or missing references)
			//IL_1678: Unknown result type (might be due to invalid IL or missing references)
			//IL_1247: Unknown result type (might be due to invalid IL or missing references)
			//IL_125f: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.knockBackResist = 0.2f * Main.GameModeInfo.KnockbackToEnemiesMultiplier;
			base.NPC.dontTakeDamage = false;
			base.NPC.noTileCollide = false;
			base.NPC.noGravity = false;
			base.NPC.reflectsProjectiles = false;
			if (base.NPC.ai[0] != 7f && Main.player[base.NPC.target].dead)
			{
				base.NPC.TargetClosest();
				if (Main.player[base.NPC.target].dead)
				{
					base.NPC.ai[0] = 7f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.TargetClosest();
				Vector2 mimicTargetDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
				if (Main.netMode != 1 && (base.NPC.velocity.X != 0f || base.NPC.velocity.Y > 100f || base.NPC.justHit || ((Vector2)(ref mimicTargetDirection)).Length() < 80f))
				{
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[0] == 1f)
			{
				base.NPC.ai[1]++;
				if (Main.netMode != 1 && base.NPC.ai[1] > 36f)
				{
					base.NPC.ai[0] = 2f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[0] == 2f)
			{
				Vector2 mimicTargetDirection2 = Main.player[base.NPC.target].Center - base.NPC.Center;
				if (Main.netMode != 1 && ((Vector2)(ref mimicTargetDirection2)).Length() > 600f)
				{
					base.NPC.ai[0] = 5f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
				if (base.NPC.velocity.Y == 0f)
				{
					base.NPC.TargetClosest();
					base.NPC.velocity.X *= 0.85f;
					base.NPC.ai[1]++;
					float jumpDelay = 10f + (CalamityWorld.death ? 10f : 20f) * ((float)base.NPC.life / (float)base.NPC.lifeMax);
					float jumpXVelocity = 5f + (CalamityWorld.death ? 7f : 5f) * (1f - (float)base.NPC.life / (float)base.NPC.lifeMax);
					float jumpYVelocity = (CalamityWorld.death ? 7f : 5f);
					if (!Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
					{
						jumpYVelocity += 2f;
					}
					if (Main.netMode != 1 && base.NPC.ai[1] > jumpDelay)
					{
						base.NPC.ai[3]++;
						if (base.NPC.ai[3] >= 3f)
						{
							base.NPC.ai[3] = 0f;
							jumpYVelocity *= 2f;
							jumpXVelocity /= 2f;
						}
						base.NPC.ai[1] = 0f;
						base.NPC.velocity.Y -= jumpYVelocity;
						base.NPC.velocity.X = jumpXVelocity * (float)base.NPC.direction;
						base.NPC.netUpdate = true;
					}
				}
				else
				{
					base.NPC.knockBackResist = 0f;
					base.NPC.velocity.X *= 0.99f;
					if (base.NPC.direction < 0 && base.NPC.velocity.X > -1f)
					{
						base.NPC.velocity.X = -1f;
					}
					if (base.NPC.direction > 0 && base.NPC.velocity.X < 1f)
					{
						base.NPC.velocity.X = 1f;
					}
				}
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] > (CalamityWorld.death ? 130f : 170f) && base.NPC.velocity.Y == 0f && Main.netMode != 1)
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						base.NPC.ai[0] = 3f;
						break;
					case 1:
						base.NPC.ai[0] = 4f;
						base.NPC.noTileCollide = true;
						base.NPC.velocity.Y = (CalamityWorld.death ? (-12f) : (-10f));
						break;
					case 2:
						base.NPC.ai[0] = 6f;
						break;
					default:
						base.NPC.ai[0] = 2f;
						break;
					}
					if (Main.tenthAnniversaryWorld && base.NPC.type == 476 && base.NPC.ai[0] == 3f && Main.rand.NextBool())
					{
						base.NPC.ai[0] = 8f;
					}
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[0] == 3f)
			{
				base.NPC.damage = 0;
				base.NPC.velocity.X *= 0.85f;
				base.NPC.dontTakeDamage = true;
				base.NPC.ai[1]++;
				if (Main.netMode != 1 && base.NPC.ai[1] >= (CalamityWorld.death ? 60f : 90f))
				{
					base.NPC.ai[0] = 2f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
				if (Main.expertMode)
				{
					base.NPC.ReflectProjectiles(base.NPC.Hitbox);
					base.NPC.reflectsProjectiles = true;
				}
			}
			else if (base.NPC.ai[0] == 4f)
			{
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				base.NPC.knockBackResist = 0f;
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.direction = -1;
				}
				else
				{
					base.NPC.direction = 1;
				}
				base.NPC.spriteDirection = base.NPC.direction;
				base.NPC.TargetClosest();
				Vector2 mimicTargetCenter = Main.player[base.NPC.target].Center;
				mimicTargetCenter.Y -= 350f;
				Vector2 mimicTargetDirection3 = mimicTargetCenter - base.NPC.Center;
				if (base.NPC.ai[2] == 1f)
				{
					base.NPC.ai[1]++;
					mimicTargetDirection3 = Main.player[base.NPC.target].Center - base.NPC.Center;
					((Vector2)(ref mimicTargetDirection3)).Normalize();
					mimicTargetDirection3 *= (CalamityWorld.death ? 12f : 10f);
					base.NPC.velocity = (base.NPC.velocity * 4f + mimicTargetDirection3) / 5f;
					if (Main.netMode != 1 && base.NPC.ai[1] > 6f)
					{
						base.NPC.ai[1] = 0f;
						base.NPC.ai[0] = 4.1f;
						base.NPC.ai[2] = 0f;
						base.NPC.velocity = mimicTargetDirection3;
						base.NPC.netUpdate = true;
					}
				}
				else if (Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) < 40f && base.NPC.Center.Y < Main.player[base.NPC.target].Center.Y - 300f)
				{
					if (Main.netMode != 1)
					{
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 1f;
						base.NPC.netUpdate = true;
					}
				}
				else
				{
					((Vector2)(ref mimicTargetDirection3)).Normalize();
					mimicTargetDirection3 *= (CalamityWorld.death ? 16f : 14f);
					base.NPC.velocity = (base.NPC.velocity * 5f + mimicTargetDirection3) / 6f;
				}
			}
			else if (base.NPC.ai[0] == 4.1f)
			{
				base.NPC.knockBackResist = 0f;
				if (base.NPC.ai[2] == 0f && Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1) && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.ai[2] = 1f;
				}
				if (base.NPC.position.Y + (float)base.NPC.height >= Main.player[base.NPC.target].position.Y || base.NPC.velocity.Y <= 0f)
				{
					base.NPC.ai[1]++;
					if (Main.netMode != 1 && base.NPC.ai[1] > 10f)
					{
						base.NPC.ai[0] = 2f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						base.NPC.ai[3] = 0f;
						base.NPC.netUpdate = true;
						if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
						{
							base.NPC.ai[0] = 5f;
						}
					}
				}
				else if (base.NPC.ai[2] == 0f)
				{
					base.NPC.noTileCollide = true;
					base.NPC.noGravity = true;
					base.NPC.knockBackResist = 0f;
				}
				base.NPC.velocity.Y += (CalamityWorld.death ? 0.3f : 0.25f);
				if (base.NPC.velocity.Y > (CalamityWorld.death ? 24f : 20f))
				{
					base.NPC.velocity.Y = (CalamityWorld.death ? 24f : 20f);
				}
			}
			else if (base.NPC.ai[0] == 5f)
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.direction = 1;
				}
				else
				{
					base.NPC.direction = -1;
				}
				base.NPC.spriteDirection = base.NPC.direction;
				base.NPC.noTileCollide = true;
				base.NPC.noGravity = true;
				base.NPC.knockBackResist = 0f;
				Vector2 chaseTargetDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
				chaseTargetDirection.Y -= 4f;
				if (Main.netMode != 1 && ((Vector2)(ref chaseTargetDirection)).Length() < 200f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.ai[0] = 2f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
				if (((Vector2)(ref chaseTargetDirection)).Length() > 10f)
				{
					((Vector2)(ref chaseTargetDirection)).Normalize();
					chaseTargetDirection *= (CalamityWorld.death ? 15f : 12.5f);
				}
				base.NPC.velocity = (base.NPC.velocity * 4f + chaseTargetDirection) / 5f;
			}
			else if (base.NPC.ai[0] == 6f)
			{
				base.NPC.knockBackResist = 0f;
				if (base.NPC.velocity.Y == 0f)
				{
					base.NPC.TargetClosest();
					base.NPC.velocity.X *= 0.8f;
					base.NPC.ai[1]++;
					if (base.NPC.ai[1] > 5f)
					{
						base.NPC.ai[1] = 0f;
						base.NPC.velocity.Y -= 4f;
						if (Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height < base.NPC.Center.Y)
						{
							base.NPC.velocity.Y -= 1.25f;
						}
						if (Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height < base.NPC.Center.Y - 40f)
						{
							base.NPC.velocity.Y -= 1.5f;
						}
						if (Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height < base.NPC.Center.Y - 80f)
						{
							base.NPC.velocity.Y -= 1.75f;
						}
						if (Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height < base.NPC.Center.Y - 120f)
						{
							base.NPC.velocity.Y -= 2f;
						}
						if (Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height < base.NPC.Center.Y - 160f)
						{
							base.NPC.velocity.Y -= 2.25f;
						}
						if (Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height < base.NPC.Center.Y - 200f)
						{
							base.NPC.velocity.Y -= 2.5f;
						}
						if (!Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
						{
							base.NPC.velocity.Y -= 2f;
						}
						base.NPC.velocity.X = (CalamityWorld.death ? 16 : 14) * base.NPC.direction;
						base.NPC.ai[2]++;
						base.NPC.netUpdate = true;
					}
				}
				else
				{
					base.NPC.velocity.X *= 0.98f;
					if (base.NPC.direction < 0 && base.NPC.velocity.X > -8f)
					{
						base.NPC.velocity.X = -8f;
					}
					if (base.NPC.direction > 0 && base.NPC.velocity.X < 8f)
					{
						base.NPC.velocity.X = 8f;
					}
				}
				if (Main.netMode != 1 && base.NPC.ai[2] >= 3f && base.NPC.velocity.Y == 0f)
				{
					base.NPC.ai[0] = 2f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[0] == 7f)
			{
				base.NPC.damage = 0;
				base.NPC.life = base.NPC.lifeMax;
				base.NPC.defense = 9999;
				base.NPC.noTileCollide = true;
				base.NPC.alpha += 7;
				if (base.NPC.alpha > 255)
				{
					base.NPC.alpha = 255;
				}
				base.NPC.velocity.X *= 0.98f;
			}
			else
			{
				if (base.NPC.ai[0] != 8f)
				{
					return false;
				}
				base.NPC.velocity.X *= 0.85f;
				base.NPC.ai[1]++;
				if (Main.netMode != 1)
				{
					if (!Main.tenthAnniversaryWorld || base.NPC.ai[1] >= 180f)
					{
						base.NPC.ai[0] = 2f;
						base.NPC.ai[1] = 0f;
						base.NPC.netUpdate = true;
					}
					else if (base.NPC.ai[1] % 20f == 0f)
					{
						int num = 10;
						Vector2 vector = default(Vector2);
						for (int i = 0; i < num; i++)
						{
							int itemID = ItemID.Sets.ItemsForStuffCannon[Main.rand.Next(ItemID.Sets.ItemsForStuffCannon.Length)];
							int item = Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, itemID, 1, noBroadcast: false, -1, noGrabDelay: true);
							float num2 = Main.rand.Next(10, 26);
							((Vector2)(ref vector))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
							Vector2 val = Main.player[base.NPC.target].Center - new Vector2(0f, 120f);
							float targetXDist = val.X - vector.X;
							float targetYDist = val.Y - vector.Y;
							targetXDist += (float)Main.rand.Next(-50, 51) * 0.1f;
							targetYDist += (float)Main.rand.Next(-50, 51) * 0.1f;
							float targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
							targetDistance = num2 / targetDistance;
							targetXDist *= targetDistance;
							targetYDist *= targetDistance;
							targetXDist += (float)Main.rand.Next(-50, 51) * 0.1f;
							targetYDist += (float)Main.rand.Next(-50, 51) * 0.1f;
							Main.item[item].velocity.X = targetXDist;
							Main.item[item].velocity.Y = targetYDist;
							Main.item[item].noGrabDelay = 100;
							if (Main.netMode != 0)
							{
								NetMessage.SendData(21, -1, -1, null, item);
							}
						}
					}
				}
			}
			return false;
		}
	}

	public class BlazingWheelAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0305: Unknown result type (might be due to invalid IL or missing references)
			//IL_0319: Unknown result type (might be due to invalid IL or missing references)
			//IL_031e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0333: Unknown result type (might be due to invalid IL or missing references)
			//IL_0339: Unknown result type (might be due to invalid IL or missing references)
			//IL_034c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0356: Unknown result type (might be due to invalid IL or missing references)
			//IL_035b: Unknown result type (might be due to invalid IL or missing references)
			//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0403: Unknown result type (might be due to invalid IL or missing references)
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.TargetClosest();
				base.NPC.directionY = 1;
				base.NPC.ai[0] = 1f;
			}
			int wheelVelocity = (CalamityWorld.death ? 9 : 6);
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.rotation += (float)(base.NPC.direction * base.NPC.directionY) * 0.13f;
				if (base.NPC.collideY)
				{
					base.NPC.ai[0] = 2f;
				}
				if (!base.NPC.collideY && base.NPC.ai[0] == 2f)
				{
					base.NPC.direction = -base.NPC.direction;
					base.NPC.ai[1] = 1f;
					base.NPC.ai[0] = 1f;
				}
				if (base.NPC.collideX)
				{
					base.NPC.directionY = -base.NPC.directionY;
					base.NPC.ai[1] = 1f;
				}
			}
			else
			{
				base.NPC.rotation -= (float)(base.NPC.direction * base.NPC.directionY) * 0.13f;
				if (base.NPC.collideX)
				{
					base.NPC.ai[0] = 2f;
				}
				if (!base.NPC.collideX && base.NPC.ai[0] == 2f)
				{
					base.NPC.directionY = -base.NPC.directionY;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[0] = 1f;
				}
				if (base.NPC.collideY)
				{
					base.NPC.direction = -base.NPC.direction;
					base.NPC.ai[1] = 0f;
				}
			}
			base.NPC.velocity.X = wheelVelocity * base.NPC.direction;
			base.NPC.velocity.Y = wheelVelocity * base.NPC.directionY;
			float lighting = (float)(270 - Main.mouseTextColor) / 400f;
			Lighting.AddLight((int)(base.NPC.position.X + (float)(base.NPC.width / 2)) / 16, (int)(base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16, 0.9f, 0.3f + lighting, 0.2f);
			if (base.NPC.localAI[0] > (CalamityWorld.death ? 90f : 120f) - 30f)
			{
				Dust dust = Dust.NewDustDirect(base.NPC.Center + Main.rand.NextVector2CircularEdge(5f, 5f), 1, 1, 6, 0f, 0f, 0, default(Color), 3f);
				dust.noGravity = true;
				dust.velocity *= 0f;
			}
			if (Main.netMode != 1)
			{
				base.NPC.localAI[0]++;
				if (base.NPC.localAI[0] >= (CalamityWorld.death ? 90f : 120f))
				{
					base.NPC.localAI[0] = 0f;
					for (int i = 0; i < 4; i++)
					{
						Vector2 vector255 = Utils.RotatedBy(new Vector2(0f, -5f), (double)((float)Math.PI / 2f * (float)i), default(Vector2));
						int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, vector255, 188, 20, 0f, Main.myPlayer);
						Main.projectile[proj].tileCollide = false;
						Main.projectile[proj].friendly = false;
						Main.projectile[proj].trap = false;
					}
				}
			}
			return false;
		}
	}

	public class CasterAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0143: Unknown result type (might be due to invalid IL or missing references)
			//IL_014e: Unknown result type (might be due to invalid IL or missing references)
			//IL_06a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0182: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01de: Unknown result type (might be due to invalid IL or missing references)
			//IL_0220: Unknown result type (might be due to invalid IL or missing references)
			//IL_024e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0254: Unknown result type (might be due to invalid IL or missing references)
			//IL_0268: Unknown result type (might be due to invalid IL or missing references)
			//IL_0272: Unknown result type (might be due to invalid IL or missing references)
			//IL_0277: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0711: Unknown result type (might be due to invalid IL or missing references)
			//IL_0717: Unknown result type (might be due to invalid IL or missing references)
			//IL_0735: Unknown result type (might be due to invalid IL or missing references)
			//IL_073f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0744: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0301: Unknown result type (might be due to invalid IL or missing references)
			//IL_030b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0310: Unknown result type (might be due to invalid IL or missing references)
			//IL_0788: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_07bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_07dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0356: Unknown result type (might be due to invalid IL or missing references)
			//IL_0384: Unknown result type (might be due to invalid IL or missing references)
			//IL_038a: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_03af: Unknown result type (might be due to invalid IL or missing references)
			//IL_0811: Unknown result type (might be due to invalid IL or missing references)
			//IL_083c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0842: Unknown result type (might be due to invalid IL or missing references)
			//IL_0858: Unknown result type (might be due to invalid IL or missing references)
			//IL_0862: Unknown result type (might be due to invalid IL or missing references)
			//IL_0867: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_08f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_08fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0901: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_041f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0425: Unknown result type (might be due to invalid IL or missing references)
			//IL_043b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0445: Unknown result type (might be due to invalid IL or missing references)
			//IL_044a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0947: Unknown result type (might be due to invalid IL or missing references)
			//IL_0975: Unknown result type (might be due to invalid IL or missing references)
			//IL_097b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0991: Unknown result type (might be due to invalid IL or missing references)
			//IL_099b: Unknown result type (might be due to invalid IL or missing references)
			//IL_09a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a30: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a7d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a83: Unknown result type (might be due to invalid IL or missing references)
			//IL_1071: Unknown result type (might be due to invalid IL or missing references)
			//IL_1076: Unknown result type (might be due to invalid IL or missing references)
			//IL_047a: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1162: Unknown result type (might be due to invalid IL or missing references)
			//IL_116d: Unknown result type (might be due to invalid IL or missing references)
			//IL_10af: Unknown result type (might be due to invalid IL or missing references)
			//IL_10c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0571: Unknown result type (might be due to invalid IL or missing references)
			//IL_059b: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0500: Unknown result type (might be due to invalid IL or missing references)
			//IL_052b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0531: Unknown result type (might be due to invalid IL or missing references)
			//IL_0547: Unknown result type (might be due to invalid IL or missing references)
			//IL_0551: Unknown result type (might be due to invalid IL or missing references)
			//IL_0556: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a10: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a2c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a36: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a3b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b0f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b5f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b65: Unknown result type (might be due to invalid IL or missing references)
			//IL_16ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_16d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1184: Unknown result type (might be due to invalid IL or missing references)
			//IL_1189: Unknown result type (might be due to invalid IL or missing references)
			//IL_118e: Unknown result type (might be due to invalid IL or missing references)
			//IL_11a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_11a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_11be: Unknown result type (might be due to invalid IL or missing references)
			//IL_11c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_11d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0adc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b06: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b0c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b22: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b2c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b31: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a6b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a96: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a9c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0abc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d91: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dbe: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dc4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1214: Unknown result type (might be due to invalid IL or missing references)
			//IL_121e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1236: Unknown result type (might be due to invalid IL or missing references)
			//IL_1240: Unknown result type (might be due to invalid IL or missing references)
			//IL_1255: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e61: Unknown result type (might be due to invalid IL or missing references)
			//IL_1eb1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1eb7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ed5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1edf: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ee4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c41: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c8e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c94: Unknown result type (might be due to invalid IL or missing references)
			//IL_1290: Unknown result type (might be due to invalid IL or missing references)
			//IL_1263: Unknown result type (might be due to invalid IL or missing references)
			//IL_129e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1271: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f5a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1faa: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fb0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fce: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fd8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fdd: Unknown result type (might be due to invalid IL or missing references)
			//IL_12ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_127f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2009: Unknown result type (might be due to invalid IL or missing references)
			//IL_188b: Unknown result type (might be due to invalid IL or missing references)
			//IL_18ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_190b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1912: Unknown result type (might be due to invalid IL or missing references)
			//IL_14f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_153b: Unknown result type (might be due to invalid IL or missing references)
			//IL_12ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_2059: Unknown result type (might be due to invalid IL or missing references)
			//IL_20a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_20ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_15e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_15ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_1653: Unknown result type (might be due to invalid IL or missing references)
			//IL_167d: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.TargetClosest();
			base.NPC.velocity.X *= 0.93f;
			if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
			{
				base.NPC.velocity.X = 0f;
			}
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.ai[0] = 500f;
			}
			if (base.NPC.type == 172)
			{
				if (base.NPC.alpha < 255)
				{
					base.NPC.alpha++;
				}
				if (base.NPC.justHit)
				{
					base.NPC.alpha = 0;
				}
			}
			if (base.NPC.ai[2] != 0f && base.NPC.ai[3] != 0f)
			{
				if (base.NPC.type == 172)
				{
					base.NPC.alpha = 255;
				}
				SoundEngine.PlaySound(in SoundID.Item8, base.NPC.Center);
				for (int i = 0; i < 50; i++)
				{
					if (base.NPC.type == 29 || base.NPC.type == 45)
					{
						int goblinDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 27, 0f, 0f, 100, default(Color), Main.rand.Next(1, 3));
						Dust obj = Main.dust[goblinDust];
						obj.velocity *= 3f;
						if (Main.dust[goblinDust].scale > 1f)
						{
							Main.dust[goblinDust].noGravity = true;
						}
					}
					else if (base.NPC.type == 32)
					{
						int darkCasterDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 172, 0f, 0f, 100, default(Color), 1.5f);
						Dust obj2 = Main.dust[darkCasterDust];
						obj2.velocity *= 3f;
						Main.dust[darkCasterDust].noGravity = true;
					}
					else if (base.NPC.type == 283 || base.NPC.type == 284)
					{
						int necromancerDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173);
						Dust obj3 = Main.dust[necromancerDust];
						obj3.velocity *= 2f;
						Main.dust[necromancerDust].scale = 1.4f;
					}
					else if (base.NPC.type == 285 || base.NPC.type == 286)
					{
						int diabolistDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 174, 0f, 0f, 100, default(Color), 1.5f);
						Dust obj4 = Main.dust[diabolistDust];
						obj4.velocity *= 3f;
						Main.dust[diabolistDust].noGravity = true;
					}
					else if (base.NPC.type == 281 || base.NPC.type == 282)
					{
						int raggedCasterDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 175, 0f, 0f, 100, default(Color), 1.5f);
						Dust obj5 = Main.dust[raggedCasterDust];
						obj5.velocity *= 3f;
						Main.dust[raggedCasterDust].noGravity = true;
					}
					else if (base.NPC.type == 172)
					{
						int runeWizardDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 106, 0f, 0f, 100, default(Color), 2.5f);
						Dust obj6 = Main.dust[runeWizardDust];
						obj6.velocity *= 3f;
						Main.dust[runeWizardDust].noGravity = true;
					}
					else if (base.NPC.type == 533)
					{
						int desertSpiritDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 27, 0f, 0f, 100, default(Color), 2.5f);
						Dust obj7 = Main.dust[desertSpiritDust];
						obj7.velocity *= 3f;
						Main.dust[desertSpiritDust].noGravity = true;
					}
					else
					{
						int fireImpDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, 0f, 0f, 100, default(Color), 2.5f);
						Dust obj8 = Main.dust[fireImpDust];
						obj8.velocity *= 3f;
						Main.dust[fireImpDust].noGravity = true;
					}
				}
				base.NPC.position.X = base.NPC.ai[2] * 16f - (float)(base.NPC.width / 2) + 8f;
				base.NPC.position.Y = base.NPC.ai[3] * 16f - (float)base.NPC.height;
				base.NPC.velocity.X = 0f;
				base.NPC.velocity.Y = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				SoundEngine.PlaySound(in SoundID.Item8, base.NPC.Center);
				for (int j = 0; j < 50; j++)
				{
					if (base.NPC.type == 29 || base.NPC.type == 45)
					{
						int goblinCastDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 27, 0f, 0f, 100, default(Color), Main.rand.Next(1, 3));
						Dust obj9 = Main.dust[goblinCastDust];
						obj9.velocity *= 3f;
						if (Main.dust[goblinCastDust].scale > 1f)
						{
							Main.dust[goblinCastDust].noGravity = true;
						}
					}
					else if (base.NPC.type == 32)
					{
						int darkCasterCastDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 172, 0f, 0f, 100, default(Color), 1.5f);
						Dust obj10 = Main.dust[darkCasterCastDust];
						obj10.velocity *= 3f;
						Main.dust[darkCasterCastDust].noGravity = true;
					}
					else if (base.NPC.type == 172)
					{
						int runeWizardCastDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 106, 0f, 0f, 100, default(Color), 2.5f);
						Dust obj11 = Main.dust[runeWizardCastDust];
						obj11.velocity *= 3f;
						Main.dust[runeWizardCastDust].noGravity = true;
					}
					else if (base.NPC.type == 283 || base.NPC.type == 284)
					{
						int necromancerCastDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173);
						Dust obj12 = Main.dust[necromancerCastDust];
						obj12.velocity *= 2f;
						Main.dust[necromancerCastDust].scale = 1.4f;
					}
					else if (base.NPC.type == 285 || base.NPC.type == 286)
					{
						int diabolistCastDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 174, 0f, 0f, 100, default(Color), 1.5f);
						Dust obj13 = Main.dust[diabolistCastDust];
						obj13.velocity *= 3f;
						Main.dust[diabolistCastDust].noGravity = true;
					}
					else if (base.NPC.type == 281 || base.NPC.type == 282)
					{
						int raggedCasterCastDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 175, 0f, 0f, 100, default(Color), 1.5f);
						Dust obj14 = Main.dust[raggedCasterCastDust];
						obj14.velocity *= 3f;
						Main.dust[raggedCasterCastDust].noGravity = true;
					}
					else if (base.NPC.type == 533)
					{
						int desertSpiritCastDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 27, 0f, 0f, 100, default(Color), 2.5f);
						Dust obj15 = Main.dust[desertSpiritCastDust];
						obj15.velocity *= 3f;
						Main.dust[desertSpiritCastDust].noGravity = true;
					}
					else
					{
						int fireImpCastDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 6, 0f, 0f, 100, default(Color), 2.5f);
						Dust obj16 = Main.dust[fireImpCastDust];
						obj16.velocity *= 3f;
						Main.dust[fireImpCastDust].noGravity = true;
					}
				}
			}
			if (base.NPC.justHit)
			{
				base.NPC.ai[0] = ((base.NPC.type == 172 && Main.zenithWorld) ? 5f : (CalamityWorld.revenge ? 2f : 1f));
			}
			base.NPC.ai[0] += ((base.NPC.type == 172 && Main.zenithWorld) ? 5f : (CalamityWorld.revenge ? 2f : 1f));
			if (base.NPC.type == 283 || base.NPC.type == 284)
			{
				if (base.NPC.ai[0] % 50f == 0f && base.NPC.ai[0] <= 250f)
				{
					base.NPC.ai[1] = 55f;
					base.NPC.netUpdate = true;
				}
				if (base.NPC.ai[0] >= 400f)
				{
					base.NPC.ai[0] = 700f;
				}
			}
			else if (base.NPC.type == 172)
			{
				if (base.NPC.ai[0] == 80f || base.NPC.ai[0] == 150f || base.NPC.ai[0] == 230f || base.NPC.ai[0] == 300f || base.NPC.ai[0] == 380f || base.NPC.ai[0] == 450f)
				{
					base.NPC.ai[1] = 55f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.type == 533)
			{
				if (base.NPC.ai[0] == 180f)
				{
					base.NPC.ai[1] = 181f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.type == 281 || base.NPC.type == 282)
			{
				if (base.NPC.ai[0] == 20f || base.NPC.ai[0] == 40f || base.NPC.ai[0] == 60f || base.NPC.ai[0] == 120f || base.NPC.ai[0] == 140f || base.NPC.ai[0] == 160f || base.NPC.ai[0] == 220f || base.NPC.ai[0] == 240f || base.NPC.ai[0] == 260f)
				{
					base.NPC.ai[1] = 55f;
					base.NPC.netUpdate = true;
				}
				if (base.NPC.ai[0] >= 460f)
				{
					base.NPC.ai[0] = 700f;
				}
			}
			else
			{
				if (Main.getGoodWorld && base.NPC.type == 24 && NPC.AnyNPCs(113))
				{
					base.NPC.ai[0]++;
					if (base.NPC.ai[0] % 2f == 1f)
					{
						base.NPC.ai[0]--;
					}
				}
				if (base.NPC.ai[0] % 100f == 0f && base.NPC.ai[0] <= 300f)
				{
					base.NPC.ai[1] = 55f;
					base.NPC.netUpdate = true;
				}
			}
			if ((base.NPC.type == 285 || base.NPC.type == 286) && base.NPC.ai[0] > 400f)
			{
				base.NPC.ai[0] = 650f;
			}
			if (base.NPC.type == 533 && base.NPC.ai[0] >= 360f)
			{
				base.NPC.ai[0] = 650f;
			}
			if (base.NPC.ai[0] >= 650f && Main.netMode != 1)
			{
				base.NPC.ai[0] = (CalamityWorld.revenge ? 2f : 1f);
				int targetTileX = (int)Main.player[base.NPC.target].position.X / 16;
				int targetTileY = (int)Main.player[base.NPC.target].position.Y / 16;
				Vector2 chosenTile = Vector2.Zero;
				if (base.NPC.AI_AttemptToFindTeleportSpot(ref chosenTile, targetTileX, targetTileY))
				{
					base.NPC.ai[1] = 20f;
					base.NPC.ai[2] = chosenTile.X;
					base.NPC.ai[3] = chosenTile.Y;
				}
				base.NPC.netUpdate = true;
			}
			if (base.NPC.ai[1] > 0f)
			{
				base.NPC.ai[1]--;
				if (base.NPC.type == 533)
				{
					if (base.NPC.ai[1] % 30f == 0f && base.NPC.ai[1] / 30f < 5f)
					{
						SoundEngine.PlaySound(in SoundID.Item8, base.NPC.Center);
						if (Main.netMode != 1)
						{
							Point spiritCenter = base.NPC.Center.ToTileCoordinates();
							Point targetCenter = Main.player[base.NPC.target].Center.ToTileCoordinates();
							Vector2 targetDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
							int randProjRadius = 6;
							int spiritSpawnRadius = 6;
							int targetSpawnRadius = 0;
							int solidTileCheckRadius = 2;
							int projSpawnTries = 0;
							bool targetTooFar = false;
							if (((Vector2)(ref targetDirection)).Length() > 2000f)
							{
								targetTooFar = true;
							}
							while (!targetTooFar && projSpawnTries < 50)
							{
								projSpawnTries++;
								int spiritProjSpawnX = Main.rand.Next(targetCenter.X - randProjRadius, targetCenter.X + randProjRadius + 1);
								int spiritProjSpawnY = Main.rand.Next(targetCenter.Y - randProjRadius, targetCenter.Y + randProjRadius + 1);
								if ((spiritProjSpawnY >= targetCenter.Y - targetSpawnRadius && spiritProjSpawnY <= targetCenter.Y + targetSpawnRadius && spiritProjSpawnX >= targetCenter.X - targetSpawnRadius && spiritProjSpawnX <= targetCenter.X + targetSpawnRadius) || (spiritProjSpawnY >= spiritCenter.Y - spiritSpawnRadius && spiritProjSpawnY <= spiritCenter.Y + spiritSpawnRadius && spiritProjSpawnX >= spiritCenter.X - spiritSpawnRadius && spiritProjSpawnX <= spiritCenter.X + spiritSpawnRadius) || Main.tile[spiritProjSpawnX, spiritProjSpawnY].HasUnactuatedTile)
								{
									continue;
								}
								bool canSpawnProj = true;
								if (canSpawnProj && Main.tile[spiritProjSpawnX, spiritProjSpawnY].LiquidType == 1)
								{
									canSpawnProj = false;
								}
								if (canSpawnProj && Collision.SolidTiles(spiritProjSpawnX - solidTileCheckRadius, spiritProjSpawnX + solidTileCheckRadius, spiritProjSpawnY - solidTileCheckRadius, spiritProjSpawnY + solidTileCheckRadius))
								{
									canSpawnProj = false;
								}
								if (canSpawnProj)
								{
									int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spiritProjSpawnX * 16 + 8, spiritProjSpawnY * 16 + 8, 0f, 0f, 596, 0, 1f, Main.myPlayer, base.NPC.target);
									if (CalamityWorld.death)
									{
										Main.projectile[proj].extraUpdates++;
									}
									break;
								}
							}
						}
					}
				}
				else if (base.NPC.ai[1] == 25f)
				{
					if (base.NPC.type >= 281 && base.NPC.type <= 286)
					{
						if (Main.netMode != 1)
						{
							float dungeonCasterProjSpeed = (CalamityWorld.death ? 8f : 6f);
							if (base.NPC.type == 285 || base.NPC.type == 286)
							{
								dungeonCasterProjSpeed = (CalamityWorld.death ? 10f : 8f);
							}
							if (base.NPC.type == 281 || base.NPC.type == 282)
							{
								dungeonCasterProjSpeed = (CalamityWorld.death ? 5f : 4f);
							}
							Vector2 dungeonCasterPos = default(Vector2);
							((Vector2)(ref dungeonCasterPos))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y);
							float dungeonCasterTargetX = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - dungeonCasterPos.X;
							float dungeonCasterTargetY = Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height * 0.5f - dungeonCasterPos.Y;
							float dungeonCasterTargetDist = (float)Math.Sqrt(dungeonCasterTargetX * dungeonCasterTargetX + dungeonCasterTargetY * dungeonCasterTargetY);
							dungeonCasterTargetDist = dungeonCasterProjSpeed / dungeonCasterTargetDist;
							dungeonCasterTargetX *= dungeonCasterTargetDist;
							dungeonCasterTargetY *= dungeonCasterTargetDist;
							int damage = 16;
							int projType = 290;
							if (base.NPC.type == 285 || base.NPC.type == 286)
							{
								projType = 291;
								damage = 32;
							}
							if (base.NPC.type == 281 || base.NPC.type == 282)
							{
								projType = 293;
								damage = 32;
							}
							int dungeonCasterProj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), dungeonCasterPos.X, dungeonCasterPos.Y, dungeonCasterTargetX, dungeonCasterTargetY, projType, damage, 0f, Main.myPlayer);
							Main.projectile[dungeonCasterProj].timeLeft = 300;
							if (projType == 291)
							{
								Main.projectile[dungeonCasterProj].ai[0] = Main.player[base.NPC.target].Center.X;
								Main.projectile[dungeonCasterProj].ai[1] = Main.player[base.NPC.target].Center.Y;
								Main.projectile[dungeonCasterProj].netUpdate = true;
							}
							base.NPC.localAI[0] = 0f;
						}
					}
					else
					{
						if (base.NPC.type != 172)
						{
							SoundEngine.PlaySound(in SoundID.Item8, base.NPC.Center);
						}
						if (Main.netMode != 1)
						{
							if (base.NPC.type == 29 || base.NPC.type == 45)
							{
								NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y - 8, 30);
							}
							else if (base.NPC.type == 32)
							{
								NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y - 8, 33);
							}
							else if (base.NPC.type == 172)
							{
								float num = (CalamityWorld.death ? 12f : 10f);
								Vector2 vector14 = default(Vector2);
								((Vector2)(ref vector14))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
								float runeWizardTargetX = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - vector14.X;
								float runeWizardTargetY = Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height * 0.5f - vector14.Y;
								float runeWizardTargetDist = (float)Math.Sqrt(runeWizardTargetX * runeWizardTargetX + runeWizardTargetY * runeWizardTargetY);
								runeWizardTargetDist = num / runeWizardTargetDist;
								runeWizardTargetX *= runeWizardTargetDist;
								runeWizardTargetY *= runeWizardTargetDist;
								int runeWizardProj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), vector14.X, vector14.Y, runeWizardTargetX, runeWizardTargetY, 129, 40, 0f, Main.myPlayer);
								Main.projectile[runeWizardProj].timeLeft = 300;
								base.NPC.localAI[0] = 0f;
							}
							else
							{
								NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2 + base.NPC.direction * 8, (int)base.NPC.position.Y + 20, 25);
							}
						}
					}
				}
			}
			if (base.NPC.type == 29 || base.NPC.type == 45)
			{
				if (Main.rand.NextBool(5))
				{
					int shadowflameSpawnDust = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y + 2f), base.NPC.width, base.NPC.height, 27, base.NPC.velocity.X * 0.2f, base.NPC.velocity.Y * 0.2f, 100, default(Color), 1.5f);
					Dust obj17 = Main.dust[shadowflameSpawnDust];
					obj17.noGravity = true;
					obj17.velocity.X *= 0.5f;
					obj17.velocity.Y = -2f;
				}
			}
			else if (base.NPC.type == 32)
			{
				if (!Main.rand.NextBool(3))
				{
					int waterSpawnDust = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y + 2f), base.NPC.width, base.NPC.height, 172, base.NPC.velocity.X * 0.2f, base.NPC.velocity.Y * 0.2f, 100, default(Color), 0.9f);
					Dust obj18 = Main.dust[waterSpawnDust];
					obj18.noGravity = true;
					obj18.velocity.X *= 0.3f;
					obj18.velocity.Y *= 0.2f;
					obj18.velocity.Y--;
				}
			}
			else
			{
				if (base.NPC.type == 172)
				{
					int runeWizardDustAmt = 1;
					if (base.NPC.alpha == 255)
					{
						runeWizardDustAmt = 2;
					}
					for (int r = 0; r < runeWizardDustAmt; r++)
					{
						if (Main.rand.Next(255) > 255 - base.NPC.alpha)
						{
							int runeSpawnDust = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y + 2f), base.NPC.width, base.NPC.height, 106, base.NPC.velocity.X * 0.2f, base.NPC.velocity.Y * 0.2f, 100, default(Color), 1.2f);
							Dust obj19 = Main.dust[runeSpawnDust];
							obj19.noGravity = true;
							obj19.velocity.X *= 0.1f + (float)Main.rand.Next(30) * 0.01f;
							obj19.velocity.Y *= 0.1f + (float)Main.rand.Next(30) * 0.01f;
							obj19.scale *= 1f + (float)Main.rand.Next(6) * 0.1f;
						}
					}
					return false;
				}
				if (base.NPC.type == 283 || base.NPC.type == 284)
				{
					if (Main.rand.NextBool())
					{
						int necroSpawnDust = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y + 2f), base.NPC.width, base.NPC.height, 173);
						Dust obj20 = Main.dust[necroSpawnDust];
						obj20.velocity.X *= 0.5f;
						obj20.velocity.Y *= 0.5f;
					}
				}
				else if (base.NPC.type == 285 || base.NPC.type == 286)
				{
					if (Main.rand.NextBool())
					{
						int flameSpawnDust = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y + 2f), base.NPC.width, base.NPC.height, 174, base.NPC.velocity.X * 0.2f, base.NPC.velocity.Y * 0.2f, 100);
						Dust obj21 = Main.dust[flameSpawnDust];
						obj21.noGravity = true;
						obj21.velocity *= 0.4f;
						obj21.velocity.Y -= 0.7f;
						return false;
					}
				}
				else if (base.NPC.type == 281 || base.NPC.type == 282)
				{
					if (Main.rand.NextBool())
					{
						int ghostSpawnDust = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y + 2f), base.NPC.width, base.NPC.height, 175, base.NPC.velocity.X * 0.2f, base.NPC.velocity.Y * 0.2f, 100, default(Color), 0.1f);
						Dust obj22 = Main.dust[ghostSpawnDust];
						obj22.noGravity = true;
						obj22.velocity *= 0.5f;
						obj22.fadeIn = 1.2f;
					}
				}
				else
				{
					if (base.NPC.type == 533)
					{
						Lighting.AddLight(base.NPC.Top, 0.6f, 0.6f, 0.3f);
						return false;
					}
					if (Main.rand.NextBool())
					{
						int desertSpawnDust = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y + 2f), base.NPC.width, base.NPC.height, 6, base.NPC.velocity.X * 0.2f, base.NPC.velocity.Y * 0.2f, 100, default(Color), 2f);
						Dust obj23 = Main.dust[desertSpawnDust];
						obj23.noGravity = true;
						obj23.velocity.X *= 1f;
						obj23.velocity.Y *= 1f;
					}
				}
			}
			return false;
		}
	}

	public class CoriteAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0229: Unknown result type (might be due to invalid IL or missing references)
			//IL_022f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0259: Unknown result type (might be due to invalid IL or missing references)
			//IL_0263: Unknown result type (might be due to invalid IL or missing references)
			//IL_0268: Unknown result type (might be due to invalid IL or missing references)
			//IL_042f: Unknown result type (might be due to invalid IL or missing references)
			//IL_043a: Unknown result type (might be due to invalid IL or missing references)
			//IL_043f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0444: Unknown result type (might be due to invalid IL or missing references)
			//IL_0446: Unknown result type (might be due to invalid IL or missing references)
			//IL_0448: Unknown result type (might be due to invalid IL or missing references)
			//IL_044e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0453: Unknown result type (might be due to invalid IL or missing references)
			//IL_0458: Unknown result type (might be due to invalid IL or missing references)
			//IL_0471: Unknown result type (might be due to invalid IL or missing references)
			//IL_047b: Unknown result type (might be due to invalid IL or missing references)
			//IL_047d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0483: Unknown result type (might be due to invalid IL or missing references)
			//IL_0488: Unknown result type (might be due to invalid IL or missing references)
			//IL_048a: Unknown result type (might be due to invalid IL or missing references)
			//IL_048c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0492: Unknown result type (might be due to invalid IL or missing references)
			//IL_0497: Unknown result type (might be due to invalid IL or missing references)
			//IL_049f: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_029c: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0951: Unknown result type (might be due to invalid IL or missing references)
			//IL_0967: Unknown result type (might be due to invalid IL or missing references)
			//IL_051d: Unknown result type (might be due to invalid IL or missing references)
			//IL_097b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0996: Unknown result type (might be due to invalid IL or missing references)
			//IL_070f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0716: Unknown result type (might be due to invalid IL or missing references)
			//IL_071b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c3a: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_07fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0805: Unknown result type (might be due to invalid IL or missing references)
			//IL_0807: Unknown result type (might be due to invalid IL or missing references)
			//IL_0593: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_055a: Unknown result type (might be due to invalid IL or missing references)
			//IL_056e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d34: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d39: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c80: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c96: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a32: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a3c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a41: Unknown result type (might be due to invalid IL or missing references)
			//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_06db: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_06fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c52: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c57: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aac: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0abb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0abd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0837: Unknown result type (might be due to invalid IL or missing references)
			//IL_0865: Unknown result type (might be due to invalid IL or missing references)
			//IL_086b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0890: Unknown result type (might be due to invalid IL or missing references)
			//IL_089a: Unknown result type (might be due to invalid IL or missing references)
			//IL_089f: Unknown result type (might be due to invalid IL or missing references)
			//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_08be: Unknown result type (might be due to invalid IL or missing references)
			//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_08db: Unknown result type (might be due to invalid IL or missing references)
			//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_08e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_08ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_0345: Unknown result type (might be due to invalid IL or missing references)
			//IL_034d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0352: Unknown result type (might be due to invalid IL or missing references)
			//IL_0390: Unknown result type (might be due to invalid IL or missing references)
			//IL_0396: Unknown result type (might be due to invalid IL or missing references)
			//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d73: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d7e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d83: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d88: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d93: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d98: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dc7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b34: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aea: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0afc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b11: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b16: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b1d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b22: Unknown result type (might be due to invalid IL or missing references)
			//IL_0df7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e22: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e28: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e3f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e49: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e4e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e6c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e82: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e8d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e92: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e97: Unknown result type (might be due to invalid IL or missing references)
			//IL_0eb9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ee7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0eed: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f12: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f1c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f21: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f3f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f55: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f60: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f65: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f7f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f89: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f96: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fa1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fa6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fab: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fb0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fb5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fcf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ffd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1003: Unknown result type (might be due to invalid IL or missing references)
			//IL_1028: Unknown result type (might be due to invalid IL or missing references)
			//IL_1032: Unknown result type (might be due to invalid IL or missing references)
			//IL_1037: Unknown result type (might be due to invalid IL or missing references)
			//IL_1055: Unknown result type (might be due to invalid IL or missing references)
			//IL_106b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1076: Unknown result type (might be due to invalid IL or missing references)
			//IL_107b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1080: Unknown result type (might be due to invalid IL or missing references)
			//IL_1095: Unknown result type (might be due to invalid IL or missing references)
			//IL_109f: Unknown result type (might be due to invalid IL or missing references)
			//IL_10ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_10b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_10bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_10c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_10c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_10cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1221: Unknown result type (might be due to invalid IL or missing references)
			//IL_122c: Unknown result type (might be due to invalid IL or missing references)
			//IL_10e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_1114: Unknown result type (might be due to invalid IL or missing references)
			//IL_111a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1131: Unknown result type (might be due to invalid IL or missing references)
			//IL_113b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1140: Unknown result type (might be due to invalid IL or missing references)
			//IL_115e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1174: Unknown result type (might be due to invalid IL or missing references)
			//IL_117f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1184: Unknown result type (might be due to invalid IL or missing references)
			//IL_1189: Unknown result type (might be due to invalid IL or missing references)
			//IL_119e: Unknown result type (might be due to invalid IL or missing references)
			//IL_11a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_11b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_11c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_11c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_11cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_11d4: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.TargetClosest(faceTarget: false);
			base.NPC.rotation = base.NPC.velocity.ToRotation();
			if (Math.Sign(base.NPC.velocity.X) != 0)
			{
				base.NPC.spriteDirection = -Math.Sign(base.NPC.velocity.X);
			}
			if (base.NPC.rotation < -(float)Math.PI / 2f)
			{
				base.NPC.rotation += (float)Math.PI;
			}
			if (base.NPC.rotation > (float)Math.PI / 2f)
			{
				base.NPC.rotation -= (float)Math.PI;
			}
			if (base.NPC.type == 418)
			{
				base.NPC.spriteDirection = Math.Sign(base.NPC.velocity.X);
			}
			float npcKBResist = 0.4f;
			float upwardChargeSpeed = 12f;
			float idealUpwardDelta = 200f;
			float maximumDistanceBeforeCharge = 900f;
			float upwardMovementIntertia = 30f;
			float chargePhaseWait = 30f;
			float chargeWaitSlowdownMult = 0.95f;
			int chargeRandomness = 50;
			float chargeSpeed = 14f;
			float maximumChargeTime = 30f;
			float chargeDistanceCheck = 100f;
			float chargeIntertia = 20f;
			float chargeAcceleration = 0f;
			float minimumChargeSpeed = 7f;
			bool hasCoolDustPhase = true;
			if (base.NPC.type == 418)
			{
				npcKBResist = 0.3f;
				upwardChargeSpeed = 10f;
				idealUpwardDelta = 300f;
				maximumDistanceBeforeCharge = 1000f;
				upwardMovementIntertia = 60f;
				chargePhaseWait = 5f;
				chargeWaitSlowdownMult = 0.8f;
				chargeRandomness = 0;
				chargeSpeed = 10f;
				chargeDistanceCheck = 150f;
				chargeIntertia = 60f;
				chargeAcceleration = 1f / 3f;
				minimumChargeSpeed = 8f;
				hasCoolDustPhase = false;
			}
			chargeAcceleration *= chargeIntertia;
			if (CalamityWorld.death)
			{
				upwardChargeSpeed *= 1.25f;
				chargeSpeed *= 1.25f;
			}
			if (base.NPC.type == 388 && base.NPC.ai[0] != 3f)
			{
				int idx = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 226, 0f, 0f, 100, default(Color), 0.5f);
				Main.dust[idx].noGravity = true;
				Main.dust[idx].velocity = base.NPC.velocity / 5f;
				Vector2 rotationVector = default(Vector2);
				((Vector2)(ref rotationVector))._002Ector(-10f, 10f);
				if (base.NPC.spriteDirection == 1)
				{
					rotationVector.X *= -1f;
				}
				rotationVector = rotationVector.RotatedBy(base.NPC.rotation);
				Main.dust[idx].position = base.NPC.Center + rotationVector;
			}
			if (base.NPC.type == 418)
			{
				int dustSpawnChance = ((base.NPC.ai[0] != 2f) ? 1 : 2);
				int dustSpawnAreaSize = ((base.NPC.ai[0] == 2f) ? 30 : 20);
				for (int i = 0; i < 2; i++)
				{
					if (Main.rand.Next(3) < dustSpawnChance)
					{
						int idx2 = Dust.NewDust(base.NPC.Center - new Vector2((float)dustSpawnAreaSize), dustSpawnAreaSize * 2, dustSpawnAreaSize * 2, 6, base.NPC.velocity.X * 0.5f, base.NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
						Main.dust[idx2].noGravity = true;
						Dust obj = Main.dust[idx2];
						obj.velocity *= 0.2f;
						Main.dust[idx2].fadeIn = 1f;
					}
				}
			}
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.damage = 0;
				base.NPC.knockBackResist = npcKBResist;
				Vector2 playerDistanceNorm = Main.player[base.NPC.target].Center - base.NPC.Center;
				Vector2 upwardVelocity = playerDistanceNorm - Vector2.UnitY * idealUpwardDelta;
				float num = base.NPC.Distance(Main.player[base.NPC.target].Center);
				playerDistanceNorm = Vector2.Normalize(playerDistanceNorm) * upwardChargeSpeed;
				upwardVelocity = Vector2.Normalize(upwardVelocity) * upwardChargeSpeed;
				bool closeAngleDistance = Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1);
				if (base.NPC.ai[3] >= 120f)
				{
					closeAngleDistance = true;
				}
				closeAngleDistance &= base.NPC.AngleTo(Main.player[base.NPC.target].Center) > (float)Math.PI / 8f && base.NPC.AngleTo(Main.player[base.NPC.target].Center) < (float)Math.PI * 7f / 8f;
				if ((num < maximumDistanceBeforeCharge) | closeAngleDistance)
				{
					base.NPC.ai[0] = 1f;
					base.NPC.ai[2] = playerDistanceNorm.X;
					base.NPC.ai[3] = playerDistanceNorm.Y;
					base.NPC.netUpdate = true;
				}
				else
				{
					base.NPC.velocity = (base.NPC.velocity * (upwardMovementIntertia - 1f) + upwardVelocity) / upwardMovementIntertia;
					if (!closeAngleDistance)
					{
						base.NPC.ai[3]++;
						if (base.NPC.ai[3] == 120f)
						{
							base.NPC.netUpdate = true;
						}
					}
					else
					{
						base.NPC.ai[3] = 0f;
					}
				}
			}
			else if (base.NPC.ai[0] == 1f)
			{
				base.NPC.damage = 0;
				base.NPC.knockBackResist = 0f;
				bool decelerate = true;
				if (base.NPC.type == 418)
				{
					decelerate = ((Vector2)(ref base.NPC.velocity)).Length() > 2f;
					if (!decelerate && base.NPC.target >= 0 && !Main.player[base.NPC.target].DeadOrGhost)
					{
						Vector2 maxVelocity = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.Zero) * 0.1f;
						base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, maxVelocity, 0.25f);
					}
				}
				if (decelerate)
				{
					NPC nPC = base.NPC;
					nPC.velocity *= chargeWaitSlowdownMult;
				}
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] >= chargePhaseWait)
				{
					base.NPC.damage = base.NPC.defDamage;
					base.NPC.ai[0] = 2f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
					Vector2 velocity = new Vector2(base.NPC.ai[2], base.NPC.ai[3]) + new Vector2((float)Main.rand.Next(-chargeRandomness, chargeRandomness + 1), (float)Main.rand.Next(-chargeRandomness, chargeRandomness + 1)) * 0.04f;
					((Vector2)(ref velocity)).Normalize();
					velocity *= chargeSpeed;
					base.NPC.velocity = velocity;
				}
				if (base.NPC.type == 388 && Main.rand.NextBool(4))
				{
					int idx3 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 226, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[idx3].noGravity = true;
					Dust obj2 = Main.dust[idx3];
					obj2.velocity *= 2f;
					Main.dust[idx3].velocity = Main.dust[idx3].velocity / 2f + Vector2.Normalize(Main.dust[idx3].position - base.NPC.Center);
				}
			}
			else if (base.NPC.ai[0] == 2f)
			{
				base.NPC.damage = base.NPC.defDamage;
				base.NPC.knockBackResist = 0f;
				base.NPC.ai[1]++;
				bool aboveAndFar = Vector2.Distance(base.NPC.Center, Main.player[base.NPC.target].Center) > chargeDistanceCheck && base.NPC.Center.Y > Main.player[base.NPC.target].Center.Y;
				if (((base.NPC.ai[1] >= maximumChargeTime) & aboveAndFar) || ((Vector2)(ref base.NPC.velocity)).Length() < minimumChargeSpeed)
				{
					base.NPC.damage = 0;
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					NPC nPC2 = base.NPC;
					nPC2.velocity /= 2f;
					base.NPC.netUpdate = true;
					if (base.NPC.type == 418)
					{
						base.NPC.ai[1] = 45f;
						base.NPC.ai[0] = 4f;
					}
				}
				else
				{
					Vector2 distanceNormalized = Vector2.Normalize(Main.player[base.NPC.target].Center - base.NPC.Center);
					if (distanceNormalized.HasNaNs())
					{
						((Vector2)(ref distanceNormalized))._002Ector((float)base.NPC.direction, 0f);
					}
					base.NPC.velocity = (base.NPC.velocity * (chargeIntertia - 1f) + distanceNormalized * (((Vector2)(ref base.NPC.velocity)).Length() + chargeAcceleration)) / chargeIntertia;
				}
				if (hasCoolDustPhase && Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.ai[0] = 3f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[0] == 4f)
			{
				base.NPC.damage = 0;
				base.NPC.ai[1] -= 3f;
				if (base.NPC.ai[1] <= 0f)
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
				NPC nPC3 = base.NPC;
				nPC3.velocity *= (CalamityWorld.death ? 0.9f : 0.95f);
			}
			if (hasCoolDustPhase && base.NPC.ai[0] != 3f && Vector2.Distance(base.NPC.Center, Main.player[base.NPC.target].Center) < 64f)
			{
				base.NPC.ai[0] = 3f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				base.NPC.netUpdate = true;
			}
			if (base.NPC.ai[0] == 3f)
			{
				base.NPC.damage = base.NPC.defDamage;
				base.NPC.position = base.NPC.Center;
				base.NPC.width = (base.NPC.height = (CalamityWorld.death ? 360 : 240));
				NPC nPC4 = base.NPC;
				nPC4.position -= base.NPC.Size;
				base.NPC.velocity = Vector2.Zero;
				base.NPC.alpha = 255;
				Lighting.AddLight((int)base.NPC.Center.X / 16, (int)base.NPC.Center.Y / 16, 0.2f, 0.7f, 1.1f);
				for (int j = 0; j < 10; j++)
				{
					int idx4 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 31, 0f, 0f, 100, default(Color), 1.5f);
					Dust obj3 = Main.dust[idx4];
					obj3.velocity *= 1.4f;
					Main.dust[idx4].position = ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + base.NPC.Center;
				}
				for (int k = 0; k < 40; k++)
				{
					int idx5 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 226, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[idx5].noGravity = true;
					Dust obj4 = Main.dust[idx5];
					obj4.velocity *= 2f;
					Main.dust[idx5].position = ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + base.NPC.Center;
					Main.dust[idx5].velocity = Main.dust[idx5].velocity / 2f + Vector2.Normalize(Main.dust[idx5].position - base.NPC.Center);
					if (Main.rand.NextBool())
					{
						idx5 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 226, 0f, 0f, 100, default(Color), 0.9f);
						Main.dust[idx5].noGravity = true;
						Dust obj5 = Main.dust[idx5];
						obj5.velocity *= 1.2f;
						Main.dust[idx5].position = ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + base.NPC.Center;
						Main.dust[idx5].velocity = Main.dust[idx5].velocity / 2f + Vector2.Normalize(Main.dust[idx5].position - base.NPC.Center);
					}
					if (Main.rand.NextBool(4))
					{
						idx5 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 226, 0f, 0f, 100, default(Color), 0.7f);
						Dust obj6 = Main.dust[idx5];
						obj6.velocity *= 1.2f;
						Main.dust[idx5].position = ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * ((float)Main.rand.NextDouble() * 96f) + base.NPC.Center;
						Main.dust[idx5].velocity = Main.dust[idx5].velocity / 2f + Vector2.Normalize(Main.dust[idx5].position - base.NPC.Center);
					}
				}
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] >= 3f)
				{
					SoundEngine.PlaySound(in SoundID.Item14, base.NPC.Center);
					base.NPC.life = 0;
					base.NPC.HitEffect();
					base.NPC.active = false;
				}
			}
			return false;
		}
	}

	public class FighterAI : VanillaAIOverride
	{
		public static void BuffedPsychoAI(NPC npc)
		{
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			int psychoAlphaMax = 200;
			if (npc.ai[2] == 0f)
			{
				npc.alpha = psychoAlphaMax;
				npc.TargetClosest();
				if (!Main.player[npc.target].dead)
				{
					Vector2 val = Main.player[npc.target].Center - npc.Center;
					if (((Vector2)(ref val)).Length() < 170f)
					{
						npc.ai[2] = -16f;
					}
				}
				if (npc.velocity.X != 0f || npc.velocity.Y < 0f || npc.velocity.Y > 2f || npc.justHit)
				{
					npc.ai[2] = -16f;
				}
			}
			if (!(npc.ai[2] < 0f))
			{
				return;
			}
			if (npc.alpha > 0)
			{
				npc.alpha -= psychoAlphaMax / 16;
				if (npc.alpha < 0)
				{
					npc.alpha = 0;
				}
			}
			npc.ai[2]++;
			if (npc.ai[2] == 0f)
			{
				npc.ai[2] = 1f;
				npc.velocity.X = npc.direction * 2;
			}
		}

		public static void BuffedSwampThingAI(NPC npc)
		{
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			if (Main.netMode != 1 && Main.rand.NextBool(240))
			{
				npc.ai[2] = Main.rand.Next(-480, -60);
				npc.netUpdate = true;
			}
			if (npc.ai[2] < 0f)
			{
				npc.TargetClosest();
				if (npc.justHit)
				{
					npc.ai[2] = 0f;
				}
				if (Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
				{
					npc.ai[2] = 0f;
				}
			}
			if (npc.ai[2] < 0f)
			{
				npc.velocity.X *= 0.9f;
				if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
				{
					npc.velocity.X = 0f;
				}
				npc.ai[2]++;
				if (npc.ai[2] == 0f)
				{
					npc.velocity.X = (float)npc.direction * 0.1f;
				}
			}
		}

		public static void MedusaHeadDustEffect(NPC npc, float time)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			Vector2 headPosition = npc.Top + new Vector2((float)(npc.spriteDirection * 6), 6f);
			float rotationVectorMult = MathHelper.Lerp(20f, 30f, (time * 3f + 50f) / 182f);
			Main.rand.NextFloat();
			for (float i = 0f; i < 2f; i++)
			{
				Vector2 rotationVector = Vector2.UnitY.RotatedByRandom(Math.PI * 2.0) * (Main.rand.NextFloat() * 0.5f + 0.5f);
				Dust dust = Dust.NewDustDirect(headPosition, 0, 0, 228);
				dust.position = headPosition + rotationVector * rotationVectorMult;
				dust.noGravity = true;
				dust.velocity = rotationVector * 2f;
				dust.scale = 0.5f + Main.rand.NextFloat() * 0.5f;
			}
		}

		public static void FighterRunningAI(NPC npc, float velocityMax, float acceleration, float turnDeceleration, bool extraDeceleration = false, float extraDecelerationFactor = 0.99f)
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			if (npc.velocity.X < 0f - velocityMax || npc.velocity.X > velocityMax)
			{
				if (npc.velocity.Y == 0f)
				{
					npc.velocity *= turnDeceleration;
				}
			}
			else if (npc.velocity.X < velocityMax && npc.direction == 1)
			{
				npc.velocity.X += acceleration;
				if (extraDeceleration && npc.velocity.Y == 0f && npc.velocity.X < 0f)
				{
					npc.velocity.X *= extraDecelerationFactor;
				}
				if (npc.velocity.X > velocityMax)
				{
					npc.velocity.X = velocityMax;
				}
			}
			else if (npc.velocity.X > 0f - velocityMax && npc.direction == -1)
			{
				if (extraDeceleration && npc.velocity.Y == 0f && npc.velocity.X > 0f)
				{
					npc.velocity.X *= extraDecelerationFactor;
				}
				npc.velocity.X -= acceleration;
				if (npc.velocity.X < 0f - velocityMax)
				{
					npc.velocity.X = 0f - velocityMax;
				}
			}
		}

		public static void TryConvertToWallClimber(NPC npc)
		{
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			if (!new List<int>
			{
				163,
				239,
				530,
				236,
				164,
				ModContent.NPCType<AstralachneaGround>()
			}.Contains(npc.type))
			{
				return;
			}
			int tileCoordsX = (int)npc.Center.X / 16;
			int tileCoordsY = (int)npc.Center.Y / 16;
			bool climbWalls = false;
			for (int x = tileCoordsX - 1; x <= tileCoordsX + 1; x++)
			{
				for (int y = tileCoordsY - 1; y <= tileCoordsY + 1; y++)
				{
					if (Main.tile[x, y].WallType > 0)
					{
						climbWalls = true;
					}
				}
			}
			int transformType = -1;
			if (!climbWalls)
			{
				return;
			}
			if (npc.type == ModContent.NPCType<AstralachneaGround>())
			{
				transformType = ModContent.NPCType<AstralachneaWall>();
			}
			else
			{
				switch (npc.type)
				{
				case 163:
					transformType = 238;
					break;
				case 239:
					transformType = 240;
					break;
				case 530:
					transformType = 531;
					break;
				case 236:
					transformType = 237;
					break;
				case 164:
					transformType = 165;
					break;
				}
			}
			if (transformType != -1)
			{
				npc.Transform(transformType);
			}
		}

		public override bool AI(Mod mod)
		{
			//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04df: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_034c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0357: Unknown result type (might be due to invalid IL or missing references)
			//IL_0361: Unknown result type (might be due to invalid IL or missing references)
			//IL_0366: Unknown result type (might be due to invalid IL or missing references)
			//IL_036b: Unknown result type (might be due to invalid IL or missing references)
			//IL_017b: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0205: Unknown result type (might be due to invalid IL or missing references)
			//IL_020a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0214: Unknown result type (might be due to invalid IL or missing references)
			//IL_0219: Unknown result type (might be due to invalid IL or missing references)
			//IL_0683: Unknown result type (might be due to invalid IL or missing references)
			//IL_0694: Unknown result type (might be due to invalid IL or missing references)
			//IL_0282: Unknown result type (might be due to invalid IL or missing references)
			//IL_0286: Unknown result type (might be due to invalid IL or missing references)
			//IL_028b: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02df: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c78: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c88: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cb3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cc3: Unknown result type (might be due to invalid IL or missing references)
			//IL_127e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1289: Unknown result type (might be due to invalid IL or missing references)
			//IL_128e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1293: Unknown result type (might be due to invalid IL or missing references)
			//IL_12b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_12b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_12bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_12c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_12d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_12db: Unknown result type (might be due to invalid IL or missing references)
			//IL_12dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_12e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_12e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dfa: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dff: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e09: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e0e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e24: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e29: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e33: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e38: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e40: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e44: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e49: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ce9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d01: Unknown result type (might be due to invalid IL or missing references)
			//IL_12fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1142: Unknown result type (might be due to invalid IL or missing references)
			//IL_1147: Unknown result type (might be due to invalid IL or missing references)
			//IL_1149: Unknown result type (might be due to invalid IL or missing references)
			//IL_1151: Unknown result type (might be due to invalid IL or missing references)
			//IL_1156: Unknown result type (might be due to invalid IL or missing references)
			//IL_115b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1169: Unknown result type (might be due to invalid IL or missing references)
			//IL_1171: Unknown result type (might be due to invalid IL or missing references)
			//IL_13b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_13c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_2490: Unknown result type (might be due to invalid IL or missing references)
			//IL_24a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_1403: Unknown result type (might be due to invalid IL or missing references)
			//IL_1413: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac4: Unknown result type (might be due to invalid IL or missing references)
			//IL_20a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_20b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1836: Unknown result type (might be due to invalid IL or missing references)
			//IL_183b: Unknown result type (might be due to invalid IL or missing references)
			//IL_16c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_16a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_16b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_16b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aef: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aff: Unknown result type (might be due to invalid IL or missing references)
			//IL_27af: Unknown result type (might be due to invalid IL or missing references)
			//IL_27c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_20dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_20ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a8f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a99: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a9e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ac0: Unknown result type (might be due to invalid IL or missing references)
			//IL_2acb: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b00: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b3c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2112: Unknown result type (might be due to invalid IL or missing references)
			//IL_212a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1566: Unknown result type (might be due to invalid IL or missing references)
			//IL_1576: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d3b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b50: Unknown result type (might be due to invalid IL or missing references)
			//IL_225b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2260: Unknown result type (might be due to invalid IL or missing references)
			//IL_2271: Unknown result type (might be due to invalid IL or missing references)
			//IL_227b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2280: Unknown result type (might be due to invalid IL or missing references)
			//IL_2285: Unknown result type (might be due to invalid IL or missing references)
			//IL_228c: Unknown result type (might be due to invalid IL or missing references)
			//IL_22a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_22a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_22b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_22c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_22c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_22cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_22cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_22d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_22d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_22de: Unknown result type (might be due to invalid IL or missing references)
			//IL_22e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_22eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_22f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_22ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_2310: Unknown result type (might be due to invalid IL or missing references)
			//IL_231a: Unknown result type (might be due to invalid IL or missing references)
			//IL_231f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2324: Unknown result type (might be due to invalid IL or missing references)
			//IL_232f: Unknown result type (might be due to invalid IL or missing references)
			//IL_175b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b67: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b7f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b80: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ba8: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bae: Unknown result type (might be due to invalid IL or missing references)
			//IL_262b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2658: Unknown result type (might be due to invalid IL or missing references)
			//IL_265e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2693: Unknown result type (might be due to invalid IL or missing references)
			//IL_2698: Unknown result type (might be due to invalid IL or missing references)
			//IL_26a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_26ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_26b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_26bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_26c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_26cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_26d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_18b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_18be: Unknown result type (might be due to invalid IL or missing references)
			//IL_17c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_17db: Unknown result type (might be due to invalid IL or missing references)
			//IL_15f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1604: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cbd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cc8: Unknown result type (might be due to invalid IL or missing references)
			//IL_294e: Unknown result type (might be due to invalid IL or missing references)
			//IL_297b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2981: Unknown result type (might be due to invalid IL or missing references)
			//IL_23c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_23e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b4e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b59: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b5e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b63: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ce8: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c02: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c2c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c32: Unknown result type (might be due to invalid IL or missing references)
			//IL_1930: Unknown result type (might be due to invalid IL or missing references)
			//IL_193e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1943: Unknown result type (might be due to invalid IL or missing references)
			//IL_1945: Unknown result type (might be due to invalid IL or missing references)
			//IL_195b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1960: Unknown result type (might be due to invalid IL or missing references)
			//IL_1969: Unknown result type (might be due to invalid IL or missing references)
			//IL_1980: Unknown result type (might be due to invalid IL or missing references)
			//IL_1985: Unknown result type (might be due to invalid IL or missing references)
			//IL_1987: Unknown result type (might be due to invalid IL or missing references)
			//IL_199a: Unknown result type (might be due to invalid IL or missing references)
			//IL_19a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_19b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_19b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_19c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_19c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ea4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f06: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f16: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ece: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ed9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ede: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ee3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ee8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0eea: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f3c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f4c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ef5: Unknown result type (might be due to invalid IL or missing references)
			//IL_3466: Unknown result type (might be due to invalid IL or missing references)
			//IL_347c: Unknown result type (might be due to invalid IL or missing references)
			//IL_36c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_36d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_36d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_36dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f84: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f8d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fa1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fa6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fb0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fb5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fbe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fd2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fd7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fe1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fe6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fef: Unknown result type (might be due to invalid IL or missing references)
			//IL_3df0: Unknown result type (might be due to invalid IL or missing references)
			//IL_3dfb: Unknown result type (might be due to invalid IL or missing references)
			//IL_372c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3747: Unknown result type (might be due to invalid IL or missing references)
			//IL_3773: Unknown result type (might be due to invalid IL or missing references)
			//IL_378e: Unknown result type (might be due to invalid IL or missing references)
			//IL_37d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_37f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_359f: Unknown result type (might be due to invalid IL or missing references)
			//IL_35b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_32bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_32cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_32df: Unknown result type (might be due to invalid IL or missing references)
			//IL_32ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_32ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_32f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_32f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_32f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_32fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_3302: Unknown result type (might be due to invalid IL or missing references)
			//IL_3307: Unknown result type (might be due to invalid IL or missing references)
			//IL_3309: Unknown result type (might be due to invalid IL or missing references)
			//IL_330d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3312: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e25: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e30: Unknown result type (might be due to invalid IL or missing references)
			//IL_3821: Unknown result type (might be due to invalid IL or missing references)
			//IL_383c: Unknown result type (might be due to invalid IL or missing references)
			//IL_35d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_35e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_35e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_35ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_35f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_35fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_3600: Unknown result type (might be due to invalid IL or missing references)
			//IL_3605: Unknown result type (might be due to invalid IL or missing references)
			//IL_360a: Unknown result type (might be due to invalid IL or missing references)
			//IL_360c: Unknown result type (might be due to invalid IL or missing references)
			//IL_361c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3621: Unknown result type (might be due to invalid IL or missing references)
			//IL_362f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3639: Unknown result type (might be due to invalid IL or missing references)
			//IL_363e: Unknown result type (might be due to invalid IL or missing references)
			//IL_3640: Unknown result type (might be due to invalid IL or missing references)
			//IL_364a: Unknown result type (might be due to invalid IL or missing references)
			//IL_364f: Unknown result type (might be due to invalid IL or missing references)
			//IL_4284: Unknown result type (might be due to invalid IL or missing references)
			//IL_428e: Unknown result type (might be due to invalid IL or missing references)
			//IL_4293: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e61: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e6c: Unknown result type (might be due to invalid IL or missing references)
			//IL_386d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3888: Unknown result type (might be due to invalid IL or missing references)
			//IL_3337: Unknown result type (might be due to invalid IL or missing references)
			//IL_334f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3355: Unknown result type (might be due to invalid IL or missing references)
			//IL_3357: Unknown result type (might be due to invalid IL or missing references)
			//IL_335c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3370: Unknown result type (might be due to invalid IL or missing references)
			//IL_3375: Unknown result type (might be due to invalid IL or missing references)
			//IL_3388: Unknown result type (might be due to invalid IL or missing references)
			//IL_338d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3392: Unknown result type (might be due to invalid IL or missing references)
			//IL_38b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_38d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_5a56: Unknown result type (might be due to invalid IL or missing references)
			//IL_5a65: Unknown result type (might be due to invalid IL or missing references)
			//IL_5a6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_5a0d: Unknown result type (might be due to invalid IL or missing references)
			//IL_5a21: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e96: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ea1: Unknown result type (might be due to invalid IL or missing references)
			//IL_5a9f: Unknown result type (might be due to invalid IL or missing references)
			//IL_5aa9: Unknown result type (might be due to invalid IL or missing references)
			//IL_5aae: Unknown result type (might be due to invalid IL or missing references)
			//IL_5ab6: Unknown result type (might be due to invalid IL or missing references)
			//IL_5ac5: Unknown result type (might be due to invalid IL or missing references)
			//IL_5aca: Unknown result type (might be due to invalid IL or missing references)
			//IL_5acf: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ecb: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ed6: Unknown result type (might be due to invalid IL or missing references)
			//IL_5af2: Unknown result type (might be due to invalid IL or missing references)
			//IL_5afc: Unknown result type (might be due to invalid IL or missing references)
			//IL_5b01: Unknown result type (might be due to invalid IL or missing references)
			//IL_5b09: Unknown result type (might be due to invalid IL or missing references)
			//IL_5b18: Unknown result type (might be due to invalid IL or missing references)
			//IL_5b1d: Unknown result type (might be due to invalid IL or missing references)
			//IL_5b22: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f00: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f0b: Unknown result type (might be due to invalid IL or missing references)
			//IL_5b45: Unknown result type (might be due to invalid IL or missing references)
			//IL_5b4f: Unknown result type (might be due to invalid IL or missing references)
			//IL_5b54: Unknown result type (might be due to invalid IL or missing references)
			//IL_5b5c: Unknown result type (might be due to invalid IL or missing references)
			//IL_5b6b: Unknown result type (might be due to invalid IL or missing references)
			//IL_5b70: Unknown result type (might be due to invalid IL or missing references)
			//IL_5b75: Unknown result type (might be due to invalid IL or missing references)
			//IL_7617: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f3d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f48: Unknown result type (might be due to invalid IL or missing references)
			//IL_5dec: Unknown result type (might be due to invalid IL or missing references)
			//IL_5c4c: Unknown result type (might be due to invalid IL or missing references)
			//IL_5e17: Unknown result type (might be due to invalid IL or missing references)
			//IL_5e43: Unknown result type (might be due to invalid IL or missing references)
			//IL_5cab: Unknown result type (might be due to invalid IL or missing references)
			//IL_6434: Unknown result type (might be due to invalid IL or missing references)
			//IL_5d15: Unknown result type (might be due to invalid IL or missing references)
			//IL_645f: Unknown result type (might be due to invalid IL or missing references)
			//IL_648b: Unknown result type (might be due to invalid IL or missing references)
			//IL_5f26: Unknown result type (might be due to invalid IL or missing references)
			//IL_5f3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_5d77: Unknown result type (might be due to invalid IL or missing references)
			//IL_7069: Unknown result type (might be due to invalid IL or missing references)
			//IL_6e25: Unknown result type (might be due to invalid IL or missing references)
			//IL_6e3d: Unknown result type (might be due to invalid IL or missing references)
			//IL_6c9b: Unknown result type (might be due to invalid IL or missing references)
			//IL_6526: Unknown result type (might be due to invalid IL or missing references)
			//IL_5f60: Unknown result type (might be due to invalid IL or missing references)
			//IL_5f83: Unknown result type (might be due to invalid IL or missing references)
			//IL_5fa1: Unknown result type (might be due to invalid IL or missing references)
			//IL_5fb1: Unknown result type (might be due to invalid IL or missing references)
			//IL_7094: Unknown result type (might be due to invalid IL or missing references)
			//IL_70c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_6e5f: Unknown result type (might be due to invalid IL or missing references)
			//IL_6cc6: Unknown result type (might be due to invalid IL or missing references)
			//IL_6cf2: Unknown result type (might be due to invalid IL or missing references)
			//IL_67e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_67fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_654e: Unknown result type (might be due to invalid IL or missing references)
			//IL_655d: Unknown result type (might be due to invalid IL or missing references)
			//IL_61fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_6215: Unknown result type (might be due to invalid IL or missing references)
			//IL_621a: Unknown result type (might be due to invalid IL or missing references)
			//IL_621f: Unknown result type (might be due to invalid IL or missing references)
			//IL_6229: Unknown result type (might be due to invalid IL or missing references)
			//IL_622e: Unknown result type (might be due to invalid IL or missing references)
			//IL_6233: Unknown result type (might be due to invalid IL or missing references)
			//IL_6249: Unknown result type (might be due to invalid IL or missing references)
			//IL_6253: Unknown result type (might be due to invalid IL or missing references)
			//IL_6267: Unknown result type (might be due to invalid IL or missing references)
			//IL_6271: Unknown result type (might be due to invalid IL or missing references)
			//IL_6276: Unknown result type (might be due to invalid IL or missing references)
			//IL_627b: Unknown result type (might be due to invalid IL or missing references)
			//IL_6282: Unknown result type (might be due to invalid IL or missing references)
			//IL_628b: Unknown result type (might be due to invalid IL or missing references)
			//IL_6292: Unknown result type (might be due to invalid IL or missing references)
			//IL_629b: Unknown result type (might be due to invalid IL or missing references)
			//IL_62c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_62c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_771f: Unknown result type (might be due to invalid IL or missing references)
			//IL_7737: Unknown result type (might be due to invalid IL or missing references)
			//IL_681e: Unknown result type (might be due to invalid IL or missing references)
			//IL_682e: Unknown result type (might be due to invalid IL or missing references)
			//IL_684c: Unknown result type (might be due to invalid IL or missing references)
			//IL_685c: Unknown result type (might be due to invalid IL or missing references)
			//IL_7ac4: Unknown result type (might be due to invalid IL or missing references)
			//IL_7ac9: Unknown result type (might be due to invalid IL or missing references)
			//IL_6e90: Unknown result type (might be due to invalid IL or missing references)
			//IL_7ae8: Unknown result type (might be due to invalid IL or missing references)
			//IL_7aed: Unknown result type (might be due to invalid IL or missing references)
			//IL_7af2: Unknown result type (might be due to invalid IL or missing references)
			//IL_7afc: Unknown result type (might be due to invalid IL or missing references)
			//IL_7b01: Unknown result type (might be due to invalid IL or missing references)
			//IL_7b06: Unknown result type (might be due to invalid IL or missing references)
			//IL_7381: Unknown result type (might be due to invalid IL or missing references)
			//IL_7296: Unknown result type (might be due to invalid IL or missing references)
			//IL_65bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_65d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_77ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_7456: Unknown result type (might be due to invalid IL or missing references)
			//IL_746f: Unknown result type (might be due to invalid IL or missing references)
			//IL_7475: Unknown result type (might be due to invalid IL or missing references)
			//IL_748c: Unknown result type (might be due to invalid IL or missing references)
			//IL_65fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_661a: Unknown result type (might be due to invalid IL or missing references)
			//IL_661f: Unknown result type (might be due to invalid IL or missing references)
			//IL_6624: Unknown result type (might be due to invalid IL or missing references)
			//IL_7e3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_7e59: Unknown result type (might be due to invalid IL or missing references)
			//IL_79c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_79de: Unknown result type (might be due to invalid IL or missing references)
			//IL_739f: Unknown result type (might be due to invalid IL or missing references)
			//IL_73a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_73d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_73d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_73e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_73f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_740c: Unknown result type (might be due to invalid IL or missing references)
			//IL_741b: Unknown result type (might be due to invalid IL or missing references)
			//IL_7420: Unknown result type (might be due to invalid IL or missing references)
			//IL_7425: Unknown result type (might be due to invalid IL or missing references)
			//IL_72b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_72b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_72e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_72ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_72fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_7308: Unknown result type (might be due to invalid IL or missing references)
			//IL_7313: Unknown result type (might be due to invalid IL or missing references)
			//IL_731d: Unknown result type (might be due to invalid IL or missing references)
			//IL_7322: Unknown result type (might be due to invalid IL or missing references)
			//IL_7327: Unknown result type (might be due to invalid IL or missing references)
			//IL_7e7c: Unknown result type (might be due to invalid IL or missing references)
			//IL_7e97: Unknown result type (might be due to invalid IL or missing references)
			//IL_7ea9: Unknown result type (might be due to invalid IL or missing references)
			//IL_7ed5: Unknown result type (might be due to invalid IL or missing references)
			//IL_7a01: Unknown result type (might be due to invalid IL or missing references)
			//IL_7a1c: Unknown result type (might be due to invalid IL or missing references)
			//IL_7a2e: Unknown result type (might be due to invalid IL or missing references)
			//IL_7a5a: Unknown result type (might be due to invalid IL or missing references)
			//IL_77d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_74ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_74b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_74b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_6660: Unknown result type (might be due to invalid IL or missing references)
			//IL_6665: Unknown result type (might be due to invalid IL or missing references)
			//IL_6676: Unknown result type (might be due to invalid IL or missing references)
			//IL_6680: Unknown result type (might be due to invalid IL or missing references)
			//IL_6687: Unknown result type (might be due to invalid IL or missing references)
			//IL_668c: Unknown result type (might be due to invalid IL or missing references)
			//IL_85f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_8603: Unknown result type (might be due to invalid IL or missing references)
			//IL_7d85: Unknown result type (might be due to invalid IL or missing references)
			//IL_7d9c: Unknown result type (might be due to invalid IL or missing references)
			//IL_7da2: Unknown result type (might be due to invalid IL or missing references)
			//IL_7db6: Unknown result type (might be due to invalid IL or missing references)
			//IL_7dc0: Unknown result type (might be due to invalid IL or missing references)
			//IL_7dc5: Unknown result type (might be due to invalid IL or missing references)
			//IL_7803: Unknown result type (might be due to invalid IL or missing references)
			//IL_7817: Unknown result type (might be due to invalid IL or missing references)
			//IL_781d: Unknown result type (might be due to invalid IL or missing references)
			//IL_7831: Unknown result type (might be due to invalid IL or missing references)
			//IL_783b: Unknown result type (might be due to invalid IL or missing references)
			//IL_7840: Unknown result type (might be due to invalid IL or missing references)
			//IL_81e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_81fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_8201: Unknown result type (might be due to invalid IL or missing references)
			//IL_8215: Unknown result type (might be due to invalid IL or missing references)
			//IL_821f: Unknown result type (might be due to invalid IL or missing references)
			//IL_8224: Unknown result type (might be due to invalid IL or missing references)
			//IL_787c: Unknown result type (might be due to invalid IL or missing references)
			//IL_7881: Unknown result type (might be due to invalid IL or missing references)
			//IL_7883: Unknown result type (might be due to invalid IL or missing references)
			//IL_7888: Unknown result type (might be due to invalid IL or missing references)
			//IL_788d: Unknown result type (might be due to invalid IL or missing references)
			//IL_7892: Unknown result type (might be due to invalid IL or missing references)
			//IL_78a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_78ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_78ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_78bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_801b: Unknown result type (might be due to invalid IL or missing references)
			//IL_8036: Unknown result type (might be due to invalid IL or missing references)
			//IL_7baa: Unknown result type (might be due to invalid IL or missing references)
			//IL_7bc5: Unknown result type (might be due to invalid IL or missing references)
			//IL_8059: Unknown result type (might be due to invalid IL or missing references)
			//IL_8074: Unknown result type (might be due to invalid IL or missing references)
			//IL_8089: Unknown result type (might be due to invalid IL or missing references)
			//IL_80b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_7be8: Unknown result type (might be due to invalid IL or missing references)
			//IL_7c03: Unknown result type (might be due to invalid IL or missing references)
			//IL_7c18: Unknown result type (might be due to invalid IL or missing references)
			//IL_7c44: Unknown result type (might be due to invalid IL or missing references)
			//IL_66c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_66d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_66d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_66db: Unknown result type (might be due to invalid IL or missing references)
			//IL_66e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_66eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_a686: Unknown result type (might be due to invalid IL or missing references)
			//IL_a1d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_a1d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_a1f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_a226: Unknown result type (might be due to invalid IL or missing references)
			//IL_a24f: Unknown result type (might be due to invalid IL or missing references)
			//IL_a270: Unknown result type (might be due to invalid IL or missing references)
			//IL_8113: Unknown result type (might be due to invalid IL or missing references)
			//IL_8115: Unknown result type (might be due to invalid IL or missing references)
			//IL_8128: Unknown result type (might be due to invalid IL or missing references)
			//IL_812d: Unknown result type (might be due to invalid IL or missing references)
			//IL_812f: Unknown result type (might be due to invalid IL or missing references)
			//IL_8134: Unknown result type (might be due to invalid IL or missing references)
			//IL_8139: Unknown result type (might be due to invalid IL or missing references)
			//IL_813e: Unknown result type (might be due to invalid IL or missing references)
			//IL_8145: Unknown result type (might be due to invalid IL or missing references)
			//IL_814a: Unknown result type (might be due to invalid IL or missing references)
			//IL_8158: Unknown result type (might be due to invalid IL or missing references)
			//IL_815a: Unknown result type (might be due to invalid IL or missing references)
			//IL_815c: Unknown result type (might be due to invalid IL or missing references)
			//IL_8161: Unknown result type (might be due to invalid IL or missing references)
			//IL_8166: Unknown result type (might be due to invalid IL or missing references)
			//IL_8170: Unknown result type (might be due to invalid IL or missing references)
			//IL_8175: Unknown result type (might be due to invalid IL or missing references)
			//IL_817a: Unknown result type (might be due to invalid IL or missing references)
			//IL_7cae: Unknown result type (might be due to invalid IL or missing references)
			//IL_7cb0: Unknown result type (might be due to invalid IL or missing references)
			//IL_7cc3: Unknown result type (might be due to invalid IL or missing references)
			//IL_7cc8: Unknown result type (might be due to invalid IL or missing references)
			//IL_7cca: Unknown result type (might be due to invalid IL or missing references)
			//IL_7ccf: Unknown result type (might be due to invalid IL or missing references)
			//IL_7cd4: Unknown result type (might be due to invalid IL or missing references)
			//IL_7cd9: Unknown result type (might be due to invalid IL or missing references)
			//IL_7ce0: Unknown result type (might be due to invalid IL or missing references)
			//IL_7ce5: Unknown result type (might be due to invalid IL or missing references)
			//IL_7cf3: Unknown result type (might be due to invalid IL or missing references)
			//IL_7cf5: Unknown result type (might be due to invalid IL or missing references)
			//IL_7cf7: Unknown result type (might be due to invalid IL or missing references)
			//IL_7cfc: Unknown result type (might be due to invalid IL or missing references)
			//IL_7d01: Unknown result type (might be due to invalid IL or missing references)
			//IL_7d0b: Unknown result type (might be due to invalid IL or missing references)
			//IL_7d10: Unknown result type (might be due to invalid IL or missing references)
			//IL_7d15: Unknown result type (might be due to invalid IL or missing references)
			//IL_9fa3: Unknown result type (might be due to invalid IL or missing references)
			//IL_9faa: Unknown result type (might be due to invalid IL or missing references)
			//IL_b362: Unknown result type (might be due to invalid IL or missing references)
			//IL_b383: Unknown result type (might be due to invalid IL or missing references)
			//IL_b393: Unknown result type (might be due to invalid IL or missing references)
			//IL_b398: Unknown result type (might be due to invalid IL or missing references)
			//IL_a050: Unknown result type (might be due to invalid IL or missing references)
			//IL_a058: Unknown result type (might be due to invalid IL or missing references)
			//IL_b3bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_b3eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_a58c: Unknown result type (might be due to invalid IL or missing references)
			//IL_a5a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_b0ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_b108: Unknown result type (might be due to invalid IL or missing references)
			//IL_b128: Unknown result type (might be due to invalid IL or missing references)
			//IL_b143: Unknown result type (might be due to invalid IL or missing references)
			//IL_96d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_96fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_9e64: Unknown result type (might be due to invalid IL or missing references)
			//IL_9e73: Unknown result type (might be due to invalid IL or missing references)
			//IL_9e78: Unknown result type (might be due to invalid IL or missing references)
			//IL_9e7d: Unknown result type (might be due to invalid IL or missing references)
			//IL_9742: Unknown result type (might be due to invalid IL or missing references)
			//IL_9751: Unknown result type (might be due to invalid IL or missing references)
			//IL_9756: Unknown result type (might be due to invalid IL or missing references)
			//IL_976e: Unknown result type (might be due to invalid IL or missing references)
			//IL_9bef: Unknown result type (might be due to invalid IL or missing references)
			//IL_9c05: Unknown result type (might be due to invalid IL or missing references)
			//IL_9c1c: Unknown result type (might be due to invalid IL or missing references)
			//IL_9c34: Unknown result type (might be due to invalid IL or missing references)
			//IL_986b: Unknown result type (might be due to invalid IL or missing references)
			//IL_98bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_9c65: Unknown result type (might be due to invalid IL or missing references)
			//IL_9c7b: Unknown result type (might be due to invalid IL or missing references)
			//IL_9c92: Unknown result type (might be due to invalid IL or missing references)
			//IL_9caa: Unknown result type (might be due to invalid IL or missing references)
			//IL_9d1f: Unknown result type (might be due to invalid IL or missing references)
			//IL_9d26: Unknown result type (might be due to invalid IL or missing references)
			//IL_9d2b: Unknown result type (might be due to invalid IL or missing references)
			//IL_8cef: Unknown result type (might be due to invalid IL or missing references)
			//IL_8c72: Unknown result type (might be due to invalid IL or missing references)
			//IL_8c87: Unknown result type (might be due to invalid IL or missing references)
			//IL_8c8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_8c91: Unknown result type (might be due to invalid IL or missing references)
			//IL_8e54: Unknown result type (might be due to invalid IL or missing references)
			//IL_5182: Unknown result type (might be due to invalid IL or missing references)
			//IL_518c: Unknown result type (might be due to invalid IL or missing references)
			//IL_5191: Unknown result type (might be due to invalid IL or missing references)
			//IL_52b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_52cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_51c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_51cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_51d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_557a: Unknown result type (might be due to invalid IL or missing references)
			//IL_5590: Unknown result type (might be due to invalid IL or missing references)
			//IL_5595: Unknown result type (might be due to invalid IL or missing references)
			//IL_559a: Unknown result type (might be due to invalid IL or missing references)
			//IL_55c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_55e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_90f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_90f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_9284: Unknown result type (might be due to invalid IL or missing references)
			//IL_928b: Unknown result type (might be due to invalid IL or missing references)
			//IL_9138: Unknown result type (might be due to invalid IL or missing references)
			//IL_917b: Unknown result type (might be due to invalid IL or missing references)
			//IL_9470: Unknown result type (might be due to invalid IL or missing references)
			//IL_9477: Unknown result type (might be due to invalid IL or missing references)
			//IL_93fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_940b: Unknown result type (might be due to invalid IL or missing references)
			//IL_9458: Unknown result type (might be due to invalid IL or missing references)
			//IL_945d: Unknown result type (might be due to invalid IL or missing references)
			//IL_931c: Unknown result type (might be due to invalid IL or missing references)
			//IL_933b: Unknown result type (might be due to invalid IL or missing references)
			//IL_9216: Unknown result type (might be due to invalid IL or missing references)
			//IL_9218: Unknown result type (might be due to invalid IL or missing references)
			//IL_921a: Unknown result type (might be due to invalid IL or missing references)
			//IL_921f: Unknown result type (might be due to invalid IL or missing references)
			//IL_9224: Unknown result type (might be due to invalid IL or missing references)
			//IL_922e: Unknown result type (might be due to invalid IL or missing references)
			//IL_9233: Unknown result type (might be due to invalid IL or missing references)
			//IL_9238: Unknown result type (might be due to invalid IL or missing references)
			//IL_58da: Unknown result type (might be due to invalid IL or missing references)
			//IL_58e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_58ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_58ef: Unknown result type (might be due to invalid IL or missing references)
			int npcType = base.NPC.type;
			if (base.NPC.ModNPC != null && base.NPC.ModNPC.AIType != 0)
			{
				npcType = base.NPC.ModNPC.AIType;
			}
			if (npcType == 466)
			{
				BuffedPsychoAI(base.NPC);
			}
			if (npcType == 166)
			{
				BuffedSwampThingAI(base.NPC);
			}
			if (npcType == 461)
			{
				if (base.NPC.wet)
				{
					base.NPC.knockBackResist = 0f;
					base.NPC.ai[3] = -0.10101f;
					base.NPC.noGravity = true;
					base.NPC.width = 34;
					base.NPC.height = 24;
					base.NPC.position = base.NPC.Center - base.NPC.Size / 2f;
					base.NPC.TargetClosest();
					if (base.NPC.collideX)
					{
						base.NPC.velocity.X = 0f - base.NPC.oldVelocity.X;
					}
					if (base.NPC.velocity.X < 0f)
					{
						base.NPC.direction = -1;
					}
					if (base.NPC.velocity.X > 0f)
					{
						base.NPC.direction = 1;
					}
					if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].Center, 1, 1))
					{
						base.NPC.velocity = (base.NPC.velocity * 19f + base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center, -Vector2.UnitY) * 10f) / 20f;
						return false;
					}
					float velocityMultiplier = 10f;
					if (base.NPC.velocity.Y > 0f)
					{
						velocityMultiplier = 6f;
					}
					if (base.NPC.velocity.Y < 0f)
					{
						velocityMultiplier = 16f;
					}
					Vector2 directionVectorNormalized = default(Vector2);
					((Vector2)(ref directionVectorNormalized))._002Ector((float)base.NPC.direction, -1f);
					((Vector2)(ref directionVectorNormalized)).Normalize();
					directionVectorNormalized *= velocityMultiplier;
					if (velocityMultiplier < 5f)
					{
						base.NPC.velocity = (base.NPC.velocity * 24f + directionVectorNormalized) / 25f;
						return false;
					}
					base.NPC.velocity = (base.NPC.velocity * 9f + directionVectorNormalized) / 10f;
					return false;
				}
				base.NPC.knockBackResist = (CalamityWorld.death ? 0.1f : 0.2f);
				base.NPC.noGravity = false;
				base.NPC.width = 18;
				base.NPC.height = 40;
				base.NPC.position = base.NPC.Center - base.NPC.Size / 2f;
				if (base.NPC.ai[3] == -0.10101f)
				{
					base.NPC.ai[3] = 0f;
					float velocityMagnitude = ((Vector2)(ref base.NPC.velocity)).Length();
					velocityMagnitude *= 2f;
					if (velocityMagnitude > 12f)
					{
						velocityMagnitude = 12f;
					}
					((Vector2)(ref base.NPC.velocity)).Normalize();
					NPC nPC = base.NPC;
					nPC.velocity *= velocityMagnitude;
					base.NPC.direction = (base.NPC.spriteDirection = (base.NPC.velocity.X > 0f).ToDirectionInt());
				}
			}
			if (npcType == 379 || npcType == 380)
			{
				if (base.NPC.ai[3] < 0f)
				{
					base.NPC.damage = 0;
					base.NPC.velocity.X *= 0.93f;
					if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
					{
						base.NPC.velocity.X = 0f;
					}
					int targetNPC = (int)(0f - base.NPC.ai[3] - 1f);
					int directionToTarget = Math.Sign(Main.npc[targetNPC].Center.X - base.NPC.Center.X);
					if (directionToTarget != base.NPC.direction)
					{
						base.NPC.velocity.X = 0f;
						base.NPC.direction = directionToTarget;
						base.NPC.netUpdate = true;
					}
					if (base.NPC.justHit && Main.netMode != 1 && Main.npc[targetNPC].localAI[0] == 0f)
					{
						Main.npc[targetNPC].localAI[0] = 1f;
					}
					if (base.NPC.ai[0] < 1000f)
					{
						base.NPC.ai[0] = 1000f;
					}
					base.NPC.ai[0]++;
					if (base.NPC.ai[0] >= 1300f)
					{
						base.NPC.ai[0] = 1000f;
						base.NPC.netUpdate = true;
					}
					return false;
				}
				if (base.NPC.ai[0] >= 1000f)
				{
					base.NPC.ai[0] = 0f;
				}
				base.NPC.damage = base.NPC.defDamage;
			}
			if (npcType == 383 && base.NPC.ai[2] == 0f && base.NPC.localAI[0] == 0f && Main.netMode != 1)
			{
				int shield = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 384, base.NPC.whoAmI);
				base.NPC.ai[2] = shield + 1;
				base.NPC.localAI[0] = -1f;
				base.NPC.netUpdate = true;
				Main.npc[shield].ai[0] = base.NPC.whoAmI;
				Main.npc[shield].netUpdate = true;
			}
			if (npcType == 383)
			{
				int shield2 = (int)base.NPC.ai[2] - 1;
				if (shield2 != -1 && Main.npc[shield2].active && Main.npc[shield2].type == 384)
				{
					base.NPC.dontTakeDamage = true;
				}
				else
				{
					base.NPC.dontTakeDamage = false;
					base.NPC.ai[2] = 0f;
					if (base.NPC.localAI[0] == -1f)
					{
						base.NPC.localAI[0] = (CalamityWorld.death ? 60f : 120f);
					}
					if (base.NPC.localAI[0] > 0f)
					{
						base.NPC.localAI[0]--;
					}
				}
			}
			if (npcType == 482)
			{
				int activeTime = 300;
				int defendTime = 120;
				base.NPC.damage = base.NPC.defDamage;
				base.NPC.defense = base.NPC.defDefense;
				if (base.NPC.ai[2] < 0f)
				{
					base.NPC.damage = 0;
					base.NPC.defense = base.NPC.defDefense + 15;
					base.NPC.ai[2]++;
					base.NPC.velocity.X *= 0.9f;
					if ((double)Math.Abs(base.NPC.velocity.X) < 0.001)
					{
						base.NPC.velocity.X = 0.001f * (float)base.NPC.direction;
					}
					if (Math.Abs(base.NPC.velocity.Y) > 1f)
					{
						base.NPC.ai[2] += 10f;
					}
					if (base.NPC.ai[2] >= 0f)
					{
						base.NPC.netUpdate = true;
						base.NPC.velocity.X += (float)base.NPC.direction * 0.3f;
					}
					return false;
				}
				if (base.NPC.ai[2] < (float)activeTime)
				{
					if (base.NPC.justHit)
					{
						base.NPC.ai[2] += 15f;
					}
					base.NPC.ai[2]++;
				}
				else if (base.NPC.velocity.Y == 0f)
				{
					base.NPC.ai[2] = (float)defendTime * -1f;
					base.NPC.netUpdate = true;
				}
			}
			if (npcType == 480)
			{
				int afterHitTime = 90;
				int afterWaitTime = 210;
				int maxTime = 270;
				int debuffTime = (CalamityWorld.death ? 4 : 2) * 60;
				int turnToStoneTime = 20;
				float mesudaActiveDistance = (CalamityWorld.death ? 1500f : 900f);
				float medusaEffectDistance = (CalamityWorld.death ? 1600f : 1000f);
				if (base.NPC.ai[2] > 0f)
				{
					base.NPC.ai[2]--;
				}
				else if (base.NPC.ai[2] == 0f)
				{
					if (((Main.player[base.NPC.target].Center.X < base.NPC.Center.X && base.NPC.direction < 0) || (Main.player[base.NPC.target].Center.X > base.NPC.Center.X && base.NPC.direction > 0)) && base.NPC.velocity.Y == 0f && base.NPC.Distance(Main.player[base.NPC.target].Center) < mesudaActiveDistance && Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
					{
						base.NPC.ai[2] = (float)maxTime * -1f - (float)turnToStoneTime;
						base.NPC.netUpdate = true;
					}
				}
				else
				{
					if (base.NPC.ai[2] < 0f && base.NPC.ai[2] < (float)maxTime * -1f)
					{
						base.NPC.velocity.X *= 0.9f;
						if (base.NPC.velocity.Y < -2f || base.NPC.velocity.Y > 4f || base.NPC.justHit)
						{
							base.NPC.ai[2] = afterHitTime;
						}
						else
						{
							base.NPC.ai[2]++;
							if (base.NPC.ai[2] == 0f)
							{
								base.NPC.ai[2] = afterWaitTime;
							}
						}
						float time = base.NPC.ai[2] + (float)maxTime + (float)turnToStoneTime;
						if (time == 1f)
						{
							SoundEngine.PlaySound(in SoundID.NPCDeath17, base.NPC.Center);
						}
						if (time < (float)turnToStoneTime)
						{
							MedusaHeadDustEffect(base.NPC, time);
						}
						Lighting.AddLight(base.NPC.Center, 0.9f, 0.75f, 0.1f);
						return false;
					}
					if (base.NPC.ai[2] < 0f && base.NPC.ai[2] >= (float)maxTime * -1f)
					{
						Lighting.AddLight(base.NPC.Center, 0.9f, 0.75f, 0.1f);
						base.NPC.velocity.X *= 0.9f;
						if (base.NPC.velocity.Y < -2f || base.NPC.velocity.Y > 4f || base.NPC.justHit)
						{
							base.NPC.ai[2] = afterHitTime;
						}
						else
						{
							base.NPC.ai[2]++;
							if (base.NPC.ai[2] == 0f)
							{
								base.NPC.ai[2] = afterWaitTime;
							}
						}
						float time2 = base.NPC.ai[2] + (float)maxTime;
						if (time2 < 180f && (Main.rand.NextBool(3) || base.NPC.ai[2] % 3f == 0f))
						{
							MedusaHeadDustEffect(base.NPC, time2);
						}
						if (!Main.dedServ)
						{
							Player player = Main.LocalPlayer;
							if (!player.dead && player.active && player.FindBuffIndex(156) == -1 && base.NPC.Distance(player.Center) < medusaEffectDistance)
							{
								bool canTurnPlayerToStone = base.NPC.Distance(player.Center) < 30f;
								if (!canTurnPlayerToStone)
								{
									float x = (float)Math.Cos(0.7853981852531433);
									Vector2 vector6 = Vector2.Normalize(player.Center - base.NPC.Center);
									if (vector6.X > x || vector6.X < 0f - x)
									{
										canTurnPlayerToStone = true;
									}
								}
								if ((((player.Center.X < base.NPC.Center.X && base.NPC.direction < 0 && player.direction > 0) || (player.Center.X > base.NPC.Center.X && base.NPC.direction > 0 && player.direction < 0)) & canTurnPlayerToStone) && (Collision.CanHitLine(base.NPC.Center, 1, 1, player.Center, 1, 1) || Collision.CanHitLine(base.NPC.Center - Vector2.UnitY * 16f, 1, 1, player.Center, 1, 1) || Collision.CanHitLine(base.NPC.Center + Vector2.UnitY * 8f, 1, 1, player.Center, 1, 1)))
								{
									player.AddBuff(156, debuffTime + (int)base.NPC.ai[2] * -1);
								}
							}
						}
						return false;
					}
				}
			}
			Vector2 center;
			if (npcType == 471)
			{
				if (base.NPC.ai[3] < 0f)
				{
					base.NPC.knockBackResist = 0f;
					base.NPC.defense = (int)Math.Round((double)base.NPC.defDefense * 1.3);
					base.NPC.noGravity = true;
					base.NPC.noTileCollide = true;
					base.NPC.direction = (base.NPC.velocity.X > 0f).ToDirectionInt();
					base.NPC.rotation = base.NPC.velocity.X * 0.1f;
					if (Main.netMode != 1)
					{
						base.NPC.localAI[3]++;
						if (base.NPC.localAI[3] > (float)Main.rand.Next(20, CalamityWorld.death ? 40 : 120))
						{
							base.NPC.localAI[3] = 0f;
							Vector2 spawnPosition = base.NPC.Center;
							spawnPosition += base.NPC.velocity;
							NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)spawnPosition.X, (int)spawnPosition.Y, 30);
						}
					}
				}
				else
				{
					base.NPC.localAI[3] = 0f;
					base.NPC.knockBackResist = 0.2f;
					base.NPC.rotation *= 0.9f;
					base.NPC.defense = base.NPC.defDefense;
					base.NPC.noGravity = false;
					base.NPC.noTileCollide = false;
				}
				if (base.NPC.ai[3] == 1f)
				{
					base.NPC.knockBackResist = 0f;
					base.NPC.defense += 10;
				}
				if (base.NPC.ai[3] == -1f)
				{
					base.NPC.TargetClosest();
					float velocityMultiplier2 = 10f;
					float turnValue = 40f;
					Vector2 targetDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
					float playerDistance = ((Vector2)(ref targetDirection)).Length();
					velocityMultiplier2 += playerDistance / 200f;
					((Vector2)(ref targetDirection)).Normalize();
					targetDirection *= velocityMultiplier2;
					base.NPC.velocity = (base.NPC.velocity * (turnValue - 1f) + targetDirection) / turnValue;
					if (playerDistance < 500f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
					{
						base.NPC.ai[3] = 0f;
						base.NPC.ai[2] = 0f;
					}
					return false;
				}
				if (base.NPC.ai[3] == -2f)
				{
					base.NPC.velocity.Y -= 0.2f;
					if (base.NPC.velocity.Y < -12f)
					{
						base.NPC.velocity.Y = -12f;
					}
					if (Main.player[base.NPC.target].Center.Y - base.NPC.Center.Y > 200f)
					{
						base.NPC.TargetClosest();
						base.NPC.ai[3] = -3f;
						if (Main.player[base.NPC.target].Center.X > base.NPC.Center.X)
						{
							base.NPC.ai[2] = 1f;
						}
						else
						{
							base.NPC.ai[2] = -1f;
						}
					}
					base.NPC.velocity.X *= 0.99f;
					return false;
				}
				if (base.NPC.ai[3] == -3f)
				{
					if (base.NPC.direction == 0)
					{
						base.NPC.TargetClosest();
					}
					if (base.NPC.ai[2] == 0f)
					{
						base.NPC.ai[2] = base.NPC.direction;
					}
					base.NPC.velocity.Y *= 0.9f;
					base.NPC.velocity.X += base.NPC.ai[2] * 0.3f;
					if (base.NPC.velocity.X > 10f)
					{
						base.NPC.velocity.X = 10f;
					}
					if (base.NPC.velocity.X < -10f)
					{
						base.NPC.velocity.X = -10f;
					}
					float playerDistance2 = Main.player[base.NPC.target].Center.X - base.NPC.Center.X;
					if ((base.NPC.ai[2] < 0f && playerDistance2 > 300f) || (base.NPC.ai[2] > 0f && playerDistance2 < -300f))
					{
						base.NPC.ai[3] = -4f;
						base.NPC.ai[2] = 0f;
						return false;
					}
					if (Math.Abs(Main.player[base.NPC.target].Center.X - base.NPC.Center.X) > 800f)
					{
						base.NPC.ai[3] = -1f;
						base.NPC.ai[2] = 0f;
					}
					return false;
				}
				if (base.NPC.ai[3] == -4f)
				{
					base.NPC.ai[2]++;
					base.NPC.velocity.Y += 0.1f;
					if (((Vector2)(ref base.NPC.velocity)).Length() > 4f)
					{
						NPC nPC2 = base.NPC;
						nPC2.velocity *= 0.9f;
					}
					int tileAtCenterX = (int)base.NPC.Center.X / 16;
					int tileAtBottom = (int)(base.NPC.position.Y + (float)base.NPC.height + 12f) / 16;
					bool ableToRestart = false;
					for (int i = tileAtCenterX - 1; i <= tileAtCenterX + 1; i++)
					{
						if (Main.tile[i, tileAtBottom].HasTile && Main.tileSolid[Main.tile[i, tileAtBottom].TileType])
						{
							ableToRestart = true;
						}
					}
					if (ableToRestart && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
					{
						base.NPC.ai[3] = 0f;
						base.NPC.ai[2] = 0f;
					}
					else if (base.NPC.ai[2] > 300f || base.NPC.Center.Y > Main.player[base.NPC.target].Center.Y + 200f)
					{
						base.NPC.ai[3] = -1f;
						base.NPC.ai[2] = 0f;
					}
				}
				else
				{
					if (base.NPC.ai[3] == 1f)
					{
						Vector2 spawnPosiion = base.NPC.Center;
						spawnPosiion.Y -= 70f;
						base.NPC.velocity.X *= 0.8f;
						base.NPC.ai[2]++;
						if (base.NPC.ai[2] == (CalamityWorld.death ? 15f : 30f))
						{
							if (Main.netMode != 1)
							{
								NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)spawnPosiion.X, (int)spawnPosiion.Y + 18, 472);
							}
						}
						else if (base.NPC.ai[2] >= 90f)
						{
							base.NPC.ai[3] = -2f;
							base.NPC.ai[2] = 0f;
						}
						for (int j = 0; j < 2; j++)
						{
							Vector2 dustVelocity = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
							dustVelocity *= (float)Main.rand.Next(0, 100) * 0.1f;
							((Vector2)(ref dustVelocity)).Normalize();
							dustVelocity *= (float)Main.rand.Next(50, 90) * 0.1f;
							int dustIdx = Dust.NewDust(spawnPosiion, 1, 1, 27);
							Main.dust[dustIdx].velocity = -dustVelocity * 0.3f;
							Main.dust[dustIdx].alpha = 100;
							if (Main.rand.NextBool())
							{
								Main.dust[dustIdx].noGravity = true;
								Main.dust[dustIdx].scale += 0.3f;
							}
						}
						return false;
					}
					base.NPC.ai[2]++;
					int maxSkullCount = 10;
					if (base.NPC.velocity.Y == 0f && NPC.CountNPCS(472) < maxSkullCount)
					{
						if (base.NPC.ai[2] >= 180f)
						{
							base.NPC.ai[2] = 0f;
							base.NPC.ai[3] = 1f;
						}
					}
					else
					{
						if (NPC.CountNPCS(472) >= maxSkullCount)
						{
							base.NPC.ai[2]++;
						}
						if (base.NPC.ai[2] >= 360f)
						{
							base.NPC.ai[2] = 0f;
							base.NPC.ai[3] = -2f;
							base.NPC.velocity.Y -= 3f;
						}
					}
					if (base.NPC.target >= 0 && !Main.player[base.NPC.target].dead)
					{
						center = Main.player[base.NPC.target].Center - base.NPC.Center;
						if (((Vector2)(ref center)).Length() > 800f)
						{
							base.NPC.ai[3] = -1f;
							base.NPC.ai[2] = 0f;
						}
					}
				}
				if (Main.player[base.NPC.target].dead)
				{
					base.NPC.TargetClosest();
					if (Main.player[base.NPC.target].dead && base.NPC.timeLeft > 1)
					{
						base.NPC.timeLeft = 1;
					}
				}
			}
			if (npcType == 419)
			{
				base.NPC.reflectsProjectiles = false;
				base.NPC.takenDamageMultiplier = 1f;
				int chargeTime = 6;
				int yFlyTime = 10;
				float velocityMultiplier3 = 20f;
				if (base.NPC.ai[2] > 0f)
				{
					base.NPC.ai[2]--;
				}
				if (base.NPC.ai[2] == 0f)
				{
					if (((Main.player[base.NPC.target].Center.X < base.NPC.Center.X && base.NPC.direction < 0) || (Main.player[base.NPC.target].Center.X > base.NPC.Center.X && base.NPC.direction > 0)) && Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
					{
						base.NPC.ai[2] = -1f;
						base.NPC.netUpdate = true;
						base.NPC.TargetClosest();
					}
				}
				else
				{
					if (base.NPC.ai[2] < 0f && base.NPC.ai[2] > (float)chargeTime * -1f)
					{
						base.NPC.ai[2]--;
						base.NPC.velocity.X *= 0.9f;
						return false;
					}
					if (base.NPC.ai[2] == (float)chargeTime * -1f)
					{
						base.NPC.ai[2]--;
						base.NPC.TargetClosest();
						Vector2 vectorToPlayer = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Top - Vector2.UnitY * 30f, Vector2.Normalize(new Vector2((float)base.NPC.spriteDirection, -1f)));
						base.NPC.velocity = vectorToPlayer * velocityMultiplier3;
						base.NPC.netUpdate = true;
						return false;
					}
					if (base.NPC.ai[2] < (float)chargeTime * -1f)
					{
						base.NPC.ai[2]--;
						if (base.NPC.velocity.Y == 0f)
						{
							base.NPC.ai[2] = (CalamityWorld.death ? 60f : 90f);
						}
						else if (base.NPC.ai[2] < (float)chargeTime * -1f - (float)yFlyTime)
						{
							base.NPC.velocity.Y += 0.15f;
							if (base.NPC.velocity.Y > 24f)
							{
								base.NPC.velocity.Y = 24f;
							}
						}
						base.NPC.reflectsProjectiles = true;
						base.NPC.takenDamageMultiplier = (CalamityWorld.death ? 2f : 3f);
						if (base.NPC.justHit)
						{
							base.NPC.ai[2] = (CalamityWorld.death ? 60f : 90f);
							base.NPC.netUpdate = true;
						}
						return false;
					}
				}
			}
			if (npcType == 415)
			{
				int timeToReset = 42;
				int timeToBreathFire = 18;
				if (base.NPC.justHit)
				{
					base.NPC.ai[2] = (CalamityWorld.death ? 30f : 60f);
					base.NPC.netUpdate = true;
				}
				if (base.NPC.ai[2] > 0f)
				{
					base.NPC.ai[2]--;
				}
				if (base.NPC.ai[2] == 0f)
				{
					int solarFlareCount = 0;
					int maxFlareCount = 6;
					for (int k = 0; k < Main.maxNPCs; k++)
					{
						if (Main.npc[k].active && Main.npc[k].type == 516)
						{
							solarFlareCount++;
						}
					}
					if (solarFlareCount > maxFlareCount)
					{
						base.NPC.ai[2] = (CalamityWorld.death ? 30f : 60f);
					}
					else if (((Main.player[base.NPC.target].Center.X < base.NPC.Center.X && base.NPC.direction < 0) || (Main.player[base.NPC.target].Center.X > base.NPC.Center.X && base.NPC.direction > 0)) && Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
					{
						base.NPC.ai[2] = -1f;
						base.NPC.netUpdate = true;
						base.NPC.TargetClosest();
					}
				}
				else if (base.NPC.ai[2] < 0f && base.NPC.ai[2] > 0f - (float)timeToReset)
				{
					base.NPC.ai[2]--;
					if (base.NPC.ai[2] == 0f - (float)timeToReset)
					{
						base.NPC.ai[2] = 180 + 10 * Main.rand.Next(10);
					}
					base.NPC.velocity.X *= 0.8f;
					if (base.NPC.ai[2] == 0f - (float)timeToBreathFire || base.NPC.ai[2] == 0f - (float)timeToBreathFire - 8f || base.NPC.ai[2] == 0f - (float)timeToBreathFire - 16f)
					{
						for (int l = 0; l < 20; l++)
						{
							Vector2 spawnPosition2 = base.NPC.Center + Vector2.UnitX * (float)base.NPC.spriteDirection * 40f;
							Dust obj = Main.dust[Dust.NewDust(spawnPosition2, 0, 0, 259)];
							Vector2 velocity = Vector2.UnitY.RotatedByRandom(Math.PI * 2.0);
							obj.position = spawnPosition2 + velocity * 4f;
							obj.velocity = velocity * 2f + Vector2.UnitX * Main.rand.NextFloat() * (float)base.NPC.spriteDirection * 3f;
							obj.scale = 0.3f + velocity.X * (0f - (float)base.NPC.spriteDirection);
							obj.fadeIn = 0.7f;
							obj.noGravity = true;
						}
						if (base.NPC.velocity.X > -0.5f && base.NPC.velocity.X < 0.5f)
						{
							base.NPC.velocity.X = 0f;
						}
						if (Main.netMode != 1)
						{
							NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X + base.NPC.spriteDirection * 45, (int)base.NPC.Center.Y + 8, 516, 0, 0f, 0f, 0f, 0f, base.NPC.target);
						}
					}
					return false;
				}
			}
			if (npcType == 428 && NPC.CountNPCS(427) < 6)
			{
				base.NPC.localAI[0]++;
				if (base.NPC.localAI[0] >= (CalamityWorld.death ? 90f : (CalamityWorld.revenge ? 150f : 300f)))
				{
					int num = (int)base.NPC.Center.X / 16 - 1;
					int centerTileY = (int)base.NPC.Center.Y / 16 - 1;
					if (!Collision.SolidTiles(num, num + 2, centerTileY, centerTileY + 1) && Main.netMode != 1)
					{
						base.NPC.Transform(427);
						base.NPC.life = base.NPC.lifeMax;
						base.NPC.localAI[0] = 0f;
						return false;
					}
				}
				if (Utils.NextBool(consequent: (base.NPC.localAI[0] < (CalamityWorld.revenge ? 30f : 60f)) ? 16 : ((base.NPC.localAI[0] < (CalamityWorld.death ? 45f : (CalamityWorld.revenge ? 60f : 120f))) ? 8 : ((base.NPC.localAI[0] < (CalamityWorld.death ? 60f : (CalamityWorld.revenge ? 90f : 180f))) ? 4 : ((base.NPC.localAI[0] < (CalamityWorld.death ? 75f : (CalamityWorld.revenge ? 120f : 240f))) ? 2 : ((!(base.NPC.localAI[0] < (CalamityWorld.death ? 90f : (CalamityWorld.revenge ? 150f : 300f)))) ? 1 : 1)))), r: Main.rand))
				{
					Dust dust = Main.dust[Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 229)];
					dust.noGravity = true;
					dust.scale = 1f;
					dust.noLight = true;
					dust.velocity = base.NPC.DirectionFrom(dust.position) * ((Vector2)(ref dust.velocity)).Length();
					dust.position -= dust.velocity * 5f;
					dust.position.X += base.NPC.direction * 6;
					dust.position.Y += 4f;
				}
			}
			if (npcType == 427 && NPC.CountNPCS(426) < 3)
			{
				base.NPC.localAI[0]++;
				base.NPC.localAI[0] += Math.Abs(base.NPC.velocity.X) / 2f;
				if (base.NPC.localAI[0] >= (CalamityWorld.death ? 300f : (CalamityWorld.revenge ? 600f : 1200f)) && Main.netMode != 1)
				{
					int num2 = (int)base.NPC.Center.X / 16 - 2;
					int centerTileY2 = (int)base.NPC.Center.Y / 16 - 3;
					if (!Collision.SolidTiles(num2, num2 + 4, centerTileY2, centerTileY2 + 4))
					{
						base.NPC.Transform(426);
						base.NPC.life = base.NPC.lifeMax;
						base.NPC.localAI[0] = 0f;
						return false;
					}
				}
				if (Utils.NextBool(consequent: (base.NPC.localAI[0] < (CalamityWorld.death ? 60f : (CalamityWorld.revenge ? 120f : 240f))) ? 32 : ((base.NPC.localAI[0] < (CalamityWorld.death ? 120f : (CalamityWorld.revenge ? 240f : 480f))) ? 16 : ((base.NPC.localAI[0] < (CalamityWorld.death ? 180f : (CalamityWorld.revenge ? 360f : 720f))) ? 6 : ((base.NPC.localAI[0] < (CalamityWorld.death ? 240f : (CalamityWorld.revenge ? 480f : 960f))) ? 2 : ((!(base.NPC.localAI[0] < (CalamityWorld.death ? 300f : (CalamityWorld.revenge ? 600f : 1200f)))) ? 1 : 1)))), r: Main.rand))
				{
					Dust obj2 = Main.dust[Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 229)];
					obj2.noGravity = true;
					obj2.scale = 1f;
					obj2.noLight = true;
				}
			}
			bool jump = false;
			if (base.NPC.velocity.X == 0f)
			{
				jump = true;
			}
			if (base.NPC.justHit)
			{
				jump = false;
			}
			if (Main.netMode != 1 && npcType == 198 && (double)base.NPC.life <= (double)base.NPC.lifeMax * 0.9)
			{
				base.NPC.Transform(199);
			}
			if (Main.netMode != 1 && npcType == 348 && (double)base.NPC.life <= (double)base.NPC.lifeMax * 0.9)
			{
				base.NPC.Transform(349);
			}
			int aiGateValue = 60;
			if (npcType == 120 || npcType == ModContent.NPCType<RenegadeWarlock>())
			{
				aiGateValue = 180;
				if (base.NPC.ai[3] == -30f)
				{
					NPC nPC3 = base.NPC;
					nPC3.velocity *= 0f;
					base.NPC.ai[3] = 0f;
					SoundEngine.PlaySound(in SoundID.Item8, base.NPC.Center);
					float distX = base.NPC.oldPos[2].X + (float)base.NPC.width * 0.5f - base.NPC.Center.X;
					float distY = base.NPC.oldPos[2].Y + (float)base.NPC.height * 0.5f - base.NPC.Center.Y;
					float distance = (float)Math.Sqrt(distX * distX + distY * distY);
					distance = 2f / distance;
					distX *= distance;
					distY *= distance;
					for (int m = 0; m < 20; m++)
					{
						int dustIdx2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 71, distX, distY, 200, default(Color), 2f);
						Main.dust[dustIdx2].noGravity = true;
						Main.dust[dustIdx2].velocity.X *= 2f;
					}
					for (int n = 0; n < 20; n++)
					{
						int dustIdx3 = Dust.NewDust(base.NPC.oldPos[2], base.NPC.width, base.NPC.height, 71, 0f - distX, 0f - distY, 200, default(Color), 2f);
						Main.dust[dustIdx3].noGravity = true;
						Main.dust[dustIdx3].velocity.X *= 2f;
					}
				}
			}
			bool canIncrementAI3 = false;
			bool reset = true;
			if (npcType == 343 || npcType == 47 || npcType == 67 || npcType == 109 || npcType == 110 || npcType == 111 || npcType == 120 || npcType == ModContent.NPCType<RenegadeWarlock>() || npcType == 163 || npcType == 164 || npcType == 239 || npcType == 168 || npcType == 199 || npcType == 206 || npcType == 214 || npcType == 215 || npcType == 216 || npcType == 217 || npcType == 218 || npcType == 219 || npcType == 220 || npcType == 226 || npcType == 243 || npcType == 251 || npcType == 257 || npcType == 258 || npcType == 290 || npcType == 291 || npcType == 292 || npcType == 293 || npcType == 305 || npcType == 306 || npcType == 307 || npcType == 308 || npcType == 309 || npcType == 348 || npcType == 349 || npcType == 350 || npcType == 351 || npcType == 379 || (npcType >= 430 && npcType <= 436) || npcType == 380 || npcType == 381 || npcType == 382 || npcType == 383 || npcType == 386 || npcType == 391 || (npcType >= 449 && npcType <= 452) || npcType == 466 || npcType == 464 || npcType == 166 || npcType == 469 || npcType == 468 || npcType == 471 || npcType == 470 || npcType == 480 || npcType == 481 || npcType == 482 || npcType == 411 || npcType == 424 || npcType == 409 || (npcType >= 494 && npcType <= 506) || npcType == 425 || npcType == 427 || npcType == 426 || npcType == 428 || npcType == 580 || npcType == 415 || npcType == 419 || npcType == 520 || (npcType >= 524 && npcType <= 527) || npcType == 528 || npcType == 529 || npcType == 530 || npcType == 532)
			{
				reset = false;
			}
			bool ableToAlterAI3 = false;
			if (npcType == 425 || npcType == 471)
			{
				ableToAlterAI3 = true;
			}
			bool npcTimer = base.NPC.ai[2] <= 0f;
			if (npcType <= 382)
			{
				switch (npcType)
				{
				}
			}
			else if (npcType <= 424)
			{
				switch (npcType)
				{
				}
			}
			else if (npcType <= 466)
			{
				switch (npcType)
				{
				}
			}
			else if (npcType - 498 > 8)
			{
				_ = 520;
			}
			if (!ableToAlterAI3 & npcTimer)
			{
				if (base.NPC.velocity.Y == 0f && ((base.NPC.velocity.X > 0f && base.NPC.direction < 0) || (base.NPC.velocity.X < 0f && base.NPC.direction > 0)))
				{
					canIncrementAI3 = true;
				}
				if ((base.NPC.position.X == base.NPC.oldPosition.X || base.NPC.ai[3] >= (float)aiGateValue) | canIncrementAI3)
				{
					base.NPC.ai[3]++;
				}
				else if ((double)Math.Abs(base.NPC.velocity.X) > 0.9 && base.NPC.ai[3] > 0f)
				{
					base.NPC.ai[3]--;
				}
				if (base.NPC.ai[3] > (float)(aiGateValue * 10))
				{
					base.NPC.ai[3] = 0f;
				}
				if (base.NPC.justHit)
				{
					base.NPC.ai[3] = 0f;
				}
				if (base.NPC.ai[3] == (float)aiGateValue)
				{
					base.NPC.netUpdate = true;
				}
			}
			if (npcType == 463 && Main.netMode != 1)
			{
				if (base.NPC.localAI[3] > 0f)
				{
					base.NPC.localAI[3]--;
				}
				if (base.NPC.justHit && base.NPC.localAI[3] <= 0f)
				{
					base.NPC.localAI[3] = (CalamityWorld.death ? 45f : (CalamityWorld.revenge ? 60f : 75f));
					float nailVelocity = (CalamityWorld.death ? 12f : (CalamityWorld.revenge ? 10f : 8f));
					int type = 498;
					int damage = (int)((double)base.NPC.damage * 0.15);
					Vector2 destination = new Vector2(base.NPC.Center.X, base.NPC.Center.Y - 100f) - base.NPC.Center;
					destination = destination.SafeNormalize(-Vector2.UnitY);
					destination *= nailVelocity;
					int numProj = Main.rand.Next(3, 6);
					float rotation = MathHelper.ToRadians((float)(numProj * 15));
					for (int num3 = 0; num3 < numProj; num3++)
					{
						Vector2 spinningpoint = destination;
						double radians = MathHelper.Lerp(0f - rotation, rotation, (float)num3 / (float)(numProj - 1));
						center = default(Vector2);
						Vector2 perturbedSpeed = spinningpoint.RotatedBy(radians, center);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center - Vector2.UnitY * (float)(base.NPC.width / 4), perturbedSpeed, type, damage, 1f, 255);
					}
				}
			}
			if (npcType == 460)
			{
				if (base.NPC.velocity.Y < -0.3f || base.NPC.velocity.Y > 0.3f)
				{
					base.NPC.knockBackResist = 0f;
				}
				else
				{
					base.NPC.knockBackResist = 0.1f;
				}
			}
			if (npcType == 469)
			{
				base.NPC.knockBackResist = 0.25f;
				if (base.NPC.ai[2] == 1f)
				{
					base.NPC.knockBackResist = 0f;
				}
				bool spiderAI = false;
				int centerTileX = (int)base.NPC.Center.X / 16;
				int centerTileY3 = (int)base.NPC.Center.Y / 16;
				for (int num4 = centerTileX - 1; num4 <= centerTileX + 1; num4++)
				{
					for (int y = centerTileY3 - 1; y <= centerTileY3 + 1; y++)
					{
						if (Main.tile[num4, y] != null && Main.tile[num4, y].WallType > 0)
						{
							spiderAI = true;
							break;
						}
					}
					if (spiderAI)
					{
						break;
					}
				}
				if ((base.NPC.ai[2] == 0f) & spiderAI)
				{
					if (base.NPC.velocity.Y == 0f)
					{
						base.NPC.velocity.Y = -4.6f;
						base.NPC.velocity.X *= 1.5f;
					}
					else if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.ai[2] = 1f;
					}
				}
				if (spiderAI && base.NPC.ai[2] == 1f && Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					Vector2 distanceVector = Main.player[base.NPC.target].Center - base.NPC.Center;
					float distanceMagnitude = ((Vector2)(ref distanceVector)).Length();
					distanceVector = distanceVector.SafeNormalize(-Vector2.UnitY);
					distanceVector *= 6f + distanceMagnitude / 300f;
					base.NPC.velocity = (base.NPC.velocity * 29f + distanceVector) / 30f;
					base.NPC.noGravity = true;
					base.NPC.ai[2] = 1f;
					return false;
				}
				base.NPC.noGravity = false;
				base.NPC.ai[2] = 0f;
			}
			if (npcType == 462 && base.NPC.velocity.Y == 0f)
			{
				center = Main.player[base.NPC.target].Center - base.NPC.Center;
				if (((Vector2)(ref center)).Length() < 150f && Math.Abs(base.NPC.velocity.X) > 3f && ((base.NPC.velocity.X < 0f && base.NPC.Center.X > Main.player[base.NPC.target].Center.X) || (base.NPC.velocity.X > 0f && base.NPC.Center.X < Main.player[base.NPC.target].Center.X)))
				{
					base.NPC.velocity.X *= 2f;
					base.NPC.velocity.Y -= 4.5f;
					if (base.NPC.Center.Y - Main.player[base.NPC.target].Center.Y > 20f)
					{
						base.NPC.velocity.Y -= 0.5f;
					}
					if (base.NPC.Center.Y - Main.player[base.NPC.target].Center.Y > 40f)
					{
						base.NPC.velocity.Y--;
					}
					if (base.NPC.Center.Y - Main.player[base.NPC.target].Center.Y > 80f)
					{
						base.NPC.velocity.Y -= 1.5f;
					}
					if (base.NPC.Center.Y - Main.player[base.NPC.target].Center.Y > 100f)
					{
						base.NPC.velocity.Y -= 1.5f;
					}
					if (Math.Abs(base.NPC.velocity.X) > 9f)
					{
						if (base.NPC.velocity.X < 0f)
						{
							base.NPC.velocity.X = -9f;
						}
						else
						{
							base.NPC.velocity.X = 9f;
						}
					}
				}
			}
			if (base.NPC.ai[3] < (float)aiGateValue && (Main.eclipse || !Main.dayTime || (double)base.NPC.position.Y > Main.worldSurface * 16.0 || (Main.invasionType == 1 && (npcType == 343 || npcType == 350)) || (Main.invasionType == 1 && (npcType == 26 || npcType == 27 || npcType == 28 || npcType == 111 || npcType == 471)) || npcType == 73 || (Main.invasionType == 3 && npcType >= 212 && npcType <= 216) || (Main.invasionType == 4 && (npcType == 381 || npcType == 382 || npcType == 383 || npcType == 385 || npcType == 386 || npcType == 389 || npcType == 391 || npcType == 520)) || npcType == 31 || npcType == 294 || npcType == 295 || npcType == 296 || npcType == 47 || npcType == 67 || npcType == 77 || npcType == 78 || npcType == 79 || npcType == 80 || npcType == 110 || npcType == 120 || npcType == ModContent.NPCType<RenegadeWarlock>() || npcType == 168 || npcType == 181 || npcType == 185 || npcType == 198 || npcType == 199 || npcType == 206 || npcType == 217 || npcType == 218 || npcType == 219 || npcType == 220 || npcType == 239 || npcType == 243 || npcType == 254 || npcType == 255 || npcType == 257 || npcType == 258 || npcType == 291 || npcType == 292 || npcType == 293 || npcType == 379 || npcType == 380 || npcType == 464 || npcType == 470 || npcType == 424 || (npcType == 411 && (base.NPC.ai[1] >= 180f || base.NPC.ai[1] < 90f)) || npcType == 409 || npcType == 425 || npcType == 429 || npcType == 427 || npcType == 428 || npcType == 580 || npcType == 415 || npcType == 419 || (npcType >= 524 && npcType <= 527) || npcType == 528 || npcType == 529 || npcType == 530 || npcType == 532))
			{
				if ((npcType == 3 || npcType == 331 || npcType == 332 || npcType == 21 || (npcType >= 449 && npcType <= 452) || npcType == 31 || npcType == 294 || npcType == 296 || npcType == 295 || npcType == 77 || npcType == 110 || npcType == 132 || npcType == 167 || npcType == 161 || npcType == 162 || npcType == 186 || npcType == 187 || npcType == 188 || npcType == 189 || npcType == 197 || npcType == 200 || npcType == 201 || npcType == 202 || npcType == 203 || npcType == 223 || npcType == 291 || npcType == 292 || npcType == 293 || npcType == 320 || npcType == 321 || npcType == 319 || npcType == 481 || npcType == ModContent.NPCType<BucketZombie>()) && Main.rand.NextBool(1000))
				{
					SoundEngine.PlaySound(in SoundID.ZombieMoan, base.NPC.Center);
				}
				if (npcType == 489 && Main.rand.NextBool(800))
				{
					SoundEngine.PlaySound(in SoundID.ZombieMoan, base.NPC.Center);
				}
				if ((npcType == 78 || npcType == 79 || npcType == 80) && Main.rand.NextBool(500))
				{
					SoundEngine.PlaySound(in SoundID.Mummy, base.NPC.Center);
				}
				if (npcType == 159 && Main.rand.NextBool(500))
				{
					SoundEngine.PlaySound(in SoundID.Zombie7, base.NPC.Center);
				}
				if (npcType == 162 && Main.rand.NextBool(500))
				{
					SoundEngine.PlaySound(in SoundID.Zombie6, base.NPC.Center);
				}
				if (npcType == 181 && Main.rand.NextBool(500))
				{
					SoundEngine.PlaySound(in SoundID.Zombie8, base.NPC.Center);
				}
				if (npcType >= 269 && npcType <= 280 && Main.rand.NextBool(1000))
				{
					SoundEngine.PlaySound(in SoundID.ZombieMoan, base.NPC.Center);
				}
				base.NPC.TargetClosest();
			}
			else if (base.NPC.ai[2] <= 0f || (npcType != 110 && npcType != 111 && npcType != 206 && npcType != 214 && npcType != 215 && npcType != 216 && npcType != 291 && npcType != 292 && npcType != 293 && npcType != 350 && npcType != 381 && npcType != 382 && npcType != 383 && npcType != 385 && npcType != 386 && npcType != 389 && npcType != 391 && npcType != 469 && npcType != 166 && npcType != 466 && npcType != 471 && npcType != 411 && npcType != 410 && npcType != 424 && npcType != 425 && npcType != 426 && npcType != 415 && npcType != 419 && npcType != 520))
			{
				if (Main.dayTime && (double)(base.NPC.position.Y / 16f) < Main.worldSurface && base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
				}
				if (base.NPC.velocity.X == 0f)
				{
					if (base.NPC.velocity.Y == 0f)
					{
						base.NPC.ai[0]++;
						if (base.NPC.ai[0] >= 2f)
						{
							base.NPC.direction *= -1;
							base.NPC.spriteDirection = base.NPC.direction;
							base.NPC.ai[0] = 0f;
						}
					}
				}
				else
				{
					base.NPC.ai[0] = 0f;
				}
				if (base.NPC.direction == 0)
				{
					base.NPC.direction = 1;
				}
			}
			if (npcType == 159 || npcType == 349)
			{
				if (npcType == 159 && ((base.NPC.velocity.X > 0f && base.NPC.direction < 0) || (base.NPC.velocity.X < 0f && base.NPC.direction > 0)))
				{
					base.NPC.velocity.X *= 0.95f;
				}
				if (base.NPC.velocity.X < -8f || base.NPC.velocity.X > 8f)
				{
					if (base.NPC.velocity.Y == 0f)
					{
						NPC nPC4 = base.NPC;
						nPC4.velocity *= 0.8f;
					}
				}
				else if (base.NPC.velocity.X < 8f && base.NPC.direction == 1)
				{
					if (base.NPC.velocity.Y == 0f && base.NPC.velocity.X < 0f)
					{
						base.NPC.velocity.X *= 0.99f;
					}
					base.NPC.velocity.X += 0.09f;
					if (base.NPC.velocity.X > 8f)
					{
						base.NPC.velocity.X = 8f;
					}
				}
				else if (base.NPC.velocity.X > -8f && base.NPC.direction == -1)
				{
					if (base.NPC.velocity.Y == 0f && base.NPC.velocity.X > 0f)
					{
						base.NPC.velocity.X *= 0.99f;
					}
					base.NPC.velocity.X -= 0.09f;
					if (base.NPC.velocity.X < -8f)
					{
						base.NPC.velocity.X = -8f;
					}
				}
			}
			else if (npcType == 199)
			{
				FighterRunningAI(base.NPC, CalamityWorld.death ? 8f : 6f, 0.12f, 0.8f, extraDeceleration: true, 0.8f);
			}
			else if (npcType == 120 || npcType == ModContent.NPCType<RenegadeWarlock>() || npcType == 166 || npcType == 213 || npcType == 258 || npcType == 528 || npcType == 529)
			{
				FighterRunningAI(base.NPC, CalamityWorld.death ? 6f : 4f, 0.09f, 0.8f, extraDeceleration: true, 0.8f);
			}
			else if (npcType == 461 || npcType == 27 || npcType == 77 || npcType == 104 || npcType == 163 || npcType == 162 || npcType == 196 || npcType == 197 || npcType == 212 || npcType == 257 || npcType == 326 || npcType == 343 || npcType == 348 || npcType == 351 || (npcType >= 524 && npcType <= 527) || npcType == 530)
			{
				FighterRunningAI(base.NPC, CalamityWorld.death ? 3f : 2.5f, 0.09f, 0.8f);
			}
			else
			{
				switch (npcType)
				{
				case 109:
					FighterRunningAI(base.NPC, CalamityWorld.death ? 5f : 4f, 0.06f, 0.8f);
					break;
				case 21:
				case 26:
				case 31:
				case 47:
				case 73:
				case 140:
				case 164:
				case 167:
				case 168:
				case 181:
				case 185:
				case 198:
				case 201:
				case 202:
				case 203:
				case 217:
				case 218:
				case 219:
				case 226:
				case 239:
				case 254:
				case 294:
				case 295:
				case 296:
				case 338:
				case 339:
				case 340:
				case 342:
				case 385:
				case 389:
				case 425:
				case 429:
				case 462:
				case 463:
				case 464:
				case 466:
				case 469:
				case 470:
				case 480:
				case 482:
				case 635:
				{
					float maxVelocity5 = 1.5f;
					switch (npcType)
					{
					case 294:
						maxVelocity5 = 2f;
						break;
					case 295:
						maxVelocity5 = 1.75f;
						break;
					case 296:
						maxVelocity5 = 1.25f;
						break;
					case 201:
						maxVelocity5 = 1.1f;
						break;
					case 202:
						maxVelocity5 = 0.9f;
						break;
					case 203:
						maxVelocity5 = 1.2f;
						break;
					case 338:
						maxVelocity5 = 1.75f;
						break;
					case 339:
						maxVelocity5 = 1.25f;
						break;
					case 340:
						maxVelocity5 = 2f;
						break;
					case 385:
						maxVelocity5 = 1.8f;
						break;
					case 389:
						maxVelocity5 = 2.25f;
						break;
					case 462:
						maxVelocity5 = 4f;
						break;
					case 463:
						maxVelocity5 = (CalamityWorld.revenge ? 0.75f : 0.6f);
						break;
					case 466:
						maxVelocity5 = 3.75f;
						break;
					case 469:
						maxVelocity5 = 3.25f;
						break;
					case 480:
						maxVelocity5 = 1.5f + (1f - (float)base.NPC.life / (float)base.NPC.lifeMax) * 2f;
						break;
					case 425:
						maxVelocity5 = (CalamityWorld.revenge ? 6f : 4.8f);
						break;
					case 429:
						maxVelocity5 = 4f;
						break;
					}
					if (npcType == 21 || npcType == 201 || npcType == 202 || npcType == 203 || npcType == 342)
					{
						maxVelocity5 *= 1f + (1f - base.NPC.scale);
					}
					maxVelocity5 *= 1.25f;
					if (CalamityWorld.death)
					{
						maxVelocity5 *= 1.25f;
					}
					bool extraSlowdown = base.NPC.velocity.Y == 0f && npcType == 462 && ((base.NPC.direction > 0 && base.NPC.velocity.X < 0f) || (base.NPC.direction < 0 && base.NPC.velocity.X > 0f));
					FighterRunningAI(base.NPC, maxVelocity5, 0.09f, 0.9f, extraSlowdown, 0.9f);
					break;
				}
				case 269:
				case 270:
				case 271:
				case 272:
				case 273:
				case 274:
				case 275:
				case 276:
				case 277:
				case 278:
				case 279:
				case 280:
				{
					float maxVelocity6 = 1.5f;
					if (npcType == 269)
					{
						maxVelocity6 = 2f;
					}
					if (npcType == 270)
					{
						maxVelocity6 = 1f;
					}
					if (npcType == 271)
					{
						maxVelocity6 = 1.5f;
					}
					if (npcType == 272)
					{
						maxVelocity6 = 3f;
					}
					if (npcType == 273)
					{
						maxVelocity6 = 1.25f;
					}
					if (npcType == 274)
					{
						maxVelocity6 = 3f;
					}
					if (npcType == 275)
					{
						maxVelocity6 = 3.25f;
					}
					if (npcType == 276)
					{
						maxVelocity6 = 2f;
					}
					if (npcType == 277)
					{
						maxVelocity6 = 2.75f;
					}
					if (npcType == 278)
					{
						maxVelocity6 = 1.8f;
					}
					if (npcType == 279)
					{
						maxVelocity6 = 1.3f;
					}
					if (npcType == 280)
					{
						maxVelocity6 = 2.5f;
					}
					maxVelocity6 *= 1f + (1f - base.NPC.scale);
					maxVelocity6 *= 1.25f;
					if (CalamityWorld.death)
					{
						maxVelocity6 *= 1.25f;
					}
					FighterRunningAI(base.NPC, maxVelocity6, 0.09f, 0.8f);
					break;
				}
				default:
					if (npcType >= 305 && npcType <= 314)
					{
						float maxVelocity = 1.5f;
						if (npcType == 305 || npcType == 310)
						{
							maxVelocity = 2f;
						}
						if (npcType == 306 || npcType == 311)
						{
							maxVelocity = 1.25f;
						}
						if (npcType == 307 || npcType == 312)
						{
							maxVelocity = 2.25f;
						}
						if (npcType == 308 || npcType == 313)
						{
							maxVelocity = 1.5f;
						}
						if (npcType == 309 || npcType == 314)
						{
							maxVelocity = 1f;
						}
						maxVelocity *= 1.25f;
						if (CalamityWorld.death)
						{
							maxVelocity *= 1.25f;
						}
						if (npcType < 310)
						{
							if (base.NPC.velocity.Y == 0f)
							{
								base.NPC.velocity.X *= 0.85f;
								if (base.NPC.velocity.X > -0.3f && base.NPC.velocity.X < 0.3f)
								{
									base.NPC.velocity.Y = -9f;
									base.NPC.velocity.X = maxVelocity * (float)base.NPC.direction;
								}
							}
							else if (base.NPC.spriteDirection == base.NPC.direction)
							{
								base.NPC.velocity.X = (base.NPC.velocity.X * 10f + maxVelocity * (float)base.NPC.direction) / 11f;
							}
						}
						else
						{
							FighterRunningAI(base.NPC, maxVelocity, 0.09f, 0.8f);
						}
						break;
					}
					if (npcType == 67 || npcType == 220 || npcType == 428)
					{
						FighterRunningAI(base.NPC, CalamityWorld.death ? 4f : (CalamityWorld.revenge ? 1f : 0.5f), CalamityWorld.revenge ? 0.06f : 0.05f, 0.7f);
						break;
					}
					if (npcType == 78 || npcType == 79 || npcType == 80)
					{
						float maxVelocity2 = 3f;
						float acceleration = 0.15f;
						if (npcType == 79)
						{
							maxVelocity2 *= 1.5f;
						}
						if (CalamityWorld.death)
						{
							maxVelocity2 *= 1.25f;
						}
						FighterRunningAI(base.NPC, maxVelocity2, acceleration, 0.7f);
						break;
					}
					if (npcType == 287)
					{
						FighterRunningAI(base.NPC, CalamityWorld.death ? 7f : (CalamityWorld.revenge ? 6f : 5f), CalamityWorld.revenge ? 0.3f : 0.2f, 0.7f);
						break;
					}
					if (npcType == 243)
					{
						FighterRunningAI(base.NPC, CalamityWorld.death ? 4.5f : (CalamityWorld.revenge ? 3f : 2f), CalamityWorld.revenge ? 0.3f : 0.2f, 0.7f);
						break;
					}
					if (npcType == 251)
					{
						FighterRunningAI(base.NPC, CalamityWorld.death ? 5f : (CalamityWorld.revenge ? 3.5f : 2.5f), CalamityWorld.revenge ? 0.3f : 0.2f, 0.8f);
						break;
					}
					if (npcType == 386)
					{
						if (base.NPC.ai[2] > 0f)
						{
							if (base.NPC.velocity.Y == 0f)
							{
								base.NPC.velocity.X *= 0.8f;
							}
						}
						else
						{
							FighterRunningAI(base.NPC, CalamityWorld.death ? 5f : 3.5f, 0.2f, 0.8f);
						}
						break;
					}
					if (npcType == 460)
					{
						float acceleration2 = 0.2f;
						if (Math.Abs(base.NPC.velocity.X) > 2f)
						{
							acceleration2 *= 0.8f;
						}
						if ((double)Math.Abs(base.NPC.velocity.X) > 2.5)
						{
							acceleration2 *= 0.8f;
						}
						if (Math.Abs(base.NPC.velocity.X) > 3f)
						{
							acceleration2 *= 0.8f;
						}
						if ((double)Math.Abs(base.NPC.velocity.X) > 3.5)
						{
							acceleration2 *= 0.8f;
						}
						if (Math.Abs(base.NPC.velocity.X) > 4f)
						{
							acceleration2 *= 0.8f;
						}
						if (Math.Abs(base.NPC.velocity.X) > 4.5f)
						{
							acceleration2 *= 0.8f;
						}
						if (Math.Abs(base.NPC.velocity.X) > 5f)
						{
							acceleration2 *= 0.8f;
						}
						if ((double)Math.Abs(base.NPC.velocity.X) > 5.5)
						{
							acceleration2 *= 0.8f;
						}
						FighterRunningAI(base.NPC, CalamityWorld.death ? 10f : 7f, acceleration2, 0.8f);
						break;
					}
					if (npcType == 508 || npcType == 580 || npcType == 582)
					{
						float xAdditive = (CalamityWorld.death ? 3.5f : 3f);
						float turnValue2 = 90f;
						float absoluteVelocityX = Math.Abs(base.NPC.velocity.X);
						if (absoluteVelocityX > 2.75f)
						{
							xAdditive = (CalamityWorld.death ? 7f : 6f);
							turnValue2 += 100f;
						}
						else if ((double)absoluteVelocityX > 2.25)
						{
							xAdditive = (CalamityWorld.death ? 5f : 4.25f);
							turnValue2 += 80f;
						}
						if ((double)Math.Abs(base.NPC.velocity.Y) < 0.5)
						{
							if (base.NPC.velocity.X > 0f && base.NPC.direction < 0)
							{
								NPC nPC5 = base.NPC;
								nPC5.velocity *= 0.9f;
							}
							if (base.NPC.velocity.X < 0f && base.NPC.direction > 0)
							{
								NPC nPC6 = base.NPC;
								nPC6.velocity *= 0.9f;
							}
						}
						if (Math.Abs(base.NPC.velocity.Y) > 0.3f)
						{
							turnValue2 *= 3f;
						}
						if (base.NPC.velocity.X <= 0f && base.NPC.direction < 0)
						{
							base.NPC.velocity.X = (base.NPC.velocity.X * turnValue2 - xAdditive) / (turnValue2 + 1f);
						}
						else if (base.NPC.velocity.X >= 0f && base.NPC.direction > 0)
						{
							base.NPC.velocity.X = (base.NPC.velocity.X * turnValue2 + xAdditive) / (turnValue2 + 1f);
						}
						else if (Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) > 20f && Math.Abs(base.NPC.velocity.Y) <= 0.3f)
						{
							base.NPC.velocity.X *= 0.99f;
							base.NPC.velocity.X += (float)base.NPC.direction * 0.025f;
						}
						break;
					}
					if (npcType == 391 || npcType == 427 || npcType == 415 || npcType == 419 || npcType == 518 || npcType == 532)
					{
						float maxVelocity3 = 5f;
						float acceleration3 = 0.25f;
						float turnMultiplier = 0.7f;
						switch (npcType)
						{
						case 427:
							maxVelocity3 = 6f;
							acceleration3 = 0.2f;
							turnMultiplier = 0.8f;
							break;
						case 415:
							maxVelocity3 = 4f;
							acceleration3 = 0.1f;
							turnMultiplier = 0.95f;
							break;
						case 419:
							maxVelocity3 = 6f;
							acceleration3 = 0.15f;
							turnMultiplier = 0.85f;
							break;
						case 518:
							maxVelocity3 = 5f;
							acceleration3 = 0.1f;
							turnMultiplier = 0.95f;
							break;
						case 532:
							maxVelocity3 = 5f;
							acceleration3 = 0.15f;
							turnMultiplier = 0.98f;
							break;
						}
						if (CalamityWorld.revenge)
						{
							maxVelocity3 *= 1.25f;
							acceleration3 *= 1.25f;
						}
						if (CalamityWorld.death)
						{
							maxVelocity3 *= 1.25f;
							acceleration3 *= 1.25f;
						}
						FighterRunningAI(base.NPC, maxVelocity3, acceleration3, turnMultiplier);
						break;
					}
					if ((npcType >= 430 && npcType <= 436) || npcType == 494 || npcType == 495)
					{
						if (base.NPC.ai[2] == 0f)
						{
							base.NPC.damage = 0;
							float maxVelocity4 = (CalamityWorld.death ? 2.5f : 1.5f);
							maxVelocity4 *= 1f + (1f - base.NPC.scale);
							FighterRunningAI(base.NPC, maxVelocity4, 0.09f, 0.8f, extraDeceleration: true, 0.8f);
							if (base.NPC.velocity.Y == 0f && (!Main.dayTime || (double)base.NPC.position.Y > Main.worldSurface * 16.0) && !Main.player[base.NPC.target].dead)
							{
								Vector2 playerDistance3 = base.NPC.Center - Main.player[base.NPC.target].Center;
								int slowdownDistance = 50;
								if (npcType >= 494 && npcType <= 495)
								{
									slowdownDistance = 42;
								}
								if (((Vector2)(ref playerDistance3)).Length() < (float)slowdownDistance && Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
								{
									base.NPC.velocity.X *= 0.7f;
									base.NPC.ai[2] = 1f;
								}
							}
						}
						else
						{
							base.NPC.damage = (int)Math.Round((double)base.NPC.defDamage * 1.4);
							base.NPC.ai[3] = 1f;
							base.NPC.velocity.X *= 0.9f;
							if (Math.Abs(base.NPC.velocity.X) < 0.1f)
							{
								base.NPC.velocity.X = 0f;
							}
							base.NPC.ai[2]++;
							if (base.NPC.ai[2] >= 20f || base.NPC.velocity.Y != 0f || (Main.dayTime && (double)base.NPC.position.Y < Main.worldSurface * 16.0))
							{
								base.NPC.ai[2] = 0f;
							}
						}
						break;
					}
					switch (npcType)
					{
					default:
						switch (npcType)
						{
						default:
						{
							if (npcType == 424 || npcType == 429 || npcType == 520)
							{
								break;
							}
							float velocityMax = 1f;
							if (npcType == 186)
							{
								velocityMax = 1.1f;
							}
							if (npcType == 187)
							{
								velocityMax = 0.9f;
							}
							if (npcType == 188)
							{
								velocityMax = 1.2f;
							}
							if (npcType == 189)
							{
								velocityMax = 0.8f;
							}
							if (npcType == 132)
							{
								velocityMax = 0.95f;
							}
							if (npcType == 200)
							{
								velocityMax = 0.87f;
							}
							if (npcType == 223)
							{
								velocityMax = 1.05f;
							}
							if (npcType == ModContent.NPCType<BucketZombie>())
							{
								velocityMax = 0.85f;
							}
							if (npcType == 489)
							{
								center = Main.player[base.NPC.target].Center - base.NPC.Center;
								float playerDistance4 = ((Vector2)(ref center)).Length();
								playerDistance4 *= 0.0025f;
								if ((double)playerDistance4 > 1.5)
								{
									playerDistance4 = 1.5f;
								}
								velocityMax = ((!Main.expertMode) ? (2.5f - playerDistance4) : (3f - playerDistance4));
								velocityMax *= 0.8f;
							}
							if (npcType == 489 || npcType == 3 || npcType == 132 || npcType == 186 || npcType == 187 || npcType == 188 || npcType == 189 || npcType == 200 || npcType == 223 || npcType == 331 || npcType == 332 || npcType == ModContent.NPCType<BucketZombie>())
							{
								velocityMax *= 1f + (1f - base.NPC.scale);
							}
							if (CalamityWorld.revenge)
							{
								velocityMax *= 1.25f;
							}
							if (CalamityWorld.death)
							{
								velocityMax *= 1.25f;
							}
							FighterRunningAI(base.NPC, velocityMax, 0.09f, 0.8f, extraDeceleration: true, 0.8f);
							break;
						}
						case 410:
						case 411:
						case 468:
						case 481:
						case 498:
						case 499:
						case 500:
						case 501:
						case 502:
						case 503:
						case 504:
						case 505:
						case 506:
							break;
						}
						break;
					case 110:
					case 111:
					case 206:
					case 214:
					case 215:
					case 216:
					case 290:
					case 291:
					case 292:
					case 293:
					case 350:
					case 379:
					case 380:
					case 381:
					case 382:
					case 449:
					case 450:
					case 451:
					case 452:
						break;
					}
					break;
				}
			}
			if (npcType >= 277 && npcType <= 280)
			{
				Lighting.AddLight((int)base.NPC.Center.X / 16, (int)base.NPC.Center.Y / 16, 0.2f, 0.1f, 0f);
			}
			else
			{
				switch (npcType)
				{
				case 520:
					Lighting.AddLight(base.NPC.Top + new Vector2(0f, 20f), 0.3f, 0.3f, 0.7f);
					break;
				case 525:
				{
					Vector3 rgb5 = new Vector3(0.7f, 1f, 0.2f) * 0.5f;
					Lighting.AddLight(base.NPC.Top + new Vector2(0f, 15f), rgb5);
					break;
				}
				case 526:
				{
					Vector3 rgb4 = new Vector3(1f, 1f, 0.5f) * 0.4f;
					Lighting.AddLight(base.NPC.Top + new Vector2(0f, 15f), rgb4);
					break;
				}
				case 527:
				{
					Vector3 rgb3 = new Vector3(0.6f, 0.3f, 1f) * 0.4f;
					Lighting.AddLight(base.NPC.Top + new Vector2(0f, 15f), rgb3);
					break;
				}
				case 415:
				{
					base.NPC.hide = false;
					ActiveEntityIterator<NPC>.Enumerator enumerator3 = Main.ActiveNPCs.GetEnumerator();
					while (enumerator3.MoveNext())
					{
						NPC n4 = enumerator3.Current;
						if (n4.type == 416 && n4.ai[0] == (float)base.NPC.whoAmI)
						{
							base.NPC.hide = true;
							break;
						}
					}
					break;
				}
				case 258:
					if (base.NPC.velocity.Y != 0f)
					{
						base.NPC.TargetClosest();
						base.NPC.spriteDirection = base.NPC.direction;
						if (Main.player[base.NPC.target].Center.X < base.NPC.position.X && base.NPC.velocity.X > 0f)
						{
							base.NPC.velocity.X *= 0.95f;
						}
						else if (Main.player[base.NPC.target].Center.X > base.NPC.position.X + (float)base.NPC.width && base.NPC.velocity.X < 0f)
						{
							base.NPC.velocity.X *= 0.95f;
						}
						if (Main.player[base.NPC.target].Center.X < base.NPC.position.X && base.NPC.velocity.X > -5f)
						{
							base.NPC.velocity.X -= 0.1f;
						}
						else if (Main.player[base.NPC.target].Center.X > base.NPC.position.X + (float)base.NPC.width && base.NPC.velocity.X < 5f)
						{
							base.NPC.velocity.X += 0.1f;
						}
					}
					else if (Main.player[base.NPC.target].Center.Y + 50f < base.NPC.position.Y && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						base.NPC.velocity.Y = -9f;
					}
					break;
				case 425:
				{
					if (base.NPC.velocity.Y == 0f)
					{
						base.NPC.ai[2] = 0f;
					}
					if (base.NPC.velocity.Y != 0f && base.NPC.ai[2] == 1f)
					{
						base.NPC.TargetClosest();
						base.NPC.spriteDirection = -base.NPC.direction;
						if (Collision.CanHit(base.NPC.Center, 0, 0, Main.player[base.NPC.target].Center, 0, 0))
						{
							float idealLocationX = Main.player[base.NPC.target].Center.X - (float)(base.NPC.direction * 600) - base.NPC.Center.X;
							float idealLocationY = Main.player[base.NPC.target].Bottom.Y - base.NPC.Bottom.Y;
							if (idealLocationX < 0f && base.NPC.velocity.X > 0f)
							{
								base.NPC.velocity.X *= 0.9f;
							}
							else if (idealLocationX > 0f && base.NPC.velocity.X < 0f)
							{
								base.NPC.velocity.X *= 0.9f;
							}
							if (idealLocationX < 0f && base.NPC.velocity.X > -7f)
							{
								base.NPC.velocity.X -= 0.12f;
							}
							else if (idealLocationX > 0f && base.NPC.velocity.X < 7f)
							{
								base.NPC.velocity.X += 0.12f;
							}
							if (base.NPC.velocity.X > 8f)
							{
								base.NPC.velocity.X = 8f;
							}
							if (base.NPC.velocity.X < -8f)
							{
								base.NPC.velocity.X = -8f;
							}
							if (idealLocationY < -20f && base.NPC.velocity.Y > 0f)
							{
								base.NPC.velocity.Y *= 0.8f;
							}
							else if (idealLocationY > 20f && base.NPC.velocity.Y < 0f)
							{
								base.NPC.velocity.Y *= 0.8f;
							}
							if (idealLocationY < -20f && base.NPC.velocity.Y > -7f)
							{
								base.NPC.velocity.Y -= 0.35f;
							}
							else if (idealLocationY > 20f && base.NPC.velocity.Y < 7f)
							{
								base.NPC.velocity.Y += 0.35f;
							}
						}
						if (Main.rand.NextBool(3))
						{
							Vector2 position = base.NPC.Center + new Vector2((float)(base.NPC.direction * -14), -8f) - Vector2.One * 4f;
							Vector2 velocity2 = new Vector2((float)(base.NPC.direction * -6), 12f) * 0.2f + Utils.RandomVector2(Main.rand, -1f, 1f) * 0.1f;
							Dust obj5 = Main.dust[Dust.NewDust(position, 8, 8, 229, velocity2.X, velocity2.Y, 100, Color.Transparent, 1f + Main.rand.NextFloat() * 0.5f)];
							obj5.noGravity = true;
							obj5.velocity = velocity2;
							obj5.customData = base.NPC;
						}
						ActiveEntityIterator<NPC>.Enumerator enumerator4 = Main.ActiveNPCs.GetEnumerator();
						while (enumerator4.MoveNext())
						{
							NPC n5 = enumerator4.Current;
							if (n5.whoAmI != base.NPC.whoAmI && n5.type == npcType && Math.Abs(base.NPC.position.X - n5.position.X) + Math.Abs(base.NPC.position.Y - n5.position.Y) < (float)base.NPC.width)
							{
								if (base.NPC.position.X < n5.position.X)
								{
									base.NPC.velocity.X -= 0.05f;
								}
								else
								{
									base.NPC.velocity.X += 0.05f;
								}
								if (base.NPC.position.Y < n5.position.Y)
								{
									base.NPC.velocity.Y -= 0.05f;
								}
								else
								{
									base.NPC.velocity.Y += 0.05f;
								}
							}
						}
					}
					else if (Main.player[base.NPC.target].Center.Y + 100f < base.NPC.position.Y && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						base.NPC.velocity.Y = -7f;
						base.NPC.ai[2] = 1f;
					}
					if (Main.netMode == 1)
					{
						break;
					}
					base.NPC.localAI[2]++;
					bool closeToPlayer = base.NPC.Distance(Main.player[base.NPC.target].Center) < 600f && Math.Abs(base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center).Y) < 0.5f;
					float vortexShotgunGateValue = (CalamityWorld.death ? 240f : (CalamityWorld.revenge ? 360f : 480f));
					if (((base.NPC.localAI[2] >= vortexShotgunGateValue) & closeToPlayer) && Collision.CanHitLine(base.NPC.Center, 0, 0, Main.player[base.NPC.target].Center, 0, 0))
					{
						base.NPC.localAI[2] = 0f;
						Vector2 spawnPosition3 = base.NPC.Center + new Vector2((float)base.NPC.direction * 30f, 2f);
						float vortexLaserVelocity = (CalamityWorld.death ? 7f : (CalamityWorld.revenge ? 6f : 5f));
						Vector2 baseLaserVelocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center, Vector2.UnitX * (float)base.NPC.direction) * vortexLaserVelocity;
						int damage2 = (Main.expertMode ? 50 : 75);
						float maxSpread = (CalamityWorld.death ? 0.7f : (CalamityWorld.revenge ? 0.6f : 0.5f));
						for (int num5 = 0; num5 < 4; num5++)
						{
							Vector2 randomizedVelocity = baseLaserVelocity + Utils.RandomVector2(Main.rand, 0f - maxSpread, maxSpread);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPosition3, randomizedVelocity, 577, damage2, 1f, Main.myPlayer);
						}
					}
					break;
				}
				case 427:
					if (base.NPC.velocity.Y == 0f)
					{
						base.NPC.ai[2] = 0f;
						base.NPC.rotation = 0f;
					}
					else
					{
						base.NPC.rotation = base.NPC.velocity.X * 0.1f;
					}
					if (base.NPC.velocity.Y != 0f && base.NPC.ai[2] == 1f)
					{
						base.NPC.TargetClosest();
						base.NPC.spriteDirection = -base.NPC.direction;
						if (Collision.CanHit(base.NPC.Center, 0, 0, Main.player[base.NPC.target].Center, 0, 0))
						{
							float playerDistX = Main.player[base.NPC.target].Center.X - base.NPC.Center.X;
							float playerDistY = Main.player[base.NPC.target].Center.Y - base.NPC.Center.Y;
							if (playerDistX < 0f && base.NPC.velocity.X > 0f)
							{
								base.NPC.velocity.X *= 0.98f;
							}
							else if (playerDistX > 0f && base.NPC.velocity.X < 0f)
							{
								base.NPC.velocity.X *= 0.98f;
							}
							if (playerDistX < -20f && base.NPC.velocity.X > 0f - (CalamityWorld.revenge ? 8f : 6f))
							{
								base.NPC.velocity.X -= (CalamityWorld.revenge ? 0.025f : 0.015f);
							}
							else if (playerDistX > 20f && base.NPC.velocity.X < (CalamityWorld.revenge ? 8f : 6f))
							{
								base.NPC.velocity.X += (CalamityWorld.revenge ? 0.025f : 0.015f);
							}
							if (base.NPC.velocity.X > (CalamityWorld.revenge ? 8f : 6f))
							{
								base.NPC.velocity.X = (CalamityWorld.revenge ? 8f : 6f);
							}
							if (base.NPC.velocity.X < 0f - (CalamityWorld.revenge ? 8f : 6f))
							{
								base.NPC.velocity.X = 0f - (CalamityWorld.revenge ? 8f : 6f);
							}
							if (playerDistY < -20f && base.NPC.velocity.Y > 0f)
							{
								base.NPC.velocity.Y *= 0.98f;
							}
							else if (playerDistY > 20f && base.NPC.velocity.Y < 0f)
							{
								base.NPC.velocity.Y *= 0.98f;
							}
							if (playerDistY < -20f && base.NPC.velocity.Y > 0f - (CalamityWorld.revenge ? 8f : 6f))
							{
								base.NPC.velocity.Y -= (CalamityWorld.revenge ? 0.25f : 0.15f);
							}
							else if (playerDistY > 20f && base.NPC.velocity.Y < (CalamityWorld.revenge ? 8f : 6f))
							{
								base.NPC.velocity.Y += (CalamityWorld.revenge ? 0.25f : 0.15f);
							}
						}
						ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
						while (enumerator2.MoveNext())
						{
							NPC n3 = enumerator2.Current;
							if (n3.whoAmI != base.NPC.whoAmI && n3.type == npcType && Math.Abs(base.NPC.position.X - n3.position.X) + Math.Abs(base.NPC.position.Y - n3.position.Y) < (float)base.NPC.width)
							{
								if (base.NPC.position.X < n3.position.X)
								{
									base.NPC.velocity.X -= 0.05f;
								}
								else
								{
									base.NPC.velocity.X += 0.05f;
								}
								if (base.NPC.position.Y < n3.position.Y)
								{
									base.NPC.velocity.Y -= 0.05f;
								}
								else
								{
									base.NPC.velocity.Y += 0.05f;
								}
							}
						}
					}
					else if (Main.player[base.NPC.target].Center.Y + 100f < base.NPC.position.Y && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						base.NPC.velocity.Y = 0f - (CalamityWorld.revenge ? 7f : 5f);
						base.NPC.ai[2] = 1f;
					}
					break;
				case 426:
				{
					if (base.NPC.ai[1] > 0f && base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y *= 0.85f;
						if (base.NPC.velocity.Y == 0f)
						{
							base.NPC.velocity.Y = -0.4f;
						}
					}
					if (base.NPC.velocity.Y != 0f)
					{
						base.NPC.TargetClosest();
						base.NPC.spriteDirection = base.NPC.direction;
						if (Collision.CanHit(base.NPC.Center, 0, 0, Main.player[base.NPC.target].Center, 0, 0))
						{
							float distanceToLocationX = Main.player[base.NPC.target].Center.X - (float)(base.NPC.direction * (CalamityWorld.revenge ? 450 : 300)) - base.NPC.Center.X;
							if (distanceToLocationX < 40f && base.NPC.velocity.X > 0f)
							{
								base.NPC.velocity.X *= 0.98f;
							}
							else if (distanceToLocationX > 40f && base.NPC.velocity.X < 0f)
							{
								base.NPC.velocity.X *= 0.98f;
							}
							if (distanceToLocationX < 40f && base.NPC.velocity.X > 0f - (CalamityWorld.revenge ? 8f : 6f))
							{
								base.NPC.velocity.X -= (CalamityWorld.revenge ? 0.25f : 0.2f);
							}
							else if (distanceToLocationX > 40f && base.NPC.velocity.X < (CalamityWorld.revenge ? 8f : 6f))
							{
								base.NPC.velocity.X += (CalamityWorld.revenge ? 0.25f : 0.2f);
							}
							if (base.NPC.velocity.X > (CalamityWorld.revenge ? 8f : 6f))
							{
								base.NPC.velocity.X = (CalamityWorld.revenge ? 8f : 6f);
							}
							if (base.NPC.velocity.X < 0f - (CalamityWorld.revenge ? 8f : 6f))
							{
								base.NPC.velocity.X = 0f - (CalamityWorld.revenge ? 8f : 6f);
							}
						}
					}
					else if (Main.player[base.NPC.target].Center.Y + 100f < base.NPC.position.Y && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						base.NPC.velocity.Y = 0f - (CalamityWorld.revenge ? 8f : 6f);
					}
					ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
					while (enumerator.MoveNext())
					{
						NPC n2 = enumerator.Current;
						if (n2.whoAmI != base.NPC.whoAmI && n2.type == npcType && Math.Abs(base.NPC.position.X - n2.position.X) + Math.Abs(base.NPC.position.Y - n2.position.Y) < (float)base.NPC.width)
						{
							if (base.NPC.position.X < n2.position.X)
							{
								base.NPC.velocity.X -= 0.1f;
							}
							else
							{
								base.NPC.velocity.X += 0.1f;
							}
							if (base.NPC.position.Y < n2.position.Y)
							{
								base.NPC.velocity.Y -= 0.1f;
							}
							else
							{
								base.NPC.velocity.Y += 0.1f;
							}
						}
					}
					if (Main.rand.NextBool(6) && base.NPC.ai[1] <= 20f)
					{
						Dust obj3 = Main.dust[Dust.NewDust(base.NPC.Center + new Vector2((float)((base.NPC.spriteDirection == 1) ? 8 : (-20)), -20f), 8, 8, 229, base.NPC.velocity.X, base.NPC.velocity.Y, 100)];
						obj3.velocity = obj3.velocity / 4f + base.NPC.velocity / 2f;
						obj3.scale = 0.6f;
						obj3.noLight = true;
					}
					if (base.NPC.ai[1] >= 57f)
					{
						int dustType = Utils.SelectRandom<int>(Main.rand, 161, 229);
						Dust obj4 = Main.dust[Dust.NewDust(base.NPC.Center + new Vector2((float)((base.NPC.spriteDirection == 1) ? 8 : (-20)), -20f), 8, 8, dustType, base.NPC.velocity.X, base.NPC.velocity.Y, 100)];
						obj4.velocity = obj4.velocity / 4f + base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Top);
						obj4.scale = 1.2f;
						obj4.noLight = true;
					}
					if (Main.rand.NextBool(6))
					{
						Dust dust2 = Main.dust[Dust.NewDust(base.NPC.Center, 2, 2, 229)];
						dust2.position = base.NPC.Center + new Vector2((float)((base.NPC.spriteDirection == 1) ? 26 : (-26)), 24f);
						dust2.velocity.X = 0f;
						if (dust2.velocity.Y < 0f)
						{
							dust2.velocity.Y = 0f;
						}
						dust2.noGravity = true;
						dust2.scale = 1f;
						dust2.noLight = true;
					}
					break;
				}
				case 185:
					if (base.NPC.velocity.Y == 0f)
					{
						base.NPC.rotation = 0f;
						base.NPC.localAI[0] = 0f;
					}
					else if (base.NPC.localAI[0] == 1f)
					{
						base.NPC.rotation += base.NPC.velocity.X * 0.05f;
					}
					break;
				case 428:
					if (base.NPC.velocity.Y == 0f)
					{
						base.NPC.rotation = 0f;
					}
					else
					{
						base.NPC.rotation += base.NPC.velocity.X * 0.08f;
					}
					break;
				}
			}
			if (npcType == 159 && Main.netMode != 1 && base.NPC.Distance(Main.player[base.NPC.target].Center) > 300f)
			{
				base.NPC.Transform(158);
			}
			if (Main.netMode != 1 && base.NPC.velocity.Y == 0f)
			{
				TryConvertToWallClimber(base.NPC);
			}
			bool prehardmodeSpiders = (base.NPC.type == 164 || base.NPC.type == 165 || base.NPC.type == 239 || base.NPC.type == 240) && CalamityWorld.revenge;
			if (Main.netMode != 1 && Main.expertMode && base.NPC.target >= 0 && ((npcType == 163 || npcType == 238 || base.NPC.type == 236 || base.NPC.type == 237) | prehardmodeSpiders) && Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
			{
				base.NPC.localAI[0]++;
				if (base.NPC.justHit)
				{
					base.NPC.localAI[0] = 0f;
				}
				float webSpitGateValue = (CalamityWorld.death ? 180f : (CalamityWorld.revenge ? 300f : 420f));
				Vector2 mouth = default(Vector2);
				((Vector2)(ref mouth))._002Ector(base.NPC.Center.X + ((base.NPC.direction == -1) ? (-22f) : 12f), base.NPC.Center.Y - 4f);
				if (base.NPC.localAI[0] > webSpitGateValue - 30f)
				{
					Dust dust3 = Dust.NewDustDirect(mouth, 1, 1, 30, 0f, 0f, 100, default(Color), 1.5f);
					dust3.noGravity = true;
					dust3.velocity *= 0f;
				}
				if (base.NPC.localAI[0] >= webSpitGateValue)
				{
					base.NPC.localAI[0] = 0f;
					Vector2 velocity3 = (Main.player[base.NPC.target].Center - mouth).SafeNormalize(-Vector2.UnitY) * (prehardmodeSpiders ? 6f : 10f);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), mouth, velocity3, 472, 18, 0f, Main.myPlayer);
				}
			}
			else if ((npcType == 163 || npcType == 238 || base.NPC.type == 236 || base.NPC.type == 237) | prehardmodeSpiders)
			{
				base.NPC.localAI[0] = 0f;
			}
			if (npcType == 243)
			{
				if (base.NPC.justHit || base.NPC.confused || base.NPC.velocity.Y != 0f || Main.player[base.NPC.target].dead || Main.player[base.NPC.target].frozen || ((base.NPC.direction <= 0 || !(base.NPC.Center.X < Main.player[base.NPC.target].Center.X)) && (base.NPC.direction >= 0 || !(base.NPC.Center.X > Main.player[base.NPC.target].Center.X))) || !Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					base.NPC.ai[2] = 0f;
				}
				base.NPC.ai[2]++;
				Vector2 eyeLocation = base.NPC.Center + Vector2.UnitX * ((base.NPC.direction == -1) ? (-14f) : 6f) - Vector2.UnitY * 40f;
				if (Main.netMode != 1 && base.NPC.ai[2] >= (CalamityWorld.death ? 60f : (CalamityWorld.revenge ? 90f : 120f)) && base.NPC.velocity.Y == 0f && !Main.player[base.NPC.target].dead && !Main.player[base.NPC.target].frozen && ((base.NPC.direction > 0 && base.NPC.Center.X < Main.player[base.NPC.target].Center.X) || (base.NPC.direction < 0 && base.NPC.Center.X > Main.player[base.NPC.target].Center.X)) && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					base.NPC.netUpdate = true;
					float iceLaserVelocity = (CalamityWorld.death ? 12f : (CalamityWorld.revenge ? 10f : 8f));
					Vector2 spawnPosition4 = eyeLocation;
					Vector2 velocity4 = (Main.player[base.NPC.target].Center - spawnPosition4).SafeNormalize(-Vector2.UnitY) * iceLaserVelocity;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPosition4 + velocity4.SafeNormalize(-Vector2.UnitY) * 100f, velocity4, 257, 32, 0f, Main.myPlayer);
					base.NPC.ai[2] = 0f;
				}
				if (base.NPC.ai[2] > (CalamityWorld.death ? 60f : (CalamityWorld.revenge ? 90f : 120f)) - 30f)
				{
					Dust dust4 = Dust.NewDustDirect(eyeLocation, 1, 1, 92, 0f, 0f, 200, default(Color), 1.5f);
					dust4.noGravity = true;
					dust4.velocity *= 0f;
				}
			}
			if (npcType == 251)
			{
				if (base.NPC.justHit || base.NPC.confused || base.NPC.velocity.Y != 0f || Main.player[base.NPC.target].dead || ((base.NPC.direction <= 0 || !(base.NPC.Center.X < Main.player[base.NPC.target].Center.X)) && (base.NPC.direction >= 0 || !(base.NPC.Center.X > Main.player[base.NPC.target].Center.X))) || !Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					base.NPC.ai[2] = 0f;
				}
				base.NPC.ai[2]++;
				Vector2 eyeLocation2 = default(Vector2);
				((Vector2)(ref eyeLocation2))._002Ector(base.NPC.position.X + ((base.NPC.direction == -1) ? (-12f) : 6f) + (float)base.NPC.width * 0.5f, base.NPC.position.Y + 6f);
				if (Main.netMode != 1 && base.NPC.ai[2] >= (CalamityWorld.death ? 60f : (CalamityWorld.revenge ? 90f : 120f)) && base.NPC.velocity.Y == 0f && !Main.player[base.NPC.target].dead && ((base.NPC.direction > 0 && base.NPC.Center.X < Main.player[base.NPC.target].Center.X) || (base.NPC.direction < 0 && base.NPC.Center.X > Main.player[base.NPC.target].Center.X)) && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					float eyeLaserVelocity = (CalamityWorld.death ? 6f : (CalamityWorld.revenge ? 5f : 4f));
					Vector2 spawnPosition5 = eyeLocation2;
					Vector2 velocity5 = (Main.player[base.NPC.target].Center - spawnPosition5).SafeNormalize(-Vector2.UnitY) * eyeLaserVelocity;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPosition5 + velocity5.SafeNormalize(-Vector2.UnitY) * 80f, velocity5, 83, 40, 0f, Main.myPlayer);
					base.NPC.ai[2] = 0f;
				}
				if (base.NPC.ai[2] > (CalamityWorld.death ? 60f : (CalamityWorld.revenge ? 90f : 120f)) - 30f)
				{
					Dust dust5 = Dust.NewDustDirect(eyeLocation2, 1, 1, 27, 0f, 0f, 100, default(Color), 1.5f);
					dust5.noGravity = true;
					dust5.velocity *= 0f;
				}
			}
			if (npcType == 386)
			{
				if (base.NPC.confused)
				{
					base.NPC.ai[2] = -40f;
				}
				else
				{
					if (base.NPC.ai[2] < 40f)
					{
						base.NPC.ai[2]++;
					}
					if (base.NPC.ai[2] > 0f && NPC.CountNPCS(387) >= 4 * NPC.CountNPCS(386))
					{
						base.NPC.ai[2] = 0f;
					}
					if (base.NPC.justHit)
					{
						base.NPC.ai[2] = -20f;
					}
					if (base.NPC.ai[2] == 20f)
					{
						int centerTileX2 = (int)base.NPC.position.X / 16;
						int centerTileY4 = (int)base.NPC.position.Y / 16;
						int centerTileX3 = (int)base.NPC.position.X / 16;
						int centerTileY5 = (int)base.NPC.position.Y / 16;
						int maxTurretDistX = 5;
						int maxTurretDistanceY = 2;
						int tryCounter = 0;
						bool createdTurret = false;
						while (!createdTurret && tryCounter < 100)
						{
							tryCounter++;
							int turretSpawnX = Main.rand.Next(centerTileX2 - maxTurretDistX, centerTileX2 + maxTurretDistX);
							for (int num6 = Main.rand.Next(centerTileY4 - maxTurretDistX, centerTileY4 + maxTurretDistX); num6 < centerTileY4 + maxTurretDistX; num6++)
							{
								if ((num6 < centerTileY4 - maxTurretDistanceY || num6 > centerTileY4 + maxTurretDistanceY || turretSpawnX < centerTileX2 - maxTurretDistanceY || turretSpawnX > centerTileX2 + maxTurretDistanceY) && (num6 < centerTileY5 || num6 > centerTileY5 || turretSpawnX < centerTileX3 || turretSpawnX > centerTileX3) && Main.tile[turretSpawnX, num6].HasUnactuatedTile)
								{
									bool notLava = true;
									if (Main.tile[turretSpawnX, num6 - 1].LiquidType == 1)
									{
										notLava = false;
									}
									if (notLava && Main.tileSolid[Main.tile[turretSpawnX, num6].TileType] && !Collision.SolidTiles(turretSpawnX - 1, turretSpawnX + 1, num6 - 4, num6 - 1))
									{
										int turretIdx = NPC.NewNPC(base.NPC.GetSource_FromAI(), turretSpawnX * 16 - base.NPC.width / 2, num6 * 16, 387);
										Main.npc[turretIdx].position.Y = num6 * 16 - Main.npc[turretIdx].height;
										createdTurret = true;
										base.NPC.netUpdate = true;
										break;
									}
								}
							}
						}
					}
					if (base.NPC.ai[2] == 40f)
					{
						base.NPC.ai[2] = -90f;
					}
				}
			}
			if (npcType == 389)
			{
				if (base.NPC.confused)
				{
					base.NPC.ai[2] = -40f;
				}
				else
				{
					if (base.NPC.ai[2] < 20f)
					{
						base.NPC.ai[2]++;
					}
					if (base.NPC.justHit)
					{
						base.NPC.ai[2] = -20f;
					}
					if (base.NPC.ai[2] == 20f && Main.netMode != 1)
					{
						base.NPC.ai[2] = -10 + Main.rand.Next(3) * -10;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y + 8f, base.NPC.direction * 8, 0f, 437, 25, 1f, Main.myPlayer);
					}
				}
			}
			if (npcType == 110 || npcType == 111 || npcType == 206 || npcType == 214 || npcType == 215 || npcType == 216 || npcType == 290 || npcType == 291 || npcType == 292 || npcType == 293 || npcType == 350 || npcType == 379 || npcType == 380 || npcType == 381 || npcType == 382 || (npcType >= 449 && npcType <= 452) || npcType == 468 || npcType == 481 || npcType == 411 || npcType == 409 || (npcType >= 498 && npcType <= 506) || npcType == 424 || npcType == 426 || npcType == 520)
			{
				bool npcAllowedToShoot = npcType == 381 || npcType == 382 || npcType == 520;
				bool isAlienQueen = npcType == 426;
				bool canShootAtTarget = true;
				int stardustGateValue = -1;
				int stardustGateValueAdd = -1;
				if (npcType == 411)
				{
					npcAllowedToShoot = true;
					stardustGateValue = 90;
					stardustGateValueAdd = 90;
					if (base.NPC.ai[1] <= 150f)
					{
						canShootAtTarget = false;
					}
				}
				if (base.NPC.confused)
				{
					base.NPC.ai[2] = 0f;
				}
				else
				{
					if (base.NPC.ai[1] > 0f)
					{
						base.NPC.ai[1]--;
					}
					if (base.NPC.justHit)
					{
						base.NPC.ai[1] = 30f;
						base.NPC.ai[2] = 0f;
					}
					int attackTimeMax = 70;
					if (npcType == 379 || npcType == 380)
					{
						attackTimeMax = 80;
					}
					if (npcType == 381 || npcType == 382)
					{
						attackTimeMax = 80;
					}
					if (npcType == 520)
					{
						attackTimeMax = 15;
					}
					if (npcType == 350)
					{
						attackTimeMax = 110;
					}
					if (npcType == 291)
					{
						attackTimeMax = 200;
					}
					if (npcType == 292)
					{
						attackTimeMax = 120;
					}
					if (npcType == 293)
					{
						attackTimeMax = 90;
					}
					if (npcType == 111)
					{
						attackTimeMax = 180;
					}
					if (npcType == 206)
					{
						attackTimeMax = 50;
					}
					if (npcType == 481)
					{
						attackTimeMax = 100;
					}
					if (npcType == 214)
					{
						attackTimeMax = 40;
					}
					if (npcType == 215)
					{
						attackTimeMax = 80;
					}
					if (npcType == 290)
					{
						attackTimeMax = 30;
					}
					if (npcType == 411)
					{
						attackTimeMax = 300;
					}
					if (npcType == 409)
					{
						attackTimeMax = 60;
					}
					if (npcType == 424)
					{
						attackTimeMax = 180;
					}
					if (npcType == 426)
					{
						attackTimeMax = 60;
					}
					bool priateCaptainBoost = false;
					if (npcType == 216)
					{
						if (base.NPC.localAI[2] >= 20f)
						{
							priateCaptainBoost = true;
						}
						attackTimeMax = ((!priateCaptainBoost) ? 8 : 60);
					}
					if (CalamityWorld.revenge)
					{
						attackTimeMax = (int)((double)attackTimeMax * 0.75);
					}
					int modifiedAttackTime = attackTimeMax / 2;
					if (npcType == 424)
					{
						modifiedAttackTime = attackTimeMax - 1;
					}
					if (npcType == 426)
					{
						modifiedAttackTime = attackTimeMax - 1;
					}
					if (base.NPC.ai[2] > 0f)
					{
						if (canShootAtTarget)
						{
							base.NPC.TargetClosest();
						}
						if (base.NPC.ai[1] == (float)modifiedAttackTime)
						{
							if (npcType == 216)
							{
								base.NPC.localAI[2]++;
							}
							float projSpeed = (CalamityWorld.death ? 6f : 11f);
							if (npcType == 111)
							{
								projSpeed = (CalamityWorld.death ? 5f : 9f);
							}
							if (npcType == 206)
							{
								projSpeed = (CalamityWorld.death ? 4f : 7f);
							}
							if (npcType == 290)
							{
								projSpeed = (CalamityWorld.death ? 5f : 9f);
							}
							if (npcType == 293)
							{
								projSpeed = (CalamityWorld.death ? 2.5f : 4f);
							}
							if (npcType == 214)
							{
								projSpeed = (CalamityWorld.death ? 8f : 14f);
							}
							if (npcType == 215)
							{
								projSpeed = (CalamityWorld.death ? 9f : 16f);
							}
							if (npcType == 382)
							{
								projSpeed = (CalamityWorld.death ? 4f : 7f);
							}
							if (npcType == 520)
							{
								projSpeed = (CalamityWorld.death ? 5f : 8f);
							}
							if (npcType == 409)
							{
								projSpeed = 4f;
							}
							if (npcType >= 449 && npcType <= 452)
							{
								projSpeed = (CalamityWorld.death ? 4f : 7f);
							}
							if (npcType == 481)
							{
								projSpeed = (CalamityWorld.death ? 5f : 8f);
							}
							if (npcType == 468)
							{
								projSpeed = (CalamityWorld.death ? 4.5f : 7.5f);
							}
							if (npcType == 411)
							{
								projSpeed = 1f;
							}
							if (npcType >= 498 && npcType <= 506)
							{
								projSpeed = (CalamityWorld.death ? 4f : 7f);
							}
							if (CalamityWorld.revenge)
							{
								projSpeed *= 1.25f;
							}
							Vector2 spawnPosition6 = default(Vector2);
							((Vector2)(ref spawnPosition6))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
							if (npcType == 481)
							{
								spawnPosition6.Y -= 14f;
							}
							if (npcType == 206)
							{
								spawnPosition6.Y -= 10f;
							}
							if (npcType == 290)
							{
								spawnPosition6.Y -= 10f;
							}
							if (npcType == 381 || npcType == 382)
							{
								spawnPosition6.Y += 6f;
							}
							if (npcType == 520)
							{
								spawnPosition6.Y = base.NPC.position.Y + 20f;
							}
							if (npcType >= 498 && npcType <= 506)
							{
								spawnPosition6.Y -= 8f;
							}
							if (npcType == 426)
							{
								spawnPosition6 += new Vector2((float)(base.NPC.spriteDirection * 2), -12f);
								projSpeed = (CalamityWorld.death ? 6f : (CalamityWorld.revenge ? 9f : 7f));
							}
							float distX2 = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - spawnPosition6.X;
							float projOffset = Math.Abs(distX2) * 0.1f;
							if (npcType == 291 || npcType == 292)
							{
								projOffset = 0f;
							}
							if (npcType == 215)
							{
								projOffset = Math.Abs(distX2) * 0.08f;
							}
							if (npcType == 214 || (npcType == 216 && !priateCaptainBoost))
							{
								projOffset = 0f;
							}
							if (npcType == 381 || npcType == 382 || npcType == 520)
							{
								projOffset = 0f;
							}
							if (npcType >= 449 && npcType <= 452)
							{
								projOffset = Math.Abs(distX2) * (float)Main.rand.Next(10, 50) * 0.01f;
							}
							if (npcType == 468)
							{
								projOffset = Math.Abs(distX2) * (float)Main.rand.Next(10, 50) * 0.01f;
							}
							if (npcType == 481)
							{
								projOffset = Math.Abs(distX2) * (float)Main.rand.Next(-10, 11) * 0.0035f;
							}
							if (npcType >= 498 && npcType <= 506)
							{
								projOffset = Math.Abs(distX2) * (float)Main.rand.Next(1, 11) * 0.0025f;
							}
							float distY2 = Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height * 0.5f - spawnPosition6.Y - projOffset;
							float magnitude = (float)Math.Sqrt(distX2 * distX2 + distY2 * distY2);
							base.NPC.netUpdate = true;
							magnitude = projSpeed / magnitude;
							distX2 *= magnitude;
							distY2 *= magnitude;
							int damage3 = 35;
							int projectileType = 82;
							if (npcType == 350)
							{
								damage3 = 45;
							}
							if (npcType == 111)
							{
								projectileType = 81;
								damage3 = 11;
							}
							if (npcType == 379 || npcType == 380)
							{
								projectileType = 81;
								damage3 = 40;
							}
							if (npcType == 381)
							{
								projectileType = 436;
								damage3 = 24;
							}
							if (npcType == 382)
							{
								projectileType = 438;
								damage3 = 30;
							}
							if (npcType == 520)
							{
								projectileType = 592;
								damage3 = 35;
							}
							if (npcType >= 449 && npcType <= 452)
							{
								projectileType = 471;
								damage3 = 20;
							}
							if (npcType >= 498 && npcType <= 506)
							{
								projectileType = 572;
								damage3 = 14;
							}
							if (npcType == 481)
							{
								projectileType = 508;
								damage3 = 18;
							}
							if (npcType == 206)
							{
								projectileType = 177;
								damage3 = 37;
							}
							if (npcType == 468)
							{
								projectileType = 501;
								damage3 = 50;
							}
							if (npcType == 411)
							{
								projectileType = 537;
								damage3 = (Main.expertMode ? 45 : 60);
							}
							if (npcType == 424)
							{
								projectileType = 573;
								damage3 = (Main.expertMode ? 45 : 60);
							}
							if (npcType == 426)
							{
								projectileType = 581;
								damage3 = (Main.expertMode ? 45 : 60);
							}
							if (npcType == 291)
							{
								projectileType = 302;
								damage3 = 100;
							}
							if (npcType == 290)
							{
								projectileType = 300;
								damage3 = 60;
							}
							if (npcType == 293)
							{
								projectileType = 303;
								damage3 = 60;
							}
							if (npcType == 214)
							{
								projectileType = 180;
								damage3 = 25;
							}
							if (npcType == 215)
							{
								projectileType = 82;
								damage3 = 40;
							}
							if (npcType == 292)
							{
								damage3 = 50;
								projectileType = 180;
							}
							if (npcType == 216)
							{
								projectileType = 180;
								damage3 = 30;
								if (priateCaptainBoost)
								{
									damage3 = 100;
									projectileType = 240;
									base.NPC.localAI[2] = 0f;
								}
							}
							spawnPosition6.X += distX2;
							spawnPosition6.Y += distY2;
							if (Main.expertMode && npcType == 290)
							{
								damage3 = (int)((double)damage3 * 0.75);
							}
							if (Main.expertMode && npcType >= 381 && npcType <= 392)
							{
								damage3 = (int)((double)damage3 * 0.8);
							}
							if (Main.netMode != 1)
							{
								switch (npcType)
								{
								case 292:
								{
									Vector2 bulletSpawnPosition = base.NPC.Center;
									Vector2 bulletVelocity = default(Vector2);
									for (int num8 = 0; num8 < 4; num8++)
									{
										distX2 = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - spawnPosition6.X;
										distY2 = Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height * 0.5f - spawnPosition6.Y;
										magnitude = (float)Math.Sqrt(distX2 * distX2 + distY2 * distY2);
										magnitude = (CalamityWorld.death ? 8f : (CalamityWorld.revenge ? 7f : 6f)) / magnitude;
										int shotgunSpread = 20;
										distX2 += (float)Main.rand.Next(-shotgunSpread, shotgunSpread + 1);
										distY2 += (float)Main.rand.Next(-shotgunSpread, shotgunSpread + 1);
										distX2 *= magnitude;
										distY2 *= magnitude;
										((Vector2)(ref bulletVelocity))._002Ector(distX2, distY2);
										Projectile.NewProjectile(base.NPC.GetSource_FromAI(), bulletSpawnPosition + bulletVelocity.SafeNormalize(-Vector2.UnitY) * 30f, bulletVelocity, projectileType, damage3, 0f, Main.myPlayer);
									}
									break;
								}
								case 411:
								{
									int proj2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPosition6.X, spawnPosition6.Y, distX2, distY2, projectileType, damage3, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
									if (CalamityWorld.death)
									{
										Main.projectile[proj2].extraUpdates++;
										Main.projectile[proj2].timeLeft = 480;
									}
									break;
								}
								case 424:
								{
									for (int num7 = 0; num7 < 4; num7++)
									{
										int proj3 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X - (float)(base.NPC.spriteDirection * 4), base.NPC.Center.Y + 6f, (float)(-3 + 2 * num7) * 0.15f, (0f - (float)Main.rand.Next(0, 3)) * 0.2f - 0.1f, projectileType, damage3, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
										if (CalamityWorld.death)
										{
											Main.projectile[proj3].extraUpdates++;
											Main.projectile[proj3].timeLeft = 1200;
										}
									}
									break;
								}
								case 409:
								{
									int idx = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 410, base.NPC.whoAmI);
									Main.npc[idx].velocity = new Vector2(distX2, -6f + distY2);
									break;
								}
								default:
								{
									int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPosition6.X, spawnPosition6.Y, distX2, distY2, projectileType, damage3, 0f, Main.myPlayer);
									if (CalamityWorld.death)
									{
										Main.projectile[proj].extraUpdates++;
										Main.projectile[proj].timeLeft = 1200;
									}
									break;
								}
								}
							}
							if (Math.Abs(distY2) > Math.Abs(distX2) * 2f)
							{
								if (distY2 > 0f)
								{
									base.NPC.ai[2] = 1f;
								}
								else
								{
									base.NPC.ai[2] = 5f;
								}
							}
							else if (Math.Abs(distX2) > Math.Abs(distY2) * 2f)
							{
								base.NPC.ai[2] = 3f;
							}
							else if (distY2 > 0f)
							{
								base.NPC.ai[2] = 2f;
							}
							else
							{
								base.NPC.ai[2] = 4f;
							}
						}
						if ((base.NPC.velocity.Y != 0f && !isAlienQueen) || base.NPC.ai[1] <= 0f)
						{
							base.NPC.ai[2] = 0f;
							base.NPC.ai[1] = 0f;
						}
						else if (!npcAllowedToShoot || (stardustGateValue != -1 && base.NPC.ai[1] >= (float)stardustGateValue && base.NPC.ai[1] < (float)(stardustGateValue + stardustGateValueAdd) && (!isAlienQueen || base.NPC.velocity.Y == 0f)))
						{
							base.NPC.velocity.X *= 0.9f;
							base.NPC.spriteDirection = base.NPC.direction;
						}
					}
					if (npcType == 468 && !Main.eclipse)
					{
						npcAllowedToShoot = true;
					}
					else if (((base.NPC.ai[2] <= 0f) | npcAllowedToShoot) && ((base.NPC.velocity.Y == 0f) | isAlienQueen) && base.NPC.ai[1] <= 0f && !Main.player[base.NPC.target].dead)
					{
						bool canAttack = Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
						if (npcType == 520)
						{
							canAttack = Collision.CanHitLine(base.NPC.Top + new Vector2(0f, 20f), 0, 0, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
						}
						if (Main.player[base.NPC.target].stealth == 0f && Main.player[base.NPC.target].itemAnimation == 0)
						{
							canAttack = false;
						}
						if (canAttack)
						{
							Vector2 projSpawnPosition = default(Vector2);
							((Vector2)(ref projSpawnPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
							float distX3 = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - projSpawnPosition.X;
							float distXAbsolute = Math.Abs(distX3) * 0.1f;
							float distY3 = Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height * 0.5f - projSpawnPosition.Y - distXAbsolute;
							distX3 += (float)Main.rand.Next(-40, 41);
							distY3 += (float)Main.rand.Next(-40, 41);
							float playerDistance5 = (float)Math.Sqrt(distX3 * distX3 + distY3 * distY3);
							float maxAttackDistance = 700f;
							if (npcType == 214)
							{
								maxAttackDistance = 550f;
							}
							if (npcType == 215)
							{
								maxAttackDistance = 800f;
							}
							if (npcType >= 498 && npcType <= 506)
							{
								maxAttackDistance = 190f;
							}
							if (npcType >= 449 && npcType <= 452)
							{
								maxAttackDistance = 200f;
							}
							if (npcType == 481)
							{
								maxAttackDistance = 400f;
							}
							if (npcType == 468)
							{
								maxAttackDistance = 400f;
							}
							if (CalamityWorld.death)
							{
								maxAttackDistance *= 1.25f;
							}
							if (playerDistance5 < maxAttackDistance)
							{
								base.NPC.netUpdate = true;
								base.NPC.velocity.X *= 0.5f;
								playerDistance5 = 10f / playerDistance5;
								distX3 *= playerDistance5;
								distY3 *= playerDistance5;
								base.NPC.ai[2] = 3f;
								base.NPC.ai[1] = attackTimeMax;
								if (Math.Abs(distY3) > Math.Abs(distX3) * 2f)
								{
									if (distY3 > 0f)
									{
										base.NPC.ai[2] = 1f;
									}
									else
									{
										base.NPC.ai[2] = 5f;
									}
								}
								else if (Math.Abs(distX3) > Math.Abs(distY3) * 2f)
								{
									base.NPC.ai[2] = 3f;
								}
								else if (distY3 > 0f)
								{
									base.NPC.ai[2] = 2f;
								}
								else
								{
									base.NPC.ai[2] = 4f;
								}
							}
						}
					}
					if (base.NPC.ai[2] <= 0f || (npcAllowedToShoot && (stardustGateValue == -1 || base.NPC.ai[1] < (float)stardustGateValue || base.NPC.ai[1] >= (float)(stardustGateValue + stardustGateValueAdd))))
					{
						float maxVelocity7 = 1f;
						float acceleration4 = 0.07f;
						float decelerationFactor = 0.8f;
						switch (npcType)
						{
						case 214:
							maxVelocity7 = 2f;
							acceleration4 = 0.09f;
							break;
						case 215:
							maxVelocity7 = 1.5f;
							acceleration4 = 0.08f;
							break;
						case 381:
						case 382:
							maxVelocity7 = 2f;
							acceleration4 = 0.5f;
							break;
						case 520:
							maxVelocity7 = 4f;
							acceleration4 = 1f;
							decelerationFactor = 0.7f;
							break;
						case 411:
							maxVelocity7 = 2f;
							acceleration4 = 0.5f;
							break;
						case 409:
							maxVelocity7 = 2f;
							acceleration4 = 0.5f;
							break;
						default:
							if (base.NPC.type == 426)
							{
								maxVelocity7 = 4f;
								acceleration4 = 0.6f;
								decelerationFactor = 0.95f;
							}
							break;
						}
						if (CalamityWorld.revenge)
						{
							maxVelocity7 *= 1.5f;
							acceleration4 *= 1.5f;
						}
						bool forceDeceleration = false;
						if ((npcType == 381 || npcType == 382) && Vector2.Distance(base.NPC.Center, Main.player[base.NPC.target].Center) < 300f && Collision.CanHitLine(base.NPC.Center, 0, 0, Main.player[base.NPC.target].Center, 0, 0))
						{
							forceDeceleration = true;
							base.NPC.ai[3] = 0f;
						}
						if (npcType == 520 && Vector2.Distance(base.NPC.Center, Main.player[base.NPC.target].Center) < 400f && Collision.CanHitLine(base.NPC.Center, 0, 0, Main.player[base.NPC.target].Center, 0, 0))
						{
							forceDeceleration = true;
							base.NPC.ai[3] = 0f;
						}
						if ((base.NPC.velocity.X < 0f - maxVelocity7 || base.NPC.velocity.X > maxVelocity7) | forceDeceleration)
						{
							if (base.NPC.velocity.Y == 0f)
							{
								NPC nPC7 = base.NPC;
								nPC7.velocity *= decelerationFactor;
							}
						}
						else if (base.NPC.velocity.X < maxVelocity7 && base.NPC.direction == 1)
						{
							base.NPC.velocity.X += acceleration4;
							if (base.NPC.velocity.X > maxVelocity7)
							{
								base.NPC.velocity.X = maxVelocity7;
							}
						}
						else if (base.NPC.velocity.X > 0f - maxVelocity7 && base.NPC.direction == -1)
						{
							base.NPC.velocity.X -= acceleration4;
							if (base.NPC.velocity.X < 0f - maxVelocity7)
							{
								base.NPC.velocity.X = 0f - maxVelocity7;
							}
						}
					}
					if (npcType == 520)
					{
						base.NPC.localAI[2]++;
						if (base.NPC.localAI[2] >= 6f)
						{
							base.NPC.localAI[2] = 0f;
							base.NPC.localAI[3] = Main.player[base.NPC.target].DirectionFrom(base.NPC.Top + new Vector2(0f, 20f)).ToRotation();
						}
					}
				}
			}
			if (npcType == 109 && Main.netMode != 1 && !Main.player[base.NPC.target].dead)
			{
				if (base.NPC.justHit)
				{
					base.NPC.ai[2] = 0f;
				}
				base.NPC.ai[2]++;
				float bombDelay = (CalamityWorld.death ? 60f : 180f);
				if (base.NPC.ai[2] > bombDelay)
				{
					Vector2 spawnPosition7 = default(Vector2);
					((Vector2)(ref spawnPosition7))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f - (float)(base.NPC.direction * 24), base.NPC.position.Y + 4f);
					if (!Main.rand.NextBool(5) || NPC.AnyNPCs(378))
					{
						int velocityX = 3 * base.NPC.direction;
						int velocityY = -5;
						int clownBomb = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPosition7.X, spawnPosition7.Y, velocityX, velocityY, 75, 0, 0f, Main.myPlayer, 0f, 0f, 0f);
						Main.projectile[clownBomb].timeLeft = 300;
						if (CalamityWorld.death)
						{
							Main.projectile[clownBomb].extraUpdates++;
							Main.projectile[clownBomb].timeLeft = 600;
						}
						base.NPC.ai[2] = 0f;
					}
					else
					{
						base.NPC.ai[2] = (0f - bombDelay) * 2f;
						int chatteringTeethBomb = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)spawnPosition7.X, (int)spawnPosition7.Y, 378);
						NetMessage.SendData(23, -1, -1, null, chatteringTeethBomb);
					}
				}
			}
			bool canOpenDoors = false;
			if (base.NPC.velocity.Y == 0f)
			{
				int j2 = (int)(base.NPC.position.Y + (float)base.NPC.height + 7f) / 16;
				int num9 = (int)base.NPC.position.X / 16;
				int npcRight = (int)(base.NPC.position.X + (float)base.NPC.width) / 16;
				for (int num10 = num9; num10 <= npcRight; num10++)
				{
					if (Main.tile[num10, j2].HasUnactuatedTile && Main.tileSolid[Main.tile[num10, j2].TileType])
					{
						canOpenDoors = true;
						break;
					}
				}
			}
			if (npcType == 428)
			{
				canOpenDoors = false;
			}
			if (base.NPC.velocity.Y >= 0f)
			{
				int velocitySign = 0;
				if (base.NPC.velocity.X < 0f)
				{
					velocitySign = -1;
				}
				if (base.NPC.velocity.X > 0f)
				{
					velocitySign = 1;
				}
				Vector2 positionDelta = base.NPC.position;
				positionDelta.X += base.NPC.velocity.X;
				int x2 = (int)((positionDelta.X + (float)(base.NPC.width / 2) + (float)((base.NPC.width / 2 + 1) * velocitySign)) / 16f);
				int y2 = (int)((positionDelta.Y + (float)base.NPC.height - 1f) / 16f);
				if ((float)(x2 * 16) < positionDelta.X + (float)base.NPC.width && (float)((x2 + 1) * 16) > positionDelta.X && ((Main.tile[x2, y2].HasUnactuatedTile && !Main.tile[x2, y2].TopSlope && !Main.tile[x2, y2 - 1].TopSlope && Main.tileSolid[Main.tile[x2, y2].TileType] && !Main.tileSolidTop[Main.tile[x2, y2].TileType]) || (Main.tile[x2, y2 - 1].IsHalfBlock && Main.tile[x2, y2 - 1].HasUnactuatedTile)) && (!Main.tile[x2, y2 - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[x2, y2 - 1].TileType] || Main.tileSolidTop[Main.tile[x2, y2 - 1].TileType] || (Main.tile[x2, y2 - 1].IsHalfBlock && (!Main.tile[x2, y2 - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[x2, y2 - 4].TileType] || Main.tileSolidTop[Main.tile[x2, y2 - 4].TileType]))) && (!Main.tile[x2, y2 - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[x2, y2 - 2].TileType] || Main.tileSolidTop[Main.tile[x2, y2 - 2].TileType]) && (!Main.tile[x2, y2 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[x2, y2 - 3].TileType] || Main.tileSolidTop[Main.tile[x2, y2 - 3].TileType]) && (!Main.tile[x2 - velocitySign, y2 - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[x2 - velocitySign, y2 - 3].TileType]))
				{
					float yAdjust = (float)y2 * 16f;
					if (Main.tile[x2, y2].IsHalfBlock)
					{
						yAdjust += 8f;
					}
					if (Main.tile[x2, y2 - 1].IsHalfBlock)
					{
						yAdjust -= 8f;
					}
					if (yAdjust < positionDelta.Y + (float)base.NPC.height)
					{
						float gfxOffRelativeToDelta = positionDelta.Y + (float)base.NPC.height - yAdjust;
						float yOffsetFloor = 16.1f;
						if (npcType == 163 || npcType == 164 || npcType == 236 || npcType == 239 || npcType == 530)
						{
							yOffsetFloor += 8f;
						}
						if (gfxOffRelativeToDelta <= yOffsetFloor)
						{
							base.NPC.gfxOffY += base.NPC.position.Y + (float)base.NPC.height - yAdjust;
							base.NPC.position.Y = yAdjust - (float)base.NPC.height;
							if (gfxOffRelativeToDelta < 9f)
							{
								base.NPC.stepSpeed = 1f;
							}
							else
							{
								base.NPC.stepSpeed = 2f;
							}
						}
					}
				}
			}
			if (canOpenDoors)
			{
				int x3 = (int)((base.NPC.Center.X + (float)(15 * base.NPC.direction)) / 16f);
				int y3 = (int)((base.NPC.position.Y + (float)base.NPC.height - 15f) / 16f);
				if (npcType == 109 || npcType == 163 || npcType == 164 || npcType == 199 || npcType == 236 || npcType == 239 || npcType == 257 || npcType == 258 || npcType == 290 || npcType == 391 || npcType == 425 || npcType == 427 || npcType == 426 || npcType == 580 || npcType == 415 || npcType == 530 || npcType == 532)
				{
					x3 = (int)((base.NPC.position.X + (float)(base.NPC.width / 2) + (float)((base.NPC.width / 2 + 16) * base.NPC.direction)) / 16f);
				}
				if ((Main.tile[x3, y3 - 1].HasUnactuatedTile && (Main.tile[x3, y3 - 1].TileType == 10 || Main.tile[x3, y3 - 1].TileType == 388)) & reset)
				{
					base.NPC.ai[2]++;
					base.NPC.ai[3] = 0f;
					if (base.NPC.ai[2] >= 60f)
					{
						base.NPC.velocity.X = -0.5f * (float)base.NPC.direction;
						int timerIncrement = 5;
						if (Main.tile[x3, y3 - 1].TileType == 388)
						{
							timerIncrement = 2;
						}
						base.NPC.ai[1] += timerIncrement;
						if (npcType == 27)
						{
							base.NPC.ai[1]++;
						}
						if (npcType == 31 || npcType == 294 || npcType == 295 || npcType == 296)
						{
							base.NPC.ai[1] += 6f;
						}
						base.NPC.ai[2] = 0f;
						bool readyToOpenDoor = false;
						if (base.NPC.ai[1] >= 10f)
						{
							readyToOpenDoor = true;
							base.NPC.ai[1] = 10f;
						}
						if (npcType == 460)
						{
							readyToOpenDoor = true;
						}
						WorldGen.KillTile(x3, y3 - 1, fail: true);
						if (((Main.netMode != 1 || !readyToOpenDoor) & readyToOpenDoor) && Main.netMode != 1)
						{
							if (npcType == 26)
							{
								WorldGen.KillTile(x3, y3 - 1);
								if (Main.dedServ)
								{
									NetMessage.SendData(17, -1, -1, null, 0, x3, y3 - 1);
								}
							}
							else
							{
								if (Main.tile[x3, y3 - 1].TileType == 10)
								{
									bool canOpenDoor = WorldGen.OpenDoor(x3, y3 - 1, base.NPC.direction);
									if (!canOpenDoor)
									{
										base.NPC.ai[3] = aiGateValue;
										base.NPC.netUpdate = true;
									}
									if (Main.dedServ & canOpenDoor)
									{
										NetMessage.SendData(19, -1, -1, null, 0, x3, y3 - 1, base.NPC.direction);
									}
								}
								if (Main.tile[x3, y3 - 1].TileType == 388)
								{
									bool canOpenTallGate = WorldGen.ShiftTallGate(x3, y3 - 1, closing: false);
									if (!canOpenTallGate)
									{
										base.NPC.ai[3] = aiGateValue;
										base.NPC.netUpdate = true;
									}
									if (Main.dedServ & canOpenTallGate)
									{
										NetMessage.SendData(19, -1, -1, null, 4, x3, y3 - 1);
									}
								}
							}
						}
					}
				}
				else
				{
					int alteredDirection = base.NPC.spriteDirection;
					if (npcType == 425)
					{
						alteredDirection *= -1;
					}
					if ((base.NPC.velocity.X < 0f && alteredDirection == -1) || (base.NPC.velocity.X > 0f && alteredDirection == 1))
					{
						if (base.NPC.height >= 32 && Main.tile[x3, y3 - 2].HasUnactuatedTile && Main.tileSolid[Main.tile[x3, y3 - 2].TileType])
						{
							if (Main.tile[x3, y3 - 3].HasUnactuatedTile && Main.tileSolid[Main.tile[x3, y3 - 3].TileType])
							{
								base.NPC.velocity.Y = -9f;
								base.NPC.netUpdate = true;
							}
							else
							{
								base.NPC.velocity.Y = -8f;
								base.NPC.netUpdate = true;
							}
						}
						else if (Main.tile[x3, y3 - 1].HasUnactuatedTile && Main.tileSolid[Main.tile[x3, y3 - 1].TileType])
						{
							base.NPC.velocity.Y = -7f;
							base.NPC.netUpdate = true;
						}
						else if (base.NPC.position.Y + (float)base.NPC.height - (float)(y3 * 16) > 20f && Main.tile[x3, y3].HasUnactuatedTile && !Main.tile[x3, y3].TopSlope && Main.tileSolid[Main.tile[x3, y3].TileType])
						{
							base.NPC.velocity.Y = -6f;
							base.NPC.netUpdate = true;
						}
						else if (base.NPC.directionY < 0 && npcType != 67 && (!Main.tile[x3, y3 + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[x3, y3 + 1].TileType]) && (!Main.tile[x3 + base.NPC.direction, y3 + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[x3 + base.NPC.direction, y3 + 1].TileType]))
						{
							base.NPC.velocity.Y = -9f;
							base.NPC.velocity.X *= 2f;
							base.NPC.netUpdate = true;
						}
						else if (reset)
						{
							base.NPC.ai[1] = 0f;
							base.NPC.ai[2] = 0f;
						}
						if (((base.NPC.velocity.Y == 0f) & jump) && base.NPC.ai[3] == 1f)
						{
							base.NPC.velocity.Y = -6f;
						}
					}
					switch (npcType)
					{
					case 31:
					case 47:
					case 77:
					case 104:
					case 168:
					case 196:
					case 294:
					case 295:
					case 296:
					case 385:
					case 389:
					case 464:
					case 470:
					case 524:
					case 525:
					case 526:
					case 527:
						if (base.NPC.velocity.Y == 0f && Math.Abs(base.NPC.position.X + (float)(base.NPC.width / 2) - (Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2))) < 100f && Math.Abs(base.NPC.position.Y + (float)(base.NPC.height / 2) - (Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2))) < 50f && ((base.NPC.direction > 0 && base.NPC.velocity.X >= 1f) || (base.NPC.direction < 0 && base.NPC.velocity.X <= -1f)))
						{
							base.NPC.velocity.X *= 3f;
							if (base.NPC.velocity.X > 4f)
							{
								base.NPC.velocity.X = 4f;
							}
							if (base.NPC.velocity.X < -4f)
							{
								base.NPC.velocity.X = -4f;
							}
							base.NPC.velocity.Y = -5f;
							base.NPC.netUpdate = true;
						}
						break;
					}
					if ((npcType == 120 || npcType == ModContent.NPCType<RenegadeWarlock>()) && base.NPC.velocity.Y < 0f)
					{
						base.NPC.velocity.Y *= 1.1f;
					}
					if (npcType == 287 && base.NPC.velocity.Y == 0f && Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) < 150f && Math.Abs(base.NPC.Center.Y - Main.player[base.NPC.target].Center.Y) < 50f && ((base.NPC.direction > 0 && base.NPC.velocity.X >= 1f) || (base.NPC.direction < 0 && base.NPC.velocity.X <= -1f)))
					{
						base.NPC.velocity.X = (CalamityWorld.death ? 9 : (CalamityWorld.revenge ? 8 : 7)) * base.NPC.direction;
						base.NPC.velocity.Y = 0f - (CalamityWorld.death ? 4f : (CalamityWorld.revenge ? 3.5f : 3f));
						base.NPC.netUpdate = true;
					}
					if (npcType == 287 && base.NPC.velocity.Y < 0f)
					{
						base.NPC.velocity.X *= (CalamityWorld.death ? 1.2f : (CalamityWorld.revenge ? 1.15f : 1.1f));
						base.NPC.velocity.Y *= (CalamityWorld.death ? 1.1f : (CalamityWorld.revenge ? 1.075f : 1.05f));
					}
					if (npcType == 460 && base.NPC.velocity.Y < 0f)
					{
						base.NPC.velocity.X *= 1.35f;
						base.NPC.velocity.Y *= 1.15f;
					}
				}
			}
			else if (reset)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
			}
			if (Main.netMode != 1 && (npcType == 120 || npcType == ModContent.NPCType<RenegadeWarlock>()) && base.NPC.ai[3] >= (float)aiGateValue)
			{
				int targetTileX = (int)Main.player[base.NPC.target].Center.X / 16;
				int targetTileY = (int)Main.player[base.NPC.target].Center.Y / 16;
				Vector2 chosenTile = Vector2.Zero;
				if (base.NPC.AI_AttemptToFindTeleportSpot(ref chosenTile, targetTileX, targetTileY, 20, 9))
				{
					base.NPC.position.X = chosenTile.X * 16f - (float)(base.NPC.width / 2);
					base.NPC.position.Y = chosenTile.Y * 16f - (float)base.NPC.height;
					base.NPC.ai[3] = -30f;
					base.NPC.netUpdate = true;
				}
			}
			return false;
		}
	}

	public class FlowInvaderAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_015b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0160: Unknown result type (might be due to invalid IL or missing references)
			//IL_018d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0164: Unknown result type (might be due to invalid IL or missing references)
			//IL_0172: Unknown result type (might be due to invalid IL or missing references)
			//IL_0181: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Unknown result type (might be due to invalid IL or missing references)
			//IL_018b: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
			float velocityMult = (CalamityWorld.death ? 8f : 6.5f);
			float moveSpeed = (CalamityWorld.death ? 0.25f : 0.2f);
			base.NPC.TargetClosest();
			Vector2 desiredVelocity3 = Main.player[base.NPC.target].Center - base.NPC.Center + new Vector2(0f, -300f);
			float velocityCheck = ((Vector2)(ref desiredVelocity3)).Length();
			if (velocityCheck < 20f)
			{
				desiredVelocity3 = base.NPC.velocity;
			}
			else if (velocityCheck < 40f)
			{
				((Vector2)(ref desiredVelocity3)).Normalize();
				desiredVelocity3 *= velocityMult * 0.35f;
			}
			else if (velocityCheck < 80f)
			{
				((Vector2)(ref desiredVelocity3)).Normalize();
				desiredVelocity3 *= velocityMult * 0.65f;
			}
			else
			{
				((Vector2)(ref desiredVelocity3)).Normalize();
				desiredVelocity3 *= velocityMult;
			}
			base.NPC.SimpleFlyMovement(desiredVelocity3, moveSpeed);
			base.NPC.rotation = base.NPC.velocity.X * 0.1f;
			if (!(++base.NPC.ai[0] >= (CalamityWorld.death ? 30f : 50f)))
			{
				return false;
			}
			base.NPC.ai[0] = 0f;
			if (Main.netMode != 1)
			{
				Vector2 projDirection = Vector2.Zero;
				while (Math.Abs(projDirection.X) < 1.5f)
				{
					projDirection = Vector2.UnitY.RotatedByRandom(1.5707963705062866) * new Vector2(5f, 3f);
				}
				int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, projDirection, 539, 60, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
				Main.projectile[proj].extraUpdates++;
			}
			return false;
		}
	}

	public class FlyingAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0340: Unknown result type (might be due to invalid IL or missing references)
			//IL_036c: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Unknown result type (might be due to invalid IL or missing references)
			//IL_0132: Unknown result type (might be due to invalid IL or missing references)
			//IL_013c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0141: Unknown result type (might be due to invalid IL or missing references)
			//IL_017f: Unknown result type (might be due to invalid IL or missing references)
			//IL_018a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0194: Unknown result type (might be due to invalid IL or missing references)
			//IL_0199: Unknown result type (might be due to invalid IL or missing references)
			//IL_019e: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0227: Unknown result type (might be due to invalid IL or missing references)
			//IL_022d: Unknown result type (might be due to invalid IL or missing references)
			//IL_023b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0245: Unknown result type (might be due to invalid IL or missing references)
			//IL_024a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0288: Unknown result type (might be due to invalid IL or missing references)
			//IL_0293: Unknown result type (might be due to invalid IL or missing references)
			//IL_029d: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e03: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e08: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e1b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e38: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e62: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e7a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e92: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e9e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b27: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a84: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a89: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a8e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a93: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a9b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0abd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aec: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b76: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b40: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b4f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b56: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b5b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b63: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bdc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bec: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bc9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b8f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b9e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ba5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0baa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0782: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_22b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_22c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_2707: Unknown result type (might be due to invalid IL or missing references)
			//IL_2733: Unknown result type (might be due to invalid IL or missing references)
			//IL_27b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_27e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_28da: Unknown result type (might be due to invalid IL or missing references)
			//IL_28f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_28fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_2900: Unknown result type (might be due to invalid IL or missing references)
			//IL_290a: Unknown result type (might be due to invalid IL or missing references)
			//IL_2914: Unknown result type (might be due to invalid IL or missing references)
			//IL_2919: Unknown result type (might be due to invalid IL or missing references)
			//IL_292d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2932: Unknown result type (might be due to invalid IL or missing references)
			//IL_296b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2971: Unknown result type (might be due to invalid IL or missing references)
			//IL_2985: Unknown result type (might be due to invalid IL or missing references)
			//IL_298f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2994: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bb1: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bcd: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bd2: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bd7: Unknown result type (might be due to invalid IL or missing references)
			//IL_2be1: Unknown result type (might be due to invalid IL or missing references)
			//IL_2beb: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bf0: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c04: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c09: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c1f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c25: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c44: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c4e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c53: Unknown result type (might be due to invalid IL or missing references)
			//IL_12cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_12da: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a36: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a3d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a42: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a47: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a62: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a6d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a77: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a7c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a81: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a89: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a95: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a9a: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ac5: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ad0: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ada: Unknown result type (might be due to invalid IL or missing references)
			//IL_2adf: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ae4: Unknown result type (might be due to invalid IL or missing references)
			//IL_2af8: Unknown result type (might be due to invalid IL or missing references)
			//IL_2afd: Unknown result type (might be due to invalid IL or missing references)
			//IL_23da: Unknown result type (might be due to invalid IL or missing references)
			//IL_23df: Unknown result type (might be due to invalid IL or missing references)
			//IL_23e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_23ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_23f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_2403: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b12: Unknown result type (might be due to invalid IL or missing references)
			//IL_170e: Unknown result type (might be due to invalid IL or missing references)
			//IL_171b: Unknown result type (might be due to invalid IL or missing references)
			//IL_24d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_24d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c99: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ce5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ceb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d02: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d0c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d11: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d6f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dbb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dc1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ebb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1efd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fa1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fdd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fe3: Unknown result type (might be due to invalid IL or missing references)
			if (base.NPC.target < 0 || base.NPC.target <= 255 || Main.player[base.NPC.target].dead)
			{
				base.NPC.TargetClosest();
			}
			if (base.NPC.type == 619)
			{
				if (Main.dayTime)
				{
					base.NPC.velocity.Y -= 0.3f;
					base.NPC.EncourageDespawn(60);
				}
				NPC nPC = base.NPC;
				nPC.position += base.NPC.netOffset;
				if (base.NPC.alpha == 255)
				{
					base.NPC.spriteDirection = base.NPC.direction;
					base.NPC.velocity.Y = -6f;
					for (int i = 0; i < 35; i++)
					{
						Dust dust = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 5);
						dust.velocity *= 1f;
						dust.scale = 1f + Main.rand.NextFloat() * 0.5f;
						dust.fadeIn = 1.5f + Main.rand.NextFloat() * 0.5f;
						dust.velocity += base.NPC.velocity * 0.5f;
					}
				}
				base.NPC.alpha -= 15;
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
				if (base.NPC.alpha != 0)
				{
					for (int j = 0; j < 2; j++)
					{
						Dust dust2 = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 5);
						dust2.velocity *= 1f;
						dust2.scale = 1f + Main.rand.NextFloat() * 0.5f;
						dust2.fadeIn = 1.5f + Main.rand.NextFloat() * 0.5f;
						dust2.velocity += base.NPC.velocity * 0.3f;
					}
				}
				NPC nPC2 = base.NPC;
				nPC2.position -= base.NPC.netOffset;
			}
			NPCAimedTarget targetData = base.NPC.GetTargetData();
			bool targetDead = false;
			if (targetData.Type == NPCTargetType.Player)
			{
				targetDead = Main.player[base.NPC.target].dead;
			}
			bool queenBeeHornet = base.NPC.type == 232 && base.NPC.ai[3] == 1f;
			if (queenBeeHornet)
			{
				if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					if (base.NPC.localAI[1] != 0f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
					{
						base.NPC.localAI[1] = 0f;
						base.NPC.localAI[2] = 0f;
						base.NPC.SyncVanillaLocalAI();
					}
				}
				else if (base.NPC.localAI[1] == 0f)
				{
					base.NPC.localAI[2]++;
				}
				if (base.NPC.localAI[2] >= (CalamityWorld.death ? 60f : 120f))
				{
					base.NPC.localAI[1] = 1f;
					base.NPC.localAI[2] = 0f;
					base.NPC.SyncVanillaLocalAI();
				}
				if (base.NPC.localAI[1] == 0f)
				{
					base.NPC.alpha = 0;
					base.NPC.noTileCollide = false;
				}
				else
				{
					base.NPC.wet = false;
					base.NPC.alpha = 200;
					base.NPC.noTileCollide = true;
				}
			}
			bool deathModeVelocityBuff = true;
			float maxVelocity = 6f;
			float acceleration = 0.05f;
			if (base.NPC.type == 6 || base.NPC.type == 173)
			{
				maxVelocity = 4f;
				acceleration = 0.035f;
			}
			else if (base.NPC.type == 94)
			{
				maxVelocity = 4.2f;
				acceleration = 0.022f;
			}
			else if (base.NPC.type == 619)
			{
				maxVelocity = 6f;
				acceleration = 0.1f;
			}
			else if (base.NPC.type == 42 || (base.NPC.type >= 231 && base.NPC.type <= 235))
			{
				maxVelocity = 3.5f;
				acceleration = 0.021f;
				if (base.NPC.type == 231)
				{
					maxVelocity = 3f;
					acceleration = 0.017f;
				}
				maxVelocity *= 1f - base.NPC.scale;
				acceleration *= 1f - base.NPC.scale;
				if ((double)(base.NPC.position.Y / 16f) < Main.worldSurface && !queenBeeHornet)
				{
					if (Main.player[base.NPC.target].position.Y - base.NPC.position.Y > 300f && base.NPC.velocity.Y < 0f)
					{
						base.NPC.velocity.Y *= 0.97f;
					}
					if (Main.player[base.NPC.target].position.Y - base.NPC.position.Y < 80f && base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y *= 0.97f;
					}
				}
				if (queenBeeHornet)
				{
					maxVelocity *= 1.4f;
					acceleration *= 1.8f;
					maxVelocity += base.NPC.ai[2] * 1.5f;
					acceleration += base.NPC.ai[2] * 0.01f;
				}
			}
			else if (base.NPC.type == 176)
			{
				maxVelocity = 4f;
				acceleration = 0.017f;
			}
			else if (base.NPC.type == 252)
			{
				if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					maxVelocity = 6f;
					acceleration = 0.1f;
				}
				else
				{
					acceleration = 0.01f;
					maxVelocity = 2f;
				}
			}
			else if (base.NPC.type == 205)
			{
				maxVelocity = 7f;
				acceleration = 0.06f;
			}
			else if (base.NPC.type == 23)
			{
				maxVelocity = 2f;
				acceleration = 0.05f;
			}
			else if (base.NPC.type == 5)
			{
				maxVelocity = 5f + base.NPC.ai[2] * 2f;
				acceleration = 0.03f + base.NPC.ai[2] * 0.03f;
			}
			else if (base.NPC.type == 210 || base.NPC.type == 211)
			{
				if (base.NPC.ai[3] == 0f)
				{
					base.NPC.ai[1]++;
					float flyAwayTime = (CalamityWorld.death ? 30f : 40f);
					float originalFlyAwayVelocity = 6f;
					float flyAwayVelocity = 60f * originalFlyAwayVelocity / flyAwayTime;
					float flyAwayAccel = (base.NPC.ai[1] - flyAwayTime) / flyAwayTime;
					if (flyAwayAccel > 1f)
					{
						flyAwayAccel = 1f;
					}
					else
					{
						if (base.NPC.velocity.X > flyAwayVelocity)
						{
							base.NPC.velocity.X = flyAwayVelocity;
						}
						if (base.NPC.velocity.X < 0f - flyAwayVelocity)
						{
							base.NPC.velocity.X = 0f - flyAwayVelocity;
						}
						if (base.NPC.velocity.Y > flyAwayVelocity)
						{
							base.NPC.velocity.Y = flyAwayVelocity;
						}
						if (base.NPC.velocity.Y < 0f - flyAwayVelocity)
						{
							base.NPC.velocity.Y = 0f - flyAwayVelocity;
						}
					}
					maxVelocity = 5f;
					acceleration = 0.1f * flyAwayAccel;
				}
				else
				{
					deathModeVelocityBuff = false;
					maxVelocity = 5f + base.NPC.ai[2] * 2f;
					acceleration = 0.1f + base.NPC.ai[2] * 0.04f;
				}
			}
			if (CalamityWorld.revenge)
			{
				maxVelocity *= 1.25f;
				acceleration *= 1.25f;
			}
			if (CalamityWorld.death & deathModeVelocityBuff)
			{
				maxVelocity *= 1.25f;
				acceleration *= 1.25f;
			}
			if (queenBeeHornet)
			{
				float deceleration = 1f - acceleration;
				if (targetDead)
				{
					Vector2 destination = base.NPC.Center - Vector2.UnitY;
					Vector2 idealVelocity = base.NPC.SafeDirectionTo(destination) * maxVelocity * 0.5f;
					idealVelocity.X *= base.NPC.direction;
					idealVelocity.Y *= 2.5f;
					base.NPC.SimpleFlyMovement(idealVelocity, acceleration);
					base.NPC.EncourageDespawn(10);
					base.NPC.wet = false;
					base.NPC.noTileCollide = true;
				}
				else if (base.NPC.Distance(targetData.Center) > 400f)
				{
					Vector2 idealVelocity2 = base.NPC.SafeDirectionTo(targetData.Center) * maxVelocity;
					base.NPC.SimpleFlyMovement(idealVelocity2, acceleration);
				}
				else if (base.NPC.Distance(targetData.Center) < 160f)
				{
					Vector2 idealVelocity3 = base.NPC.SafeDirectionTo(targetData.Center) * maxVelocity;
					base.NPC.SimpleFlyMovement(-idealVelocity3, acceleration);
				}
				else
				{
					NPC nPC3 = base.NPC;
					nPC3.velocity *= deceleration;
				}
				if (targetData.Center.X - base.NPC.Center.X > 0f)
				{
					base.NPC.spriteDirection = 1;
				}
				else
				{
					base.NPC.spriteDirection = -1;
				}
				base.NPC.rotation = base.NPC.velocity.X * 0.1f;
				float reboundSpeed = 0.7f;
				if (base.NPC.collideX)
				{
					base.NPC.netUpdate = true;
					base.NPC.velocity.X = base.NPC.oldVelocity.X * (0f - reboundSpeed);
					if (base.NPC.direction == -1 && base.NPC.velocity.X > 0f && base.NPC.velocity.X < 2f)
					{
						base.NPC.velocity.X = 2f;
					}
					if (base.NPC.direction == 1 && base.NPC.velocity.X < 0f && base.NPC.velocity.X > -2f)
					{
						base.NPC.velocity.X = -2f;
					}
				}
				if (base.NPC.collideY)
				{
					base.NPC.netUpdate = true;
					base.NPC.velocity.Y = base.NPC.oldVelocity.Y * (0f - reboundSpeed);
					if (base.NPC.velocity.Y > 0f && (double)base.NPC.velocity.Y < 1.5)
					{
						base.NPC.velocity.Y = 2f;
					}
					if (base.NPC.velocity.Y < 0f && (double)base.NPC.velocity.Y > -1.5)
					{
						base.NPC.velocity.Y = -2f;
					}
				}
			}
			else
			{
				Vector2 vector = base.NPC.Center;
				float targetXDist = Main.player[base.NPC.target].Center.X;
				float targetYDist = Main.player[base.NPC.target].Center.Y;
				targetXDist = (int)(targetXDist / 8f) * 8;
				targetYDist = (int)(targetYDist / 8f) * 8;
				vector.X = (int)(vector.X / 8f) * 8;
				vector.Y = (int)(vector.Y / 8f) * 8;
				targetXDist -= vector.X;
				targetYDist -= vector.Y;
				float targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
				float targetDistCheck = targetDistance;
				if (targetDistance == 0f)
				{
					targetXDist = base.NPC.velocity.X;
					targetYDist = base.NPC.velocity.Y;
				}
				else
				{
					targetDistance = maxVelocity / targetDistance;
					targetXDist *= targetDistance;
					targetYDist *= targetDistance;
				}
				if (base.NPC.type == 42 || base.NPC.type == 176 || (base.NPC.type >= 231 && base.NPC.type <= 235) || base.NPC.type == 6 || base.NPC.type == 94 || base.NPC.type == 173 || base.NPC.type == 205 || base.NPC.type == 210 || base.NPC.type == 211 || base.NPC.type == 619)
				{
					if (targetDistCheck > 100f || base.NPC.type == 94 || base.NPC.type == 210 || base.NPC.type == 211 || base.NPC.type == 619 || base.NPC.type == 42 || base.NPC.type == 176 || (base.NPC.type >= 231 && base.NPC.type <= 235))
					{
						base.NPC.ai[0]++;
						if (base.NPC.ai[0] > 0f)
						{
							base.NPC.velocity.Y += (CalamityWorld.revenge ? 0.03f : 0.023f);
						}
						else
						{
							base.NPC.velocity.Y -= (CalamityWorld.revenge ? 0.03f : 0.023f);
						}
						if (base.NPC.ai[0] < -100f || base.NPC.ai[0] > 100f)
						{
							base.NPC.velocity.X += (CalamityWorld.revenge ? 0.03f : 0.023f);
						}
						else
						{
							base.NPC.velocity.X -= (CalamityWorld.revenge ? 0.03f : 0.023f);
						}
						if (base.NPC.ai[0] > 200f)
						{
							base.NPC.ai[0] = -200f;
						}
					}
					if (targetDistCheck < 150f && (base.NPC.type == 6 || base.NPC.type == 94 || base.NPC.type == 173 || base.NPC.type == 619))
					{
						base.NPC.velocity.X += targetXDist * (CalamityWorld.revenge ? 0.009f : 0.007f);
						base.NPC.velocity.Y += targetYDist * (CalamityWorld.revenge ? 0.009f : 0.007f);
					}
					if ((base.NPC.type == 210 || base.NPC.type == 211) && base.NPC.ai[3] == 1f)
					{
						float pushVelocity = 0.5f + base.NPC.ai[2] * 0.2f;
						for (int k = 0; k < Main.maxNPCs; k++)
						{
							if (Main.npc[k].active && k != base.NPC.whoAmI && Main.npc[k].type == base.NPC.type && Vector2.Distance(base.NPC.Center, Main.npc[k].Center) < 32f * base.NPC.scale)
							{
								if (base.NPC.position.X < Main.npc[k].position.X)
								{
									base.NPC.velocity.X -= pushVelocity;
								}
								else
								{
									base.NPC.velocity.X += pushVelocity;
								}
								if (base.NPC.position.Y < Main.npc[k].position.Y)
								{
									base.NPC.velocity.Y -= pushVelocity;
								}
								else
								{
									base.NPC.velocity.Y += pushVelocity;
								}
							}
						}
					}
				}
				if (targetDead)
				{
					targetXDist = (float)base.NPC.direction * maxVelocity / 2f;
					targetYDist = (0f - maxVelocity) / 2f;
				}
				if (base.NPC.velocity.X < targetXDist)
				{
					base.NPC.velocity.X += acceleration;
					if (base.NPC.type != 173 && base.NPC.type != 6 && base.NPC.type != 94 && base.NPC.type != 619 && base.NPC.velocity.X < 0f && targetXDist > 0f)
					{
						base.NPC.velocity.X += acceleration;
					}
				}
				else if (base.NPC.velocity.X > targetXDist)
				{
					base.NPC.velocity.X -= acceleration;
					if (base.NPC.type != 173 && base.NPC.type != 6 && base.NPC.type != 94 && base.NPC.type != 619 && base.NPC.velocity.X > 0f && targetXDist < 0f)
					{
						base.NPC.velocity.X -= acceleration;
					}
				}
				if (base.NPC.velocity.Y < targetYDist)
				{
					base.NPC.velocity.Y += acceleration;
					if (base.NPC.type != 173 && base.NPC.type != 6 && base.NPC.type != 94 && base.NPC.type != 619 && base.NPC.velocity.Y < 0f && targetYDist > 0f)
					{
						base.NPC.velocity.Y += acceleration;
					}
				}
				else if (base.NPC.velocity.Y > targetYDist)
				{
					base.NPC.velocity.Y -= acceleration;
					if (base.NPC.type != 173 && base.NPC.type != 6 && base.NPC.type != 94 && base.NPC.type != 619 && base.NPC.velocity.Y > 0f && targetYDist < 0f)
					{
						base.NPC.velocity.Y -= acceleration;
					}
				}
				if (base.NPC.type == 5)
				{
					float pushVelocity2 = 0.5f + base.NPC.ai[2] * 0.25f;
					for (int l = 0; l < Main.maxNPCs; l++)
					{
						if (Main.npc[l].active && l != base.NPC.whoAmI && Main.npc[l].type == base.NPC.type && Vector2.Distance(base.NPC.Center, Main.npc[l].Center) < 48f * base.NPC.scale)
						{
							if (base.NPC.position.X < Main.npc[l].position.X)
							{
								base.NPC.velocity.X -= pushVelocity2;
							}
							else
							{
								base.NPC.velocity.X += pushVelocity2;
							}
							if (base.NPC.position.Y < Main.npc[l].position.Y)
							{
								base.NPC.velocity.Y -= pushVelocity2;
							}
							else
							{
								base.NPC.velocity.Y += pushVelocity2;
							}
						}
					}
				}
				if (base.NPC.type == 23)
				{
					if (targetXDist > 0f)
					{
						base.NPC.spriteDirection = 1;
						base.NPC.rotation = (float)Math.Atan2(targetYDist, targetXDist);
					}
					else if (targetXDist < 0f)
					{
						base.NPC.spriteDirection = -1;
						base.NPC.rotation = (float)Math.Atan2(targetYDist, targetXDist) + (float)Math.PI;
					}
				}
				else if (base.NPC.type == 6 || base.NPC.type == 94 || base.NPC.type == 173 || base.NPC.type == 619)
				{
					base.NPC.rotation = (float)Math.Atan2(targetYDist, targetXDist) - (float)Math.PI / 2f;
				}
				else if (base.NPC.type == 205 || base.NPC.type == 42 || base.NPC.type == 176 || (base.NPC.type >= 231 && base.NPC.type <= 235))
				{
					if (base.NPC.velocity.X > 0f)
					{
						base.NPC.spriteDirection = 1;
					}
					if (base.NPC.velocity.X < 0f)
					{
						base.NPC.spriteDirection = -1;
					}
					base.NPC.rotation = base.NPC.velocity.X * 0.1f;
				}
				else
				{
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) - (float)Math.PI / 2f;
				}
				if (base.NPC.type == 42 || base.NPC.type == 176 || (base.NPC.type >= 231 && base.NPC.type <= 235) || base.NPC.type == 6 || base.NPC.type == 23 || base.NPC.type == 94 || base.NPC.type == 173 || base.NPC.type == 205 || base.NPC.type == 210 || base.NPC.type == 211 || base.NPC.type == 619)
				{
					float reboundSpeed2 = 0.7f;
					if (base.NPC.type == 6 || base.NPC.type == 173)
					{
						reboundSpeed2 = 0.4f;
					}
					if (base.NPC.collideX)
					{
						base.NPC.netUpdate = true;
						base.NPC.velocity.X = base.NPC.oldVelocity.X * (0f - reboundSpeed2);
						if (base.NPC.direction == -1 && base.NPC.velocity.X > 0f && base.NPC.velocity.X < 2f)
						{
							base.NPC.velocity.X = 2f;
						}
						if (base.NPC.direction == 1 && base.NPC.velocity.X < 0f && base.NPC.velocity.X > -2f)
						{
							base.NPC.velocity.X = -2f;
						}
					}
					if (base.NPC.collideY)
					{
						base.NPC.netUpdate = true;
						base.NPC.velocity.Y = base.NPC.oldVelocity.Y * (0f - reboundSpeed2);
						if (base.NPC.velocity.Y > 0f && (double)base.NPC.velocity.Y < 1.5)
						{
							base.NPC.velocity.Y = 2f;
						}
						if (base.NPC.velocity.Y < 0f && (double)base.NPC.velocity.Y > -1.5)
						{
							base.NPC.velocity.Y = -2f;
						}
					}
					if (base.NPC.type == 619)
					{
						int bloodDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, base.NPC.velocity.X * 0.2f, base.NPC.velocity.Y * 0.2f, 100);
						Dust obj = Main.dust[bloodDust];
						obj.velocity *= 0.5f;
					}
					else if (base.NPC.type == 23)
					{
						int meteorDust = Dust.NewDust(new Vector2(base.NPC.position.X - base.NPC.velocity.X, base.NPC.position.Y - base.NPC.velocity.Y), base.NPC.width, base.NPC.height, 6, base.NPC.velocity.X * 0.2f, base.NPC.velocity.Y * 0.2f, 100, default(Color), 2f);
						Dust obj2 = Main.dust[meteorDust];
						obj2.noGravity = true;
						obj2.velocity.X *= 0.3f;
						obj2.velocity.Y *= 0.3f;
					}
					else if (base.NPC.type != 205 && base.NPC.type != 252 && base.NPC.type != 210 && base.NPC.type != 211 && Main.rand.NextBool(20))
					{
						int dustType = 18;
						if (base.NPC.type == 173)
						{
							dustType = 5;
						}
						int idleDust = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y + (float)base.NPC.height * 0.25f), base.NPC.width, (int)((float)base.NPC.height * 0.5f), dustType, base.NPC.velocity.X, 2f, 75, base.NPC.color, base.NPC.scale);
						Dust obj3 = Main.dust[idleDust];
						obj3.velocity.X *= 0.5f;
						obj3.velocity.Y *= 0.1f;
					}
				}
				else if (base.NPC.type != 252 && Main.rand.NextBool(40))
				{
					int otherIdleDust = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y + (float)base.NPC.height * 0.25f), base.NPC.width, (int)((float)base.NPC.height * 0.5f), 5, base.NPC.velocity.X, 2f);
					Dust obj4 = Main.dust[otherIdleDust];
					obj4.velocity.X *= 0.5f;
					obj4.velocity.Y *= 0.1f;
				}
				if ((base.NPC.type == 6 || base.NPC.type == 94 || base.NPC.type == 173 || base.NPC.type == 619) && base.NPC.wet)
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y *= 0.95f;
					}
					base.NPC.velocity.Y -= (CalamityWorld.revenge ? 0.4f : 0.3f);
					if (base.NPC.velocity.Y < 0f - (CalamityWorld.revenge ? 3f : 2f))
					{
						base.NPC.velocity.Y = 0f - (CalamityWorld.revenge ? 3f : 2f);
					}
				}
				if (base.NPC.type == 205 && base.NPC.wet)
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y *= 0.95f;
					}
					base.NPC.velocity.Y -= 0.7f;
					if (base.NPC.velocity.Y < -6f)
					{
						base.NPC.velocity.Y = -6f;
					}
					base.NPC.TargetClosest();
				}
			}
			if (base.NPC.type == 42 || base.NPC.type == 176 || (base.NPC.type >= 231 && base.NPC.type <= 235))
			{
				if (base.NPC.wet)
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y *= 0.95f;
					}
					base.NPC.velocity.Y -= 0.5f;
					if (base.NPC.velocity.Y < -4f)
					{
						base.NPC.velocity.Y = -4f;
					}
					base.NPC.TargetClosest();
				}
				if (base.NPC.ai[1] == 301f)
				{
					SoundEngine.PlaySound(in SoundID.Item17, base.NPC.Center);
					base.NPC.ai[1] = 0f;
				}
				if (Main.netMode != 1)
				{
					base.NPC.ai[1] += (((base.NPC.type == 176) | queenBeeHornet) ? 2f : 1f) + base.NPC.ai[2];
					if (base.NPC.justHit && !queenBeeHornet)
					{
						base.NPC.ai[1] = 0f;
					}
					if (base.NPC.ai[1] >= 240f)
					{
						if (targetData.Type != NPCTargetType.None && Collision.CanHit(base.NPC, targetData))
						{
							float projSpeed = ((CalamityWorld.death || Main.hardMode) ? 5f : 8f);
							projSpeed += base.NPC.ai[2] * ((CalamityWorld.death || Main.hardMode) ? 2f : 4f);
							if (queenBeeHornet)
							{
								projSpeed += 2f;
							}
							Vector2 projSpawnPosition = base.NPC.Center;
							float projTargetXDist = targetData.Center.X - projSpawnPosition.X;
							float projTargetYDist = targetData.Center.Y - projSpawnPosition.Y;
							if ((projTargetXDist < 0f && base.NPC.velocity.X < 0f) || (projTargetXDist > 0f && base.NPC.velocity.X > 0f))
							{
								float projTargetDistance = (float)Math.Sqrt(projTargetXDist * projTargetXDist + projTargetYDist * projTargetYDist);
								projTargetDistance = projSpeed / projTargetDistance;
								projTargetXDist *= projTargetDistance;
								projTargetYDist *= projTargetDistance;
								int projDamage = (int)((queenBeeHornet ? 15f : 10f) * base.NPC.scale);
								if (base.NPC.type == 176)
								{
									projDamage = (int)(30f * base.NPC.scale);
								}
								int stingerType = 55;
								int stingerSpawn = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projSpawnPosition.X, projSpawnPosition.Y, projTargetXDist, projTargetYDist, stingerType, projDamage, 0f, Main.myPlayer);
								Main.projectile[stingerSpawn].timeLeft = ((CalamityWorld.death || Main.hardMode) ? 600 : 300);
								Main.projectile[stingerSpawn].extraUpdates += ((CalamityWorld.death || Main.hardMode) ? 1 : 0);
								base.NPC.ai[1] = 301f;
								base.NPC.netUpdate = true;
							}
							else
							{
								base.NPC.ai[1] = 0f;
							}
						}
						else
						{
							base.NPC.ai[1] = 0f;
						}
					}
				}
			}
			if (Main.netMode != 1 && !targetDead)
			{
				if (Main.getGoodWorld && base.NPC.type == 6 && NPC.AnyNPCs(13))
				{
					if (base.NPC.justHit)
					{
						base.NPC.localAI[0] = 0f;
					}
					base.NPC.localAI[0]++;
					if (base.NPC.localAI[0] == 60f)
					{
						if (targetData.Type != NPCTargetType.None && Collision.CanHit(base.NPC, targetData))
						{
							NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.position.X + (float)(base.NPC.width / 2) + base.NPC.velocity.X), (int)(base.NPC.position.Y + (float)(base.NPC.height / 2) + base.NPC.velocity.Y), 666);
						}
						base.NPC.localAI[0] = 0f;
					}
				}
				if (base.NPC.type == 94)
				{
					if (base.NPC.justHit || !Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						base.NPC.localAI[0] = 0f;
					}
					base.NPC.localAI[0]++;
					if (base.NPC.localAI[0] == 180f)
					{
						if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
						{
							NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.position.X + (float)(base.NPC.width / 2) + base.NPC.velocity.X), (int)(base.NPC.position.Y + (float)(base.NPC.height / 2) + base.NPC.velocity.Y), 112);
						}
						base.NPC.localAI[0] = 0f;
					}
					if (base.NPC.localAI[0] > 150f)
					{
						Dust dust3 = Dust.NewDustDirect(base.NPC.Center + base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center, -Vector2.UnitY) * 25f + Main.rand.NextVector2CircularEdge(3f, 3f), 1, 1, 18, base.NPC.velocity.X * 0.1f, base.NPC.velocity.Y * 0.1f, 80, default(Color), 1.3f);
						dust3.noGravity = true;
						dust3.velocity *= 0.3f;
					}
				}
				if (base.NPC.type == 619)
				{
					if (base.NPC.justHit || targetData.Type == NPCTargetType.None || !Collision.CanHit(base.NPC, targetData))
					{
						base.NPC.localAI[0] = 0f;
					}
					base.NPC.localAI[0]++;
					if (base.NPC.localAI[0] >= 120f)
					{
						if (targetData.Type != NPCTargetType.None && Collision.CanHit(base.NPC, targetData))
						{
							Vector2 val = base.NPC.Center - targetData.Center;
							if (((Vector2)(ref val)).Length() < 400f)
							{
								Vector2 bloodShotPosition = base.NPC.DirectionTo(new Vector2(targetData.Center.X, targetData.Position.Y));
								base.NPC.velocity = -bloodShotPosition * 5f;
								base.NPC.netUpdate = true;
								base.NPC.localAI[0] = 0f;
								bloodShotPosition = base.NPC.DirectionTo(new Vector2(targetData.Center.X, targetData.Position.Y));
								int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, bloodShotPosition * (CalamityWorld.death ? 6f : 10f), 811, 50, 1f, Main.myPlayer);
								if (CalamityWorld.death)
								{
									Main.projectile[proj].extraUpdates++;
									Main.projectile[proj].timeLeft = 1200;
								}
							}
							else
							{
								base.NPC.localAI[0] = 0f;
							}
						}
						else
						{
							base.NPC.localAI[0] = 0f;
						}
					}
					if (base.NPC.localAI[0] > 90f)
					{
						Dust dust4 = Dust.NewDustDirect(base.NPC.Center + base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center, -Vector2.UnitY) * 25f + Main.rand.NextVector2CircularEdge(5f, 5f), 1, 1, 5, 0f, 0f, 100, default(Color), 3f);
						dust4.fadeIn = 1.7f;
						dust4.noGravity = true;
						dust4.velocity *= 0f;
					}
				}
			}
			if (!queenBeeHornet && ((Main.dayTime && base.NPC.type != 173 && base.NPC.type != 6 && base.NPC.type != 23 && base.NPC.type != 210 && base.NPC.type != 211 && base.NPC.type != 94 && base.NPC.type != 205 && base.NPC.type != 252 && base.NPC.type != 619) || Main.player[base.NPC.target].dead))
			{
				base.NPC.velocity.Y -= acceleration * 2f;
				if (base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
				}
			}
			if (((base.NPC.velocity.X > 0f && base.NPC.oldVelocity.X < 0f) || (base.NPC.velocity.X < 0f && base.NPC.oldVelocity.X > 0f) || (base.NPC.velocity.Y > 0f && base.NPC.oldVelocity.Y < 0f) || (base.NPC.velocity.Y < 0f && base.NPC.oldVelocity.Y > 0f)) && !base.NPC.justHit)
			{
				base.NPC.netUpdate = true;
			}
			return false;
		}
	}

	public class FlyingFishAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0442: Unknown result type (might be due to invalid IL or missing references)
			//IL_045a: Unknown result type (might be due to invalid IL or missing references)
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0163: Unknown result type (might be due to invalid IL or missing references)
			//IL_0169: Unknown result type (might be due to invalid IL or missing references)
			//IL_0177: Unknown result type (might be due to invalid IL or missing references)
			//IL_0181: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01de: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0607: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0315: Unknown result type (might be due to invalid IL or missing references)
			//IL_033e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0344: Unknown result type (might be due to invalid IL or missing references)
			//IL_0352: Unknown result type (might be due to invalid IL or missing references)
			//IL_035c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0361: Unknown result type (might be due to invalid IL or missing references)
			//IL_038b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0396: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0243: Unknown result type (might be due to invalid IL or missing references)
			//IL_026c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0272: Unknown result type (might be due to invalid IL or missing references)
			//IL_0280: Unknown result type (might be due to invalid IL or missing references)
			//IL_028a: Unknown result type (might be due to invalid IL or missing references)
			//IL_028f: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_08aa: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.noGravity = true;
			if (base.NPC.collideX)
			{
				if (base.NPC.oldVelocity.X > 0f)
				{
					base.NPC.direction = -1;
				}
				else
				{
					base.NPC.direction = 1;
				}
				base.NPC.velocity.X = base.NPC.direction;
			}
			if (base.NPC.collideY)
			{
				if (base.NPC.oldVelocity.Y > 0f)
				{
					base.NPC.directionY = -1;
				}
				else
				{
					base.NPC.directionY = 1;
				}
				base.NPC.velocity.Y = base.NPC.directionY;
			}
			if (base.NPC.type == 587)
			{
				NPC nPC = base.NPC;
				nPC.position += base.NPC.netOffset;
				if (base.NPC.alpha == 255)
				{
					base.NPC.velocity.Y = -6f;
					base.NPC.netUpdate = true;
					for (int i = 0; i < 15; i++)
					{
						Dust dust = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 5);
						dust.velocity *= 0.5f;
						dust.scale = 1f + Main.rand.NextFloat() * 0.5f;
						dust.fadeIn = 1.5f + Main.rand.NextFloat() * 0.5f;
						dust.velocity += base.NPC.velocity * 0.5f;
					}
				}
				base.NPC.alpha -= 15;
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
				if (base.NPC.alpha != 0)
				{
					for (int j = 0; j < 2; j++)
					{
						Dust dust2 = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 5);
						dust2.velocity *= 1f;
						dust2.scale = 1f + Main.rand.NextFloat() * 0.5f;
						dust2.fadeIn = 1.5f + Main.rand.NextFloat() * 0.5f;
						dust2.velocity += base.NPC.velocity * 0.3f;
					}
				}
				if (Main.rand.NextBool(3))
				{
					Dust dust3 = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 5);
					dust3.velocity *= 0f;
					dust3.alpha = 120;
					dust3.scale = 0.7f + Main.rand.NextFloat() * 0.5f;
					dust3.velocity += base.NPC.velocity * 0.3f;
				}
				NPC nPC2 = base.NPC;
				nPC2.position -= base.NPC.netOffset;
			}
			int fishTarget = base.NPC.target;
			int fishDirection = base.NPC.direction;
			if (base.NPC.target == 255 || (Main.player[base.NPC.target].wet && base.NPC.type != 587) || Main.player[base.NPC.target].dead || Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
			{
				base.NPC.ai[0] = 90f;
				base.NPC.TargetClosest();
			}
			else if (base.NPC.ai[0] > 0f)
			{
				base.NPC.ai[0]--;
				base.NPC.TargetClosest();
			}
			if (base.NPC.netUpdate && fishTarget == base.NPC.target && fishDirection == base.NPC.direction)
			{
				base.NPC.netUpdate = false;
			}
			float acceleration = 0.05f;
			float verticalAcceleration = 0.01f;
			float maxVelocity = 6f;
			float maxYSpeed = 3f;
			float turnAroundXDist = 30f;
			float turnAroundYDist = 100f;
			float targetXDist = Math.Abs(base.NPC.position.X + (float)(base.NPC.width / 2) - (Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2)));
			float targetYDist = Main.player[base.NPC.target].position.Y - (float)(base.NPC.height / 2);
			if (base.NPC.type == 581 || base.NPC.type == 509)
			{
				acceleration = 0.09f;
				verticalAcceleration = 0.03f;
				maxVelocity = 9f;
				maxYSpeed = 6f;
				turnAroundXDist = 40f;
				turnAroundYDist = 150f;
				targetYDist = Main.player[base.NPC.target].Center.Y - (float)(base.NPC.height / 2);
				base.NPC.rotation = base.NPC.velocity.X * 0.1f;
				for (int p = 0; p < Main.maxNPCs; p++)
				{
					if (p != base.NPC.whoAmI && Main.npc[p].active && Main.npc[p].type == base.NPC.type && Math.Abs(base.NPC.position.X - Main.npc[p].position.X) + Math.Abs(base.NPC.position.Y - Main.npc[p].position.Y) < (float)base.NPC.width)
					{
						if (base.NPC.position.X < Main.npc[p].position.X)
						{
							base.NPC.velocity.X = base.NPC.velocity.X - 0.05f;
						}
						else
						{
							base.NPC.velocity.X = base.NPC.velocity.X + 0.05f;
						}
						if (base.NPC.position.Y < Main.npc[p].position.Y)
						{
							base.NPC.velocity.Y = base.NPC.velocity.Y - 0.05f;
						}
						else
						{
							base.NPC.velocity.Y = base.NPC.velocity.Y + 0.05f;
						}
					}
				}
			}
			else if (base.NPC.type == 587)
			{
				acceleration = 0.16f;
				verticalAcceleration = 0.12f;
				maxVelocity = 9f;
				maxYSpeed = 5f;
				turnAroundXDist = 0f;
				turnAroundYDist = 250f;
				targetYDist = Main.player[base.NPC.target].position.Y;
				if (Main.dayTime)
				{
					targetYDist = 0f;
					base.NPC.direction *= -1;
				}
			}
			if (CalamityWorld.death)
			{
				maxVelocity *= 1.25f;
				acceleration *= 1.25f;
			}
			if (base.NPC.ai[0] <= 0f)
			{
				maxVelocity *= 0.8f;
				acceleration *= 0.7f;
				targetYDist = base.NPC.Center.Y + (float)(base.NPC.directionY * 1000);
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.direction = -1;
				}
				else if (base.NPC.velocity.X > 0f || base.NPC.direction == 0)
				{
					base.NPC.direction = 1;
				}
			}
			if (targetXDist > turnAroundXDist)
			{
				if (base.NPC.direction == -1 && base.NPC.velocity.X > 0f - maxVelocity)
				{
					base.NPC.velocity.X = base.NPC.velocity.X - acceleration;
					if (base.NPC.velocity.X > maxVelocity)
					{
						base.NPC.velocity.X = base.NPC.velocity.X - acceleration;
					}
					else if (base.NPC.velocity.X > 0f)
					{
						base.NPC.velocity.X = base.NPC.velocity.X - acceleration / 2f;
					}
					if (base.NPC.velocity.X < 0f - maxVelocity)
					{
						base.NPC.velocity.X = 0f - maxVelocity;
					}
				}
				else if (base.NPC.direction == 1 && base.NPC.velocity.X < maxVelocity)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + acceleration;
					if (base.NPC.velocity.X < 0f - maxVelocity)
					{
						base.NPC.velocity.X = base.NPC.velocity.X + acceleration;
					}
					else if (base.NPC.velocity.X < 0f)
					{
						base.NPC.velocity.X = base.NPC.velocity.X + acceleration / 2f;
					}
					if (base.NPC.velocity.X > maxVelocity)
					{
						base.NPC.velocity.X = maxVelocity;
					}
				}
			}
			if (targetXDist > turnAroundYDist)
			{
				targetYDist -= turnAroundYDist / 2f;
			}
			if (base.NPC.position.Y < targetYDist)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + verticalAcceleration;
				if (base.NPC.velocity.Y < 0f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + verticalAcceleration;
				}
			}
			else
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - verticalAcceleration;
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - verticalAcceleration;
				}
			}
			if (base.NPC.velocity.Y < 0f - maxYSpeed)
			{
				base.NPC.velocity.Y = 0f - maxYSpeed;
			}
			if (base.NPC.velocity.Y > maxYSpeed)
			{
				base.NPC.velocity.Y = maxYSpeed;
			}
			if (base.NPC.wet && base.NPC.type != 587)
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y * 0.95f;
				}
				base.NPC.velocity.Y = base.NPC.velocity.Y - 0.7f;
				if (base.NPC.velocity.Y < -6f)
				{
					base.NPC.velocity.Y = -6f;
				}
			}
			return false;
		}
	}

	public class FlyingWeaponAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_00da: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0237: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0301: Unknown result type (might be due to invalid IL or missing references)
			//IL_024f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0254: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.knockBackResist = 0f;
			base.NPC.noGravity = true;
			base.NPC.noTileCollide = true;
			if (base.NPC.type == 84)
			{
				Lighting.AddLight((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f), 0.2f, 0.05f, 0.3f);
			}
			else if (base.NPC.type == 179)
			{
				Lighting.AddLight((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f), 0.3f, 0.15f, 0.05f);
			}
			else
			{
				Lighting.AddLight((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f), 0.05f, 0.2f, 0.3f);
			}
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead)
			{
				base.NPC.TargetClosest();
			}
			if (base.NPC.ai[0] == 0f)
			{
				float chargeSpeed = (CalamityWorld.death ? 16f : 12f);
				base.NPC.velocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center, -Vector2.UnitY) * chargeSpeed;
				base.NPC.rotation = base.NPC.velocity.ToRotation() + (float)Math.PI / 4f;
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
				return false;
			}
			if (base.NPC.ai[0] == 1f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= (CalamityWorld.death ? 0.98f : 0.99f);
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] >= (CalamityWorld.death ? 50f : 100f))
				{
					base.NPC.netUpdate = true;
					base.NPC.ai[0] = 2f;
					base.NPC.ai[1] = 0f;
					base.NPC.velocity = Vector2.Zero;
				}
			}
			else
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= (CalamityWorld.death ? 0.94f : 0.96f);
				base.NPC.ai[1]++;
				float anglularSpeed = base.NPC.ai[1] / (CalamityWorld.death ? 90f : 150f);
				anglularSpeed = 0.1f + anglularSpeed * 0.4f;
				base.NPC.rotation += anglularSpeed * (float)base.NPC.direction;
				if (base.NPC.ai[1] >= (CalamityWorld.death ? 90f : 150f))
				{
					base.NPC.netUpdate = true;
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
				}
			}
			return false;
		}
	}

	public class GraniteElementalAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0199: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0503: Unknown result type (might be due to invalid IL or missing references)
			//IL_0508: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0606: Unknown result type (might be due to invalid IL or missing references)
			//IL_0611: Unknown result type (might be due to invalid IL or missing references)
			//IL_0616: Unknown result type (might be due to invalid IL or missing references)
			//IL_061b: Unknown result type (might be due to invalid IL or missing references)
			//IL_06fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0705: Unknown result type (might be due to invalid IL or missing references)
			//IL_070a: Unknown result type (might be due to invalid IL or missing references)
			//IL_070f: Unknown result type (might be due to invalid IL or missing references)
			//IL_072f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0733: Unknown result type (might be due to invalid IL or missing references)
			//IL_0738: Unknown result type (might be due to invalid IL or missing references)
			//IL_0746: Unknown result type (might be due to invalid IL or missing references)
			//IL_0753: Unknown result type (might be due to invalid IL or missing references)
			//IL_0758: Unknown result type (might be due to invalid IL or missing references)
			//IL_075a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0761: Unknown result type (might be due to invalid IL or missing references)
			//IL_0766: Unknown result type (might be due to invalid IL or missing references)
			//IL_0540: Unknown result type (might be due to invalid IL or missing references)
			//IL_0544: Unknown result type (might be due to invalid IL or missing references)
			//IL_0549: Unknown result type (might be due to invalid IL or missing references)
			//IL_0557: Unknown result type (might be due to invalid IL or missing references)
			//IL_0561: Unknown result type (might be due to invalid IL or missing references)
			//IL_0566: Unknown result type (might be due to invalid IL or missing references)
			//IL_0568: Unknown result type (might be due to invalid IL or missing references)
			//IL_0570: Unknown result type (might be due to invalid IL or missing references)
			//IL_0575: Unknown result type (might be due to invalid IL or missing references)
			//IL_0580: Unknown result type (might be due to invalid IL or missing references)
			//IL_0598: Unknown result type (might be due to invalid IL or missing references)
			//IL_0643: Unknown result type (might be due to invalid IL or missing references)
			//IL_0647: Unknown result type (might be due to invalid IL or missing references)
			//IL_064c: Unknown result type (might be due to invalid IL or missing references)
			//IL_065a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0664: Unknown result type (might be due to invalid IL or missing references)
			//IL_0669: Unknown result type (might be due to invalid IL or missing references)
			//IL_066b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0673: Unknown result type (might be due to invalid IL or missing references)
			//IL_0678: Unknown result type (might be due to invalid IL or missing references)
			//IL_0254: Unknown result type (might be due to invalid IL or missing references)
			//IL_0259: Unknown result type (might be due to invalid IL or missing references)
			//IL_026d: Unknown result type (might be due to invalid IL or missing references)
			//IL_027c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0283: Unknown result type (might be due to invalid IL or missing references)
			//IL_0288: Unknown result type (might be due to invalid IL or missing references)
			//IL_028d: Unknown result type (might be due to invalid IL or missing references)
			//IL_068d: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_03df: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_0406: Unknown result type (might be due to invalid IL or missing references)
			//IL_040d: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0311: Unknown result type (might be due to invalid IL or missing references)
			//IL_07be: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_047f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0485: Unknown result type (might be due to invalid IL or missing references)
			//IL_048a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0492: Unknown result type (might be due to invalid IL or missing references)
			//IL_0497: Unknown result type (might be due to invalid IL or missing references)
			//IL_0498: Unknown result type (might be due to invalid IL or missing references)
			//IL_049d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0435: Unknown result type (might be due to invalid IL or missing references)
			//IL_0448: Unknown result type (might be due to invalid IL or missing references)
			//IL_0337: Unknown result type (might be due to invalid IL or missing references)
			//IL_033e: Unknown result type (might be due to invalid IL or missing references)
			//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0905: Unknown result type (might be due to invalid IL or missing references)
			//IL_090c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0911: Unknown result type (might be due to invalid IL or missing references)
			//IL_034c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0361: Unknown result type (might be due to invalid IL or missing references)
			//IL_0931: Unknown result type (might be due to invalid IL or missing references)
			//IL_0936: Unknown result type (might be due to invalid IL or missing references)
			//IL_093f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0943: Unknown result type (might be due to invalid IL or missing references)
			//IL_0948: Unknown result type (might be due to invalid IL or missing references)
			//IL_0956: Unknown result type (might be due to invalid IL or missing references)
			//IL_0960: Unknown result type (might be due to invalid IL or missing references)
			//IL_0965: Unknown result type (might be due to invalid IL or missing references)
			//IL_0967: Unknown result type (might be due to invalid IL or missing references)
			//IL_0971: Unknown result type (might be due to invalid IL or missing references)
			//IL_0976: Unknown result type (might be due to invalid IL or missing references)
			//IL_0390: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_09eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a42: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a51: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a56: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a99: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a9e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0acf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b57: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b5c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b71: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b86: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b8d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ba9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b03: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b0a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bed: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b33: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b47: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.noGravity = true;
			base.NPC.noTileCollide = false;
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.defense = base.NPC.defDefense;
			if (base.NPC.justHit && Main.netMode != 1 && Main.rand.NextBool(10))
			{
				base.NPC.netUpdate = true;
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 0f;
			}
			if (base.NPC.ai[0] == -1f)
			{
				base.NPC.damage = 0;
				base.NPC.defense = base.NPC.defDefense + 10;
				base.NPC.noGravity = false;
				base.NPC.velocity.X *= 0.98f;
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] >= 120f)
				{
					base.NPC.ai[0] = (base.NPC.ai[1] = (base.NPC.ai[2] = (base.NPC.ai[3] = 0f)));
				}
			}
			else if (base.NPC.ai[0] == 0f)
			{
				base.NPC.TargetClosest();
				if (Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					base.NPC.ai[0] = 1f;
					return false;
				}
				Vector2 targetDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
				targetDirection.Y -= Main.player[base.NPC.target].height / 4;
				if (((Vector2)(ref targetDirection)).Length() > (CalamityWorld.death ? 400f : 800f))
				{
					base.NPC.ai[0] = 2f;
					return false;
				}
				Vector2 elementalCenter = base.NPC.Center;
				elementalCenter.X = Main.player[base.NPC.target].Center.X;
				Vector2 targetDistance = elementalCenter - base.NPC.Center;
				if (((Vector2)(ref targetDistance)).Length() > 8f && Collision.CanHit(base.NPC.Center, 1, 1, elementalCenter, 1, 1))
				{
					base.NPC.ai[0] = 3f;
					base.NPC.ai[1] = elementalCenter.X;
					base.NPC.ai[2] = elementalCenter.Y;
					Vector2 elementalCenter2 = base.NPC.Center;
					elementalCenter2.Y = Main.player[base.NPC.target].Center.Y;
					if (((Vector2)(ref targetDistance)).Length() > 8f && Collision.CanHit(base.NPC.Center, 1, 1, elementalCenter2, 1, 1) && Collision.CanHit(elementalCenter2, 1, 1, Main.player[base.NPC.target].position, 1, 1))
					{
						base.NPC.ai[0] = 3f;
						base.NPC.ai[1] = elementalCenter2.X;
						base.NPC.ai[2] = elementalCenter2.Y;
					}
				}
				else
				{
					elementalCenter = base.NPC.Center;
					elementalCenter.Y = Main.player[base.NPC.target].Center.Y;
					Vector2 val = elementalCenter - base.NPC.Center;
					if (((Vector2)(ref val)).Length() > 8f && Collision.CanHit(base.NPC.Center, 1, 1, elementalCenter, 1, 1))
					{
						base.NPC.ai[0] = 3f;
						base.NPC.ai[1] = elementalCenter.X;
						base.NPC.ai[2] = elementalCenter.Y;
					}
				}
				if (base.NPC.ai[0] == 0f)
				{
					base.NPC.localAI[0] = 0f;
					((Vector2)(ref targetDirection)).Normalize();
					targetDirection *= 0.5f;
					NPC nPC = base.NPC;
					nPC.velocity += targetDirection;
					base.NPC.ai[0] = 4f;
					base.NPC.ai[1] = 0f;
				}
			}
			else if (base.NPC.ai[0] == 1f)
			{
				Vector2 targetDirectionAgain = Main.player[base.NPC.target].Center - base.NPC.Center;
				float attackTimeMax2 = ((Vector2)(ref targetDirectionAgain)).Length();
				float attackTimeMax3 = 2f;
				attackTimeMax3 += attackTimeMax2 / (CalamityWorld.death ? 160f : 180f);
				int attackTimeMax4 = 50;
				((Vector2)(ref targetDirectionAgain)).Normalize();
				targetDirectionAgain *= attackTimeMax3;
				base.NPC.velocity = (base.NPC.velocity * (float)(attackTimeMax4 - 1) + targetDirectionAgain) / (float)attackTimeMax4;
				if (!Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
				}
			}
			else if (base.NPC.ai[0] == 2f)
			{
				base.NPC.noTileCollide = true;
				Vector2 targetDirection3 = Main.player[base.NPC.target].Center - base.NPC.Center;
				float num = ((Vector2)(ref targetDirection3)).Length();
				float scaleFactor23 = (CalamityWorld.death ? 3f : 2.5f);
				int attackTimeMax6 = 4;
				((Vector2)(ref targetDirection3)).Normalize();
				targetDirection3 *= scaleFactor23;
				base.NPC.velocity = (base.NPC.velocity * (float)(attackTimeMax6 - 1) + targetDirection3) / (float)attackTimeMax6;
				if (num < 600f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.ai[0] = 0f;
				}
			}
			else if (base.NPC.ai[0] == 3f)
			{
				Vector2 elementalDirection = new Vector2(base.NPC.ai[1], base.NPC.ai[2]) - base.NPC.Center;
				float attackTimeMax7 = ((Vector2)(ref elementalDirection)).Length();
				float attackTimeMax8 = 2f;
				float attackTimeMax9 = 3f;
				((Vector2)(ref elementalDirection)).Normalize();
				elementalDirection *= attackTimeMax8;
				base.NPC.velocity = (base.NPC.velocity * (attackTimeMax9 - 1f) + elementalDirection) / attackTimeMax9;
				if (base.NPC.collideX || base.NPC.collideY)
				{
					base.NPC.ai[0] = 4f;
					base.NPC.ai[1] = 0f;
				}
				if (attackTimeMax7 < attackTimeMax8 || attackTimeMax7 > 800f || Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					base.NPC.ai[0] = 0f;
				}
			}
			else if (base.NPC.ai[0] == 4f)
			{
				if (base.NPC.collideX)
				{
					base.NPC.velocity.X = base.NPC.velocity.X * -0.8f;
				}
				if (base.NPC.collideY)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y * -0.8f;
				}
				Vector2 stationaryTargetDist;
				if (base.NPC.velocity.X == 0f && base.NPC.velocity.Y == 0f)
				{
					stationaryTargetDist = Main.player[base.NPC.target].Center - base.NPC.Center;
					stationaryTargetDist.Y -= Main.player[base.NPC.target].height / 4;
					((Vector2)(ref stationaryTargetDist)).Normalize();
					base.NPC.velocity = stationaryTargetDist * 0.1f;
				}
				float scaleFactor24 = (CalamityWorld.death ? 2.5f : 2f);
				stationaryTargetDist = base.NPC.velocity;
				((Vector2)(ref stationaryTargetDist)).Normalize();
				stationaryTargetDist *= scaleFactor24;
				base.NPC.velocity = (base.NPC.velocity * 19f + stationaryTargetDist) / 20f;
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] > 180f)
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
				}
				if (Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					base.NPC.ai[0] = 0f;
				}
				base.NPC.localAI[0]++;
				if (base.NPC.localAI[0] >= 5f && !Collision.SolidCollision(base.NPC.position - new Vector2(10f, 10f), base.NPC.width + 20, base.NPC.height + 20))
				{
					base.NPC.localAI[0] = 0f;
					Vector2 elementalCenter4 = base.NPC.Center;
					elementalCenter4.X = Main.player[base.NPC.target].Center.X;
					if (Collision.CanHit(base.NPC.Center, 1, 1, elementalCenter4, 1, 1) && Collision.CanHit(base.NPC.Center, 1, 1, elementalCenter4, 1, 1) && Collision.CanHit(Main.player[base.NPC.target].Center, 1, 1, elementalCenter4, 1, 1))
					{
						base.NPC.ai[0] = 3f;
						base.NPC.ai[1] = elementalCenter4.X;
						base.NPC.ai[2] = elementalCenter4.Y;
						return false;
					}
					elementalCenter4 = base.NPC.Center;
					elementalCenter4.Y = Main.player[base.NPC.target].Center.Y;
					if (Collision.CanHit(base.NPC.Center, 1, 1, elementalCenter4, 1, 1) && Collision.CanHit(Main.player[base.NPC.target].Center, 1, 1, elementalCenter4, 1, 1))
					{
						base.NPC.ai[0] = 3f;
						base.NPC.ai[1] = elementalCenter4.X;
						base.NPC.ai[2] = elementalCenter4.Y;
					}
				}
			}
			return false;
		}
	}

	public class HerplingAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0202: Unknown result type (might be due to invalid IL or missing references)
			//IL_0207: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_0108: Unknown result type (might be due to invalid IL or missing references)
			//IL_0113: Unknown result type (might be due to invalid IL or missing references)
			//IL_0118: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0128: Unknown result type (might be due to invalid IL or missing references)
			//IL_012d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0142: Unknown result type (might be due to invalid IL or missing references)
			//IL_0148: Unknown result type (might be due to invalid IL or missing references)
			//IL_016f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0179: Unknown result type (might be due to invalid IL or missing references)
			//IL_017e: Unknown result type (might be due to invalid IL or missing references)
			//IL_017f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0189: Unknown result type (might be due to invalid IL or missing references)
			//IL_018e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0193: Unknown result type (might be due to invalid IL or missing references)
			//IL_0199: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01db: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0669: Unknown result type (might be due to invalid IL or missing references)
			//IL_067f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0611: Unknown result type (might be due to invalid IL or missing references)
			//IL_061c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0621: Unknown result type (might be due to invalid IL or missing references)
			//IL_0626: Unknown result type (might be due to invalid IL or missing references)
			//IL_031d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0348: Unknown result type (might be due to invalid IL or missing references)
			//IL_034e: Unknown result type (might be due to invalid IL or missing references)
			//IL_038d: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_03de: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_040e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0438: Unknown result type (might be due to invalid IL or missing references)
			//IL_043e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0455: Unknown result type (might be due to invalid IL or missing references)
			//IL_045f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0464: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a6b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0498: Unknown result type (might be due to invalid IL or missing references)
			//IL_04db: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0501: Unknown result type (might be due to invalid IL or missing references)
			//IL_0526: Unknown result type (might be due to invalid IL or missing references)
			//IL_0530: Unknown result type (might be due to invalid IL or missing references)
			//IL_0535: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d0a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d15: Unknown result type (might be due to invalid IL or missing references)
			if (base.NPC.ai[2] > 1f)
			{
				base.NPC.ai[2]--;
			}
			if (base.NPC.ai[2] == 0f)
			{
				base.NPC.ai[0] = -100f;
				base.NPC.ai[2] = 1f;
				base.NPC.TargetClosest();
				base.NPC.spriteDirection = base.NPC.direction;
			}
			if (base.NPC.type == 378)
			{
				Vector2 dustOffset = default(Vector2);
				((Vector2)(ref dustOffset))._002Ector(-6f, -10f);
				dustOffset.X *= base.NPC.spriteDirection;
				if (base.NPC.ai[1] != 5f && Main.rand.NextBool(3))
				{
					NPC nPC = base.NPC;
					nPC.position += base.NPC.netOffset;
					int dustID = Dust.NewDust(base.NPC.Center + dustOffset - Vector2.One * 5f, 4, 4, 6);
					Dust obj = Main.dust[dustID];
					obj.scale = 1.5f;
					obj.noGravity = true;
					obj.velocity = obj.velocity * 0.25f + Vector2.Normalize(dustOffset) * 1f;
					obj.velocity = obj.velocity.RotatedBy(-(float)Math.PI / 2f * (float)base.NPC.direction);
					NPC nPC2 = base.NPC;
					nPC2.position -= base.NPC.netOffset;
				}
				if (base.NPC.ai[1] == 5f)
				{
					base.NPC.velocity = Vector2.Zero;
					base.NPC.position.X += base.NPC.width / 2;
					base.NPC.position.Y += base.NPC.height / 2;
					base.NPC.width = 160;
					base.NPC.height = 160;
					base.NPC.position.X -= base.NPC.width / 2;
					base.NPC.position.Y -= base.NPC.height / 2;
					base.NPC.dontTakeDamage = true;
					NPC nPC3 = base.NPC;
					nPC3.position += base.NPC.netOffset;
					if (base.NPC.ai[2] > 7f)
					{
						for (int i = 0; i < 8; i++)
						{
							Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y), base.NPC.width, base.NPC.height, 31, 0f, 0f, 100, default(Color), 1.5f);
						}
						for (int j = 0; j < 32; j++)
						{
							int dustID2 = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y), base.NPC.width, base.NPC.height, 6, 0f, 0f, 100, default(Color), 2.5f);
							Dust obj2 = Main.dust[dustID2];
							obj2.velocity *= 3f;
							obj2.noGravity = true;
							dustID2 = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.position.Y), base.NPC.width, base.NPC.height, 6, 0f, 0f, 100, default(Color), 1.5f);
							Dust obj3 = Main.dust[dustID2];
							obj3.velocity *= 2f;
							obj3.noGravity = true;
						}
						for (int k = 0; k < 2; k++)
						{
							int goreID = Gore.NewGore(base.NPC.GetSource_FromThis(), base.NPC.position + new Vector2((float)(base.NPC.width * Main.rand.Next(100)) / 100f, (float)(base.NPC.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64));
							Gore obj4 = Main.gore[goreID];
							obj4.velocity *= 0.3f;
							obj4.velocity.X += (float)Main.rand.Next(-10, 11) * 0.05f;
							obj4.velocity.Y += (float)Main.rand.Next(-10, 11) * 0.05f;
						}
						if (base.NPC.ai[2] == 9f)
						{
							SoundEngine.PlaySound(in SoundID.Item14, base.NPC.position);
						}
					}
					if (base.NPC.ai[2] == 1f)
					{
						base.NPC.life = -1;
						base.NPC.HitEffect();
						base.NPC.active = false;
					}
					NPC nPC4 = base.NPC;
					nPC4.position -= base.NPC.netOffset;
					return false;
				}
			}
			if (base.NPC.type == 378 && base.NPC.ai[1] != 5f)
			{
				if (base.NPC.wet || Vector2.Distance(base.NPC.Center, Main.player[base.NPC.target].Center) < 64f)
				{
					base.NPC.ai[1] = 5f;
					base.NPC.ai[2] = 10f;
					base.NPC.netUpdate = true;
					return false;
				}
			}
			else if (base.NPC.wet && base.NPC.type != 177)
			{
				if (base.NPC.collideX)
				{
					base.NPC.direction *= -1;
					base.NPC.spriteDirection = base.NPC.direction;
				}
				if (base.NPC.collideY)
				{
					base.NPC.TargetClosest();
					if (base.NPC.oldVelocity.Y < 0f)
					{
						base.NPC.velocity.Y = 5f;
					}
					else
					{
						base.NPC.velocity.Y = base.NPC.velocity.Y - 2f;
					}
					base.NPC.spriteDirection = base.NPC.direction;
				}
				if (base.NPC.velocity.Y > 4f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y * 0.9f;
				}
				base.NPC.velocity.Y = base.NPC.velocity.Y - 0.45f;
				if (base.NPC.velocity.Y < -6f)
				{
					base.NPC.velocity.Y = -6f;
				}
			}
			base.NPC.damage = ((base.NPC.velocity.Y != 0f && !(((Vector2)(ref base.NPC.velocity)).Length() < 3f)) ? base.NPC.defDamage : 0);
			if (base.NPC.velocity.Y == 0f)
			{
				if (base.NPC.ai[3] == base.NPC.position.X)
				{
					base.NPC.direction *= -1;
					base.NPC.ai[2] = 300f;
				}
				base.NPC.ai[3] = 0f;
				base.NPC.velocity.X = base.NPC.velocity.X * 0.8f;
				if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
				{
					base.NPC.velocity.X = 0f;
				}
				if (base.NPC.type == 177)
				{
					base.NPC.ai[0] += 3f;
				}
				else
				{
					base.NPC.ai[0] += 10f;
				}
				Vector2 herplingPosition = default(Vector2);
				((Vector2)(ref herplingPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
				float num = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - herplingPosition.X;
				float herplingTargetY = Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height * 0.5f - herplingPosition.Y;
				float herplingTargetDist = (float)Math.Sqrt(num * num + herplingTargetY * herplingTargetY);
				float herplingJumpHeight = 400f / herplingTargetDist;
				herplingJumpHeight = ((base.NPC.type != 177) ? (herplingJumpHeight * 10f) : (herplingJumpHeight * 5f));
				if (herplingJumpHeight > 30f)
				{
					herplingJumpHeight = 30f;
				}
				base.NPC.ai[0] += (int)herplingJumpHeight;
				if (base.NPC.ai[0] >= 0f)
				{
					base.NPC.netUpdate = true;
					if (base.NPC.ai[2] == 1f)
					{
						base.NPC.TargetClosest();
					}
					if (base.NPC.type == 177)
					{
						if (base.NPC.ai[1] == 2f)
						{
							base.NPC.velocity.Y = -14f;
							base.NPC.velocity.X = base.NPC.velocity.X + 3f * (float)base.NPC.direction;
							if (herplingTargetDist < 350f && herplingTargetDist > 200f)
							{
								base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction;
							}
							base.NPC.ai[0] = (CalamityWorld.death ? (-100f) : (-200f));
							base.NPC.ai[1] = 0f;
							base.NPC.ai[3] = base.NPC.position.X;
						}
						else
						{
							base.NPC.velocity.Y = -10f;
							base.NPC.velocity.X = base.NPC.velocity.X + (float)(5 * base.NPC.direction);
							if (herplingTargetDist < 350f && herplingTargetDist > 200f)
							{
								base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction;
							}
							base.NPC.ai[0] = (CalamityWorld.death ? (-60f) : (-120f));
							base.NPC.ai[1]++;
						}
					}
					else
					{
						if (base.NPC.type == 378)
						{
							SoundEngine.PlaySound(in SoundID.Zombie124, base.NPC.position);
						}
						if (base.NPC.ai[1] == 3f)
						{
							base.NPC.velocity.Y = -9f;
							base.NPC.velocity.X = base.NPC.velocity.X + (float)(2 * base.NPC.direction);
							if (herplingTargetDist < 350f && herplingTargetDist > 200f)
							{
								base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction;
							}
							base.NPC.ai[0] = (CalamityWorld.death ? (-100f) : (-200f));
							base.NPC.ai[1] = 0f;
							base.NPC.ai[3] = base.NPC.position.X;
						}
						else
						{
							base.NPC.velocity.Y = -5f;
							base.NPC.velocity.X = base.NPC.velocity.X + (float)(4 * base.NPC.direction);
							if (herplingTargetDist < 350f && herplingTargetDist > 200f)
							{
								base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction;
							}
							base.NPC.ai[0] = (CalamityWorld.death ? (-60f) : (-120f));
							base.NPC.ai[1]++;
						}
					}
				}
				else if (base.NPC.ai[0] >= -30f)
				{
					base.NPC.aiAction = 1;
				}
				base.NPC.spriteDirection = base.NPC.direction;
				return false;
			}
			if (base.NPC.target < 255)
			{
				if (base.NPC.type == 177)
				{
					bool derplingDropOnTarget = false;
					if (base.NPC.position.Y + (float)base.NPC.height < Main.player[base.NPC.target].position.Y && base.NPC.position.X + (float)base.NPC.width > Main.player[base.NPC.target].position.X && base.NPC.position.X < Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width)
					{
						derplingDropOnTarget = true;
						base.NPC.velocity.X = base.NPC.velocity.X * 0.9f;
						if (base.NPC.velocity.Y < 0f)
						{
							base.NPC.velocity.Y = base.NPC.velocity.Y * 0.9f;
							base.NPC.velocity.Y = base.NPC.velocity.Y + 0.15f;
						}
					}
					if (!derplingDropOnTarget && ((base.NPC.direction == 1 && base.NPC.velocity.X < 4f) || (base.NPC.direction == -1 && base.NPC.velocity.X > -4f)))
					{
						if ((base.NPC.direction == -1 && (double)base.NPC.velocity.X < 0.1) || (base.NPC.direction == 1 && (double)base.NPC.velocity.X > -0.1))
						{
							base.NPC.velocity.X = base.NPC.velocity.X + 0.3f * (float)base.NPC.direction;
							return false;
						}
						base.NPC.velocity.X = base.NPC.velocity.X * 0.9f;
					}
				}
				else if ((base.NPC.direction == 1 && base.NPC.velocity.X < 3f) || (base.NPC.direction == -1 && base.NPC.velocity.X > -3f))
				{
					if ((base.NPC.direction == -1 && (double)base.NPC.velocity.X < 0.1) || (base.NPC.direction == 1 && (double)base.NPC.velocity.X > -0.1))
					{
						base.NPC.velocity.X = base.NPC.velocity.X + 0.3f * (float)base.NPC.direction;
						return false;
					}
					base.NPC.velocity.X = base.NPC.velocity.X * 0.9f;
				}
			}
			return false;
		}
	}

	public class HoveringAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0447: Unknown result type (might be due to invalid IL or missing references)
			//IL_0486: Unknown result type (might be due to invalid IL or missing references)
			//IL_071e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0750: Unknown result type (might be due to invalid IL or missing references)
			//IL_076a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0774: Unknown result type (might be due to invalid IL or missing references)
			//IL_0779: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cb8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0939: Unknown result type (might be due to invalid IL or missing references)
			//IL_0978: Unknown result type (might be due to invalid IL or missing references)
			//IL_0845: Unknown result type (might be due to invalid IL or missing references)
			//IL_0873: Unknown result type (might be due to invalid IL or missing references)
			//IL_0879: Unknown result type (might be due to invalid IL or missing references)
			//IL_0890: Unknown result type (might be due to invalid IL or missing references)
			//IL_089a: Unknown result type (might be due to invalid IL or missing references)
			//IL_089f: Unknown result type (might be due to invalid IL or missing references)
			//IL_079a: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_053f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0546: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f38: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f43: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f48: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f4d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a63: Unknown result type (might be due to invalid IL or missing references)
			//IL_0633: Unknown result type (might be due to invalid IL or missing references)
			//IL_065f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dba: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dcf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0adf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b0d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b13: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b27: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b31: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b36: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e36: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e84: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ecf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ed6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c1b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c22: Unknown result type (might be due to invalid IL or missing references)
			bool hoverDownDistCheck = false;
			bool runAway = base.NPC.type == 330 && !Main.pumpkinMoon;
			if (base.NPC.type == 253 && !Main.eclipse)
			{
				runAway = true;
			}
			if (base.NPC.type == 490 && Main.dayTime)
			{
				runAway = true;
			}
			if (!runAway)
			{
				if (base.NPC.ai[2] >= 0f)
				{
					int hoverDistance = 16;
					bool changeDirectionX = false;
					bool changeDirectionY = false;
					if (base.NPC.position.X > base.NPC.ai[0] - (float)hoverDistance && base.NPC.position.X < base.NPC.ai[0] + (float)hoverDistance)
					{
						changeDirectionX = true;
					}
					else if ((base.NPC.velocity.X < 0f && base.NPC.direction > 0) || (base.NPC.velocity.X > 0f && base.NPC.direction < 0))
					{
						changeDirectionX = true;
					}
					hoverDistance += 24;
					if (base.NPC.position.Y > base.NPC.ai[1] - (float)hoverDistance && base.NPC.position.Y < base.NPC.ai[1] + (float)hoverDistance)
					{
						changeDirectionY = true;
					}
					if (changeDirectionX & changeDirectionY)
					{
						base.NPC.ai[2]++;
						if (base.NPC.ai[2] >= 40f)
						{
							base.NPC.ai[2] = -200f;
							base.NPC.direction *= -1;
							base.NPC.velocity.X = base.NPC.velocity.X * -1f;
							base.NPC.collideX = false;
						}
					}
					else
					{
						base.NPC.ai[0] = base.NPC.position.X;
						base.NPC.ai[1] = base.NPC.position.Y;
						base.NPC.ai[2] = 0f;
					}
					base.NPC.TargetClosest();
				}
				else if (base.NPC.type == 253)
				{
					base.NPC.TargetClosest();
					base.NPC.ai[2] += 30f;
				}
				else
				{
					if (base.NPC.type == 330)
					{
						base.NPC.ai[2] += 5f;
					}
					else
					{
						base.NPC.ai[2] += 15f;
					}
					if (Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) > base.NPC.position.X + (float)(base.NPC.width / 2))
					{
						base.NPC.direction = -1;
					}
					else
					{
						base.NPC.direction = 1;
					}
				}
			}
			int npcTileX = (int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f) + base.NPC.direction * 2;
			int npcTileY = (int)((base.NPC.position.Y + (float)base.NPC.height) / 16f);
			bool hoverDownwards = true;
			bool canOpenDoor = false;
			int tileCheckLoopAmt = 6;
			if (base.NPC.type == 122)
			{
				Vector2 gastropodPosition = default(Vector2);
				((Vector2)(ref gastropodPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
				float gastropodTargetX = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) - gastropodPosition.X;
				float gastropodTargetY = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2) - gastropodPosition.Y;
				float gastropodTargetDist = (float)Math.Sqrt(gastropodTargetX * gastropodTargetX + gastropodTargetY * gastropodTargetY);
				gastropodTargetDist = 6f / gastropodTargetDist;
				gastropodTargetX *= gastropodTargetDist;
				gastropodTargetY *= gastropodTargetDist;
				if (base.NPC.justHit)
				{
					base.NPC.localAI[1] = 0f;
					base.NPC.ai[3] = 0f;
				}
				if (Main.netMode != 1 && base.NPC.ai[3] == 32f && !Main.player[base.NPC.target].npcTypeNoAggro[base.NPC.type])
				{
					int damage = 25;
					int projType = 84;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), gastropodPosition.X, gastropodPosition.Y, gastropodTargetX, gastropodTargetY, projType, damage, 0f, Main.myPlayer);
				}
				tileCheckLoopAmt = 12;
				if (base.NPC.ai[3] > 0f)
				{
					base.NPC.ai[3]++;
					if (base.NPC.ai[3] >= 64f)
					{
						base.NPC.ai[3] = 0f;
					}
				}
				if (Main.netMode != 1 && base.NPC.ai[3] == 0f)
				{
					base.NPC.localAI[1]++;
					if (base.NPC.localAI[1] > (CalamityWorld.death ? 60f : 120f) && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) && !Main.player[base.NPC.target].npcTypeNoAggro[base.NPC.type])
					{
						base.NPC.localAI[1] = 0f;
						base.NPC.ai[3] = 1f;
						base.NPC.netUpdate = true;
					}
				}
			}
			else if (base.NPC.type == 75)
			{
				tileCheckLoopAmt = 8;
				if (Main.rand.NextBool(6))
				{
					int pixieDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 55, 0f, 0f, 200, base.NPC.color);
					Dust obj = Main.dust[pixieDust];
					obj.velocity *= 0.3f;
				}
				if (Main.rand.NextBool(40))
				{
					SoundEngine.PlaySound(in SoundID.Pixie, base.NPC.Center);
				}
			}
			else if (base.NPC.type == 169)
			{
				Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0f, 0.6f, 0.75f);
				base.NPC.alpha = 30;
				if (Main.rand.NextBool(3))
				{
					int iceEleDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 92, 0f, 0f, 200);
					Dust obj2 = Main.dust[iceEleDust];
					obj2.velocity *= 0.3f;
					Main.dust[iceEleDust].noGravity = true;
				}
				Vector2 iceElementalPosition = default(Vector2);
				((Vector2)(ref iceElementalPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
				float iceElementalTargetX = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) - iceElementalPosition.X;
				float iceElementalTargetY = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2) - iceElementalPosition.Y;
				float iceElementalTargetDist = (float)Math.Sqrt(iceElementalTargetX * iceElementalTargetX + iceElementalTargetY * iceElementalTargetY);
				iceElementalTargetDist = 6f / iceElementalTargetDist;
				iceElementalTargetX *= iceElementalTargetDist;
				iceElementalTargetY *= iceElementalTargetDist;
				if (iceElementalTargetX > 0f)
				{
					base.NPC.direction = 1;
				}
				else
				{
					base.NPC.direction = -1;
				}
				base.NPC.spriteDirection = base.NPC.direction;
				if (base.NPC.direction < 0)
				{
					base.NPC.rotation = (float)Math.Atan2(0.0 - (double)iceElementalTargetY, 0.0 - (double)iceElementalTargetX);
				}
				else
				{
					base.NPC.rotation = (float)Math.Atan2(iceElementalTargetY, iceElementalTargetX);
				}
				if (base.NPC.justHit || !Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					base.NPC.localAI[1] = 0f;
				}
				tileCheckLoopAmt = 15;
				if (base.NPC.localAI[1] > (CalamityWorld.death ? 120f : 180f) - 30f)
				{
					Dust dust = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 92, 0f, 0f, 200, default(Color), 2f);
					dust.noGravity = true;
					dust.velocity *= 0f;
				}
				if (Main.netMode != 1)
				{
					base.NPC.localAI[1]++;
					if (base.NPC.localAI[1] > (CalamityWorld.death ? 120f : 180f) && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						base.NPC.localAI[1] = 0f;
						int dmg = 45;
						int projType2 = 128;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), iceElementalPosition.X, iceElementalPosition.Y, iceElementalTargetX, iceElementalTargetY, projType2, dmg, 0f, Main.myPlayer);
						base.NPC.netUpdate = true;
					}
				}
			}
			else if (base.NPC.type == 268)
			{
				base.NPC.rotation = base.NPC.velocity.X * 0.1f;
				tileCheckLoopAmt = ((!(Main.player[base.NPC.target].Center.Y < base.NPC.Center.Y)) ? 9 : 18);
				if (base.NPC.justHit)
				{
					base.NPC.ai[3] = 0f;
				}
				if (Main.netMode != 1 && !base.NPC.confused)
				{
					base.NPC.ai[3]++;
					if (base.NPC.ai[3] >= (CalamityWorld.death ? 60f : (CalamityWorld.revenge ? 90f : 120f)))
					{
						base.NPC.ai[3] = 0f;
						Vector2 ichorStickerPosition = default(Vector2);
						((Vector2)(ref ichorStickerPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f - 4f, base.NPC.position.Y + (float)base.NPC.height * 0.7f);
						if (Collision.CanHit(ichorStickerPosition, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
						{
							float num = (CalamityWorld.death ? 6f : (CalamityWorld.revenge ? 5f : 4f));
							float ichorStickerTargetX = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) - ichorStickerPosition.X;
							float ichorStickerAbsTargetX = Math.Abs(ichorStickerTargetX) * 0.1f;
							float ichorStickerTargetY = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2) - ichorStickerPosition.Y - ichorStickerAbsTargetX;
							float ichorStickerTargetDist = (float)Math.Sqrt(ichorStickerTargetX * ichorStickerTargetX + ichorStickerTargetY * ichorStickerTargetY);
							ichorStickerTargetDist = num / ichorStickerTargetDist;
							ichorStickerTargetX *= ichorStickerTargetDist;
							ichorStickerTargetY *= ichorStickerTargetDist;
							int dmg2 = 40;
							int projType3 = 288;
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), ichorStickerPosition.X, ichorStickerPosition.Y, ichorStickerTargetX, ichorStickerTargetY, projType3, dmg2, 0f, Main.myPlayer);
						}
					}
				}
			}
			if (base.NPC.type == 490)
			{
				tileCheckLoopAmt = 8;
				if (base.NPC.target >= 0)
				{
					Vector2 val = Main.player[base.NPC.target].Center - base.NPC.Center;
					float dripperTargetDist = ((Vector2)(ref val)).Length();
					dripperTargetDist /= 70f;
					if (dripperTargetDist > 8f)
					{
						dripperTargetDist = 8f;
					}
					tileCheckLoopAmt += (int)dripperTargetDist;
				}
			}
			for (int y = npcTileY; y < npcTileY + tileCheckLoopAmt; y++)
			{
				if ((Main.tile[npcTileX, y].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX, y].TileType]) || Main.tile[npcTileX, y].LiquidAmount > 0)
				{
					if (y <= npcTileY + 1)
					{
						canOpenDoor = true;
					}
					hoverDownwards = false;
					break;
				}
			}
			if (Main.player[base.NPC.target].npcTypeNoAggro[base.NPC.type])
			{
				bool canOpenTallGate = false;
				for (int yInc = npcTileY; yInc < npcTileY + tileCheckLoopAmt - 2; yInc++)
				{
					if ((Main.tile[npcTileX, yInc].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX, yInc].TileType]) || Main.tile[npcTileX, yInc].LiquidAmount > 0)
					{
						canOpenTallGate = true;
						break;
					}
				}
				base.NPC.directionY = (!canOpenTallGate).ToDirectionInt();
			}
			if (base.NPC.type == 169 || base.NPC.type == 268)
			{
				for (int iceIchorY = npcTileY - 3; iceIchorY < npcTileY; iceIchorY++)
				{
					if ((Main.tile[npcTileX, iceIchorY].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX, iceIchorY].TileType]) || Main.tile[npcTileX, iceIchorY].LiquidAmount > 0)
					{
						canOpenDoor = false;
						hoverDownDistCheck = true;
						break;
					}
				}
			}
			if (hoverDownDistCheck)
			{
				hoverDownwards = true;
				if (base.NPC.type == 268)
				{
					base.NPC.velocity.Y += (CalamityWorld.revenge ? 3f : 2f);
				}
			}
			if (hoverDownwards)
			{
				if (base.NPC.type == 75 || base.NPC.type == 169)
				{
					base.NPC.velocity.Y += (CalamityWorld.revenge ? 0.3f : 0.2f);
					if (base.NPC.velocity.Y > (CalamityWorld.revenge ? 3f : 2f))
					{
						base.NPC.velocity.Y = (CalamityWorld.revenge ? 3f : 2f);
					}
				}
				else if (base.NPC.type == 490)
				{
					base.NPC.velocity.Y += 0.05f;
					if (base.NPC.velocity.Y > 1f)
					{
						base.NPC.velocity.Y = 1f;
					}
				}
				else
				{
					base.NPC.velocity.Y += 0.15f;
					if (base.NPC.velocity.Y > 4f)
					{
						base.NPC.velocity.Y = 4f;
					}
				}
			}
			else
			{
				if (base.NPC.type == 75 || base.NPC.type == 169)
				{
					if ((base.NPC.directionY < 0 && base.NPC.velocity.Y > 0f) | canOpenDoor)
					{
						base.NPC.velocity.Y -= (CalamityWorld.revenge ? 0.3f : 0.2f);
					}
				}
				else if (base.NPC.type == 490)
				{
					if ((base.NPC.directionY < 0 && base.NPC.velocity.Y > 0f) | canOpenDoor)
					{
						base.NPC.velocity.Y -= 0.1f;
					}
					if (base.NPC.velocity.Y < -1f)
					{
						base.NPC.velocity.Y = -1f;
					}
				}
				else if (base.NPC.directionY < 0 && base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y -= 0.15f;
				}
				if (base.NPC.velocity.Y < 0f - (CalamityWorld.revenge ? 5.5f : 4f))
				{
					base.NPC.velocity.Y = 0f - (CalamityWorld.revenge ? 5.5f : 4f);
				}
			}
			if (base.NPC.type == 75 && base.NPC.wet)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - 0.3f;
				if (base.NPC.velocity.Y < -3f)
				{
					base.NPC.velocity.Y = -3f;
				}
			}
			if (base.NPC.collideX)
			{
				base.NPC.velocity.X = base.NPC.oldVelocity.X * -0.4f;
				if (base.NPC.direction == -1 && base.NPC.velocity.X > 0f && base.NPC.velocity.X < 1f)
				{
					base.NPC.velocity.X = 1f;
				}
				if (base.NPC.direction == 1 && base.NPC.velocity.X < 0f && base.NPC.velocity.X > -1f)
				{
					base.NPC.velocity.X = -1f;
				}
			}
			if (base.NPC.collideY)
			{
				base.NPC.velocity.Y = base.NPC.oldVelocity.Y * -0.25f;
				if (base.NPC.velocity.Y > 0f && base.NPC.velocity.Y < 1f)
				{
					base.NPC.velocity.Y = 1f;
				}
				if (base.NPC.velocity.Y < 0f && base.NPC.velocity.Y > -1f)
				{
					base.NPC.velocity.Y = -1f;
				}
			}
			float maxHoverVel = 2f;
			if (base.NPC.type == 75)
			{
				maxHoverVel = 3f;
			}
			if (base.NPC.type == 253)
			{
				maxHoverVel = 4f;
			}
			if (base.NPC.type == 490)
			{
				maxHoverVel = 1.5f;
			}
			if (CalamityWorld.death)
			{
				maxHoverVel *= 1.25f;
			}
			if (base.NPC.type == 330)
			{
				base.NPC.alpha = 0;
				maxHoverVel = 6f;
				if (!runAway)
				{
					base.NPC.TargetClosest();
				}
				else if (base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
				}
				if (base.NPC.direction < 0 && base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X * 0.8f;
				}
				if (base.NPC.direction > 0 && base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X * 0.8f;
				}
			}
			if (base.NPC.direction == -1 && base.NPC.velocity.X > 0f - maxHoverVel)
			{
				base.NPC.velocity.X -= (CalamityWorld.revenge ? 0.15f : 0.1f);
				if (base.NPC.velocity.X > maxHoverVel)
				{
					base.NPC.velocity.X -= (CalamityWorld.revenge ? 0.15f : 0.1f);
				}
				else if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X += (CalamityWorld.revenge ? 0.1f : 0.05f);
				}
				if (base.NPC.velocity.X < 0f - maxHoverVel)
				{
					base.NPC.velocity.X = 0f - maxHoverVel;
				}
			}
			else if (base.NPC.direction == 1 && base.NPC.velocity.X < maxHoverVel)
			{
				base.NPC.velocity.X += (CalamityWorld.revenge ? 0.15f : 0.1f);
				if (base.NPC.velocity.X < 0f - maxHoverVel)
				{
					base.NPC.velocity.X += (CalamityWorld.revenge ? 0.15f : 0.1f);
				}
				else if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X -= (CalamityWorld.revenge ? 0.1f : 0.05f);
				}
				if (base.NPC.velocity.X > maxHoverVel)
				{
					base.NPC.velocity.X = maxHoverVel;
				}
			}
			maxHoverVel = ((base.NPC.type != 490) ? (CalamityWorld.revenge ? 2.5f : 1.5f) : 1.5f);
			if (CalamityWorld.death)
			{
				maxHoverVel *= 1.25f;
			}
			if (base.NPC.directionY == -1 && base.NPC.velocity.Y > 0f - maxHoverVel)
			{
				base.NPC.velocity.Y -= (CalamityWorld.revenge ? 0.06f : 0.04f);
				if (base.NPC.velocity.Y > maxHoverVel)
				{
					base.NPC.velocity.Y -= (CalamityWorld.revenge ? 0.1f : 0.05f);
				}
				else if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y += (CalamityWorld.revenge ? 0.05f : 0.03f);
				}
				if (base.NPC.velocity.Y < 0f - maxHoverVel)
				{
					base.NPC.velocity.Y = 0f - maxHoverVel;
				}
			}
			else if (base.NPC.directionY == 1 && base.NPC.velocity.Y < maxHoverVel)
			{
				base.NPC.velocity.Y += (CalamityWorld.revenge ? 0.06f : 0.04f);
				if (base.NPC.velocity.Y < 0f - maxHoverVel)
				{
					base.NPC.velocity.Y += (CalamityWorld.revenge ? 0.1f : 0.05f);
				}
				else if (base.NPC.velocity.Y < 0f)
				{
					base.NPC.velocity.Y -= (CalamityWorld.revenge ? 0.05f : 0.03f);
				}
				if (base.NPC.velocity.Y > maxHoverVel)
				{
					base.NPC.velocity.Y = maxHoverVel;
				}
			}
			if (base.NPC.type == 122)
			{
				Lighting.AddLight((int)base.NPC.position.X / 16, (int)base.NPC.position.Y / 16, 0.4f, 0f, 0.25f);
			}
			return false;
		}
	}

	public class JellyfishAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0293: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0132: Unknown result type (might be due to invalid IL or missing references)
			//IL_013d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0142: Unknown result type (might be due to invalid IL or missing references)
			//IL_0147: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0351: Unknown result type (might be due to invalid IL or missing references)
			//IL_0365: Unknown result type (might be due to invalid IL or missing references)
			//IL_0642: Unknown result type (might be due to invalid IL or missing references)
			//IL_065e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0668: Unknown result type (might be due to invalid IL or missing references)
			//IL_066d: Unknown result type (might be due to invalid IL or missing references)
			//IL_068f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0699: Unknown result type (might be due to invalid IL or missing references)
			//IL_069e: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_06d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0701: Unknown result type (might be due to invalid IL or missing references)
			//IL_0706: Unknown result type (might be due to invalid IL or missing references)
			//IL_0907: Unknown result type (might be due to invalid IL or missing references)
			//IL_091d: Unknown result type (might be due to invalid IL or missing references)
			//IL_079d: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_07bd: Unknown result type (might be due to invalid IL or missing references)
			float damagingVelocity = ((base.NPC.type == 103) ? 7.2f : 5.6f);
			base.NPC.damage = ((base.NPC.dontTakeDamage || ((Vector2)(ref base.NPC.velocity)).Length() > damagingVelocity) ? base.NPC.defDamage : 0);
			bool endEarly = false;
			if (base.NPC.wet && base.NPC.ai[1] == 1f)
			{
				endEarly = true;
			}
			else
			{
				base.NPC.dontTakeDamage = false;
			}
			if (base.NPC.type == 63 || base.NPC.type == 64 || base.NPC.type == 103 || base.NPC.type == 242)
			{
				if (base.NPC.wet)
				{
					if (base.NPC.target >= 0 && Main.player[base.NPC.target].wet && !Main.player[base.NPC.target].dead)
					{
						Vector2 val = Main.player[base.NPC.target].Center - base.NPC.Center;
						if (((Vector2)(ref val)).Length() < 200f)
						{
							if (base.NPC.ai[1] == 0f)
							{
								base.NPC.ai[2] += 2f;
							}
							else
							{
								base.NPC.ai[2] -= 0.25f;
							}
						}
					}
					if (endEarly)
					{
						base.NPC.dontTakeDamage = true;
						base.NPC.ai[2]++;
						if (base.NPC.ai[2] >= 90f)
						{
							base.NPC.ai[1] = 0f;
						}
					}
					else
					{
						base.NPC.ai[2]++;
						if (base.NPC.ai[2] >= 300f)
						{
							base.NPC.ai[1] = 1f;
							base.NPC.ai[2] = 0f;
						}
					}
				}
				else
				{
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
				}
			}
			float lightIntensity = 1f;
			if (endEarly)
			{
				lightIntensity += 0.5f;
			}
			if (base.NPC.type == 63)
			{
				Lighting.AddLight((int)base.NPC.Center.X / 16, (int)base.NPC.Center.Y / 16, 0.05f * lightIntensity, 0.15f * lightIntensity, 0.4f * lightIntensity);
			}
			else if (base.NPC.type == 103)
			{
				Lighting.AddLight((int)base.NPC.Center.X / 16, (int)base.NPC.Center.Y / 16, 0.05f * lightIntensity, 0.45f * lightIntensity, 0.1f * lightIntensity);
			}
			else if (base.NPC.type != 221 && base.NPC.type != 242)
			{
				Lighting.AddLight((int)base.NPC.Center.X / 16, (int)base.NPC.Center.Y / 16, 0.35f * lightIntensity, 0.05f * lightIntensity, 0.2f * lightIntensity);
			}
			if (base.NPC.direction == 0)
			{
				base.NPC.TargetClosest();
			}
			if (endEarly)
			{
				return false;
			}
			if (!base.NPC.wet)
			{
				base.NPC.rotation += base.NPC.velocity.X * 0.1f;
				if (base.NPC.velocity.Y == 0f)
				{
					base.NPC.velocity.X *= 0.98f;
					if (Math.Abs(base.NPC.velocity.X) < 0.01f)
					{
						base.NPC.velocity.X = 0f;
					}
				}
				base.NPC.velocity.Y += 0.2f;
				if (base.NPC.velocity.Y > 10f)
				{
					base.NPC.velocity.Y = 10f;
				}
				base.NPC.ai[0] = 1f;
				return false;
			}
			if (base.NPC.collideX)
			{
				base.NPC.velocity.X *= 1f;
				base.NPC.direction *= -1;
			}
			if (base.NPC.collideY)
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y) * -1f;
					base.NPC.directionY = -1;
					base.NPC.ai[0] = -1f;
				}
				else if (base.NPC.velocity.Y < 0f)
				{
					base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y);
					base.NPC.directionY = 1;
					base.NPC.ai[0] = 1f;
				}
			}
			bool targetInWater = false;
			if (!base.NPC.friendly)
			{
				base.NPC.TargetClosest(faceTarget: false);
				if ((Main.player[base.NPC.target].wet || (CalamityWorld.death && base.NPC.Distance(Main.player[base.NPC.target].Center) < 400f)) && !Main.player[base.NPC.target].dead)
				{
					targetInWater = true;
				}
			}
			if (targetInWater)
			{
				base.NPC.localAI[2] = 1f;
				base.NPC.rotation = base.NPC.velocity.ToRotation() + (float)Math.PI / 2f;
				NPC nPC = base.NPC;
				nPC.velocity *= 0.96f;
				float minimumSpeed = 0.2f;
				if (base.NPC.type == 103)
				{
					NPC nPC2 = base.NPC;
					nPC2.velocity *= 0.98f;
					minimumSpeed = 0.6f;
				}
				if (base.NPC.type == 221)
				{
					NPC nPC3 = base.NPC;
					nPC3.velocity *= 0.99f;
					minimumSpeed = 1f;
				}
				if (base.NPC.type == 242)
				{
					NPC nPC4 = base.NPC;
					nPC4.velocity *= 0.995f;
					minimumSpeed = 3f;
				}
				minimumSpeed *= 0.8f;
				if (((Vector2)(ref base.NPC.velocity)).Length() < minimumSpeed)
				{
					if (base.NPC.type == 221)
					{
						base.NPC.localAI[0] = 1f;
					}
					base.NPC.TargetClosest();
					float lungeSpeed = ((base.NPC.type == 103) ? 18f : 14f);
					base.NPC.velocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center, -Vector2.UnitY) * lungeSpeed;
				}
			}
			else
			{
				base.NPC.localAI[2] = 0f;
				base.NPC.velocity.X += (float)base.NPC.direction * 0.02f;
				base.NPC.rotation = base.NPC.velocity.X * 0.4f;
				if (base.NPC.velocity.X < -1f || base.NPC.velocity.X > 1f)
				{
					base.NPC.velocity.X *= 0.95f;
				}
				if (base.NPC.ai[0] == -1f)
				{
					base.NPC.velocity.Y -= 0.01f;
					if (base.NPC.velocity.Y < -1f)
					{
						base.NPC.ai[0] = 1f;
					}
				}
				else
				{
					base.NPC.velocity.Y += 0.01f;
					if (base.NPC.velocity.Y > 1f)
					{
						base.NPC.ai[0] = -1f;
					}
				}
				int x = (int)base.NPC.Center.X / 16;
				int y = (int)base.NPC.Center.Y / 16;
				if (Main.tile[x, y - 1].LiquidAmount > 128)
				{
					if (Main.tile[x, y + 1].HasTile)
					{
						base.NPC.ai[0] = -1f;
					}
					else if (Main.tile[x, y + 2].HasTile)
					{
						base.NPC.ai[0] = -1f;
					}
				}
				else
				{
					base.NPC.ai[0] = 1f;
				}
				if ((double)Math.Abs(base.NPC.velocity.Y) > 1.2)
				{
					base.NPC.velocity.Y *= 0.99f;
				}
			}
			return false;
		}
	}

	public class MartianProbeAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_022c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0236: Unknown result type (might be due to invalid IL or missing references)
			//IL_023b: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0420: Unknown result type (might be due to invalid IL or missing references)
			//IL_0426: Unknown result type (might be due to invalid IL or missing references)
			//IL_042b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0432: Unknown result type (might be due to invalid IL or missing references)
			//IL_0437: Unknown result type (might be due to invalid IL or missing references)
			//IL_0411: Unknown result type (might be due to invalid IL or missing references)
			//IL_0416: Unknown result type (might be due to invalid IL or missing references)
			//IL_041a: Unknown result type (might be due to invalid IL or missing references)
			//IL_041f: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
			if (base.NPC.ai[0] == 0f)
			{
				if (base.NPC.direction == 0)
				{
					base.NPC.TargetClosest();
					base.NPC.netUpdate = true;
				}
				if (base.NPC.collideX)
				{
					base.NPC.direction = -base.NPC.direction;
					base.NPC.netUpdate = true;
				}
				base.NPC.velocity.X = 6f * (float)base.NPC.direction;
				Point centerTileCoords = base.NPC.Center.ToTileCoordinates();
				int distanceFromGround = 30;
				if (WorldGen.InWorld(centerTileCoords.X, centerTileCoords.Y, 30))
				{
					for (int y = 0; y < 30; y++)
					{
						if (WorldGen.SolidTile(centerTileCoords.X, centerTileCoords.Y + y))
						{
							distanceFromGround = y;
							break;
						}
					}
				}
				if (distanceFromGround < 15)
				{
					base.NPC.velocity.Y = Math.Max(base.NPC.velocity.Y - 0.05f, -3.5f);
				}
				else if (distanceFromGround < 20)
				{
					base.NPC.velocity.Y *= 0.95f;
				}
				else
				{
					base.NPC.velocity.Y = Math.Min(base.NPC.velocity.Y + 0.05f, 1.5f);
				}
				int playerIndex = base.NPC.FindClosestPlayer(out var distanceFromPlayer);
				if (playerIndex == -1 || Main.player[playerIndex].dead)
				{
					return false;
				}
				if (distanceFromPlayer < 440f && Main.player[playerIndex].Center.Y > base.NPC.Center.Y)
				{
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[0] == 1f)
			{
				base.NPC.ai[1]++;
				NPC nPC = base.NPC;
				nPC.velocity *= 0.93f;
				if (base.NPC.ai[1] >= (CalamityWorld.death ? 5f : 45f))
				{
					base.NPC.ai[1] = 0f;
					base.NPC.ai[0] = 2f;
					int closetPlayer = base.NPC.FindClosestPlayer();
					base.NPC.ai[3] = ((closetPlayer != -1) ? ((float)(Main.player[closetPlayer].Center.X < base.NPC.Center.X).ToDirectionInt()) : 1f);
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[0] == 2f)
			{
				base.NPC.noTileCollide = true;
				base.NPC.ai[1]++;
				base.NPC.velocity.Y = Math.Max(base.NPC.velocity.Y - 0.2f, -12f);
				base.NPC.velocity.X = Math.Min(base.NPC.velocity.X + base.NPC.ai[3] * 0.1f, 6f);
				if ((base.NPC.position.Y < (float)(-base.NPC.height) || base.NPC.ai[1] >= 135f) && Main.netMode != 1)
				{
					Main.StartInvasion(4);
					base.NPC.active = false;
					base.NPC.netUpdate = true;
				}
			}
			Color val = Color.SkyBlue;
			Vector3 lightColor = ((Color)(ref val)).ToVector3();
			if (base.NPC.ai[0] == 2f)
			{
				val = Color.Red;
				lightColor = ((Color)(ref val)).ToVector3();
			}
			lightColor *= 0.65f;
			Lighting.AddLight(base.NPC.Center, lightColor);
			return false;
		}
	}

	public class MimicAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_022b: Unknown result type (might be due to invalid IL or missing references)
			bool isLostHoppingPresent = base.NPC.type == 341 && !Main.snowMoon;
			if (base.NPC.ai[3] == 0f)
			{
				base.NPC.position.X += 8f;
				if (base.NPC.position.Y / 16f > (float)Main.UnderworldLayer)
				{
					base.NPC.ai[3] = 3f;
				}
				else if ((double)(base.NPC.position.Y / 16f) > Main.worldSurface)
				{
					base.NPC.TargetClosest();
					base.NPC.ai[3] = 2f;
				}
				else
				{
					base.NPC.ai[3] = 1f;
				}
			}
			if (base.NPC.type == 341 || base.NPC.type == 629)
			{
				base.NPC.ai[3] = 1f;
			}
			base.NPC.dontTakeDamage = base.NPC.ai[0] == 0f;
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.damage = 0;
				if (!isLostHoppingPresent)
				{
					base.NPC.TargetClosest();
				}
				if (Main.netMode != 1)
				{
					if (base.NPC.velocity.X != 0f || base.NPC.velocity.Y < 0f || base.NPC.velocity.Y > 0.3f)
					{
						base.NPC.ai[0] = 1f;
						base.NPC.netUpdate = true;
						return false;
					}
					Rectangle detectionZone = default(Rectangle);
					((Rectangle)(ref detectionZone))._002Ector((int)base.NPC.position.X - 80, (int)base.NPC.position.Y - 80, base.NPC.width + 160, base.NPC.height + 160);
					if (((Rectangle)(ref detectionZone)).Intersects(Main.player[base.NPC.target].Hitbox) || base.NPC.life < base.NPC.lifeMax)
					{
						base.NPC.ai[0] = 1f;
						base.NPC.netUpdate = true;
					}
				}
			}
			else if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				base.NPC.ai[2]++;
				int timeSpentStopping = 20;
				if (base.NPC.ai[1] == 0f)
				{
					timeSpentStopping = 12;
				}
				if (base.NPC.ai[2] < (float)timeSpentStopping)
				{
					base.NPC.velocity.X *= 0.9f;
					return false;
				}
				base.NPC.ai[2] = 0f;
				if (!isLostHoppingPresent)
				{
					base.NPC.TargetClosest();
				}
				if (base.NPC.direction == 0)
				{
					base.NPC.direction = -1;
				}
				base.NPC.spriteDirection = base.NPC.direction;
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] == 2f)
				{
					base.NPC.velocity.X = (float)base.NPC.direction * 4f;
					base.NPC.velocity.Y = -8f;
					base.NPC.ai[1] = 0f;
				}
				else
				{
					base.NPC.velocity.X = (float)base.NPC.direction * 5.5f;
					base.NPC.velocity.Y = -4f;
				}
				base.NPC.netUpdate = true;
			}
			else
			{
				base.NPC.damage = base.NPC.defDamage;
				if (base.NPC.direction == 1 && base.NPC.velocity.X < 1f)
				{
					base.NPC.velocity.X += 0.1f;
					return false;
				}
				if (base.NPC.direction == -1 && base.NPC.velocity.X > -1f)
				{
					base.NPC.velocity.X -= 0.1f;
				}
			}
			return false;
		}
	}

	public class MothronEggAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0303: Unknown result type (might be due to invalid IL or missing references)
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = ((Main.rand.NextBool(10) && NPC.CountNPCS(477) < 2) ? 477 : 479);
				if ((int)base.NPC.ai[1] == 477)
				{
					base.NPC.defense = (int)Math.Round((double)base.NPC.defDefense * 1.5);
					base.NPC.scale *= 2f;
					base.NPC.width = (base.NPC.height = (int)(34f * base.NPC.scale));
					base.NPC.netUpdate = true;
				}
			}
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X *= 0.9f;
				base.NPC.rotation += base.NPC.velocity.X * 0.02f;
			}
			else
			{
				base.NPC.velocity.X *= 0.99f;
				base.NPC.rotation += base.NPC.velocity.X * 0.04f;
			}
			float hatchTimer = (((int)base.NPC.ai[1] == 477) ? 480f : 240f);
			if (CalamityWorld.death)
			{
				hatchTimer *= 0.5f;
			}
			base.NPC.ai[0]++;
			if (base.NPC.ai[0] >= hatchTimer)
			{
				int hatchType = ((NPC.CountNPCS(477) < 2) ? ((int)base.NPC.ai[1]) : 479);
				base.NPC.Transform(hatchType);
			}
			if (Main.netMode != 1 && base.NPC.velocity.Y == 0f && Math.Abs(base.NPC.velocity.X) < 0.2f && base.NPC.ai[0] >= hatchTimer * 0.75f)
			{
				float hatchCompleteness = base.NPC.ai[0] - hatchTimer * 0.75f;
				hatchCompleteness /= hatchTimer * 0.25f;
				if ((float)Main.rand.Next(-10, 120) < hatchCompleteness * 100f)
				{
					base.NPC.velocity.Y -= (float)Main.rand.Next(20, 40) * 0.025f;
					base.NPC.velocity.X += (float)Main.rand.Next(-20, 20) * 0.025f;
					NPC nPC = base.NPC;
					nPC.velocity *= 1f + hatchCompleteness * 2f;
					base.NPC.netUpdate = true;
				}
			}
			return false;
		}
	}

	public class PlantAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0290: Unknown result type (might be due to invalid IL or missing references)
			//IL_0295: Unknown result type (might be due to invalid IL or missing references)
			//IL_0296: Unknown result type (might be due to invalid IL or missing references)
			//IL_029b: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_0378: Unknown result type (might be due to invalid IL or missing references)
			//IL_0400: Unknown result type (might be due to invalid IL or missing references)
			//IL_031e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0493: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0505: Unknown result type (might be due to invalid IL or missing references)
			//IL_050b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0510: Unknown result type (might be due to invalid IL or missing references)
			//IL_0515: Unknown result type (might be due to invalid IL or missing references)
			//IL_0439: Unknown result type (might be due to invalid IL or missing references)
			//IL_055b: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0578: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_080f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0814: Unknown result type (might be due to invalid IL or missing references)
			//IL_0819: Unknown result type (might be due to invalid IL or missing references)
			//IL_0823: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b34: Unknown result type (might be due to invalid IL or missing references)
			//IL_0994: Unknown result type (might be due to invalid IL or missing references)
			//IL_0846: Unknown result type (might be due to invalid IL or missing references)
			//IL_084b: Unknown result type (might be due to invalid IL or missing references)
			//IL_085f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0864: Unknown result type (might be due to invalid IL or missing references)
			//IL_087b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0881: Unknown result type (might be due to invalid IL or missing references)
			//IL_0895: Unknown result type (might be due to invalid IL or missing references)
			//IL_089f: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_09bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_09eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_08f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0900: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a45: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a4a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a4f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a63: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a68: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a7c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a81: Unknown result type (might be due to invalid IL or missing references)
			//IL_0be8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c6f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c7f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c8f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cb4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cc4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cf4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d04: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d09: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d10: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d15: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d1a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d1f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d29: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d30: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d35: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d49: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d5a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d93: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d95: Unknown result type (might be due to invalid IL or missing references)
			if (base.NPC.ai[0] < 0f || base.NPC.ai[0] >= (float)Main.maxTilesX || base.NPC.ai[1] < 0f || base.NPC.ai[1] >= (float)Main.maxTilesX)
			{
				return false;
			}
			if (!Main.tile[(int)base.NPC.ai[0], (int)base.NPC.ai[1]].HasTile)
			{
				base.NPC.life = -1;
				base.NPC.HitEffect();
				base.NPC.active = false;
				return false;
			}
			FixExploitManEaters.ProtectSpot((int)base.NPC.ai[0], (int)base.NPC.ai[1]);
			base.NPC.TargetClosest();
			float acceleration = 0.035f;
			float minDistance = 250f;
			switch (base.NPC.type)
			{
			case 43:
				minDistance = 350f;
				break;
			case 101:
				minDistance = 225f;
				break;
			case 259:
				minDistance = ((!CalamityWorld.revenge) ? 100f : 200f);
				break;
			case 175:
				acceleration = 0.05f;
				minDistance = 500f;
				break;
			case 260:
				acceleration = 0.15f;
				minDistance = (CalamityWorld.revenge ? 450f : 350f);
				break;
			}
			if (CalamityWorld.death)
			{
				acceleration *= 1.25f;
				minDistance *= 1.25f;
			}
			float maxVelocity = 2f + ((base.NPC.type == 43) ? 1f : 0f) + ((base.NPC.type == 175) ? 2f : 0f);
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] > 300f)
			{
				minDistance *= 1.3f;
				if (CalamityWorld.revenge)
				{
					maxVelocity += 2f;
				}
				if (base.NPC.ai[2] > 450f)
				{
					base.NPC.ai[2] = 0f;
				}
			}
			Vector2 anchorPosition = default(Vector2);
			((Vector2)(ref anchorPosition))._002Ector(base.NPC.ai[0] * 16f + 8f, base.NPC.ai[1] * 16f + 8f);
			Vector2 distanceVector = Main.player[base.NPC.target].Center - anchorPosition;
			float distanceMagnitude = ((Vector2)(ref distanceVector)).Length();
			if (distanceMagnitude > minDistance)
			{
				float normalizedMagnitude = minDistance / distanceMagnitude;
				distanceVector *= normalizedMagnitude;
			}
			if (base.NPC.position.X < base.NPC.ai[0] * 16f + 8f + distanceVector.X)
			{
				base.NPC.velocity.X += acceleration;
				if (base.NPC.velocity.X < 0f && distanceVector.X > 0f)
				{
					base.NPC.velocity.X += acceleration * 1.5f;
				}
			}
			else if (base.NPC.position.X > base.NPC.ai[0] * 16f + 8f + distanceVector.X)
			{
				base.NPC.velocity.X -= acceleration;
				if (base.NPC.velocity.X > 0f && distanceVector.X < 0f)
				{
					base.NPC.velocity.X -= acceleration * 1.5f;
				}
			}
			if (base.NPC.position.Y < base.NPC.ai[1] * 16f + 8f + distanceVector.Y)
			{
				base.NPC.velocity.Y += acceleration;
				if (base.NPC.velocity.Y < 0f && distanceVector.Y > 0f)
				{
					base.NPC.velocity.Y += acceleration * 1.5f;
				}
			}
			else if (base.NPC.position.Y > base.NPC.ai[1] * 16f + 8f + distanceVector.Y)
			{
				base.NPC.velocity.Y -= acceleration;
				if (base.NPC.velocity.Y > 0f && distanceVector.Y < 0f)
				{
					base.NPC.velocity.Y -= acceleration * 1.5f;
				}
			}
			base.NPC.velocity = Vector2.Clamp(base.NPC.velocity, new Vector2(0f - maxVelocity), new Vector2(maxVelocity));
			if (base.NPC.type == 259 || base.NPC.type == 260)
			{
				base.NPC.rotation = base.NPC.AngleTo(Main.player[base.NPC.target].Center) + (float)Math.PI / 2f;
			}
			else
			{
				base.NPC.spriteDirection = (distanceVector.X > 0f).ToDirectionInt();
				base.NPC.rotation = base.NPC.AngleTo(Main.player[base.NPC.target].Center) + (float)(distanceVector.X < 0f).ToInt() * (float)Math.PI;
			}
			if (base.NPC.collideX)
			{
				base.NPC.netUpdate = true;
				base.NPC.velocity.X = base.NPC.oldVelocity.X * -0.7f;
				if (base.NPC.velocity.X > 0f && base.NPC.velocity.X < 2f)
				{
					base.NPC.velocity.X = 2f;
				}
				if (base.NPC.velocity.X < 0f && base.NPC.velocity.X > -2f)
				{
					base.NPC.velocity.X = -2f;
				}
			}
			if (base.NPC.collideY)
			{
				base.NPC.netUpdate = true;
				base.NPC.velocity.Y = base.NPC.oldVelocity.Y * -0.7f;
				if (base.NPC.velocity.Y > 0f && base.NPC.velocity.Y < 2f)
				{
					base.NPC.velocity.Y = 2f;
				}
				if (base.NPC.velocity.Y < 0f && base.NPC.velocity.Y > -2f)
				{
					base.NPC.velocity.Y = -2f;
				}
			}
			if ((base.NPC.type == 260 || base.NPC.type == 259) && !Main.player[base.NPC.target].DeadOrGhost)
			{
				if (base.NPC.localAI[0] > ((base.NPC.type != 260) ? 240f : (CalamityWorld.revenge ? 120f : 150f)) - 30f)
				{
					Dust dust = Dust.NewDustDirect(base.NPC.Center + base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center, -Vector2.UnitY) * ((base.NPC.type == 260) ? 20f : 12f) + Main.rand.NextVector2CircularEdge(5f, 5f), 1, 1, 59, 0f, 0f, 100, default(Color), 3f);
					dust.noGravity = true;
					dust.velocity *= 0f;
				}
				if (base.NPC.localAI[0] == ((base.NPC.type != 260) ? 240f : (CalamityWorld.revenge ? 120f : 150f)) - 1f)
				{
					SoundEngine.PlaySound(in SoundID.Item17, base.NPC.Center);
				}
			}
			if (Main.netMode != 1)
			{
				if (base.NPC.type == 101 && !Main.player[base.NPC.target].DeadOrGhost)
				{
					if (base.NPC.justHit)
					{
						base.NPC.localAI[0] = 0f;
					}
					base.NPC.localAI[0]++;
					if (base.NPC.localAI[0] >= 90f)
					{
						if (!Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height) && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
						{
							int damage = 17;
							int type = 96;
							Vector2 flameVelocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center, -Vector2.UnitY) * 12f;
							int flame = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, flameVelocity, type, damage, 0f, Main.myPlayer);
							Main.projectile[flame].timeLeft = 180;
							base.NPC.localAI[0] = 0f;
						}
						else
						{
							base.NPC.localAI[0] = 75f;
						}
					}
				}
				if ((base.NPC.type == 260 || base.NPC.type == 259) && !Main.player[base.NPC.target].DeadOrGhost)
				{
					if (base.NPC.justHit || Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height) || !Collision.CanHit(base.NPC, Main.player[base.NPC.target]))
					{
						base.NPC.localAI[0] = 0f;
					}
					base.NPC.localAI[0]++;
					float sporeSpawnGateValue = ((base.NPC.type != 260) ? 240f : (CalamityWorld.revenge ? 120f : 150f));
					if (base.NPC.localAI[0] >= sporeSpawnGateValue)
					{
						if (!Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height) && Collision.CanHit(base.NPC, Main.player[base.NPC.target]))
						{
							float speed = ((base.NPC.type != 260) ? 8f : (CalamityWorld.revenge ? 16f : 14f));
							distanceVector.X = Main.player[base.NPC.target].Center.X - base.NPC.Center.X;
							float absoluteYDistance = Math.Abs(distanceVector.X * 0.1f);
							if (Main.player[base.NPC.target].Center.Y - base.NPC.Center.Y > 0f)
							{
								absoluteYDistance = 0f;
							}
							Vector2 velocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center - base.NPC.Center - Vector2.UnitY * absoluteYDistance, -Vector2.UnitY) * speed;
							int idx = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 261);
							Main.npc[idx].velocity = velocity;
							Main.npc[idx].netUpdate = true;
						}
						base.NPC.localAI[0] = 0f;
					}
				}
			}
			return false;
		}
	}

	public class SmallStarCellAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_012c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0131: Unknown result type (might be due to invalid IL or missing references)
			//IL_0175: Unknown result type (might be due to invalid IL or missing references)
			//IL_0187: Unknown result type (might be due to invalid IL or missing references)
			//IL_0196: Unknown result type (might be due to invalid IL or missing references)
			//IL_019b: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_020d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0217: Unknown result type (might be due to invalid IL or missing references)
			//IL_021c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0221: Unknown result type (might be due to invalid IL or missing references)
			//IL_022e: Unknown result type (might be due to invalid IL or missing references)
			//IL_022f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0231: Unknown result type (might be due to invalid IL or missing references)
			//IL_0236: Unknown result type (might be due to invalid IL or missing references)
			//IL_0240: Unknown result type (might be due to invalid IL or missing references)
			//IL_0253: Unknown result type (might be due to invalid IL or missing references)
			//IL_025d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0262: Unknown result type (might be due to invalid IL or missing references)
			float turnBigDelay = (CalamityWorld.death ? 100f : 200f);
			if (((Vector2)(ref base.NPC.velocity)).Length() > 4f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.95f;
			}
			NPC nPC2 = base.NPC;
			nPC2.velocity *= 0.99f;
			base.NPC.ai[0]++;
			float cellScale = MathHelper.Clamp(base.NPC.ai[0] / turnBigDelay, 0f, 1f);
			base.NPC.scale = 1f + 0.3f * cellScale;
			if (base.NPC.ai[0] >= turnBigDelay)
			{
				if (Main.netMode != 1)
				{
					base.NPC.Transform(405);
					base.NPC.netUpdate = true;
				}
				return false;
			}
			base.NPC.rotation += base.NPC.velocity.X * 0.1f;
			if (!(base.NPC.ai[0] > 20f))
			{
				return false;
			}
			Vector2 cellCenter = base.NPC.Center;
			int dustAmt = (int)(base.NPC.ai[0] / (turnBigDelay / 2f));
			for (int i = 0; i < dustAmt + 1; i++)
			{
				if (Main.rand.NextBool())
				{
					float dustScale = 0.4f;
					if (i % 2 == 1)
					{
						dustScale = 0.65f;
					}
					Vector2 dustRotation = cellCenter + ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * (12f - (float)(dustAmt * 2));
					int cellDust = Dust.NewDust(dustRotation - Vector2.One * 12f, 24, 24, 226, base.NPC.velocity.X / 2f, base.NPC.velocity.Y / 2f);
					Dust obj = Main.dust[cellDust];
					obj.position -= new Vector2(2f);
					Main.dust[cellDust].velocity = Vector2.Normalize(cellCenter - dustRotation) * 1.5f * (10f - (float)dustAmt * 2f) / 10f;
					Main.dust[cellDust].noGravity = true;
					Main.dust[cellDust].scale = dustScale;
					Main.dust[cellDust].customData = base.NPC;
				}
			}
			return false;
		}
	}

	public class SpiderAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0208: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0520: Unknown result type (might be due to invalid IL or missing references)
			//IL_0586: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0554: Unknown result type (might be due to invalid IL or missing references)
			//IL_064d: Unknown result type (might be due to invalid IL or missing references)
			//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0625: Unknown result type (might be due to invalid IL or missing references)
			//IL_0339: Unknown result type (might be due to invalid IL or missing references)
			//IL_0359: Unknown result type (might be due to invalid IL or missing references)
			//IL_0373: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b83: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b99: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a77: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a8e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a94: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0afe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b03: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b08: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b11: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b23: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b28: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b3c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b41: Unknown result type (might be due to invalid IL or missing references)
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead)
			{
				base.NPC.TargetClosest();
			}
			float speed = 2.5f;
			float mvtAdjust = 0.1f;
			if (base.NPC.type == 531)
			{
				speed = 5f;
				mvtAdjust = 0.2f;
			}
			if (CalamityWorld.death)
			{
				speed *= 1.25f;
				mvtAdjust *= 1.25f;
			}
			Vector2 npcPos = base.NPC.Center;
			Vector2 targetPos = Main.player[base.NPC.target].Center;
			targetPos.X = (int)(targetPos.X / 8f) * 8;
			targetPos.Y = (int)(targetPos.Y / 8f) * 8;
			npcPos.X = (int)(npcPos.X / 8f) * 8;
			npcPos.Y = (int)(npcPos.Y / 8f) * 8;
			targetPos.X -= npcPos.X;
			targetPos.Y -= npcPos.Y;
			float targetDist = ((Vector2)(ref targetPos)).Length();
			if (targetDist == 0f)
			{
				targetPos.X = base.NPC.velocity.X;
				targetPos.Y = base.NPC.velocity.Y;
			}
			else
			{
				targetDist = speed / targetDist;
				targetPos.X *= targetDist;
				targetPos.Y *= targetDist;
			}
			if (Main.player[base.NPC.target].dead)
			{
				targetPos.X = (float)base.NPC.direction * speed / 2f;
				targetPos.Y = (0f - speed) / 2f;
			}
			base.NPC.spriteDirection = -1;
			if (!Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
			{
				base.NPC.ai[0]++;
				if (base.NPC.ai[0] > 0f)
				{
					base.NPC.velocity.Y += 0.023f;
				}
				else
				{
					base.NPC.velocity.Y -= 0.023f;
				}
				if (base.NPC.ai[0] < -100f || base.NPC.ai[0] > 100f)
				{
					base.NPC.velocity.X += 0.023f;
				}
				else
				{
					base.NPC.velocity.X -= 0.023f;
				}
				if (base.NPC.ai[0] > 200f)
				{
					base.NPC.ai[0] = -200f;
				}
				base.NPC.velocity.X += targetPos.X * 0.009f;
				base.NPC.velocity.Y += targetPos.Y * 0.009f;
				base.NPC.rotation = base.NPC.velocity.ToRotation();
				if (base.NPC.velocity.X > 2.5f)
				{
					base.NPC.velocity.X *= 0.9f;
				}
				if (base.NPC.velocity.X < -2.5f)
				{
					base.NPC.velocity.X *= 0.9f;
				}
				if (base.NPC.velocity.Y > 2.5f)
				{
					base.NPC.velocity.Y *= 0.9f;
				}
				if (base.NPC.velocity.Y < -2.5f)
				{
					base.NPC.velocity.Y *= 0.9f;
				}
				base.NPC.velocity.X = MathHelper.Clamp(base.NPC.velocity.X, -4f, 4f);
				base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y, -4f, 4f);
			}
			else
			{
				if (base.NPC.velocity.X < targetPos.X)
				{
					base.NPC.velocity.X += mvtAdjust;
					if (base.NPC.velocity.X < 0f && targetPos.X > 0f)
					{
						base.NPC.velocity.X += mvtAdjust;
					}
				}
				else if (base.NPC.velocity.X > targetPos.X)
				{
					base.NPC.velocity.X -= mvtAdjust;
					if (base.NPC.velocity.X > 0f && targetPos.X < 0f)
					{
						base.NPC.velocity.X -= mvtAdjust;
					}
				}
				if (base.NPC.velocity.Y < targetPos.Y)
				{
					base.NPC.velocity.Y += mvtAdjust;
					if (base.NPC.velocity.Y < 0f && targetPos.Y > 0f)
					{
						base.NPC.velocity.Y += mvtAdjust;
					}
				}
				else if (base.NPC.velocity.Y > targetPos.Y)
				{
					base.NPC.velocity.Y -= mvtAdjust;
					if (base.NPC.velocity.Y > 0f && targetPos.Y < 0f)
					{
						base.NPC.velocity.Y -= mvtAdjust;
					}
				}
				base.NPC.rotation = targetPos.ToRotation();
			}
			if (base.NPC.type == 531)
			{
				base.NPC.rotation += (float)Math.PI / 2f;
			}
			float half = 0.5f;
			if (base.NPC.collideX)
			{
				base.NPC.netUpdate = true;
				base.NPC.velocity.X = base.NPC.oldVelocity.X * (0f - half);
				if (base.NPC.direction == -1 && base.NPC.velocity.X > 0f && base.NPC.velocity.X < 2f)
				{
					base.NPC.velocity.X = 2f;
				}
				if (base.NPC.direction == 1 && base.NPC.velocity.X < 0f && base.NPC.velocity.X > -2f)
				{
					base.NPC.velocity.X = -2f;
				}
			}
			if (base.NPC.collideY)
			{
				base.NPC.netUpdate = true;
				base.NPC.velocity.Y = base.NPC.oldVelocity.Y * (0f - half);
				if (base.NPC.velocity.Y > 0f && base.NPC.velocity.Y < 1.5f)
				{
					base.NPC.velocity.Y = 2f;
				}
				if (base.NPC.velocity.Y < 0f && base.NPC.velocity.Y > -1.5f)
				{
					base.NPC.velocity.Y = -2f;
				}
			}
			if (((base.NPC.velocity.X > 0f && base.NPC.oldVelocity.X < 0f) || (base.NPC.velocity.X < 0f && base.NPC.oldVelocity.X > 0f) || (base.NPC.velocity.Y > 0f && base.NPC.oldVelocity.Y < 0f) || (base.NPC.velocity.Y < 0f && base.NPC.oldVelocity.Y > 0f)) && !base.NPC.justHit)
			{
				base.NPC.netUpdate = true;
			}
			if (Main.netMode != 1)
			{
				bool prehardmodeSpiders = (base.NPC.type == 164 || base.NPC.type == 165 || base.NPC.type == 239 || base.NPC.type == 240) && CalamityWorld.revenge;
				if (base.NPC.target >= 0 && Main.expertMode && ((base.NPC.type == 163 || base.NPC.type == 238 || base.NPC.type == 236 || base.NPC.type == 237) | prehardmodeSpiders) && Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					base.NPC.localAI[0]++;
					if (base.NPC.justHit)
					{
						base.NPC.localAI[0] = 0f;
					}
					float webSpitGateValue = (CalamityWorld.death ? 180f : (CalamityWorld.revenge ? 300f : 420f));
					if (base.NPC.localAI[0] > webSpitGateValue - 30f)
					{
						Dust dust = Dust.NewDustDirect(base.NPC.Center, 1, 1, 30, 0f, 0f, 100, default(Color), 1.5f);
						dust.noGravity = true;
						dust.velocity *= 0f;
					}
					if (base.NPC.localAI[0] >= webSpitGateValue)
					{
						base.NPC.localAI[0] = 0f;
						Vector2 velocity = Main.player[base.NPC.target].Center - base.NPC.Center;
						((Vector2)(ref velocity)).Normalize();
						velocity *= (prehardmodeSpiders ? 5f : 8f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity, 472, 18, 0f, Main.myPlayer);
					}
				}
				else
				{
					base.NPC.localAI[0] = 0f;
				}
				int npcX = (int)base.NPC.Center.X / 16;
				int npcY = (int)base.NPC.Center.Y / 16;
				bool climbingWall = false;
				for (int i = npcX - 1; i <= npcX + 1; i++)
				{
					for (int j = npcY - 1; j <= npcY + 1; j++)
					{
						if (Main.tile[i, j].WallType > 0)
						{
							climbingWall = true;
						}
					}
				}
				if (!climbingWall)
				{
					if (base.NPC.type == 237)
					{
						base.NPC.Transform(236);
						return false;
					}
					if (base.NPC.type == 238)
					{
						base.NPC.Transform(163);
						return false;
					}
					if (base.NPC.type == 240)
					{
						base.NPC.Transform(239);
						return false;
					}
					if (base.NPC.type == 531)
					{
						base.NPC.Transform(530);
						return false;
					}
					base.NPC.Transform(164);
				}
			}
			return false;
		}
	}

	public class SpikeBallAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			if (base.NPC.ai[0] == 0f)
			{
				if (Main.netMode != 1)
				{
					base.NPC.TargetClosest();
					base.NPC.direction *= -1;
					base.NPC.directionY *= -1;
					base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2 + 8);
					base.NPC.ai[1] = base.NPC.position.X + (float)(base.NPC.width / 2);
					base.NPC.ai[2] = base.NPC.position.Y + (float)(base.NPC.height / 2);
					if (base.NPC.direction == 0)
					{
						base.NPC.direction = 1;
					}
					if (base.NPC.directionY == 0)
					{
						base.NPC.directionY = 1;
					}
					base.NPC.ai[3] = 1f + (float)Main.rand.Next(15) * 0.1f;
					base.NPC.velocity.Y = (float)(base.NPC.directionY * 6) * base.NPC.ai[3];
					base.NPC.ai[0]++;
					base.NPC.netUpdate = true;
					return false;
				}
				base.NPC.ai[1] = base.NPC.position.X + (float)(base.NPC.width / 2);
				base.NPC.ai[2] = base.NPC.position.Y + (float)(base.NPC.height / 2);
			}
			else
			{
				float maxSpinSpeed = (CalamityWorld.death ? 12f : 9f) * base.NPC.ai[3];
				float spinAcceleration = (CalamityWorld.death ? 0.4f : 0.3f) * base.NPC.ai[3];
				float timeToReachMaxSpeed = maxSpinSpeed / spinAcceleration / 2f;
				if (base.NPC.ai[0] >= 1f && base.NPC.ai[0] < (float)(int)timeToReachMaxSpeed)
				{
					base.NPC.velocity.Y = (float)base.NPC.directionY * maxSpinSpeed;
					base.NPC.ai[0]++;
					return false;
				}
				if (base.NPC.ai[0] >= (float)(int)timeToReachMaxSpeed)
				{
					base.NPC.velocity.Y = 0f;
					base.NPC.directionY *= -1;
					base.NPC.velocity.X = maxSpinSpeed * (float)base.NPC.direction;
					base.NPC.ai[0] = -1f;
					return false;
				}
				if (base.NPC.directionY > 0)
				{
					if (base.NPC.velocity.Y >= maxSpinSpeed)
					{
						base.NPC.directionY *= -1;
						base.NPC.velocity.Y = maxSpinSpeed;
					}
				}
				else if (base.NPC.directionY < 0 && base.NPC.velocity.Y <= 0f - maxSpinSpeed)
				{
					base.NPC.directionY *= -1;
					base.NPC.velocity.Y = 0f - maxSpinSpeed;
				}
				if (base.NPC.direction > 0)
				{
					if (base.NPC.velocity.X >= maxSpinSpeed)
					{
						base.NPC.direction *= -1;
						base.NPC.velocity.X = maxSpinSpeed;
					}
				}
				else if (base.NPC.direction < 0 && base.NPC.velocity.X <= 0f - maxSpinSpeed)
				{
					base.NPC.direction *= -1;
					base.NPC.velocity.X = 0f - maxSpinSpeed;
				}
				base.NPC.velocity.X = base.NPC.velocity.X + spinAcceleration * (float)base.NPC.direction;
				base.NPC.velocity.Y = base.NPC.velocity.Y + spinAcceleration * (float)base.NPC.directionY;
			}
			return false;
		}
	}

	public class SporeAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0109: Unknown result type (might be due to invalid IL or missing references)
			//IL_0198: Unknown result type (might be due to invalid IL or missing references)
			if (base.NPC.type == 265)
			{
				Lighting.AddLight(base.NPC.Center, 0.5f, 0.2f, 0.5f);
			}
			if (base.NPC.timeLeft > 5)
			{
				base.NPC.timeLeft = 5;
			}
			base.NPC.noTileCollide = true;
			base.NPC.velocity.Y += 0.02f;
			if (base.NPC.velocity.Y > 1f)
			{
				base.NPC.velocity.Y = 1f;
			}
			if (base.NPC.ai[0] != -1f)
			{
				base.NPC.TargetClosest();
				float acceleration = (CalamityWorld.death ? 0.25f : (Main.expertMode ? 0.2f : 0.1f));
				float velocity = (CalamityWorld.death ? 6.25f : (Main.expertMode ? 5f : 3f));
				if (base.NPC.Center.X < Main.player[base.NPC.target].position.X)
				{
					if (base.NPC.velocity.X < 0f)
					{
						base.NPC.velocity.X *= 0.96f;
					}
					base.NPC.velocity.X += acceleration;
				}
				else if (base.NPC.position.X > Main.player[base.NPC.target].Center.X)
				{
					if (base.NPC.velocity.X > 0f)
					{
						base.NPC.velocity.X *= 0.96f;
					}
					base.NPC.velocity.X -= acceleration;
				}
				if (base.NPC.velocity.X > velocity || base.NPC.velocity.X < 0f - velocity)
				{
					base.NPC.velocity.X *= 0.97f;
				}
			}
			else
			{
				base.NPC.velocity.X *= 0.98f;
				base.NPC.damage = (base.NPC.defDamage = 0);
			}
			base.NPC.rotation = base.NPC.velocity.X * 0.2f;
			return false;
		}
	}

	public class StarCellAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_041e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0429: Unknown result type (might be due to invalid IL or missing references)
			//IL_042e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0433: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_06fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0458: Unknown result type (might be due to invalid IL or missing references)
			//IL_0463: Unknown result type (might be due to invalid IL or missing references)
			//IL_0468: Unknown result type (might be due to invalid IL or missing references)
			//IL_046d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0118: Unknown result type (might be due to invalid IL or missing references)
			//IL_011d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0131: Unknown result type (might be due to invalid IL or missing references)
			//IL_0140: Unknown result type (might be due to invalid IL or missing references)
			//IL_0147: Unknown result type (might be due to invalid IL or missing references)
			//IL_014c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_082f: Unknown result type (might be due to invalid IL or missing references)
			//IL_083a: Unknown result type (might be due to invalid IL or missing references)
			//IL_083f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0844: Unknown result type (might be due to invalid IL or missing references)
			//IL_0276: Unknown result type (might be due to invalid IL or missing references)
			//IL_027b: Unknown result type (might be due to invalid IL or missing references)
			//IL_028f: Unknown result type (might be due to invalid IL or missing references)
			//IL_029e: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_02af: Unknown result type (might be due to invalid IL or missing references)
			//IL_0169: Unknown result type (might be due to invalid IL or missing references)
			//IL_0170: Unknown result type (might be due to invalid IL or missing references)
			//IL_0727: Unknown result type (might be due to invalid IL or missing references)
			//IL_072b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0730: Unknown result type (might be due to invalid IL or missing references)
			//IL_073e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0748: Unknown result type (might be due to invalid IL or missing references)
			//IL_074d: Unknown result type (might be due to invalid IL or missing references)
			//IL_074f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0757: Unknown result type (might be due to invalid IL or missing references)
			//IL_075c: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_019b: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0872: Unknown result type (might be due to invalid IL or missing references)
			//IL_0876: Unknown result type (might be due to invalid IL or missing references)
			//IL_087b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0889: Unknown result type (might be due to invalid IL or missing references)
			//IL_0896: Unknown result type (might be due to invalid IL or missing references)
			//IL_089b: Unknown result type (might be due to invalid IL or missing references)
			//IL_089d: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0771: Unknown result type (might be due to invalid IL or missing references)
			//IL_033e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0344: Unknown result type (might be due to invalid IL or missing references)
			//IL_0349: Unknown result type (might be due to invalid IL or missing references)
			//IL_0351: Unknown result type (might be due to invalid IL or missing references)
			//IL_0356: Unknown result type (might be due to invalid IL or missing references)
			//IL_0357: Unknown result type (might be due to invalid IL or missing references)
			//IL_035c: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0307: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0201: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e44: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e3b: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0508: Unknown result type (might be due to invalid IL or missing references)
			//IL_020e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0222: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e58: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e5d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e7f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e84: Unknown result type (might be due to invalid IL or missing references)
			//IL_0251: Unknown result type (might be due to invalid IL or missing references)
			//IL_0264: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a42: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a47: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a4c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a7f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a86: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0901: Unknown result type (might be due to invalid IL or missing references)
			//IL_0919: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ace: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ad2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ad7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b00: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b05: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b62: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b7a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0be0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0be5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0616: Unknown result type (might be due to invalid IL or missing references)
			//IL_0626: Unknown result type (might be due to invalid IL or missing references)
			//IL_062b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c28: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c2d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c42: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c57: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c5e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ce9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cee: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d03: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d18: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d1f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c6f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c76: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d45: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c92: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c99: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d71: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d85: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cc2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cd6: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.noTileCollide = false;
			if (base.NPC.ai[0] == 0f)
			{
				if (base.NPC.type == 467 || base.NPC.type == 421)
				{
					base.NPC.damage = 0;
				}
				base.NPC.TargetClosest();
				if (Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					base.NPC.ai[0] = 1f;
				}
				else
				{
					Vector2 cellTargetDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
					cellTargetDirection.Y -= Main.player[base.NPC.target].height / 4;
					if (((Vector2)(ref cellTargetDirection)).Length() > 800f)
					{
						base.NPC.ai[0] = 2f;
					}
					else
					{
						Vector2 cellCenter = base.NPC.Center;
						cellCenter.X = Main.player[base.NPC.target].Center.X;
						Vector2 cellFaceDirection = cellCenter - base.NPC.Center;
						if (((Vector2)(ref cellFaceDirection)).Length() > 8f && Collision.CanHit(base.NPC.Center, 1, 1, cellCenter, 1, 1))
						{
							base.NPC.ai[0] = 3f;
							base.NPC.ai[1] = cellCenter.X;
							base.NPC.ai[2] = cellCenter.Y;
							Vector2 cellCenter2 = base.NPC.Center;
							cellCenter2.Y = Main.player[base.NPC.target].Center.Y;
							if (((Vector2)(ref cellFaceDirection)).Length() > 8f && Collision.CanHit(base.NPC.Center, 1, 1, cellCenter2, 1, 1) && Collision.CanHit(cellCenter2, 1, 1, Main.player[base.NPC.target].position, 1, 1))
							{
								base.NPC.ai[0] = 3f;
								base.NPC.ai[1] = cellCenter2.X;
								base.NPC.ai[2] = cellCenter2.Y;
							}
						}
						else
						{
							cellCenter = base.NPC.Center;
							cellCenter.Y = Main.player[base.NPC.target].Center.Y;
							Vector2 val = cellCenter - base.NPC.Center;
							if (((Vector2)(ref val)).Length() > 8f && Collision.CanHit(base.NPC.Center, 1, 1, cellCenter, 1, 1))
							{
								base.NPC.ai[0] = 3f;
								base.NPC.ai[1] = cellCenter.X;
								base.NPC.ai[2] = cellCenter.Y;
							}
						}
						if (base.NPC.ai[0] == 0f)
						{
							base.NPC.localAI[0] = 0f;
							((Vector2)(ref cellTargetDirection)).Normalize();
							cellTargetDirection *= 0.5f;
							NPC nPC = base.NPC;
							nPC.velocity += cellTargetDirection;
							base.NPC.ai[0] = 4f;
							base.NPC.ai[1] = 0f;
						}
					}
				}
			}
			else if (base.NPC.ai[0] == 1f)
			{
				if (base.NPC.type == 467)
				{
					base.NPC.damage = base.NPC.defDamage;
				}
				else if (base.NPC.type == 421)
				{
					base.NPC.damage = 0;
				}
				base.NPC.rotation += (float)base.NPC.direction * 0.3f;
				Vector2 attacktargetDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
				if (base.NPC.type == 421)
				{
					attacktargetDirection = Main.player[base.NPC.target].Top - base.NPC.Center;
				}
				float attackTargetDist = ((Vector2)(ref attacktargetDirection)).Length();
				float attackVelocity = (CalamityWorld.death ? 9f : 7.5f);
				attackVelocity += attackTargetDist / 100f;
				int attackVelocityMult = (CalamityWorld.death ? 40 : 45);
				((Vector2)(ref attacktargetDirection)).Normalize();
				attacktargetDirection *= attackVelocity;
				base.NPC.velocity = (base.NPC.velocity * (float)(attackVelocityMult - 1) + attacktargetDirection) / (float)attackVelocityMult;
				if (!Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
				}
				if (base.NPC.type == 421 && attackTargetDist < 40f && Main.player[base.NPC.target].active && !Main.player[base.NPC.target].dead)
				{
					bool headcrabAttach = true;
					for (int p = 0; p < Main.maxNPCs; p++)
					{
						NPC nPC7 = Main.npc[p];
						if (nPC7.active && nPC7.type == base.NPC.type && nPC7.ai[0] == 5f && nPC7.target == base.NPC.target)
						{
							headcrabAttach = false;
							break;
						}
					}
					if (headcrabAttach)
					{
						base.NPC.Center = Main.player[base.NPC.target].Top;
						base.NPC.velocity = Vector2.Zero;
						base.NPC.ai[0] = 5f;
						base.NPC.ai[1] = 0f;
						base.NPC.netUpdate = true;
					}
				}
			}
			else if (base.NPC.ai[0] == 2f)
			{
				if (base.NPC.type == 467 || base.NPC.type == 421)
				{
					base.NPC.damage = 0;
				}
				base.NPC.rotation = base.NPC.velocity.X * 0.1f;
				base.NPC.noTileCollide = true;
				Vector2 idleTargetDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
				float num = ((Vector2)(ref idleTargetDirection)).Length();
				float idleVelocity = (CalamityWorld.death ? 6f : 4.5f);
				int idleVelocityMult = 2;
				((Vector2)(ref idleTargetDirection)).Normalize();
				idleTargetDirection *= idleVelocity;
				base.NPC.velocity = (base.NPC.velocity * (float)(idleVelocityMult - 1) + idleTargetDirection) / (float)idleVelocityMult;
				if (num < 600f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.ai[0] = 0f;
				}
			}
			else if (base.NPC.ai[0] == 3f)
			{
				if (base.NPC.type == 467 || base.NPC.type == 421)
				{
					base.NPC.damage = 0;
				}
				base.NPC.rotation = base.NPC.velocity.X * 0.1f;
				Vector2 blockedCellDirection = new Vector2(base.NPC.ai[1], base.NPC.ai[2]) - base.NPC.Center;
				float blockedTargetDist = ((Vector2)(ref blockedCellDirection)).Length();
				float blockedVelocity = (CalamityWorld.death ? 4f : 3f);
				float blockedVelocityMult = 2f;
				((Vector2)(ref blockedCellDirection)).Normalize();
				blockedCellDirection *= blockedVelocity;
				base.NPC.velocity = (base.NPC.velocity * (blockedVelocityMult - 1f) + blockedCellDirection) / blockedVelocityMult;
				if (base.NPC.collideX || base.NPC.collideY)
				{
					base.NPC.ai[0] = 4f;
					base.NPC.ai[1] = 0f;
				}
				if (blockedTargetDist < blockedVelocity || blockedTargetDist > 800f || Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					base.NPC.ai[0] = 0f;
				}
			}
			else if (base.NPC.ai[0] == 4f)
			{
				if (base.NPC.type == 467 || base.NPC.type == 421)
				{
					base.NPC.damage = 0;
				}
				base.NPC.rotation = base.NPC.velocity.X * 0.1f;
				if (base.NPC.collideX)
				{
					base.NPC.velocity.X *= -0.8f;
				}
				if (base.NPC.collideY)
				{
					base.NPC.velocity.Y *= -0.8f;
				}
				Vector2 smolCellDirection;
				if (base.NPC.velocity.X == 0f && base.NPC.velocity.Y == 0f)
				{
					smolCellDirection = Main.player[base.NPC.target].Center - base.NPC.Center;
					smolCellDirection.Y -= Main.player[base.NPC.target].height / 4;
					((Vector2)(ref smolCellDirection)).Normalize();
					base.NPC.velocity = smolCellDirection * 0.1f;
				}
				float smolCellVelocity = (CalamityWorld.death ? 4f : 3f);
				float smolCellVelocityMult = (CalamityWorld.death ? 16f : 18f);
				smolCellDirection = base.NPC.velocity;
				((Vector2)(ref smolCellDirection)).Normalize();
				smolCellDirection *= smolCellVelocity;
				base.NPC.velocity = (base.NPC.velocity * (smolCellVelocityMult - 1f) + smolCellDirection) / smolCellVelocityMult;
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] > 180f)
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
				}
				if (Collision.CanHit(base.NPC.Center, 1, 1, Main.player[base.NPC.target].Center, 1, 1))
				{
					base.NPC.ai[0] = 0f;
				}
				base.NPC.localAI[0]++;
				if (base.NPC.localAI[0] >= 5f && !Collision.SolidCollision(base.NPC.position - new Vector2(10f, 10f), base.NPC.width + 20, base.NPC.height + 20))
				{
					base.NPC.localAI[0] = 0f;
					Vector2 cellCentered = base.NPC.Center;
					cellCentered.X = Main.player[base.NPC.target].Center.X;
					if (Collision.CanHit(base.NPC.Center, 1, 1, cellCentered, 1, 1) && Collision.CanHit(base.NPC.Center, 1, 1, cellCentered, 1, 1) && Collision.CanHit(Main.player[base.NPC.target].Center, 1, 1, cellCentered, 1, 1))
					{
						base.NPC.ai[0] = 3f;
						base.NPC.ai[1] = cellCentered.X;
						base.NPC.ai[2] = cellCentered.Y;
					}
					else
					{
						cellCentered = base.NPC.Center;
						cellCentered.Y = Main.player[base.NPC.target].Center.Y;
						if (Collision.CanHit(base.NPC.Center, 1, 1, cellCentered, 1, 1) && Collision.CanHit(Main.player[base.NPC.target].Center, 1, 1, cellCentered, 1, 1))
						{
							base.NPC.ai[0] = 3f;
							base.NPC.ai[1] = cellCentered.X;
							base.NPC.ai[2] = cellCentered.Y;
						}
					}
				}
			}
			else if (base.NPC.ai[0] == 5f)
			{
				Player player8 = Main.player[base.NPC.target];
				if (!player8.active || player8.dead)
				{
					base.NPC.damage = 0;
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
				else
				{
					base.NPC.damage = base.NPC.defDamage;
					base.NPC.Center = ((player8.gravDir == 1f) ? player8.Top : player8.Bottom) + new Vector2((float)(player8.direction * 4), 0f);
					base.NPC.gfxOffY = player8.gfxOffY;
					base.NPC.velocity = Vector2.Zero;
					if (!player8.creativeGodMode)
					{
						player8.AddBuff(163, 59);
					}
				}
			}
			if (base.NPC.type == 405)
			{
				base.NPC.rotation = 0f;
				for (int r = 0; r < Main.maxNPCs; r++)
				{
					if (r != base.NPC.whoAmI && Main.npc[r].active && Main.npc[r].type == base.NPC.type && Math.Abs(base.NPC.position.X - Main.npc[r].position.X) + Math.Abs(base.NPC.position.Y - Main.npc[r].position.Y) < (float)base.NPC.width)
					{
						if (base.NPC.position.X < Main.npc[r].position.X)
						{
							base.NPC.velocity.X -= 0.05f;
						}
						else
						{
							base.NPC.velocity.X += 0.05f;
						}
						if (base.NPC.position.Y < Main.npc[r].position.Y)
						{
							base.NPC.velocity.Y -= 0.05f;
						}
						else
						{
							base.NPC.velocity.Y += 0.05f;
						}
					}
				}
			}
			else
			{
				if (base.NPC.type != 421)
				{
					return false;
				}
				base.NPC.hide = base.NPC.ai[0] == 5f;
				base.NPC.rotation = base.NPC.velocity.X * 0.1f;
				for (int s = 0; s < Main.maxNPCs; s++)
				{
					if (s != base.NPC.whoAmI && Main.npc[s].active && Main.npc[s].type == base.NPC.type && Math.Abs(base.NPC.position.X - Main.npc[s].position.X) + Math.Abs(base.NPC.position.Y - Main.npc[s].position.Y) < (float)base.NPC.width)
					{
						if (base.NPC.position.X < Main.npc[s].position.X)
						{
							base.NPC.velocity.X -= 0.05f;
						}
						else
						{
							base.NPC.velocity.X += 0.05f;
						}
						if (base.NPC.position.Y < Main.npc[s].position.Y)
						{
							base.NPC.velocity.Y -= 0.05f;
						}
						else
						{
							base.NPC.velocity.Y += 0.05f;
						}
					}
				}
			}
			return false;
		}
	}

	public class SwimmingAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0567: Unknown result type (might be due to invalid IL or missing references)
			//IL_0576: Unknown result type (might be due to invalid IL or missing references)
			//IL_0585: Unknown result type (might be due to invalid IL or missing references)
			//IL_058a: Unknown result type (might be due to invalid IL or missing references)
			//IL_058f: Unknown result type (might be due to invalid IL or missing references)
			//IL_086a: Unknown result type (might be due to invalid IL or missing references)
			//IL_087f: Unknown result type (might be due to invalid IL or missing references)
			//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0500: Unknown result type (might be due to invalid IL or missing references)
			//IL_0505: Unknown result type (might be due to invalid IL or missing references)
			if (base.NPC.direction == 0)
			{
				base.NPC.TargetClosest();
			}
			if (base.NPC.wet)
			{
				bool noWetTargets = false;
				base.NPC.TargetClosest(faceTarget: false);
				if ((Main.player[base.NPC.target].wet || (CalamityWorld.death && base.NPC.Distance(Main.player[base.NPC.target].Center) < 400f)) && !Main.player[base.NPC.target].dead)
				{
					noWetTargets = true;
				}
				if (!noWetTargets)
				{
					if (base.NPC.collideX)
					{
						base.NPC.velocity.X *= -1f;
						base.NPC.direction *= -1;
						base.NPC.netUpdate = true;
					}
					if (base.NPC.collideY)
					{
						base.NPC.netUpdate = true;
						if (base.NPC.velocity.Y > 0f)
						{
							base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y) * -1f;
							base.NPC.directionY = -1;
							base.NPC.ai[0] = -1f;
						}
						else if (base.NPC.velocity.Y < 0f)
						{
							base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y);
							base.NPC.directionY = 1;
							base.NPC.ai[0] = 1f;
						}
					}
				}
				if (base.NPC.type == 102)
				{
					Lighting.AddLight((int)(base.NPC.position.X + (float)(base.NPC.width / 2) + (float)(base.NPC.direction * (base.NPC.width + 8))) / 16, (int)(base.NPC.position.Y + 2f) / 16, 0.07f, 0.04f, 0.025f);
				}
				if (noWetTargets)
				{
					base.NPC.TargetClosest();
					if (base.NPC.type == 157)
					{
						if ((base.NPC.velocity.X > 0f).ToDirectionInt() != (base.NPC.velocity.X > 0f).ToDirectionInt())
						{
							base.NPC.velocity.X *= 0.95f;
						}
						base.NPC.velocity.X += (float)base.NPC.direction * 0.5f;
						base.NPC.velocity.Y += (float)base.NPC.directionY * 0.4f;
						if (base.NPC.velocity.X > 16f)
						{
							base.NPC.velocity.X = 14f;
						}
						if (base.NPC.velocity.X < -16f)
						{
							base.NPC.velocity.X = -14f;
						}
						if (base.NPC.velocity.Y > 10f)
						{
							base.NPC.velocity.Y = 8f;
						}
						if (base.NPC.velocity.Y < -10f)
						{
							base.NPC.velocity.Y = -8f;
						}
					}
					else if (base.NPC.type == 65 || base.NPC.type == 102)
					{
						base.NPC.velocity.X += (float)base.NPC.direction * 0.3f;
						base.NPC.velocity.Y += (float)base.NPC.directionY * 0.3f;
						if (base.NPC.velocity.X > 10f)
						{
							base.NPC.velocity.X = 10f;
						}
						if (base.NPC.velocity.X < -10f)
						{
							base.NPC.velocity.X = -10f;
						}
						if (base.NPC.velocity.Y > 6f)
						{
							base.NPC.velocity.Y = 6f;
						}
						if (base.NPC.velocity.Y < -6f)
						{
							base.NPC.velocity.Y = -6f;
						}
						base.NPC.velocity = Vector2.Clamp(base.NPC.velocity, new Vector2(-10f, -6f), new Vector2(10f, 6f));
					}
					else
					{
						base.NPC.velocity.X += (float)base.NPC.direction * 0.2f;
						base.NPC.velocity.Y += (float)base.NPC.directionY * 0.2f;
						base.NPC.velocity = Vector2.Clamp(base.NPC.velocity, new Vector2(-6f, -4f), new Vector2(6f, 4f));
					}
				}
				else
				{
					if (base.NPC.type == 157)
					{
						base.NPC.directionY = (Main.player[base.NPC.target].position.Y > base.NPC.position.Y).ToDirectionInt();
						base.NPC.velocity.X += (float)base.NPC.direction * 0.2f;
						if (base.NPC.velocity.X < -2f || base.NPC.velocity.X > 2f)
						{
							base.NPC.velocity.X *= 0.95f;
						}
						if (base.NPC.ai[0] == -1f)
						{
							float yVelocityMin = -0.6f;
							if (base.NPC.directionY < 0)
							{
								yVelocityMin = -1f;
							}
							if (base.NPC.directionY > 0)
							{
								yVelocityMin = -0.2f;
							}
							base.NPC.velocity.Y -= 0.02f;
							if (base.NPC.velocity.Y < yVelocityMin)
							{
								base.NPC.ai[0] = 1f;
							}
						}
						else
						{
							float yVelocityMin2 = 0.6f;
							if (base.NPC.directionY < 0)
							{
								yVelocityMin2 = 0.2f;
							}
							if (base.NPC.directionY > 0)
							{
								yVelocityMin2 = 1f;
							}
							base.NPC.velocity.Y += 0.02f;
							if (base.NPC.velocity.Y > yVelocityMin2)
							{
								base.NPC.ai[0] = -1f;
							}
						}
					}
					else
					{
						base.NPC.velocity.X += (float)base.NPC.direction * 0.1f;
						if (base.NPC.velocity.X < -1f || base.NPC.velocity.X > 1f)
						{
							base.NPC.velocity.X *= 0.95f;
						}
						if (base.NPC.ai[0] == -1f)
						{
							base.NPC.velocity.Y -= 0.01f;
							if (base.NPC.velocity.Y < -0.3f)
							{
								base.NPC.ai[0] = 1f;
							}
						}
						else
						{
							base.NPC.velocity.Y += 0.01f;
							if ((double)base.NPC.velocity.Y > 0.3)
							{
								base.NPC.ai[0] = -1f;
							}
						}
					}
					int x = (int)base.NPC.Center.X / 16;
					int y = (int)base.NPC.Center.Y / 16;
					if (Main.tile[x, y - 1].LiquidAmount > 128)
					{
						if (Main.tile[x, y + 1].HasTile)
						{
							base.NPC.ai[0] = -1f;
						}
						else if (Main.tile[x, y + 2].HasTile)
						{
							base.NPC.ai[0] = -1f;
						}
					}
					if (base.NPC.type != 157 && Math.Abs(base.NPC.velocity.Y) < 0.4f)
					{
						base.NPC.velocity.Y *= 0.95f;
					}
				}
			}
			else
			{
				if (base.NPC.velocity.Y == 0f)
				{
					if (base.NPC.type == 65)
					{
						base.NPC.velocity.X *= 0.94f;
						if ((double)Math.Abs(base.NPC.velocity.X) < 0.2)
						{
							base.NPC.velocity.X = 0f;
						}
					}
					else if (Main.netMode != 1)
					{
						base.NPC.velocity.Y = Main.rand.NextFloat(-5f, -2f);
						base.NPC.velocity.X = Main.rand.NextFloat(-2f, -2f);
						base.NPC.netUpdate = true;
					}
				}
				base.NPC.velocity.Y += 0.3f;
				if (base.NPC.velocity.Y > 10f)
				{
					base.NPC.velocity.Y = 10f;
				}
				base.NPC.ai[0] = 1f;
			}
			base.NPC.rotation = base.NPC.velocity.Y * (float)base.NPC.direction * 0.1f;
			base.NPC.rotation = MathHelper.Clamp(base.NPC.rotation, -0.2f, 0.2f);
			return false;
		}
	}

	public class TeslaTurretAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0379: Unknown result type (might be due to invalid IL or missing references)
			//IL_038d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0143: Unknown result type (might be due to invalid IL or missing references)
			//IL_0158: Unknown result type (might be due to invalid IL or missing references)
			//IL_015e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0160: Unknown result type (might be due to invalid IL or missing references)
			//IL_016f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0174: Unknown result type (might be due to invalid IL or missing references)
			//IL_0179: Unknown result type (might be due to invalid IL or missing references)
			//IL_0182: Unknown result type (might be due to invalid IL or missing references)
			//IL_0187: Unknown result type (might be due to invalid IL or missing references)
			//IL_032f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0343: Unknown result type (might be due to invalid IL or missing references)
			//IL_0456: Unknown result type (might be due to invalid IL or missing references)
			//IL_046c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0476: Unknown result type (might be due to invalid IL or missing references)
			//IL_047b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0486: Unknown result type (might be due to invalid IL or missing references)
			//IL_048b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0495: Unknown result type (might be due to invalid IL or missing references)
			//IL_049a: Unknown result type (might be due to invalid IL or missing references)
			//IL_049f: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0201: Unknown result type (might be due to invalid IL or missing references)
			//IL_0206: Unknown result type (might be due to invalid IL or missing references)
			//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_04af: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0215: Unknown result type (might be due to invalid IL or missing references)
			//IL_022f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0235: Unknown result type (might be due to invalid IL or missing references)
			//IL_025f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0264: Unknown result type (might be due to invalid IL or missing references)
			//IL_0267: Unknown result type (might be due to invalid IL or missing references)
			//IL_026c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0271: Unknown result type (might be due to invalid IL or missing references)
			//IL_0273: Unknown result type (might be due to invalid IL or missing references)
			//IL_0278: Unknown result type (might be due to invalid IL or missing references)
			//IL_0285: Unknown result type (might be due to invalid IL or missing references)
			//IL_028a: Unknown result type (might be due to invalid IL or missing references)
			//IL_028f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0295: Unknown result type (might be due to invalid IL or missing references)
			//IL_029a: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0304: Unknown result type (might be due to invalid IL or missing references)
			//IL_0311: Unknown result type (might be due to invalid IL or missing references)
			//IL_0316: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.TargetClosest(faceTarget: false);
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.velocity.X *= 0.93f;
			if (Math.Abs(base.NPC.velocity.X) < 0.1f)
			{
				base.NPC.velocity.X = 0f;
			}
			float appearTime = 120f;
			float alphaFadeinTime = 60f;
			if (base.NPC.ai[1] < appearTime)
			{
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] > appearTime - alphaFadeinTime)
				{
					float alphaRatio = (base.NPC.ai[1] - alphaFadeinTime) / (appearTime - alphaFadeinTime);
					base.NPC.alpha = (int)((1f - alphaRatio) * 255f);
				}
				else
				{
					base.NPC.alpha = 255;
				}
				base.NPC.dontTakeDamage = true;
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y = 0;
				float angularRatio = base.NPC.ai[1] / alphaFadeinTime;
				Vector2 spinningpoint = Utils.RotatedBy(new Vector2(0f, -30f), (double)(angularRatio * 1.5f * ((float)Math.PI * 2f)), default(Vector2)) * new Vector2(1f, 0.4f);
				for (int i = 0; i < 4; i++)
				{
					Vector2 dustSpawnDelta = Vector2.Zero;
					float scaleFactor2 = 1f;
					switch (i)
					{
					case 0:
						dustSpawnDelta = Vector2.UnitY * -15f;
						scaleFactor2 = 0.15f;
						break;
					case 1:
						dustSpawnDelta = Vector2.UnitY * -5f;
						scaleFactor2 = 0.3f;
						break;
					case 2:
						dustSpawnDelta = Vector2.UnitY * 5f;
						scaleFactor2 = 0.6f;
						break;
					case 3:
						dustSpawnDelta = Vector2.UnitY * 20f;
						scaleFactor2 = 0.45f;
						break;
					}
					int idx = Dust.NewDust(base.NPC.Center, 0, 0, 226, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[idx].noGravity = true;
					Main.dust[idx].position = base.NPC.Center + spinningpoint * scaleFactor2 + dustSpawnDelta;
					Main.dust[idx].velocity = Vector2.Zero;
					spinningpoint *= -1f;
					idx = Dust.NewDust(base.NPC.Center, 0, 0, 226, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[idx].noGravity = true;
					Main.dust[idx].position = base.NPC.Center + spinningpoint * scaleFactor2 + dustSpawnDelta;
					Main.dust[idx].velocity = Vector2.Zero;
				}
				Lighting.AddLight((int)base.NPC.Center.X / 16, (int)(base.NPC.Center.Y - 10f) / 16, 0.1f * angularRatio, 0.5f * angularRatio, 0.7f * angularRatio);
				return false;
			}
			Lighting.AddLight((int)base.NPC.Center.X / 16, (int)(base.NPC.Center.Y - 10f) / 16, 0.1f, 0.5f, 0.7f);
			base.NPC.dontTakeDamage = false;
			if (base.NPC.ai[0] < 60f)
			{
				base.NPC.ai[0]++;
			}
			if (base.NPC.justHit)
			{
				base.NPC.ai[0] = 0f;
			}
			if (base.NPC.ai[0] == 60f)
			{
				base.NPC.ai[0] = (CalamityWorld.death ? (-60f) : (-120f));
				Vector2 distanceVector = Main.player[base.NPC.target].Center + Main.player[base.NPC.target].velocity * 20f - (base.NPC.Center - Vector2.UnitY * 10f);
				if (distanceVector.HasNaNs())
				{
					distanceVector = -Vector2.UnitY;
				}
				Vector2 velocity = Vector2.Normalize(distanceVector) * 14f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center - Vector2.UnitY * 10f, velocity, 435, 28, 0f, Main.myPlayer);
			}
			return false;
		}
	}

	public class TortoiseAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0110: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0604: Unknown result type (might be due to invalid IL or missing references)
			//IL_0629: Unknown result type (might be due to invalid IL or missing references)
			//IL_0633: Unknown result type (might be due to invalid IL or missing references)
			//IL_0638: Unknown result type (might be due to invalid IL or missing references)
			//IL_0738: Unknown result type (might be due to invalid IL or missing references)
			//IL_075b: Unknown result type (might be due to invalid IL or missing references)
			//IL_077c: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_120a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1257: Unknown result type (might be due to invalid IL or missing references)
			//IL_125d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1282: Unknown result type (might be due to invalid IL or missing references)
			//IL_128c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1291: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a3a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a44: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a49: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a83: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a89: Unknown result type (might be due to invalid IL or missing references)
			//IL_1aa8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ab2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ab7: Unknown result type (might be due to invalid IL or missing references)
			//IL_13d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_13fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dc1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ded: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b14: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b1e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b23: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b28: Unknown result type (might be due to invalid IL or missing references)
			//IL_1eba: Unknown result type (might be due to invalid IL or missing references)
			//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f03: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ce0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d0b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d11: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d28: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d37: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d3c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b32: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b47: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b4d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b6c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b76: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b7b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b80: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b95: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b9b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bab: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bb5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bba: Unknown result type (might be due to invalid IL or missing references)
			//IL_14f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1541: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bf2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c20: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c25: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c34: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c39: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c40: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c46: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c6c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c76: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c7b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c94: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c9e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f5c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f66: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f6b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e09: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e0e: Unknown result type (might be due to invalid IL or missing references)
			if (base.NPC.target < 0 || Main.player[base.NPC.target].dead || base.NPC.direction == 0)
			{
				base.NPC.TargetClosest();
			}
			int turtleFaceDirection = 0;
			if (base.NPC.velocity.X < 0f)
			{
				turtleFaceDirection = -1;
			}
			if (base.NPC.velocity.X > 0f)
			{
				turtleFaceDirection = 1;
			}
			Vector2 position = base.NPC.position;
			position.X += base.NPC.velocity.X;
			int turtleTileX = (int)((position.X + (float)(base.NPC.width / 2) + (float)((base.NPC.width / 2 + 1) * turtleFaceDirection)) / 16f);
			int turtleTileY = (int)((position.Y + (float)base.NPC.height - 1f) / 16f);
			if ((float)(turtleTileX * 16) < position.X + (float)base.NPC.width && (float)(turtleTileX * 16 + 16) > position.X && ((Main.tile[turtleTileX, turtleTileY].HasUnactuatedTile && !Main.tile[turtleTileX, turtleTileY].TopSlope && !Main.tile[turtleTileX, turtleTileY - 1].TopSlope && ((Main.tileSolid[Main.tile[turtleTileX, turtleTileY].TileType] && !Main.tileSolidTop[Main.tile[turtleTileX, turtleTileY].TileType]) || (Main.tileSolidTop[Main.tile[turtleTileX, turtleTileY].TileType] && (!Main.tileSolid[Main.tile[turtleTileX, turtleTileY - 1].TileType] || !Main.tile[turtleTileX, turtleTileY - 1].HasUnactuatedTile) && Main.tile[turtleTileX, turtleTileY].TileType != 16 && Main.tile[turtleTileX, turtleTileY].TileType != 18 && Main.tile[turtleTileX, turtleTileY].TileType != 134))) || (Main.tile[turtleTileX, turtleTileY - 1].IsHalfBlock && Main.tile[turtleTileX, turtleTileY - 1].HasUnactuatedTile)) && (!Main.tile[turtleTileX, turtleTileY - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[turtleTileX, turtleTileY - 1].TileType] || Main.tileSolidTop[Main.tile[turtleTileX, turtleTileY - 1].TileType] || (Main.tile[turtleTileX, turtleTileY - 1].IsHalfBlock && (!Main.tile[turtleTileX, turtleTileY - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[turtleTileX, turtleTileY - 4].TileType] || Main.tileSolidTop[Main.tile[turtleTileX, turtleTileY - 4].TileType]))) && (!Main.tile[turtleTileX, turtleTileY - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[turtleTileX, turtleTileY - 2].TileType] || Main.tileSolidTop[Main.tile[turtleTileX, turtleTileY - 2].TileType]) && (!Main.tile[turtleTileX, turtleTileY - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[turtleTileX, turtleTileY - 3].TileType] || Main.tileSolidTop[Main.tile[turtleTileX, turtleTileY - 3].TileType]) && (!Main.tile[turtleTileX - turtleFaceDirection, turtleTileY - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[turtleTileX - turtleFaceDirection, turtleTileY - 3].TileType] || Main.tileSolidTop[Main.tile[turtleTileX - turtleFaceDirection, turtleTileY - 3].TileType]))
			{
				float tilePixelPosition = turtleTileY * 16;
				if (Main.tile[turtleTileX, turtleTileY].IsHalfBlock)
				{
					tilePixelPosition += 8f;
				}
				if (Main.tile[turtleTileX, turtleTileY - 1].IsHalfBlock)
				{
					tilePixelPosition -= 8f;
				}
				if (tilePixelPosition < position.Y + (float)base.NPC.height)
				{
					float percentageTileRisen = position.Y + (float)base.NPC.height - tilePixelPosition;
					if ((double)percentageTileRisen <= 16.1)
					{
						base.NPC.gfxOffY += base.NPC.position.Y + (float)base.NPC.height - tilePixelPosition;
						base.NPC.position.Y = tilePixelPosition - (float)base.NPC.height;
						if (percentageTileRisen < 9f)
						{
							base.NPC.stepSpeed = 0.75f;
						}
						else
						{
							base.NPC.stepSpeed = 1.5f;
						}
					}
				}
			}
			if (base.NPC.type == 154 && Main.rand.NextBool(10))
			{
				int iceTortoiseDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 67, base.NPC.velocity.X * 0.5f, base.NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
				Main.dust[iceTortoiseDust].noGravity = true;
				Dust obj = Main.dust[iceTortoiseDust];
				obj.velocity *= 0.2f;
			}
			if (base.NPC.ai[0] == 0f)
			{
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.direction = -1;
				}
				else if (base.NPC.velocity.X > 0f)
				{
					base.NPC.direction = 1;
				}
				base.NPC.spriteDirection = base.NPC.direction;
				Vector2 tortoisePosition = default(Vector2);
				((Vector2)(ref tortoisePosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
				float num = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - tortoisePosition.X;
				float tortoiseTargetY = Main.player[base.NPC.target].position.Y - tortoisePosition.Y;
				float tortoiseTargetDist = (float)Math.Sqrt(num * num + tortoiseTargetY * tortoiseTargetY);
				bool canHitPlayer = Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
				if (base.NPC.type == 496 || base.NPC.type == 497)
				{
					if ((tortoiseTargetDist > 200f) & canHitPlayer)
					{
						base.NPC.ai[1] += 3f;
					}
					if (tortoiseTargetDist > 600f && (canHitPlayer || base.NPC.position.Y + (float)base.NPC.height > Main.player[base.NPC.target].position.Y - 200f))
					{
						base.NPC.ai[1] += 6f;
					}
				}
				else
				{
					if ((tortoiseTargetDist > 200f) & canHitPlayer)
					{
						base.NPC.ai[1] += 6f;
					}
					if (tortoiseTargetDist > 600f && (canHitPlayer || base.NPC.position.Y + (float)base.NPC.height > Main.player[base.NPC.target].position.Y - 200f))
					{
						base.NPC.ai[1] += 15f;
					}
				}
				if (base.NPC.wet)
				{
					base.NPC.ai[1] = 1000f;
				}
				base.NPC.defense = base.NPC.defDefense;
				base.NPC.damage = 0;
				if (base.NPC.type == 496 || base.NPC.type == 497)
				{
					base.NPC.knockBackResist = 0.5f;
				}
				else
				{
					base.NPC.knockBackResist = 0.15f;
				}
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] >= (CalamityWorld.death ? 400f : 500f))
				{
					base.NPC.ai[1] = 0f;
					base.NPC.ai[0] = 1f;
				}
				if (!base.NPC.justHit && base.NPC.velocity.X != base.NPC.oldVelocity.X)
				{
					base.NPC.direction *= -1;
				}
				if (base.NPC.velocity.Y == 0f && Main.player[base.NPC.target].position.Y < base.NPC.position.Y + (float)base.NPC.height)
				{
					int tortoiseLeftTileX;
					int tortoiseRightTileX;
					if (base.NPC.direction > 0)
					{
						tortoiseLeftTileX = (int)(((double)base.NPC.position.X + (double)base.NPC.width * 0.5) / 16.0);
						tortoiseRightTileX = tortoiseLeftTileX + 3;
					}
					else
					{
						tortoiseRightTileX = (int)(((double)base.NPC.position.X + (double)base.NPC.width * 0.5) / 16.0);
						tortoiseLeftTileX = tortoiseRightTileX - 3;
					}
					int tortoiseBotTileY = (int)((base.NPC.position.Y + (float)base.NPC.height + 2f) / 16f) - 1;
					int tortoiseTopTileY = tortoiseBotTileY + 4;
					bool onSolidTile = false;
					for (int x = tortoiseLeftTileX; x <= tortoiseRightTileX; x++)
					{
						for (int y = tortoiseBotTileY; y <= tortoiseTopTileY; y++)
						{
							if (Main.tile[x, y] != null && Main.tile[x, y].HasUnactuatedTile && Main.tileSolid[Main.tile[x, y].TileType])
							{
								onSolidTile = true;
							}
						}
					}
					if (!onSolidTile)
					{
						base.NPC.direction *= -1;
						base.NPC.velocity.X = 0.1f * (float)base.NPC.direction;
					}
				}
				if (base.NPC.type == 496 || base.NPC.type == 497)
				{
					float giantShellyMaxVel = 1f;
					if (base.NPC.velocity.X < 0f - giantShellyMaxVel || base.NPC.velocity.X > giantShellyMaxVel)
					{
						if (base.NPC.velocity.Y == 0f)
						{
							NPC nPC = base.NPC;
							nPC.velocity *= 0.8f;
						}
					}
					else if (base.NPC.velocity.X < giantShellyMaxVel && base.NPC.direction == 1)
					{
						base.NPC.velocity.X = base.NPC.velocity.X + 0.1f;
						if (base.NPC.velocity.X > giantShellyMaxVel)
						{
							base.NPC.velocity.X = giantShellyMaxVel;
						}
					}
					else if (base.NPC.velocity.X > 0f - giantShellyMaxVel && base.NPC.direction == -1)
					{
						base.NPC.velocity.X = base.NPC.velocity.X - 0.1f;
						if (base.NPC.velocity.X < 0f - giantShellyMaxVel)
						{
							base.NPC.velocity.X = 0f - giantShellyMaxVel;
						}
					}
				}
				else
				{
					float tortoiseMaxVel = 2f;
					if (tortoiseTargetDist < 400f)
					{
						if (base.NPC.velocity.X < 0f - tortoiseMaxVel || base.NPC.velocity.X > tortoiseMaxVel)
						{
							if (base.NPC.velocity.Y == 0f)
							{
								NPC nPC2 = base.NPC;
								nPC2.velocity *= 0.8f;
							}
						}
						else if (base.NPC.velocity.X < tortoiseMaxVel && base.NPC.direction == 1)
						{
							base.NPC.velocity.X = base.NPC.velocity.X + 0.1f;
							if (base.NPC.velocity.X > tortoiseMaxVel)
							{
								base.NPC.velocity.X = tortoiseMaxVel;
							}
						}
						else if (base.NPC.velocity.X > 0f - tortoiseMaxVel && base.NPC.direction == -1)
						{
							base.NPC.velocity.X = base.NPC.velocity.X - 0.1f;
							if (base.NPC.velocity.X < 0f - tortoiseMaxVel)
							{
								base.NPC.velocity.X = 0f - tortoiseMaxVel;
							}
						}
					}
					else if (base.NPC.velocity.X < -3f || base.NPC.velocity.X > 3f)
					{
						if (base.NPC.velocity.Y == 0f)
						{
							NPC nPC3 = base.NPC;
							nPC3.velocity *= 0.8f;
						}
					}
					else if (base.NPC.velocity.X < 3f && base.NPC.direction == 1)
					{
						base.NPC.velocity.X = base.NPC.velocity.X + 0.1f;
						if (base.NPC.velocity.X > 3f)
						{
							base.NPC.velocity.X = 3f;
						}
					}
					else if (base.NPC.velocity.X > -3f && base.NPC.direction == -1)
					{
						base.NPC.velocity.X = base.NPC.velocity.X - 0.1f;
						if (base.NPC.velocity.X < -3f)
						{
							base.NPC.velocity.X = -3f;
						}
					}
				}
			}
			else if (base.NPC.ai[0] == 1f)
			{
				base.NPC.damage = 0;
				base.NPC.velocity.X = base.NPC.velocity.X * 0.5f;
				if (base.NPC.type == 496 || base.NPC.type == 497)
				{
					base.NPC.ai[1]++;
				}
				else
				{
					base.NPC.ai[1] += 2f;
				}
				if (base.NPC.ai[1] >= 30f)
				{
					base.NPC.netUpdate = true;
					base.NPC.TargetClosest();
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[0] = 3f;
					if (base.NPC.type == 417)
					{
						base.NPC.ai[0] = 6f;
						base.NPC.ai[2] = Main.rand.Next(2, 5);
					}
				}
			}
			else
			{
				if (base.NPC.ai[0] == 3f)
				{
					if (base.NPC.type == 154 && Main.rand.Next(3) < 2)
					{
						int iceTortoiseSpinDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 67, base.NPC.velocity.X * 0.5f, base.NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
						Main.dust[iceTortoiseSpinDust].noGravity = true;
						Dust obj2 = Main.dust[iceTortoiseSpinDust];
						obj2.velocity *= 0.2f;
					}
					if (base.NPC.type == 496 || base.NPC.type == 497)
					{
						base.NPC.damage = (int)Math.Round((double)base.NPC.defDamage * 1.2);
					}
					else
					{
						base.NPC.damage = (int)Math.Round((double)base.NPC.defDamage * 1.4);
					}
					base.NPC.defense = base.NPC.defDefense * 2;
					base.NPC.ai[1]++;
					if (base.NPC.ai[1] == 1f)
					{
						base.NPC.netUpdate = true;
						base.NPC.TargetClosest();
						base.NPC.ai[2] += 0.3f;
						base.NPC.rotation += base.NPC.ai[2] * (float)base.NPC.direction;
						base.NPC.ai[1]++;
						bool num2 = Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
						float spinAttackSpeed = 15f;
						if (!num2)
						{
							spinAttackSpeed = 6f;
						}
						if (base.NPC.type == 496 || base.NPC.type == 497)
						{
							spinAttackSpeed *= 0.75f;
						}
						Vector2 spinAttackPosition = default(Vector2);
						((Vector2)(ref spinAttackPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
						float spinAttackTargetX = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - spinAttackPosition.X;
						float absoluteSpinTargetX = Math.Abs(spinAttackTargetX) * 0.2f;
						if (base.NPC.directionY > 0)
						{
							absoluteSpinTargetX = 0f;
						}
						float spinAttackTargetY = Main.player[base.NPC.target].position.Y - spinAttackPosition.Y - absoluteSpinTargetX;
						float spinAttackTargetDist = (float)Math.Sqrt(spinAttackTargetX * spinAttackTargetX + spinAttackTargetY * spinAttackTargetY);
						base.NPC.netUpdate = true;
						spinAttackTargetDist = spinAttackSpeed / spinAttackTargetDist;
						spinAttackTargetX *= spinAttackTargetDist;
						spinAttackTargetY *= spinAttackTargetDist;
						if (!num2)
						{
							spinAttackTargetY = -10f;
						}
						base.NPC.velocity.X = spinAttackTargetX;
						base.NPC.velocity.Y = spinAttackTargetY;
						base.NPC.ai[3] = base.NPC.velocity.X;
					}
					else
					{
						if (base.NPC.position.X + (float)base.NPC.width > Main.player[base.NPC.target].position.X && base.NPC.position.X < Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width && base.NPC.position.Y < Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height)
						{
							base.NPC.velocity.X = base.NPC.velocity.X * 0.8f;
							base.NPC.ai[3] = 0f;
							if (base.NPC.velocity.Y < 0f)
							{
								base.NPC.velocity.Y = base.NPC.velocity.Y + 0.3f;
							}
						}
						if (base.NPC.ai[3] != 0f)
						{
							base.NPC.velocity.X = base.NPC.ai[3];
							base.NPC.velocity.Y = base.NPC.velocity.Y - 0.33f;
						}
						if (base.NPC.ai[1] >= 90f)
						{
							base.NPC.noGravity = false;
							base.NPC.ai[1] = 0f;
							base.NPC.ai[0] = 4f;
						}
					}
					if (base.NPC.wet && base.NPC.directionY < 0)
					{
						base.NPC.velocity.Y = base.NPC.velocity.Y - 0.45f;
					}
					base.NPC.rotation += base.NPC.ai[2] * (float)base.NPC.direction;
					return false;
				}
				if (base.NPC.ai[0] == 4f)
				{
					base.NPC.damage = 0;
					if (base.NPC.wet && base.NPC.directionY < 0)
					{
						base.NPC.velocity.Y = base.NPC.velocity.Y - 0.45f;
					}
					base.NPC.velocity.X = base.NPC.velocity.X * 0.95f;
					if (base.NPC.ai[2] > 0f)
					{
						base.NPC.ai[2] -= 0.01f;
						base.NPC.rotation += base.NPC.ai[2] * (float)base.NPC.direction;
					}
					else if (base.NPC.velocity.Y >= 0f)
					{
						base.NPC.rotation = 0f;
					}
					if (base.NPC.ai[2] <= 0f && (base.NPC.velocity.Y == 0f || base.NPC.wet))
					{
						base.NPC.netUpdate = true;
						base.NPC.rotation = 0f;
						base.NPC.ai[2] = 0f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[0] = 5f;
					}
				}
				else
				{
					if (base.NPC.ai[0] == 6f)
					{
						base.NPC.damage = (int)Math.Round((double)base.NPC.defDamage * 1.2);
						base.NPC.defense = base.NPC.defDefense * 2;
						base.NPC.knockBackResist = 0f;
						if (Main.rand.Next(3) < 2)
						{
							int spinAttackDust = Dust.NewDust(base.NPC.Center - new Vector2(30f), 60, 60, 6, base.NPC.velocity.X * 0.5f, base.NPC.velocity.Y * 0.5f, 90, default(Color), 1.5f);
							Dust obj3 = Main.dust[spinAttackDust];
							obj3.noGravity = true;
							obj3.velocity *= 0.2f;
							obj3.fadeIn = 1f;
						}
						base.NPC.ai[1]++;
						if (base.NPC.ai[3] > 0f)
						{
							if (base.NPC.ai[3] == 1f)
							{
								Vector2 vector68 = base.NPC.Center - new Vector2(50f);
								for (int i = 0; i < 32; i++)
								{
									int spinEndDust = Dust.NewDust(vector68, 100, 100, 6, 0f, 0f, 100, default(Color), 2.5f);
									Dust obj4 = Main.dust[spinEndDust];
									obj4.noGravity = true;
									obj4.velocity *= 3f;
									spinEndDust = Dust.NewDust(vector68, 100, 100, 6, 0f, 0f, 100, default(Color), 1.5f);
									obj4.velocity *= 2f;
									obj4.noGravity = true;
								}
								if (!Main.dedServ)
								{
									for (int j = 0; j < 4; j++)
									{
										int spinEndGore = Gore.NewGore(base.NPC.GetSource_FromAI(), vector68 + new Vector2((float)(50 * Main.rand.Next(100)) / 100f, (float)(50 * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64));
										Gore obj5 = Main.gore[spinEndGore];
										obj5.velocity *= 0.3f;
										obj5.velocity.X += (float)Main.rand.Next(-10, 11) * 0.05f;
										obj5.velocity.Y += (float)Main.rand.Next(-10, 11) * 0.05f;
									}
								}
							}
							for (int k = 0; k < 5; k++)
							{
								int moreSpinEndDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 31, 0f, 0f, 100, default(Color), 1.5f);
								Dust obj6 = Main.dust[moreSpinEndDust];
								obj6.velocity *= Main.rand.NextFloat();
							}
							base.NPC.ai[3]++;
							if (base.NPC.ai[3] >= 10f)
							{
								base.NPC.ai[3] = 0f;
							}
						}
						if (base.NPC.ai[1] == 1f)
						{
							base.NPC.netUpdate = true;
							base.NPC.TargetClosest();
							bool num3 = Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
							float spinAboveSpeed = 24f;
							if (!num3)
							{
								spinAboveSpeed = 10f;
							}
							Vector2 vector69 = default(Vector2);
							((Vector2)(ref vector69))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
							float spinAboveTargetX = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - vector69.X;
							float absoluteSpinAboveTargetX = Math.Abs(spinAboveTargetX) * 0.2f;
							if (base.NPC.directionY > 0)
							{
								absoluteSpinAboveTargetX = 0f;
							}
							float spinAboveTargetY = Main.player[base.NPC.target].position.Y - vector69.Y - absoluteSpinAboveTargetX;
							float spinAboveTargetDist = (float)Math.Sqrt(spinAboveTargetX * spinAboveTargetX + spinAboveTargetY * spinAboveTargetY);
							base.NPC.netUpdate = true;
							spinAboveTargetDist = spinAboveSpeed / spinAboveTargetDist;
							spinAboveTargetX *= spinAboveTargetDist;
							spinAboveTargetY *= spinAboveTargetDist;
							if (!num3)
							{
								spinAboveTargetY = -12f;
							}
							base.NPC.velocity.X = spinAboveTargetX;
							base.NPC.velocity.Y = spinAboveTargetY;
						}
						else
						{
							if (base.NPC.position.X + (float)base.NPC.width > Main.player[base.NPC.target].position.X && base.NPC.position.X < Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width && base.NPC.position.Y < Main.player[base.NPC.target].position.Y + (float)Main.player[base.NPC.target].height)
							{
								base.NPC.velocity.X = base.NPC.velocity.X * 0.9f;
								if (base.NPC.velocity.Y < 0f)
								{
									base.NPC.velocity.Y = base.NPC.velocity.Y + 0.3f;
								}
							}
							if (base.NPC.ai[2] == 0f || base.NPC.ai[1] >= 1200f)
							{
								base.NPC.ai[1] = 0f;
								base.NPC.ai[0] = 5f;
							}
						}
						if (base.NPC.wet && base.NPC.directionY < 0)
						{
							base.NPC.velocity.Y = base.NPC.velocity.Y - 0.45f;
						}
						base.NPC.rotation += MathHelper.Clamp(base.NPC.velocity.X / 10f * (float)base.NPC.direction, -(float)Math.PI / 10f, (float)Math.PI / 10f);
						return false;
					}
					if (base.NPC.ai[0] == 5f)
					{
						base.NPC.damage = 0;
						base.NPC.rotation = 0f;
						base.NPC.velocity.X = 0f;
						if (base.NPC.type == 496 || base.NPC.type == 497)
						{
							base.NPC.ai[1]++;
						}
						else
						{
							base.NPC.ai[1] += 2f;
						}
						if (base.NPC.ai[1] >= 30f)
						{
							base.NPC.TargetClosest();
							base.NPC.netUpdate = true;
							base.NPC.ai[1] = 0f;
							base.NPC.ai[0] = 0f;
						}
						if (base.NPC.wet)
						{
							base.NPC.ai[0] = 3f;
							base.NPC.ai[1] = 0f;
						}
					}
				}
			}
			return false;
		}
	}

	public class UnicornAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0395: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_065b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0660: Unknown result type (might be due to invalid IL or missing references)
			//IL_0671: Unknown result type (might be due to invalid IL or missing references)
			//IL_067b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0680: Unknown result type (might be due to invalid IL or missing references)
			//IL_0685: Unknown result type (might be due to invalid IL or missing references)
			//IL_068c: Unknown result type (might be due to invalid IL or missing references)
			//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_06de: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0700: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a88: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0757: Unknown result type (might be due to invalid IL or missing references)
			//IL_075c: Unknown result type (might be due to invalid IL or missing references)
			//IL_076d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0777: Unknown result type (might be due to invalid IL or missing references)
			//IL_077c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0781: Unknown result type (might be due to invalid IL or missing references)
			//IL_0788: Unknown result type (might be due to invalid IL or missing references)
			//IL_079e: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_07bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_07da: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_07fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_080c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0816: Unknown result type (might be due to invalid IL or missing references)
			//IL_081b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0820: Unknown result type (might be due to invalid IL or missing references)
			//IL_082b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d0b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d26: Unknown result type (might be due to invalid IL or missing references)
			//IL_0acc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0527: Unknown result type (might be due to invalid IL or missing references)
			//IL_0537: Unknown result type (might be due to invalid IL or missing references)
			//IL_0495: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d4f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dbc: Unknown result type (might be due to invalid IL or missing references)
			//IL_08ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_090d: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_167d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1682: Unknown result type (might be due to invalid IL or missing references)
			//IL_169f: Unknown result type (might be due to invalid IL or missing references)
			//IL_16d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_16fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f92: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f97: Unknown result type (might be due to invalid IL or missing references)
			//IL_171d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fb3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fb5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fba: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fc1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fda: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fe0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fee: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ff3: Unknown result type (might be due to invalid IL or missing references)
			//IL_152f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1539: Unknown result type (might be due to invalid IL or missing references)
			//IL_153e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1263: Unknown result type (might be due to invalid IL or missing references)
			//IL_126d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1272: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a36: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a4f: Unknown result type (might be due to invalid IL or missing references)
			int turnAroundDelay = 30;
			int turnAroundDelayMult = 8;
			bool flag = false;
			bool isRunning = false;
			bool shouldTurnAround = false;
			if (base.NPC.velocity.Y == 0f && ((base.NPC.velocity.X > 0f && base.NPC.direction < 0) || (base.NPC.velocity.X < 0f && base.NPC.direction > 0)))
			{
				isRunning = true;
				base.NPC.ai[3]++;
			}
			if (base.NPC.type == 546)
			{
				turnAroundDelayMult = 3;
				bool noYVelocity = base.NPC.velocity.Y == 0f;
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.whoAmI != base.NPC.whoAmI && n.type == base.NPC.type && Math.Abs(base.NPC.position.X - n.position.X) + Math.Abs(base.NPC.position.Y - n.position.Y) < (float)base.NPC.width)
					{
						if (base.NPC.position.X < n.position.X)
						{
							base.NPC.velocity.X -= 0.05f;
						}
						else
						{
							base.NPC.velocity.X += 0.05f;
						}
						if (base.NPC.position.Y < n.position.Y)
						{
							base.NPC.velocity.Y -= 0.05f;
						}
						else
						{
							base.NPC.velocity.Y += 0.05f;
						}
					}
				}
				if (noYVelocity)
				{
					base.NPC.velocity.Y = 0f;
				}
			}
			if ((base.NPC.position.X == base.NPC.oldPosition.X || base.NPC.ai[3] >= (float)turnAroundDelay) | isRunning)
			{
				base.NPC.ai[3]++;
				shouldTurnAround = true;
			}
			else if (base.NPC.ai[3] > 0f)
			{
				base.NPC.ai[3]--;
			}
			if (base.NPC.ai[3] > (float)(turnAroundDelay * turnAroundDelayMult))
			{
				base.NPC.ai[3] = 0f;
			}
			if (base.NPC.justHit)
			{
				base.NPC.ai[3] = 0f;
			}
			if (base.NPC.ai[3] == (float)turnAroundDelay)
			{
				base.NPC.netUpdate = true;
			}
			Vector2 npcPosition = default(Vector2);
			((Vector2)(ref npcPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
			float num = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - npcPosition.X;
			float targetYDist = Main.player[base.NPC.target].position.Y - npcPosition.Y;
			float targetDistance = (float)Math.Sqrt(num * num + targetYDist * targetYDist);
			if (targetDistance < 200f && !shouldTurnAround)
			{
				base.NPC.ai[3] = 0f;
			}
			if (base.NPC.type == 410)
			{
				base.NPC.ai[1]++;
				bool spawnTwinkle = base.NPC.ai[1] >= (CalamityWorld.death ? 60f : 120f);
				if (!spawnTwinkle && base.NPC.velocity.Y == 0f)
				{
					ActiveEntityIterator<Player>.Enumerator enumerator2 = Main.ActivePlayers.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						Player plr = enumerator2.Current;
						if (!plr.dead && plr.Distance(base.NPC.Center) < 800f && plr.Center.Y < base.NPC.Center.Y && Math.Abs(plr.Center.X - base.NPC.Center.X) < 20f)
						{
							spawnTwinkle = true;
							break;
						}
					}
				}
				if (spawnTwinkle && Main.netMode != 1)
				{
					for (int k = 0; k < 3; k++)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, (Main.rand.NextFloat() - 0.5f) * 2f, -4f - 10f * Main.rand.NextFloat(), 538, 50, 0f, Main.myPlayer);
					}
					base.NPC.HitEffect(9999);
					base.NPC.active = false;
					return false;
				}
			}
			else if (base.NPC.type == 423)
			{
				if (base.NPC.ai[2] == 1f)
				{
					base.NPC.ai[1]++;
					base.NPC.velocity.X = base.NPC.velocity.X * 0.7f;
					if (base.NPC.ai[1] < 30f)
					{
						Vector2 nebulaBeastDustRotation = base.NPC.Center + Vector2.UnitX * (float)base.NPC.spriteDirection * -20f;
						Dust obj = Main.dust[Dust.NewDust(nebulaBeastDustRotation, 0, 0, 242)];
						Vector2 nebulaBeastDustVelocity = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						obj.position = nebulaBeastDustRotation + nebulaBeastDustVelocity * 20f;
						obj.velocity = -nebulaBeastDustVelocity * 2f;
						obj.scale = 0.5f + nebulaBeastDustVelocity.X * (0f - (float)base.NPC.spriteDirection);
						obj.fadeIn = 1f;
						obj.noGravity = true;
					}
					else if (base.NPC.ai[1] == 30f)
					{
						for (int l = 0; l < 20; l++)
						{
							Vector2 nebulaBeastDustRotation2 = base.NPC.Center + Vector2.UnitX * (float)base.NPC.spriteDirection * -20f;
							Dust obj2 = Main.dust[Dust.NewDust(nebulaBeastDustRotation2, 0, 0, 242)];
							Vector2 nebulaBeastDustVelocity2 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
							obj2.position = nebulaBeastDustRotation2 + nebulaBeastDustVelocity2 * 4f;
							obj2.velocity = nebulaBeastDustVelocity2 * 4f + Vector2.UnitX * Main.rand.NextFloat() * (float)base.NPC.spriteDirection * -5f;
							obj2.scale = 0.5f + nebulaBeastDustVelocity2.X * (0f - (float)base.NPC.spriteDirection);
							obj2.fadeIn = 1f;
							obj2.noGravity = true;
						}
					}
					if (base.NPC.velocity.X > -0.5f && base.NPC.velocity.X < 0.5f)
					{
						base.NPC.velocity.X = 0f;
					}
					if (base.NPC.ai[1] == 30f && Main.netMode != 1)
					{
						int nebulaBeastProjDamage = (Main.expertMode ? 35 : 50);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X + (float)(base.NPC.spriteDirection * -20), base.NPC.Center.Y, base.NPC.spriteDirection * -7, 0f, 575, nebulaBeastProjDamage, 0f, Main.myPlayer, base.NPC.target);
					}
					if (base.NPC.ai[1] >= 60f)
					{
						base.NPC.ai[1] = 0f - (float)Main.rand.Next(320, CalamityWorld.death ? 361 : 601);
						base.NPC.ai[2] = 0f;
					}
				}
				else
				{
					base.NPC.ai[1]++;
					if (base.NPC.ai[1] >= 180f && targetDistance < 500f && base.NPC.velocity.Y == 0f)
					{
						flag = true;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 1f;
						base.NPC.netUpdate = true;
					}
					else if (base.NPC.velocity.Y == 0f && targetDistance < 100f && Math.Abs(base.NPC.velocity.X) > 3f && ((base.NPC.Center.X < Main.player[base.NPC.target].Center.X && base.NPC.velocity.X > 0f) || (base.NPC.Center.X > Main.player[base.NPC.target].Center.X && base.NPC.velocity.X < 0f)))
					{
						base.NPC.velocity.Y = base.NPC.velocity.Y - 6f;
					}
				}
			}
			else if (base.NPC.type == 155 || base.NPC.type == 329 || base.NPC.type == ModContent.NPCType<Rotdog>())
			{
				if (base.NPC.velocity.Y == 0f && targetDistance < 100f && Math.Abs(base.NPC.velocity.X) > 3f && ((base.NPC.position.X + (float)(base.NPC.width / 2) < Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) && base.NPC.velocity.X > 0f) || (base.NPC.position.X + (float)(base.NPC.width / 2) > Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) && base.NPC.velocity.X < 0f)))
				{
					base.NPC.velocity.Y -= 6f;
				}
			}
			else if (base.NPC.type == 546 && base.NPC.velocity.Y == 0f && Math.Abs(base.NPC.velocity.X) > 3f && ((base.NPC.Center.X < Main.player[base.NPC.target].Center.X && base.NPC.velocity.X > 0f) || (base.NPC.Center.X > Main.player[base.NPC.target].Center.X && base.NPC.velocity.X < 0f)))
			{
				base.NPC.velocity.Y -= 6f;
				SoundEngine.PlaySound(in SoundID.NPCHit11, base.NPC.Center);
			}
			if (base.NPC.ai[3] < (float)turnAroundDelay)
			{
				if ((base.NPC.type == 329 || base.NPC.type == 315) && !Main.pumpkinMoon)
				{
					if (base.NPC.timeLeft > 10)
					{
						base.NPC.timeLeft = 10;
					}
				}
				else
				{
					base.NPC.TargetClosest();
				}
			}
			else
			{
				if (base.NPC.velocity.X == 0f)
				{
					if (base.NPC.velocity.Y == 0f)
					{
						base.NPC.ai[0]++;
						if (base.NPC.ai[0] >= 2f)
						{
							base.NPC.direction *= -1;
							base.NPC.spriteDirection = base.NPC.direction;
							base.NPC.ai[0] = 0f;
						}
					}
				}
				else
				{
					base.NPC.ai[0] = 0f;
				}
				base.NPC.directionY = -1;
				if (base.NPC.direction == 0)
				{
					base.NPC.direction = 1;
				}
			}
			float maxVelocity = 9f;
			float acceleration = 0.1f;
			if (CalamityWorld.death)
			{
				maxVelocity *= 1.25f;
				acceleration *= 1.25f;
			}
			if (!flag && (base.NPC.velocity.Y == 0f || base.NPC.wet || (base.NPC.velocity.X <= 0f && base.NPC.direction < 0) || (base.NPC.velocity.X >= 0f && base.NPC.direction > 0)))
			{
				if (base.NPC.type == ModContent.NPCType<Rotdog>())
				{
					base.NPC.velocity.X *= 0.99f;
				}
				if (base.NPC.type == 155)
				{
					if (base.NPC.velocity.X > 0f && base.NPC.direction < 0)
					{
						base.NPC.velocity.X *= 0.9f;
					}
					if (base.NPC.velocity.X < 0f && base.NPC.direction > 0)
					{
						base.NPC.velocity.X *= 0.9f;
					}
				}
				else if (base.NPC.type == 329)
				{
					if (base.NPC.velocity.X > 0f && base.NPC.direction < 0)
					{
						base.NPC.velocity.X *= 0.2f;
					}
					if (base.NPC.velocity.X < 0f && base.NPC.direction > 0)
					{
						base.NPC.velocity.X *= 0.2f;
					}
					if (base.NPC.direction > 0 && base.NPC.velocity.X < 3f)
					{
						base.NPC.velocity.X += 0.15f;
					}
					if (base.NPC.direction < 0 && base.NPC.velocity.X > -3f)
					{
						base.NPC.velocity.X -= 0.15f;
					}
				}
				else if (base.NPC.type == 315)
				{
					if (base.NPC.velocity.X > 0f && base.NPC.direction < 0)
					{
						base.NPC.velocity.X *= 0.9f;
					}
					if (base.NPC.velocity.X < 0f && base.NPC.direction > 0)
					{
						base.NPC.velocity.X *= 0.9f;
					}
					if (base.NPC.velocity.X < 0f - maxVelocity || base.NPC.velocity.X > maxVelocity)
					{
						if (base.NPC.velocity.Y == 0f)
						{
							NPC nPC = base.NPC;
							nPC.velocity *= 0.8f;
						}
					}
					else if (base.NPC.velocity.X < maxVelocity && base.NPC.direction == 1)
					{
						base.NPC.velocity.X += 0.1f;
						if (base.NPC.velocity.X > maxVelocity)
						{
							base.NPC.velocity.X = maxVelocity;
						}
					}
					else if (base.NPC.velocity.X > 0f - maxVelocity && base.NPC.direction == -1)
					{
						base.NPC.velocity.X -= 0.1f;
						if (base.NPC.velocity.X < 0f - maxVelocity)
						{
							base.NPC.velocity.X = 0f - maxVelocity;
						}
					}
				}
				else if (base.NPC.type == 410)
				{
					if (Math.Sign(base.NPC.velocity.X) != base.NPC.direction)
					{
						base.NPC.velocity.X *= 0.8f;
					}
					acceleration = 0.2f;
				}
				else if (base.NPC.type == 423)
				{
					if (Math.Sign(base.NPC.velocity.X) != base.NPC.direction)
					{
						base.NPC.velocity.X *= 0.8f;
					}
					maxVelocity = 12f;
					acceleration = 0.2f;
				}
				else if (base.NPC.type == 546)
				{
					if (Math.Sign(base.NPC.velocity.X) != base.NPC.direction)
					{
						base.NPC.velocity.X *= 0.9f;
					}
					float sandstormPush = MathHelper.Lerp(0.6f, 1f, Math.Abs(Main.windSpeedCurrent)) * (float)Math.Sign(Main.windSpeedCurrent);
					if (!Main.player[base.NPC.target].ZoneSandstorm)
					{
						sandstormPush = 0f;
					}
					maxVelocity = 6f + sandstormPush * (float)base.NPC.direction * 4f;
					acceleration = 0.2f;
				}
				if (CalamityWorld.death)
				{
					maxVelocity *= 1.25f;
					acceleration *= 1.25f;
				}
				if (base.NPC.velocity.X < 0f - maxVelocity || base.NPC.velocity.X > maxVelocity)
				{
					if (base.NPC.velocity.Y == 0f)
					{
						NPC nPC2 = base.NPC;
						nPC2.velocity *= 0.8f;
					}
				}
				else if (base.NPC.velocity.X < maxVelocity && base.NPC.direction == 1)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + acceleration;
					if (base.NPC.velocity.X > maxVelocity)
					{
						base.NPC.velocity.X = maxVelocity;
					}
				}
				else if (base.NPC.velocity.X > 0f - maxVelocity && base.NPC.direction == -1)
				{
					base.NPC.velocity.X = base.NPC.velocity.X - acceleration;
					if (base.NPC.velocity.X < 0f - maxVelocity)
					{
						base.NPC.velocity.X = 0f - maxVelocity;
					}
				}
			}
			if (base.NPC.velocity.Y >= 0f)
			{
				int faceDirection = 0;
				if (base.NPC.velocity.X < 0f)
				{
					faceDirection = -1;
				}
				if (base.NPC.velocity.X > 0f)
				{
					faceDirection = 1;
				}
				Vector2 position = base.NPC.position;
				position.X += base.NPC.velocity.X;
				int x = (int)((position.X + (float)(base.NPC.width / 2) + (float)((base.NPC.width / 2 + 1) * faceDirection)) / 16f);
				int y = (int)((position.Y + (float)base.NPC.height - 1f) / 16f);
				if ((float)(x * 16) < position.X + (float)base.NPC.width && (float)(x * 16 + 16) > position.X && ((Main.tile[x, y].HasUnactuatedTile && !Main.tile[x, y].TopSlope && !Main.tile[x, y - 1].TopSlope && Main.tileSolid[Main.tile[x, y].TileType] && !Main.tileSolidTop[Main.tile[x, y].TileType]) || (Main.tile[x, y - 1].IsHalfBlock && Main.tile[x, y - 1].HasUnactuatedTile)) && (!Main.tile[x, y - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[x, y - 1].TileType] || Main.tileSolidTop[Main.tile[x, y - 1].TileType] || (Main.tile[x, y - 1].IsHalfBlock && (!Main.tile[x, y - 4].HasUnactuatedTile || !Main.tileSolid[Main.tile[x, y - 4].TileType] || Main.tileSolidTop[Main.tile[x, y - 4].TileType]))) && (!Main.tile[x, y - 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[x, y - 2].TileType] || Main.tileSolidTop[Main.tile[x, y - 2].TileType]) && (!Main.tile[x, y - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[x, y - 3].TileType] || Main.tileSolidTop[Main.tile[x, y - 3].TileType]) && (!Main.tile[x - faceDirection, y - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[x - faceDirection, y - 3].TileType]))
				{
					float tilePixelPosition = y * 16;
					if (Main.tile[x, y].IsHalfBlock)
					{
						tilePixelPosition += 8f;
					}
					if (Main.tile[x, y - 1].IsHalfBlock)
					{
						tilePixelPosition -= 8f;
					}
					if (tilePixelPosition < position.Y + (float)base.NPC.height)
					{
						float percentageTileRisen = position.Y + (float)base.NPC.height - tilePixelPosition;
						if ((double)percentageTileRisen <= 16.1)
						{
							base.NPC.gfxOffY += base.NPC.position.Y + (float)base.NPC.height - tilePixelPosition;
							base.NPC.position.Y = tilePixelPosition - (float)base.NPC.height;
							if (percentageTileRisen < 9f)
							{
								base.NPC.stepSpeed = 1f;
							}
							else
							{
								base.NPC.stepSpeed = 2f;
							}
						}
					}
				}
			}
			if (base.NPC.velocity.Y == 0f)
			{
				int npcTileX = (int)((base.NPC.position.X + (float)(base.NPC.width / 2) + (float)((base.NPC.width / 2 + 2) * base.NPC.direction) + base.NPC.velocity.X * 5f) / 16f);
				int npcTileY = (int)((base.NPC.position.Y + (float)base.NPC.height - 15f) / 16f);
				int spriteDirection = base.NPC.spriteDirection;
				if (base.NPC.type == 423 || base.NPC.type == 410 || base.NPC.type == 546)
				{
					spriteDirection *= -1;
				}
				if ((base.NPC.velocity.X < 0f && spriteDirection == -1) || (base.NPC.velocity.X > 0f && spriteDirection == 1))
				{
					bool pillarEnemy = base.NPC.type == 410 || base.NPC.type == 423;
					if (Main.tile[npcTileX, npcTileY - 2].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX, npcTileY - 2].TileType])
					{
						if (Main.tile[npcTileX, npcTileY - 3].HasUnactuatedTile && Main.tileSolid[Main.tile[npcTileX, npcTileY - 3].TileType])
						{
							base.NPC.velocity.Y = -10.5f;
							base.NPC.netUpdate = true;
						}
						else
						{
							base.NPC.velocity.Y = -9.5f;
							base.NPC.netUpdate = true;
						}
					}
					else if (Main.tile[npcTileX, npcTileY - 1].HasUnactuatedTile && !Main.tile[npcTileX, npcTileY - 1].TopSlope && Main.tileSolid[Main.tile[npcTileX, npcTileY - 1].TileType])
					{
						base.NPC.velocity.Y = -9f;
						base.NPC.netUpdate = true;
					}
					else if (base.NPC.position.Y + (float)base.NPC.height - (float)(npcTileY * 16) > 20f && Main.tile[npcTileX, npcTileY].HasUnactuatedTile && !Main.tile[npcTileX, npcTileY].TopSlope && Main.tileSolid[Main.tile[npcTileX, npcTileY].TileType])
					{
						base.NPC.velocity.Y = -7f;
						base.NPC.netUpdate = true;
					}
					else if ((base.NPC.directionY < 0 || Math.Abs(base.NPC.velocity.X) > 3f) && (!pillarEnemy || !Main.tile[npcTileX, npcTileY + 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[npcTileX, npcTileY + 1].TileType]) && (!Main.tile[npcTileX, npcTileY + 2].HasUnactuatedTile || !Main.tileSolid[Main.tile[npcTileX, npcTileY + 2].TileType]) && (!Main.tile[npcTileX + base.NPC.direction, npcTileY + 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[npcTileX + base.NPC.direction, npcTileY + 3].TileType]))
					{
						base.NPC.velocity.Y = -10f;
						base.NPC.netUpdate = true;
					}
				}
			}
			if (base.NPC.type == 423 && Math.Abs(base.NPC.velocity.X) >= maxVelocity * 0.95f)
			{
				Rectangle hitbox = base.NPC.Hitbox;
				for (int m = 0; m < 2; m++)
				{
					if (Main.rand.NextBool(3))
					{
						Dust obj3 = Main.dust[Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, 242)];
						obj3.velocity = Vector2.Zero;
						obj3.noGravity = true;
						obj3.fadeIn = 1f;
						obj3.scale = 0.5f + Main.rand.NextFloat();
					}
				}
			}
			if (base.NPC.type == 546)
			{
				base.NPC.rotation += base.NPC.velocity.X * 0.05f;
				base.NPC.spriteDirection = -base.NPC.direction;
			}
			return false;
		}
	}

	public class WormAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Unknown result type (might be due to invalid IL or missing references)
			//IL_012f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0134: Unknown result type (might be due to invalid IL or missing references)
			//IL_0139: Unknown result type (might be due to invalid IL or missing references)
			//IL_2298: Unknown result type (might be due to invalid IL or missing references)
			//IL_229d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00da: Unknown result type (might be due to invalid IL or missing references)
			//IL_0290: Unknown result type (might be due to invalid IL or missing references)
			//IL_029b: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_024c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0276: Unknown result type (might be due to invalid IL or missing references)
			//IL_027c: Unknown result type (might be due to invalid IL or missing references)
			//IL_18c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_18d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_18e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_18e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_18ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_18fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_1904: Unknown result type (might be due to invalid IL or missing references)
			//IL_1909: Unknown result type (might be due to invalid IL or missing references)
			//IL_1910: Unknown result type (might be due to invalid IL or missing references)
			//IL_1912: Unknown result type (might be due to invalid IL or missing references)
			//IL_1914: Unknown result type (might be due to invalid IL or missing references)
			//IL_193e: Unknown result type (might be due to invalid IL or missing references)
			//IL_196f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1979: Unknown result type (might be due to invalid IL or missing references)
			//IL_197e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1983: Unknown result type (might be due to invalid IL or missing references)
			//IL_1993: Unknown result type (might be due to invalid IL or missing references)
			//IL_1998: Unknown result type (might be due to invalid IL or missing references)
			//IL_19a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_19a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_19a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_19d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a01: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a0b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a10: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a15: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a25: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_16fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1537: Unknown result type (might be due to invalid IL or missing references)
			//IL_1553: Unknown result type (might be due to invalid IL or missing references)
			//IL_1582: Unknown result type (might be due to invalid IL or missing references)
			//IL_159b: Unknown result type (might be due to invalid IL or missing references)
			//IL_205e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2077: Unknown result type (might be due to invalid IL or missing references)
			//IL_2090: Unknown result type (might be due to invalid IL or missing references)
			//IL_209c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ce7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d0b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2131: Unknown result type (might be due to invalid IL or missing references)
			//IL_2136: Unknown result type (might be due to invalid IL or missing references)
			//IL_2173: Unknown result type (might be due to invalid IL or missing references)
			//IL_21b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1da5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dc0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e92: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e99: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e9e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e24: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dff: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ec2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ec9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ece: Unknown result type (might be due to invalid IL or missing references)
			//IL_2628: Unknown result type (might be due to invalid IL or missing references)
			//IL_2633: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fda: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fe5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fea: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fef: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f26: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f31: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f36: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f3b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2006: Unknown result type (might be due to invalid IL or missing references)
			//IL_200d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2012: Unknown result type (might be due to invalid IL or missing references)
			//IL_2016: Unknown result type (might be due to invalid IL or missing references)
			//IL_2022: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f52: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f59: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f5e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f62: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f6e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a74: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a79: Unknown result type (might be due to invalid IL or missing references)
			//IL_2cc7: Unknown result type (might be due to invalid IL or missing references)
			//IL_2cd1: Unknown result type (might be due to invalid IL or missing references)
			//IL_2cd6: Unknown result type (might be due to invalid IL or missing references)
			if (base.NPC.type == 117 && base.NPC.localAI[1] == 0f)
			{
				base.NPC.localAI[1] = 1f;
				SoundEngine.PlaySound(in SoundID.NPCDeath13, base.NPC.Center);
				int dustVelocity = 1;
				if (base.NPC.velocity.X < 0f)
				{
					dustVelocity = -1;
				}
				for (int i = 0; i < 20; i++)
				{
					Dust.NewDust(new Vector2(base.NPC.position.X - 20f, base.NPC.position.Y - 20f), base.NPC.width + 40, base.NPC.height + 40, 5, dustVelocity * 8, -1f);
				}
			}
			if (base.NPC.type >= 621 && base.NPC.type <= 623)
			{
				NPC nPC = base.NPC;
				nPC.position += base.NPC.netOffset;
				base.NPC.dontTakeDamage = base.NPC.alpha > 0;
				if (base.NPC.type == 621 || (base.NPC.type != 621 && Main.npc[(int)base.NPC.ai[1]].alpha < 85))
				{
					if (base.NPC.dontTakeDamage)
					{
						for (int k = 0; k < 2; k++)
						{
							Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100);
						}
					}
					base.NPC.alpha -= 42;
					if (base.NPC.alpha < 0)
					{
						base.NPC.alpha = 0;
					}
				}
				if (base.NPC.alpha == 0 && Main.rand.NextBool(5))
				{
					Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100);
				}
				NPC nPC2 = base.NPC;
				nPC2.position -= base.NPC.netOffset;
			}
			else if (base.NPC.type == 402 && base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[1] = Main.rand.Next(-2, 0);
				base.NPC.netUpdate = true;
			}
			bool wormHead = base.NPC.type == 10 || base.NPC.type == 39 || base.NPC.type == 95 || base.NPC.type == 117 || base.NPC.type == 510 || (!Main.player[base.NPC.target].ZoneUndergroundDesert && base.NPC.type == 513);
			float acceleration = ((base.NPC.type == 513) ? 0.1f : 0.2f);
			base.NPC.defense = (int)Math.Round((double)base.NPC.defDefense * 1.3);
			if (base.NPC.ai[3] > 0f)
			{
				base.NPC.realLife = (int)base.NPC.ai[3];
			}
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || (wormHead && (double)Main.player[base.NPC.target].position.Y < Main.worldSurface * 16.0))
			{
				base.NPC.TargetClosest();
			}
			if (Main.player[base.NPC.target].dead || (wormHead && (double)Main.player[base.NPC.target].position.Y < Main.worldSurface * 16.0))
			{
				if (base.NPC.timeLeft > 300)
				{
					base.NPC.timeLeft = 300;
				}
				if (wormHead)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + acceleration;
				}
			}
			if (base.NPC.type == 621 && Main.dayTime)
			{
				base.NPC.EncourageDespawn(60);
				base.NPC.velocity.Y++;
			}
			if (Main.netMode != 1)
			{
				if (base.NPC.type == 87 && base.NPC.ai[0] == 0f)
				{
					int maxParts = (CalamityWorld.death ? 30 : 21);
					base.NPC.ai[3] = base.NPC.whoAmI;
					base.NPC.realLife = base.NPC.whoAmI;
					int currentNPC = base.NPC.whoAmI;
					for (int j = 0; j < maxParts; j++)
					{
						int wyvernSegmentType = 89;
						if (j == 1 || j == 12)
						{
							wyvernSegmentType = 88;
						}
						else if (j == maxParts - 3)
						{
							wyvernSegmentType = 90;
						}
						else if (j == maxParts - 2)
						{
							wyvernSegmentType = 91;
						}
						else if (j == maxParts - 1)
						{
							wyvernSegmentType = 92;
						}
						int wyvernSegment = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.position.X + (float)(base.NPC.width / 2)), (int)(base.NPC.position.Y + (float)base.NPC.height), wyvernSegmentType, base.NPC.whoAmI);
						Main.npc[wyvernSegment].ai[3] = base.NPC.whoAmI;
						Main.npc[wyvernSegment].realLife = base.NPC.whoAmI;
						Main.npc[wyvernSegment].ai[1] = currentNPC;
						Main.npc[currentNPC].ai[0] = wyvernSegment;
						NetMessage.SendData(23, -1, -1, null, wyvernSegment);
						currentNPC = wyvernSegment;
					}
				}
				if (base.NPC.type == 513 && base.NPC.ai[0] == 0f)
				{
					base.NPC.ai[3] = base.NPC.whoAmI;
					base.NPC.realLife = base.NPC.whoAmI;
					int currentTombCrawler = base.NPC.whoAmI;
					int tombCrawlerSegments = Main.rand.Next(11, CalamityWorld.death ? 25 : 15);
					for (int m = 0; m < tombCrawlerSegments; m++)
					{
						int tombCrawlerSegmentType = 514;
						if (m == tombCrawlerSegments - 1)
						{
							tombCrawlerSegmentType = 515;
						}
						int tombCrawlerSegment = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.position.X + (float)(base.NPC.width / 2)), (int)(base.NPC.position.Y + (float)base.NPC.height), tombCrawlerSegmentType, base.NPC.whoAmI);
						Main.npc[tombCrawlerSegment].ai[3] = base.NPC.whoAmI;
						Main.npc[tombCrawlerSegment].realLife = base.NPC.whoAmI;
						Main.npc[tombCrawlerSegment].ai[1] = currentTombCrawler;
						Main.npc[currentTombCrawler].ai[0] = tombCrawlerSegment;
						NetMessage.SendData(23, -1, -1, null, tombCrawlerSegment);
						currentTombCrawler = tombCrawlerSegment;
					}
				}
				if (base.NPC.type == 412 && base.NPC.ai[0] == 0f)
				{
					base.NPC.ai[3] = base.NPC.whoAmI;
					base.NPC.realLife = base.NPC.whoAmI;
					int projTargetDistance = base.NPC.whoAmI;
					int crawltipedeSegments = (CalamityWorld.death ? 70 : 50);
					for (int n = 0; n < crawltipedeSegments; n++)
					{
						int crawltipedeSegmentType = 413;
						if (n == crawltipedeSegments - 1)
						{
							crawltipedeSegmentType = 414;
						}
						int crawltipedeSegment = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.position.X + (float)(base.NPC.width / 2)), (int)(base.NPC.position.Y + (float)base.NPC.height), crawltipedeSegmentType, base.NPC.whoAmI);
						Main.npc[crawltipedeSegment].ai[3] = base.NPC.whoAmI;
						Main.npc[crawltipedeSegment].realLife = base.NPC.whoAmI;
						Main.npc[crawltipedeSegment].ai[1] = projTargetDistance;
						Main.npc[projTargetDistance].ai[0] = crawltipedeSegment;
						NetMessage.SendData(23, -1, -1, null, crawltipedeSegment);
						projTargetDistance = crawltipedeSegment;
					}
				}
				if (base.NPC.type == 621 && base.NPC.ai[0] == 0f)
				{
					base.NPC.ai[3] = base.NPC.whoAmI;
					base.NPC.realLife = base.NPC.whoAmI;
					int bloodEelSegment = 0;
					int currentBloodEel = base.NPC.whoAmI;
					int bloodEelSegments = (CalamityWorld.death ? 44 : 34);
					for (int p = 0; p < bloodEelSegments; p++)
					{
						int bloodEelSegmentType = 622;
						if (p == bloodEelSegments - 1)
						{
							bloodEelSegmentType = 623;
						}
						bloodEelSegment = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.position.X + (float)(base.NPC.width / 2)), (int)(base.NPC.position.Y + (float)base.NPC.height), bloodEelSegmentType, base.NPC.whoAmI);
						Main.npc[bloodEelSegment].ai[3] = base.NPC.whoAmI;
						Main.npc[bloodEelSegment].realLife = base.NPC.whoAmI;
						Main.npc[bloodEelSegment].ai[1] = currentBloodEel;
						Main.npc[bloodEelSegment].CopyInteractions(base.NPC);
						Main.npc[currentBloodEel].ai[0] = bloodEelSegment;
						NetMessage.SendData(23, -1, -1, null, bloodEelSegment);
						currentBloodEel = bloodEelSegment;
					}
				}
				else if ((base.NPC.type == 10 || base.NPC.type == 11 || base.NPC.type == 7 || base.NPC.type == 8 || base.NPC.type == 39 || base.NPC.type == 40 || base.NPC.type == 117 || base.NPC.type == 118) && base.NPC.ai[0] == 0f)
				{
					if (base.NPC.type == 10 || base.NPC.type == 7 || base.NPC.type == 39 || base.NPC.type == 117)
					{
						base.NPC.ai[3] = base.NPC.whoAmI;
						base.NPC.realLife = base.NPC.whoAmI;
						switch (base.NPC.type)
						{
						case 7:
							base.NPC.ai[2] = Main.rand.Next(13, CalamityWorld.death ? 30 : 19);
							break;
						case 10:
							base.NPC.ai[2] = Main.rand.Next(25, CalamityWorld.death ? 50 : 31);
							break;
						case 39:
							base.NPC.ai[2] = Main.rand.Next(16, CalamityWorld.death ? 33 : 23);
							break;
						case 95:
							base.NPC.ai[2] = Main.rand.Next(12, CalamityWorld.death ? 27 : 18);
							break;
						case 98:
							base.NPC.ai[2] = Main.rand.Next(27, CalamityWorld.death ? 45 : 33);
							break;
						case 117:
							base.NPC.ai[2] = Main.rand.Next(CalamityWorld.death ? 3 : 5, CalamityWorld.death ? 5 : 8);
							break;
						case 510:
							base.NPC.ai[2] = Main.rand.Next(15, CalamityWorld.death ? 35 : 24);
							break;
						}
						base.NPC.ai[0] = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.position.X + (float)(base.NPC.width / 2)), (int)(base.NPC.position.Y + (float)base.NPC.height), base.NPC.type + 1, base.NPC.whoAmI);
					}
					else if ((base.NPC.type == 11 || base.NPC.type == 8 || base.NPC.type == 40 || base.NPC.type == 118) && base.NPC.ai[2] > 0f)
					{
						base.NPC.ai[0] = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.position.X + (float)(base.NPC.width / 2)), (int)(base.NPC.position.Y + (float)base.NPC.height), base.NPC.type, base.NPC.whoAmI);
					}
					else
					{
						base.NPC.ai[0] = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.position.X + (float)(base.NPC.width / 2)), (int)(base.NPC.position.Y + (float)base.NPC.height), base.NPC.type + 1, base.NPC.whoAmI);
					}
					Main.npc[(int)base.NPC.ai[0]].ai[3] = base.NPC.ai[3];
					Main.npc[(int)base.NPC.ai[0]].realLife = base.NPC.realLife;
					Main.npc[(int)base.NPC.ai[0]].ai[1] = base.NPC.whoAmI;
					Main.npc[(int)base.NPC.ai[0]].ai[2] = base.NPC.ai[2] - 1f;
					base.NPC.netUpdate = true;
				}
				if (base.NPC.ai[1] > 0f && base.NPC.ai[1] < (float)Main.npc.Length && (!Main.npc[(int)base.NPC.ai[1]].active || Main.npc[(int)base.NPC.ai[1]].aiStyle != base.NPC.aiStyle))
				{
					base.NPC.life = 0;
					base.NPC.HitEffect();
					base.NPC.active = false;
					NetMessage.SendData(28, -1, -1, null, base.NPC.whoAmI, -1f);
				}
				if (base.NPC.ai[0] > 0f && base.NPC.ai[0] < (float)Main.npc.Length && (!Main.npc[(int)base.NPC.ai[0]].active || Main.npc[(int)base.NPC.ai[0]].aiStyle != base.NPC.aiStyle))
				{
					base.NPC.life = 0;
					base.NPC.HitEffect();
					base.NPC.active = false;
					NetMessage.SendData(28, -1, -1, null, base.NPC.whoAmI, -1f);
				}
				if (!base.NPC.active && Main.dedServ)
				{
					NetMessage.SendData(28, -1, -1, null, base.NPC.whoAmI, -1f);
				}
			}
			int tilePositionX = (int)(base.NPC.position.X / 16f) - 1;
			int tileWidthPosX = (int)((base.NPC.position.X + (float)base.NPC.width) / 16f) + 2;
			int tilePositionY = (int)(base.NPC.position.Y / 16f) - 1;
			int tileWidthPosY = (int)((base.NPC.position.Y + (float)base.NPC.height) / 16f) + 2;
			if (tilePositionX < 0)
			{
				tilePositionX = 0;
			}
			if (tileWidthPosX > Main.maxTilesX)
			{
				tileWidthPosX = Main.maxTilesX;
			}
			if (tilePositionY < 0)
			{
				tilePositionY = 0;
			}
			if (tileWidthPosY > Main.maxTilesY)
			{
				tileWidthPosY = Main.maxTilesY;
			}
			bool flying = false;
			if (base.NPC.type >= 87 && base.NPC.type <= 92)
			{
				flying = true;
			}
			if (base.NPC.type == 402 && base.NPC.ai[1] == -1f)
			{
				flying = true;
			}
			if (base.NPC.type >= 412 && base.NPC.type <= 414)
			{
				flying = true;
			}
			if (base.NPC.type >= 621 && base.NPC.type <= 623)
			{
				flying = true;
			}
			if (!flying)
			{
				Vector2 flyingPos = default(Vector2);
				for (int x = tilePositionX; x < tileWidthPosX; x++)
				{
					for (int y = tilePositionY; y < tileWidthPosY; y++)
					{
						if (!(Main.tile[x, y] != null) || ((!Main.tile[x, y].HasUnactuatedTile || (!Main.tileSolid[Main.tile[x, y].TileType] && (!Main.tileSolidTop[Main.tile[x, y].TileType] || Main.tile[x, y].TileFrameY != 0))) && Main.tile[x, y].LiquidAmount <= 64))
						{
							continue;
						}
						flyingPos.X = x * 16;
						flyingPos.Y = y * 16;
						if (base.NPC.position.X + (float)base.NPC.width > flyingPos.X && base.NPC.position.X < flyingPos.X + 16f && base.NPC.position.Y + (float)base.NPC.height > flyingPos.Y && base.NPC.position.Y < flyingPos.Y + 16f)
						{
							flying = true;
							if (Main.rand.NextBool(100) && base.NPC.type != 117 && Main.tile[x, y].HasUnactuatedTile)
							{
								WorldGen.KillTile(x, y, fail: true, effectOnly: true);
							}
						}
					}
				}
			}
			if (!flying && (base.NPC.type == 10 || base.NPC.type == 7 || base.NPC.type == 39 || base.NPC.type == 117 || base.NPC.type == 513))
			{
				Rectangle rectangle = default(Rectangle);
				((Rectangle)(ref rectangle))._002Ector((int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height);
				int noFlyZone = 1000;
				bool outsideNoFlyZone = true;
				Rectangle rectangle2 = default(Rectangle);
				for (int f = 0; f < 255; f++)
				{
					if (Main.player[f].active)
					{
						((Rectangle)(ref rectangle2))._002Ector((int)Main.player[f].position.X - noFlyZone, (int)Main.player[f].position.Y - noFlyZone, noFlyZone * 2, noFlyZone * 2);
						if (((Rectangle)(ref rectangle)).Intersects(rectangle2))
						{
							outsideNoFlyZone = false;
							break;
						}
					}
				}
				if (outsideNoFlyZone)
				{
					flying = true;
				}
			}
			if ((base.NPC.type >= 87 && base.NPC.type <= 92) || (base.NPC.type >= 621 && base.NPC.type <= 623))
			{
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.spriteDirection = 1;
				}
				else if (base.NPC.velocity.X > 0f)
				{
					base.NPC.spriteDirection = -1;
				}
			}
			if (base.NPC.type == 414)
			{
				if (base.NPC.justHit)
				{
					base.NPC.localAI[3] = 3f;
				}
				if (base.NPC.localAI[2] > 0f)
				{
					base.NPC.localAI[2] -= 16f;
					if (base.NPC.localAI[2] == 0f)
					{
						base.NPC.localAI[2] = -128f;
					}
				}
				else if (base.NPC.localAI[2] < 0f)
				{
					base.NPC.localAI[2] += 16f;
				}
				else if (base.NPC.localAI[3] > 0f)
				{
					base.NPC.localAI[2] = 128f;
					base.NPC.localAI[3]--;
				}
			}
			if (base.NPC.type == 412)
			{
				Vector2 crawltipedeDustPos = base.NPC.Center + (base.NPC.rotation - (float)Math.PI / 2f).ToRotationVector2() * 8f;
				Vector2 crawltipedeDustRotation = base.NPC.rotation.ToRotationVector2() * 16f;
				Dust obj = Main.dust[Dust.NewDust(crawltipedeDustPos + crawltipedeDustRotation, 0, 0, 6, base.NPC.velocity.X, base.NPC.velocity.Y, 100, Color.Transparent, 1f + Main.rand.NextFloat() * 3f)];
				obj.noGravity = true;
				obj.noLight = true;
				obj.position -= new Vector2(4f);
				obj.fadeIn = 1f;
				obj.velocity = Vector2.Zero;
				Dust obj2 = Main.dust[Dust.NewDust(crawltipedeDustPos - crawltipedeDustRotation, 0, 0, 6, base.NPC.velocity.X, base.NPC.velocity.Y, 100, Color.Transparent, 1f + Main.rand.NextFloat() * 3f)];
				obj2.noGravity = true;
				obj2.noLight = true;
				obj2.position -= new Vector2(4f);
				obj2.fadeIn = 1f;
				obj2.velocity = Vector2.Zero;
			}
			float wormSpeed = 10f;
			float wormAccel = 0.09f;
			if (base.NPC.type == 95)
			{
				wormSpeed = 6.5f;
				wormAccel = 0.05f;
			}
			if (base.NPC.type == 10)
			{
				wormSpeed = 7.5f;
				wormAccel = 0.06f;
			}
			if (base.NPC.type == 513)
			{
				wormSpeed = 8f;
				wormAccel = 0.13f;
			}
			if (base.NPC.type == 510)
			{
				if (!Main.player[base.NPC.target].dead && Main.player[base.NPC.target].ZoneSandstorm)
				{
					wormSpeed = 16f;
					wormAccel = 0.35f;
				}
				else
				{
					wormAccel = 0.25f;
				}
			}
			if (base.NPC.type == 87)
			{
				wormSpeed = 11f;
				wormAccel = 0.3f;
			}
			if (base.NPC.type == 402)
			{
				wormSpeed = 9f;
				wormAccel = 0.25f;
			}
			if (base.NPC.type == 117 && Main.wofNPCIndex >= 0)
			{
				float num = (float)Main.npc[Main.wofNPCIndex].life / (float)Main.npc[Main.wofNPCIndex].lifeMax;
				if (num < 0.75f)
				{
					wormSpeed++;
					wormAccel += 0.1f;
				}
				if (num < 0.5f)
				{
					wormSpeed++;
					wormAccel += 0.1f;
				}
				if (num < 0.25f)
				{
					wormSpeed += 2f;
					wormAccel += 0.1f;
				}
			}
			if (base.NPC.type == 621)
			{
				wormSpeed = 18f;
				wormAccel = 0.6f;
			}
			if (CalamityWorld.death)
			{
				wormSpeed *= 1.25f;
				wormAccel *= 1.25f;
			}
			Vector2 segmentPosition = default(Vector2);
			((Vector2)(ref segmentPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
			float wormTargetX = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2);
			float wormTargetY = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2);
			if (base.NPC.type == 412)
			{
				wormSpeed = 12f;
				wormAccel = 0.32f;
				int crawltipedeTargetY = -1;
				int targetTileX = (int)(Main.player[base.NPC.target].Center.X / 16f);
				int targetTileY = (int)(Main.player[base.NPC.target].Center.Y / 16f);
				for (int l = targetTileX - 2; l <= targetTileX + 2; l++)
				{
					for (int num2 = targetTileY; num2 <= targetTileY + 15; num2++)
					{
						if (WorldGen.SolidTile2(l, num2))
						{
							crawltipedeTargetY = num2;
							break;
						}
					}
					if (crawltipedeTargetY > 0)
					{
						break;
					}
				}
				if (crawltipedeTargetY > 0)
				{
					crawltipedeTargetY *= 16;
					float crawltipedeYTarget = crawltipedeTargetY - 800;
					if (Main.player[base.NPC.target].position.Y > crawltipedeYTarget)
					{
						wormTargetY = crawltipedeYTarget;
						if (Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) < 500f)
						{
							wormTargetX = ((!(base.NPC.velocity.X > 0f)) ? (Main.player[base.NPC.target].Center.X - 600f) : (Main.player[base.NPC.target].Center.X + 600f));
						}
					}
				}
				else
				{
					wormSpeed = 28f;
					wormAccel = 0.8f;
				}
				float maxWormSpeed = wormSpeed * 1.3f;
				float minWormSpeed = wormSpeed * 0.7f;
				float velocityCheck = ((Vector2)(ref base.NPC.velocity)).Length();
				if (velocityCheck > 0f)
				{
					if (velocityCheck > maxWormSpeed)
					{
						((Vector2)(ref base.NPC.velocity)).Normalize();
						NPC nPC3 = base.NPC;
						nPC3.velocity *= maxWormSpeed;
					}
					else if (velocityCheck < minWormSpeed)
					{
						((Vector2)(ref base.NPC.velocity)).Normalize();
						NPC nPC4 = base.NPC;
						nPC4.velocity *= minWormSpeed;
					}
				}
				if (crawltipedeTargetY > 0)
				{
					for (int num3 = 0; num3 < Main.maxNPCs; num3++)
					{
						if (Main.npc[num3].active && Main.npc[num3].type == base.NPC.type && num3 != base.NPC.whoAmI)
						{
							Vector2 targetDirection = Main.npc[num3].Center - base.NPC.Center;
							if (((Vector2)(ref targetDirection)).Length() < 400f)
							{
								((Vector2)(ref targetDirection)).Normalize();
								targetDirection *= 1000f;
								wormTargetX -= targetDirection.X;
								wormTargetY -= targetDirection.Y;
							}
						}
					}
				}
				else
				{
					for (int num4 = 0; num4 < Main.maxNPCs; num4++)
					{
						if (Main.npc[num4].active && Main.npc[num4].type == base.NPC.type && num4 != base.NPC.whoAmI)
						{
							Vector2 idleDirection = Main.npc[num4].Center - base.NPC.Center;
							if (((Vector2)(ref idleDirection)).Length() < 60f)
							{
								((Vector2)(ref idleDirection)).Normalize();
								idleDirection *= 200f;
								wormTargetX -= idleDirection.X;
								wormTargetY -= idleDirection.Y;
							}
						}
					}
				}
			}
			wormTargetX = (int)(wormTargetX / 16f) * 16;
			wormTargetY = (int)(wormTargetY / 16f) * 16;
			segmentPosition.X = (int)(segmentPosition.X / 16f) * 16;
			segmentPosition.Y = (int)(segmentPosition.Y / 16f) * 16;
			wormTargetX -= segmentPosition.X;
			wormTargetY -= segmentPosition.Y;
			float wormTargetDist = (float)Math.Sqrt(wormTargetX * wormTargetX + wormTargetY * wormTargetY);
			if (base.NPC.ai[1] > 0f && base.NPC.ai[1] < (float)Main.npc.Length)
			{
				try
				{
					segmentPosition = new Vector2(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
					wormTargetX = Main.npc[(int)base.NPC.ai[1]].position.X + (float)(Main.npc[(int)base.NPC.ai[1]].width / 2) - segmentPosition.X;
					wormTargetY = Main.npc[(int)base.NPC.ai[1]].position.Y + (float)(Main.npc[(int)base.NPC.ai[1]].height / 2) - segmentPosition.Y;
				}
				catch
				{
				}
				base.NPC.rotation = (float)Math.Atan2(wormTargetY, wormTargetX) + (float)Math.PI / 2f;
				wormTargetDist = (float)Math.Sqrt(wormTargetX * wormTargetX + wormTargetY * wormTargetY);
				int segmentWidth = base.NPC.width;
				if (base.NPC.type >= 87 && base.NPC.type <= 92)
				{
					segmentWidth = 42;
				}
				if (base.NPC.type >= 412 && base.NPC.type <= 414)
				{
					segmentWidth += 6;
				}
				if (base.NPC.type >= 621 && base.NPC.type <= 623)
				{
					segmentWidth = 24;
				}
				wormTargetDist = (wormTargetDist - (float)segmentWidth) / wormTargetDist;
				wormTargetX *= wormTargetDist;
				wormTargetY *= wormTargetDist;
				base.NPC.velocity = Vector2.Zero;
				base.NPC.position.X = base.NPC.position.X + wormTargetX;
				base.NPC.position.Y = base.NPC.position.Y + wormTargetY;
				if ((base.NPC.type >= 87 && base.NPC.type <= 92) || (base.NPC.type >= 621 && base.NPC.type <= 623))
				{
					if (wormTargetX < 0f)
					{
						base.NPC.spriteDirection = 1;
					}
					else if (wormTargetX > 0f)
					{
						base.NPC.spriteDirection = -1;
					}
				}
			}
			else
			{
				if (!flying)
				{
					base.NPC.TargetClosest();
					base.NPC.velocity.Y = base.NPC.velocity.Y + 0.11f;
					if (base.NPC.velocity.Y > wormSpeed)
					{
						base.NPC.velocity.Y = wormSpeed;
					}
					if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)wormSpeed * 0.4)
					{
						if (base.NPC.velocity.X < 0f)
						{
							base.NPC.velocity.X = base.NPC.velocity.X - wormAccel * 1.1f;
						}
						else
						{
							base.NPC.velocity.X = base.NPC.velocity.X + wormAccel * 1.1f;
						}
					}
					else if (base.NPC.velocity.Y == wormSpeed)
					{
						if (base.NPC.velocity.X < wormTargetX)
						{
							base.NPC.velocity.X = base.NPC.velocity.X + wormAccel;
						}
						else if (base.NPC.velocity.X > wormTargetX)
						{
							base.NPC.velocity.X = base.NPC.velocity.X - wormAccel;
						}
					}
					else if (base.NPC.velocity.Y > 4f)
					{
						if (base.NPC.velocity.X < 0f)
						{
							base.NPC.velocity.X = base.NPC.velocity.X + wormAccel * 0.9f;
						}
						else
						{
							base.NPC.velocity.X = base.NPC.velocity.X - wormAccel * 0.9f;
						}
					}
				}
				else
				{
					if (base.NPC.type != 87 && base.NPC.type != 117 && base.NPC.type != 412 && base.NPC.type != 621 && base.NPC.soundDelay == 0)
					{
						float soundDelay = wormTargetDist / 40f;
						if (soundDelay < 10f)
						{
							soundDelay = 10f;
						}
						if (soundDelay > 20f)
						{
							soundDelay = 20f;
						}
						base.NPC.soundDelay = (int)soundDelay;
						SoundEngine.PlaySound(in SoundID.WormDig, base.NPC.Center);
					}
					wormTargetDist = (float)Math.Sqrt(wormTargetX * wormTargetX + wormTargetY * wormTargetY);
					float absoluteTargetX = Math.Abs(wormTargetX);
					float absoluteTargetY = Math.Abs(wormTargetY);
					float timeToReachTarget = wormSpeed / wormTargetDist;
					wormTargetX *= timeToReachTarget;
					wormTargetY *= timeToReachTarget;
					bool wormShouldFlee = false;
					if (base.NPC.type == 7 && ((!Main.player[base.NPC.target].ZoneCorrupt && !Main.player[base.NPC.target].ZoneCrimson) || Main.player[base.NPC.target].dead))
					{
						wormShouldFlee = true;
					}
					if ((base.NPC.type == 513 && (double)Main.player[base.NPC.target].position.Y < Main.worldSurface * 16.0 && !Main.player[base.NPC.target].ZoneSandstorm && !Main.player[base.NPC.target].ZoneUndergroundDesert) || Main.player[base.NPC.target].dead)
					{
						wormShouldFlee = true;
					}
					if ((base.NPC.type == 510 && (double)Main.player[base.NPC.target].position.Y < Main.worldSurface * 16.0 && !Main.player[base.NPC.target].ZoneSandstorm && !Main.player[base.NPC.target].ZoneUndergroundDesert) || Main.player[base.NPC.target].dead)
					{
						wormShouldFlee = true;
					}
					if (wormShouldFlee)
					{
						bool definitelyFlee = true;
						for (int num5 = 0; num5 < 255; num5++)
						{
							if (Main.player[num5].active && !Main.player[num5].dead && Main.player[num5].ZoneCorrupt)
							{
								definitelyFlee = false;
							}
						}
						if (definitelyFlee)
						{
							if (Main.netMode != 1 && (double)(base.NPC.position.Y / 16f) > (Main.rockLayer + (double)Main.maxTilesY) / 2.0)
							{
								base.NPC.active = false;
								int q = (int)base.NPC.ai[0];
								while (q > 0 && q < Main.maxNPCs && Main.npc[q].active && Main.npc[q].aiStyle == base.NPC.aiStyle)
								{
									int num6 = (int)Main.npc[q].ai[0];
									Main.npc[q].active = false;
									base.NPC.life = 0;
									if (Main.dedServ)
									{
										NetMessage.SendData(23, -1, -1, null, q);
									}
									q = num6;
								}
								if (Main.dedServ)
								{
									NetMessage.SendData(23, -1, -1, null, base.NPC.whoAmI);
								}
							}
							wormTargetX = 0f;
							wormTargetY = wormSpeed;
						}
					}
					bool shouldSwoopDown = false;
					if (base.NPC.type == 87)
					{
						if (((base.NPC.velocity.X > 0f && wormTargetX < 0f) || (base.NPC.velocity.X < 0f && wormTargetX > 0f) || (base.NPC.velocity.Y > 0f && wormTargetY < 0f) || (base.NPC.velocity.Y < 0f && wormTargetY > 0f)) && Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y) > wormAccel / 2f && wormTargetDist < 300f)
						{
							shouldSwoopDown = true;
							if (Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y) < wormSpeed)
							{
								NPC nPC5 = base.NPC;
								nPC5.velocity *= 1.1f;
							}
						}
						if (base.NPC.position.Y > Main.player[base.NPC.target].position.Y || (double)(Main.player[base.NPC.target].position.Y / 16f) > Main.worldSurface || Main.player[base.NPC.target].dead)
						{
							shouldSwoopDown = true;
							if (Math.Abs(base.NPC.velocity.X) < wormSpeed / 2f)
							{
								if (base.NPC.velocity.X == 0f)
								{
									base.NPC.velocity.X = base.NPC.velocity.X - (float)base.NPC.direction;
								}
								base.NPC.velocity.X = base.NPC.velocity.X * 1.1f;
							}
							else if (base.NPC.velocity.Y > 0f - wormSpeed)
							{
								base.NPC.velocity.Y = base.NPC.velocity.Y - wormAccel;
							}
						}
					}
					if (base.NPC.type == 621)
					{
						if (((base.NPC.velocity.X > 0f && wormTargetX < 0f) || (base.NPC.velocity.X < 0f && wormTargetX > 0f) || (base.NPC.velocity.Y > 0f && wormTargetY < 0f) || (base.NPC.velocity.Y < 0f && wormTargetY > 0f)) && Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y) > wormAccel / 2f && wormTargetDist < 120f)
						{
							shouldSwoopDown = true;
							if (Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y) < wormSpeed)
							{
								NPC nPC6 = base.NPC;
								nPC6.velocity *= 1.1f;
							}
						}
						if (base.NPC.position.Y > Main.player[base.NPC.target].position.Y || Main.player[base.NPC.target].dead)
						{
							shouldSwoopDown = true;
							if (Math.Abs(base.NPC.velocity.X) < wormSpeed / 2f)
							{
								if (base.NPC.velocity.X == 0f)
								{
									base.NPC.velocity.X -= base.NPC.direction;
								}
								base.NPC.velocity.X *= 1.1f;
							}
							else if (base.NPC.velocity.Y > 0f - wormSpeed)
							{
								base.NPC.velocity.Y -= wormAccel;
							}
						}
					}
					if (!shouldSwoopDown)
					{
						if ((base.NPC.velocity.X > 0f && wormTargetX > 0f) || (base.NPC.velocity.X < 0f && wormTargetX < 0f) || (base.NPC.velocity.Y > 0f && wormTargetY > 0f) || (base.NPC.velocity.Y < 0f && wormTargetY < 0f))
						{
							if (base.NPC.velocity.X < wormTargetX)
							{
								base.NPC.velocity.X = base.NPC.velocity.X + wormAccel;
							}
							else if (base.NPC.velocity.X > wormTargetX)
							{
								base.NPC.velocity.X = base.NPC.velocity.X - wormAccel;
							}
							if (base.NPC.velocity.Y < wormTargetY)
							{
								base.NPC.velocity.Y = base.NPC.velocity.Y + wormAccel;
							}
							else if (base.NPC.velocity.Y > wormTargetY)
							{
								base.NPC.velocity.Y = base.NPC.velocity.Y - wormAccel;
							}
							if ((double)Math.Abs(wormTargetY) < (double)wormSpeed * 0.2 && ((base.NPC.velocity.X > 0f && wormTargetX < 0f) || (base.NPC.velocity.X < 0f && wormTargetX > 0f)))
							{
								if (base.NPC.velocity.Y > 0f)
								{
									base.NPC.velocity.Y = base.NPC.velocity.Y + wormAccel * 2f;
								}
								else
								{
									base.NPC.velocity.Y = base.NPC.velocity.Y - wormAccel * 2f;
								}
							}
							if ((double)Math.Abs(wormTargetX) < (double)wormSpeed * 0.2 && ((base.NPC.velocity.Y > 0f && wormTargetY < 0f) || (base.NPC.velocity.Y < 0f && wormTargetY > 0f)))
							{
								if (base.NPC.velocity.X > 0f)
								{
									base.NPC.velocity.X = base.NPC.velocity.X + wormAccel * 2f;
								}
								else
								{
									base.NPC.velocity.X = base.NPC.velocity.X - wormAccel * 2f;
								}
							}
						}
						else if (absoluteTargetX > absoluteTargetY)
						{
							if (base.NPC.velocity.X < wormTargetX)
							{
								base.NPC.velocity.X = base.NPC.velocity.X + wormAccel * 1.1f;
							}
							else if (base.NPC.velocity.X > wormTargetX)
							{
								base.NPC.velocity.X = base.NPC.velocity.X - wormAccel * 1.1f;
							}
							if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)wormSpeed * 0.5)
							{
								if (base.NPC.velocity.Y > 0f)
								{
									base.NPC.velocity.Y = base.NPC.velocity.Y + wormAccel;
								}
								else
								{
									base.NPC.velocity.Y = base.NPC.velocity.Y - wormAccel;
								}
							}
						}
						else
						{
							if (base.NPC.velocity.Y < wormTargetY)
							{
								base.NPC.velocity.Y = base.NPC.velocity.Y + wormAccel * 1.1f;
							}
							else if (base.NPC.velocity.Y > wormTargetY)
							{
								base.NPC.velocity.Y = base.NPC.velocity.Y - wormAccel * 1.1f;
							}
							if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)wormSpeed * 0.5)
							{
								if (base.NPC.velocity.X > 0f)
								{
									base.NPC.velocity.X = base.NPC.velocity.X + wormAccel;
								}
								else
								{
									base.NPC.velocity.X = base.NPC.velocity.X - wormAccel;
								}
							}
						}
					}
				}
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
				if (base.NPC.type == 7 || base.NPC.type == 10 || base.NPC.type == 39 || base.NPC.type == 95 || base.NPC.type == 98 || base.NPC.type == 117 || base.NPC.type == 510 || base.NPC.type == 513)
				{
					if (flying)
					{
						if (base.NPC.localAI[0] != 1f)
						{
							base.NPC.netUpdate = true;
						}
						base.NPC.localAI[0] = 1f;
					}
					else
					{
						if (base.NPC.localAI[0] != 0f)
						{
							base.NPC.netUpdate = true;
						}
						base.NPC.localAI[0] = 0f;
					}
					if (((base.NPC.velocity.X > 0f && base.NPC.oldVelocity.X < 0f) || (base.NPC.velocity.X < 0f && base.NPC.oldVelocity.X > 0f) || (base.NPC.velocity.Y > 0f && base.NPC.oldVelocity.Y < 0f) || (base.NPC.velocity.Y < 0f && base.NPC.oldVelocity.Y > 0f)) && !base.NPC.justHit)
					{
						base.NPC.netUpdate = true;
					}
				}
			}
			return false;
		}
	}

	public const float ClingerShootGateValue = 120f;

	public const float ClingerShootGateValue_Rev = 90f;

	public const float ClingerTelegraphTime = 30f;

	public const float IchorStickerShootGateValue = 120f;

	public const float IchorStickerShootGateValue_Rev = 90f;

	public const float IchorStickerShootGateValue_Death = 60f;

	public const float IchorStickerTelegraphTime = 30f;

	public const float SpiderWebSpitGateValue = 420f;

	public const float SpiderWebSpitGateValue_Rev = 300f;

	public const float SpiderWebSpitGateValue_Death = 180f;

	public const float SpiderWebSpitTelegraphTime = 30f;

	public const float AntlionSandSpitGateValue = 200f;

	public const float AntlionSandSpitTelegraphTime = 30f;

	public const float BlazingWheelFlameGateValue = 120f;

	public const float BlazingWheelFlameGateValue_Death = 90f;

	public const float BlazingWheelTelegraphTime = 30f;

	public const float HarpyFeatherGateValue = 600f;

	public const float HarpyFeatherTelegraphTime = 30f;

	public const float DemonScytheGateValue = 450f;

	public const float DemonScytheTelegraphTime = 30f;

	public const float RedDevilTridentGateValue = 375f;

	public const float RedDevilTridentTelegraphTime = 30f;

	public const float IceElementalFrostBlastGateValue = 180f;

	public const float IceElementalFrostBlastGateValue_Death = 120f;

	public const float IceElementalFrostBlastTelegraphTime = 30f;

	public const float FungiBulbSporeShootGateValue = 240f;

	public const float GiantFungiBulbSporeShootGateValue = 150f;

	public const float GiantFungiBulbSporeShootGateValue_Rev = 120f;

	public const float FungiBulbSporeTelegraphTime = 30f;

	public const float CorruptorVileSpitGateValue = 180f;

	public const float CorruptorVileSpitTelegraphTime = 30f;

	public const float BloodSquidBloodShotGateValue = 120f;

	public const float BloodSquidBloodShotTelegraphTime = 30f;

	public const float IceGolemFrostBeamGateValue = 120f;

	public const float IceGolemFrostBeamGateValue_Rev = 90f;

	public const float IceGolemFrostBeamGateValue_Death = 60f;

	public const float IceGolemFrostBeamTelegraphTime = 30f;

	public const float EyezorLaserGateValue = 120f;

	public const float EyezorLaserGateValue_Rev = 90f;

	public const float EyezorLaserGateValue_Death = 60f;

	public const float EyezorLaserTelegraphTime = 30f;
}
