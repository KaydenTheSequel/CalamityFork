using System.Linq;
using CalamityMod.Effects;
using CalamityMod.Graphics;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.Skies;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Graphic;

public class DoGVisualsManager : ModSystem
{
	public float FillProgress;

	public float BackgroundLightningFill;

	public float BackgroundLightningMax;

	public float BackgroundLightningTimer;

	public static bool ShouldDrawToBackgroundTargets => DoGSky.SkyIntensity > 0f;

	public static bool ShouldDrawToForegroundTarget => MetaballManager.metaballs.Any((Metaball m) => m.AnythingToDraw && m is DoGDistortionMetaball);

	public static RenderTargetLease DistortionRiftBackgroundContentsTarget { get; private set; }

	public static RenderTargetLease DistortionRiftPrimitivesTarget { get; private set; }

	public static RenderTargetLease DistortionForegroundContentsTarget { get; private set; }

	public override void OnModLoad()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		if (!Main.dedServ)
		{
			On_Main.DrawSunAndMoon += new hook_DrawSunAndMoon(DiscardCelestialObjects);
		}
	}

	public override void PostSetupContent()
	{
		if (!Main.dedServ)
		{
			Main.QueueMainThreadAction(delegate
			{
				DistortionRiftBackgroundContentsTarget = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice);
				DistortionRiftPrimitivesTarget = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice);
				DistortionForegroundContentsTarget = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice);
			});
			RenderTargetManager.RenderTargetUpdateLoopEvent += PrepareTargets;
		}
	}

	public override void OnModUnload()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		if (!Main.dedServ)
		{
			On_Main.DrawSunAndMoon -= new hook_DrawSunAndMoon(DiscardCelestialObjects);
			RenderTargetManager.RenderTargetUpdateLoopEvent -= PrepareTargets;
		}
	}

	public override void PostUpdateEverything()
	{
		if (!(DoGSky.SkyIntensity > 0.1f))
		{
			return;
		}
		if (SkyManager.Instance["Ambience"].IsActive())
		{
			SkyManager.Instance["Ambience"].Deactivate();
		}
		if (!CalamityClientConfig.Instance.FancyBackgroundVisuals)
		{
			return;
		}
		if (Main.rand.NextBool(200) && DoGSky.SkyIntensity > 0f && BackgroundLightningTimer <= 0f)
		{
			BackgroundLightningTimer = (Main.rand.NextBool(10) ? Main.rand.Next(30, 45) : Main.rand.Next(5, 20));
			BackgroundLightningMax = Main.rand.NextFloat(0.7f, 0.9f);
		}
		if (BackgroundLightningTimer > 0f)
		{
			float minFill = BackgroundLightningMax * 0.5f;
			BackgroundLightningFill = Main.rand.NextFloat(minFill, BackgroundLightningMax);
			BackgroundLightningTimer--;
			return;
		}
		if (BackgroundLightningTimer < 0f)
		{
			BackgroundLightningTimer = 0f;
		}
		BackgroundLightningFill = MathHelper.Lerp(BackgroundLightningFill, 0f, 0.05f);
	}

	public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		if (DoGSky.SkyIntensity > 0f)
		{
			FillProgress += 0.05f;
		}
		else
		{
			FillProgress -= 0.05f;
		}
		FillProgress = MathHelper.Clamp(FillProgress, 0f, 1f);
		if (FillProgress > 0f)
		{
			Color colorToUse = DoGSky.DoGSkyColor;
			float tileBrightness = (CalamityClientConfig.Instance.FancyBackgroundVisuals ? 35f : 190f);
			float num = (int)((Color)(ref Main.ColorOfTheSkies)).R;
			Color white = Color.White;
			((Color)(ref backgroundColor)).R = (byte)MathHelper.Lerp(num, (float)(int)((Color)(ref white)).R * 0.035f, FillProgress);
			float num2 = (int)((Color)(ref Main.ColorOfTheSkies)).G;
			white = Color.White;
			((Color)(ref backgroundColor)).G = (byte)MathHelper.Lerp(num2, (float)(int)((Color)(ref white)).G * 0.035f, FillProgress);
			float num3 = (int)((Color)(ref Main.ColorOfTheSkies)).B;
			white = Color.White;
			((Color)(ref backgroundColor)).B = (byte)MathHelper.Lerp(num3, (float)(int)((Color)(ref white)).B * 0.035f, FillProgress);
			backgroundColor = new Color(((Color)(ref backgroundColor)).ToVector3() + ((Color)(ref colorToUse)).ToVector3() * 0.025f * FillProgress);
			((Color)(ref tileColor)).R = (byte)MathHelper.Lerp((float)(int)((Color)(ref Main.ColorOfTheSkies)).R, tileBrightness, FillProgress);
			((Color)(ref tileColor)).G = (byte)MathHelper.Lerp((float)(int)((Color)(ref Main.ColorOfTheSkies)).G, tileBrightness, FillProgress);
			((Color)(ref tileColor)).B = (byte)MathHelper.Lerp((float)(int)((Color)(ref Main.ColorOfTheSkies)).B, tileBrightness, FillProgress);
			float tileColorAdditive = (CalamityClientConfig.Instance.FancyBackgroundVisuals ? 0.075f : 0.4f);
			tileColor = new Color(((Color)(ref tileColor)).ToVector3() + ((Color)(ref colorToUse)).ToVector3() * tileColorAdditive * FillProgress);
		}
	}

	private void DiscardCelestialObjects(orig_DrawSunAndMoon orig, Main self, Main.SceneArea sceneArea, Color moonColor, Color sunColor, float tempMushroomInfluence)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (DoGSky.SkyIntensity > 0f)
		{
			tempMushroomInfluence = DoGSky.SkyIntensity;
			moonColor *= 1f - DoGSky.SkyIntensity;
		}
		orig.Invoke(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);
	}

	private void PrepareTargets()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		if (ShouldDrawToBackgroundTargets)
		{
			using (DistortionRiftBackgroundContentsTarget.Scope(preserveContents: true, Color.Transparent))
			{
				DrawDistortionRiftBackground();
			}
			using (DistortionRiftPrimitivesTarget.Scope(preserveContents: true, Color.Transparent))
			{
				Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, Main.Rasterizer, (Effect)null, CalamityUtils.BackgroundMatrix);
				if (SkyManager.Instance["CalamityMod:DevourerofGodsHead"].IsActive())
				{
					(SkyManager.Instance["CalamityMod:DevourerofGodsHead"] as DoGSky).DrawRiftToRenderTarget();
				}
				Main.spriteBatch.End();
			}
		}
		if (ShouldDrawToForegroundTarget)
		{
			using (DistortionForegroundContentsTarget.Scope(preserveContents: true, Color.Transparent))
			{
				DrawDistortionForeground();
			}
		}
	}

	private void DrawDistortionRiftBackground()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, Main.Rasterizer, (Effect)null, CalamityUtils.BackgroundMatrix);
		DrawDistortionLightningBackdrop_Background();
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, Main.Rasterizer, (Effect)null, CalamityUtils.BackgroundMatrix);
		DrawDistortionClouds_Background();
		Main.spriteBatch.End();
	}

	private void DrawDistortionLightningBackdrop_Background()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Color backdropColor = Color.Lerp(Color.Lerp(Color.Black, Color.White, BackgroundLightningFill), DoGSky.DoGSkyColor, 0.25f);
		Main.spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), backdropColor);
	}

	private void DrawDistortionClouds_Background()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		Effect value = CalamityShaders.DoGBackgroundFogShader.Value;
		Texture2D cloudsTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/RealisticClouds", (AssetRequestMode)2).Value;
		Texture2D distortionTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Neurons2", (AssetRequestMode)2).Value;
		Texture2D erosionTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/HarshNoise", (AssetRequestMode)2).Value;
		Vector2 screenSize = default(Vector2);
		((Vector2)(ref screenSize))._002Ector((float)Main.screenWidth, (float)Main.screenHeight);
		Color darkPixelColor = Color.Lerp(Color.Lerp(Color.Black, Color.DarkGray, 0.3f), Color.Black, BackgroundLightningFill * 0.8f);
		Color brightPixelColor = Color.Lerp(Color.Lerp(Color.Black, DoGSky.DoGSkyColor, 0.6f), Color.Black, BackgroundLightningFill * 0.8f);
		value.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly);
		value.Parameters["overallOpacity"].SetValue(1f);
		value.Parameters["distortionStrength"].SetValue(0.24f);
		value.Parameters["mainNoiseTextureScale"].SetValue(2f);
		value.Parameters["distortionTextureScale"].SetValue(0.8f);
		value.Parameters["erosionTextureScale"].SetValue(0.26f);
		value.Parameters["erosionMin"].SetValue(0.06f);
		value.Parameters["gradientPrecision"].SetValue(20f);
		value.Parameters["pixelationFactor"].SetValue(screenSize * 0.5f);
		value.Parameters["worldOffset"].SetValue(Main.screenPosition / cloudsTexture.Size() * 0.001f);
		value.Parameters["darkerPixelColor"].SetValue(((Color)(ref darkPixelColor)).ToVector3());
		value.Parameters["brighterPixelColor"].SetValue(((Color)(ref brightPixelColor)).ToVector3());
		((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)distortionTexture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[1] = SamplerState.LinearWrap;
		((Game)Main.instance).GraphicsDevice.Textures[2] = (Texture)(object)erosionTexture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[2] = SamplerState.LinearWrap;
		value.CurrentTechnique.Passes[0].Apply();
		Main.spriteBatch.Draw(cloudsTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
	}

	private void DrawDistortionForeground()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, Main.Rasterizer, (Effect)null, CalamityUtils.BackgroundMatrix);
		Main.spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, Main.Rasterizer, (Effect)null, CalamityUtils.BackgroundMatrix);
		DrawDistortionClouds_Foreground();
		DrawDistortionWinds_Foreground();
		Main.spriteBatch.End();
	}

	private void DrawDistortionWinds_Foreground()
	{
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		Effect value = CalamityShaders.DoGDistortionWindsShader.Value;
		Vector2 screenSize = default(Vector2);
		((Vector2)(ref screenSize))._002Ector((float)Main.screenWidth, (float)Main.screenHeight);
		Texture2D windsTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Neurons2", (AssetRequestMode)2).Value;
		Texture2D highlightsTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/SharpNoise", (AssetRequestMode)2).Value;
		Texture2D distortionTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/MeltyNoise", (AssetRequestMode)2).Value;
		Texture2D erosionTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Pebbles", (AssetRequestMode)2).Value;
		value.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly * 1.075f);
		value.Parameters["overallOpacity"].SetValue(0.8f);
		value.Parameters["distortionStrength"].SetValue(0.3f);
		value.Parameters["mainNoiseTextureScale"].SetValue(1.6f);
		value.Parameters["distortionTextureScale"].SetValue(1.76f);
		value.Parameters["erosionTextureScale"].SetValue(2f);
		value.Parameters["erosionMin"].SetValue(0.5f);
		value.Parameters["gradientPrecision"].SetValue(20f);
		value.Parameters["pixelationFactor"].SetValue(screenSize * 0.5f);
		value.Parameters["worldOffset"].SetValue(Main.screenPosition / windsTexture.Size() * 0.025f);
		EffectParameter obj = value.Parameters["darkerPixelColor"];
		Color val = Color.Lerp(Color.DarkGray, Color.Black, 0.64f);
		obj.SetValue(((Color)(ref val)).ToVector3());
		EffectParameter obj2 = value.Parameters["brighterPixelColor"];
		val = Color.Lerp(Color.DarkGray, Color.Black, 0.32f);
		obj2.SetValue(((Color)(ref val)).ToVector3());
		EffectParameter obj3 = value.Parameters["highlightsColor"];
		val = Color.Fuchsia;
		obj3.SetValue(((Color)(ref val)).ToVector3());
		((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)highlightsTexture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
		((Game)Main.instance).GraphicsDevice.Textures[2] = (Texture)(object)distortionTexture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[2] = SamplerState.LinearWrap;
		((Game)Main.instance).GraphicsDevice.Textures[3] = (Texture)(object)erosionTexture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[3] = SamplerState.LinearWrap;
		value.CurrentTechnique.Passes[0].Apply();
		Main.spriteBatch.Draw(windsTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
		value.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly * 0.8f);
		value.Parameters["overallOpacity"].SetValue(0.7f);
		value.Parameters["distortionStrength"].SetValue(0.6f);
		value.Parameters["mainNoiseTextureScale"].SetValue(1.24f);
		value.Parameters["distortionTextureScale"].SetValue(0.46f);
		value.Parameters["erosionTextureScale"].SetValue(3f);
		value.Parameters["erosionMin"].SetValue(0.75f);
		EffectParameter obj4 = value.Parameters["highlightsColor"];
		val = new Color(0, 221, 250);
		obj4.SetValue(((Color)(ref val)).ToVector3());
		value.CurrentTechnique.Passes[0].Apply();
		Main.spriteBatch.Draw(windsTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
	}

	private void DrawDistortionClouds_Foreground()
	{
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		Effect value = CalamityShaders.DoGBackgroundFogShader.Value;
		Texture2D cloudsTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/RealisticClouds", (AssetRequestMode)2).Value;
		Texture2D erosionTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/HarshNoise", (AssetRequestMode)2).Value;
		Vector2 screenSize = default(Vector2);
		((Vector2)(ref screenSize))._002Ector((float)Main.screenWidth, (float)Main.screenHeight);
		value.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly * 2f);
		value.Parameters["overallOpacity"].SetValue(1f);
		value.Parameters["distortionStrength"].SetValue(0.12f);
		value.Parameters["mainNoiseTextureScale"].SetValue(1f);
		value.Parameters["distortionTextureScale"].SetValue(0.8f);
		value.Parameters["erosionTextureScale"].SetValue(0.26f);
		value.Parameters["erosionMin"].SetValue(0.06f);
		value.Parameters["gradientPrecision"].SetValue(20f);
		value.Parameters["pixelationFactor"].SetValue(screenSize * 0.5f);
		value.Parameters["worldOffset"].SetValue(Main.screenPosition / cloudsTexture.Size() * 0.025f);
		EffectParameter obj = value.Parameters["darkerPixelColor"];
		Color val = Color.Lerp(Color.Black, new Color(0, 221, 250), 0.25f);
		obj.SetValue(((Color)(ref val)).ToVector3());
		EffectParameter obj2 = value.Parameters["brighterPixelColor"];
		val = Color.Lerp(Color.Black, Color.Fuchsia, 0.25f);
		obj2.SetValue(((Color)(ref val)).ToVector3());
		((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)cloudsTexture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[1] = SamplerState.LinearWrap;
		((Game)Main.instance).GraphicsDevice.Textures[2] = (Texture)(object)erosionTexture;
		((Game)Main.instance).GraphicsDevice.SamplerStates[2] = SamplerState.LinearWrap;
		value.CurrentTechnique.Passes[0].Apply();
		Main.spriteBatch.Draw(cloudsTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
	}
}
