using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VenusianBolt : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public bool explode = true;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.ignoreWater = true;
		base.Projectile.extraUpdates = 7;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
		Lighting.AddLight(base.Projectile.Center, 0.25f, 0.2f, 0f);
		if (base.Projectile.timeLeft == 1)
		{
			explode = false;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item73, base.Projectile.Center);
			base.Projectile.localAI[0]++;
		}
		if (time == 5)
		{
			for (int i = 0; i < 12; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 278);
				dust.velocity = base.Projectile.velocity.RotatedByRandom(0.25) * Main.rand.NextFloat(0.1f, 1f);
				dust.scale = Main.rand.NextFloat(0.45f, 0.8f);
				dust.noGravity = true;
				dust.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Orange : Color.Coral, 0.7f);
			}
		}
		if (targetDist < 1400f)
		{
			if (time > 8 && time % 3 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(15f, 15f) - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 10f, -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.8f), affectedByGravity: false, 35, 0.8f, Color.Lerp(Color.OrangeRed, Color.Coral, Main.rand.NextFloat(0f, 1f))));
			}
			if (time > 6 && time % 2 == 0)
			{
				for (int j = 0; j < 2; j++)
				{
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 10f, -base.Projectile.velocity, affectedByGravity: false, 8, 0.08f * ((j == 0) ? 0.5f : 1f), Color.Lerp(Color.Orange, Color.Coral, 0.25f) * 0.5f, new Vector2(0.3f, 1f), quickShrink: false, glow: false, 0.8f));
				}
			}
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(189, 300);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		if (!explode)
		{
			return;
		}
		Main.player[base.Projectile.owner].SetScreenshake(3.5f);
		SoundStyle style = SoundID.DD2_ExplosiveTrapExplode with
		{
			Volume = 1f,
			Pitch = -0.5f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		style = SoundID.Item74 with
		{
			Volume = 0.7f,
			Pitch = 1f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i < 6; i++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Lerp(Color.Red, Color.Coral, Utils.GetLerpValue(-2f, 6f, i, clamped: true)), (i == 5) ? "CalamityMod/Particles/FlameExplosion" : "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 0.1f + 0.03f * (float)i, (int)(30f - (float)i * 1.3f), UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		for (int j = 0; j < 2; j++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OrangeRed, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 1f, 3f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.5f, 1.5f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		for (int k = 0; k < 20; k++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 278);
			dust.velocity = Utils.RotatedByRandom(new Vector2(9f, 9f), 100.0) * Main.rand.NextFloat(0.2f, 2f);
			dust.scale = Main.rand.NextFloat(0.65f, 1.25f);
			dust.noGravity = true;
			dust.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Orange : Color.Coral, 0.7f);
		}
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		int explosionDamage = base.Projectile.damage / 4;
		float explosionKB = 6f;
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<VenusianExplosion>(), explosionDamage, explosionKB, base.Projectile.owner);
		int cinderDamage = (int)((double)base.Projectile.damage * 0.02);
		float cinderKB = 0f;
		Vector2 cinderPos = base.Projectile.Center;
		int numCinders = 10;
		Vector2 cinderVel = default(Vector2);
		for (int l = 0; l < numCinders; l++)
		{
			((Vector2)(ref cinderVel))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			while (cinderVel.X == 0f && cinderVel.Y == 0f)
			{
				((Vector2)(ref cinderVel))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			}
			((Vector2)(ref cinderVel)).Normalize();
			cinderVel *= (float)Main.rand.Next(70, 101) * 0.1f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), cinderPos, cinderVel + new Vector2(0f, -3f), ModContent.ProjectileType<VenusianFlame>(), cinderDamage, cinderKB, base.Projectile.owner);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], Color.White);
		return true;
	}
}
