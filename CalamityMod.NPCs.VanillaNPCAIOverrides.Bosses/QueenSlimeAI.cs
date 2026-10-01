using System;
using System.Collections.Generic;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public class QueenSlimeAI : VanillaAIOverride
{
	public class CrystalSlimeAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00db: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0794: Unknown result type (might be due to invalid IL or missing references)
			//IL_0389: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0141: Unknown result type (might be due to invalid IL or missing references)
			//IL_016d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb3: Unknown result type (might be due to invalid IL or missing references)
			//IL_048d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0518: Unknown result type (might be due to invalid IL or missing references)
			//IL_0292: Unknown result type (might be due to invalid IL or missing references)
			//IL_02af: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_052c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0531: Unknown result type (might be due to invalid IL or missing references)
			//IL_053f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0540: Unknown result type (might be due to invalid IL or missing references)
			//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02df: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_030b: Unknown result type (might be due to invalid IL or missing references)
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			if (base.NPC.localAI[0] > 0f)
			{
				base.NPC.localAI[0]--;
			}
			if (!base.NPC.wet && Main.player[base.NPC.target].active && !Main.player[base.NPC.target].dead && !Main.player[base.NPC.target].npcTypeNoAggro[base.NPC.type])
			{
				Player obj = Main.player[base.NPC.target];
				Vector2 center = base.NPC.Center;
				float num19 = obj.Center.X - center.X;
				float num20 = obj.Center.Y - center.Y;
				float num21 = (float)Math.Sqrt(num19 * num19 + num20 * num20);
				int num22 = NPC.CountNPCS(658);
				if (Main.expertMode && num22 < 5 && Math.Abs(num19) < 500f && Math.Abs(num20) < 550f && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) && base.NPC.velocity.Y == 0f)
				{
					base.NPC.ai[0] = -40f;
					if (base.NPC.velocity.Y == 0f)
					{
						base.NPC.velocity.X *= 0.9f;
					}
					if (Main.netMode != 1 && base.NPC.localAI[0] == 0f)
					{
						Vector2 vector6 = default(Vector2);
						for (int k = 0; k < 3; k++)
						{
							((Vector2)(ref vector6))._002Ector((float)(k - 1), -4f);
							vector6.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
							vector6.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
							((Vector2)(ref vector6)).Normalize();
							vector6 *= 6f + (float)Main.rand.Next(-50, 51) * 0.01f;
							if (num21 > 350f)
							{
								vector6 *= 2f;
							}
							else if (num21 > 250f)
							{
								vector6 *= 1.5f;
							}
							int type = 920;
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), center, vector6 * (death ? 0.8f : 0.5f), type, SpikeDamage, 0f, Main.myPlayer);
							base.NPC.localAI[0] = 25f;
							if (num22 > 4)
							{
								break;
							}
						}
					}
				}
				else if (Math.Abs(num19) < 500f && Math.Abs(num20) < 550f && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) && base.NPC.velocity.Y == 0f)
				{
					float num23 = num21;
					base.NPC.ai[0] = -40f;
					if (base.NPC.velocity.Y == 0f)
					{
						base.NPC.velocity.X *= 0.9f;
					}
					if (Main.netMode != 1 && base.NPC.localAI[0] == 0f)
					{
						num20 = Main.player[base.NPC.target].position.Y - center.Y - (float)Main.rand.Next(0, 200);
						num21 = (float)Math.Sqrt(num19 * num19 + num20 * num20);
						num21 = 4.5f / num21;
						num21 *= 2f;
						if (num23 > 350f)
						{
							num21 *= 2f;
						}
						else if (num23 > 250f)
						{
							num21 *= 1.5f;
						}
						num19 *= num21;
						num20 *= num21;
						base.NPC.localAI[0] = 50f;
						int type2 = 920;
						Vector2 spikeVelocity = new Vector2(num19, num20) * (death ? 0.8f : 0.5f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), center, spikeVelocity, type2, SpikeDamage, 0f, Main.myPlayer);
					}
				}
			}
			if (base.NPC.ai[2] > 1f)
			{
				base.NPC.ai[2]--;
			}
			if (base.NPC.wet)
			{
				if (base.NPC.collideY)
				{
					base.NPC.velocity.Y = -2f;
				}
				if (base.NPC.velocity.Y < 0f && base.NPC.ai[3] == base.NPC.position.X)
				{
					base.NPC.direction *= -1;
					base.NPC.ai[2] = 200f;
				}
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.ai[3] = base.NPC.position.X;
				}
				if (base.NPC.velocity.Y > 2f)
				{
					base.NPC.velocity.Y *= 0.9f;
				}
				base.NPC.velocity.Y -= 0.5f;
				if (base.NPC.velocity.Y < -4f)
				{
					base.NPC.velocity.Y = -4f;
				}
				if (base.NPC.ai[2] == 1f)
				{
					base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
				}
			}
			base.NPC.aiAction = 0;
			if (base.NPC.ai[2] == 0f)
			{
				base.NPC.ai[0] = -100f;
				base.NPC.ai[2] = 1f;
				base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
			}
			if (base.NPC.velocity.Y == 0f)
			{
				if (base.NPC.collideY && base.NPC.oldVelocity.Y != 0f && Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.position.X -= base.NPC.velocity.X + (float)base.NPC.direction;
				}
				if (base.NPC.ai[3] == base.NPC.position.X)
				{
					base.NPC.direction *= -1;
					base.NPC.ai[2] = 200f;
				}
				base.NPC.ai[3] = 0f;
				base.NPC.velocity.X *= 0.8f;
				if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
				{
					base.NPC.velocity.X = 0f;
				}
				base.NPC.ai[0] += (death ? 16f : (Main.expertMode ? 10f : 7f));
				float num33 = -1000f;
				int num34 = 0;
				if (base.NPC.ai[0] >= 0f)
				{
					num34 = 1;
				}
				if (base.NPC.ai[0] >= num33 && base.NPC.ai[0] <= num33 * 0.5f)
				{
					num34 = 2;
				}
				if (base.NPC.ai[0] >= num33 * 2f && base.NPC.ai[0] <= num33 * 1.5f)
				{
					num34 = 3;
				}
				if (num34 > 0)
				{
					base.NPC.netUpdate = true;
					if (base.NPC.ai[2] == 1f)
					{
						base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
					}
					if (num34 == 3)
					{
						base.NPC.velocity.Y = -8f;
						base.NPC.velocity.X += (death ? 9 : (Main.expertMode ? 6 : 3)) * base.NPC.direction;
						base.NPC.ai[0] = -200f;
						base.NPC.ai[3] = base.NPC.position.X;
					}
					else
					{
						base.NPC.velocity.Y = -6f;
						base.NPC.velocity.X += (death ? 6 : (Main.expertMode ? 4 : 2)) * base.NPC.direction;
						base.NPC.ai[0] = -120f;
						if (num34 == 1)
						{
							base.NPC.ai[0] += num33;
						}
						else
						{
							base.NPC.ai[0] += num33 * 2f;
						}
					}
				}
				else if (base.NPC.ai[0] >= -30f)
				{
					base.NPC.aiAction = 1;
				}
			}
			else if (base.NPC.target < 255 && ((base.NPC.direction == 1 && base.NPC.velocity.X < 3f) || (base.NPC.direction == -1 && base.NPC.velocity.X > -3f)))
			{
				if (base.NPC.collideX && Math.Abs(base.NPC.velocity.X) == 0.2f)
				{
					base.NPC.position.X -= 1.4f * (float)base.NPC.direction;
				}
				if (base.NPC.collideY && base.NPC.oldVelocity.Y != 0f && Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.position.X -= base.NPC.velocity.X + (float)base.NPC.direction;
				}
				if ((base.NPC.direction == -1 && (double)base.NPC.velocity.X < 0.01) || (base.NPC.direction == 1 && (double)base.NPC.velocity.X > -0.01))
				{
					base.NPC.velocity.X += 0.2f * (float)base.NPC.direction;
				}
				else
				{
					base.NPC.velocity.X *= 0.93f;
				}
			}
			return false;
		}
	}

	public class BouncySlimeAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00db: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0127: Unknown result type (might be due to invalid IL or missing references)
			//IL_0153: Unknown result type (might be due to invalid IL or missing references)
			//IL_0547: Unknown result type (might be due to invalid IL or missing references)
			//IL_0995: Unknown result type (might be due to invalid IL or missing references)
			//IL_0227: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_02df: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			if (base.NPC.localAI[0] > 0f)
			{
				base.NPC.localAI[0]--;
			}
			if (!base.NPC.wet && Main.player[base.NPC.target].active && !Main.player[base.NPC.target].dead && !Main.player[base.NPC.target].npcTypeNoAggro[base.NPC.type])
			{
				Player obj = Main.player[base.NPC.target];
				Vector2 center2 = base.NPC.Center;
				float num24 = obj.Center.X - center2.X;
				float num25 = obj.Center.Y - center2.Y;
				float num26 = (float)Math.Sqrt(num24 * num24 + num25 * num25);
				float num27 = num26;
				if (Math.Abs(num24) < 500f && Math.Abs(num25) < 550f && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) && base.NPC.velocity.Y == 0f)
				{
					base.NPC.ai[0] = -40f;
					if (base.NPC.velocity.Y == 0f)
					{
						base.NPC.velocity.X *= 0.9f;
					}
					if (Main.netMode != 1 && base.NPC.localAI[0] == 0f)
					{
						num25 = Main.player[base.NPC.target].position.Y - center2.Y - (float)Main.rand.Next(0, 200);
						num26 = (float)Math.Sqrt(num24 * num24 + num25 * num25);
						num26 = 4.5f / num26;
						num26 *= 2f;
						if (num27 > 350f)
						{
							num26 *= 1.75f;
						}
						else if (num27 > 250f)
						{
							num26 *= 1.25f;
						}
						num24 *= num26;
						num25 *= num26;
						base.NPC.localAI[0] = 40f;
						if (Main.expertMode)
						{
							base.NPC.localAI[0] = 30f;
						}
						int type = 921;
						Vector2 pinkBallVelocity = new Vector2(num24, num25) * (death ? 0.8f : 0.5f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), center2, pinkBallVelocity, type, SmallGelDamage, 0f, Main.myPlayer);
					}
				}
			}
			if (base.NPC.ai[2] > 1f)
			{
				base.NPC.ai[2]--;
			}
			if (base.NPC.wet)
			{
				if (base.NPC.collideY)
				{
					base.NPC.velocity.Y = -2f;
				}
				if (base.NPC.velocity.Y < 0f && base.NPC.ai[3] == base.NPC.position.X)
				{
					base.NPC.direction *= -1;
					base.NPC.ai[2] = 200f;
				}
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.ai[3] = base.NPC.position.X;
				}
				if (base.NPC.velocity.Y > 2f)
				{
					base.NPC.velocity.Y *= 0.9f;
				}
				base.NPC.velocity.Y -= 0.5f;
				if (base.NPC.velocity.Y < -4f)
				{
					base.NPC.velocity.Y = -4f;
				}
				if (base.NPC.ai[2] == 1f)
				{
					base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
				}
			}
			base.NPC.aiAction = 0;
			if (base.NPC.ai[2] == 0f)
			{
				base.NPC.ai[0] = -100f;
				base.NPC.ai[2] = 1f;
				base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
			}
			if (base.NPC.velocity.Y == 0f)
			{
				if (base.NPC.collideY && base.NPC.oldVelocity.Y != 0f && Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.position.X -= base.NPC.velocity.X + (float)base.NPC.direction;
				}
				if (base.NPC.ai[3] == base.NPC.position.X)
				{
					base.NPC.direction *= -1;
					base.NPC.ai[2] = 200f;
				}
				base.NPC.ai[3] = 0f;
				base.NPC.velocity.X *= 0.8f;
				if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
				{
					base.NPC.velocity.X = 0f;
				}
				base.NPC.ai[0] += (death ? 11f : (Main.expertMode ? 7f : 5f));
				float num33 = -500f;
				int num34 = 0;
				if (base.NPC.ai[0] >= 0f)
				{
					num34 = 1;
				}
				if (base.NPC.ai[0] >= num33 && base.NPC.ai[0] <= num33 * 0.5f)
				{
					num34 = 2;
				}
				if (base.NPC.ai[0] >= num33 * 2f && base.NPC.ai[0] <= num33 * 1.5f)
				{
					num34 = 3;
				}
				if (num34 > 0)
				{
					base.NPC.netUpdate = true;
					if (base.NPC.ai[2] == 1f)
					{
						base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
					}
					if (num34 == 3)
					{
						base.NPC.velocity.Y = -8f;
						base.NPC.velocity.X += (death ? 9 : (Main.expertMode ? 6 : 3)) * base.NPC.direction;
						base.NPC.ai[0] = -200f;
						base.NPC.ai[3] = base.NPC.position.X;
					}
					else
					{
						base.NPC.velocity.Y = -6f;
						base.NPC.velocity.X += (death ? 6 : (Main.expertMode ? 4 : 2)) * base.NPC.direction;
						base.NPC.ai[0] = -120f;
						if (num34 == 1)
						{
							base.NPC.ai[0] += num33;
						}
						else
						{
							base.NPC.ai[0] += num33 * 2f;
						}
					}
					base.NPC.velocity.Y *= 1.6f;
					base.NPC.velocity.X *= 1.2f;
				}
				else if (base.NPC.ai[0] >= -30f)
				{
					base.NPC.aiAction = 1;
				}
			}
			else if (base.NPC.target < 255 && ((base.NPC.direction == 1 && base.NPC.velocity.X < 3f) || (base.NPC.direction == -1 && base.NPC.velocity.X > -3f)))
			{
				if (base.NPC.collideX && Math.Abs(base.NPC.velocity.X) == 0.2f)
				{
					base.NPC.position.X -= 1.4f * (float)base.NPC.direction;
				}
				if (base.NPC.collideY && base.NPC.oldVelocity.Y != 0f && Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.position.X -= base.NPC.velocity.X + (float)base.NPC.direction;
				}
				if ((base.NPC.direction == -1 && (double)base.NPC.velocity.X < 0.01) || (base.NPC.direction == 1 && (double)base.NPC.velocity.X > -0.01))
				{
					base.NPC.velocity.X += 0.2f * (float)base.NPC.direction;
				}
				else
				{
					base.NPC.velocity.X *= 0.93f;
				}
			}
			return false;
		}
	}

	public static int SmallGelDamage = (Main.masterMode ? 20 : 17);

	public static int SpikeDamage = (Main.masterMode ? 20 : 17);

	public static int LargeGelDamage = 30;

	public static int SlamDamage = 40;

	public override bool AI(Mod mod)
	{
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_21a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_21a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_183c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1847: Unknown result type (might be due to invalid IL or missing references)
		//IL_2669: Unknown result type (might be due to invalid IL or missing references)
		//IL_2674: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1871: Unknown result type (might be due to invalid IL or missing references)
		//IL_1876: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1006: Unknown result type (might be due to invalid IL or missing references)
		//IL_221e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2226: Unknown result type (might be due to invalid IL or missing references)
		//IL_222b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2230: Unknown result type (might be due to invalid IL or missing references)
		//IL_2241: Unknown result type (might be due to invalid IL or missing references)
		//IL_2246: Unknown result type (might be due to invalid IL or missing references)
		//IL_224b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2250: Unknown result type (might be due to invalid IL or missing references)
		//IL_225c: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_21cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_27b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_27c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_27c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_27ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1089: Unknown result type (might be due to invalid IL or missing references)
		//IL_108e: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c89: Unknown result type (might be due to invalid IL or missing references)
		//IL_1caa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1caf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cde: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b31: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b49: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b84: Unknown result type (might be due to invalid IL or missing references)
		//IL_27ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_27f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_27f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_27fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_2800: Unknown result type (might be due to invalid IL or missing references)
		//IL_2802: Unknown result type (might be due to invalid IL or missing references)
		//IL_2807: Unknown result type (might be due to invalid IL or missing references)
		//IL_2811: Unknown result type (might be due to invalid IL or missing references)
		//IL_2816: Unknown result type (might be due to invalid IL or missing references)
		//IL_2818: Unknown result type (might be due to invalid IL or missing references)
		//IL_281e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2825: Unknown result type (might be due to invalid IL or missing references)
		//IL_282e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2847: Unknown result type (might be due to invalid IL or missing references)
		//IL_2849: Unknown result type (might be due to invalid IL or missing references)
		//IL_2876: Unknown result type (might be due to invalid IL or missing references)
		//IL_2878: Unknown result type (might be due to invalid IL or missing references)
		//IL_27d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_27e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_27e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_27ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_2270: Unknown result type (might be due to invalid IL or missing references)
		//IL_2275: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_18cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18db: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_10af: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_10be: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1124: Unknown result type (might be due to invalid IL or missing references)
		//IL_112e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_19eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_19fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a01: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a05: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1713: Unknown result type (might be due to invalid IL or missing references)
		//IL_173f: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_239c: Unknown result type (might be due to invalid IL or missing references)
		//IL_23ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_23bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_23c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_23cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_23d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_23d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_23db: Unknown result type (might be due to invalid IL or missing references)
		//IL_23dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_23e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_23e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1753: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a39: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a46: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f31: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1963: Unknown result type (might be due to invalid IL or missing references)
		//IL_1965: Unknown result type (might be due to invalid IL or missing references)
		//IL_196c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1971: Unknown result type (might be due to invalid IL or missing references)
		//IL_1976: Unknown result type (might be due to invalid IL or missing references)
		//IL_1938: Unknown result type (might be due to invalid IL or missing references)
		//IL_193a: Unknown result type (might be due to invalid IL or missing references)
		//IL_24df: Unknown result type (might be due to invalid IL or missing references)
		//IL_24f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_24f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_24fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_250e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2513: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_24d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_24dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_241c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2434: Unknown result type (might be due to invalid IL or missing references)
		//IL_243a: Unknown result type (might be due to invalid IL or missing references)
		//IL_243c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2441: Unknown result type (might be due to invalid IL or missing references)
		//IL_2455: Unknown result type (might be due to invalid IL or missing references)
		//IL_245a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2403: Unknown result type (might be due to invalid IL or missing references)
		//IL_2415: Unknown result type (might be due to invalid IL or missing references)
		//IL_241a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1acf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aef: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_25c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_25d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_25d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_25dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_25e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_25e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_2602: Unknown result type (might be due to invalid IL or missing references)
		//IL_2607: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b00: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2009: Unknown result type (might be due to invalid IL or missing references)
		//IL_200e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2010: Unknown result type (might be due to invalid IL or missing references)
		//IL_2024: Unknown result type (might be due to invalid IL or missing references)
		//IL_202a: Unknown result type (might be due to invalid IL or missing references)
		//IL_202c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2031: Unknown result type (might be due to invalid IL or missing references)
		//IL_203f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2041: Unknown result type (might be due to invalid IL or missing references)
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		float slimeScale = 1f;
		bool teleported = false;
		bool phase2 = lifeRatio <= 0.5f;
		bool phase3 = lifeRatio <= (death ? 0.5f : 0.4f);
		bool phase4 = lifeRatio <= (death ? 0.5f : 0.2f);
		bool phase5 = (lifeRatio <= 0.25f) & death;
		if (base.NPC.localAI[0] == 0f)
		{
			base.NPC.ai[1] = -20f;
			base.NPC.localAI[0] = base.NPC.lifeMax;
			base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
			base.NPC.netUpdate = true;
		}
		Lighting.AddLight(base.NPC.Center, 1f, 0.7f, 0.9f);
		int despawnDistanceInTiles = 500;
		if (Main.player[base.NPC.target].dead || Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) / 16f > (float)despawnDistanceInTiles)
		{
			base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
			if (Main.player[base.NPC.target].dead || Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) / 16f > (float)despawnDistanceInTiles)
			{
				base.NPC.EncourageDespawn(10);
				if (Main.player[base.NPC.target].Center.X < base.NPC.Center.X)
				{
					base.NPC.direction = 1;
				}
				else
				{
					base.NPC.direction = -1;
				}
			}
		}
		if ((base.NPC.ai[0] == 1f || base.NPC.ai[0] == 2f) && Math.Abs(base.NPC.velocity.X) > 0.1f)
		{
			base.NPC.velocity.X *= 0.8f;
			if (Math.Abs(base.NPC.velocity.X) <= 0.1f)
			{
				base.NPC.velocity.X = 0f;
			}
		}
		float teleportGateValue = (death ? 300f : 600f);
		if (!Main.player[base.NPC.target].dead && base.NPC.timeLeft > 10 && !phase2 && base.NPC.ai[3] >= teleportGateValue && base.NPC.ai[0] == 0f && base.NPC.velocity.Y == 0f)
		{
			base.NPC.damage = 0;
			base.NPC.ai[0] = 2f;
			base.NPC.ai[1] = 0f;
			if (Main.netMode != 1)
			{
				base.NPC.netUpdate = true;
				CalamityTargetingParameters options = CalamityTargetingParameters.Defaults;
				options.faceTarget = false;
				base.NPC.CalamityTargeting(options);
				Vector2 vectorAimedAheadOfTarget = Main.player[base.NPC.target].Center + Utils.SafeNormalize(new Vector2((float)Math.Round(Main.player[base.NPC.target].velocity.X), 0f), Vector2.Zero) * 800f;
				Point predictiveTeleportPoint = vectorAimedAheadOfTarget.ToTileCoordinates();
				int randomTeleportOffset = 5;
				int teleportTries = 0;
				while (teleportTries < 100)
				{
					teleportTries++;
					int teleportTileX = Main.rand.Next(predictiveTeleportPoint.X - randomTeleportOffset, predictiveTeleportPoint.X + randomTeleportOffset + 1);
					int teleportTileY = Main.rand.Next(predictiveTeleportPoint.Y - randomTeleportOffset, predictiveTeleportPoint.Y);
					if (!Main.tile[teleportTileX, teleportTileY].HasUnactuatedTile)
					{
						bool canTeleportToTile = true;
						if (canTeleportToTile && Main.tile[teleportTileX, teleportTileY].LiquidType == 1)
						{
							canTeleportToTile = false;
						}
						if (canTeleportToTile && !Collision.CanHitLine(base.NPC.Center, 0, 0, vectorAimedAheadOfTarget, 0, 0))
						{
							canTeleportToTile = false;
						}
						if (canTeleportToTile)
						{
							base.NPC.localAI[1] = teleportTileX * 16 + 8;
							base.NPC.localAI[2] = teleportTileY * 16 + 16;
							base.NPC.ai[3] = 0f;
							break;
						}
					}
				}
				if (teleportTries >= 100)
				{
					Vector2 bottom = Main.player[Player.FindClosest(base.NPC.position, base.NPC.width, base.NPC.height)].Bottom;
					base.NPC.localAI[1] = bottom.X;
					base.NPC.localAI[2] = bottom.Y;
					base.NPC.ai[3] = 0f;
				}
			}
		}
		if (!phase2)
		{
			if (base.NPC.ai[3] < teleportGateValue)
			{
				if (!Collision.CanHitLine(base.NPC.Center, 0, 0, Main.player[base.NPC.target].Center, 0, 0) || Math.Abs(base.NPC.Top.Y - Main.player[base.NPC.target].Bottom.Y) > 320f)
				{
					base.NPC.ai[3] += (death ? 3f : 2f);
				}
				else
				{
					base.NPC.ai[3]++;
				}
			}
		}
		else
		{
			float teleportNetUpdate = base.NPC.ai[3];
			base.NPC.ai[3]--;
			if (base.NPC.ai[3] < 0f)
			{
				if (Main.netMode != 1 && teleportNetUpdate > 0f)
				{
					base.NPC.netUpdate = true;
				}
				base.NPC.ai[3] = 0f;
			}
		}
		if (base.NPC.timeLeft <= 10 && ((phase2 && base.NPC.ai[0] != 0f) || (!phase2 && base.NPC.ai[0] != 3f)))
		{
			if (phase2)
			{
				base.NPC.ai[0] = 0f;
			}
			else
			{
				base.NPC.ai[0] = 3f;
			}
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
			base.NPC.ai[3] = 0f;
			base.NPC.netUpdate = true;
		}
		base.NPC.noTileCollide = false;
		base.NPC.noGravity = false;
		if (phase2)
		{
			base.NPC.localAI[3]++;
			if (base.NPC.localAI[3] >= 24f)
			{
				base.NPC.localAI[3] = 0f;
			}
			if ((base.NPC.ai[0] == 4f || base.NPC.ai[0] == 6f) && base.NPC.ai[2] == 1f)
			{
				base.NPC.localAI[3] = 6f;
			}
			if (base.NPC.ai[0] == 5f && base.NPC.ai[2] != 1f)
			{
				base.NPC.localAI[3] = 7f;
			}
		}
		switch ((int)base.NPC.ai[0])
		{
		case 0:
		{
			base.NPC.damage = 0;
			if (phase2)
			{
				QueenSlime_FlyMovement(base.NPC);
			}
			else
			{
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = false;
				if (base.NPC.velocity.Y == 0f)
				{
					base.NPC.velocity.X *= 0.8f;
					if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
					{
						base.NPC.velocity.X = 0f;
					}
				}
			}
			if (base.NPC.timeLeft <= 10 || (!phase2 && base.NPC.velocity.Y != 0f))
			{
				break;
			}
			base.NPC.ai[1]++;
			int idleTime = (death ? 30 : 40);
			if (phase2)
			{
				idleTime = (death ? 60 : 80);
			}
			if (phase4)
			{
				idleTime /= 2;
			}
			if (!(base.NPC.ai[1] > (float)idleTime))
			{
				break;
			}
			base.NPC.ai[1] = 0f;
			if (phase2)
			{
				Player player4 = Main.player[base.NPC.target];
				switch ((int)base.NPC.Calamity().newAI[0])
				{
				default:
					base.NPC.ai[0] = (Main.rand.NextBool() ? 6f : 5f);
					break;
				case 5:
					base.NPC.ai[0] = (phase4 ? 6f : (Main.rand.NextBool() ? 4f : 6f));
					break;
				case 6:
					base.NPC.ai[0] = (phase4 ? 5f : (Main.rand.NextBool() ? 5f : 4f));
					break;
				}
				if (base.NPC.ai[0] == 4f || base.NPC.ai[0] == 6f)
				{
					base.NPC.ai[2] = 1f;
					if (player4 != null && player4.active && !player4.dead && (player4.Bottom.Y < base.NPC.Bottom.Y || Math.Abs(player4.Center.X - base.NPC.Center.X) > 450f))
					{
						base.NPC.ai[0] = 5f;
						base.NPC.ai[2] = 0f;
					}
				}
			}
			else
			{
				switch ((int)base.NPC.Calamity().newAI[0])
				{
				default:
					base.NPC.ai[0] = (Main.rand.NextBool() ? 5f : 4f);
					break;
				case 4:
					base.NPC.ai[0] = (Main.rand.NextBool() ? 3f : 5f);
					break;
				case 5:
					base.NPC.ai[0] = (Main.rand.NextBool() ? 4f : 3f);
					break;
				}
			}
			base.NPC.netUpdate = true;
			break;
		}
		case 1:
		{
			base.NPC.damage = 0;
			base.NPC.rotation = 0f;
			base.NPC.ai[1]++;
			float teleportEndTime = (death ? 15f : 20f);
			slimeScale = MathHelper.Clamp(base.NPC.ai[1] / teleportEndTime, 0f, 1f);
			slimeScale = 0.5f + slimeScale * 0.5f;
			if (base.NPC.ai[1] >= teleportEndTime && Main.netMode != 1)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
				base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
			}
			if (Main.netMode == 1 && base.NPC.ai[1] >= teleportEndTime * 2f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
			}
			Color newColor2 = NPC.AI_121_QueenSlime_GetDustColor();
			((Color)(ref newColor2)).A = 150;
			for (int l = 0; l < 10; l++)
			{
				int queenSlimeDust = Dust.NewDust(base.NPC.position + Vector2.UnitX * -20f, base.NPC.width + 40, base.NPC.height, 4, base.NPC.velocity.X, base.NPC.velocity.Y, 50, newColor2, 1.5f);
				Main.dust[queenSlimeDust].noGravity = true;
				Dust obj = Main.dust[queenSlimeDust];
				obj.velocity *= 2f;
			}
			break;
		}
		case 2:
		{
			base.NPC.damage = 0;
			base.NPC.rotation = 0f;
			base.NPC.ai[1]++;
			float teleportTime = (death ? 30f : 40f);
			slimeScale = MathHelper.Clamp((teleportTime - base.NPC.ai[1]) / teleportTime, 0f, 1f);
			slimeScale = 0.5f + slimeScale * 0.5f;
			if (base.NPC.ai[1] >= teleportTime)
			{
				teleported = true;
			}
			if (base.NPC.ai[1] == teleportTime)
			{
				Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.Center + new Vector2(-40f, (float)(-base.NPC.height / 2)), base.NPC.velocity, 1258);
				if (death)
				{
					int numGelProjectiles2 = 12;
					if (Main.getGoodWorld)
					{
						numGelProjectiles2 = 15;
					}
					float gelVelocity = (death ? 20f : 16f);
					int type2 = 926;
					if (Main.netMode != 1)
					{
						Vector2 spinningpoint2 = default(Vector2);
						for (int m = 0; m < numGelProjectiles2; m++)
						{
							((Vector2)(ref spinningpoint2))._002Ector(gelVelocity, 0f);
							if (Main.getGoodWorld)
							{
								spinningpoint2 *= Main.rand.NextFloat() + 0.5f;
							}
							spinningpoint2 = spinningpoint2.RotatedBy((float)(-m) * ((float)Math.PI * 2f) / (float)numGelProjectiles2, Vector2.Zero);
							int proj4 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, spinningpoint2, type2, LargeGelDamage, 0f, Main.myPlayer, 0f, -2f);
							Main.projectile[proj4].timeLeft = 900;
						}
					}
					SoundEngine.PlaySound(in SoundID.Item155, base.NPC.Center);
				}
			}
			if (base.NPC.ai[1] >= teleportTime && Main.netMode != 1)
			{
				base.NPC.Bottom = new Vector2(base.NPC.localAI[1], base.NPC.localAI[2]);
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			if (Main.netMode == 1 && base.NPC.ai[1] >= teleportTime * 2f)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
			}
			if (!teleported)
			{
				Color newColor3 = NPC.AI_121_QueenSlime_GetDustColor();
				((Color)(ref newColor3)).A = 150;
				for (int n = 0; n < 10; n++)
				{
					int queenSlimeDust2 = Dust.NewDust(base.NPC.position + Vector2.UnitX * -20f, base.NPC.width + 40, base.NPC.height, 4, base.NPC.velocity.X, base.NPC.velocity.Y, 50, newColor3, 1.5f);
					Main.dust[queenSlimeDust2].noGravity = true;
					Dust obj2 = Main.dust[queenSlimeDust2];
					obj2.velocity *= 0.5f;
				}
			}
			break;
		}
		case 3:
			if (base.NPC.velocity.Y > 0f)
			{
				base.NPC.velocity.Y += (death ? 0.05f : 0f);
			}
			base.NPC.rotation = 0f;
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.damage = 0;
				base.NPC.velocity.X *= 0.8f;
				if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
				{
					base.NPC.velocity.X = 0f;
				}
				float timerIncrement = (death ? 6f : 5f);
				base.NPC.ai[1] += timerIncrement;
				if (lifeRatio < 0.85f)
				{
					base.NPC.ai[1] += timerIncrement;
				}
				if (lifeRatio < 0.7f)
				{
					base.NPC.ai[1] += timerIncrement;
				}
				if (!(base.NPC.ai[1] >= 0f))
				{
					break;
				}
				base.NPC.damage = base.NPC.defDamage;
				float distanceBelowTarget = base.NPC.position.Y - (Main.player[base.NPC.target].position.Y + 80f);
				float speedMult = 1f;
				if (distanceBelowTarget > 0f)
				{
					speedMult += distanceBelowTarget * 0.002f;
				}
				if (speedMult > 2f)
				{
					speedMult = 2f;
				}
				base.NPC.netUpdate = true;
				base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
				if (base.NPC.ai[2] == 3f)
				{
					base.NPC.velocity.Y = -13f * speedMult;
					base.NPC.velocity.X += (death ? 6f : 5.5f) * (float)base.NPC.direction;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					if (base.NPC.timeLeft > 10)
					{
						base.NPC.Calamity().newAI[0] = base.NPC.ai[0];
						base.NPC.SyncExtraAI();
						base.NPC.ai[0] = 0f;
					}
					else
					{
						base.NPC.ai[1] = -60f;
					}
				}
				else if (base.NPC.ai[2] == 2f)
				{
					base.NPC.velocity.Y = (0f - (death ? 8f : 6f)) * speedMult;
					base.NPC.velocity.X += (death ? 7.5f : 7f) * (float)base.NPC.direction;
					base.NPC.ai[1] = -40f;
					base.NPC.ai[2]++;
				}
				else
				{
					base.NPC.velocity.Y = (0f - (death ? 10f : 8f)) * speedMult;
					base.NPC.velocity.X += (death ? 6.5f : 6f) * (float)base.NPC.direction;
					base.NPC.ai[1] = -40f;
					base.NPC.ai[2]++;
				}
				if (death)
				{
					base.NPC.velocity.X *= (death ? 1.4f : 1.2f);
				}
				base.NPC.noTileCollide = true;
			}
			else
			{
				if (base.NPC.target >= 255)
				{
					break;
				}
				float jumpVelocity = (death ? 7f : 4.5f);
				if (Main.getGoodWorld)
				{
					jumpVelocity = 12f;
				}
				if ((base.NPC.direction == 1 && base.NPC.velocity.X < jumpVelocity) || (base.NPC.direction == -1 && base.NPC.velocity.X > 0f - jumpVelocity))
				{
					if ((base.NPC.direction == -1 && (double)base.NPC.velocity.X < 0.1) || (base.NPC.direction == 1 && (double)base.NPC.velocity.X > -0.1))
					{
						base.NPC.velocity.X += (death ? 0.45f : 0.3f) * (float)base.NPC.direction;
					}
					else
					{
						base.NPC.velocity.X *= (death ? 0.85f : 0.91f);
					}
				}
				if (!Main.player[base.NPC.target].dead)
				{
					if (base.NPC.velocity.Y > 0f && base.NPC.Bottom.Y > Main.player[base.NPC.target].Top.Y)
					{
						base.NPC.noTileCollide = false;
					}
					else if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].Center, 1, 1) && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
					{
						base.NPC.noTileCollide = false;
					}
					else
					{
						base.NPC.noTileCollide = true;
					}
				}
			}
			break;
		case 4:
		case 6:
		{
			base.NPC.damage = 0;
			base.NPC.rotation *= 0.9f;
			base.NPC.noTileCollide = true;
			base.NPC.noGravity = true;
			if (base.NPC.ai[2] == 1f)
			{
				base.NPC.noTileCollide = false;
				base.NPC.noGravity = false;
				int slamDelay = 30;
				if (phase2)
				{
					slamDelay = 10;
				}
				if (Main.getGoodWorld)
				{
					slamDelay = 0;
				}
				if (base.NPC.velocity.Y == 0f)
				{
					SoundEngine.PlaySound(in SoundID.Item167, base.NPC.Center);
					if (Main.netMode != 1)
					{
						int type3 = 922;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Bottom, Vector2.Zero, type3, SlamDamage, 0f, Main.myPlayer);
						if (death)
						{
							float expandDelay = 0f;
							int maxSmashes = (death ? 22 : 16);
							float maxSmashOffset = (float)maxSmashes * 100f;
							Vector2 extraSmashPosition = base.NPC.Bottom + Vector2.UnitX * maxSmashOffset;
							int maxSmashesPerSide = maxSmashes / 2;
							float maxExpandDelay = (death ? 10f : 15f) * (float)maxSmashesPerSide;
							float smashSpawnDistanceOffset = 200f;
							for (int num = 0; num < maxSmashes + 1; num++)
							{
								expandDelay = MathHelper.Lerp(0f, maxExpandDelay, (float)Math.Abs(num - maxSmashesPerSide) / (float)maxSmashesPerSide);
								if (num != maxSmashesPerSide)
								{
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), extraSmashPosition, Vector2.Zero, type3, SlamDamage, 0f, Main.myPlayer, 0f - expandDelay);
								}
								extraSmashPosition -= Vector2.UnitX * smashSpawnDistanceOffset;
							}
						}
						if ((base.NPC.ai[0] == 6f) & phase3)
						{
							float projectileVelocity2 = (death ? 18f : 12f);
							type3 = 920;
							Vector2 destination2 = (new Vector2(base.NPC.Center.X, base.NPC.Center.Y - 100f) - base.NPC.Center).SafeNormalize(Vector2.UnitY);
							destination2 *= projectileVelocity2;
							int numProj = 20;
							float rotation2 = MathHelper.ToRadians(100f);
							for (int num2 = 0; num2 < numProj; num2++)
							{
								Vector2 perturbedSpeed2 = destination2.RotatedBy(MathHelper.Lerp(0f - rotation2, rotation2, (float)num2 / (float)(numProj - 1)));
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, perturbedSpeed2, type3, SpikeDamage, 0f, Main.myPlayer, 0f, -2f);
							}
							if (phase5)
							{
								numProj = 15;
								destination2 *= 0.65f;
								for (int num3 = 0; num3 < numProj; num3++)
								{
									Vector2 perturbedSpeed3 = destination2.RotatedBy(MathHelper.Lerp(0f - rotation2, rotation2, (float)num3 / (float)(numProj - 1)));
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, perturbedSpeed3, type3, SpikeDamage, 0f, Main.myPlayer, 0f, -2f);
								}
							}
						}
					}
					for (int num4 = 0; num4 < 20; num4++)
					{
						int slamDust = Dust.NewDust(base.NPC.Bottom - new Vector2((float)(base.NPC.width / 2), 30f), base.NPC.width, 30, 31, base.NPC.velocity.X, base.NPC.velocity.Y, 40, NPC.AI_121_QueenSlime_GetDustColor());
						Main.dust[slamDust].noGravity = true;
						Main.dust[slamDust].velocity.Y = -5f + Main.rand.NextFloat() * -3f;
						Main.dust[slamDust].velocity.X *= 7f;
					}
					base.NPC.Calamity().newAI[0] = base.NPC.ai[0];
					base.NPC.SyncExtraAI();
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.netUpdate = true;
				}
				else if (base.NPC.ai[1] >= (float)slamDelay)
				{
					for (int num5 = 0; num5 < 4; num5++)
					{
						Vector2 position = base.NPC.Bottom - new Vector2(Main.rand.NextFloatDirection() * 16f, (float)Main.rand.Next(8));
						int slamDust2 = Dust.NewDust(position, 2, 2, 31, base.NPC.velocity.X, base.NPC.velocity.Y, 40, NPC.AI_121_QueenSlime_GetDustColor(), 1.4f);
						Main.dust[slamDust2].position = position;
						Main.dust[slamDust2].noGravity = true;
						Main.dust[slamDust2].velocity.Y = base.NPC.velocity.Y * 0.9f;
						Main.dust[slamDust2].velocity.X = (Main.rand.NextBool() ? (-10f) : 10f) + Main.rand.NextFloatDirection() * 3f;
					}
				}
				base.NPC.velocity.X *= 0.8f;
				float slamNetUpdate = base.NPC.ai[1];
				base.NPC.ai[1]++;
				if (base.NPC.ai[1] >= (float)slamDelay)
				{
					if (slamNetUpdate < (float)slamDelay)
					{
						base.NPC.netUpdate = true;
					}
					if (phase2 && base.NPC.ai[1] > (float)(slamDelay + 120))
					{
						base.NPC.Calamity().newAI[0] = base.NPC.ai[0];
						base.NPC.SyncExtraAI();
						base.NPC.ai[0] = 0f;
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
						base.NPC.velocity.Y *= 0.8f;
						base.NPC.netUpdate = true;
						break;
					}
					base.NPC.damage = base.NPC.defDamage;
					base.NPC.velocity.Y += (death ? 1.75f : 1.5f);
					float slamVelocity = (death ? 15.5f : 15f);
					if (Main.getGoodWorld)
					{
						base.NPC.velocity.Y++;
						slamVelocity = 15.99f;
					}
					if (base.NPC.velocity.Y == 0f)
					{
						base.NPC.velocity.Y = 0.01f;
					}
					if (base.NPC.velocity.Y >= slamVelocity)
					{
						base.NPC.velocity.Y = slamVelocity;
					}
					if (!(((base.NPC.ai[0] == 4f) & phase3) | phase4) || base.NPC.ai[1] % 12f != 0f)
					{
						break;
					}
					SoundEngine.PlaySound(in SoundID.Item154, base.NPC.Center);
					if (Main.netMode == 1)
					{
						break;
					}
					Vector2 fireFrom = base.NPC.Center;
					int projectileAmt = 2;
					int type4 = 920;
					Vector2 velocityIncrease = (death ? (Vector2.UnitY * 4f) : Vector2.Zero);
					for (int num6 = 0; num6 < projectileAmt; num6++)
					{
						int totalProjectiles = 2;
						float radians = (float)Math.PI * 2f / (float)totalProjectiles;
						for (int num7 = 0; num7 < totalProjectiles; num7++)
						{
							Vector2 projVelocity = (base.NPC.velocity + velocityIncrease).RotatedBy(radians * (float)num7 + (float)Math.PI / 2f);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireFrom, projVelocity, type4, SpikeDamage, 0f, Main.myPlayer, 0f, -1f);
						}
					}
				}
				else
				{
					base.NPC.velocity.Y *= 0.8f;
				}
				break;
			}
			if (Main.netMode != 1 && base.NPC.ai[1] == 0f)
			{
				base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
				base.NPC.netUpdate = true;
			}
			base.NPC.ai[1]++;
			if (!(base.NPC.ai[1] >= 30f))
			{
				break;
			}
			if (base.NPC.ai[1] >= 60f)
			{
				base.NPC.ai[1] = 60f;
				if (Main.netMode != 1)
				{
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 1f;
					base.NPC.velocity.Y = -3f;
					base.NPC.netUpdate = true;
				}
			}
			Player player3 = Main.player[base.NPC.target];
			Vector2 center = base.NPC.Center;
			if (!player3.dead && player3.active && Math.Abs(base.NPC.Center.X - player3.Center.X) / 16f <= (float)despawnDistanceInTiles)
			{
				center = player3.Center;
			}
			center.Y -= 384f;
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity = center - base.NPC.Center;
				base.NPC.velocity = base.NPC.velocity.SafeNormalize(Vector2.Zero);
				NPC nPC = base.NPC;
				nPC.velocity *= (death ? 27.3f : 24f);
			}
			else
			{
				base.NPC.velocity.Y *= 0.95f;
			}
			break;
		}
		case 5:
		{
			base.NPC.damage = 0;
			base.NPC.rotation *= 0.9f;
			base.NPC.noTileCollide = true;
			base.NPC.noGravity = true;
			if (phase2)
			{
				base.NPC.ai[3] = 0f;
			}
			if (base.NPC.ai[2] == 1f)
			{
				base.NPC.ai[1]++;
				if (!(base.NPC.ai[1] >= 10f))
				{
					break;
				}
				if (Main.netMode != 1)
				{
					int numGelProjectiles = (phase4 ? Main.rand.Next(9, 12) : (phase2 ? Main.rand.Next(6, 9) : 12));
					if (Main.getGoodWorld)
					{
						numGelProjectiles = 15;
					}
					float projectileVelocity = (death ? 12f : 10.5f);
					int type = 926;
					if (phase2)
					{
						Vector2 destination = (new Vector2(base.NPC.Center.X, base.NPC.Center.Y + 100f) - base.NPC.Center).SafeNormalize(Vector2.UnitY);
						destination *= projectileVelocity;
						float rotation = MathHelper.ToRadians(120f);
						for (int i = 0; i < numGelProjectiles; i++)
						{
							if (Main.getGoodWorld)
							{
								destination *= Main.rand.NextFloat() + 0.5f;
							}
							Vector2 perturbedSpeed = destination.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)i / (float)(numGelProjectiles - 1)));
							int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, perturbedSpeed, type, LargeGelDamage, 0f, Main.myPlayer, 0f, -2f);
							Main.projectile[proj].timeLeft = 900;
						}
					}
					else
					{
						Vector2 spinningpoint = default(Vector2);
						for (int j = 0; j < numGelProjectiles; j++)
						{
							((Vector2)(ref spinningpoint))._002Ector(projectileVelocity, 0f);
							if (Main.getGoodWorld)
							{
								spinningpoint *= Main.rand.NextFloat() + 0.5f;
							}
							spinningpoint = spinningpoint.RotatedBy((float)(-j) * ((float)Math.PI * 2f) / (float)numGelProjectiles, Vector2.Zero);
							int proj2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, spinningpoint, type, LargeGelDamage, 0f, Main.myPlayer, 0f, -2f);
							Main.projectile[proj2].timeLeft = 900;
						}
					}
					List<int> targets = new List<int>();
					for (int p = 0; p < 255; p++)
					{
						if (Main.player[p].active && !Main.player[p].dead)
						{
							targets.Add(p);
						}
						if (targets.Count > 2)
						{
							break;
						}
					}
					foreach (int t in targets)
					{
						Vector2 velocity2 = (Main.player[t].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * projectileVelocity;
						int proj3 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity2, type, LargeGelDamage, 0f, Main.myPlayer, 0f, -2f);
						Main.projectile[proj3].timeLeft = 900;
					}
				}
				SoundEngine.PlaySound(in SoundID.Item155, base.NPC.Center);
				base.NPC.Calamity().newAI[0] = base.NPC.ai[0];
				base.NPC.SyncExtraAI();
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
				break;
			}
			if (Main.netMode != 1 && base.NPC.ai[1] == 0f)
			{
				base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
				base.NPC.netUpdate = true;
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= 50f)
			{
				base.NPC.ai[1] = 50f;
				if (Main.netMode != 1)
				{
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 1f;
					base.NPC.netUpdate = true;
				}
			}
			float slamDustRadius = 100f;
			for (int k = 0; k < 4; k++)
			{
				Vector2 slamDustArea = base.NPC.Center + Main.rand.NextVector2CircularEdge(slamDustRadius, slamDustRadius);
				if (!phase2)
				{
					slamDustArea += new Vector2(0f, 20f);
				}
				Vector2 v = slamDustArea - base.NPC.Center;
				v = v.SafeNormalize(Vector2.Zero) * -8f;
				int superSlamDust = Dust.NewDust(slamDustArea, 2, 2, 31, v.X, v.Y, 40, NPC.AI_121_QueenSlime_GetDustColor(), 1.8f);
				Main.dust[superSlamDust].position = slamDustArea;
				Main.dust[superSlamDust].noGravity = true;
				Main.dust[superSlamDust].alpha = 250;
				Main.dust[superSlamDust].velocity = v;
				Main.dust[superSlamDust].customData = base.NPC;
			}
			if (phase2)
			{
				QueenSlime_FlyMovement(base.NPC);
			}
			break;
		}
		}
		base.NPC.dontTakeDamage = (base.NPC.hide = teleported);
		if (slimeScale != base.NPC.scale)
		{
			base.NPC.position.X += base.NPC.width / 2;
			base.NPC.position.Y += base.NPC.height;
			base.NPC.scale = slimeScale;
			base.NPC.width = (int)(114f * base.NPC.scale);
			base.NPC.height = (int)(100f * base.NPC.scale);
			base.NPC.position.X -= base.NPC.width / 2;
			base.NPC.position.Y -= base.NPC.height;
		}
		if (base.NPC.life <= 0 || (phase4 && !death))
		{
			return false;
		}
		if (Main.netMode == 1)
		{
			return false;
		}
		if (base.NPC.localAI[0] >= (float)(base.NPC.lifeMax / 2) && base.NPC.life < base.NPC.lifeMax / 2)
		{
			base.NPC.localAI[0] = base.NPC.life;
			base.NPC.ai[0] = 0f;
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
			base.NPC.netUpdate = true;
		}
		float slimeSpawnHealthGateValue = (Main.zenithWorld ? 0.01f : (phase3 ? 0.04f : (phase2 ? 0.03f : 0.025f)));
		if (death)
		{
			slimeSpawnHealthGateValue *= 0.5f;
		}
		int slimeSpawnThreshold = (int)((float)base.NPC.lifeMax * slimeSpawnHealthGateValue);
		if (!((float)(base.NPC.life + slimeSpawnThreshold) < base.NPC.localAI[0]))
		{
			return false;
		}
		base.NPC.localAI[0] = base.NPC.life;
		int offset = 16;
		int x = (int)(base.NPC.position.X + (float)offset + (float)Main.rand.Next(base.NPC.width - offset * 2));
		int y = (int)(base.NPC.position.Y + (float)offset + (float)Main.rand.Next(base.NPC.height - offset * 2));
		int random = Main.rand.Next(2);
		if (phase2)
		{
			random++;
		}
		if (phase3)
		{
			random = 2;
		}
		int typeToSpawn = 658;
		switch (random)
		{
		case 0:
			typeToSpawn = 658;
			break;
		case 1:
			typeToSpawn = 659;
			break;
		case 2:
			typeToSpawn = 660;
			break;
		}
		int slimeScale2 = NPC.NewNPC(base.NPC.GetSource_FromAI(), x, y, typeToSpawn);
		Main.npc[slimeScale2].SetDefaults(typeToSpawn);
		Main.npc[slimeScale2].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
		Main.npc[slimeScale2].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
		Main.npc[slimeScale2].ai[0] = -500 * Main.rand.Next(3);
		Main.npc[slimeScale2].ai[1] = 0f;
		if (Main.dedServ && slimeScale2 < Main.maxNPCs)
		{
			NetMessage.SendData(23, -1, -1, null, slimeScale2);
		}
		return false;
	}

	public static void QueenSlime_FlyMovement(NPC npc)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		bool num = CalamityWorld.death || BossRushEvent.BossRushActive;
		npc.noTileCollide = true;
		npc.noGravity = true;
		float flyVelocity = (num ? 19f : 16f);
		float flyAcceleration = (num ? 0.15f : 0.12f);
		float flyDistanceY = (num ? 350f : 450f);
		Vector2 desiredVelocity = npc.Center;
		if (npc.timeLeft > 10)
		{
			if (!Collision.CanHit(npc, Main.player[npc.target]))
			{
				bool flyToSolidTilesAboveTarget = false;
				Vector2 center = Main.player[npc.target].Center;
				for (int i = 0; i < 16; i++)
				{
					float tileDistanceAboveTarget = 16 * i;
					Point point = (center + new Vector2(0f, 0f - tileDistanceAboveTarget)).ToTileCoordinates();
					if (WorldGen.SolidOrSlopedTile(point.X, point.Y))
					{
						desiredVelocity = center + new Vector2(0f, 0f - tileDistanceAboveTarget + 16f) - npc.Center;
						flyToSolidTilesAboveTarget = true;
						break;
					}
				}
				if (!flyToSolidTilesAboveTarget)
				{
					desiredVelocity = center - npc.Center;
				}
			}
			else
			{
				desiredVelocity = Main.player[npc.target].Center + new Vector2(0f, 0f - flyDistanceY) - npc.Center;
			}
		}
		else
		{
			desiredVelocity = npc.Center + new Vector2(500f * (float)npc.direction, 0f - flyDistanceY) - npc.Center;
		}
		float distanceFromFlightTarget = ((Vector2)(ref desiredVelocity)).Length();
		if (Math.Abs(desiredVelocity.X) < 40f)
		{
			desiredVelocity.X = npc.velocity.X;
		}
		if (distanceFromFlightTarget > 100f && ((npc.velocity.X < -12f && desiredVelocity.X > 0f) || (npc.velocity.X > 12f && desiredVelocity.X < 0f)))
		{
			flyAcceleration = 0.2f;
		}
		if (distanceFromFlightTarget < 40f)
		{
			desiredVelocity = npc.velocity;
		}
		else if (distanceFromFlightTarget < 80f)
		{
			desiredVelocity = desiredVelocity.SafeNormalize(Vector2.UnitY);
			desiredVelocity *= flyVelocity * 0.65f;
		}
		else
		{
			desiredVelocity = desiredVelocity.SafeNormalize(Vector2.UnitY);
			desiredVelocity *= flyVelocity;
		}
		npc.SimpleFlyMovement(desiredVelocity, flyAcceleration);
		npc.rotation = npc.velocity.X * 0.1f;
		if (npc.rotation > 0.5f)
		{
			npc.rotation = 0.5f;
		}
		if (npc.rotation < -0.5f)
		{
			npc.rotation = -0.5f;
		}
	}
}
