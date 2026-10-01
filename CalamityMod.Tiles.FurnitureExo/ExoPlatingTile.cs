using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace CalamityMod.Tiles.FurnitureExo;

public class ExoPlatingTile : GlowMaskTile
{
	public override void SetupStatic()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeDecorativeTiles(base.Type);
		base.MineResist = 3f;
		base.HitSound = SoundID.Tink;
		AddMapEntry(new Color(52, 67, 78));
		base.AnimationFrameHeight = 90;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 107, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		int yPos = j % 2;
		frameYOffset = yPos * base.AnimationFrameHeight;
	}

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}
}
