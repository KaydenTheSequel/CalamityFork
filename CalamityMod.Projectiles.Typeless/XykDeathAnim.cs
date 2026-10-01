using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class XykDeathAnim : ModProjectile, ILocalizedModType, IModType
{
	public int endTime = 25;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public ref float time => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 1);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 32;
		base.Projectile.scale = 0.55f;
	}

	public override void AI()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		bool Orange = player.Calamity().XykVisualsOrange;
		Color effectColor = player.Calamity().XykFXColor;
		if (time == 0f)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/XykDie");
			style.Volume = 1f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (time < (float)endTime)
		{
			float fade = Utils.GetLerpValue(35f, 0f, time, clamped: true);
			float numberOfDusts = 3f;
			float rotFactor = 360f / numberOfDusts;
			for (int i = 0; (float)i < numberOfDusts; i++)
			{
				MathHelper.ToRadians((float)i * rotFactor);
				Vector2 velOffset = CalamityUtils.RandomVelocity(100f, 70f, 250f, 0.04f);
				velOffset *= Main.rand.NextFloat(15f, 30f) * fade;
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + velOffset * 2.5f, -velOffset * Main.rand.NextFloat(0.08f, 0.12f) * 1.5f, affectedByGravity: false, 14, Main.rand.NextFloat(1.1f, 1.25f) - 0.2f * fade, effectColor));
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + velOffset * 2.5f, 278, -velOffset * Main.rand.NextFloat(0.08f, 0.12f) * 1.5f, 0, default(Color), Main.rand.NextFloat(0.4f, 0.6f));
				dust.noGravity = true;
				dust.color = effectColor;
			}
		}
		if (time >= (float)endTime)
		{
			base.Projectile.scale *= 1.15f;
			for (int j = 0; j < 3; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, effectColor, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.7f * (float)(j + 1) * base.Projectile.scale, 1f * base.Projectile.scale, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.35f * (float)(j + 1) * base.Projectile.scale, 0.5f * base.Projectile.scale, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			for (int k = 0; k < 6; k++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, new Vector2((float)((!Orange) ? 1 : 0), (float)(Orange ? 1 : 0)) * -5f * (float)((k % 2 != 0) ? 1 : (-1)), affectedByGravity: false, 15, (0.08f - (float)k * 0.01f) * base.Projectile.scale, effectColor, new Vector2(5f, 0.8f), quickShrink: true, glow: false, 1.2f));
			}
			if (time == (float)endTime)
			{
				if (Orange)
				{
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, effectColor, "CalamityMod/Particles/GlowSquareParticleBig", Vector2.One, (float)Math.PI / 4f, 0f, 2.2f, 22, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, effectColor, "CalamityMod/Particles/GlowSquareParticleBig", Vector2.One, (float)Math.PI / 4f, 0f, 1.2f, 47, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
				else
				{
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, effectColor, "CalamityMod/Particles/HighResHollowCircleHardEdge", Vector2.One, (float)Math.PI / 4f, 0f, 0.3f, 22, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
				for (int l = 0; l < 30; l++)
				{
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Orange ? 267 : 278, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(2.5f, 15f));
					dust2.scale = Main.rand.NextFloat(0.85f, 1.15f) * (Orange ? 1f : 1.2f);
					dust2.noGravity = Orange;
					dust2.color = Color.Lerp(Color.White, effectColor, 0.5f);
					if (Orange)
					{
						GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(5.5f, 20f), effectColor, "CalamityMod/Particles/GlowSquareParticle", Vector2.One, (float)Math.PI / 4f, Main.rand.NextFloat(1.3f, 3.8f), 0.2f, 38, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					}
					else
					{
						GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(5.5f, 20f), "CalamityMod/Particles/Sparkle", affectedByGravity: false, 38, Main.rand.NextFloat(2.2f, 4.8f), effectColor, new Vector2(0.4f, Main.rand.NextFloat(0.9f, 1.4f)), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.1f));
					}
				}
			}
		}
		time++;
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.damage <= 0)
		{
			return false;
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}
}
