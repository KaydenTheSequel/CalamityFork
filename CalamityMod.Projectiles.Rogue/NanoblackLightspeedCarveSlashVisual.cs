using System;
using System.Collections.Generic;
using CalamityMod.Effects;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class NanoblackLightspeedCarveSlashVisual : ModProjectile, ILocalizedModType, IModType
{
	private const int VisualLifetime = 80;

	private const int GrowTime = 3;

	private const int FadeInTime = 1;

	private const int FadeOutTime = 5;

	private const int SlashDelay = 2;

	private const int HoldTime = 5;

	private const float FlashIntensity = 1.8f;

	private const float FlashDuration = 0.15f;

	private const float RangeMultiplier = 2.2f;

	private const float MinWidthStandard = 14f;

	private const float MaxWidthStandard = 46f;

	private const float MinWidthPerfect = 28f;

	private const float MaxWidthPerfect = 62f;

	private const float RevealSoftEdge = 0.25f;

	private const float RevealOvershoot = 0.22f;

	private const float RetreatSoftEdge = 0.18f;

	private const float RetreatOvershoot = 0.18f;

	private const int SlashResolution = 12;

	private const float LightningAmplitude = 2.5f;

	private const float LightningAmplitudePerfect = 3.5f;

	private const int LightningSubdivisions = 3;

	private const float LightningDecay = 0.45f;

	private const float LightningDriftSpeed = 4f;

	private const float MaxDepth = 40f;

	private const float DepthWobbleAmount = 8f;

	private const float DepthWobbleSpeed = 6f;

	private const float BreathingAmount = 0.06f;

	private const float BreathingSpeed = 9f;

	private const float CurveStrength = 0.12f;

	private const float CurveStrengthPerfect = 0.18f;

	private const int EchoCount = 1;

	private const float EchoOpacityFalloff = 0.4f;

	private const float EchoWidthFalloff = 0.75f;

	private const float EchoTimeLag = 4.5f;

	private const float GiantSlashLengthMult = 3.4f;

	private const float GiantSlashWidth = 120f;

	private const int GiantSlashGrowTime = 3;

	private const int GiantSlashHoldTime = 5;

	private const int GiantSlashFadeTime = 12;

	private const int GiantSlashDelay = 4;

	private const int GiantSlashStagger = 4;

	private const float GiantSlashScreenShake = 0f;

	private const float GiantSlashImpactFlash = 2.2f;

	private const float GiantSlashImpactFlashDuration = 3f;

	private static readonly BlendState GiantSlashDarkenBlend;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	private int GiantSlashCount => 1;

	internal bool IsPerfect => base.Projectile.ai[0] == 1f;

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.friendly = false;
		base.Projectile.hostile = false;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 80;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void AI()
	{
		if (!IsPerfect)
		{
			return;
		}
		float age = 80 - base.Projectile.timeLeft;
		int count = GiantSlashCount;
		for (int g = 0; g < count; g++)
		{
			float giantAge = GetGiantAge(age, g);
			if (giantAge >= 0f && giantAge < 1f)
			{
				Main.player[base.Projectile.owner].SetScreenshake(0f);
			}
		}
	}

	private static float Hash(float x)
	{
		float num = MathF.Sin(x * 127.1f) * 43758.547f;
		return num - MathF.Floor(num);
	}

	private List<Vector3> BuildSlashPath(Vector2 center, Vector2 direction, float halfLength, float curveAmount, float slashSeed, float growProgress, float time)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		List<Vector3> path = new List<Vector3>(12);
		Vector2 perp = direction.RotatedBy(1.5707963705062866);
		bool valid = true;
		for (int p = 0; p < 12; p++)
		{
			float s = (float)p / 11f * 2f - 1f;
			Vector2 basePos = center + direction * s * halfLength;
			float curveBulge = (1f - s * s) * curveAmount * halfLength;
			basePos += perp * curveBulge;
			float lightning = 0f;
			float amplitude = (IsPerfect ? 3.5f : 2.5f);
			float freq = 1f;
			for (int oct = 0; oct < 3; oct++)
			{
				float noiseInput = slashSeed * 13.7f + s * freq * 1.8f + (float)oct * 5.3f + time * 4f * (1f + (float)oct * 0.3f);
				lightning += (Hash(noiseInput) * 2f - 1f) * amplitude;
				amplitude *= 0.45f;
				freq *= 1.8f;
			}
			float envelopeWindow = 1f - s * s;
			lightning *= envelopeWindow * growProgress;
			basePos += perp * lightning;
			float depthWobble = MathF.Sin(time * 6f + slashSeed * 4.1f + s * 2f) * 8f;
			float z = (MathF.Sin(s * (float)Math.PI + slashSeed * 2.9f) * 40f + depthWobble) * (0.5f + 0.5f * growProgress);
			Vector2 screenPos = basePos - Main.screenPosition;
			if (float.IsNaN(screenPos.X) || float.IsNaN(screenPos.Y) || float.IsNaN(z) || float.IsInfinity(screenPos.X) || float.IsInfinity(screenPos.Y) || float.IsInfinity(z))
			{
				valid = false;
				break;
			}
			path.Add(new Vector3(screenPos, z));
		}
		if (!valid || path.Count < 2)
		{
			path.Clear();
			Vector2 start = center - direction * halfLength - Main.screenPosition;
			Vector2 end = center + direction * halfLength - Main.screenPosition;
			path.Add(new Vector3(start, 0f));
			path.Add(new Vector3(end, 0f));
		}
		return path;
	}

	private static float SlashWidthFunc(float progress, float baseWidth, float cracklePhase, float revealProgress, float retreatProgress)
	{
		float taperInLinear = MathHelper.Clamp(progress / 0.08f, 0f, 1f);
		float taperOutLinear = MathHelper.Clamp((1f - progress) / 0.08f, 0f, 1f);
		float num = 1f - MathF.Pow(1f - taperInLinear, 3f);
		float taperOut = 1f - MathF.Pow(1f - taperOutLinear, 3f);
		float taper = num * taperOut;
		float revealLinear = MathHelper.Clamp((revealProgress - progress) / 0.25f, 0f, 1f);
		float reveal = 1f - MathF.Pow(1f - revealLinear, 4f);
		float retreatRaw = MathHelper.Clamp((progress - retreatProgress + 0.18f) / 0.18f, 0f, 1f);
		float retreat = 1f - MathF.Pow(1f - retreatRaw, 4f);
		float crackle = 1f + 0.06f * MathF.Sin(progress * 13f + cracklePhase) + 0.04f * MathF.Sin(progress * 29f - cracklePhase * 1.7f);
		return baseWidth * taper * crackle * reveal * retreat;
	}

	private static float GiantSlashWidthFunc(float progress, float baseWidth, float revealProgress, float retreatProgress)
	{
		float num = 1f - MathF.Pow(1f - MathHelper.Clamp(progress / 0.06f, 0f, 1f), 4f);
		float taperOut = 1f - MathF.Pow(1f - MathHelper.Clamp((1f - progress) / 0.06f, 0f, 1f), 4f);
		float taper = num * taperOut;
		float centered = (progress - 0.5f) * 2f;
		float starProfile = MathF.Pow(1f - centered * centered, 0.35f);
		float starShape = MathHelper.Lerp(0.3f, 1f, starProfile);
		float revealLinear = MathHelper.Clamp((revealProgress - progress) / 0.2f, 0f, 1f);
		float reveal = 1f - MathF.Pow(1f - revealLinear, 5f);
		float retreatRaw = MathHelper.Clamp((progress - retreatProgress + 0.2f) / 0.2f, 0f, 1f);
		float retreat = 1f - MathF.Pow(1f - retreatRaw, 5f);
		return baseWidth * taper * starShape * reveal * retreat;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		int slashCount = (IsPerfect ? 12 : 8);
		float age = 80 - base.Projectile.timeLeft;
		Effect shader = CalamityShaders.NanoblackSlashShader.Value;
		float time = (float)Main.gameTimeCache.TotalGameTime.TotalSeconds;
		EffectParameter obj = shader.Parameters["uTime"];
		if (obj != null)
		{
			obj.SetValue(time);
		}
		Color c = (IsPerfect ? NanoblackReaper.LightspeedCarveColor1 : NanoblackReaper.NanoblackSlashColor2);
		EffectParameter obj2 = shader.Parameters["uColor"];
		if (obj2 != null)
		{
			obj2.SetValue(new Vector3((float)(int)((Color)(ref c)).R / 255f, (float)(int)((Color)(ref c)).G / 255f, (float)(int)((Color)(ref c)).B / 255f));
		}
		EffectParameter obj3 = shader.Parameters["uBrightness"];
		if (obj3 != null)
		{
			obj3.SetValue(1f);
		}
		Matrix view = Main.GameViewMatrix.ZoomMatrix;
		Matrix projection = Matrix.CreateOrthographicOffCenter(0f, (float)Main.screenWidth, (float)Main.screenHeight, 0f, -200f, 200f);
		float baseLength = NanoblackLightspeedCarve.HitboxRadius * 0.9f * 2.2f;
		float seed = (float)base.Projectile.identity * 1.913f;
		float curveBase = (IsPerfect ? 0.18f : 0.12f);
		GraphicsDevice device = ((Game)Main.instance).GraphicsDevice;
		using RenderTargetLease lease = RenderTargetPool.Shared.Rent(device, Main.screenWidth, Main.screenHeight, RenderTargetDescriptor.Default);
		Main.spriteBatch.End(out var ss);
		using (lease.Scope(preserveContents: true, Color.Transparent))
		{
			using SanePrimitiveRenderer.ShaderScope scope = SanePrimitiveRenderer.BeginShaderScope(shader, Matrix.Identity, in view, in projection, "uTransformMatrix", null, DepthStencilState.None, BlendState.Additive);
			DrawAllSlashes(scope, slashCount, age, time, seed, baseLength, curveBase);
			if (IsPerfect)
			{
				int giantCount = GiantSlashCount;
				for (int g = 0; g < giantCount; g++)
				{
					DrawGiantSlash(scope, age, time, seed, baseLength, g);
				}
			}
		}
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect)null, Matrix.Identity);
		Main.spriteBatch.Draw((Texture2D)(object)lease.Target, Vector2.Zero, Color.White);
		Main.spriteBatch.End();
		Effect value = CalamityShaders.GaussianBloomShader.Value;
		EffectParameter obj4 = value.Parameters["uSource"];
		if (obj4 != null)
		{
			obj4.SetValue(new Vector4((float)Main.screenWidth, (float)Main.screenHeight, 0f, 0f));
		}
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect)null, Matrix.Identity);
		value.CurrentTechnique.Passes["BloomShader"].Apply();
		Main.spriteBatch.Draw((Texture2D)(object)lease.Target, Vector2.Zero, Color.White);
		Main.spriteBatch.End();
		if (IsPerfect)
		{
			int giantCount2 = GiantSlashCount;
			for (int i = 0; i < giantCount2; i++)
			{
				DrawGiantSlashBlackCore(age, time, seed, baseLength, i);
			}
		}
		Main.spriteBatch.Begin(in ss);
		return false;
	}

	private void DrawAllSlashes(SanePrimitiveRenderer.ShaderScope scope, int slashCount, float age, float time, float seed, float baseLength, float curveBase)
	{
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < slashCount; i++)
		{
			float startDelay = i * 2;
			float growLinear = Utils.GetLerpValue(startDelay, startDelay + 3f, age, clamped: true);
			if (growLinear <= 0f)
			{
				continue;
			}
			MathF.Pow(1f - growLinear, 2.5f);
			float fadeIn = Utils.GetLerpValue(startDelay, startDelay + 1f, age, clamped: true);
			float f = (float)Math.PI * 2f * (float)i / (float)slashCount + seed * 0.1f;
			float lengthScale = 0.75f + 0.2f * MathF.Sin(seed + (float)i * 1.7f);
			float offsetScale = 0.25f * MathF.Cos(seed * 1.3f + (float)i * 2.1f);
			float widthInterpolant = 0.5f + 0.5f * MathF.Sin(seed * 0.7f + (float)i * 2.3f);
			float num = (IsPerfect ? 28f : 14f);
			float maxWidth = (IsPerfect ? 62f : 46f);
			float slashWidth = MathHelper.Lerp(num, maxWidth, widthInterpolant);
			Vector2 direction = f.ToRotationVector2();
			Vector2 offset = direction.RotatedBy(1.5707963705062866) * baseLength * offsetScale;
			Vector2 center = base.Projectile.Center + offset;
			float halfLength = baseLength * lengthScale;
			float slashSeed = seed + (float)i * 3.17f;
			float curveDirection = ((i % 2 == 0) ? 1f : (-1f));
			float curveSigned = curveBase * curveDirection * (0.7f + 0.6f * Hash(slashSeed * 0.43f));
			float num2 = startDelay + 3f + 5f;
			float retreatAge = Utils.GetLerpValue(num2, num2 + 5f, age, clamped: true);
			float slashFadeOut = 1f - retreatAge;
			if (slashFadeOut <= 0f)
			{
				continue;
			}
			float smoothFadeOut = MathF.Pow(slashFadeOut, 0.5f);
			float flashProgress = 1f - slashFadeOut;
			float flash = 1f;
			if (flashProgress > 0f && flashProgress < 0.15f)
			{
				float flashT = flashProgress / 0.15f;
				flash = 1f + 0.79999995f * (1f - MathF.Pow(1f - MathF.Sin(flashT * (float)Math.PI), 2f));
			}
			float widthCollapse = 0.7f + 0.3f * MathF.Pow(smoothFadeOut, 3f);
			float retreatProgress = MathF.Pow(1f - smoothFadeOut, 0.4f) * 1.1800001f;
			float colorShift = MathF.Pow(1f - smoothFadeOut, 2f);
			Color lightspeedCarveColor = NanoblackReaper.LightspeedCarveColor1;
			Color dyingColor = NanoblackReaper.LightspeedCarveColor2;
			Color baseColor = Color.Lerp(lightspeedCarveColor, dyingColor, colorShift);
			float num3 = MathF.Pow(MathHelper.Clamp(smoothFadeOut / 0.2f, 0f, 1f), 0.3f);
			float shimmer = 1f + 0.08f * MathF.Sin(time * 14f + slashSeed * 7.7f) + 0.05f * MathF.Sin(time * 23f - slashSeed * 4.1f);
			float opacity = num3 * fadeIn * flash * shimmer;
			float cracklePhase = slashSeed * 11f + time * 12f;
			float breathing = 1f + 0.06f * MathF.Sin(time * 9f + slashSeed * 3.3f);
			float fadedWidth = slashWidth * widthCollapse * breathing;
			for (int echo = 1; echo >= 0; echo--)
			{
				float echoAge = age - (float)echo * 4.5f;
				float echoGrowLinear = Utils.GetLerpValue(startDelay, startDelay + 3f, echoAge, clamped: true);
				if (!(echoGrowLinear <= 0f))
				{
					float echoGrow = 1f - MathF.Pow(1f - echoGrowLinear, 2.5f);
					float echoFadeIn = Utils.GetLerpValue(startDelay, startDelay + 1f, echoAge, clamped: true);
					float echoOpacity = opacity * echoFadeIn * MathF.Pow(0.4f, echo);
					float echoWidth = fadedWidth * MathF.Pow(0.75f, echo);
					float echoTime = time - (float)echo * 0.06f;
					float echoLength = halfLength * ((echo == 0) ? 1f : (0.97f - (float)echo * 0.02f));
					List<Vector3> path = BuildSlashPath(center, direction, echoLength, curveSigned, slashSeed, echoGrow, echoTime);
					Color echoTint = NanoblackReaper.NanoblackSlashColor2;
					Color echoColor = ((echo == 0) ? (baseColor * echoOpacity) : (Color.Lerp(echoTint, dyingColor, colorShift) * echoOpacity));
					float capturedEchoWidth = echoWidth;
					float capturedCrackle = cracklePhase - (float)echo * 5f;
					float capturedReveal = echoGrow * 1.22f;
					float capturedRetreat = retreatProgress;
					using PooledPrimitiveMesh mesh = TriangleStripBuilder.BuildStripPooled(path, (float progress) => SlashWidthFunc(progress, capturedEchoWidth, capturedCrackle, capturedReveal, capturedRetreat), echoColor, PrimitiveMeshCache.Shared, null, null, 2);
					scope.Draw(mesh.View);
				}
			}
		}
	}

	private void DrawGiantSlash(SanePrimitiveRenderer.ShaderScope scope, float age, float time, float seed, float baseLength, int giantIndex)
	{
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		float giantAge = GetGiantAge(age, giantIndex);
		if (!TryGetGiantTimeline(giantAge, out var growProgress, out var fadeLinear, out var smoothFade))
		{
			return;
		}
		float impactFlash = CalculateImpactFlash(giantAge);
		List<Vector3> path = BuildGiantSlashPath(time, seed, baseLength, giantIndex, growProgress);
		float widthCollapse = 0.6f + 0.4f * MathF.Pow(smoothFade, 3f);
		float impactWidthBoost = 1f + 0.15f * MathF.Max(impactFlash - 1f, 0f);
		float currentWidth = 120f * widthCollapse * impactWidthBoost;
		float opacity = MathF.Pow(MathHelper.Clamp(smoothFade / 0.15f, 0f, 1f), 0.3f) * MathF.Min(impactFlash, 2.5f);
		Color edgeColor = NanoblackReaper.LightspeedCarveColor1 * opacity;
		float retreatProgress = CalculateGiantRetreatProgress(fadeLinear);
		float capturedWidth = currentWidth;
		float capturedReveal = growProgress * 1.22f;
		float capturedRetreat = retreatProgress;
		using PooledPrimitiveMesh mesh = TriangleStripBuilder.BuildStripPooled(path, (float progress) => GiantSlashWidthFunc(progress, capturedWidth, capturedReveal, capturedRetreat), edgeColor, PrimitiveMeshCache.Shared, null, null, 2);
		scope.Draw(mesh.View);
	}

	private void DrawGiantSlashBlackCore(float age, float time, float seed, float baseLength, int giantIndex)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		if (!TryGetGiantTimeline(GetGiantAge(age, giantIndex), out var growProgress, out var fadeLinear, out var smoothFade))
		{
			return;
		}
		List<Vector3> path = BuildGiantSlashPath(time, seed, baseLength, giantIndex, growProgress);
		float widthCollapse = 0.6f + 0.4f * MathF.Pow(smoothFade, 3f);
		float coreWidth = 78f * widthCollapse;
		float opacity = MathF.Pow(MathHelper.Clamp(smoothFade / 0.15f, 0f, 1f), 0.3f);
		Color coreColor = Color.White * opacity;
		float retreatProgress = CalculateGiantRetreatProgress(fadeLinear);
		float capturedWidth = coreWidth;
		float capturedReveal = growProgress * 1.22f;
		float capturedRetreat = retreatProgress;
		Effect shader = CalamityShaders.NanoblackSlashShader.Value;
		EffectParameter obj = shader.Parameters["uColor"];
		if (obj != null)
		{
			obj.SetValue(new Vector3(1f, 1f, 1f));
		}
		EffectParameter obj2 = shader.Parameters["uBrightness"];
		if (obj2 != null)
		{
			obj2.SetValue(1.5f);
		}
		Matrix view = Main.GameViewMatrix.ZoomMatrix;
		Matrix projection = Matrix.CreateOrthographicOffCenter(0f, (float)Main.screenWidth, (float)Main.screenHeight, 0f, -200f, 200f);
		using SanePrimitiveRenderer.ShaderScope scope = SanePrimitiveRenderer.BeginShaderScope(shader, Matrix.Identity, in view, in projection, "uTransformMatrix", null, DepthStencilState.None, GiantSlashDarkenBlend);
		using PooledPrimitiveMesh mesh = TriangleStripBuilder.BuildStripPooled(path, (float progress) => GiantSlashWidthFunc(progress, capturedWidth, capturedReveal, capturedRetreat), coreColor, PrimitiveMeshCache.Shared, null, null, 2);
		scope.Draw(mesh.View);
		Color c = NanoblackReaper.LightspeedCarveColor1;
		EffectParameter obj3 = shader.Parameters["uColor"];
		if (obj3 != null)
		{
			obj3.SetValue(new Vector3((float)(int)((Color)(ref c)).R / 255f, (float)(int)((Color)(ref c)).G / 255f, (float)(int)((Color)(ref c)).B / 255f));
		}
		EffectParameter obj4 = shader.Parameters["uBrightness"];
		if (obj4 != null)
		{
			obj4.SetValue(1f);
		}
	}

	private static float CalculateGiantRetreatProgress(float fadeLinear)
	{
		if (!(fadeLinear > 0f))
		{
			return 0f;
		}
		return MathF.Pow(fadeLinear, 0.4f) * 1.2f;
	}

	private float GetGiantAge(float age, int giantIndex)
	{
		return age - 4f - (float)(giantIndex * 4);
	}

	private static bool TryGetGiantTimeline(float giantAge, out float growProgress, out float fadeLinear, out float smoothFade)
	{
		growProgress = 0f;
		fadeLinear = 0f;
		smoothFade = 0f;
		if (giantAge < 0f)
		{
			return false;
		}
		float growLinear = MathHelper.Clamp(giantAge / 3f, 0f, 1f);
		growProgress = 1f - MathF.Pow(1f - growLinear, 4f);
		float fadeStartAge = 8f;
		fadeLinear = MathHelper.Clamp((giantAge - fadeStartAge) / 12f, 0f, 1f);
		float fadeOut = 1f - fadeLinear;
		if (fadeOut <= 0f)
		{
			return false;
		}
		smoothFade = MathF.Pow(fadeOut, 0.5f);
		return true;
	}

	private static float CalculateImpactFlash(float giantAge)
	{
		if (giantAge >= 3f)
		{
			return 1f;
		}
		float flashT = 1f - giantAge / 3f;
		return 1f + 1.2f * flashT * flashT;
	}

	private List<Vector3> BuildGiantSlashPath(float time, float seed, float baseLength, int giantIndex, float growProgress)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		Vector2 direction = (Hash(seed * 2.71f + (float)giantIndex * 7.13f) * ((float)Math.PI * 2f)).ToRotationVector2();
		float halfLength = baseLength * 3.4f;
		float slashSeed = seed * 5.13f + (float)giantIndex * 11.7f;
		return BuildSlashPath(base.Projectile.Center, direction, halfLength, 0f, slashSeed, growProgress, time);
	}

	static NanoblackLightspeedCarveSlashVisual()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		GiantSlashDarkenBlend = new BlendState
		{
			ColorSourceBlend = (Blend)1,
			ColorDestinationBlend = (Blend)5,
			AlphaSourceBlend = (Blend)1,
			AlphaDestinationBlend = (Blend)5
		};
	}
}
