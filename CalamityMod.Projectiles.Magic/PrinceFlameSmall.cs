using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class PrinceFlameSmall : ModProjectile, ILocalizedModType, IModType
{
	public const int AttackDelay = 12;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = 80 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % Main.projFrames[base.Type];
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 15f, base.Projectile.timeLeft, clamped: true);
		if (Main.rand.NextBool(3))
		{
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, PrinceFlameLarge.FlameColor * base.Projectile.Opacity, Color.DarkSlateGray * base.Projectile.Opacity, Main.rand.NextFloat(0.4f, 0.6f), 140f, Main.rand.NextFloat(-0.1f, 0.1f)));
		}
		if (Time > 12f)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: false, 600f, 14f, 32f);
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center + Main.rand.NextVector2Circular(6f, 6f), (-base.Projectile.velocity).RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.1f, 0.4f), affectedByGravity: false, 9, Main.rand.NextFloat(0.4f, 1f), PrinceFlameLarge.FlameColor));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		lightColor = Color.Lerp(lightColor, Color.White, 0.8f);
		((Color)(ref lightColor)).A = (byte)(((Color)(ref lightColor)).A / 2);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (Time > 12f)
		{
			return null;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 60);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(2f, 12f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.8f, 1.5f);
			dust.color = Main.hslToRgb(Main.rand.NextFloat(0.033f, 0.167f), 1f, 0.66f);
			dust.noLightEmittence = true;
		}
	}
}
