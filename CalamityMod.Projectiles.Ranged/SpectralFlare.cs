using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SpectralFlare : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 6);
		base.Projectile.scale = 1.15f;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.netImportant = true;
		base.Projectile.aiStyle = 33;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 3;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30 * base.Projectile.MaxUpdates;
		base.DrawOriginOffsetY = -10;
	}

	public override bool PreAI()
	{
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 80;
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
		}
		float xVel = base.Projectile.ai[0];
		float yVel = base.Projectile.ai[1];
		if (xVel == 0f && yVel == 0f)
		{
			xVel = 1f;
		}
		float dist = MathF.Sqrt(xVel * xVel + yVel * yVel);
		dist = 4f / dist;
		xVel *= dist;
		yVel *= dist;
		if (base.Projectile.alpha < 70)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.position - Vector2.UnitY * 2f, 7, 7, 175, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), 1.15f);
			dust.noGravity = true;
			dust.velocity *= 0.3f;
			dust.position.X -= xVel;
			dust.position.Y -= yVel;
			dust.velocity.X -= xVel;
			dust.velocity.Y -= yVel;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.ai[0] = base.Projectile.velocity.X;
			base.Projectile.ai[1] = base.Projectile.velocity.Y;
			if (base.Projectile.localAI[1] == 1f)
			{
				base.Projectile.velocity.Y += 0.09f;
				if (base.Projectile.velocity.Y > 16f)
				{
					base.Projectile.velocity.Y = 16f;
				}
			}
		}
		else
		{
			if (!Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
			{
				base.Projectile.localAI[0] = 0f;
				base.Projectile.localAI[1] = 1f;
			}
			base.Projectile.damage = 0;
		}
		base.Projectile.rotation = MathF.Atan2(base.Projectile.ai[1], base.Projectile.ai[0]) + (float)Math.PI / 2f;
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Nightwither>(), Main.rand.NextBool(3) ? 600 : 300);
	}
}
