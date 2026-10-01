using System;
using CalamityMod.Skies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Backgrounds;

public class AstralSurfaceBGStyle : ModSurfaceBackgroundStyle
{
	private readonly int FrontBGYOffset = 275;

	private readonly int CloseBGYOffset = 175;

	private readonly int MiddleBGYOffset = 475;

	public override void ModifyFarFades(float[] fades, float transitionSpeed)
	{
		for (int i = 0; i < fades.Length; i++)
		{
			if (i == base.Slot)
			{
				fades[i] += transitionSpeed;
				if (fades[i] > 1f)
				{
					fades[i] = 1f;
				}
			}
			else
			{
				fades[i] -= transitionSpeed;
				if (fades[i] < 0f)
				{
					fades[i] = 0f;
				}
			}
		}
	}

	public override int ChooseFarTexture()
	{
		return BackgroundTextureLoader.GetBackgroundSlot("CalamityMod/Backgrounds/AstralSurfaceHorizon");
	}

	public override int ChooseMiddleTexture()
	{
		return BackgroundTextureLoader.GetBackgroundSlot("CalamityMod/Backgrounds/AstralSurfaceFar");
	}

	public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
	{
		return BackgroundTextureLoader.GetBackgroundSlot("CalamityMod/Skies/AstralSurfaceMiddle");
	}

