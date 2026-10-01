using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
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

public class SkeletronPrimeAI : VanillaAIOverride
{
	public class PrimeLaserAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0481: Unknown result type (might be due to invalid IL or missing references)
			//IL_049f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0904: Unknown result type (might be due to invalid IL or missing references)
			//IL_0909: Unknown result type (might be due to invalid IL or missing references)
			//IL_091c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0926: Unknown result type (might be due to invalid IL or missing references)
			//IL_0941: Unknown result type (might be due to invalid IL or missing references)
			//IL_094b: Unknown result type (might be due to invalid IL or missing references)
			//IL_067a: Unknown result type (might be due to invalid IL or missing references)
			//IL_067f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0692: Unknown result type (might be due to invalid IL or missing references)
			//IL_069c: Unknown result type (might be due to invalid IL or missing references)
			//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_07db: Unknown result type (might be due to invalid IL or missing references)
			//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_07df: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a8d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a7e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a92: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a9c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aad: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aaf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0acd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0acf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ad4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ade: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae8: Unknown result type (might be due to invalid IL or missing references)
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
			}
			base.NPC.spriteDirection = -(int)base.NPC.ai[0];
			if (!Main.npc[(int)base.NPC.ai[1]].active || Main.npc[(int)base.NPC.ai[1]].aiStyle != 32)
			{
				base.NPC.ai[2] += 10f;
				if (base.NPC.ai[2] > 50f || !Main.dedServ)
				{
					base.NPC.life = -1;
					base.NPC.HitEffect();
					base.NPC.active = false;
				}
			}
			CalamityGlobalNPC.primeLaser = base.NPC.whoAmI;
			bool cannonAlive = false;
			bool viceAlive = false;
			bool sawAlive = false;
			if (CalamityGlobalNPC.primeCannon != -1 && Main.npc[CalamityGlobalNPC.primeCannon].active)
			{
				cannonAlive = true;
			}
			if (CalamityGlobalNPC.primeVice != -1 && Main.npc[CalamityGlobalNPC.primeVice].active)
			{
				viceAlive = true;
			}
			if (CalamityGlobalNPC.primeSaw != -1 && Main.npc[CalamityGlobalNPC.primeSaw].active)
			{
				sawAlive = true;
			}
			float timeToNotAttack = 180f;
			bool dontAttack = base.NPC.Calamity().newAI[2] < timeToNotAttack;
			if (dontAttack)
			{
				base.NPC.Calamity().newAI[2]++;
				if (base.NPC.Calamity().newAI[2] >= timeToNotAttack)
				{
					base.NPC.SyncExtraAI();
				}
			}
			bool normalLaserRotation = base.NPC.localAI[1] % 2f == 0f;
			float acceleration = (death ? 0.385f : 0.25f);
			float accelerationMult = 1f;
			if (!cannonAlive)
			{
				acceleration += 0.025f;
				accelerationMult += 0.5f;
			}
			if (!viceAlive)
			{
				acceleration += 0.025f;
			}
			if (!sawAlive)
			{
				acceleration += 0.025f;
			}
			if (death)
			{
				acceleration *= accelerationMult;
			}
			float topVelocity = acceleration * 100f;
			float deceleration = (death ? 0.6f : 0.8f);
			if (base.NPC.position.Y > Main.npc[(int)base.NPC.ai[1]].position.Y - 70f)
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y *= deceleration;
				}
				base.NPC.velocity.Y -= acceleration;
				if (base.NPC.velocity.Y > topVelocity)
				{
					base.NPC.velocity.Y = topVelocity;
				}
			}
			else if (base.NPC.position.Y < Main.npc[(int)base.NPC.ai[1]].position.Y - 100f)
			{
				if (base.NPC.velocity.Y < 0f)
				{
					base.NPC.velocity.Y *= deceleration;
				}
				base.NPC.velocity.Y += acceleration;
				if (base.NPC.velocity.Y < 0f - topVelocity)
				{
					base.NPC.velocity.Y = 0f - topVelocity;
				}
			}
			if (base.NPC.Center.X > Main.npc[(int)base.NPC.ai[1]].Center.X - 130f * base.NPC.ai[0])
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X *= deceleration;
				}
				base.NPC.velocity.X -= acceleration;
				if (base.NPC.velocity.X > topVelocity)
				{
					base.NPC.velocity.X = topVelocity;
				}
			}
			if (base.NPC.Center.X < Main.npc[(int)base.NPC.ai[1]].Center.X - 160f * base.NPC.ai[0])
			{
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X *= deceleration;
				}
				base.NPC.velocity.X += acceleration;
				if (base.NPC.velocity.X < 0f - topVelocity)
				{
					base.NPC.velocity.X = 0f - topVelocity;
				}
			}
			if (base.NPC.ai[2] == 0f)
			{
				if (Main.npc[(int)base.NPC.ai[1]].ai[1] == 3f && base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
				}
				base.NPC.ai[3]++;
				if (!cannonAlive)
				{
					base.NPC.ai[3]++;
				}
				if (!viceAlive)
				{
					base.NPC.ai[3]++;
				}
				if (!sawAlive)
				{
					base.NPC.ai[3]++;
				}
				if (base.NPC.ai[3] >= (death ? 200f : 800f))
				{
					base.NPC.target = Main.npc[(int)base.NPC.ai[1]].target;
					base.NPC.localAI[0] = 0f;
					base.NPC.ai[2] = 1f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
				Vector2 laserArmPosition = base.NPC.Center;
				float laserArmTargetX = Main.player[base.NPC.target].Center.X - laserArmPosition.X;
				float laserArmTargetY = Main.player[base.NPC.target].Center.Y - laserArmPosition.Y;
				float laserArmTargetDist = (float)Math.Sqrt(laserArmTargetX * laserArmTargetX + laserArmTargetY * laserArmTargetY);
				base.NPC.rotation = (float)Math.Atan2(laserArmTargetY, laserArmTargetX) - (float)Math.PI / 2f;
				if (Main.netMode != 1 && !dontAttack)
				{
					base.NPC.localAI[0]++;
					if (!cannonAlive)
					{
						base.NPC.localAI[0]++;
					}
					if (!viceAlive)
					{
						base.NPC.localAI[0]++;
					}
					if (!sawAlive)
					{
						base.NPC.localAI[0]++;
					}
					if (base.NPC.localAI[0] >= 48f)
					{
						base.NPC.localAI[0] = 0f;
						int type = 100;
						laserArmTargetDist = 4f / laserArmTargetDist;
						laserArmTargetX *= laserArmTargetDist;
						laserArmTargetY *= laserArmTargetDist;
						Vector2 laserVelocity = default(Vector2);
						((Vector2)(ref laserVelocity))._002Ector(laserArmTargetX, laserArmTargetY);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), laserArmPosition + laserVelocity.SafeNormalize(Vector2.UnitY) * 100f, laserVelocity, type, LaserDamage.CalculateMechDamage(), 0f, Main.myPlayer, 1f);
					}
				}
			}
			else if (base.NPC.ai[2] == 1f)
			{
				base.NPC.ai[3]++;
				float timeLimit = 135f;
				float timeMult = 1.882075f;
				if (!cannonAlive)
				{
					timeLimit *= timeMult;
				}
				if (!viceAlive)
				{
					timeLimit *= timeMult;
				}
				if (!sawAlive)
				{
					timeLimit *= timeMult;
				}
				if (base.NPC.ai[3] >= timeLimit)
				{
					base.NPC.target = Main.npc[(int)base.NPC.ai[1]].target;
					base.NPC.localAI[0] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
				Vector2 laserRingArmPosition = base.NPC.Center;
				float laserRingTargetX = Main.player[base.NPC.target].Center.X - laserRingArmPosition.X;
				float laserRingTargetY = Main.player[base.NPC.target].Center.Y - laserRingArmPosition.Y;
				base.NPC.rotation = (float)Math.Atan2(laserRingTargetY, laserRingTargetX) - (float)Math.PI / 2f;
				if (Main.netMode != 1 && !dontAttack)
				{
					base.NPC.localAI[0]++;
					if (!cannonAlive)
					{
						base.NPC.localAI[0] += 0.5f;
					}
					if (!viceAlive)
					{
						base.NPC.localAI[0] += 0.5f;
					}
					if (!sawAlive)
					{
						base.NPC.localAI[0] += 0.5f;
					}
					if (base.NPC.localAI[0] >= 120f)
					{
						base.NPC.localAI[0] = 0f;
						int totalProjectiles = (death ? 24 : 16);
						float radians = (float)Math.PI * 2f / (float)totalProjectiles;
						int type2 = 100;
						float velocity = 3f;
						double angleA = (double)radians * 0.5;
						double angleB = (double)MathHelper.ToRadians(90f) - angleA;
						float laserVelocityX = (float)((double)velocity * Math.Sin(angleA) / Math.Sin(angleB));
						Vector2 spinningPoint = (normalLaserRotation ? new Vector2(0f, 0f - velocity) : new Vector2(0f - laserVelocityX, 0f - velocity));
						for (int k = 0; k < totalProjectiles; k++)
						{
							Vector2 laserFireDirection = spinningPoint.RotatedBy(radians * (float)k);
							int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + laserFireDirection.SafeNormalize(Vector2.UnitY) * 100f, laserFireDirection, type2, LaserDamage.CalculateMechDamage(), 0f, Main.myPlayer, 1f);
							Main.projectile[proj].timeLeft = 900;
						}
						base.NPC.localAI[1]++;
					}
				}
			}
			return false;
		}
	}

	public class PrimeCannonAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_065e: Unknown result type (might be due to invalid IL or missing references)
			//IL_067c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0933: Unknown result type (might be due to invalid IL or missing references)
			//IL_0938: Unknown result type (might be due to invalid IL or missing references)
			//IL_094b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0955: Unknown result type (might be due to invalid IL or missing references)
			//IL_0970: Unknown result type (might be due to invalid IL or missing references)
			//IL_097a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0744: Unknown result type (might be due to invalid IL or missing references)
			//IL_0749: Unknown result type (might be due to invalid IL or missing references)
			//IL_075c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0766: Unknown result type (might be due to invalid IL or missing references)
			//IL_0781: Unknown result type (might be due to invalid IL or missing references)
			//IL_078b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a46: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a51: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a88: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a93: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a98: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a9d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
			//IL_086b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0876: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_08e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0adf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aec: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b00: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b05: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b07: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b0c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b16: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b1b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b20: Unknown result type (might be due to invalid IL or missing references)
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
			}
			base.NPC.spriteDirection = -(int)base.NPC.ai[0];
			if (!Main.npc[(int)base.NPC.ai[1]].active || Main.npc[(int)base.NPC.ai[1]].aiStyle != 32)
			{
				base.NPC.ai[2] += 10f;
				if (base.NPC.ai[2] > 50f || !Main.dedServ)
				{
					base.NPC.life = -1;
					base.NPC.HitEffect();
					base.NPC.active = false;
				}
			}
			CalamityGlobalNPC.primeCannon = base.NPC.whoAmI;
			bool laserAlive = false;
			bool viceAlive = false;
			bool sawAlive = false;
			if (CalamityGlobalNPC.primeLaser != -1 && Main.npc[CalamityGlobalNPC.primeLaser].active)
			{
				laserAlive = true;
			}
			if (CalamityGlobalNPC.primeVice != -1 && Main.npc[CalamityGlobalNPC.primeVice].active)
			{
				viceAlive = true;
			}
			if (CalamityGlobalNPC.primeSaw != -1 && Main.npc[CalamityGlobalNPC.primeSaw].active)
			{
				sawAlive = true;
			}
			float timeToNotAttack = 180f;
			bool dontAttack = base.NPC.Calamity().newAI[2] < timeToNotAttack;
			if (dontAttack)
			{
				base.NPC.Calamity().newAI[2]++;
				if (base.NPC.Calamity().newAI[2] >= timeToNotAttack)
				{
					base.NPC.SyncExtraAI();
				}
			}
			bool fireSlower = false;
			if (laserAlive)
			{
				if (Main.npc[CalamityGlobalNPC.primeLaser].ai[2] == 1f)
				{
					fireSlower = true;
				}
			}
			else
			{
				fireSlower = base.NPC.ai[2] == 0f;
				if (fireSlower)
				{
					base.NPC.ai[3]++;
					if (!laserAlive)
					{
						base.NPC.ai[3]++;
					}
					if (!viceAlive)
					{
						base.NPC.ai[3]++;
					}
					if (!sawAlive)
					{
						base.NPC.ai[3]++;
					}
					if (base.NPC.ai[3] >= (death ? 200f : 800f))
					{
						base.NPC.target = Main.npc[(int)base.NPC.ai[1]].target;
						base.NPC.localAI[0] = 0f;
						base.NPC.ai[2] = 1f;
						fireSlower = false;
						base.NPC.ai[3] = 0f;
						base.NPC.netUpdate = true;
					}
				}
				else
				{
					base.NPC.ai[3]++;
					float timeLimit = 120f;
					float timeMult = 1.882075f;
					if (!laserAlive)
					{
						timeLimit *= timeMult;
					}
					if (!viceAlive)
					{
						timeLimit *= timeMult;
					}
					if (!sawAlive)
					{
						timeLimit *= timeMult;
					}
					if (base.NPC.ai[3] >= timeLimit)
					{
						base.NPC.target = Main.npc[(int)base.NPC.ai[1]].target;
						base.NPC.localAI[0] = 0f;
						base.NPC.ai[2] = 0f;
						fireSlower = true;
						base.NPC.ai[3] = 0f;
						base.NPC.netUpdate = true;
					}
				}
			}
			float acceleration = (death ? 0.385f : 0.25f);
			float accelerationMult = 1f;
			if (!laserAlive)
			{
				acceleration += 0.025f;
				accelerationMult += 0.5f;
			}
			if (!viceAlive)
			{
				acceleration += 0.025f;
			}
			if (!sawAlive)
			{
				acceleration += 0.025f;
			}
			if (death)
			{
				acceleration *= accelerationMult;
			}
			float topVelocity = acceleration * 100f;
			float deceleration = (death ? 0.6f : 0.8f);
			if (base.NPC.position.Y > Main.npc[(int)base.NPC.ai[1]].position.Y - 70f)
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y *= deceleration;
				}
				base.NPC.velocity.Y -= acceleration;
				if (base.NPC.velocity.Y > topVelocity)
				{
					base.NPC.velocity.Y = topVelocity;
				}
			}
			else if (base.NPC.position.Y < Main.npc[(int)base.NPC.ai[1]].position.Y - 100f)
			{
				if (base.NPC.velocity.Y < 0f)
				{
					base.NPC.velocity.Y *= deceleration;
				}
				base.NPC.velocity.Y += acceleration;
				if (base.NPC.velocity.Y < 0f - topVelocity)
				{
					base.NPC.velocity.Y = 0f - topVelocity;
				}
			}
			if (base.NPC.Center.X > Main.npc[(int)base.NPC.ai[1]].Center.X + 130f)
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X *= deceleration;
				}
				base.NPC.velocity.X -= acceleration;
				if (base.NPC.velocity.X > topVelocity)
				{
					base.NPC.velocity.X = topVelocity;
				}
			}
			if (base.NPC.Center.X < Main.npc[(int)base.NPC.ai[1]].Center.X + 160f)
			{
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X *= deceleration;
				}
				base.NPC.velocity.X += acceleration;
				if (base.NPC.velocity.X < 0f - topVelocity)
				{
					base.NPC.velocity.X = 0f - topVelocity;
				}
			}
			if (fireSlower)
			{
				if (Main.npc[(int)base.NPC.ai[1]].ai[1] == 3f && base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
				}
				Vector2 cannonArmPosition = base.NPC.Center;
				float cannonArmTargetX = Main.player[base.NPC.target].Center.X - cannonArmPosition.X;
				float cannonArmTargetY = Main.player[base.NPC.target].Center.Y - cannonArmPosition.Y;
				float cannonArmTargetDist = (float)Math.Sqrt(cannonArmTargetX * cannonArmTargetX + cannonArmTargetY * cannonArmTargetY);
				base.NPC.rotation = (float)Math.Atan2(cannonArmTargetY, cannonArmTargetX) - (float)Math.PI / 2f;
				if (Main.netMode != 1 && !dontAttack)
				{
					base.NPC.localAI[0]++;
					if (!laserAlive)
					{
						base.NPC.localAI[0]++;
					}
					if (!viceAlive)
					{
						base.NPC.localAI[0]++;
					}
					if (!sawAlive)
					{
						base.NPC.localAI[0]++;
					}
					if (base.NPC.localAI[0] >= 120f)
					{
						SoundEngine.PlaySound(in SoundID.Item62, base.NPC.Center);
						base.NPC.localAI[0] = 0f;
						int type = 303;
						cannonArmTargetDist = 10f / cannonArmTargetDist;
						cannonArmTargetX *= cannonArmTargetDist;
						cannonArmTargetY *= cannonArmTargetDist;
						Vector2 rocketVelocity = default(Vector2);
						((Vector2)(ref rocketVelocity))._002Ector(cannonArmTargetX, cannonArmTargetY);
						int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), cannonArmPosition + rocketVelocity.SafeNormalize(Vector2.UnitY) * 40f, rocketVelocity, type, RocketDamage.CalculateMechDamage(), 0f, Main.myPlayer, base.NPC.target, 2f);
						Main.projectile[proj].timeLeft = 540;
					}
				}
			}
			else
			{
				Vector2 cannonSpreadArmPosition = base.NPC.Center;
				float cannonSpreadArmTargetX = Main.player[base.NPC.target].Center.X - cannonSpreadArmPosition.X;
				float cannonSpreadArmTargetY = Main.player[base.NPC.target].Center.Y - cannonSpreadArmPosition.Y;
				base.NPC.rotation = (float)Math.Atan2(cannonSpreadArmTargetY, cannonSpreadArmTargetX) - (float)Math.PI / 2f;
				if (Main.netMode != 1 && !dontAttack)
				{
					base.NPC.localAI[0]++;
					if (!laserAlive)
					{
						base.NPC.localAI[0] += 0.5f;
					}
					if (!viceAlive)
					{
						base.NPC.localAI[0] += 0.5f;
					}
					if (!sawAlive)
					{
						base.NPC.localAI[0] += 0.5f;
					}
					if (base.NPC.localAI[0] >= 180f)
					{
						SoundEngine.PlaySound(in SoundID.Item62, base.NPC.Center);
						base.NPC.localAI[0] = 0f;
						int type2 = 303;
						float rocketSpeed = 10f;
						Vector2 cannonSpreadTargetDist = (Main.player[base.NPC.target].Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * rocketSpeed;
						int numProj = 3;
						float rotation = MathHelper.ToRadians(9f);
						for (int i = 0; i < numProj; i++)
						{
							Vector2 perturbedSpeed = cannonSpreadTargetDist.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)i / (float)(numProj - 1)));
							int proj2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + perturbedSpeed.SafeNormalize(Vector2.UnitY) * 40f, perturbedSpeed, type2, RocketDamage.CalculateMechDamage(), 0f, Main.myPlayer, base.NPC.target, 2f);
							Main.projectile[proj2].timeLeft = 600;
						}
					}
				}
			}
			return false;
		}
	}

	public class PrimeViceAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e58: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e5d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e73: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e91: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ebf: Unknown result type (might be due to invalid IL or missing references)
			//IL_10f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_110d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f7c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f9a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b1d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b4b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fb2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fd0: Unknown result type (might be due to invalid IL or missing references)
			//IL_102e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1033: Unknown result type (might be due to invalid IL or missing references)
			//IL_1046: Unknown result type (might be due to invalid IL or missing references)
			//IL_1050: Unknown result type (might be due to invalid IL or missing references)
			//IL_106b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1075: Unknown result type (might be due to invalid IL or missing references)
			//IL_0448: Unknown result type (might be due to invalid IL or missing references)
			//IL_0466: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c9d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cb5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cbf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cda: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ce4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_050b: Unknown result type (might be due to invalid IL or missing references)
			//IL_08b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0958: Unknown result type (might be due to invalid IL or missing references)
			//IL_0976: Unknown result type (might be due to invalid IL or missing references)
			//IL_09f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a14: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a32: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a60: Unknown result type (might be due to invalid IL or missing references)
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
			}
			base.NPC.spriteDirection = -(int)base.NPC.ai[0];
			Vector2 viceArmPosition = base.NPC.Center;
			float num = Main.npc[(int)base.NPC.ai[1]].Center.X - 200f * base.NPC.ai[0] - viceArmPosition.X;
			float viceArmIdleYPos = Main.npc[(int)base.NPC.ai[1]].Center.Y + 230f - viceArmPosition.Y;
			float viceArmIdleDistance = (float)Math.Sqrt(num * num + viceArmIdleYPos * viceArmIdleYPos);
			if (base.NPC.ai[2] != 99f)
			{
				if (viceArmIdleDistance > 800f)
				{
					base.NPC.ai[2] = 99f;
				}
			}
			else if (viceArmIdleDistance < 400f)
			{
				base.NPC.ai[2] = 0f;
			}
			if (!Main.npc[(int)base.NPC.ai[1]].active || Main.npc[(int)base.NPC.ai[1]].aiStyle != 32)
			{
				base.NPC.ai[2] += 10f;
				if (base.NPC.ai[2] > 50f || !Main.dedServ)
				{
					base.NPC.life = -1;
					base.NPC.HitEffect();
					base.NPC.active = false;
				}
			}
			CalamityGlobalNPC.primeVice = base.NPC.whoAmI;
			bool cannonAlive = false;
			bool laserAlive = false;
			bool sawAlive = false;
			if (CalamityGlobalNPC.primeCannon != -1 && Main.npc[CalamityGlobalNPC.primeCannon].active)
			{
				cannonAlive = true;
			}
			if (CalamityGlobalNPC.primeLaser != -1 && Main.npc[CalamityGlobalNPC.primeLaser].active)
			{
				laserAlive = true;
			}
			if (CalamityGlobalNPC.primeSaw != -1 && Main.npc[CalamityGlobalNPC.primeSaw].active)
			{
				sawAlive = true;
			}
			if (base.NPC.ai[2] == 99f)
			{
				float acceleration = (death ? 0.385f : 0.25f);
				float accelerationMult = 1f;
				if (!cannonAlive)
				{
					acceleration += 0.025f;
					accelerationMult += 0.5f;
				}
				if (!laserAlive)
				{
					acceleration += 0.025f;
					accelerationMult += 0.5f;
				}
				if (!sawAlive)
				{
					acceleration += 0.025f;
				}
				if (death)
				{
					acceleration *= accelerationMult;
				}
				float topVelocity = acceleration * 100f;
				float deceleration = (death ? 0.6f : 0.8f);
				if (base.NPC.position.Y > Main.npc[(int)base.NPC.ai[1]].position.Y + 20f)
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y *= deceleration;
					}
					base.NPC.velocity.Y -= acceleration;
					if (base.NPC.velocity.Y > topVelocity)
					{
						base.NPC.velocity.Y = topVelocity;
					}
				}
				else if (base.NPC.position.Y < Main.npc[(int)base.NPC.ai[1]].position.Y - 20f)
				{
					if (base.NPC.velocity.Y < 0f)
					{
						base.NPC.velocity.Y *= deceleration;
					}
					base.NPC.velocity.Y += acceleration;
					if (base.NPC.velocity.Y < 0f - topVelocity)
					{
						base.NPC.velocity.Y = 0f - topVelocity;
					}
				}
				if (base.NPC.Center.X > Main.npc[(int)base.NPC.ai[1]].Center.X + 20f)
				{
					if (base.NPC.velocity.X > 0f)
					{
						base.NPC.velocity.X *= deceleration;
					}
					base.NPC.velocity.X -= acceleration * 2f;
					if (base.NPC.velocity.X > topVelocity)
					{
						base.NPC.velocity.X = topVelocity;
					}
				}
				if (base.NPC.Center.X < Main.npc[(int)base.NPC.ai[1]].Center.X - 20f)
				{
					if (base.NPC.velocity.X < 0f)
					{
						base.NPC.velocity.X *= deceleration;
					}
					base.NPC.velocity.X += acceleration * 2f;
					if (base.NPC.velocity.X < 0f - topVelocity)
					{
						base.NPC.velocity.X = 0f - topVelocity;
					}
				}
			}
			else
			{
				if (base.NPC.ai[2] == 0f || base.NPC.ai[2] == 3f)
				{
					if (Main.npc[(int)base.NPC.ai[1]].ai[1] == 3f && base.NPC.timeLeft > 10)
					{
						base.NPC.timeLeft = 10;
					}
					base.NPC.ai[3]++;
					if (!cannonAlive)
					{
						base.NPC.ai[3]++;
					}
					if (!laserAlive)
					{
						base.NPC.ai[3]++;
					}
					if (!sawAlive)
					{
						base.NPC.ai[3]++;
					}
					if (base.NPC.ai[3] >= (death ? 150f : 600f))
					{
						base.NPC.target = Main.npc[(int)base.NPC.ai[1]].target;
						base.NPC.ai[2]++;
						base.NPC.ai[3] = 0f;
						base.NPC.netUpdate = true;
					}
					float acceleration2 = (death ? 0.385f : 0.25f);
					float accelerationMult2 = 1f;
					if (!cannonAlive)
					{
						acceleration2 += 0.025f;
						accelerationMult2 += 0.5f;
					}
					if (!laserAlive)
					{
						acceleration2 += 0.025f;
						accelerationMult2 += 0.5f;
					}
					if (!sawAlive)
					{
						acceleration2 += 0.025f;
					}
					if (death)
					{
						acceleration2 *= accelerationMult2;
					}
					float topVelocity2 = acceleration2 * 100f;
					float deceleration2 = (death ? 0.6f : 0.8f);
					if (base.NPC.position.Y > Main.npc[(int)base.NPC.ai[1]].position.Y + 100f)
					{
						if (base.NPC.velocity.Y > 0f)
						{
							base.NPC.velocity.Y *= deceleration2;
						}
						base.NPC.velocity.Y -= acceleration2;
						if (base.NPC.velocity.Y > topVelocity2)
						{
							base.NPC.velocity.Y = topVelocity2;
						}
					}
					else if (base.NPC.position.Y < Main.npc[(int)base.NPC.ai[1]].position.Y + 70f)
					{
						if (base.NPC.velocity.Y < 0f)
						{
							base.NPC.velocity.Y *= deceleration2;
						}
						base.NPC.velocity.Y += acceleration2;
						if (base.NPC.velocity.Y < 0f - topVelocity2)
						{
							base.NPC.velocity.Y = 0f - topVelocity2;
						}
					}
					if (base.NPC.Center.X > Main.npc[(int)base.NPC.ai[1]].Center.X + 160f)
					{
						if (base.NPC.velocity.X > 0f)
						{
							base.NPC.velocity.X *= deceleration2;
						}
						base.NPC.velocity.X -= acceleration2;
						if (base.NPC.velocity.X > topVelocity2)
						{
							base.NPC.velocity.X = topVelocity2;
						}
					}
					if (base.NPC.Center.X < Main.npc[(int)base.NPC.ai[1]].Center.X + 130f)
					{
						if (base.NPC.velocity.X < 0f)
						{
							base.NPC.velocity.X *= deceleration2;
						}
						base.NPC.velocity.X += acceleration2;
						if (base.NPC.velocity.X < 0f - topVelocity2)
						{
							base.NPC.velocity.X = 0f - topVelocity2;
						}
					}
					Vector2 viceArmReelbackCurrentPos = base.NPC.Center;
					float viceArmReelbackXDest = Main.npc[(int)base.NPC.ai[1]].Center.X - 200f * base.NPC.ai[0] - viceArmReelbackCurrentPos.X;
					float viceArmReelbackYDest = Main.npc[(int)base.NPC.ai[1]].position.Y + 230f - viceArmReelbackCurrentPos.Y;
					base.NPC.rotation = (float)Math.Atan2(viceArmReelbackYDest, viceArmReelbackXDest) + (float)Math.PI / 2f;
					return false;
				}
				if (base.NPC.ai[2] == 1f)
				{
					float deceleration3 = (death ? 0.75f : 0.8f);
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y *= deceleration3;
					}
					Vector2 viceArmChargePosition = base.NPC.Center;
					float viceArmChargeTargetX = Main.npc[(int)base.NPC.ai[1]].Center.X - 280f * base.NPC.ai[0] - viceArmChargePosition.X;
					float viceArmChargeTargetY = Main.npc[(int)base.NPC.ai[1]].position.Y + 230f - viceArmChargePosition.Y;
					base.NPC.rotation = (float)Math.Atan2(viceArmChargeTargetY, viceArmChargeTargetX) + (float)Math.PI / 2f;
					base.NPC.velocity.X = (base.NPC.velocity.X * 5f + Main.npc[(int)base.NPC.ai[1]].velocity.X) / 6f;
					base.NPC.velocity.X += 0.5f;
					base.NPC.velocity.Y -= 0.5f;
					if (base.NPC.velocity.Y < -12f)
					{
						base.NPC.velocity.Y = -12f;
					}
					if (base.NPC.position.Y < Main.npc[(int)base.NPC.ai[1]].position.Y - 280f)
					{
						float chargeVelocity = 16f;
						if (!cannonAlive)
						{
							chargeVelocity += 1.5f;
						}
						if (!laserAlive)
						{
							chargeVelocity += 1.5f;
						}
						if (!sawAlive)
						{
							chargeVelocity += 1.5f;
						}
						base.NPC.ai[2] = 2f;
						viceArmChargePosition = base.NPC.Center;
						viceArmChargeTargetX = Main.player[base.NPC.target].Center.X - viceArmChargePosition.X;
						viceArmChargeTargetY = Main.player[base.NPC.target].Center.Y - viceArmChargePosition.Y;
						float viceArmChargeTargetDist = (float)Math.Sqrt(viceArmChargeTargetX * viceArmChargeTargetX + viceArmChargeTargetY * viceArmChargeTargetY);
						viceArmChargeTargetDist = chargeVelocity / viceArmChargeTargetDist;
						base.NPC.velocity.X = viceArmChargeTargetX * viceArmChargeTargetDist;
						base.NPC.velocity.Y = viceArmChargeTargetY * viceArmChargeTargetDist;
						base.NPC.netUpdate = true;
					}
				}
				else if (base.NPC.ai[2] == 2f)
				{
					if (base.NPC.position.Y > Main.player[base.NPC.target].position.Y || base.NPC.velocity.Y < 0f)
					{
						float chargeAmt = 4f;
						if (!cannonAlive)
						{
							chargeAmt++;
						}
						if (!laserAlive)
						{
							chargeAmt++;
						}
						if (!sawAlive)
						{
							chargeAmt++;
						}
						if (base.NPC.ai[3] >= chargeAmt)
						{
							base.NPC.ai[2] = 3f;
							base.NPC.ai[3] = 0f;
							return false;
						}
						base.NPC.ai[2] = 1f;
						base.NPC.ai[3]++;
					}
				}
				else if (base.NPC.ai[2] == 4f)
				{
					Vector2 viceArmOtherChargePosition = base.NPC.Center;
					float viceArmOtherChargeTargetX = Main.npc[(int)base.NPC.ai[1]].Center.X - 200f * base.NPC.ai[0] - viceArmOtherChargePosition.X;
					float viceArmOtherChargeTargetY = Main.npc[(int)base.NPC.ai[1]].position.Y + 230f - viceArmOtherChargePosition.Y;
					base.NPC.rotation = (float)Math.Atan2(viceArmOtherChargeTargetY, viceArmOtherChargeTargetX) + (float)Math.PI / 2f;
					base.NPC.velocity.Y = (base.NPC.velocity.Y * 5f + Main.npc[(int)base.NPC.ai[1]].velocity.Y) / 6f;
					base.NPC.velocity.X += 0.5f;
					if (base.NPC.velocity.X > 12f)
					{
						base.NPC.velocity.X = 12f;
					}
					if (base.NPC.Center.X < Main.npc[(int)base.NPC.ai[1]].Center.X - 500f || base.NPC.Center.X > Main.npc[(int)base.NPC.ai[1]].Center.X + 500f)
					{
						float chargeVelocity2 = 14f;
						if (!cannonAlive)
						{
							chargeVelocity2++;
						}
						if (!laserAlive)
						{
							chargeVelocity2++;
						}
						if (!sawAlive)
						{
							chargeVelocity2++;
						}
						base.NPC.ai[2] = 5f;
						viceArmOtherChargePosition = base.NPC.Center;
						viceArmOtherChargeTargetX = Main.player[base.NPC.target].Center.X - viceArmOtherChargePosition.X;
						viceArmOtherChargeTargetY = Main.player[base.NPC.target].Center.Y - viceArmOtherChargePosition.Y;
						float viceArmOtherChargeTargetDist = (float)Math.Sqrt(viceArmOtherChargeTargetX * viceArmOtherChargeTargetX + viceArmOtherChargeTargetY * viceArmOtherChargeTargetY);
						viceArmOtherChargeTargetDist = chargeVelocity2 / viceArmOtherChargeTargetDist;
						base.NPC.velocity.X = viceArmOtherChargeTargetX * viceArmOtherChargeTargetDist;
						base.NPC.velocity.Y = viceArmOtherChargeTargetY * viceArmOtherChargeTargetDist;
						base.NPC.netUpdate = true;
					}
				}
				else if (base.NPC.ai[2] == 5f && base.NPC.Center.X < Main.player[base.NPC.target].Center.X - 100f)
				{
					float chargeAmt2 = 4f;
					if (!cannonAlive)
					{
						chargeAmt2++;
					}
					if (!laserAlive)
					{
						chargeAmt2++;
					}
					if (!sawAlive)
					{
						chargeAmt2++;
					}
					if (base.NPC.ai[3] >= chargeAmt2)
					{
						base.NPC.ai[2] = 0f;
						base.NPC.ai[3] = 0f;
						return false;
					}
					base.NPC.ai[2] = 4f;
					base.NPC.ai[3]++;
				}
			}
			return false;
		}
	}

	public class PrimeSawAI : VanillaAIOverride
	{
		public override bool AI(Mod mod)
		{
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_00da: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0acd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aeb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b19: Unknown result type (might be due to invalid IL or missing references)
			//IL_10e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1104: Unknown result type (might be due to invalid IL or missing references)
			//IL_112d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1148: Unknown result type (might be due to invalid IL or missing references)
			//IL_0da2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0da7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dba: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dc4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ddf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0de9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c2e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c33: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c46: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c50: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c6b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c75: Unknown result type (might be due to invalid IL or missing references)
			//IL_0448: Unknown result type (might be due to invalid IL or missing references)
			//IL_0466: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_050b: Unknown result type (might be due to invalid IL or missing references)
			//IL_08b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_095e: Unknown result type (might be due to invalid IL or missing references)
			//IL_097c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1025: Unknown result type (might be due to invalid IL or missing references)
			//IL_102a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1040: Unknown result type (might be due to invalid IL or missing references)
			//IL_105e: Unknown result type (might be due to invalid IL or missing references)
			//IL_108c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a05: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a0a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a20: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a6c: Unknown result type (might be due to invalid IL or missing references)
			bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
			}
			Vector2 sawArmLocation = base.NPC.Center;
			float num = Main.npc[(int)base.NPC.ai[1]].Center.X - 200f * base.NPC.ai[0] - sawArmLocation.X;
			float sawArmIdleYPos = Main.npc[(int)base.NPC.ai[1]].Center.Y + 230f - sawArmLocation.Y;
			float sawArmIdleDistance = (float)Math.Sqrt(num * num + sawArmIdleYPos * sawArmIdleYPos);
			if (base.NPC.ai[2] != 99f)
			{
				if (sawArmIdleDistance > 800f)
				{
					base.NPC.ai[2] = 99f;
				}
			}
			else if (sawArmIdleDistance < 400f)
			{
				base.NPC.ai[2] = 0f;
			}
			base.NPC.spriteDirection = -(int)base.NPC.ai[0];
			if (!Main.npc[(int)base.NPC.ai[1]].active || Main.npc[(int)base.NPC.ai[1]].aiStyle != 32)
			{
				base.NPC.ai[2] += 10f;
				if (base.NPC.ai[2] > 50f || !Main.dedServ)
				{
					base.NPC.life = -1;
					base.NPC.HitEffect();
					base.NPC.active = false;
				}
			}
			CalamityGlobalNPC.primeSaw = base.NPC.whoAmI;
			bool cannonAlive = false;
			bool laserAlive = false;
			bool viceAlive = false;
			if (CalamityGlobalNPC.primeCannon != -1 && Main.npc[CalamityGlobalNPC.primeCannon].active)
			{
				cannonAlive = true;
			}
			if (CalamityGlobalNPC.primeLaser != -1 && Main.npc[CalamityGlobalNPC.primeLaser].active)
			{
				laserAlive = true;
			}
			if (CalamityGlobalNPC.primeVice != -1 && Main.npc[CalamityGlobalNPC.primeVice].active)
			{
				viceAlive = true;
			}
			if (base.NPC.ai[2] == 99f)
			{
				float acceleration = (death ? 0.385f : 0.25f);
				float accelerationMult = 1f;
				if (!cannonAlive)
				{
					acceleration += 0.025f;
					accelerationMult += 0.5f;
				}
				if (!laserAlive)
				{
					acceleration += 0.025f;
					accelerationMult += 0.5f;
				}
				if (!viceAlive)
				{
					acceleration += 0.025f;
				}
				if (death)
				{
					acceleration *= accelerationMult;
				}
				float topVelocity = acceleration * 100f;
				float deceleration = (death ? 0.6f : 0.8f);
				if (base.NPC.position.Y > Main.npc[(int)base.NPC.ai[1]].position.Y + 20f)
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y *= deceleration;
					}
					base.NPC.velocity.Y -= acceleration;
					if (base.NPC.velocity.Y > topVelocity)
					{
						base.NPC.velocity.Y = topVelocity;
					}
				}
				else if (base.NPC.position.Y < Main.npc[(int)base.NPC.ai[1]].position.Y - 20f)
				{
					if (base.NPC.velocity.Y < 0f)
					{
						base.NPC.velocity.Y *= deceleration;
					}
					base.NPC.velocity.Y += acceleration;
					if (base.NPC.velocity.Y < 0f - topVelocity)
					{
						base.NPC.velocity.Y = 0f - topVelocity;
					}
				}
				if (base.NPC.Center.X > Main.npc[(int)base.NPC.ai[1]].Center.X + 20f)
				{
					if (base.NPC.velocity.X > 0f)
					{
						base.NPC.velocity.X *= deceleration;
					}
					base.NPC.velocity.X -= acceleration * 2f;
					if (base.NPC.velocity.X > topVelocity)
					{
						base.NPC.velocity.X = topVelocity;
					}
				}
				if (base.NPC.Center.X < Main.npc[(int)base.NPC.ai[1]].Center.X - 20f)
				{
					if (base.NPC.velocity.X < 0f)
					{
						base.NPC.velocity.X *= deceleration;
					}
					base.NPC.velocity.X += acceleration * 2f;
					if (base.NPC.velocity.X < 0f - topVelocity)
					{
						base.NPC.velocity.X = 0f - topVelocity;
					}
				}
			}
			else
			{
				if (base.NPC.ai[2] == 0f || base.NPC.ai[2] == 3f)
				{
					if (Main.npc[(int)base.NPC.ai[1]].ai[1] == 3f && base.NPC.timeLeft > 10)
					{
						base.NPC.timeLeft = 10;
					}
					base.NPC.ai[3]++;
					if (!cannonAlive)
					{
						base.NPC.ai[3]++;
					}
					if (!laserAlive)
					{
						base.NPC.ai[3]++;
					}
					if (!viceAlive)
					{
						base.NPC.ai[3]++;
					}
					if (base.NPC.ai[3] >= (death ? 90f : 180f))
					{
						base.NPC.target = Main.npc[(int)base.NPC.ai[1]].target;
						base.NPC.ai[2]++;
						base.NPC.ai[3] = 0f;
						base.NPC.netUpdate = true;
					}
					float acceleration2 = (death ? 0.385f : 0.25f);
					float accelerationMult2 = 1f;
					if (!cannonAlive)
					{
						acceleration2 += 0.025f;
						accelerationMult2 += 0.5f;
					}
					if (!laserAlive)
					{
						acceleration2 += 0.025f;
						accelerationMult2 += 0.5f;
					}
					if (!viceAlive)
					{
						acceleration2 += 0.025f;
					}
					if (death)
					{
						acceleration2 *= accelerationMult2;
					}
					float topVelocity2 = acceleration2 * 100f;
					float deceleration2 = (death ? 0.6f : 0.8f);
					if (base.NPC.position.Y > Main.npc[(int)base.NPC.ai[1]].position.Y + 100f)
					{
						if (base.NPC.velocity.Y > 0f)
						{
							base.NPC.velocity.Y *= deceleration2;
						}
						base.NPC.velocity.Y -= acceleration2;
						if (base.NPC.velocity.Y > topVelocity2)
						{
							base.NPC.velocity.Y = topVelocity2;
						}
					}
					else if (base.NPC.position.Y < Main.npc[(int)base.NPC.ai[1]].position.Y + 70f)
					{
						if (base.NPC.velocity.Y < 0f)
						{
							base.NPC.velocity.Y *= deceleration2;
						}
						base.NPC.velocity.Y += acceleration2;
						if (base.NPC.velocity.Y < 0f - topVelocity2)
						{
							base.NPC.velocity.Y = 0f - topVelocity2;
						}
					}
					if (base.NPC.Center.X > Main.npc[(int)base.NPC.ai[1]].Center.X - 130f)
					{
						if (base.NPC.velocity.X > 0f)
						{
							base.NPC.velocity.X *= deceleration2;
						}
						base.NPC.velocity.X -= acceleration2 * 1.5f;
						if (base.NPC.velocity.X > topVelocity2)
						{
							base.NPC.velocity.X = topVelocity2;
						}
					}
					if (base.NPC.Center.X < Main.npc[(int)base.NPC.ai[1]].Center.X - 160f)
					{
						if (base.NPC.velocity.X < 0f)
						{
							base.NPC.velocity.X *= deceleration2;
						}
						base.NPC.velocity.X += acceleration2 * 1.5f;
						if (base.NPC.velocity.X < 0f - topVelocity2)
						{
							base.NPC.velocity.X = 0f - topVelocity2;
						}
					}
					Vector2 sawArmReelbackCurrentPos = base.NPC.Center;
					float sawArmReelbackXDest = Main.npc[(int)base.NPC.ai[1]].Center.X - 200f * base.NPC.ai[0] - sawArmReelbackCurrentPos.X;
					float sawArmReelbackYDest = Main.npc[(int)base.NPC.ai[1]].position.Y + 230f - sawArmReelbackCurrentPos.Y;
					base.NPC.rotation = (float)Math.Atan2(sawArmReelbackYDest, sawArmReelbackXDest) + (float)Math.PI / 2f;
					return false;
				}
				if (base.NPC.ai[2] == 1f)
				{
					Vector2 sawArmChargePos = base.NPC.Center;
					float sawArmChargeTargetX = Main.npc[(int)base.NPC.ai[1]].Center.X - 200f * base.NPC.ai[0] - sawArmChargePos.X;
					float sawArmChargeTargetY = Main.npc[(int)base.NPC.ai[1]].position.Y + 230f - sawArmChargePos.Y;
					base.NPC.rotation = (float)Math.Atan2(sawArmChargeTargetY, sawArmChargeTargetX) + (float)Math.PI / 2f;
					float deceleration3 = (death ? 0.875f : 0.9f);
					base.NPC.velocity.X *= deceleration3;
					base.NPC.velocity.Y -= 0.5f;
					if (base.NPC.velocity.Y < -12f)
					{
						base.NPC.velocity.Y = -12f;
					}
					if (base.NPC.position.Y < Main.npc[(int)base.NPC.ai[1]].position.Y - 200f)
					{
						float chargeVelocity = 22f;
						if (!cannonAlive)
						{
							chargeVelocity += 1.5f;
						}
						if (!laserAlive)
						{
							chargeVelocity += 1.5f;
						}
						if (!viceAlive)
						{
							chargeVelocity += 1.5f;
						}
						base.NPC.ai[2] = 2f;
						sawArmChargePos = base.NPC.Center;
						sawArmChargeTargetX = Main.player[base.NPC.target].Center.X - sawArmChargePos.X;
						sawArmChargeTargetY = Main.player[base.NPC.target].Center.Y - sawArmChargePos.Y;
						float sawArmChargeTargetDist = (float)Math.Sqrt(sawArmChargeTargetX * sawArmChargeTargetX + sawArmChargeTargetY * sawArmChargeTargetY);
						sawArmChargeTargetDist = chargeVelocity / sawArmChargeTargetDist;
						base.NPC.velocity.X = sawArmChargeTargetX * sawArmChargeTargetDist;
						base.NPC.velocity.Y = sawArmChargeTargetY * sawArmChargeTargetDist;
						base.NPC.netUpdate = true;
					}
				}
				else if (base.NPC.ai[2] == 2f)
				{
					if (base.NPC.position.Y > Main.player[base.NPC.target].position.Y || base.NPC.velocity.Y < 0f)
					{
						base.NPC.ai[2] = 3f;
					}
				}
				else
				{
					if (base.NPC.ai[2] == 4f)
					{
						float chargeVelocity2 = 11f;
						if (!cannonAlive)
						{
							chargeVelocity2 += 1.5f;
						}
						if (!laserAlive)
						{
							chargeVelocity2 += 1.5f;
						}
						if (!viceAlive)
						{
							chargeVelocity2 += 1.5f;
						}
						if (death)
						{
							chargeVelocity2 *= 1.25f;
						}
						Vector2 sawArmOtherChargePos = base.NPC.Center;
						float sawArmOtherChargeTargetX = Main.player[base.NPC.target].Center.X - sawArmOtherChargePos.X;
						float sawArmOtherChargeTargetY = Main.player[base.NPC.target].Center.Y - sawArmOtherChargePos.Y;
						float sawArmOtherChargeTargetDist = (float)Math.Sqrt(sawArmOtherChargeTargetX * sawArmOtherChargeTargetX + sawArmOtherChargeTargetY * sawArmOtherChargeTargetY);
						sawArmOtherChargeTargetDist = chargeVelocity2 / sawArmOtherChargeTargetDist;
						sawArmOtherChargeTargetX *= sawArmOtherChargeTargetDist;
						sawArmOtherChargeTargetY *= sawArmOtherChargeTargetDist;
						float acceleration3 = (death ? 0.125f : 0.08f);
						float deceleration4 = (death ? 0.6f : 0.8f);
						if (base.NPC.velocity.X > sawArmOtherChargeTargetX)
						{
							if (base.NPC.velocity.X > 0f)
							{
								base.NPC.velocity.X *= deceleration4;
							}
							base.NPC.velocity.X -= acceleration3;
						}
						if (base.NPC.velocity.X < sawArmOtherChargeTargetX)
						{
							if (base.NPC.velocity.X < 0f)
							{
								base.NPC.velocity.X *= deceleration4;
							}
							base.NPC.velocity.X += acceleration3;
						}
						if (base.NPC.velocity.Y > sawArmOtherChargeTargetY)
						{
							if (base.NPC.velocity.Y > 0f)
							{
								base.NPC.velocity.Y *= deceleration4;
							}
							base.NPC.velocity.Y -= acceleration3;
						}
						if (base.NPC.velocity.Y < sawArmOtherChargeTargetY)
						{
							if (base.NPC.velocity.Y < 0f)
							{
								base.NPC.velocity.Y *= deceleration4;
							}
							base.NPC.velocity.Y += acceleration3;
						}
						base.NPC.ai[3]++;
						if (base.NPC.justHit)
						{
							base.NPC.ai[3] += 2f;
						}
						if (base.NPC.ai[3] >= 600f)
						{
							base.NPC.ai[2] = 0f;
							base.NPC.ai[3] = 0f;
							base.NPC.netUpdate = true;
						}
						sawArmOtherChargePos = base.NPC.Center;
						sawArmOtherChargeTargetX = Main.npc[(int)base.NPC.ai[1]].Center.X - 200f * base.NPC.ai[0] - sawArmOtherChargePos.X;
						sawArmOtherChargeTargetY = Main.npc[(int)base.NPC.ai[1]].position.Y + 230f - sawArmOtherChargePos.Y;
						base.NPC.rotation = (float)Math.Atan2(sawArmOtherChargeTargetY, sawArmOtherChargeTargetX) + (float)Math.PI / 2f;
						return false;
					}
					if (base.NPC.ai[2] == 5f && ((base.NPC.velocity.X > 0f && base.NPC.Center.X > Main.player[base.NPC.target].Center.X) || (base.NPC.velocity.X < 0f && base.NPC.Center.X < Main.player[base.NPC.target].Center.X)))
					{
						base.NPC.ai[2] = 0f;
					}
				}
			}
			return false;
		}
	}

	public static int SpinDamageMult = 2;

	public static int LaserDamage = 25;

	public static int SkullDamage = 22;

	public static int RocketDamage = 30;

	public override bool AI(Mod mod)
	{
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f54: Unknown result type (might be due to invalid IL or missing references)
		//IL_1295: Unknown result type (might be due to invalid IL or missing references)
		//IL_129a: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_12dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e51: Unknown result type (might be due to invalid IL or missing references)
		//IL_085a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0865: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d26: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_17fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1807: Unknown result type (might be due to invalid IL or missing references)
		//IL_1422: Unknown result type (might be due to invalid IL or missing references)
		//IL_142c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1457: Unknown result type (might be due to invalid IL or missing references)
		//IL_1461: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_14fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1504: Unknown result type (might be due to invalid IL or missing references)
		//IL_1509: Unknown result type (might be due to invalid IL or missing references)
		//IL_150e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1510: Unknown result type (might be due to invalid IL or missing references)
		//IL_1519: Unknown result type (might be due to invalid IL or missing references)
		//IL_1529: Unknown result type (might be due to invalid IL or missing references)
		//IL_152b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1532: Unknown result type (might be due to invalid IL or missing references)
		//IL_1537: Unknown result type (might be due to invalid IL or missing references)
		//IL_153c: Unknown result type (might be due to invalid IL or missing references)
		//IL_154a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1551: Unknown result type (might be due to invalid IL or missing references)
		//IL_1052: Unknown result type (might be due to invalid IL or missing references)
		//IL_1057: Unknown result type (might be due to invalid IL or missing references)
		//IL_106a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1074: Unknown result type (might be due to invalid IL or missing references)
		//IL_108f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1099: Unknown result type (might be due to invalid IL or missing references)
		//IL_0887: Unknown result type (might be due to invalid IL or missing references)
		//IL_0892: Unknown result type (might be due to invalid IL or missing references)
		//IL_1912: Unknown result type (might be due to invalid IL or missing references)
		//IL_192c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1932: Unknown result type (might be due to invalid IL or missing references)
		//IL_1934: Unknown result type (might be due to invalid IL or missing references)
		//IL_1939: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1959: Unknown result type (might be due to invalid IL or missing references)
		//IL_1963: Unknown result type (might be due to invalid IL or missing references)
		//IL_1968: Unknown result type (might be due to invalid IL or missing references)
		//IL_18bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dda: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ded: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e08: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e31: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e41: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e48: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_19bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a71: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b92: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b97: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ba1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a13: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a48: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1acb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1adc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1180: Unknown result type (might be due to invalid IL or missing references)
		//IL_1196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cab: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		if (base.NPC.ai[3] != 0f)
		{
			NPC.mechQueen = base.NPC.whoAmI;
		}
		if (calamityGlobalNPC.newAI[1] == 0f)
		{
			base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
			calamityGlobalNPC.newAI[1] = 1f;
			if (Main.netMode != 1)
			{
				int arm = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 128, base.NPC.whoAmI);
				Main.npc[arm].ai[0] = -1f;
				Main.npc[arm].ai[1] = base.NPC.whoAmI;
				Main.npc[arm].target = base.NPC.target;
				Main.npc[arm].netUpdate = true;
				arm = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 129, base.NPC.whoAmI);
				Main.npc[arm].ai[0] = 1f;
				Main.npc[arm].ai[1] = base.NPC.whoAmI;
				Main.npc[arm].target = base.NPC.target;
				Main.npc[arm].netUpdate = true;
				arm = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 130, base.NPC.whoAmI);
				Main.npc[arm].ai[0] = -1f;
				Main.npc[arm].ai[1] = base.NPC.whoAmI;
				Main.npc[arm].target = base.NPC.target;
				Main.npc[arm].ai[3] = 150f;
				Main.npc[arm].netUpdate = true;
				arm = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 131, base.NPC.whoAmI);
				Main.npc[arm].ai[0] = 1f;
				Main.npc[arm].ai[1] = base.NPC.whoAmI;
				Main.npc[arm].target = base.NPC.target;
				Main.npc[arm].netUpdate = true;
				Main.npc[arm].ai[3] = 150f;
			}
			base.NPC.netUpdate = true;
			base.NPC.SyncExtraAI();
		}
		bool cannonAlive = false;
		bool laserAlive = false;
		bool viceAlive = false;
		bool sawAlive = false;
		if (CalamityGlobalNPC.primeCannon != -1 && Main.npc[CalamityGlobalNPC.primeCannon].active)
		{
			cannonAlive = true;
		}
		if (CalamityGlobalNPC.primeLaser != -1 && Main.npc[CalamityGlobalNPC.primeLaser].active)
		{
			laserAlive = true;
		}
		if (CalamityGlobalNPC.primeVice != -1 && Main.npc[CalamityGlobalNPC.primeVice].active)
		{
			viceAlive = true;
		}
		if (CalamityGlobalNPC.primeSaw != -1 && Main.npc[CalamityGlobalNPC.primeSaw].active)
		{
			sawAlive = true;
		}
		bool allArmsDead = !cannonAlive && !laserAlive && !viceAlive && !sawAlive;
		base.NPC.chaseable = allArmsDead;
		base.NPC.defense = base.NPC.defDefense;
		base.NPC.damage = base.NPC.defDamage;
		bool phase2 = lifeRatio < 0.66f;
		bool phase3 = lifeRatio < 0.33f;
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
			if (Main.netMode != 1)
			{
				int healAmt = base.NPC.life - 300;
				if (healAmt < 0)
				{
					int absHeal = Math.Abs(healAmt);
					base.NPC.life += absHeal;
					base.NPC.HealEffect(absHeal);
					base.NPC.netUpdate = true;
				}
			}
			base.NPC.ai[1] = 2f;
			SoundEngine.PlaySound(in SoundID.ForceRoar, base.NPC.Center);
		}
		bool immuneToSlowingDebuffs = base.NPC.ai[1] == 5f;
		base.NPC.buffImmune[ModContent.BuffType<GlacialState>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TemporalSadness>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Eutrophication>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TimeDistortion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<GalvanicCorrosion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Vaporfied>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[149] = immuneToSlowingDebuffs;
		bool normalLaserRotation = base.NPC.localAI[1] % 2f == 0f;
		if (base.NPC.ai[1] == 0f || base.NPC.ai[1] == 4f)
		{
			if ((phase2 || Main.getGoodWorld) | allArmsDead)
			{
				base.NPC.ai[2] += (phase3 ? 1.5f : 1f);
				if (base.NPC.ai[2] >= 90f - (death ? (15f * (1f - lifeRatio)) : 0f))
				{
					bool shouldSpinAround = base.NPC.ai[1] == 4f && base.NPC.position.Y < Main.player[base.NPC.target].position.Y - 320f && Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) < 600f && Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 400f;
					if (shouldSpinAround || base.NPC.ai[1] != 4f)
					{
						if (shouldSpinAround)
						{
							base.NPC.localAI[3] = 300f;
							base.NPC.localAI[1] = 0f;
							base.NPC.SyncVanillaLocalAI();
						}
						base.NPC.ai[2] = 0f;
						base.NPC.ai[1] = (shouldSpinAround ? 5f : 1f);
						base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
						base.NPC.netUpdate = true;
					}
				}
			}
			if (NPC.IsMechQueenUp)
			{
				base.NPC.rotation = base.NPC.rotation.AngleLerp(base.NPC.velocity.X / 15f * 0.5f, 0.75f);
			}
			else
			{
				base.NPC.rotation = base.NPC.velocity.X / 15f;
			}
			float acceleration = (death ? (0.125f + 0.05f * (1f - lifeRatio)) : 0.1f);
			float accelerationMult = 1f;
			if (!cannonAlive)
			{
				acceleration += 0.0125f;
				accelerationMult += 0.25f;
			}
			if (!laserAlive)
			{
				acceleration += 0.0125f;
				accelerationMult += 0.25f;
			}
			if (!viceAlive)
			{
				acceleration += 0.0125f;
			}
			if (!sawAlive)
			{
				acceleration += 0.0125f;
			}
			if (death)
			{
				acceleration *= accelerationMult;
			}
			float topVelocity = acceleration * 100f;
			float deceleration = (death ? 0.7f : 0.85f);
			float headDecelerationUpDist = 0f;
			float headDecelerationDownDist = 0f;
			float headDecelerationHorizontalDist = 0f;
			int headHorizontalDirection = ((!(Main.player[base.NPC.target].Center.X < base.NPC.Center.X)) ? 1 : (-1));
			if (NPC.IsMechQueenUp)
			{
				headDecelerationHorizontalDist = -150f * (float)headHorizontalDirection;
				headDecelerationUpDist = -100f;
				headDecelerationDownDist = -100f;
			}
			if (base.NPC.position.Y > Main.player[base.NPC.target].position.Y - (320f + headDecelerationUpDist))
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y *= deceleration;
				}
				base.NPC.velocity.Y -= acceleration;
				if (base.NPC.velocity.Y > topVelocity)
				{
					base.NPC.velocity.Y = topVelocity;
				}
			}
			else if (base.NPC.position.Y < Main.player[base.NPC.target].position.Y - (360f + headDecelerationDownDist))
			{
				if (base.NPC.velocity.Y < 0f)
				{
					base.NPC.velocity.Y *= deceleration;
				}
				base.NPC.velocity.Y += acceleration;
				if (base.NPC.velocity.Y < 0f - topVelocity)
				{
					base.NPC.velocity.Y = 0f - topVelocity;
				}
			}
			if (base.NPC.Center.X > Main.player[base.NPC.target].Center.X + (400f + headDecelerationHorizontalDist))
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X *= deceleration;
				}
				base.NPC.velocity.X -= acceleration;
				if (base.NPC.velocity.X > topVelocity)
				{
					base.NPC.velocity.X = topVelocity;
				}
			}
			if (base.NPC.Center.X < Main.player[base.NPC.target].Center.X - (400f + headDecelerationHorizontalDist))
			{
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X *= deceleration;
				}
				base.NPC.velocity.X += acceleration;
				if (base.NPC.velocity.X < 0f - topVelocity)
				{
					base.NPC.velocity.X = 0f - topVelocity;
				}
			}
		}
		else
		{
			if (base.NPC.ai[1] == 1f)
			{
				base.NPC.defense = base.NPC.defDefense * 2;
				base.NPC.damage = base.NPC.defDamage * SpinDamageMult;
				calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = true;
				if (phase2 && Main.netMode != 1)
				{
					base.NPC.localAI[0]++;
					if (base.NPC.localAI[0] >= 45f)
					{
						base.NPC.localAI[0] = 0f;
						int totalProjectiles = (death ? 15 : 12);
						float radians = (float)Math.PI * 2f / (float)totalProjectiles;
						int type = 100;
						float velocity = 3f;
						double angleA = (double)radians * 0.5;
						double angleB = (double)MathHelper.ToRadians(90f) - angleA;
						float velocityX = (float)((double)velocity * Math.Sin(angleA) / Math.Sin(angleB));
						Vector2 spinningPoint = (normalLaserRotation ? new Vector2(0f, 0f - velocity) : new Vector2(0f - velocityX, 0f - velocity));
						for (int k = 0; k < totalProjectiles; k++)
						{
							Vector2 laserFireDirection = spinningPoint.RotatedBy(radians * (float)k);
							int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + laserFireDirection.SafeNormalize(Vector2.UnitY) * 100f, laserFireDirection, type, LaserDamage.CalculateMechDamage(), 0f, Main.myPlayer, 1f);
							Main.projectile[proj].timeLeft = 900;
						}
						base.NPC.localAI[1]++;
					}
				}
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] == 2f)
				{
					SoundEngine.PlaySound(in SoundID.ForceRoar, base.NPC.Center);
				}
				float phaseTimer = 240f;
				if (phase2 && !phase3)
				{
					phaseTimer += 60f;
				}
				if (base.NPC.ai[2] >= phaseTimer - (death ? (60f * (1f - lifeRatio)) : 0f))
				{
					base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
					base.NPC.ai[2] = 0f;
					base.NPC.ai[1] = 4f;
					base.NPC.localAI[0] = 0f;
				}
				if (NPC.IsMechQueenUp)
				{
					base.NPC.rotation = base.NPC.rotation.AngleLerp(base.NPC.velocity.X / 15f * 0.5f, 0.75f);
				}
				else
				{
					base.NPC.rotation += (float)base.NPC.direction * 0.3f;
				}
				Vector2 headPosition = base.NPC.Center;
				float headTargetX = Main.player[base.NPC.target].Center.X - headPosition.X;
				float headTargetY = Main.player[base.NPC.target].Center.Y - headPosition.Y;
				float headTargetDistance = (float)Math.Sqrt(headTargetX * headTargetX + headTargetY * headTargetY);
				float speed = (death ? 8f : 6f);
				if (phase2)
				{
					speed += 0.5f;
				}
				if (phase3)
				{
					speed += 0.5f;
				}
				if (headTargetDistance > 150f)
				{
					float baseDistanceVelocityMult = 1f + MathHelper.Clamp((headTargetDistance - 150f) * 0.0015f, 0.05f, 1.5f);
					speed *= baseDistanceVelocityMult;
				}
				if (NPC.IsMechQueenUp)
				{
					float mechdusaSpeedMult = (NPC.npcsFoundForCheckActive[135] ? 0.6f : 0.75f);
					speed *= mechdusaSpeedMult;
				}
				headTargetDistance = speed / headTargetDistance;
				base.NPC.velocity.X = headTargetX * headTargetDistance;
				base.NPC.velocity.Y = headTargetY * headTargetDistance;
				if (NPC.IsMechQueenUp)
				{
					float mechdusaAccelMult = Vector2.Distance(base.NPC.Center, Main.player[base.NPC.target].Center);
					if (mechdusaAccelMult < 0.1f)
					{
						mechdusaAccelMult = 0f;
					}
					if (mechdusaAccelMult < speed)
					{
						base.NPC.velocity = base.NPC.velocity.SafeNormalize(Vector2.Zero) * mechdusaAccelMult;
					}
				}
			}
			if (base.NPC.ai[1] == 2f)
			{
				base.NPC.damage = 1000;
				calamityGlobalNPC.DR = 0.9999f;
				calamityGlobalNPC.unbreakableDR = true;
				calamityGlobalNPC.CurrentlyEnraged = true;
				calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = true;
				if (NPC.IsMechQueenUp)
				{
					base.NPC.rotation = base.NPC.rotation.AngleLerp(base.NPC.velocity.X / 15f * 0.5f, 0.75f);
				}
				else
				{
					base.NPC.rotation += (float)base.NPC.direction * 0.3f;
				}
				Vector2 enragedHeadPosition = base.NPC.Center;
				float enragedHeadTargetX = Main.player[base.NPC.target].Center.X - enragedHeadPosition.X;
				float enragedHeadTargetY = Main.player[base.NPC.target].Center.Y - enragedHeadPosition.Y;
				float enragedHeadTargetDist = (float)Math.Sqrt(enragedHeadTargetX * enragedHeadTargetX + enragedHeadTargetY * enragedHeadTargetY);
				float enragedHeadSpeed = 10f;
				enragedHeadSpeed += enragedHeadTargetDist / 100f;
				if (enragedHeadSpeed < 8f)
				{
					enragedHeadSpeed = 8f;
				}
				if (enragedHeadSpeed > 32f)
				{
					enragedHeadSpeed = 32f;
				}
				enragedHeadTargetDist = enragedHeadSpeed / enragedHeadTargetDist;
				base.NPC.velocity.X = enragedHeadTargetX * enragedHeadTargetDist;
				base.NPC.velocity.Y = enragedHeadTargetY * enragedHeadTargetDist;
				if (Main.netMode != 1)
				{
					base.NPC.localAI[0]++;
					if (base.NPC.localAI[0] >= 60f)
					{
						base.NPC.localAI[0] = 0f;
						Vector2 headCenter = base.NPC.Center;
						if (Collision.CanHit(headCenter, 1, 1, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
						{
							enragedHeadSpeed = 7f;
							float enragedHeadSkullTargetX = Main.player[base.NPC.target].Center.X - headCenter.X + (float)Main.rand.Next(-20, 21);
							float enragedHeadSkullTargetY = Main.player[base.NPC.target].Center.Y - headCenter.Y + (float)Main.rand.Next(-20, 21);
							float enragedHeadSkullTargetDist = (float)Math.Sqrt(enragedHeadSkullTargetX * enragedHeadSkullTargetX + enragedHeadSkullTargetY * enragedHeadSkullTargetY);
							enragedHeadSkullTargetDist = enragedHeadSpeed / enragedHeadSkullTargetDist;
							enragedHeadSkullTargetX *= enragedHeadSkullTargetDist;
							enragedHeadSkullTargetY *= enragedHeadSkullTargetDist;
							Vector2 value = Utils.SafeNormalize(new Vector2(enragedHeadSkullTargetX * 1f + (float)Main.rand.Next(-50, 51) * 0.01f, enragedHeadSkullTargetY * 1f + (float)Main.rand.Next(-50, 51) * 0.01f), Vector2.UnitY);
							value *= enragedHeadSpeed;
							value += base.NPC.velocity;
							enragedHeadSkullTargetX = value.X;
							enragedHeadSkullTargetY = value.Y;
							int type2 = 270;
							headCenter += value * 5f;
							int enragedSkulls = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), headCenter.X, headCenter.Y, enragedHeadSkullTargetX, enragedHeadSkullTargetY, type2, 250, 0f, Main.myPlayer, -3f);
							Main.projectile[enragedSkulls].timeLeft = 300;
						}
					}
				}
			}
			if (base.NPC.ai[1] == 3f)
			{
				if (NPC.IsMechQueenUp)
				{
					int mechdusaBossDespawning = NPC.FindFirstNPC(125);
					if (mechdusaBossDespawning >= 0)
					{
						Main.npc[mechdusaBossDespawning].EncourageDespawn(5);
					}
					mechdusaBossDespawning = NPC.FindFirstNPC(126);
					if (mechdusaBossDespawning >= 0)
					{
						Main.npc[mechdusaBossDespawning].EncourageDespawn(5);
					}
					if (!NPC.AnyNPCs(125) && !NPC.AnyNPCs(126))
					{
						mechdusaBossDespawning = NPC.FindFirstNPC(134);
						if (mechdusaBossDespawning >= 0)
						{
							Main.npc[mechdusaBossDespawning].Transform(136);
						}
						base.NPC.EncourageDespawn(5);
					}
					base.NPC.velocity.Y += 0.1f;
					if (base.NPC.velocity.Y < 0f)
					{
						base.NPC.velocity.Y *= 0.95f;
					}
					base.NPC.velocity.X *= 0.95f;
					if (base.NPC.velocity.Y > 13f)
					{
						base.NPC.velocity.Y = 13f;
					}
				}
				else
				{
					base.NPC.velocity.Y += 0.1f;
					if (base.NPC.velocity.Y < 0f)
					{
						base.NPC.velocity.Y *= 0.9f;
					}
					base.NPC.velocity.X *= 0.9f;
					if (base.NPC.timeLeft > 500)
					{
						base.NPC.timeLeft = 500;
					}
				}
			}
			if (base.NPC.ai[1] == 5f)
			{
				base.NPC.ai[2]++;
				base.NPC.rotation = base.NPC.velocity.X / 50f;
				float skullSpawnDivisor = (death ? (15f - (float)Math.Round(3f * (1f - lifeRatio))) : 15f);
				float totalSkulls = 12f;
				int skullSpread = (death ? 125 : 100);
				float spinVelocity = 30f;
				if (base.NPC.ai[2] == 2f)
				{
					SoundEngine.PlaySound(in SoundID.ForceRoar, base.NPC.Center);
					if (Main.player[base.NPC.target].velocity.X > 0f)
					{
						calamityGlobalNPC.newAI[0] = 1f;
					}
					else if (Main.player[base.NPC.target].velocity.X < 0f)
					{
						calamityGlobalNPC.newAI[0] = -1f;
					}
					else
					{
						calamityGlobalNPC.newAI[0] = Main.player[base.NPC.target].direction;
					}
					base.NPC.velocity.X = (float)Math.PI * base.NPC.localAI[3] / spinVelocity;
					NPC nPC = base.NPC;
					nPC.velocity *= 0f - calamityGlobalNPC.newAI[0];
					base.NPC.SyncExtraAI();
					base.NPC.netUpdate = true;
				}
				else if (base.NPC.ai[2] > 2f)
				{
					base.NPC.velocity = base.NPC.velocity.RotatedBy((float)Math.PI / spinVelocity * (0f - calamityGlobalNPC.newAI[0]));
					if (base.NPC.ai[2] == 3f)
					{
						NPC nPC2 = base.NPC;
						nPC2.velocity *= 0.6f;
					}
					if (base.NPC.ai[2] % skullSpawnDivisor == 0f)
					{
						base.NPC.localAI[0]++;
						if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 96f && Main.netMode != 1)
						{
							Vector2 headCenter2 = base.NPC.Center;
							float enragedHeadSpeed2 = (death ? (5f + (1f - lifeRatio)) : 4f);
							float enragedHeadSkullTargetX2 = Main.player[base.NPC.target].Center.X - headCenter2.X + (float)Main.rand.Next(-20, 21);
							float enragedHeadSkullTargetY2 = Main.player[base.NPC.target].Center.Y - headCenter2.Y + (float)Main.rand.Next(-20, 21);
							float enragedHeadSkullTargetDist2 = (float)Math.Sqrt(enragedHeadSkullTargetX2 * enragedHeadSkullTargetX2 + enragedHeadSkullTargetY2 * enragedHeadSkullTargetY2);
							enragedHeadSkullTargetDist2 = enragedHeadSpeed2 / enragedHeadSkullTargetDist2;
							enragedHeadSkullTargetX2 *= enragedHeadSkullTargetDist2;
							enragedHeadSkullTargetY2 *= enragedHeadSkullTargetDist2;
							Vector2 val = Utils.SafeNormalize(new Vector2(enragedHeadSkullTargetX2 + (float)Main.rand.Next(-skullSpread, skullSpread + 1) * 0.01f, enragedHeadSkullTargetY2 + (float)Main.rand.Next(-skullSpread, skullSpread + 1) * 0.01f), Vector2.UnitY) * enragedHeadSpeed2;
							enragedHeadSkullTargetX2 = val.X;
							enragedHeadSkullTargetY2 = val.Y;
							int type3 = 270;
							int enragedSkulls2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), headCenter2.X, headCenter2.Y + 30f, enragedHeadSkullTargetX2, enragedHeadSkullTargetY2, type3, SkullDamage.CalculateMechDamage(), 0f, Main.myPlayer, -3f);
							Main.projectile[enragedSkulls2].timeLeft = 480;
							Main.projectile[enragedSkulls2].tileCollide = false;
						}
						if (base.NPC.localAI[0] >= totalSkulls && Main.netMode != 1)
						{
							base.NPC.velocity = base.NPC.velocity.SafeNormalize(Vector2.UnitY);
							base.NPC.ai[1] = (phase3 ? 6f : 1f);
							base.NPC.ai[2] = 0f;
							base.NPC.localAI[3] = 0f;
							base.NPC.localAI[0] = 0f;
							calamityGlobalNPC.newAI[0] = 0f;
							base.NPC.SyncVanillaLocalAI();
							base.NPC.SyncExtraAI();
							base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
							base.NPC.netUpdate = true;
						}
					}
				}
			}
			if (base.NPC.ai[1] == 6f)
			{
				base.NPC.rotation = base.NPC.velocity.X / 15f;
				float flightVelocity = (death ? 25f : 18f);
				float flightAcceleration = (death ? 0.96f : 0.6f);
				Vector2 destination = default(Vector2);
				((Vector2)(ref destination))._002Ector(Main.player[base.NPC.target].Center.X, Main.player[base.NPC.target].Center.Y - 420f);
				base.NPC.SimpleFlyMovement((destination - base.NPC.Center).SafeNormalize(Vector2.UnitY) * flightVelocity, flightAcceleration);
				base.NPC.localAI[3]++;
				if (Vector2.Distance(base.NPC.Center, destination) < 80f || base.NPC.ai[2] > 0f || base.NPC.localAI[3] > 120f)
				{
					float missileSpawnDivisor = 12f;
					float totalMissiles = 10f;
					base.NPC.ai[2]++;
					if (base.NPC.ai[2] % missileSpawnDivisor == 0f)
					{
						base.NPC.localAI[0]++;
						if (Main.netMode != 1)
						{
							Vector2 velocity2 = (-Vector2.UnitY * 3f).RotatedByRandom(0.39269909262657166);
							int type4 = 303;
							int proj2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X + (float)Main.rand.Next(base.NPC.width / 2), base.NPC.Center.Y + 30f, velocity2.X, velocity2.Y, type4, RocketDamage.CalculateMechDamage(), 0f, Main.myPlayer, base.NPC.target, 1f);
							Main.projectile[proj2].timeLeft = 540;
						}
						SoundEngine.PlaySound(in SoundID.Item62, base.NPC.Center);
						if (base.NPC.localAI[0] >= totalMissiles)
						{
							base.NPC.ai[1] = 0f;
							base.NPC.ai[2] = 0f;
							base.NPC.localAI[3] = 0f;
							calamityGlobalNPC.newAI[0] = 0f;
							base.NPC.localAI[0] = 0f;
							base.NPC.SyncVanillaLocalAI();
							base.NPC.SyncExtraAI();
							base.NPC.CalamityTargeting(CalamityTargetingParameters.BossDefaults);
							base.NPC.netUpdate = true;
						}
					}
				}
			}
		}
		return false;
	}

	public override bool PreDraw(Mod mod, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.IsMechQueenUp)
		{
			return true;
		}
		CalamityGlobalNPC calNPC = base.NPC.GetGlobalNPC<CalamityGlobalNPC>();
		int frameHeight = TextureAssets.Npc[base.NPC.type].Value.Height / Main.npcFrameCount[base.NPC.type];
		if (base.NPC.ai[1] == 0f || base.NPC.ai[1] == 4f)
		{
			calNPC.newAI[2]++;
			if (calNPC.newAI[2] >= 12f)
			{
				calNPC.newAI[2] = 0f;
				calNPC.newAI[3] += frameHeight;
				if (calNPC.newAI[3] / (float)frameHeight >= 2f)
				{
					calNPC.newAI[3] = 0f;
				}
			}
		}
		else if (base.NPC.ai[1] == 5f || base.NPC.ai[1] == 6f)
		{
			calNPC.newAI[2] = 0f;
			calNPC.newAI[3] = frameHeight;
		}
		else
		{
			calNPC.newAI[2] = 0f;
			calNPC.newAI[3] = frameHeight * 2;
		}
		base.NPC.frame.Y = (int)calNPC.newAI[3];
		return true;
	}
}
