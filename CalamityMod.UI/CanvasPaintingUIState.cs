using CalamityMod.CalPlayer;
using CalamityMod.TileEntities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;

namespace CalamityMod.UI;

public class CanvasPaintingUIState
{
	public static float scrollOld;

	public static float scrollNew;

	public static bool moving;

	public static bool justClicked;

	public static void DrawCanvasUI(SpriteBatch spriteBatch)
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer p = Main.LocalPlayer.Calamity();
		if (p.CurrentlyViewedCanvasID == -1 || p.CurrentlyViewedCanvasType == -1)
		{
			return;
		}
		if (TileEntity.ByID.TryGetValue(p.CurrentlyViewedCanvasID, out var te) && te is TECanvasPainting cast)
		{
			TECanvasPainting painting = cast;
			if (!Main.playerInventory || Main.LocalPlayer.chest != -1 || Main.LocalPlayer.channel)
			{
				ClosePainting(ref p, painting);
				return;
			}
			int paintingTileSize = 80;
			float paintingFrameScale = painting.scale;
			Vector2 paintingFramePosition = painting.framePosition;
			bool hideUI = Main.keyState.PressingShift();
			Texture2D tex = TextureAssets.Tile[p.CurrentlyViewedCanvasType].Value;
			float dimension = (float)Main.screenHeight * 0.66f;
			float sizeRatio = dimension / (float)tex.Height;
			Vector2 baseDrawPos = default(Vector2);
			((Vector2)(ref baseDrawPos))._002Ector((float)Main.screenWidth * 0.5f - dimension * 0.5f, (float)Main.screenHeight * 0.5f - dimension * 0.5f);
			Vector2 posterDrawPos = baseDrawPos + Vector2.UnitX * ((dimension - (float)tex.Width * sizeRatio) * 0.5f);
			spriteBatch.End();
			spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null);
			if (!hideUI)
			{
				spriteBatch.Draw(tex, posterDrawPos, (Rectangle?)null, Color.White, 0f, new Vector2(0f, 0f), sizeRatio, (SpriteEffects)0, 0f);
			}
			MouseState state = Mouse.GetState();
			float scrollAmount = ((painting.scale >= 1f) ? 1f : 0.25f);
			if (scrollOld > scrollNew)
			{
				float increasedScale = MathHelper.Clamp(painting.scale + scrollAmount, 0.25f, 10f);
				painting.scale = increasedScale;
				painting.SendSyncPacket();
			}
			else if (scrollNew > scrollOld)
			{
				scrollAmount = ((painting.scale >= 2f) ? 1f : 0.25f);
				float decreasedScale = MathHelper.Clamp(painting.scale - scrollAmount, 0.25f, 10f);
				painting.scale = decreasedScale;
				painting.SendSyncPacket();
			}
			int borderSize = 4;
			Vector2 cursorDimension = tex.Size() * sizeRatio * painting.scale * 0.1f;
			Vector2 movingCursorPos = Vector2.Clamp(Main.MouseScreen - cursorDimension * 0.5f + new Vector2(10f, 10f), posterDrawPos, posterDrawPos + tex.Size() * sizeRatio - cursorDimension);
			Vector2 currentPos = Vector2.Clamp(paintingFramePosition / ((float)tex.Height / dimension) + posterDrawPos, posterDrawPos, posterDrawPos + tex.Size() * sizeRatio - cursorDimension);
			Vector2 cursorPosition = (moving ? movingCursorPos : currentPos);
			bool clicked = Main.mouseLeft && Main.mouseLeftRelease;
			if (moving)
			{
				painting.framePosition = (cursorPosition - posterDrawPos) * ((float)tex.Height / dimension);
				if (clicked)
				{
					moving = false;
					SoundEngine.PlaySound(in SoundID.MenuTick);
					painting.SendSyncPacket();
				}
			}
			else if (clicked || scrollOld != scrollNew)
			{
				moving = true;
				SoundEngine.PlaySound(in SoundID.MenuTick);
			}
			else
			{
				if (((KeyboardState)(ref Main.keyState)).IsKeyDown((Keys)37) && ((KeyboardState)(ref Main.oldKeyState)).IsKeyUp((Keys)37))
				{
					painting.framePosition.X = MathHelper.Clamp(painting.framePosition.X - 1f, 0f, (float)tex.Width);
					painting.SendSyncPacket();
				}
				if (((KeyboardState)(ref Main.keyState)).IsKeyDown((Keys)39) && ((KeyboardState)(ref Main.oldKeyState)).IsKeyUp((Keys)39))
				{
					painting.framePosition.X = MathHelper.Clamp(painting.framePosition.X + 1f, 0f, (float)tex.Width);
					painting.SendSyncPacket();
				}
				if (((KeyboardState)(ref Main.keyState)).IsKeyDown((Keys)38) && ((KeyboardState)(ref Main.oldKeyState)).IsKeyUp((Keys)38))
				{
					painting.framePosition.Y = MathHelper.Clamp(painting.framePosition.Y - 1f, 0f, (float)tex.Height);
					painting.SendSyncPacket();
				}
				if (((KeyboardState)(ref Main.keyState)).IsKeyDown((Keys)40) && ((KeyboardState)(ref Main.oldKeyState)).IsKeyUp((Keys)40))
				{
					painting.framePosition.Y = MathHelper.Clamp(painting.framePosition.Y + 1f, 0f, (float)tex.Height);
					painting.SendSyncPacket();
				}
			}
			if (!hideUI)
			{
				DrawRectangle(spriteBatch, cursorPosition - new Vector2((float)borderSize), new Vector2(cursorDimension.X + (float)(borderSize * 2), (float)borderSize));
				DrawRectangle(spriteBatch, cursorPosition + new Vector2((float)(-borderSize), cursorDimension.Y), new Vector2(cursorDimension.X + (float)(borderSize * 2), (float)borderSize));
				DrawRectangle(spriteBatch, cursorPosition - new Vector2((float)borderSize, 0f), new Vector2((float)borderSize, cursorDimension.Y + (float)borderSize));
				DrawRectangle(spriteBatch, cursorPosition + new Vector2(cursorDimension.X, 0f), new Vector2((float)borderSize, cursorDimension.Y + (float)borderSize));
			}
			float extraScale = 3f;
			float halfDim = (float)paintingTileSize * sizeRatio * extraScale / 2f;
			int previewSliceSize = (int)(paintingFrameScale * 0.1f * (float)tex.Width);
			float previewScale = sizeRatio / paintingFrameScale * extraScale;
			int previewDimension = (int)((float)previewSliceSize * previewScale);
			Vector2 demoPosition = posterDrawPos + new Vector2(dimension + halfDim, dimension / 2f - halfDim);
			if (!hideUI)
			{
				spriteBatch.Draw(tex, demoPosition, (Rectangle?)new Rectangle((int)paintingFramePosition.X, (int)paintingFramePosition.Y, previewSliceSize, previewSliceSize), Color.White, 0f, new Vector2(0f, 0f), previewScale, (SpriteEffects)0, 0f);
			}
			Rectangle val = Mouse();
			bool num = ((Rectangle)(ref val)).Intersects(new Rectangle((int)baseDrawPos.X, (int)baseDrawPos.Y, (int)dimension, (int)dimension));
			val = Mouse();
			bool intersectingPrev = ((Rectangle)(ref val)).Intersects(new Rectangle((int)demoPosition.X, (int)demoPosition.Y, previewDimension, previewDimension));
			if (num | intersectingPrev)
			{
				Main.blockMouse = (Main.LocalPlayer.mouseInterface = true);
			}
			if (scrollOld == 0f && scrollNew == 0f)
			{
				scrollOld = ((MouseState)(ref state)).ScrollWheelValue;
				scrollNew = ((MouseState)(ref state)).ScrollWheelValue;
			}
			else
			{
				scrollOld = scrollNew;
				scrollNew = ((MouseState)(ref state)).ScrollWheelValue;
			}
			spriteBatch.ExitShaderRegion();
		}
		else
		{
			p.CurrentlyViewedCanvasID = -1;
			p.CurrentlyViewedCanvasType = -1;
			ResetVars();
		}
	}

	public static void ClosePainting(ref CalamityPlayer clam, TECanvasPainting te)
	{
		clam.CurrentlyViewedCanvasID = -1;
		ResetVars();
		te.SendSyncPacket();
	}

	public static void ResetVars()
	{
		scrollOld = 0f;
		scrollNew = 0f;
		moving = false;
	}

	public static void DrawRectangle(SpriteBatch spriteBatch, Vector2 position, Vector2 dimensions)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		spriteBatch.Draw(TextureAssets.MagicPixel.Value, position, (Rectangle?)new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y), Main.DiscoColor, 0f, new Vector2(0f, 0f), 1f, (SpriteEffects)0, 0f);
	}

	private static Rectangle Mouse()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		return new Rectangle((int)(Main.MouseWorld.X - Main.screenPosition.X), (int)(Main.MouseWorld.Y - Main.screenPosition.Y), 10, 10);
	}
}
