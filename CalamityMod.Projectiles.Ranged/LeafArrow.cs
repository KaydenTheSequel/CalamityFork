using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class LeafArrow : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
	{
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.alpha -= 2;
		base.Projectile.ai[0] = (float)Main.rand.Next(-100, 101) * 0.0025f;
		base.Projectile.ai[1] = (float)Main.rand.Next(-100, 101) * 0.0025f;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.scale += 0.05f;
			if ((double)base.Projectile.scale > 1.2)
			{
				base.Projectile.localAI[0] = 1f;
			}
		}
		else
		{
			base.Projectile.scale -= 0.05f;
			if ((double)base.Projectile.scale < 0.8)
			{
				base.Projectile.localAI[0] = 0f;
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI;
		if (base.Projectile.localAI[1] <= 30f)
		{
			base.Projectile.localAI[1]++;
			base.Projectile.velocity.Y *= 0.975f;
			base.Projectile.velocity.X *= 0.975f;
		}
		else if (base.Projectile.localAI[1] <= 60f)
		{
			base.Projectile.localAI[1]++;
			base.Projectile.velocity.Y *= 1.025f;
			base.Projectile.velocity.X *= 1.025f;
		}
		else
		{
			base.Projectile.localAI[1] = 0f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return new Color(Main.DiscoR, 203, 103, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Grass, base.Projectile.position);
		base.Projectile.localAI[1]++;
		for (int i = 0; i < 5; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 157, 0f, 0f, 0, new Color(Main.DiscoR, 203, 103));
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 3f;
			Main.dust[dust].scale = 1.5f;
		}
	}
}
