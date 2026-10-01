using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BrimseekerAuraBall : ModProjectile, ILocalizedModType, IModType
{
	public bool Initialized;

	public const float OutwardnessMovementStep = 4f;

	public const float MaxRotationalSpeed = 0.08f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Projectile ParentProjectile => CalamityUtils.FindProjectileByIdentity((int)base.Projectile.ai[0], base.Projectile.owner);

	public float Outwardness
	{
		get
		{
			return base.Projectile.localAI[0];
		}
		set
		{
			base.Projectile.localAI[0] = value;
		}
	}

	public float RotationalSpeed
	{
		get
		{
			return base.Projectile.localAI[1];
		}
		set
		{
			base.Projectile.localAI[1] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 8);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		if (!Initialized)
		{
			base.Projectile.rotation = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
			RotationalSpeed = Main.rand.NextFloat(-0.08f, 0.08f);
			Initialized = true;
		}
		if (ParentProjectile == null)
		{
			base.Projectile.Kill();
			return;
		}
		Vector2 offsetRelativeToTarget = Vector2.UnitY.RotatedBy(base.Projectile.rotation) * Outwardness;
		base.Projectile.Center = ParentProjectile.Center + offsetRelativeToTarget;
		base.Projectile.rotation += RotationalSpeed;
		if ((float)(base.Projectile.timeLeft % 200) / 4f < 25f)
		{
			Outwardness += 4f;
		}
		else
		{
			Outwardness -= 4f;
		}
		Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 235, 0f, 0f, 100, default(Color), 1.5f);
		dust.noGravity = true;
		dust.velocity.Y = -0.15f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
	}
}
