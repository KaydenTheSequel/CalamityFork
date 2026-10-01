using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class AresPlasmaBolt : ModProjectile, ILocalizedModType, IModType
{
	private const int timeLeft = 360;

	private const float maxVelocity = 10f;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 18;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.Opacity = 0f;
		base.CooldownSlot = 1;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 360;
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 10f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.025f;
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 10f)
			{
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 10f;
			}
		}
		int fadeOutTime = 15;
		int fadeInTime = 3;
		if (base.Projectile.timeLeft < fadeOutTime)
		{
			base.Projectile.Opacity = MathHelper.Clamp((float)base.Projectile.timeLeft / (float)fadeOutTime, 0f, 1f);
		}
		else
		{
			base.Projectile.Opacity = MathHelper.Clamp(1f - (float)(base.Projectile.timeLeft - (360 - fadeInTime)) / (float)fadeInTime, 0f, 1f);
		}
		Lighting.AddLight(base.Projectile.Center, 0f, 0.4f * base.Projectile.Opacity, 0f);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 8)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) - (float)Math.PI / 2f;
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.Opacity == 1f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).G = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
