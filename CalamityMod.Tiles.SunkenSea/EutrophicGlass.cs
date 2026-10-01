using System;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class EutrophicGlass : ModTile
{
	private static int sheetWidth = 216;

	private static int sheetHeight = 72;

	public static int TypeCache;

	public Asset<Texture2D> TileTexture;

	public Asset<Texture2D> GlintTexture;

	public Vector2 GlintDir;

	public override void SetStaticDefaults()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		TypeCache = base.Type;
		TileTexture = ModContent.Request<Texture2D>(Texture + "_Tile", (AssetRequestMode)2);
		GlintTexture = ModContent.Request<Texture2D>(Texture + "_Glint", (AssetRequestMode)2);
		GlintDir = new Vector2(1f, 1f);
		((Vector2)(ref GlintDir)).Normalize();
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = false;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeSmoothTiles(base.Type);
		CalamityUtils.MergeDecorativeTiles(base.Type);
		Main.tileLighted[base.Type] = true;
		Main.tileShine2[base.Type] = false;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		TileID.Sets.WallsMergeWith[base.Type] = true;
		base.DustType = 108;
		AddMapEntry(new Color(197, 220, 220));
		base.HitSound = SoundID.Shatter;
		base.MinPick = 55;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		Rectangle frame;
		Vector2 position;
		float projection;
		float stripeWidth;
		Color lightColor;
		if (!Main.tile[i, j].IsTileActuallyInvisible())
		{
			float transparency = 0.4f;
			TileID.Sets.DrawsWalls[base.Type] = true;
			Main.tileNoSunLight[base.Type] = false;
			Tile tile = Main.tile[i, j];
			int num = i % 10;
			int yPos = j % 10;
			int frameXOffset = num * sheetWidth;
			int frameYOffset = yPos * sheetHeight;
			frame = new Rectangle(tile.TileFrameX + frameXOffset, tile.TileFrameY + frameYOffset, 16, 16);
			Color color = Lighting.GetColor(i, j) * transparency;
			TileFramingSystem.SlopedGlowmask(in tile, i, j, TileTexture.Value, frame, CalamityUtils.ApplyPaint(Main.tile[i, j].TileColor, color, deepPaintOnly: false), default(Vector2));
			Vector2 offScreen = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			position = new Vector2((float)(i * 16), (float)(j * 16)) - Main.screenPosition + offScreen;
			Vector2 screenPos = position;
			projection = Vector2.Dot(screenPos, GlintDir);
			float screenDiagonalLength = Vector2.Dot(new Vector2((float)Main.screenWidth, (float)Main.screenHeight), GlintDir);
			stripeWidth = 100f;
			lightColor = Lighting.GetColor(i, j) * 2f;
			DrawGlint(screenDiagonalLength * 0.53f);
			DrawGlint(screenDiagonalLength * 0.63f);
			DrawGlint(screenDiagonalLength * 0.73f);
		}
		void DrawGlint(float beamCenter)
		{
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			float dist = Math.Abs(projection - beamCenter);
			float strength = MathHelper.Clamp(1f - dist / stripeWidth, 0f, 1f) * 0.4f;
			if (strength > 0f)
			{
				spriteBatch.Draw(GlintTexture.Value, position, (Rectangle?)frame, lightColor * strength, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
		}
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		TileFramingSystem.CompactFraming(i, j, resetFrame);
		return false;
	}
}
