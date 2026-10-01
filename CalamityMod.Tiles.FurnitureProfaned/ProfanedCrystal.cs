using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureProfaned;

public class ProfanedCrystal : ModTile
{
	private int subsheetWidth = 324;

	private int subsheetHeight = 90;

	public override void SetStaticDefaults()
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeDecorativeTiles(base.Type);
		CalamityUtils.MergeSmoothTiles(base.Type);
		base.HitSound = SoundID.Shatter;
		base.MineResist = 1f;
		AddMapEntry(new Color(181, 136, 177));
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
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 205, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = i % 2 * subsheetWidth;
		frameYOffset = j % 2 * subsheetHeight;
	}
}
