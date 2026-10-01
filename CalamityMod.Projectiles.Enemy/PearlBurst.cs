using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class PearlBurst : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 64;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 300;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f)
		{
			Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0f / 255f, (float)(255 - base.Projectile.alpha) * 0.5f / 255f, (float)(255 - base.Projectile.alpha) * 0.5f / 255f);
			if (Main.rand.NextBool(5))
			{
				Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 92, base.Projectile.velocity.X * 0.25f, base.Projectile.velocity.Y * 0.25f, 0, new Color(255, 255, 255), 0.7f);
			}
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 3)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		base.Projectile.alpha -= 6;
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] <= 20f)
		{
			base.Projectile.velocity.X *= 0.975f;
			base.Projectile.velocity.Y *= 0.975f;
		}
		else if (base.Projectile.ai[1] > 20f && base.Projectile.ai[1] <= 39f)
		{
			base.Projectile.velocity.X *= 1.05f;
			base.Projectile.velocity.Y *= 1.05f;
		}
		else if (base.Projectile.ai[1] == 40f)
		{
			base.Projectile.ai[1] = 0f;
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] == 30f)
		{
			base.Projectile.localAI[0] = 0f;
			for (int l = 0; l < 12; l++)
			{
				Vector2 dustRotation = Vector2.UnitX * (0f - (float)base.Projectile.width) / 2f;
				dustRotation += -Vector2.UnitY.RotatedBy((float)l * (float)Math.PI / 6f) * new Vector2(8f, 16f);
				dustRotation = dustRotation.RotatedBy(base.Projectile.rotation - (float)Math.PI / 2f);
				int pearlDust = Dust.NewDust(base.Projectile.Center, 0, 0, 92, 0f, 0f, 160);
				Main.dust[pearlDust].scale = 1.1f;
				Main.dust[pearlDust].noGravity = true;
				Main.dust[pearlDust].position = base.Projectile.Center + dustRotation;
				Main.dust[pearlDust].velocity = base.Projectile.velocity * 0.1f;
				Main.dust[pearlDust].velocity = Vector2.Normalize(base.Projectile.Center - base.Projectile.velocity * 3f - Main.dust[pearlDust].position) * 1.25f;
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 255);
	}
}