	public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_065e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0676: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		float screenOff = Main.instance.screenOff;
		float scAdj = Main.instance.scAdj;
		Color COSBMAplha = Main.ColorOfSurfaceBackgroundsModified;
		Color ColorOfSurfaceBackgroundsModified = default(Color);
		((Color)(ref ColorOfSurfaceBackgroundsModified))._002Ector(63, 51, 90, (int)((Color)(ref COSBMAplha)).A);
		bool canBGDraw = false;
		if ((!Main.remixWorld || (Main.gameMenu && !WorldGen.remixWorldGen)) && (!WorldGen.remixWorldGen || !WorldGen.drunkWorldGen))
		{
			canBGDraw = true;
		}
		if (Main.mapFullscreen)
		{
			canBGDraw = false;
		}
		int offset = 30;
		if (Main.gameMenu)
		{
			offset = 0;
		}
		if (WorldGen.drunkWorldGen)
		{
			offset = -180;
		}
		float surfacePosition = (float)Main.worldSurface;
		if (surfacePosition == 0f)
		{
			surfacePosition = 1f;
		}
		float screenPosition = Main.screenPosition.Y + (float)(Main.screenHeight / 2) - 600f;
		double backgroundTopMagicNumber = (0f - screenPosition + screenOff / 2f) / (surfacePosition * 16f);
		float bgGlobalScaleMultiplier = 2f;
		int offset2 = -180;
		int menuOffset = 0;
		if (Main.gameMenu)
		{
			menuOffset -= offset2;
		}
		int pushBGTopHack = menuOffset;
		pushBGTopHack += offset;
		pushBGTopHack += offset2;
		if (canBGDraw)
		{
			float bgScale = 1.25f;
			double bgParallax = 0.4;
			int bgTopY = (int)(backgroundTopMagicNumber * 1800.0 + 1500.0) + (int)scAdj + pushBGTopHack;
			bgScale *= bgGlobalScaleMultiplier;
			int bgWidthScaled = (int)((float)SkyTextureRefs.AstralSurfaceMiddle.Value.Width * bgScale);
			SkyManager.Instance.DrawToDepth(Main.spriteBatch, 1.2f / (float)bgParallax);
			int bgStartX = (int)(0.0 - Math.IEEERemainder((double)Main.screenPosition.X * bgParallax, bgWidthScaled) - (double)(bgWidthScaled / 2));
			if (Main.gameMenu)
			{
				bgTopY = 320 + pushBGTopHack;
			}
			int bgLoops = Main.screenWidth / bgWidthScaled + 2;
			Color white;
			if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
			{
				for (int i = 0; i < bgLoops; i++)
				{
					Main.spriteBatch.Draw(SkyTextureRefs.AstralSurfaceMiddle.Value, new Vector2((float)(bgStartX + bgWidthScaled * i), (float)(bgTopY + MiddleBGYOffset)), (Rectangle?)new Rectangle(0, 0, SkyTextureRefs.AstralSurfaceMiddle.Value.Width, SkyTextureRefs.AstralSurfaceMiddle.Value.Height), ColorOfSurfaceBackgroundsModified, 0f, default(Vector2), bgScale, (SpriteEffects)0, 0f);
					SpriteBatch spriteBatch2 = Main.spriteBatch;
					Texture2D value = SkyTextureRefs.AstralSurfaceMiddleGlow.Value;
					Vector2 val = new Vector2((float)(bgStartX + bgWidthScaled * i), (float)(bgTopY + MiddleBGYOffset));
					Rectangle? val2 = new Rectangle(0, 0, SkyTextureRefs.AstralSurfaceMiddleGlow.Value.Width, SkyTextureRefs.AstralSurfaceMiddleGlow.Value.Height);
					white = Color.White;
					float num = (float)(int)((Color)(ref white)).R * 0.5f;
					white = Color.White;
					float num2 = (float)(int)((Color)(ref white)).G * 0.5f;
					white = Color.White;
					spriteBatch2.Draw(value, val, val2, new Color(num, num2, (float)(int)((Color)(ref white)).B * 0.5f, (float)(int)((Color)(ref COSBMAplha)).A), 0f, default(Vector2), bgScale, (SpriteEffects)0, 0f);
				}
			}
			bgScale = 1.31f;
			bgParallax = 0.43;
			bgTopY = (int)(backgroundTopMagicNumber * 1950.0 + 1750.0) + (int)scAdj + pushBGTopHack;
			bgScale *= bgGlobalScaleMultiplier;
			bgWidthScaled = (int)((float)SkyTextureRefs.AstralSurfaceClose.Value.Width * bgScale);
			SkyManager.Instance.DrawToDepth(Main.spriteBatch, 1f / (float)bgParallax);
			bgStartX = (int)(0.0 - Math.IEEERemainder((double)Main.screenPosition.X * bgParallax, bgWidthScaled) - (double)(bgWidthScaled / 2));
			if (Main.gameMenu)
			{
				bgTopY = 400 + pushBGTopHack;
				bgStartX -= 80;
			}
			bgLoops = Main.screenWidth / bgWidthScaled + 2;
			if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
			{
				for (int j = 0; j < bgLoops; j++)
				{
					Main.spriteBatch.Draw(SkyTextureRefs.AstralSurfaceClose.Value, new Vector2((float)(bgStartX + bgWidthScaled * j), (float)(bgTopY + CloseBGYOffset)), (Rectangle?)new Rectangle(0, 0, SkyTextureRefs.AstralSurfaceClose.Value.Width, SkyTextureRefs.AstralSurfaceClose.Value.Height), ColorOfSurfaceBackgroundsModified, 0f, default(Vector2), bgScale, (SpriteEffects)0, 0f);
					SpriteBatch spriteBatch3 = Main.spriteBatch;
					Texture2D value2 = SkyTextureRefs.AstralSurfaceCloseGlow.Value;
					Vector2 val3 = new Vector2((float)(bgStartX + bgWidthScaled * j), (float)(bgTopY + CloseBGYOffset));
					Rectangle? val4 = new Rectangle(0, 0, SkyTextureRefs.AstralSurfaceCloseGlow.Value.Width, SkyTextureRefs.AstralSurfaceCloseGlow.Value.Height);
					white = Color.White;
					float num3 = (float)(int)((Color)(ref white)).R * 0.7f;
					white = Color.White;
					float num4 = (float)(int)((Color)(ref white)).G * 0.7f;
					white = Color.White;
					spriteBatch3.Draw(value2, val3, val4, new Color(num3, num4, (float)(int)((Color)(ref white)).B * 0.7f, (float)(int)((Color)(ref COSBMAplha)).A), 0f, default(Vector2), bgScale, (SpriteEffects)0, 0f);
				}
			}
			bgScale = 1.34f;
			bgParallax = 0.49;
			bgTopY = (int)(backgroundTopMagicNumber * 2100.0 + 2000.0) + (int)scAdj + pushBGTopHack;
			bgScale *= bgGlobalScaleMultiplier;
			bgWidthScaled = (int)((float)SkyTextureRefs.AstralSurfaceFront.Value.Width * bgScale);
			SkyManager.Instance.DrawToDepth(Main.spriteBatch, 1f / (float)bgParallax);
			bgStartX = (int)(0.0 - Math.IEEERemainder((double)Main.screenPosition.X * bgParallax, bgWidthScaled) - (double)(bgWidthScaled / 2));
			if (Main.gameMenu)
			{
				bgTopY = 480 + pushBGTopHack;
				bgStartX -= 120;
			}
			bgLoops = Main.screenWidth / bgWidthScaled + 2;
			if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
			{
				for (int k = 0; k < bgLoops; k++)
				{
					Main.spriteBatch.Draw(SkyTextureRefs.AstralSurfaceFront.Value, new Vector2((float)(bgStartX + bgWidthScaled * k), (float)(bgTopY + FrontBGYOffset)), (Rectangle?)new Rectangle(0, 0, SkyTextureRefs.AstralSurfaceFront.Value.Width, SkyTextureRefs.AstralSurfaceFront.Value.Height), ColorOfSurfaceBackgroundsModified, 0f, default(Vector2), bgScale, (SpriteEffects)0, 0f);
					SpriteBatch spriteBatch4 = Main.spriteBatch;
					Texture2D value3 = SkyTextureRefs.AstralSurfaceFrontGlow.Value;
					Vector2 val5 = new Vector2((float)(bgStartX + bgWidthScaled * k), (float)(bgTopY + FrontBGYOffset));
					Rectangle? val6 = new Rectangle(0, 0, SkyTextureRefs.AstralSurfaceFrontGlow.Value.Width, SkyTextureRefs.AstralSurfaceFrontGlow.Value.Height);
					white = Color.White;
					float num5 = (float)(int)((Color)(ref white)).R * 0.9f;
					white = Color.White;
					float num6 = (float)(int)((Color)(ref white)).G * 0.9f;
					white = Color.White;
					spriteBatch4.Draw(value3, val5, val6, new Color(num5, num6, (float)(int)((Color)(ref white)).B * 0.9f, (float)(int)((Color)(ref COSBMAplha)).A), 0f, default(Vector2), bgScale, (SpriteEffects)0, 0f);
				}
			}
		}
		return false;
	}
}
