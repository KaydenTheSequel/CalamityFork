using System;
using System.Collections.Generic;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class LucreciaDNATrailCreator : ModProjectile, ILocalizedModType, IModType
{
	private List<Vector2> oldPositionsLeft = new List<Vector2>();

	private List<Vector2> oldPositionsRight = new List<Vector2>();

	private int trailTimer;

	private int middleStreakTimer = 40;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 200;
		base.Projectile.height = 170;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.hostile = false;
		base.Projectile.friendly = true;
		base.Projectile.extraUpdates = 12;
		base.Projectile.timeLeft = 2400;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.alpha = 255;
	}

	public override void AI()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		trailTimer++;
		float amplitude = 86f;
		float frequency = 0.039f;
		Vector2 perpendicular = Vector2.Normalize(new Vector2(0f - base.Projectile.velocity.Y, base.Projectile.velocity.X));
		float sineWave = (float)Math.Cos((float)trailTimer * frequency);
		Vector2 offsetLeft = perpendicular * amplitude * sineWave;
		Vector2 offsetRight = -perpendicular * amplitude * sineWave;
		Vector2 adjustedOffsetLeft = offsetLeft - new Vector2((float)base.Projectile.width, (float)base.Projectile.height);
		Vector2 adjustedOffsetRight = offsetRight - new Vector2((float)base.Projectile.width, (float)base.Projectile.height);
		oldPositionsLeft.Add(base.Projectile.Center + adjustedOffsetLeft);
		oldPositionsRight.Add(base.Projectile.Center + adjustedOffsetRight);
		middleStreakTimer--;
		if (middleStreakTimer <= 0)
		{
			middleStreakTimer = 6;
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(MathHelper.ToRadians(-170f)), "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 18, 0.24f, Color.MediumPurple * 1.3f, new Vector2(1f, 2.5f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.25f, 1f, 0.4f));
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + perpendicular, base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(MathHelper.ToRadians(170f)), "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 18, 0.24f, Color.CornflowerBlue * 1.3f, new Vector2(1f, 2.5f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.25f, 1f, 0.4f));
		}
		if (trailTimer % 7 == 0 && Main.rand.NextBool())
		{
			Vector2 position = base.Projectile.Center + offsetLeft;
			Vector2 blueTrailOrigin = base.Projectile.Center + offsetRight;
			SquishyLightParticle particle = new SquishyLightParticle(position, -base.Projectile.velocity.RotatedByRandom(0.39269909262657166) * Main.rand.NextFloat(2f, 4f), Main.rand.NextFloat(0.3f, 0.6f), Color.MediumPurple, Main.rand.Next(18, 46), 1f, 1.5f);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(blueTrailOrigin, -base.Projectile.velocity.RotatedByRandom(0.39269909262657166) * Main.rand.NextFloat(2f, 4f), Main.rand.NextFloat(0.3f, 0.6f), Color.CornflowerBlue, Main.rand.Next(18, 46), 1f, 1.5f));
			GeneralParticleHandler.SpawnParticle(particle);
		}
		int maxTrailLength = 150;
		if (oldPositionsLeft.Count > maxTrailLength)
		{
			oldPositionsLeft.RemoveAt(0);
		}
		if (oldPositionsRight.Count > maxTrailLength)
		{
			oldPositionsRight.RemoveAt(0);
		}
	}

	private float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.Lerp(12f, 0f, completionRatio);
	}

	private Color LeftColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Color val = Color.MediumPurple * 1.3f;
		float alphaScaling = -4f * completionRatio * (completionRatio - 1f);
		return val * alphaScaling;
	}

	private Color RightColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Color val = Color.CornflowerBlue * 1.3f;
		float alphaScaling = -4f * completionRatio * (completionRatio - 1f);
		return val * alphaScaling;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		MiscShaderData trailShader = GameShaders.Misc["CalamityMod:TrailStreak"];
		trailShader.SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(oldPositionsLeft, new PrimitiveSettings(WidthFunction, LeftColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 1f;
		}, smoothen: true, pixelate: false, trailShader));
		PrimitiveRenderer.RenderTrail(oldPositionsRight, new PrimitiveSettings(WidthFunction, RightColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 1f;
		}, smoothen: true, pixelate: false, trailShader));
		return false;
	}
}
