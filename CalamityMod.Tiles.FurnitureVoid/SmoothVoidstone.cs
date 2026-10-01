using System;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace CalamityMod.Tiles.FurnitureVoid;

public class SmoothVoidstone : GlowMaskTile
{
	private int animationFrameWidth = 288;

	public override void SetupStatic()
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileMergeDirt[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeSmoothTiles(base.Type);
		CalamityUtils.MergeDecorativeTiles(base.Type);
		CalamityUtils.MergeWithAbyss(base.Type);
		base.HitSound = SoundID.Tink;
		base.MineResist = 2.1f;
		AddMapEntry(new Color(27, 24, 31));
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 180, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = animationFrameWidth * TileFramingSystem.GetVariation4x4_01_Low0(i, j);
	}

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		float brightness = 1f;
		float timeFactor = (float)Main.GameUpdateCount * 0.007f;
		brightness *= MathF.Sin((float)i / 18f + timeFactor);
		brightness *= MathF.Sin((float)j / 18f + timeFactor);
		brightness *= MathF.Sin((float)i * 18f + timeFactor);
		brightness *= MathF.Sin((float)j * 18f + timeFactor);
		return Color.White * brightness;
	}
}
