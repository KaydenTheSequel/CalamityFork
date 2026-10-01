using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Crags;

public class BrimstoneSlab : ModTile
{
	private int subsheetWidth = 450;

	private int subsheetHeight = 198;

	public override void SetStaticDefaults()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithHell(base.Type);
		AddMapEntry(new Color(79, 55, 70));
		base.MineResist = 2f;
		base.MinPick = 100;
		base.HitSound = SoundID.Tink;
		base.DustType = 235;
		this.RegisterBlendMergeWith(ModContent.TileType<BrimstoneSlag>());
		this.RegisterBlendMergeWith(57);
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = i % 2 * subsheetWidth;
		frameYOffset = j % 2 * subsheetHeight;
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BrimstoneFraming(i, j, resetFrame);
	}
}
