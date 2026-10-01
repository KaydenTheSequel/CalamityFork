using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ThornBallSpike : ModProjectile, ILocalizedModType, IModType
{
	private const float MaxVelocity = 12f;

	private const int TimeLeft = 600;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 12f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.045f;
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 12f)
			{
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 12f;
			}
		}
		if (base.Projectile.timeLeft < 570)
		{
			base.Projectile.tileCollide = true;
		}
		int dustType = 171;
		int dust = Dust.NewDust(new Vector2(base.Projectile.position.X - base.Projectile.velocity.X + 2f, base.Projectile.position.Y + 2f - base.Projectile.velocity.Y), 8, 8, dustType, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, base.Projectile.alpha);
		Main.dust[dust].noGravity = true;
		Dust obj = Main.dust[dust];
		obj.velocity *= -0.25f;
		Main.dust[dust].fadeIn = 1.5f;
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(70, 120);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 595)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		for (int i = 2; i < 16; i++)
		{
			float oldXVel = base.Projectile.oldVelocity.X * (15f / (float)i);
			float oldYVel = base.Projectile.oldVelocity.Y * (15f / (float)i);
			float dustScale = 1.4f;
			int dustType = 171;
			int dust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - oldXVel * 0.5f, base.Projectile.oldPosition.Y - oldYVel * 0.5f), 8, 8, dustType, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, base.Projectile.alpha, default(Color), dustScale);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].fadeIn = 1.5f;
			dustScale = 1.2f;
			dust = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - oldXVel * 0.5f, base.Projectile.oldPosition.Y - oldYVel * 0.5f), 8, 8, dustType, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, base.Projectile.alpha, default(Color), dustScale);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.1f;
			Main.dust[dust].fadeIn = 1.5f;
		}
	}
}
