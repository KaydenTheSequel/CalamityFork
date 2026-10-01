using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AstralProbeSummon : ModProjectile, ILocalizedModType, IModType
{
	public int ProbeIndex;

	public bool CheckForSpawning;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer moddedOwner => Owner.Calamity();

	public ref float AITimer => ref base.Projectile.ai[0];

	public ref float TimerForShooting => ref base.Projectile.ai[1];

	public float ProbePositionAngle
	{
		get
		{
			float probeCount = Owner.ownedProjectileCounts[base.Type];
			if (probeCount <= 1f)
			{
				probeCount = 1f;
			}
			return (float)Math.PI * 2f * (float)ProbeIndex / probeCount + AITimer * 0.025f;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.minionSlots = 1f;
		base.Projectile.penetrate = -1;
		base.Projectile.width = 36;
		base.Projectile.height = 30;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.netImportant = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.minion = true;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		NPC target = base.Projectile.Center.MinionHoming(2500f, Owner);
		Vector2 idleDestination = Owner.Center + ProbePositionAngle.ToRotationVector2() * 150f;
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, idleDestination, 0.15f);
		AITimer++;
		CheckMinionExistince();
		SpawnEffect();
		LookInCorrectDirection(target);
		ShootTarget(target);
		base.Projectile.netUpdate = true;
	}

	public void CheckMinionExistince()
	{
		Owner.AddBuff(ModContent.BuffType<AstralProbeBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<AstralProbeSummon>())
		{
			if (Owner.dead)
			{
				moddedOwner.astralProbe = false;
			}
			if (moddedOwner.astralProbe)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public void SpawnEffect()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		if (!CheckForSpawning)
		{
			int dustAmt = 120;
			for (int dustIndex = 0; dustIndex < dustAmt; dustIndex++)
			{
				Vector2 velocity = ((float)Math.PI * 2f / (float)dustAmt * (float)dustIndex).ToRotationVector2() * 10f;
				int randomDust = Utils.SelectRandom<int>(Main.rand, ModContent.DustType<AstralOrange>(), ModContent.DustType<AstralBlue>());
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, randomDust, velocity);
				dust.customData = false;
				dust.noGravity = true;
				dust.velocity *= 0.3f;
				dust.scale = ((Vector2)(ref velocity)).Length() * 0.1f;
			}
			CheckForSpawning = true;
		}
	}

	public void LookInCorrectDirection(NPC target)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = target?.Center ?? Main.MouseWorld;
		int direction = (val.X - base.Projectile.Center.X > 0f).ToDirectionInt();
		float rotation = (val - base.Projectile.Center).ToRotation();
		base.Projectile.spriteDirection = direction;
		base.Projectile.rotation = ((direction == -1) ? (rotation + (float)Math.PI) : rotation);
		base.Projectile.netUpdate = true;
	}

	public void ShootTarget(NPC target)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		if (target != null && Main.myPlayer == base.Projectile.owner)
		{
			if (TimerForShooting == 80f)
			{
				Vector2 velocity = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, target, 35f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<AstralProbeRound>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				SoundStyle style = SoundID.Item12 with
				{
					Volume = SoundID.Item12.Volume * 0.5f,
					PitchVariance = 1f
				};
				SoundEngine.PlaySound(in style, base.Projectile.position);
				TimerForShooting = 0f;
				base.Projectile.netUpdate = true;
			}
			if (TimerForShooting < 80f)
			{
				TimerForShooting++;
			}
		}
	}
}
