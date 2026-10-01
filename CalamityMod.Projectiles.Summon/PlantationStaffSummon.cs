using System;
using System.IO;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PlantationStaffSummon : ModProjectile, ILocalizedModType, IModType
{
	public enum AIState
	{
		Idle,
		Thornball,
		Seeds,
		Ramming
	}

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer ModdedOwner => Owner.Calamity();

	public NPC Target
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return Owner.Center.MinionHoming(PlantationStaff.EnemyDistanceDetection, Owner);
		}
	}

	public AIState State
	{
		get
		{
			return (AIState)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = (float)value;
		}
	}

	public ref float AITimer => ref base.Projectile.ai[1];

	public ref float ShootSeedsTimer => ref base.Projectile.ai[2];

	public ref float SeedBurstsShot => ref base.Projectile.localAI[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.minionSlots = 3f;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.width = (base.Projectile.height = 48);
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.netImportant = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(SeedBurstsShot);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		SeedBurstsShot = reader.ReadSingle();
	}

	public override void AI()
	{
		CheckMinionExistence();
		DoAnimation();
		switch (State)
		{
		case AIState.Idle:
			IdleState();
			break;
		case AIState.Thornball:
			ThornballState();
			break;
		case AIState.Seeds:
			SeedsState();
			break;
		case AIState.Ramming:
			RammingState();
			break;
		}
	}

	private void IdleState()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (!base.Projectile.WithinRange(Owner.Center, 160f))
		{
			base.Projectile.velocity = (base.Projectile.velocity + base.Projectile.SafeDirectionTo(Owner.Center)) * 0.9f;
			base.Projectile.netUpdate = true;
		}
		if (!base.Projectile.WithinRange(Owner.Center, 320f))
		{
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(Owner.Center) * MathF.Max(5f, ((Vector2)(ref Owner.velocity)).Length());
			base.Projectile.netUpdate = true;
		}
		if (!base.Projectile.WithinRange(Owner.Center, PlantationStaff.EnemyDistanceDetection))
		{
			base.Projectile.Center = Owner.Center;
			base.Projectile.netUpdate = true;
		}
		if (Target != null)
		{
			SwitchState(AIState.Thornball);
		}
	}

	private void ThornballState()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		if (Target != null)
		{
			Vector2 targetDirection = base.Projectile.SafeDirectionTo(Target.Center);
			ShootingMovement();
			AITimer++;
			if (AITimer % PlantationStaff.ThornballFireRate == 0f && Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, targetDirection * PlantationStaff.ThornballSpeed, ModContent.ProjectileType<PlantationStaffThornball>(), base.Projectile.damage, base.Projectile.knockBack, Owner.whoAmI);
				Projectile projectile = base.Projectile;
				projectile.velocity -= targetDirection * 3f;
				ShootEffect();
				SoundEngine.PlaySound(in SoundID.Item17, base.Projectile.Center);
				base.Projectile.netUpdate = true;
			}
			if (AITimer >= (float)PlantationStaff.ThornballAmount * PlantationStaff.ThornballFireRate)
			{
				SwitchState(AIState.Seeds);
			}
		}
		else
		{
			SwitchState(AIState.Idle);
		}
	}

	private void SeedsState()
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		if (Target != null)
		{
			ShootingMovement();
			AITimer++;
			if (AITimer >= PlantationStaff.SeedBurstDelay)
			{
				ShootSeedsTimer++;
				if (ShootSeedsTimer % PlantationStaff.SeedBetweenBurstDelay == 0f && Main.myPlayer == base.Projectile.owner)
				{
					Vector2 velocity = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, Target, PlantationStaff.SeedSpeed);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<PlantationStaffSeed>(), base.Projectile.damage, base.Projectile.knockBack, Owner.whoAmI, Main.rand.Next(2));
					Projectile projectile = base.Projectile;
					projectile.velocity -= velocity.SafeNormalize(Vector2.Zero) * 2f;
					ShootEffect();
					SoundEngine.PlaySound(in SoundID.Item17, base.Projectile.Center);
					base.Projectile.netUpdate = true;
				}
				if (ShootSeedsTimer >= PlantationStaff.SeedBetweenBurstDelay * (float)PlantationStaff.SeedAmountPerBurst)
				{
					AITimer = 0f;
					ShootSeedsTimer = 0f;
					SeedBurstsShot++;
					base.Projectile.netUpdate = true;
				}
			}
			if (SeedBurstsShot >= (float)PlantationStaff.SeedBurstAmount)
			{
				SwitchState(AIState.Ramming);
			}
		}
		else
		{
			SwitchState(AIState.Idle);
		}
	}

	private void RammingState()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (Target != null)
		{
			AITimer++;
			if (AITimer <= PlantationStaff.TimeBeforeRamming)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.985f;
				return;
			}
			if (!base.Projectile.WithinRange(Target.Center, 400f))
			{
				RamMovement();
			}
			else if (((Vector2)(ref base.Projectile.velocity)).Length() < PlantationStaff.ChargingSpeed - 5f)
			{
				RamMovement();
			}
			if (AITimer >= PlantationStaff.RamTime + PlantationStaff.TimeBeforeRamming)
			{
				SwitchState(AIState.Thornball);
			}
		}
		else
		{
			SwitchState(AIState.Idle);
		}
	}

	private void ShootingMovement()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		Vector2 targetDirection = base.Projectile.SafeDirectionTo(Target.Center);
		base.Projectile.rotation = targetDirection.ToRotation();
		if (!base.Projectile.WithinRange(Target.Center, 480f))
		{
			base.Projectile.velocity = (base.Projectile.velocity * 30f + targetDirection * PlantationStaff.ChargingSpeed) / 31f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.WithinRange(Target.Center, 400f))
		{
			base.Projectile.velocity = (base.Projectile.velocity * 30f + -targetDirection * PlantationStaff.ChargingSpeed) / 31f;
			base.Projectile.netUpdate = true;
		}
	}

	private void RamMovement()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = base.Projectile.SafeDirectionTo(Target.Center) * PlantationStaff.ChargingSpeed;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.netUpdate = true;
	}

	private void SwitchState(AIState state)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		State = state;
		AITimer = 0f;
		ShootSeedsTimer = 0f;
		SeedBurstsShot = 0f;
		if (state == AIState.Ramming)
		{
			int sporeCloudAmount = 12;
			for (int sporeCloudIndex = 0; sporeCloudIndex < sporeCloudAmount; sporeCloudIndex++)
			{
				Vector2 velocity = ((float)Math.PI * 2f / (float)sporeCloudAmount * (float)sporeCloudIndex).ToRotationVector2() * PlantationStaff.SporeStartVelocity;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<PlantationStaffSporeCloud>(), base.Projectile.damage, 10f, Owner.whoAmI, Main.rand.Next(3));
			}
			int tentacleAmount = 6;
			for (int tentacleIndex = 0; tentacleIndex < tentacleAmount; tentacleIndex++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<PlantationStaffTentacle>(), base.Projectile.damage, base.Projectile.knockBack, Owner.whoAmI, tentacleIndex, base.Projectile.whoAmI);
			}
			SoundStyle style = SoundID.Roar with
			{
				Volume = 0.3f,
				Pitch = 1f,
				PitchVariance = 0.1f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		base.Projectile.netUpdate = true;
	}

	private void CheckMinionExistence()
	{
		Owner.AddBuff(ModContent.BuffType<PlantationStaffBuff>(), 2);
		if (base.Type == ModContent.ProjectileType<PlantationStaffSummon>())
		{
			if (Owner.dead)
			{
				ModdedOwner.PlantationSummon = false;
			}
			if (ModdedOwner.PlantationSummon)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	private void DoAnimation()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 8)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
			if (State == AIState.Ramming && base.Projectile.frame < 4)
			{
				base.Projectile.frame = 4;
			}
			if (State != AIState.Ramming && base.Projectile.frame > 3)
			{
				base.Projectile.frame = 0;
			}
		}
	}

	private void ShootEffect()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center, 40, base.Projectile.rotation.ToRotationVector2().RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(3f, 5f));
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (State == AIState.Ramming)
		{
			modifiers.SourceDamage *= 2f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		if (State == AIState.Ramming && CalamityClientConfig.Instance.Afterimages)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		}
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
