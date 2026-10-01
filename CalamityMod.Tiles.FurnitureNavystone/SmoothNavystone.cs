using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureNavystone;

public class SmoothNavystone : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeSmoothTiles(base.Type);
		CalamityUtils.MergeDecorativeTiles(base.Type);
		CalamityUtils.MergeWithAbyss(base.Type);
		CalamityUtils.MergeWithDesert(base.Type);
		TileID.Sets.ChecksForMerge[base.Type] = true;
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		base.HitSound = SoundID.Tink;
		base.DustType = 96;
		AddMapEntry(new Color(44, 57, 64));
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
