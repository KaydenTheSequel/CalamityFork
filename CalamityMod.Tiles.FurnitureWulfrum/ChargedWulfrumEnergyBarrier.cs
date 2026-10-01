using System;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureWulfrum;

public class ChargedWulfrumEnergyBarrier : ModTile
{
	public static int TypeCache;

	public Asset<Texture2D> ReflectTexture;

	public override void SetStaticDefaults()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		TypeCache = base.Type;
		Main.tileLighted[base.Type] = true;
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = false;
		Main.tileBrick[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeDecorativeTiles(base.Type);
		base.DustType = 108;
		AddMapEntry(new Color(112, 244, 244));
		base.HitSound = SoundID.Shatter;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		float brightness = 0.9f;
		Color val = new Color(112, 244, 244);
		Color blue = default(Color);
		((Color)(ref blue))._002Ector(54, 177, 221);
		Color value = Color.Lerp(val, blue, (MathF.Sin((float)(-j) / 80f + (float)Main.GameUpdateCount * 0.017f + (float)i / 40f) + 1f) / 2f);
		Color value2 = Color.Lerp(val, blue, (MathF.Sin((float)(j - 100) / 50f + (float)Main.GameUpdateCount * 0.004f + (float)(-i) / 30f) + 1f) / 2f);
		r = (float)(((Color)(ref value)).R + ((Color)(ref value2)).R) / 800f;
		g = (float)(((Color)(ref value)).G + ((Color)(ref value2)).G) / 800f;
		b = (float)(((Color)(ref value)).B + ((Color)(ref value2)).B) / 800f;
		r *= brightness;
		g *= brightness;
		b *= brightness;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.tile[i, j].IsTileActuallyInvisible())
		{
			MathHelper.Clamp(0.2f - (float)(j / 680), 0f, 0.2f);
			float num = (float)Main.GameUpdateCount * 0.094f;
			int scalar = i - j / 2;
			float wave1 = num * -50f + (float)(scalar * 12);
			float wave1angle = 0.3f + 0.25f * MathF.Sin(MathHelper.ToRadians(wave1));
			float transparency = 0.05f + wave1angle;
			TileID.Sets.DrawsWalls[base.Type] = true;
			Main.tileNoSunLight[base.Type] = false;
			if (ReflectTexture == null)
			{
				ReflectTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureWulfrum/ChargedWulfrumEnergyBarrierReflect", (AssetRequestMode)2);
			}
			Texture2D tex = ReflectTexture.Value;
			Tile tile = Main.tile[i, j];
			Rectangle frame = default(Rectangle);
			((Rectangle)(ref frame))._002Ector((int)tile.TileFrameX, (int)tile.TileFrameY, 16, 16);
			TileFramingSystem.SlopedGlowmask(in tile, i, j, tex, frame, CalamityUtils.ApplyPaint(Main.tile[i, j].TileColor, Color.White * transparency, deepPaintOnly: false), default(Vector2));
		}
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		TileFramingSystem.CompactFraming(i, j, resetFrame);
		return false;
	}
}
