using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SandnadoMinion : ModProjectile, ILocalizedModType, IModType
{
	public bool CheckForSpawning;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer ModdedOwner => Owner.Calamity();

	public ref float TimerForShooting => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.minionSlots = 1f;
		base.Projectile.penetrate = -1;
		base.Projectile.width = 40;
		base.Projectile.height = 43;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		NPC potentialTarget = base.Projectile.Center.MinionHoming(2000f, Owner);
		if (potentialTarget != null)
		{
			MoveToTarget(potentialTarget);
			ShootTarget(potentialTarget);
		}
		else
		{
			Idle();
		}
		CanMinionExist();
		OnSpawn();
		DoAnimation();
		base.Projectile.MinionAntiClump();
		base.Projectile.netUpdate = true;
	}

	public void CanMinionExist()
	{
		Owner.AddBuff(ModContent.BuffType<Sandnado>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<SandnadoMinion>())
		{
			if (Owner.dead)
			{
				ModdedOwner.sandnado = false;
			}
			if (ModdedOwner.sandnado)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public void OnSpawn()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (!CheckForSpawning)
		{
			int dustAmount = 25;
			for (int d = 0; d < dustAmount; d++)
			{
				Vector2 velocity = ((float)Math.PI * 2f / (float)dustAmount * (float)d).ToRotationVector2() * Main.rand.NextFloat(5f, 6.5f);
				Dust.NewDustPerfect(base.Projectile.Center, 85, velocity);
			}
			CheckForSpawning = true;
		}
	}

	public void DoAnimation()
	{
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 8 % Main.projFrames[base.Type];
	}

	public void Idle()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.WithinRange(Owner.Center, 1200f) && !base.Projectile.WithinRange(Owner.Center, 300f))
		{
			base.Projectile.velocity = (Owner.Center - base.Projectile.Center) / 30f;
			base.Projectile.netUpdate = true;
		}
		else if (!base.Projectile.WithinRange(Owner.Center, 160f))
		{
			base.Projectile.velocity = (base.Projectile.velocity * 37f + base.Projectile.SafeDirectionTo(Owner.Center) * 17f) / 40f;
			base.Projectile.netUpdate = true;
		}
		if (!base.Projectile.WithinRange(Owner.Center, 1200f))
		{
			base.Projectile.position = Owner.Center;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.3f;
			base.Projectile.netUpdate = true;
		}
	}

	public void MoveToTarget(NPC target)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		Vector2 vecToTarget = target.Center - base.Projectile.Center;
		float targetDist = ((Vector2)(ref vecToTarget)).Length();
		((Vector2)(ref vecToTarget)).Normalize();
		if (targetDist > 200f)
		{
			float speedMult = ((targetDist > 400f) ? 12f : ((targetDist > 250f) ? 6f : 3f));
			vecToTarget *= speedMult;
			base.Projectile.velocity = (base.Projectile.velocity * 40f + vecToTarget) / 41f;
		}
		else
		{
			float speedMult2 = -3f;
			vecToTarget *= speedMult2;
			base.Projectile.velocity = (base.Projectile.velocity * 40f + vecToTarget) / 41f;
		}
	}

	public void ShootTarget(NPC target)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		int randomDelay = Main.rand.Next(-5, 6);
		Vector2 ProjVel = target.Center - base.Projectile.Center;
		((Vector2)(ref ProjVel)).Normalize();
		ProjVel *= 30f;
		if (TimerForShooting == 50f + (float)randomDelay && Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, ProjVel, ModContent.ProjectileType<MiniSandShark>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			TimerForShooting = 0f;
		}
		if (TimerForShooting < 50f)
		{
			TimerForShooting++;
		}
	}
}
