using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace CalamityMod.Tiles.FurnitureVoid;

public class VoidstoneSlab : GlowMaskTile
{
	public override void SetupStatic()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileMergeDirt[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		base.HitSound = SoundID.Tink;
		base.MineResist = 7f;
		base.MinPick = 180;
		AddMapEntry(new Color(27, 24, 31));
		base.AnimationFrameHeight = 270;
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
		frameYOffset = base.AnimationFrameHeight * TileFramingSystem.GetVariation3x3_01234_Low3(i, j);
	}

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return new Color(75, 75, 75);
	}
}
