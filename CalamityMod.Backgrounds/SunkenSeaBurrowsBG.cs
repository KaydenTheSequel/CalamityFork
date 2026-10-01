using CalamityMod.BiomeManagers;
using CalamityMod.Systems.Graphic;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Backgrounds;

public class SunkenSeaBurrowsBG : ModSystem
{
	private static RenderTargetLease WaterDistortionTarget;

	public static float Transparency;

	private static bool CurrentlyRendering { get; set; }

	public static float TransitionSpeed => 0.02f;

	public override void Load()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		if (!Main.dedServ)
		{
			GeneralDrawLayerSystem.OnPrepareDraw += DrawToTarget;
			On_Main.DrawBackgroundBlackFill += new hook_DrawBackgroundBlackFill(On_Main_DrawBackgroundBlackFill);
			Main.QueueMainThreadAction(delegate
			{
				WaterDistortionTarget = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice);
			});
		}
	}

	public override void Unload()
	{
		GeneralDrawLayerSystem.OnPrepareDraw -= DrawToTarget;
	}

	public override void PreUpdatePlayers()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ && Main.LocalPlayer.InModBiome<GleamingBurrowsBiome>())
		{
			int drawLimitX = Main.screenWidth / 16;
			int drawLimitY = Main.screenHeight / 16;
			Point drawPoint = (Main.screenPosition / 16f).ToPoint();
			for (int i = 0; i < drawLimitX; i++)
			{
				for (int j = 0; j < drawLimitY; j++)
				{
					Point pos = drawPoint + new Point(i, j);
					if (!Main.tile[pos.X, pos.Y].HasTile && Main.tile[pos.X, pos.Y].WallType == 0)
					{
						Lighting.AddLight(pos.X, pos.Y, 5, 0.1f);
					}
				}
			}
		}
		if (!Main.dedServ && Main.LocalPlayer.InModBiome(ModContent.GetInstance<TimelessShoresBiome>()))
		{
			int drawLimitX2 = Main.screenWidth / 16;
			int drawLimitY2 = Main.screenHeight / 16;
			Point drawPoint2 = (Main.screenPosition / 16f).ToPoint();
			for (int k = 0; k < drawLimitX2; k++)
			{
				for (int l = 0; l < drawLimitY2; l++)
				{
					Point pos2 = drawPoint2 + new Point(k, l);
					if (!Main.tile[pos2.X, pos2.Y].HasTile && Main.tile[pos2.X, pos2.Y].WallType == 0 && Main.tile[pos2.X, pos2.Y].LiquidAmount == 0)
					{
						Lighting.AddLight(pos2.X, pos2.Y, 5, 0.2f);
					}
				}
			}
		}
		if (!Main.dedServ && Main.LocalPlayer.InModBiome(ModContent.GetInstance<PolypForestBiome>()))
		{
			int drawLimitX3 = Main.screenWidth / 16;
			int drawLimitY3 = Main.screenHeight / 16;
			Point drawPoint3 = (Main.screenPosition / 16f).ToPoint();
			for (int m = 0; m < drawLimitX3; m++)
			{
				for (int n = 0; n < drawLimitY3; n++)
				{
					Point pos3 = drawPoint3 + new Point(m, n);
					if (!Main.tile[pos3.X, pos3.Y].HasTile && Main.tile[pos3.X, pos3.Y].WallType == 0)
					{
						Lighting.AddLight(pos3.X, pos3.Y, 5, 0.2f);
					}
				}
			}
		}
		if (!Main.dedServ && Main.LocalPlayer.InModBiome(ModContent.GetInstance<RadiantReefsBiome>()))
		{
			int drawLimitX4 = Main.screenWidth / 16;
			int drawLimitY4 = Main.screenHeight / 16;
			Point drawPoint4 = (Main.screenPosition / 16f).ToPoint();
			for (int num = 0; num < drawLimitX4; num++)
			{
				for (int num2 = 0; num2 < drawLimitY4; num2++)
				{
					Point pos4 = drawPoint4 + new Point(num, num2);
					if (!Main.tile[pos4.X, pos4.Y].HasTile && Main.tile[pos4.X, pos4.Y].WallType == 0)
					{
						Lighting.AddLight(pos4.X, pos4.Y, 5, 0.2f);
					}
				}
			}
		}
		if (Main.dedServ || !Main.LocalPlayer.InModBiome(ModContent.GetInstance<BasaltGullyBiome>()))
		{
			return;
		}
		int drawLimitX5 = Main.screenWidth / 16;
		int drawLimitY5 = Main.screenHeight / 16;
		Point drawPoint5 = (Main.screenPosition / 16f).ToPoint();
		for (int num3 = 0; num3 < drawLimitX5; num3++)
		{
			for (int num4 = 0; num4 < drawLimitY5; num4++)
			{
				Point pos5 = drawPoint5 + new Point(num3, num4);
				if (pos5.Y >= Main.maxTilesY - 450 && !Main.tile[pos5.X, pos5.Y].HasTile && Main.tile[pos5.X, pos5.Y].WallType == 0)
				{
					Lighting.AddLight(pos5.X, pos5.Y, 2, 0.6f);
				}
			}
		}
	}

	private void DrawToTarget()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.gameMenu)
		{
			CurrentlyRendering = true;
			using (WaterDistortionTarget.Scope(preserveContents: true, Color.Transparent))
			{
				Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, CalamityUtils.BackgroundMatrix);
				DrawShoresBG();
				DrawBurrowsBG();
				Main.spriteBatch.End();
			}
			CurrentlyRendering = false;
		}
	}

	private void On_Main_DrawBackgroundBlackFill(orig_DrawBackgroundBlackFill orig, Main self)
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		if (Main.gameMenu || Main.screenPosition.Y + (float)Main.screenHeight < (float)(int)Main.worldSurface * 16f)
		{
			orig.Invoke(self);
			return;
		}
		orig.Invoke(self);
		if (Main.WaveQuality > 0 || !CalamityClientConfig.Instance.SunkenSeaBackgroundDistortion)
		{
			DrawShoresBG();
			DrawBurrowsBG();
			return;
		}
		MiscShaderData distortionShader = GameShaders.Misc["CalamityMod:BasicTextureDistortion"];
		Asset<Texture2D> distortionTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Swirls", (AssetRequestMode)2);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, distortionShader.Shader, CalamityUtils.BackgroundMatrix);
		Vector2 timeOffset = new Vector2(-0.012f, 0.02f) * Main.GlobalTimeWrappedHourly * 0.075f;
		Vector2 noiseScaleStrength = default(Vector2);
		((Vector2)(ref noiseScaleStrength))._002Ector(0.075f, 0.035f);
		distortionShader.Shader.Parameters["timeOffset"].SetValue(timeOffset);
		distortionShader.Shader.Parameters["noiseScaleStrength"].SetValue(noiseScaleStrength);
		distortionShader.SetShaderTexture(distortionTexture);
		Main.spriteBatch.Draw((Texture2D)(object)WaterDistortionTarget.Target, new Rectangle(16, 16, Main.screenWidth, Main.screenHeight), Color.White);
	}

	private static void DrawShoresBG()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		if (Main.LocalPlayer.InModBiome(ModContent.GetInstance<TimelessShoresBiome>()))
		{
			Transparency++;
			if (Transparency > 1f)
			{
				Transparency = 1f;
			}
		}
		else
		{
			Transparency--;
			if (Transparency < 0f)
			{
				Transparency = 0f;
			}
		}
		if (!(Transparency > 0f) || !Main.BackgroundEnabled)
		{
			return;
		}
		Vector2 vector = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
		float num = (Main.GameViewMatrix.Zoom.Y - 1f) * 0.5f * 200f;
		float Scale = 1.5f;
		float playerDrawPosition = (Main.LocalPlayer.Center.Y / 16f - 90f) * 16f;
		Vector2 vector3 = default(Vector2);
		Rectangle rectangle = default(Rectangle);
		for (int Layers = 4; Layers >= 0; Layers--)
		{
			Texture2D BGTexture = ModContent.Request<Texture2D>("CalamityMod/Backgrounds/SunkenSeaShoresBG" + Layers, (AssetRequestMode)2).Value;
			Vector2 vector2 = new Vector2((float)BGTexture.Width, (float)BGTexture.Height) * 0.5f;
			float num2 = (float)(Layers * 2) + 3f;
			((Vector2)(ref vector3))._002Ector(1f / num2);
			((Rectangle)(ref rectangle))._002Ector(0, 0, BGTexture.Width, BGTexture.Height);
			Vector2 zero = Vector2.Zero;
			switch (Layers)
			{
			case 0:
				zero.Y += 200f;
				break;
			case 1:
				zero.Y += 0f;
				break;
			case 2:
				zero.Y += 0f;
				break;
			case 3:
				zero.Y += 0f;
				break;
			case 4:
				zero.Y += 0f;
				break;
			}
			vector2 *= Scale;
			zero.Y -= num;
			float LoopWidth = Scale * (float)rectangle.Width;
			float LoopHeight = Scale * (float)rectangle.Height;
			int LoopX = (int)((vector.X * vector3.X - vector2.X + zero.X - (float)(Main.screenWidth >> 1)) / LoopWidth);
			int LoopY = (int)((vector.Y * vector3.Y - vector2.Y + zero.Y - (float)(Main.screenWidth >> 1)) / LoopWidth);
			for (int i = LoopY - 2; i < LoopY + 4 + (int)((float)Main.screenWidth / LoopHeight); i++)
			{
				for (int j = LoopX - 2; j < LoopX + 4 + (int)((float)Main.screenWidth / LoopWidth); j++)
				{
					Vector2 drawPosition = (new Vector2((float)j * Scale * ((float)rectangle.Width / vector3.X), playerDrawPosition) + vector2 - vector) * vector3 + vector - Main.screenPosition - vector2 + zero;
					Rectangle frame = rectangle;
					Color color = Color.White * Transparency;
					Main.spriteBatch.Draw(BGTexture, drawPosition, (Rectangle?)frame, color, 0f, Vector2.Zero, Scale, (SpriteEffects)0, 0f);
				}
			}
		}
	}

	private static void DrawBurrowsBG()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		if (Main.LocalPlayer.InModBiome(ModContent.GetInstance<GleamingBurrowsBiome>()))
		{
			Transparency++;
			if (Transparency > 1f)
			{
				Transparency = 1f;
			}
		}
		else
		{
			Transparency--;
			if (Transparency < 0f)
			{
				Transparency = 0f;
			}
		}
		if (!(Transparency > 0f) || !Main.BackgroundEnabled)
		{
			return;
		}
		Vector2 vector = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
		float num = (Main.GameViewMatrix.Zoom.Y - 1f) * 0.5f * 200f;
		float Scale = 1.5f;
		float playerDrawPosition = (Main.LocalPlayer.Center.Y / 16f - 90f) * 16f;
		Vector2 vector3 = default(Vector2);
		Rectangle rectangle = default(Rectangle);
		for (int Layers = 4; Layers >= 0; Layers--)
		{
			Texture2D BGTexture = ModContent.Request<Texture2D>("CalamityMod/Backgrounds/SunkenSeaBurrowsBG" + Layers, (AssetRequestMode)2).Value;
			Vector2 vector2 = new Vector2((float)BGTexture.Width, (float)BGTexture.Height) * 0.5f;
			float num2 = (float)(Layers * 2) + 3f;
			((Vector2)(ref vector3))._002Ector(1f / num2);
			((Rectangle)(ref rectangle))._002Ector(0, 0, BGTexture.Width, BGTexture.Height);
			Vector2 zero = Vector2.Zero;
			switch (Layers)
			{
			case 0:
				zero.Y += 800f;
				break;
			case 1:
				zero.Y += 200f;
				break;
			case 2:
				zero.Y += 450f;
				break;
			case 3:
				zero.Y += 45f;
				break;
			case 4:
				zero.Y += 45f;
				break;
			}
			vector2 *= Scale;
			zero.Y -= num;
			float LoopWidth = Scale * (float)rectangle.Width;
			float LoopHeight = Scale * (float)rectangle.Height;
			int LoopX = (int)((vector.X * vector3.X - vector2.X + zero.X - (float)(Main.screenWidth >> 1)) / LoopWidth);
			int LoopY = (int)((vector.Y * vector3.Y - vector2.Y + zero.Y - (float)(Main.screenWidth >> 1)) / LoopWidth);
			for (int i = LoopY - 2; i < LoopY + 4 + (int)((float)Main.screenWidth / LoopHeight); i++)
			{
				for (int j = LoopX - 2; j < LoopX + 4 + (int)((float)Main.screenWidth / LoopWidth); j++)
				{
					Vector2 drawPosition = (new Vector2((float)j * Scale * ((float)rectangle.Width / vector3.X), playerDrawPosition) + vector2 - vector) * vector3 + vector - Main.screenPosition - vector2 + zero;
					Rectangle frame = rectangle;
					Color color = Color.White * Transparency;
					Main.spriteBatch.Draw(BGTexture, drawPosition, (Rectangle?)frame, color, 0f, Vector2.Zero, Scale, (SpriteEffects)0, 0f);
				}
			}
		}
	}
}
