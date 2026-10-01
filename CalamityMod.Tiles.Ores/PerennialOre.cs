using System;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace CalamityMod.Tiles.Ores;

public class PerennialOre : GlowMaskTile
{
	public const int AnimationFrameWidth = 234;

	public override void SetupStatic()
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileSolid[base.Type] = true;
		Main.tileMergeDirt[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileOreFinderPriority[base.Type] = 710;
		Main.tileShine[base.Type] = 2500;
		Main.tileShine2[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		TileID.Sets.Ore[base.Type] = true;
		TileID.Sets.OreMergesWithMud[base.Type] = true;
		AddMapEntry(new Color(64, 207, 97), CreateMapEntryName());
		base.MineResist = 2f;
		base.MinPick = 200;
		base.HitSound = SoundID.Tink;
		Main.tileSpelunker[base.Type] = true;
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(59);
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void PostTileFrame(int i, int j, int up, int down, int left, int right, int upLeft, int upRight, int downLeft, int downRight)
	{
		Tile tile = Main.tile[i, j];
		short tileFrameX = tile.TileFrameX;
		short frameY = tile.TileFrameY;
		bool hasFlowerInFrame = false;
		switch (tileFrameX)
		{
		case 0:
			if (frameY == 0)
			{
				hasFlowerInFrame = true;
			}
			break;
		case 18:
			if (frameY == 18)
			{
				hasFlowerInFrame = true;
			}
			break;
		case 36:
			if (frameY == 0 || frameY == 36)
			{
				hasFlowerInFrame = true;
			}
			break;
		case 54:
			if (frameY == 18)
			{
				hasFlowerInFrame = true;
			}
			break;
		}
		tile.Get<TileSpecialDrawData>().Flag0 = hasFlowerInFrame;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.08f;
		g = 0.2f;
		b = 0.04f;
		if (Main.tile[i, j].Get<TileSpecialDrawData>().Flag0)
		{
			float timeScalar = (float)Main.GameUpdateCount * 0.017f;
			float jDiv14 = (float)j / 14f;
			float iDiv14 = (float)i / 14f;
			float brightness = 0.7f;
			brightness *= MathF.Sin(jDiv14 + timeScalar);
			brightness *= MathF.Sin(iDiv14 + timeScalar);
			brightness += 0.3f;
			float flowerPosBrightnessR = 0.83f * brightness;
			float flowerPosBrightnessG = 0.16f * brightness;
			float flowerPosBrightnessB = 0.31f * brightness;
			r = flowerPosBrightnessR;
			g = flowerPosBrightnessG;
			b = flowerPosBrightnessB;
		}
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = 234 * TileFramingSystem.GetVariation4x4_012_Low0(i, j);
	}

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return Color.White * 0.686f;
	}
}
