using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class TelluricGlareArrow : ModProjectile, ILocalizedModType, IModType
{
	private const int Lifetime = 180;

	public int time;

	public int fadeTime = 22;

	public bool colorAlt;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 21;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 24);
		base.Projectile.arrow = true;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 180;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.timeLeft >= 176)
		{
			return false;
		}
		return null;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Gold;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		if (time % 8 == 0 && time > 6)
		{
			bool isSpark = Main.rand.NextBool(3);
			Vector2 center2 = base.Projectile.Center;
			int type = (isSpark ? 278 : ModContent.DustType<LightDust>());
			Vector2? velocity = (base.Projectile.velocity * 2f).RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.2f, 1f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(center2, type, velocity, 0, newColor);
			dust.noGravity = true;
			dust.velocity *= (isSpark ? 0.5f : 1f);
			dust.scale = Main.rand.NextFloat(0.95f, 1.25f) * (isSpark ? 0.9f : 1f);
			dust.color = (Main.rand.NextBool(5) ? Color.Khaki : Color.Goldenrod);
			if (isSpark)
			{
				dust.noGravity = false;
			}
			else
			{
				dust.noLightEmittence = true;
			}
		}
		if (time == 4)
		{
			for (int i = 0; i < 5; i++)
			{
				if (i < 3)
				{
					Vector2 center3 = base.Projectile.Center;
					int type2 = ModContent.DustType<LightDust>();
					Vector2? velocity2 = base.Projectile.velocity.RotatedByRandom(0.800000011920929) * Main.rand.NextFloat(0.2f, 1f);
					newColor = default(Color);
					Dust dust2 = Dust.NewDustPerfect(center3, type2, velocity2, 0, newColor);
					dust2.noGravity = true;
					dust2.scale = Main.rand.NextFloat(0.85f, 1.15f);
					dust2.color = (Main.rand.NextBool(5) ? Color.Khaki : Color.Goldenrod);
					dust2.noLightEmittence = true;
				}
				else
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity.RotatedByRandom(0.800000011920929) * Main.rand.NextFloat(0.2f, 1f), "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 17, Main.rand.NextFloat(1.15f, 1.3f), Color.Lerp(Color.Orchid, Color.White, Main.rand.NextFloat(0f, 0.7f)), new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.3f, 0.4f)));
				}
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, base.Projectile.velocity.RotatedByRandom(0.800000011920929) * Main.rand.NextFloat(0.2f, 1f), affectedByGravity: false, 9, 0.017f, Color.Goldenrod, new Vector2(1.5f, 0.7f), quickShrink: true, glow: false, 1.3f));
			}
		}
		if (time == 0)
		{
			colorAlt = Main.rand.NextBool();
		}
		if (base.Projectile.timeLeft < 8 && fadeTime > 0)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.92f;
			base.Projectile.scale -= 0.023f;
			fadeTime--;
			base.Projectile.timeLeft++;
			if (fadeTime == 6)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 7, 0.012f, Color.Goldenrod, new Vector2(1.5f, 0.7f), quickShrink: true, glow: false, 2f));
			}
		}
		time++;
	}

	private void RestrictLifetime()
	{
		if (base.Projectile.timeLeft > 8)
		{
			base.Projectile.timeLeft = 8;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			RestrictLifetime();
			target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		RestrictLifetime();
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
		for (int i = 0; i < 3; i++)
		{
			GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center + Main.rand.NextVector2Circular(13f, 13f), base.Projectile.velocity * Main.rand.NextFloat(0.5f, 2.1f), affectedByGravity: false, 12, 1.1f, colorAlt ? (Main.rand.NextBool(5) ? Color.Khaki : Color.Goldenrod) : (Main.rand.NextBool(5) ? Color.Goldenrod : Color.DarkGoldenrod)));
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.8f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	private float PrimitiveWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float arrowheadCutoff = 0.36f;
		float width = 29f * base.Projectile.scale;
		float minHeadWidth = 0.02f;
		float maxHeadWidth = width;
		if (completionRatio <= arrowheadCutoff)
		{
			width = MathHelper.Lerp(minHeadWidth, maxHeadWidth, Utils.GetLerpValue(0f, arrowheadCutoff, completionRatio, clamped: true));
		}
		return width;
	}

	private Color PrimitiveColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		float endFadeRatio = 0.41f;
		float completionRatioFactor = 2.7f;
		float globalTimeFactor = 5.3f;
		float endFadeFactor = 3.2f;
		float endFadeTerm = Utils.GetLerpValue(0f, endFadeRatio * 0.5f, completionRatio, clamped: true) * endFadeFactor;
		float startingInterpolant = (float)Math.Cos(completionRatio * completionRatioFactor - Main.GlobalTimeWrappedHourly * globalTimeFactor + endFadeTerm) * 0.5f + 0.5f;
		float colorLerpFactor = 0.8f;
		Color val = Color.Lerp(colorAlt ? Color.DarkGoldenrod : Color.Goldenrod, Color.Khaki, startingInterpolant * colorLerpFactor);
		Color val2;
		if (!colorAlt)
		{
			val2 = Color.Goldenrod * 0.8f;
		}
		else
		{
			Color darkGoldenrod = Color.DarkGoldenrod;
			((Color)(ref darkGoldenrod)).A = 0;
			val2 = darkGoldenrod * 0.8f;
		}
		return Color.Lerp(val, val2, MathHelper.SmoothStep(0f, 1f, Utils.GetLerpValue(0f, endFadeRatio, completionRatio, clamped: true)));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		Vector2 overallOffset = base.Projectile.Size * 0.5f;
		overallOffset += base.Projectile.velocity * 1.4f;
		int numPoints = 92;
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveColorFunction, delegate
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return overallOffset;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), numPoints);
		return false;
	}
}
