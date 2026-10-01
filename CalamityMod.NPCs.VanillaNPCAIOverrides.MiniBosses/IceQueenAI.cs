using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.MiniBosses;

public class IceQueenAI : VanillaAIOverride
{
	public override bool AI(Mod mod)
	{
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d52: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f75: Unknown result type (might be due to invalid IL or missing references)
		//IL_084f: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dayTime)
		{
			if (base.NPC.velocity.X > 0f)
			{
				base.NPC.velocity.X += 0.25f;
			}
			else
			{
				base.NPC.velocity.X -= 0.25f;
			}
			base.NPC.velocity.Y -= 0.1f;
			base.NPC.rotation = base.NPC.velocity.X * 0.05f;
		}
		else if (base.NPC.ai[0] == 0f)
		{
			if (base.NPC.ai[2] == 0f)
			{
				base.NPC.TargetClosest();
				if (base.NPC.Center.X < Main.player[base.NPC.target].Center.X)
				{
					base.NPC.ai[2] = 1f;
				}
				else
				{
					base.NPC.ai[2] = -1f;
				}
			}
			base.NPC.TargetClosest();
			float iceQueenTargetDist = Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X);
			if (base.NPC.Center.X < Main.player[base.NPC.target].Center.X && base.NPC.ai[2] < 0f && iceQueenTargetDist > 800f)
			{
				base.NPC.ai[2] = 0f;
			}
			if (base.NPC.Center.X > Main.player[base.NPC.target].Center.X && base.NPC.ai[2] > 0f && iceQueenTargetDist > 800f)
			{
				base.NPC.ai[2] = 0f;
			}
			float iceQueenAcceleration = 0.6f;
			float iceQueenMaxVelocity = 10f;
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.75)
			{
				iceQueenAcceleration = 0.7f;
				iceQueenMaxVelocity = 12f;
			}
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.5)
			{
				iceQueenAcceleration = 0.8f;
				iceQueenMaxVelocity = 14f;
			}
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.25)
			{
				iceQueenAcceleration = 0.95f;
				iceQueenMaxVelocity = 16f;
			}
			base.NPC.velocity.X += base.NPC.ai[2] * iceQueenAcceleration;
			if (base.NPC.velocity.X > iceQueenMaxVelocity)
			{
				base.NPC.velocity.X = iceQueenMaxVelocity;
			}
			if (base.NPC.velocity.X < 0f - iceQueenMaxVelocity)
			{
				base.NPC.velocity.X = 0f - iceQueenMaxVelocity;
			}
			float num = Main.player[base.NPC.target].position.Y - (base.NPC.position.Y + (float)base.NPC.height);
			if (num < 150f)
			{
				base.NPC.velocity.Y -= 0.2f;
			}
			if (num > 200f)
			{
				base.NPC.velocity.Y += 0.2f;
			}
			if (base.NPC.velocity.Y > 9f)
			{
				base.NPC.velocity.Y = 9f;
			}
			if (base.NPC.velocity.Y < -9f)
			{
				base.NPC.velocity.Y = -9f;
			}
			base.NPC.rotation = base.NPC.velocity.X * 0.05f;
			if ((iceQueenTargetDist < 500f || base.NPC.ai[3] < 0f) && base.NPC.position.Y < Main.player[base.NPC.target].position.Y)
			{
				base.NPC.ai[3]++;
				int frostWaveFireDelay = 8;
				if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.75)
				{
					frostWaveFireDelay = 7;
				}
				if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.5)
				{
					frostWaveFireDelay = 6;
				}
				if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.25)
				{
					frostWaveFireDelay = 5;
				}
				frostWaveFireDelay++;
				if (base.NPC.ai[3] > (float)frostWaveFireDelay)
				{
					base.NPC.ai[3] = 0f - (float)frostWaveFireDelay;
				}
				if (base.NPC.ai[3] == 0f && Main.netMode != 1)
				{
					Vector2 frostWavePosition = default(Vector2);
					((Vector2)(ref frostWavePosition))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
					frostWavePosition.X += base.NPC.velocity.X * 7f;
					float frostWaveTargetX = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - frostWavePosition.X;
					float frostWaveTargetY = Main.player[base.NPC.target].Center.Y - frostWavePosition.Y;
					float frostWaveTargetDist = (float)Math.Sqrt(frostWaveTargetX * frostWaveTargetX + frostWaveTargetY * frostWaveTargetY);
					float frostWaveSpeed = 8f;
					if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.75)
					{
						frostWaveSpeed = 9f;
					}
					if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.5)
					{
						frostWaveSpeed = 10f;
					}
					if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.25)
					{
						frostWaveSpeed = 11f;
					}
					frostWaveTargetDist = frostWaveSpeed / frostWaveTargetDist;
					frostWaveTargetX *= frostWaveTargetDist;
					frostWaveTargetY *= frostWaveTargetDist;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), frostWavePosition.X, frostWavePosition.Y, frostWaveTargetX, frostWaveTargetY, 348, 50, 0f, Main.myPlayer);
				}
			}
			else if (base.NPC.ai[3] < 0f)
			{
				base.NPC.ai[3]++;
			}
			if (Main.netMode != 1)
			{
				base.NPC.ai[1] += Main.rand.Next(1, 4);
				if (base.NPC.ai[1] > 600f && iceQueenTargetDist < 600f)
				{
					base.NPC.ai[0] = -1f;
				}
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.TargetClosest();
			float icicleAttackAcceleration = 0.2f;
			float icicleAttackMaxVelocity = 10f;
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.75)
			{
				icicleAttackAcceleration = 0.24f;
				icicleAttackMaxVelocity = 12f;
			}
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.5)
			{
				icicleAttackAcceleration = 0.28f;
				icicleAttackMaxVelocity = 14f;
			}
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.25)
			{
				icicleAttackAcceleration = 0.32f;
				icicleAttackMaxVelocity = 16f;
			}
			icicleAttackAcceleration -= 0.05f;
			icicleAttackMaxVelocity--;
			if (base.NPC.Center.X < Main.player[base.NPC.target].Center.X)
			{
				base.NPC.velocity.X += icicleAttackAcceleration;
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X *= 0.98f;
				}
			}
			if (base.NPC.Center.X > Main.player[base.NPC.target].Center.X)
			{
				base.NPC.velocity.X -= icicleAttackAcceleration;
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X *= 0.98f;
				}
			}
			if (base.NPC.velocity.X > icicleAttackMaxVelocity || base.NPC.velocity.X < 0f - icicleAttackMaxVelocity)
			{
				base.NPC.velocity.X *= 0.95f;
			}
			float num2 = Main.player[base.NPC.target].position.Y - (base.NPC.position.Y + (float)base.NPC.height);
			if (num2 < 180f)
			{
				base.NPC.velocity.Y -= 0.1f;
			}
			if (num2 > 200f)
			{
				base.NPC.velocity.Y += 0.1f;
			}
			if (base.NPC.velocity.Y > 7f)
			{
				base.NPC.velocity.Y = 7f;
			}
			if (base.NPC.velocity.Y < -7f)
			{
				base.NPC.velocity.Y = -7f;
			}
			base.NPC.rotation = base.NPC.velocity.X * 0.01f;
			if (Main.netMode != 1)
			{
				base.NPC.ai[3]++;
				int icicleFireDelay = 10;
				if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.75)
				{
					icicleFireDelay = 8;
				}
				if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.5)
				{
					icicleFireDelay = 6;
				}
				if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.25)
				{
					icicleFireDelay = 4;
				}
				if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.1)
				{
					icicleFireDelay = 2;
				}
				icicleFireDelay += 3;
				if (base.NPC.ai[3] >= (float)icicleFireDelay)
				{
					base.NPC.ai[3] = 0f;
					Vector2 icicleSpawnPos = default(Vector2);
					((Vector2)(ref icicleSpawnPos))._002Ector(base.NPC.Center.X, base.NPC.position.Y + (float)base.NPC.height - 14f);
					int i = (int)(icicleSpawnPos.X / 16f);
					int j2 = (int)(icicleSpawnPos.Y / 16f);
					if (!WorldGen.SolidTile(i, j2))
					{
						float icicleFallSpeed = base.NPC.velocity.Y;
						if (icicleFallSpeed < 0f)
						{
							icicleFallSpeed = 0f;
						}
						icicleFallSpeed += 3f;
						float speedX2 = base.NPC.velocity.X * 0.25f;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), icicleSpawnPos.X, icicleSpawnPos.Y, speedX2, icicleFallSpeed, 349, 44, 0f, Main.myPlayer, Main.rand.Next(5));
					}
				}
			}
			if (Main.netMode != 1)
			{
				base.NPC.ai[1] += Main.rand.Next(1, 4);
				if (base.NPC.ai[1] > 450f)
				{
					base.NPC.ai[0] = -1f;
				}
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.TargetClosest();
			Vector2 iceRainPosition = default(Vector2);
			((Vector2)(ref iceRainPosition))._002Ector(base.NPC.Center.X, base.NPC.Center.Y - 20f);
			float iceRainXVel = Main.rand.Next(-1000, 1001);
			float iceRainYVel = Main.rand.Next(-1000, 1001);
			float iceRainVelocity = (float)Math.Sqrt(iceRainXVel * iceRainXVel + iceRainYVel * iceRainYVel);
			NPC nPC = base.NPC;
			nPC.velocity *= 0.95f;
			iceRainVelocity = 20f / iceRainVelocity;
			iceRainXVel *= iceRainVelocity;
			iceRainYVel *= iceRainVelocity;
			base.NPC.rotation += 0.2f;
			iceRainPosition.X += iceRainXVel * 4f;
			iceRainPosition.Y += iceRainYVel * 4f;
			base.NPC.ai[3]++;
			int iceRainFireDelay = 7;
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.75)
			{
				iceRainFireDelay--;
			}
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.5)
			{
				iceRainFireDelay -= 2;
			}
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.25)
			{
				iceRainFireDelay -= 3;
			}
			if ((double)base.NPC.life < (double)base.NPC.lifeMax * 0.1)
			{
				iceRainFireDelay -= 4;
			}
			if (base.NPC.ai[3] > (float)iceRainFireDelay)
			{
				base.NPC.ai[3] = 0f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), iceRainPosition.X, iceRainPosition.Y, iceRainXVel, iceRainYVel, 349, 40, 0f, Main.myPlayer);
			}
			if (Main.netMode != 1)
			{
				base.NPC.ai[1] += Main.rand.Next(1, 4);
				if (base.NPC.ai[1] > 300f)
				{
					base.NPC.ai[0] = -1f;
				}
			}
		}
		if (base.NPC.ai[0] == -1f)
		{
			int attackPicker = Main.rand.Next(3);
			base.NPC.TargetClosest();
			if (Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) > 1000f)
			{
				attackPicker = 0;
			}
			base.NPC.ai[0] = attackPicker;
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
			base.NPC.ai[3] = 0f;
		}
		return false;
	}
}
