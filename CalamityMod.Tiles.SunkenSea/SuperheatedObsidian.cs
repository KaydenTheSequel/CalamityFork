using System;
using CalamityMod.Sounds;
using CalamityMod.Systems;
using CalamityMod.Tiles.Abyss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class SuperheatedObsidian : ModTile
{
	public Asset<Texture2D> GlintTexture;

	public Vector2 GlintDir;

	public override void SetStaticDefaults()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		GlintTexture = ModContent.Request<Texture2D>(Texture + "_Glint", (AssetRequestMode)2);
		GlintDir = new Vector2(1f, 1f);
		((Vector2)(ref GlintDir)).Normalize();
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		Main.tileShine2[base.Type] = true;
		base.HitSound = CommonCalamitySounds.VoidstoneMine;
		base.DustType = 149;
		AddMapEntry(new Color(67, 61, 91));
		this.RegisterBlendMergeWith(ModContent.TileType<Shellstone>());
		this.RegisterBlendMergeWith(ModContent.TileType<AbyssGravel>());
		this.RegisterBlendMergeWith(ModContent.TileType<EutrophicSand>());
		this.RegisterBlendMergeWith(ModContent.TileType<Navystone>());
		this.RegisterBlendMergeWith(ModContent.TileType<Runestone>());
		this.RegisterBlendMergeWith(ModContent.TileType<Basalt>());
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

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		float transparency = 1f;
		Main.tileBlockLight[base.Type] = false;
		Tile tile = Main.tile[i, j];
		Rectangle frame = new Rectangle((int)tile.TileFrameX, (int)tile.TileFrameY, 16, 16);
		Color color = Lighting.GetColor(i, j) * transparency;
		TileFramingSystem.SlopedGlowmask(in tile, i, j, TextureAssets.Tile[base.Type].Value, frame, CalamityUtils.ApplyPaint(tile.TileColor, color, deepPaintOnly: false), default(Vector2));
		Vector2 offScreen = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Vector2 position = new Vector2((float)(i * 16), (float)(j * 16)) - Main.screenPosition + offScreen;
		Vector2 screenPos = position;
		float projection = Vector2.Dot(screenPos, GlintDir);
		float screenDiagonalLength = Vector2.Dot(new Vector2((float)Main.screenWidth, (float)Main.screenHeight), GlintDir);
		float stripeWidth = 100f;
		Color lightColor = Lighting.GetColor(i, j);
		DrawGlint(screenDiagonalLength * 0.53f);
		DrawGlint(screenDiagonalLength * 0.63f);
		DrawGlint(screenDiagonalLength * 0.73f);
		return false;
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
}
