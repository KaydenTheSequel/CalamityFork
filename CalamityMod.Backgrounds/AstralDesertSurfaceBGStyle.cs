using System;
using CalamityMod.Skies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Backgrounds;

public class AstralDesertSurfaceBGStyle : ModSurfaceBackgroundStyle
{
	private readonly int CloseBGYOffset = 475;

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
		return BackgroundTextureLoader.GetBackgroundSlot("CalamityMod/Backgrounds/AstralDesertSurfaceFar");
	}

	public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
	{
		return BackgroundTextureLoader.GetBackgroundSlot("CalamityMod/Skies/AstralDesertSurfaceMiddle");
	}

	public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
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
			int bgWidthScaled = (int)((float)SkyTextureRefs.AstralDesertSurfaceMiddle.Value.Width * bgScale);
			SkyManager.Instance.DrawToDepth(Main.spriteBatch, 1.2f / (float)bgParallax);
			int bgStartX = (int)(0.0 - Math.IEEERemainder((double)Main.screenPosition.X * bgParallax, bgWidthScaled) - (double)(bgWidthScaled / 2));
			if (Main.gameMenu)
			{
				bgTopY = 320 + pushBGTopHack;
			}
			int bgLoops = Main.screenWidth / bgWidthScaled + 2;
			if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
			{
				for (int i = 0; i < bgLoops; i++)
				{
					Main.spriteBatch.Draw(SkyTextureRefs.AstralDesertSurfaceMiddle.Value, new Vector2((float)(bgStartX + bgWidthScaled * i), (float)(bgTopY + MiddleBGYOffset)), (Rectangle?)new Rectangle(0, 0, SkyTextureRefs.AstralDesertSurfaceMiddle.Value.Width, SkyTextureRefs.AstralDesertSurfaceMiddle.Value.Height), ColorOfSurfaceBackgroundsModified, 0f, default(Vector2), bgScale, (SpriteEffects)0, 0f);
				}
			}
			bgScale = 1.31f;
			bgParallax = 0.43;
			bgTopY = (int)(backgroundTopMagicNumber * 1950.0 + 1750.0) + (int)scAdj + pushBGTopHack;
			bgScale *= bgGlobalScaleMultiplier;
			bgWidthScaled = (int)((float)SkyTextureRefs.AstralDesertSurfaceClose.Value.Width * bgScale);
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
					Main.spriteBatch.Draw(SkyTextureRefs.AstralDesertSurfaceClose.Value, new Vector2((float)(bgStartX + bgWidthScaled * j), (float)(bgTopY + CloseBGYOffset)), (Rectangle?)new Rectangle(0, 0, SkyTextureRefs.AstralDesertSurfaceClose.Value.Width, SkyTextureRefs.AstralDesertSurfaceClose.Value.Height), ColorOfSurfaceBackgroundsModified, 0f, default(Vector2), bgScale, (SpriteEffects)0, 0f);
				}
			}
		}
		return false;
	}
}
