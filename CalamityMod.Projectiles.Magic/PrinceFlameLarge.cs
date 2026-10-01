using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class PrinceFlameLarge : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 60;

	public const int FadeoutTime = 24;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Time => ref base.Projectile.ai[0];

	public static Color FlameColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(255, 180, 80);
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 60;
		base.Projectile.penetrate = 4;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 11;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		if (Time == 0f)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<RosePiece>());
				dust.velocity += base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedByRandom(0.6100000143051147) * 2.5f;
				dust.velocity.Y += Main.rand.NextFloat(-2.4f, 1.6f);
				dust.velocity *= 0.4f;
				dust.scale = Main.rand.NextFloat(1.2f, 1.7f);
				dust.noGravity = Main.rand.NextBool();
			}
		}
		if (base.Projectile.timeLeft == 24)
		{
			ExplodeIntoFireballs();
		}
		if (base.Projectile.timeLeft < 24)
		{
			base.Projectile.frame = (int)Math.Round(MathHelper.Lerp(4f, 7f, Utils.GetLerpValue(24f, 0f, base.Projectile.timeLeft, clamped: true)));
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.95f;
			if (Main.rand.NextBool())
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(4f, 4f), ModContent.DustType<LightDust>(), (-base.Projectile.velocity).RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.1f, 0.2f));
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(0.5f, 1f);
				dust2.color = FlameColor * 0.8f;
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, FlameColor, Color.DarkSlateGray, Main.rand.NextFloat(0.4f, 0.6f), 140f, Main.rand.NextFloat(-0.1f, 0.1f)));
			}
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, Color.Lerp(FlameColor, Color.DarkSlateGray, 0.3f), 12, Main.rand.NextFloat(0.3f, 0.4f), 0.6f, Main.rand.NextFloat(-0.1f, 0.1f), glowing: true));
			return;
		}
		if (Time % 2f == 1f && Time > 5f)
		{
			Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(16f, 16f), ModContent.DustType<LightDust>(), (-base.Projectile.velocity).RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.1f, 0.3f));
			dust3.noGravity = true;
			dust3.scale = Main.rand.NextFloat(0.8f, 1.5f);
			dust3.color = FlameColor;
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, FlameColor, Color.DarkSlateGray, Main.rand.NextFloat(1f, 1.5f), 180f, Main.rand.NextFloat(-0.1f, 0.1f)));
		}
		GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, FlameColor, 24, Main.rand.NextFloat(0.6f, 1f), 0.6f, Main.rand.NextFloat(-0.1f, 0.1f), glowing: true));
		Time++;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % 4;
	}

	public void ExplodeIntoFireballs()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.DD2_KoboldIgnite, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			int damage = (int)((float)base.Projectile.damage * 0.66f);
			float kb = base.Projectile.knockBack * 0.4f;
			float offsetAngle = Main.rand.NextFloatDirection() * 0.31f;
			for (float i = 0f; i < (float)Math.PI * 2f; i += 0.05f)
			{
				Vector2 velocity = (i + offsetAngle + MathHelper.ToRadians(45f)).ToRotationVector2() * (0.5f + (MathF.Sin(6f * i) + 1f) * 8f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), velocity);
				dust.noGravity = true;
				dust.color = Main.hslToRgb(Main.rand.NextFloat(0.05f, 0.15f), 1f, 0.66f);
			}
			for (int j = 0; j < 6; j++)
			{
				Vector2 shootVelocity = ((float)Math.PI * 2f * (float)j / 6f + offsetAngle).ToRotationVector2() * 8f;
				Vector2 flameSpawnPosition = base.Projectile.Center + shootVelocity;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), flameSpawnPosition, shootVelocity, ModContent.ProjectileType<PrinceFlameSmall>(), damage, kb, base.Projectile.owner);
			}
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
		((Color)(ref lightColor)).A = (byte)(((Color)(ref lightColor)).A / 4);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		if (timeLeft > 24)
		{
			ExplodeIntoFireballs();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
	}
}
