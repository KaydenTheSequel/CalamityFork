using CalamityMod.Tiles.Crags;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Ores;

[LegacyName(new string[] { "CharredOre" })]
public class InfernalSuevite : ModTile
{
	private int sheetWidth = 234;

	private int sheetHeight = 90;

	public override void SetStaticDefaults()
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		Main.tileOreFinderPriority[base.Type] = 675;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithHell(base.Type);
		TileID.Sets.Ore[base.Type] = true;
		AddMapEntry(new Color(17, 16, 26), CreateMapEntryName());
		base.MineResist = 2f;
		base.MinPick = 150;
		base.HitSound = SoundID.Tink;
		base.DustType = 235;
		Main.tileSpelunker[base.Type] = true;
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
		frameXOffset = i % 2 * sheetWidth;
		frameYOffset = j % 2 * sheetHeight;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.5f;
		g = 0f;
		b = 0f;
	}
}
