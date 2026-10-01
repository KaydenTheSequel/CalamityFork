using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ShadecrystalProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 50;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[2] < 255f)
		{
			base.Projectile.ai[2] += 3f;
			if (base.Projectile.ai[2] >= 255f)
			{
				base.Projectile.ai[2] = -255f;
			}
		}
		Lighting.AddLight(base.Projectile.Center, 0.15f * base.Projectile.scale, 0f, 0.15f * base.Projectile.scale);
		base.Projectile.rotation += base.Projectile.velocity.X * 0.2f;
		if (Main.rand.NextBool(6))
		{
			int crystalDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 70, 0f, 0f, base.Projectile.alpha, new Color(Math.Abs(base.Projectile.ai[2]), 0f, 255f, (float)base.Projectile.alpha));
			Main.dust[crystalDust].noGravity = true;
			Main.dust[crystalDust].velocity = base.Projectile.velocity * 0.2f;
			Main.dust[crystalDust].scale = base.Projectile.scale;
		}
		float maxVelocity = 6f;
		if (((Vector2)(ref base.Projectile.velocity)).Length() < maxVelocity)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= base.Projectile.ai[1];
			if (((Vector2)(ref base.Projectile.velocity)).Length() > maxVelocity)
			{
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= maxVelocity;
			}
		}
		else
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: false, 224f, maxVelocity * 1.25f, 30f);
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 520f)
		{
			base.Projectile.scale -= 0.01f;
			if (base.Projectile.scale <= 0.2f)
			{
				base.Projectile.scale = 0.2f;
				base.Projectile.Kill();
			}
			base.Projectile.width = (int)(6f * base.Projectile.scale);
			base.Projectile.height = (int)(6f * base.Projectile.scale);
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			int dust = Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 70, 0f, 0f, base.Projectile.alpha, new Color(Math.Abs(base.Projectile.ai[2]), 0f, 255f, (float)base.Projectile.alpha));
			Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity = base.Projectile.oldVelocity * 0.5f;
			Main.dust[dust].scale = base.Projectile.scale;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 120);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		return new Color(Math.Abs(base.Projectile.ai[2]), 0f, 255f, (float)base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
