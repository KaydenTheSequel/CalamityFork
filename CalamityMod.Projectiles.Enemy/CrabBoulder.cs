using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class CrabBoulder : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 26);
		base.Projectile.hostile = true;
		base.Projectile.timeLeft = 480;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		if (base.Projectile.ai[0]++ < 30f)
		{
			base.Projectile.scale = MathHelper.Lerp(0.004f, 1f, base.Projectile.ai[0] / 30f);
		}
		else if (base.Projectile.velocity.Y < 12f)
		{
			base.Projectile.velocity.Y += 0.18f;
		}
		base.Projectile.tileCollide = base.Projectile.ai[0] > 70f;
		base.Projectile.rotation += (float)Math.Sign(base.Projectile.velocity.X) * 0.08f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Utils.PoofOfSmoke(base.Projectile.Center);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
