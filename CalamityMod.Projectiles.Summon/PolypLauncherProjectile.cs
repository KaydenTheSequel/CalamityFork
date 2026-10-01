using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PolypLauncherProjectile : ModProjectile, ILocalizedModType, IModType
{
	public const float Gravity = 0.4f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.SentryShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 18);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		if (base.Projectile.velocity.Y <= 10f)
		{
			base.Projectile.velocity.Y += 0.4f;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] < 10f)
		{
			base.Projectile.alpha = (int)MathHelper.Lerp(255f, 0f, base.Projectile.ai[0] / 10f);
		}
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.1f * (float)base.Projectile.direction;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		for (int dust_splash = 0; dust_splash < 9; dust_splash++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 225, (0f - base.Projectile.velocity.X) * 0.15f, (0f - base.Projectile.velocity.Y) * 0.15f, 120);
		}
		int split = 0;
		Vector2 shardSpeed = default(Vector2);
		for (int shardAmt = Main.rand.Next(1, 5); split < shardAmt; split++)
		{
			float shardspeedX = (0f - base.Projectile.velocity.X) * Main.rand.NextFloat(0.5f, 0.7f) + Main.rand.NextFloat(-3f, 3f);
			float shardspeedY = (0f - base.Projectile.velocity.Y) * (float)Main.rand.Next(50, 70) * 0.01f + (float)Main.rand.Next(-8, 9) * 0.2f;
			if (shardspeedX < 2f && shardspeedX > -2f)
			{
				shardspeedX += 0f - base.Projectile.velocity.X;
			}
			if (shardspeedY > 2f && shardspeedY < 2f)
			{
				shardspeedY += 0f - base.Projectile.velocity.Y;
			}
			((Vector2)(ref shardSpeed))._002Ector(shardspeedX, shardspeedY);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + shardSpeed, shardSpeed, ModContent.ProjectileType<PolypLauncherShrapnel>(), (int)((double)base.Projectile.damage * 0.5), base.Projectile.knockBack / 2f, base.Projectile.owner, Main.rand.Next(3));
		}
	}
}
