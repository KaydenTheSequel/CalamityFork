using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class OpalChargedStrike : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 35);
		base.Projectile.friendly = true;
		base.Projectile.alpha = 55;
		base.Projectile.penetrate = 5;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 6;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		if (Collision.SolidCollision(base.Projectile.Center, 5, 5))
		{
			base.Projectile.Kill();
		}
		if (Main.rand.NextBool(3))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>(), Vector2.One.RotatedByRandom(6.2831854820251465) * 0.4f, 0, default(Color), Main.rand.NextFloat(0.75f, 0.95f));
			dust.noGravity = !Main.rand.NextBool(5);
			dust.color = (Main.rand.NextBool(3) ? Color.Orange : Color.OrangeRed);
			dust.fadeIn = -0.85f;
		}
		float playerDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (Main.rand.NextBool(3) && playerDist < 1400f && base.Projectile.timeLeft < 290)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Main.rand.NextVector2Circular(8f, 8f), scale: Main.rand.NextFloat(0.45f, 0.6f), color: Main.rand.NextBool(3) ? Color.Orange : Color.OrangeRed, velocity: base.Projectile.velocity * 0.2f, texture: "CalamityMod/Particles/BloomCircle", affectedByGravity: false, lifetime: 40, stretch: new Vector2(0.2f, 1.4f), useAddativeBlend: true, glowCenter: true, extraRotation: 0f, fadeIn: false, affectedByLight: false, shrinkSpeed: 0.13f, glowCenterScale: 1f, glowOpacity: 0.6f));
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		int mode = ProjectileID.Sets.TrailingMode[base.Type];
		Color orangeRed = Color.OrangeRed;
		((Color)(ref orangeRed)).A = 0;
		CalamityUtils.DrawAfterimagesCentered(projectile, mode, orangeRed);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 120);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.75f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 12; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>(), base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(30f)) * Main.rand.NextFloat(0.8f, 1.8f), 0, default(Color), Main.rand.NextFloat(1.9f, 2.8f));
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool(3) ? Color.Orange : Color.OrangeRed);
			dust.fadeIn = 1.85f;
		}
	}

	public override bool? CanDamage()
	{
		return base.CanDamage();
	}
}
