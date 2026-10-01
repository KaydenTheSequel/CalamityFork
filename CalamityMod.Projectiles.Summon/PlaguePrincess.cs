using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Summon;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PlaguePrincess : ModProjectile, ILocalizedModType, IModType
{
	public enum ViriliAIState
	{
		HoverNearOwner,
		ChargeAtEnemies,
		BombardEnemiesWithRockets,
		SummonPlagueBeesOnEnemies
	}

	public bool UseAfterimages;

	public new string LocalizationCategory => "Projectiles.Summon";

	public ViriliAIState CurrentState
	{
		get
		{
			return (ViriliAIState)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = (float)value;
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float AITimer => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 96;
		base.Projectile.height = 116;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 3f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		HandleMinionBools();
		DecideFrames();
		if (Owner.strongBees)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 1.167f);
		}
		UseAfterimages = false;
		base.Projectile.MaxUpdates = 1;
		base.Projectile.localNPCHitCooldown = 10;
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1300f, Owner);
		switch (CurrentState)
		{
		case ViriliAIState.HoverNearOwner:
			DoBehavior_HoverNearOwner(potentialTarget);
			break;
		case ViriliAIState.ChargeAtEnemies:
			DoBehavior_ChargeAtEnemies(potentialTarget);
			break;
		case ViriliAIState.BombardEnemiesWithRockets:
			DoBehavior_BombardEnemiesWithRockets(potentialTarget);
			break;
		case ViriliAIState.SummonPlagueBeesOnEnemies:
			DoBehavior_SummonPlagueBeesOnEnemies(potentialTarget);
			break;
		}
		AITimer++;
	}

	public void HandleMinionBools()
	{
		Owner.AddBuff(ModContent.BuffType<ViriliBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<PlaguePrincess>())
		{
			if (Owner.dead)
			{
				Owner.Calamity().virili = false;
			}
			if (Owner.Calamity().virili)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public void DecideFrames()
	{
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 6 % Main.projFrames[base.Type];
	}

	public void DoBehavior_HoverNearOwner(NPC potentialTarget)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.WithinRange(Owner.Center, 160f))
		{
			base.Projectile.velocity = (base.Projectile.velocity * 34f + base.Projectile.SafeDirectionTo(Owner.Center) * 17f) / 35f;
			if (!base.Projectile.WithinRange(Owner.Center, 2500f))
			{
				base.Projectile.Center = Owner.Center;
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.3f;
				base.Projectile.netUpdate = true;
			}
			if (Math.Abs(base.Projectile.velocity.X) > 0.2f)
			{
				base.Projectile.spriteDirection = Math.Sign(base.Projectile.velocity.X);
			}
			if (potentialTarget != null)
			{
				CurrentState = ViriliAIState.ChargeAtEnemies;
				AITimer = 0f;
				base.Projectile.netUpdate = true;
			}
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(0f, 0.1f);
		}
	}

	public void DoBehavior_ChargeAtEnemies(NPC target)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		int hoverTime = 18;
		int chargeTime = 16;
		int slowdownTime = 15;
		int chargeCount = 6;
		float hoverSpeed = 17f;
		if (target == null)
		{
			ReturnToIdleState();
			return;
		}
		base.Projectile.MaxUpdates = 2;
		base.Projectile.localNPCHitCooldown = 2;
		float wrappedAttackTimer = AITimer % (float)(hoverTime + chargeTime + slowdownTime);
		if (wrappedAttackTimer < (float)hoverTime)
		{
			base.Projectile.spriteDirection = (target.Center.X > base.Projectile.Center.X).ToDirectionInt();
			HoverToPosition(target.Center + new Vector2((float)base.Projectile.spriteDirection * -270f, -150f), hoverSpeed);
		}
		else
		{
			UseAfterimages = true;
			if (wrappedAttackTimer < (float)(hoverTime + chargeTime))
			{
				if (wrappedAttackTimer % 2f == 1f && wrappedAttackTimer >= (float)hoverTime + 3f)
				{
					Vector2 particleVelocity = -base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * Main.rand.NextFloat(6f, 10f);
					GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center + Main.rand.NextVector2Circular(50f, 50f) + base.Projectile.velocity * 5f, particleVelocity, Main.rand.NextFloat(0.45f, 0.87f), Color.ForestGreen, 30, 3.4f, 4.5f));
				}
				if (((Vector2)(ref base.Projectile.velocity)).Length() < 40f)
				{
					Projectile projectile = base.Projectile;
					projectile.velocity *= 1.1f;
				}
			}
		}
		if (wrappedAttackTimer == (float)hoverTime)
		{
			SoundEngine.PlaySound(in CommonCalamitySounds.ELRFireSound, base.Projectile.Center);
			base.Projectile.velocity = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, target, 22f, 8);
			base.Projectile.netUpdate = true;
		}
		if (wrappedAttackTimer >= (float)(hoverTime + chargeTime))
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.825f;
		}
		base.Projectile.rotation = base.Projectile.velocity.X * 0.014f;
		if (AITimer >= (float)((hoverTime + chargeTime + slowdownTime) * chargeCount))
		{
			AITimer = 0f;
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 0.3f;
			CurrentState = ViriliAIState.BombardEnemiesWithRockets;
			base.Projectile.netUpdate = true;
		}
	}

	public void DoBehavior_BombardEnemiesWithRockets(NPC target)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		int hoverTime = 50;
		int chargeTime = 50;
		int chargeCount = 6;
		float hoverSpeed = 17f;
		float rocketShootSpeed = 7f;
		if (target == null)
		{
			ReturnToIdleState();
			return;
		}
		base.Projectile.MaxUpdates = 2;
		float wrappedAttackTimer = AITimer % (float)(hoverTime + chargeTime);
		if (wrappedAttackTimer < (float)hoverTime)
		{
			base.Projectile.spriteDirection = (target.Center.X > base.Projectile.Center.X).ToDirectionInt();
			Vector2 hoverDestination = target.Center + new Vector2((float)base.Projectile.spriteDirection * -480f, -280f);
			HoverToPosition(hoverDestination, hoverSpeed);
			if (base.Projectile.WithinRange(hoverDestination, 150f))
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.85f;
				base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, hoverDestination, 0.03f);
			}
		}
		else
		{
			UseAfterimages = true;
			if (wrappedAttackTimer < (float)(hoverTime + chargeTime))
			{
				if (wrappedAttackTimer % 2f == 1f && wrappedAttackTimer >= (float)hoverTime + 3f)
				{
					Vector2 particleVelocity = -base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * Main.rand.NextFloat(6f, 10f);
					GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center + Main.rand.NextVector2Circular(50f, 50f) + base.Projectile.velocity * 5f, particleVelocity, Main.rand.NextFloat(0.45f, 0.87f), Color.ForestGreen, 30, 3.4f, 4.5f));
				}
				if (Main.myPlayer == base.Projectile.owner && wrappedAttackTimer % 6f == 5f)
				{
					Vector2 rocketSpawnPosition = base.Projectile.Center + Vector2.UnitY * base.Projectile.scale * 48f;
					Vector2 rocketVelocity = (target.Center - rocketSpawnPosition).SafeNormalize(Vector2.UnitY) * rocketShootSpeed;
					int rocket = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, rocketVelocity, ModContent.ProjectileType<MK2RocketHoming>(), (int)((float)base.Projectile.damage * 0.7f), 3f, base.Projectile.owner);
					if (Main.projectile.IndexInRange(rocket))
					{
						Main.projectile[rocket].originalDamage = (int)((float)base.Projectile.originalDamage * 0.7f);
					}
				}
				if (((Vector2)(ref base.Projectile.velocity)).Length() < 22f)
				{
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 1.1f;
				}
			}
		}
		if (wrappedAttackTimer == (float)hoverTime)
		{
			SoundEngine.PlaySound(in PlaguebringerGoliath.BarrageLaunchSound, base.Projectile.Center);
			base.Projectile.velocity = Vector2.UnitX * 22f * (float)base.Projectile.spriteDirection * 0.55f;
			base.Projectile.netUpdate = true;
		}
		if (AITimer >= (float)((hoverTime + chargeTime) * chargeCount))
		{
			AITimer = 0f;
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 0.3f;
			CurrentState = ViriliAIState.SummonPlagueBeesOnEnemies;
			base.Projectile.netUpdate = true;
		}
	}

	public void DoBehavior_SummonPlagueBeesOnEnemies(NPC target)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		int shootTime = 300;
		float hoverSpeed = 23f;
		if (target == null)
		{
			ReturnToIdleState();
			return;
		}
		Vector2 hoverDestination = target.Center - Vector2.UnitY * 350f;
		HoverToPosition(hoverDestination, hoverSpeed);
		if (base.Projectile.WithinRange(hoverDestination, 240f))
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.7f;
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, hoverDestination, 0.04f);
			if (Main.myPlayer == base.Projectile.owner && AITimer % 22f == 21f)
			{
				int num = ModContent.ProjectileType<PlagueBeeSmall>();
				int bigBee = ModContent.ProjectileType<BabyPlaguebringer>();
				int projType = num;
				if (Owner.ownedProjectileCounts[bigBee] <= 0 && Main.rand.NextBool())
				{
					projType = bigBee;
				}
				if (Main.myPlayer == base.Projectile.owner && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, target.Center, 0, 0))
				{
					int beeCount = ((projType == bigBee) ? 1 : 4);
					for (int i = 0; i < beeCount; i++)
					{
						Vector2 beeVelocity = base.Projectile.SafeDirectionTo(target.Center) * 6f;
						int bee = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, beeVelocity, projType, (int)((float)base.Projectile.damage * 0.65f), 0f, base.Projectile.owner);
						if (Main.projectile.IndexInRange(bee) && projType == bigBee)
						{
							Main.projectile[bee].frame = 2;
						}
						base.Projectile.netUpdate = true;
					}
				}
			}
		}
		if (AITimer >= (float)shootTime)
		{
			AITimer = 0f;
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.3f;
			CurrentState = ViriliAIState.ChargeAtEnemies;
			base.Projectile.netUpdate = true;
		}
	}

	public void HoverToPosition(Vector2 hoverDestination, float hoverSpeed)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		Vector2 baseHoverVelocity = base.Projectile.SafeDirectionTo(hoverDestination) * hoverSpeed;
		if (!base.Projectile.WithinRange(hoverDestination, 150f))
		{
			float hyperspeedInterpolant = Utils.GetLerpValue(base.Projectile.Distance(hoverDestination), 500f, 960f, clamped: true);
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, Vector2.Lerp(baseHoverVelocity * 1.4f, (hoverDestination - base.Projectile.Center) * 0.1f, hyperspeedInterpolant), 0.2f);
		}
		else
		{
			base.Projectile.velocity = (base.Projectile.velocity * 29f + baseHoverVelocity) / 30f;
			base.Projectile.velocity = base.Projectile.velocity.MoveTowards(baseHoverVelocity, hoverSpeed / 11f);
		}
	}

	public void ReturnToIdleState()
	{
		CurrentState = ViriliAIState.HoverNearOwner;
		base.Projectile.netUpdate = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection == 1);
		if (UseAfterimages)
		{
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				Color forestGreen = Color.ForestGreen;
				((Color)(ref forestGreen)).A = 25;
				Color afterimageDrawColor = forestGreen * base.Projectile.Opacity * (1f - (float)i / (float)base.Projectile.oldPos.Length);
				Vector2 afterimageDrawPosition = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
				Main.EntitySpriteDraw(texture, afterimageDrawPosition, frame, afterimageDrawColor, base.Projectile.rotation, origin, base.Projectile.scale, direction);
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, direction);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 180);
	}

	public override bool MinionContactDamage()
	{
		return CurrentState == ViriliAIState.ChargeAtEnemies;
	}
}
