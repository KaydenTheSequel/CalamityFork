using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MagnaShot : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 5);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Main.rand.NextVector2Circular(4f, 4f) - base.Projectile.velocity, scale: Main.rand.NextFloat(0.04f, 0.05f), color: Main.rand.NextBool(3) ? Color.DodgerBlue : Color.RoyalBlue, velocity: base.Projectile.velocity.RotatedByRandom(0.029999999329447746) * Main.rand.NextFloat(0.1f, 0.4f), texture: "CalamityMod/Particles/SquareRotated", affectedByGravity: false, lifetime: Main.rand.NextBool(7) ? 35 : 6, stretch: new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true, extraRotation: Main.rand.NextFloat(0f, (float)Math.PI * 2f), fadeIn: false, affectedByLight: false, shrinkSpeed: 0f, glowCenterScale: 1f, glowOpacity: 0.5f));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		int mode = ProjectileID.Sets.TrailingMode[base.Type];
		Color dodgerBlue = Color.DodgerBlue;
		((Color)(ref dodgerBlue)).A = 0;
		CalamityUtils.DrawAfterimagesCentered(projectile, mode, dodgerBlue);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 2; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>(), -base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(10f)) * Main.rand.NextFloat(0.8f, 1.8f), 0, default(Color), Main.rand.NextFloat(0.9f, 1.3f));
			dust.noGravity = false;
			dust.color = (Main.rand.NextBool(3) ? Color.DodgerBlue : Color.RoyalBlue);
			dust.fadeIn = -0.15f;
		}
		SoundStyle style = SoundID.DD2_WitherBeastCrystalImpact with
		{
			Volume = 0.8f,
			PitchVariance = 0.3f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override bool? CanDamage()
	{
		return base.CanDamage();
	}
}
