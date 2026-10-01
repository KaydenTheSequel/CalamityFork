using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Walls.UnsafeWalls;

[LegacyName(new string[] { "VoidstoneWallUnsafe" })]
public class UnsafeVoidstoneWall : ModWall
{
	internal static FramedMaskTexture GlowMask;

	public override string Texture => "CalamityMod/Walls/VoidstoneWall";

	public override void SetStaticDefaults()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		GlowMask = new FramedMaskTexture("CalamityMod/Walls/VoidstoneWall_Glowmask", 36, 36);
		base.DustType = 187;
		AddMapEntry(new Color(0, 0, 0));
	}

	public override void Unload()
	{
		GlowMask?.Unload();
		GlowMask = null;
	}

	public override void RandomUpdate(int i, int j)
	{
		if (Main.tile[i, j].LiquidAmount == 0 && j < Main.maxTilesY - 205)
		{
			Main.tile[i, j].Get<LiquidData>().LiquidType = 0;
			Main.tile[i, j].LiquidAmount = byte.MaxValue;
			WorldGen.SquareTileFrame(i, j);
			if (Main.dedServ)
			{
				NetMessage.sendWater(i, j);
			}
		}
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
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
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
		if (!GlowMask.HasContentInFramePos(xPos, yPos))
		{
			return;
		}
		float brightness = 1f;
		float declareThisHereToPreventRunningTheSameCalculationMultipleTimes = (float)Main.GameUpdateCount * 0.007f;
		brightness *= MathF.Sin((float)i / 18f + declareThisHereToPreventRunningTheSameCalculationMultipleTimes);
		brightness *= MathF.Sin((float)j / 18f + declareThisHereToPreventRunningTheSameCalculationMultipleTimes);
		brightness *= MathF.Sin((float)i * 18f + declareThisHereToPreventRunningTheSameCalculationMultipleTimes);
		brightness *= MathF.Sin((float)j * 18f + declareThisHereToPreventRunningTheSameCalculationMultipleTimes);
		brightness = MathHelper.Clamp(brightness, 0f, 1f);
		Color glowColor = (drawcolor *= brightness);
		if (((Color)(ref glowColor)).R > 0 || ((Color)(ref glowColor)).G > 0 || ((Color)(ref glowColor)).B > 0)
		{
			for (int k = 0; k < 3; k++)
			{
				Vector2 offset = new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)) * (0.2f * (float)k);
				spriteBatch.Draw(GlowMask.Texture, pos + offset + new Vector2((float)(-8 + xOff), -8f), (Rectangle?)frame, glowColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
		}
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		DrawWallGlow(base.Type, i, j, spriteBatch);
		return false;
	}

	public override void KillWall(int i, int j, ref bool fail)
	{
		fail = true;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
