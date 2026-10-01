using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class WhitewaterSpear : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 startVel;

	public Vector2 startPos;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 64;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 230;
		base.Projectile.extraUpdates = 5;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color lightBlue = Color.LightBlue;
		Lighting.AddLight(center, ((Color)(ref lightBlue)).ToVector3() * 0.4f);
		if (time == 0f)
		{
			startVel = base.Projectile.velocity;
			startPos = base.Projectile.Center;
		}
		if (base.Projectile.timeLeft % 3 == 0)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - base.Projectile.velocity * 2f, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 14, 1f, Color.LightBlue * 0.2f));
		}
		if (time > 80f)
		{
			base.Projectile.velocity.X = MathHelper.Lerp(base.Projectile.velocity.X, 0f - startVel.X, 0.02f);
			base.Projectile.velocity.Y = MathHelper.Lerp(base.Projectile.velocity.Y, 0f - startVel.Y, 0.02f);
		}
		else
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.995f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(103, 180);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 15f, targetHitbox);
	}
}
