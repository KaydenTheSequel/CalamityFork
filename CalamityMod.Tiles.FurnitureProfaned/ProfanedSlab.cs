using CalamityMod.Dusts.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureProfaned;

public class ProfanedSlab : ModTile
{
	private int animationFrameWidth = 234;

	public override void SetStaticDefaults()
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeDecorativeTiles(base.Type);
		CalamityUtils.MergeSmoothTiles(base.Type);
		CalamityUtils.SetMerge(base.Type, ModContent.TileType<ProfanedRock>());
		base.HitSound = SoundID.Tink;
		base.MineResist = 4f;
		AddMapEntry(new Color(122, 66, 59));
		base.AnimationFrameHeight = 90;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, ModContent.DustType<ProfanedTileRock>(), 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = i % 4 * animationFrameWidth;
		frameYOffset = j % 4 * base.AnimationFrameHeight;
	}
}
