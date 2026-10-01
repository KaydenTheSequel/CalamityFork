using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureMarnite;

public class PolishedMarniteBlock : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		Main.tileShine[base.Type] = 3500;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeSmoothTiles(base.Type);
		CalamityUtils.MergeDecorativeTiles(base.Type);
		CalamityUtils.MergeWithAbyss(base.Type);
		base.HitSound = SoundID.Tink;
		base.MineResist = 2.1f;
		AddMapEntry(new Color(50, 46, 104));
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 240, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}
}
