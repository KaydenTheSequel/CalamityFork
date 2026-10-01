using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CalamityMod.Effects;
using CalamityMod.Events;
using CalamityMod.Graphics.Primitives;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.Systems.Graphic;
using CalamityMod.Utilities.Daybreak;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Skies;

public class DoGSky : CustomSky
{
	public class RealityCrack
	{
		public Vector2 Position;

		public float Depth;

		public float Scale;

		public float Rotation;
	}

	public class DistortionRiftArm
	{
		public Vector2 ArmStartingPosition;

		public List<Vector2> ArmPoints;

		public float Depth;

		public float MaxWidth;

		public float MinDistanceBetweenPoints;

		public float MaxDistanceBetweenPoints;

		public float MinPointAngularVariance;

		public float MaxPointAngularVariance;

		public int TotalPoints;
	}

	public static readonly Color DoGTwlight;

	public static readonly Color DoGLightBlue;

	public bool Initialized;

	public int DoGIndex;

	public List<DistortionRiftArm> MainRiftCracks;

	public List<DistortionRiftArm> OuterRiftCracks;

	public List<RealityCrack> RealityCracks;

	public List<DistortionRiftArm> DistortionRiftArms;

	[CompilerGenerated]
	private static Color _003CDoGSkyColor_003Ek__BackingField;

	public static bool CanSkyBeActive { get; private set; }

	public static float SkyIntensity { get; private set; }

