using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.BaseTiles;

public abstract class BaseMonolith : ModTile
{
	public Asset<Texture2D> GlowMask;

	public SoundStyle? RightClickSound = SoundID.Mech;

	public abstract int TileWidth { get; }

	public abstract int TileHeight { get; }

	public abstract int AnimationFrameCount { get; }

	public abstract int AnimationDelay { get; }

	public abstract int CursorItemType { get; }

	public virtual bool HasBottomTile18PixelsHeight => true;

	public int EnabledFrameY => base.AnimationFrameHeight;

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = CursorItemType != 0;
		localPlayer.cursorItemIconID = CursorItemType;
	}

	public override bool RightClick(int i, int j)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		ToggleMonolith(i, j);
		SoundEngine.PlaySound(RightClickSound, (Vector2?)new Vector2((float)(i * 16), (float)(j * 16)));
		return true;
	}

	public override void HitWire(int i, int j)
	{
		ToggleMonolith(i, j);
	}

	private void ToggleMonolith(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		int width = 18 * TileWidth;
		int height = base.AnimationFrameHeight;
		int leftTopI = i - tile.TileFrameX % width / 18;
		int leftTopJ = j - tile.TileFrameY % height / 18;
		bool enabled = tile.TileFrameY >= height;
		for (int o = 0; o < TileWidth; o++)
		{
			for (int p = 0; p < TileHeight; p++)
			{
				int relI = leftTopI + o;
				int relJ = leftTopJ + p;
				Tile relTile = Main.tile[relI, relJ];
				if (enabled)
				{
					relTile.TileFrameY -= (short)height;
				}
				else
				{
					relTile.TileFrameY += (short)height;
				}
				if (Wiring.running)
				{
					Wiring.SkipWire(relI, relJ);
				}
			}
		}
		if (Main.netMode != 0)
		{
			NetMessage.SendTileSquare(-1, leftTopI, leftTopJ, TileWidth, TileHeight);
		}
	}

	public sealed override void NearbyEffects(int i, int j, bool closer)
	{
		bool enabled = Main.tile[i, j].TileFrameY >= EnabledFrameY;
		NearbyEffects(i, j, closer, enabled, Main.LocalPlayer);
	}

	public virtual void NearbyEffects(int i, int j, bool closer, bool monolithEnabled, Player localPlayer)
	{
	}

	public virtual Color GetGlowMaskDrawColor(int i, int j)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		frameCounter++;
		if (frameCounter >= AnimationDelay)
		{
			frameCounter = 0;
			if (++frame >= AnimationFrameCount)
			{
				frame = 0;
			}
		}
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		if (Main.tile[i, j].IsTileActuallyInvisible())
		{
			return false;
		}
		Tile tile = Main.tile[i, j];
		Texture2D texture = TextureAssets.Tile[base.Type].Value;
		Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange));
		Vector2 drawPos = new Vector2((float)(i * 16), (float)(j * 16)) - Main.screenPosition + zero;
		int animateFrameOffset = ((tile.TileFrameY >= EnabledFrameY) ? (Main.tileFrame[base.Type] * base.AnimationFrameHeight) : 0);
		int height = ((HasBottomTile18PixelsHeight && tile.TileFrameY % base.AnimationFrameHeight >= 18 * (TileHeight - 1)) ? 18 : 16);
		Rectangle rect = default(Rectangle);
		((Rectangle)(ref rect))._002Ector((int)tile.TileFrameX, tile.TileFrameY + animateFrameOffset, 16, height);
		Color drawColor = Lighting.GetColor(i, j);
		Color glowColor = GetGlowMaskDrawColor(i, j);
		Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)rect, drawColor, 0f, default(Vector2), 1f, (SpriteEffects)0, 0f);
		if (GlowMask != null)
		{
			Main.spriteBatch.Draw(GlowMask.Value, drawPos, (Rectangle?)rect, glowColor, 0f, default(Vector2), 1f, (SpriteEffects)0, 0f);
		}
		DrawExtra(drawPos, rect, drawColor);
		Asset<Texture2D> highlight = TextureAssets.HighlightMask[base.Type];
		if (highlight != null && highlight.IsLoaded && Main.InSmartCursorHighlightArea(i, j, out var actuallySelected))
		{
			int avgBrightness = (((Color)(ref drawColor)).R + ((Color)(ref drawColor)).G + ((Color)(ref drawColor)).B) / 3;
			if (avgBrightness > 10)
			{
				Color highlightColor = Colors.GetSelectionGlowColor(actuallySelected, avgBrightness);
				Main.spriteBatch.Draw(highlight.Value, new Vector2((float)(i * 16 - (int)Main.screenPosition.X), (float)(j * 16 - (int)Main.screenPosition.Y)) + zero, (Rectangle?)rect, highlightColor, 0f, default(Vector2), 1f, (SpriteEffects)0, 0f);
			}
		}
		return false;
	}

	public virtual void DrawExtra(Vector2 drawPos, Rectangle rect, Color tileColor)
	{
	}
}
