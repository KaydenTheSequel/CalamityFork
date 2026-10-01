using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class SeashineSwordProj : ModProjectile, ILocalizedModType, IModType
{
	private const int TimeLeft = 600;

	private const int MaxAlpha = 255;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = 600;
		base.Projectile.alpha = 255;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0f, (float)(255 - base.Projectile.alpha) * 0.5f / 255f, (float)(255 - base.Projectile.alpha) * 0.5f / 255f);
		if (base.Projectile.localAI[1] < 7f)
		{
			base.Projectile.localAI[1]++;
		}
		else
		{
			float dustScale = 1.8f * base.Projectile.scale;
			int dust = Dust.NewDust(new Vector2(base.Projectile.position.X - base.Projectile.velocity.X + 2f, base.Projectile.position.Y + 2f - base.Projectile.velocity.Y), 8, 8, 187, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, base.Projectile.alpha, default(Color), dustScale);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= -0.25f;
			dust = Dust.NewDust(new Vector2(base.Projectile.position.X - base.Projectile.velocity.X + 2f, base.Projectile.position.Y + 2f - base.Projectile.velocity.Y), 8, 8, 187, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, base.Projectile.alpha, default(Color), dustScale);
			Main.dust[dust].noGravity = true;
			Dust obj2 = Main.dust[dust];
			obj2.velocity *= -0.25f;
			Dust obj3 = Main.dust[dust];
			obj3.position -= base.Projectile.velocity * 0.5f;
			if (base.Projectile.localAI[0] == 0f)
			{
				base.Projectile.scale -= 0.02f;
				base.Projectile.alpha += 10;
				if (base.Projectile.alpha >= 250)
				{
					base.Projectile.alpha = 255;
					base.Projectile.localAI[0] = 1f;
				}
			}
			else if (base.Projectile.localAI[0] == 1f)
			{
				base.Projectile.scale += 0.02f;
				base.Projectile.alpha -= 10;
				if (base.Projectile.alpha <= 0)
				{
					base.Projectile.alpha = 0;
					base.Projectile.localAI[0] = 0f;
				}
			}
		}
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1] = 1f;
			SoundEngine.PlaySound(in SoundID.Item8, base.Projectile.Center);
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 4f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 595)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Projectile.type], lightColor, 2);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return new Color(128, 255, 255);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		for (int i = 4; i < 31; i++)
		{
			float oldXVel = base.Projectile.oldVelocity.X * (30f / (float)i);
			float oldYVel = base.Projectile.oldVelocity.Y * (30f / (float)i);
			float dustScale = 3.6f * base.Projectile.scale;
			int dust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - oldXVel * 0.5f, base.Projectile.oldPosition.Y - oldYVel * 0.5f), 8, 8, 187, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, base.Projectile.alpha, default(Color), dustScale);
			Main.dust[dust].noGravity = true;
			dustScale = 3f * base.Projectile.scale;
			dust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - oldXVel * 0.5f, base.Projectile.oldPosition.Y - oldYVel * 0.5f), 8, 8, 187, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, base.Projectile.alpha, default(Color), dustScale);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.1f;
		}
	}
}