	public static Color DoGSkyColor
	{
		[CompilerGenerated]
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return _003CDoGSkyColor_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			_003CDoGSkyColor_003Ek__BackingField = value;
		}
	}

	public override void Update(GameTime gameTime)
	{
		GenerateRift();
		DoGIndex = NPC.FindFirstNPC(ModContent.NPCType<DevourerofGodsHead>());
		GetCorrectDoGColor();
		if (CanSkyBeActive)
		{
			SkyIntensity = MathHelper.Clamp(SkyIntensity + 0.05f, 0f, 1f);
		}
		else
		{
			SkyIntensity = MathHelper.Clamp(SkyIntensity - 0.05f, 0f, 1f);
		}
	}

	private void GenerateRift()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		Vector2 riftSpawnLocation = Main.LocalPlayer.Center - Vector2.UnitY * 2000f + Main.rand.NextVector2Circular(2000f, 2000f);
		if (Initialized)
		{
			return;
		}
		MainRiftCracks = new List<DistortionRiftArm>();
		OuterRiftCracks = new List<DistortionRiftArm>();
		RealityCracks = new List<RealityCrack>();
		DistortionRiftArms = new List<DistortionRiftArm>();
		int crackCount = 3;
		for (int i = 0; i < crackCount; i++)
		{
			RealityCrack realityCrack = new RealityCrack
			{
				Position = riftSpawnLocation,
				Depth = 30f,
				Scale = 1.55f * Main.rand.NextFloat(0.9f, 1.1f),
				Rotation = (float)i * ((float)Math.PI * 2f) / (float)crackCount
			};
			RealityCracks.Add(realityCrack);
		}
		for (int j = 0; j < 14; j++)
		{
			DistortionRiftArm distortionRiftArm = new DistortionRiftArm
			{
				ArmStartingPosition = riftSpawnLocation,
				Depth = 30f,
				TotalPoints = 8,
				MaxWidth = Main.rand.NextFloat(100f, 120f),
				MinDistanceBetweenPoints = 100f,
				MaxDistanceBetweenPoints = 140f,
				MinPointAngularVariance = -6f,
				MaxPointAngularVariance = 6f,
				ArmPoints = new List<Vector2>()
			};
			MainRiftCracks.Add(distortionRiftArm);
		}
		int outerCracksAmount = Main.rand.Next(12, 17);
		for (int k = 0; k < outerCracksAmount; k++)
		{
			DistortionRiftArm distortionRiftArm2 = new DistortionRiftArm
			{
				ArmStartingPosition = riftSpawnLocation,
				Depth = 30f,
				TotalPoints = Main.rand.Next(6, 10),
				MaxWidth = Main.rand.NextFloat(6f, 9f),
				MinDistanceBetweenPoints = 400f,
				MaxDistanceBetweenPoints = 500f,
				MinPointAngularVariance = Main.rand.Next(-10, -5),
				MaxPointAngularVariance = Main.rand.Next(5, 10),
				ArmPoints = new List<Vector2>()
			};
			OuterRiftCracks.Add(distortionRiftArm2);
		}
		for (int l = 0; l < MainRiftCracks.Count; l++)
		{
			float minDistance = MainRiftCracks[l].MinDistanceBetweenPoints * MainRiftCracks[l].Depth * 0.1f;
			float maxDistance = MainRiftCracks[l].MaxDistanceBetweenPoints * MainRiftCracks[l].Depth * 0.1f;
			float minAngularVariance = MainRiftCracks[l].MinPointAngularVariance;
			float maxAngularVariance = MainRiftCracks[l].MaxPointAngularVariance;
			float placementAngle = (float)l * ((float)Math.PI * 2f) / (float)MainRiftCracks.Count;
			if (MainRiftCracks[l].ArmPoints.Count <= 0)
			{
				MainRiftCracks[l].ArmPoints = GenerateDistortionRiftArmPoints(MainRiftCracks[l].ArmStartingPosition, MainRiftCracks[l].TotalPoints, minDistance, maxDistance, minAngularVariance, maxAngularVariance, placementAngle);
			}
			DistortionRiftArms.Add(MainRiftCracks[l]);
		}
		for (int m = 0; m < OuterRiftCracks.Count; m++)
		{
			float minDistance2 = OuterRiftCracks[m].MinDistanceBetweenPoints * OuterRiftCracks[m].Depth * 0.1f;
			float maxDistance2 = OuterRiftCracks[m].MaxDistanceBetweenPoints * OuterRiftCracks[m].Depth * 0.1f;
			float minAngularVariance2 = OuterRiftCracks[m].MinPointAngularVariance;
			float maxAngularVariance2 = OuterRiftCracks[m].MaxPointAngularVariance;
			float placementAngle2 = (float)m * ((float)Math.PI * 2f) / (float)OuterRiftCracks.Count;
			if (OuterRiftCracks[m].ArmPoints.Count <= 0)
			{
				OuterRiftCracks[m].ArmPoints = GenerateDistortionRiftArmPoints(OuterRiftCracks[m].ArmStartingPosition, OuterRiftCracks[m].TotalPoints, minDistance2, maxDistance2, minAngularVariance2, maxAngularVariance2, placementAngle2);
			}
			DistortionRiftArms.Add(OuterRiftCracks[m]);
		}
		Initialized = true;
	}

	private static List<Vector2> GenerateDistortionRiftArmPoints(Vector2 startingPosition, int totalPoints, float minDistanceBetweenPoints, float maxDistanceBetweenPoints, float minAngularVariance, float maxAngularVariance, float? optionalPointPlacementAngle = null)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		List<Vector2> points = new List<Vector2>();
		for (int j = 0; j < totalPoints; j++)
		{
			switch (j)
			{
			case 0:
				points.Add(startingPosition);
				continue;
			case 1:
			{
				float distanceFromLastPoint = Main.rand.NextFloat(minDistanceBetweenPoints, maxDistanceBetweenPoints);
				Vector2 nextPointPosition = startingPosition + Main.rand.NextVector2Unit() * distanceFromLastPoint;
				if (optionalPointPlacementAngle.HasValue)
				{
					nextPointPosition = startingPosition + Vector2.UnitX.RotatedBy(optionalPointPlacementAngle.Value) * distanceFromLastPoint;
				}
				points.Add(nextPointPosition);
				continue;
			}
			}
			Vector2 previousPoint = points[j - 2];
			float distanceFromLastPoint2 = Main.rand.NextFloat(minDistanceBetweenPoints, maxDistanceBetweenPoints);
			if (j == totalPoints - 1)
			{
				distanceFromLastPoint2 = Main.rand.NextFloat(minDistanceBetweenPoints, maxDistanceBetweenPoints) * 0.5f;
			}
			Vector2 newPoint = points[j - 1] + (previousPoint.DirectionTo(points[j - 1]) * distanceFromLastPoint2).RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(minAngularVariance, maxAngularVariance) * (float)j * 0.5f));
			points.Add(newPoint);
		}
		return points;
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.gameMenu && CalamityClientConfig.Instance.FancyBackgroundVisuals && !BossRushEvent.BossRushActive)
		{
			spriteBatch.End();
			spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, Main.Rasterizer, (Effect)null, CalamityUtils.BackgroundMatrix);
			if (minDepth >= float.MinValue && maxDepth <= 5f)
			{
				DrawDistortionWinds(spriteBatch);
			}
			if (minDepth >= 3f && maxDepth <= 10f)
			{
				DrawRollingBackgroundFog(spriteBatch);
			}
			spriteBatch.End();
			spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.None, Main.Rasterizer, (Effect)null, CalamityUtils.BackgroundMatrix);
			if (minDepth >= 6f && maxDepth <= float.MaxValue)
			{
				DrawRiftAura(spriteBatch);
			}
			if (minDepth >= 6f && maxDepth <= float.MaxValue)
			{
				DrawRealityCracks(spriteBatch);
			}
			spriteBatch.End();
			spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, CalamityUtils.BackgroundMatrix);
			if (minDepth >= 6f && maxDepth <= float.MaxValue)
			{
				DrawDistortionRift(spriteBatch);
			}
			spriteBatch.End();
			spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, CalamityUtils.BackgroundMatrix);
		}
	}

	public void DrawRiftToRenderTarget()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		if (DistortionRiftArms == null || DistortionRiftArms.Count == 0)
		{
			return;
		}
		Vector2 screenSize = default(Vector2);
		((Vector2)(ref screenSize))._002Ector((float)Main.screenWidth, (float)Main.screenHeight);
		Vector2 screenCenter = Main.screenPosition + screenSize * 0.5f;
		int i;
		Vector2 depthFactor = default(Vector2);
		for (i = 0; i < DistortionRiftArms.Count; i++)
		{
			((Vector2)(ref depthFactor))._002Ector(1f / DistortionRiftArms[i].Depth, 0.9f / DistortionRiftArms[i].Depth);
			List<Vector2> parallaxedPoints = new List<Vector2>();
			for (int j = 0; j < DistortionRiftArms[i].ArmPoints.Count; j++)
			{
				parallaxedPoints.Add((DistortionRiftArms[i].ArmPoints[j] - screenCenter) * depthFactor + screenCenter);
			}
			PrimitiveRenderer.RenderTrail(parallaxedPoints, new PrimitiveSettings((float completion, Vector2 _) => MathHelper.Lerp(0f, DistortionRiftArms[i].MaxWidth, 1f - completion) * SkyIntensity, delegate
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_000a: Unknown result type (might be due to invalid IL or missing references)
				return Color.White * 1f;
			}, null, smoothen: false, pixelate: false, null, useUnscaledMatrices: true), DistortionRiftArms[i].TotalPoints + 16);
		}
	}

	private void DrawDistortionWinds(SpriteBatch spriteBatch)
	{
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		Effect value = CalamityShaders.DoGDistortionWindsShader.Value;
		Texture2D windsTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Neurons2", (AssetRequestMode)2).Value;
		Texture2D highlightsTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/SharpNoise", (AssetRequestMode)2).Value;
		Texture2D distortionTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/MeltyNoise", (AssetRequestMode)2).Value;
		Texture2D erosionTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Pebbles", (AssetRequestMode)2).Value;
		Vector2 screenSize = default(Vector2);
		((Vector2)(ref screenSize))._002Ector((float)Main.screenWidth, (float)Main.screenHeight);
		value.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly);
		value.Parameters["overallOpacity"].SetValue(SkyIntensity);
		value.Parameters["distortionStrength"].SetValue(0.3f);
		value.Parameters["mainNoiseTextureScale"].SetValue(0.8f);
		value.Parameters["distortionTextureScale"].SetValue(0.6f);
		value.Parameters["erosionTextureScale"].SetValue(2f);
		value.Parameters["erosionMin"].SetValue(0.38f * SkyIntensity);
		value.Parameters["gradientPrecision"].SetValue(20f);
		value.Parameters["pixelationFactor"].SetValue(screenSize * 0.5f);
		value.Parameters["worldOffset"].SetValue(Main.screenPosition / windsTexture.Size() * 0.025f);
		EffectParameter obj = value.Parameters["darkerPixelColor"];
		Color val = Color.Lerp(Color.DarkGray, Color.Black, 0.82f);
		obj.SetValue(((Color)(ref val)).ToVector3());
		EffectParameter obj2 = value.Parameters["brighterPixelColor"];
		val = Color.Lerp(Color.Black, DoGSkyColor, 0.32f);
		obj2.SetValue(((Color)(ref val)).ToVector3());
		EffectParameter obj3 = value.Parameters["highlightsColor"];
		val = DoGSkyColor;
		obj3.SetValue(((Color)(ref val)).ToVector3());
		((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)highlightsTexture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
		((Game)Main.instance).GraphicsDevice.Textures[2] = (Texture)(object)distortionTexture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[2] = SamplerState.LinearWrap;
		((Game)Main.instance).GraphicsDevice.Textures[3] = (Texture)(object)erosionTexture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[3] = SamplerState.LinearWrap;
		value.CurrentTechnique.Passes[0].Apply();
		spriteBatch.Draw(windsTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
	}

	private void DrawRollingBackgroundFog(SpriteBatch spriteBatch)
	{
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		Effect value = CalamityShaders.DoGBackgroundFogShader.Value;
		Texture2D cloudsTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/RealisticClouds", (AssetRequestMode)2).Value;
		Texture2D erosionTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/HarshNoise", (AssetRequestMode)2).Value;
		Vector2 screenSize = default(Vector2);
		((Vector2)(ref screenSize))._002Ector((float)Main.screenWidth, (float)Main.screenHeight);
		value.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly * 3f);
		value.Parameters["overallOpacity"].SetValue(SkyIntensity * 0.25f);
		value.Parameters["distortionStrength"].SetValue(0.12f);
		value.Parameters["mainNoiseTextureScale"].SetValue(0.6f);
		value.Parameters["distortionTextureScale"].SetValue(0.8f);
		value.Parameters["erosionTextureScale"].SetValue(0.26f);
		value.Parameters["erosionMin"].SetValue(0.06f * SkyIntensity);
		value.Parameters["gradientPrecision"].SetValue(20f);
		value.Parameters["pixelationFactor"].SetValue(screenSize * 0.5f);
		value.Parameters["worldOffset"].SetValue(Main.screenPosition / cloudsTexture.Size() * 0.005f);
		EffectParameter obj = value.Parameters["darkerPixelColor"];
		Color val = Color.Lerp(Color.Black, Color.DarkGray, 0.75f);
		obj.SetValue(((Color)(ref val)).ToVector3());
		EffectParameter obj2 = value.Parameters["brighterPixelColor"];
		val = Color.Lerp(Color.DarkGray, DoGSkyColor, 0.8f);
		obj2.SetValue(((Color)(ref val)).ToVector3());
		((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)cloudsTexture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[1] = SamplerState.LinearWrap;
		((Game)Main.instance).GraphicsDevice.Textures[2] = (Texture)(object)erosionTexture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[2] = SamplerState.LinearWrap;
		value.CurrentTechnique.Passes[0].Apply();
		spriteBatch.Draw(cloudsTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
	}

	private void DrawRiftAura(SpriteBatch spriteBatch)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		if (RealityCracks != null && RealityCracks.Count != 0)
		{
			Effect value = CalamityShaders.DoGRiftAuraShader.Value;
			Texture2D riftAuraTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Smudges", (AssetRequestMode)2).Value;
			Texture2D distortionTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Swirls", (AssetRequestMode)2).Value;
			Vector2 screenSize = default(Vector2);
			((Vector2)(ref screenSize))._002Ector((float)Main.screenWidth, (float)Main.screenHeight);
			Vector2 screenCenter = Main.screenPosition + screenSize * 0.5f;
			value.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly);
			value.Parameters["pixelSize"].SetValue(screenSize * 0.5f);
			value.Parameters["distortionStrength"].SetValue(0.78f);
			value.Parameters["opacityCutoffValue"].SetValue(0.675f * SkyIntensity);
			value.Parameters["fadeoutPower"].SetValue(1.25f);
			value.Parameters["overallOpacity"].SetValue(0.6f);
			value.Parameters["minBrightnessValue"].SetValue(0f);
			EffectParameter obj = value.Parameters["brighterPixelColor"];
			Color doGSkyColor = DoGSkyColor;
			obj.SetValue(((Color)(ref doGSkyColor)).ToVector3());
			value.Parameters["gradientPrecision"].SetValue(8f);
			((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)distortionTexture;
			((Game)Main.instance).GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
			value.CurrentTechnique.Passes[0].Apply();
			Vector2 depthFactor = default(Vector2);
			((Vector2)(ref depthFactor))._002Ector(1f / RealityCracks[0].Depth, 0.9f / RealityCracks[0].Depth);
			Vector2 drawPosition = (RealityCracks[0].Position - screenCenter) * depthFactor + screenCenter - Main.screenPosition;
			spriteBatch.Draw(riftAuraTexture, drawPosition, (Rectangle?)null, Color.White, Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f) / 270f, riftAuraTexture.Size() * 0.5f, 1f, (SpriteEffects)0, 0f);
		}
	}

	private void DrawRealityCracks(SpriteBatch spriteBatch)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		if (RealityCracks != null && RealityCracks.Count != 0)
		{
			Effect value = CalamityShaders.DoGRealityCrackShader.Value;
			Texture2D cracksTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/CrackedGlass_Glowing", (AssetRequestMode)2).Value;
			Vector2 screenSize = default(Vector2);
			((Vector2)(ref screenSize))._002Ector((float)Main.screenWidth, (float)Main.screenHeight);
			Vector2 screenCenter = Main.screenPosition + screenSize * 0.5f;
			value.Parameters["opacityCutoffValue"].SetValue(0.8f * SkyIntensity);
			value.Parameters["fadeoutPower"].SetValue(1f);
			value.Parameters["overallOpacity"].SetValue(0.1015f);
			value.Parameters["minBrightnessValue"].SetValue(0.6f);
			EffectParameter obj = value.Parameters["darkerPixelColor"];
			Color val = DoGSkyColor;
			obj.SetValue(((Color)(ref val)).ToVector3());
			EffectParameter obj2 = value.Parameters["brighterPixelColor"];
			val = Color.White;
			obj2.SetValue(((Color)(ref val)).ToVector3());
			value.CurrentTechnique.Passes[0].Apply();
			Vector2 depthFactor = default(Vector2);
			for (int i = 0; i < RealityCracks.Count; i++)
			{
				((Vector2)(ref depthFactor))._002Ector(1f / RealityCracks[i].Depth, 0.9f / RealityCracks[i].Depth);
				Vector2 drawPosition = (RealityCracks[i].Position - screenCenter) * depthFactor + screenCenter - Main.screenPosition;
				spriteBatch.Draw(cracksTexture, drawPosition, (Rectangle?)null, Color.White, RealityCracks[i].Rotation, cracksTexture.Size() * 0.5f, RealityCracks[i].Scale, (SpriteEffects)0, 0f);
			}
		}
	}

	private void DrawDistortionRift(SpriteBatch spriteBatch)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		using (spriteBatch.Scope())
		{
			spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, (RasterizerState)null, CalamityShaders.MetaballEdgeShader.Value, Main.BackgroundViewMatrix.TransformationMatrix);
			Asset<Effect> metaballEdgeShader = CalamityShaders.MetaballEdgeShader;
			Texture2D distortionRiftContents = (Texture2D)(object)DoGVisualsManager.DistortionRiftBackgroundContentsTarget.Target;
			Texture2D distortionRift = (Texture2D)(object)DoGVisualsManager.DistortionRiftPrimitivesTarget.Target;
			Viewport viewport = Main.graphics.graphicsDevice.Viewport;
			float num = ((Viewport)(ref viewport)).Width;
			viewport = Main.graphics.graphicsDevice.Viewport;
			Vector2 screenSize = default(Vector2);
			((Vector2)(ref screenSize))._002Ector(num, (float)((Viewport)(ref viewport)).Height);
			EffectParameter obj = metaballEdgeShader.Value.Parameters["layerSize"];
			if (obj != null)
			{
				obj.SetValue(distortionRiftContents.Size());
			}
			EffectParameter obj2 = metaballEdgeShader.Value.Parameters["screenSize"];
			if (obj2 != null)
			{
				obj2.SetValue(screenSize);
			}
			EffectParameter obj3 = metaballEdgeShader.Value.Parameters["layerOffset"];
			if (obj3 != null)
			{
				obj3.SetValue(Vector2.Zero);
			}
			EffectParameter obj4 = metaballEdgeShader.Value.Parameters["singleFrameScreenOffset"];
			if (obj4 != null)
			{
				obj4.SetValue(Vector2.Zero);
			}
			EffectParameter obj5 = metaballEdgeShader.Value.Parameters["edgeColor"];
			Color val;
			if (obj5 != null)
			{
				val = Color.Lerp(Color.Lerp(DoGSkyColor, Color.White, 0.6f), Color.Black, 0.15f);
				obj5.SetValue(((Color)(ref val)).ToVector4());
			}
			EffectParameter obj6 = metaballEdgeShader.Value.Parameters["layerColor"];
			if (obj6 != null)
			{
				val = DoGSkyColor;
				obj6.SetValue(((Color)(ref val)).ToVector4());
			}
			((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)distortionRiftContents;
			((Game)Main.instance).GraphicsDevice.SamplerStates[1] = SamplerState.LinearWrap;
			metaballEdgeShader.Value.CurrentTechnique.Passes[0].Apply();
			spriteBatch.Draw(distortionRift, new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y), Color.White);
			Main.spriteBatch.End();
		}
	}

	private void GetCorrectDoGColor()
	{
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		if (DoGIndex != -1)
		{
			DevourerofGodsHead DoG = Main.npc[DoGIndex].ModNPC<DevourerofGodsHead>();
			Color goalSkyColor = Color.Black;
			if (DoG.isInAgressiveState)
			{
				goalSkyColor = Color.Fuchsia;
			}
			if (DoG.isInPassiveState)
			{
				goalSkyColor = DoGLightBlue;
			}
			if (DoG.isInLaserWallState || DoG.isInPostWallState || DoG.postTeleportTimer > 0 || DoG.teleportTimer > 0 || (DoG.NPC.localAI[2] < 180f && DoG.NPC.localAI[2] > 60f))
			{
				goalSkyColor = DoGTwlight;
			}
			DoGSkyColor = Color.Lerp(DoGSkyColor, goalSkyColor, 0.1f);
		}
		else
		{
			Color goalSkyColor2 = Color.Black;
			if (Main.LocalPlayer.Calamity().monolithDevourerPShader > 0)
			{
				goalSkyColor2 = Color.Fuchsia;
			}
			if (Main.LocalPlayer.Calamity().monolithDevourerBShader > 0)
			{
				goalSkyColor2 = DoGLightBlue;
			}
			DoGSkyColor = Color.Lerp(DoGSkyColor, goalSkyColor2, 0.1f);
		}
	}

	public override Color OnTileColor(Color inColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(inColor, DoGSkyColor, SkyIntensity);
	}

	public override float GetCloudAlpha()
	{
		return 1f;
	}

	public override void Activate(Vector2 position, params object[] args)
	{
		CanSkyBeActive = true;
	}

	public override bool IsActive()
	{
		if (!CanSkyBeActive)
		{
			return SkyIntensity > 0f;
		}
		return true;
	}

	public override void Deactivate(params object[] args)
	{
		CanSkyBeActive = false;
		if (SkyIntensity <= 0.1f)
		{
			Initialized = false;
			DistortionRiftArms?.Clear();
			RealityCracks?.Clear();
		}
	}

	public override void Reset()
	{
		CanSkyBeActive = false;
		Initialized = false;
		DistortionRiftArms?.Clear();
		RealityCracks?.Clear();
	}

	static DoGSky()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		DoGTwlight = new Color(147, 24, 204);
		DoGLightBlue = new Color(0, 221, 250);
	}
}
