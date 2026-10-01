using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class UltimaBolt : ModProjectile, ILocalizedModType, IModType
{
	public const int MaxBounces = 1;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public float Bounces
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public float DefaultHue => ((float)Math.Sin(Main.GlobalTimeWrappedHourly * 2.5f) + 1f) * 0.5f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 42;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.arrow = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public Color GetColorFromHue(float hue)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		return Main.hslToRgb(hue, 1f, 0.725f) * base.Projectile.Opacity;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return GetColorFromHue(DefaultHue);
	}

	public void ProduceExplosionDust(int dustCount)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < dustCount; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
				dust.color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.8f);
				dust.scale = Main.rand.NextFloat(1f, 1.25f);
				dust.velocity = Main.rand.NextVector2Circular(7f, 7f);
				dust.noGravity = true;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (Bounces >= 1f)
		{
			base.Projectile.Kill();
			return false;
		}
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		Bounces++;
		ProduceExplosionDust(24);
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			if (i > 0)
			{
				CalamityUtils.DistanceClamp(ref base.Projectile.oldPos[i], ref base.Projectile.oldPos[i - 1], 8f);
			}
			float completionRatio = (float)i / (float)base.Projectile.oldPos.Length;
			Color color = GetColorFromHue((DefaultHue + completionRatio * 0.5f) % 1f) * (float)Math.Pow(1f - completionRatio, 2.0);
			Main.EntitySpriteDraw(texture, base.Projectile.oldPos[i] + texture.Size() * 0.5f - Main.screenPosition, null, color, base.Projectile.rotation, texture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		base.Projectile.ExpandHitboxBy(90, 90);
		base.Projectile.Damage();
		ProduceExplosionDust(32);
	}
}
