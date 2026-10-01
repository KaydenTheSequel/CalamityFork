using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Crags;

public class ScorchedBone : ModTile
{
	private int sheetWidth = 450;

	private int sheetHeight = 198;

	public override void SetStaticDefaults()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithHell(base.Type);
		CalamityUtils.SetMerge(base.Type, ModContent.TileType<BrimstoneSlag>());
		base.DustType = 155;
		base.HitSound = SoundID.Dig;
		base.MinPick = 100;
		AddMapEntry(new Color(87, 62, 67));
		this.RegisterBlendMergeWith(ModContent.TileType<BrimstoneSlag>());
		this.RegisterBlendMergeWith(57);
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = i % 3 * sheetWidth;
		frameYOffset = j % 3 * sheetHeight;
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BrimstoneFraming(i, j, resetFrame);
	}
}
