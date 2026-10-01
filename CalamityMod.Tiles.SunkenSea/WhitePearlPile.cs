using System;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Sounds;
using CalamityMod.Systems;
using CalamityMod.Tiles.Abyss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class WhitePearlPile : ModTile
{
	public Asset<Texture2D> GlintTexture;

	public Vector2 GlintDir;

	public override void SetStaticDefaults()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		GlintTexture = ModContent.Request<Texture2D>(Texture + "_Glint", (AssetRequestMode)2);
		GlintDir = new Vector2(1f, -1f);
		((Vector2)(ref GlintDir)).Normalize();
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		Main.tileShine[base.Type] = 3500;
		Main.tileShine2[base.Type] = true;
		base.HitSound = CommonCalamitySounds.VoidstoneMine;
		base.DustType = 149;
		AddMapEntry(new Color(217, 209, 213));
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		TileID.Sets.Falling[base.Type] = true;
		TileID.Sets.FallingBlockProjectile[base.Type] = new TileID.Sets.FallingBlockProjectileInfo(ModContent.ProjectileType<WhitePearlFalling>(), 15);
		this.RegisterBlendMergeWith(ModContent.TileType<Shellstone>());
		this.RegisterBlendMergeWith(ModContent.TileType<AbyssGravel>());
		this.RegisterBlendMergeWith(ModContent.TileType<EutrophicSand>());
		this.RegisterBlendMergeWith(ModContent.TileType<Navystone>());
		this.RegisterBlendMergeWith(ModContent.TileType<Runestone>());
		this.RegisterBlendMergeWith(396);
		this.RegisterBlendMergeWith(53);
		this.RegisterBlendMergeWith(397);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(57);
		this.RegisterBlendMergeWith(59);
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}

	public override void PostTileFrame(int i, int j, int up, int down, int left, int right, int upLeft, int upRight, int downLeft, int downRight)
	{
		Tile tile = Main.tile[i, j];
		ref TileSpecialDrawData reference = ref tile.Get<TileSpecialDrawData>();
		tile = Main.tile[i - 1, j];
		int flag;
		if (tile.HasTile)
		{
			tile = Main.tile[i + 1, j];
			if (tile.HasTile)
			{
				tile = Main.tile[i, j - 1];
				if (tile.HasTile)
				{
					tile = Main.tile[i, j + 1];
					flag = ((!tile.HasTile) ? 1 : 0);
					goto IL_0078;
				}
			}
		}
		flag = 1;
		goto IL_0078;
		IL_0078:
		reference.Flag0 = (byte)flag != 0;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Framing.GetTileSafely(i, j);
		Vector2 offScreen = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Vector2 position = new Vector2((float)(i * 16), (float)(j * 16)) - Main.screenPosition + offScreen;
		int frameX = tile.TileFrameX + i % 1;
		int frameY = tile.TileFrameY + j % 1;
		Rectangle sourceRect = new Rectangle(frameX, frameY, 16, 16);
		Vector2 screenPos = position;
		float projection = Vector2.Dot(screenPos, GlintDir);
		float screenDiagonalLength = Vector2.Dot(new Vector2((float)Main.screenWidth, (float)Main.screenHeight), GlintDir);
		float stripeWidth = 100f;
		Color lightColor = Lighting.GetColor(i, j) * 6f;
		DrawGlint(screenDiagonalLength * 0.05f);
		DrawGlint(screenDiagonalLength * 0.5f);
		DrawGlint(screenDiagonalLength * 1.05f);
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
				spriteBatch.Draw(GlintTexture.Value, position, (Rectangle?)sourceRect, lightColor * strength, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
		}
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		float projection;
		float stripeWidth;
		float maxStrength;
		if (Main.tile[i, j].Get<TileSpecialDrawData>().Flag0)
		{
			Color litColor = Lighting.GetColor(i, j);
			float brightness = (float)(((Color)(ref litColor)).R + ((Color)(ref litColor)).G + ((Color)(ref litColor)).B) / 765f;
			float darknessFactor = 1f - brightness;
			Vector2 screenPos = new Vector2((float)(i * 16), (float)(j * 16)) - Main.screenPosition;
			projection = Vector2.Dot(screenPos, GlintDir);
			float num = Vector2.Dot(new Vector2((float)Main.screenWidth, (float)Main.screenHeight), GlintDir);
			stripeWidth = 100f;
			maxStrength = 0f;
			UpdateMaxStrength(num * 0.05f);
			UpdateMaxStrength(num * 0.5f);
			UpdateMaxStrength(num * 1.05f);
			if (maxStrength > 0f)
			{
				float intensity = 0.6f * maxStrength * (0.5f + darknessFactor * 0.5f);
				r = 37f / 51f * intensity;
				g = 0.6431373f * intensity;
				b = 0.72156864f * intensity;
			}
		}
		void UpdateMaxStrength(float beamCenter)
		{
			float dist = Math.Abs(projection - beamCenter);
			float strength = MathHelper.Clamp(1f - dist / stripeWidth, 0f, 1f);
			if (strength > maxStrength)
			{
				maxStrength = strength;
			}
		}
	}
}
