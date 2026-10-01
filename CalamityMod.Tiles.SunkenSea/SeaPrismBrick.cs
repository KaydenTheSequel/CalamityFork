using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class SeaPrismBrick : ModTile
{
	private static int sheetWidth = 216;

	private static int sheetHeight = 72;

	public override void SetStaticDefaults()
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		Main.tileShine[base.Type] = 3500;
		Main.tileShine2[base.Type] = true;
		TileID.Sets.DrawsWalls[base.Type] = true;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		base.DustType = 33;
		AddMapEntry(new Color(47, 193, 236));
		base.HitSound = SoundID.Tink;
		Main.tileSpelunker[base.Type] = true;
		base.MinPick = 55;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		int xPos = i % 3;
		int yPos = j % 3;
		frameXOffset = xPos * sheetWidth;
		frameYOffset = yPos * sheetHeight;
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		TileFramingSystem.CompactFraming(i, j, resetFrame);
		return false;
	}
}
