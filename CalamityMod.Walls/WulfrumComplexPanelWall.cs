using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

public class WulfrumComplexPanelWall : ModWall
{
	internal static FramedMaskTexture GlowMask;

	public override void SetStaticDefaults()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		GlowMask = new FramedMaskTexture("CalamityMod/Walls/WulfrumComplexPanelWall_Glow", 36, 36);
		Main.wallHouse[base.Type] = true;
		AddMapEntry(new Color(28, 28, 29));
	}

	public override void Unload()
	{
		GlowMask?.Unload();
		GlowMask = null;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 180, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public static void DrawWallGlow(int wallType, int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		if (GlowMask.Texture == null)
		{
			return;
		}
		Tile tile = Main.tile[i, j];
		int xLength = 32;
		int xOff = 0;
		int xPos = tile.WallFrameX + xOff;
		int yPos = tile.WallFrameY;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(xPos, yPos, xLength, 32);
		Color drawcolor = WorldGen.paintColor(tile.WallColor);
		((Color)(ref drawcolor)).A = byte.MaxValue;
		Vector2 zero = default(Vector2);
		((Vector2)(ref zero))._002Ector((float)Main.offScreenRange, (float)Main.offScreenRange);
		if (Main.drawToScreen)
		{
			zero = Vector2.Zero;
		}
		Vector2 pos = new Vector2((float)(i * 16 - (int)Main.screenPosition.X), (float)(j * 16 - (int)Main.screenPosition.Y)) + zero;
		Color lightColor = Lighting.GetColor(i, j, Color.White);
		spriteBatch.Draw(TextureAssets.Wall[wallType].Value, pos + new Vector2((float)(-8 + xOff), -8f), (Rectangle?)frame, lightColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		if (GlowMask.HasContentInFramePos(xPos, yPos))
		{
			float brightness = MathHelper.Clamp(0.2f - (float)(j / 680), 0f, 0.2f);
			float num = (float)Main.GameUpdateCount * 0.064f;
			int scalar = i - j / 2;
			float wave1 = num * -50f + (float)(scalar * 12);
			float wave1angle = 0.3f + 0.25f * MathF.Sin(MathHelper.ToRadians(wave1));
			drawcolor *= brightness;
			float transparency = 0.02f + wave1angle / 4f;
			Color glowColor = Color.White * transparency;
			for (int k = 0; k < 3; k++)
			{
				spriteBatch.Draw(GlowMask.Texture, pos + new Vector2((float)(-8 + xOff), -8f), (Rectangle?)frame, glowColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
		}
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		DrawWallGlow(base.Type, i, j, spriteBatch);
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
