using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Ores;

public class ExodiumOre : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithSet(base.Type, 408);
		TileID.Sets.Ore[base.Type] = true;
		TileID.Sets.OreMergesWithMud[base.Type] = true;
		AddMapEntry(new Color(51, 48, 68), CreateMapEntryName());
		base.MineResist = 3f;
		base.MinPick = 225;
		base.HitSound = SoundID.Tink;
		Main.tileOreFinderPriority[base.Type] = 760;
		Main.tileSpelunker[base.Type] = true;
		base.SetStaticDefaults();
		TileID.Sets.ChecksForMerge[base.Type] = true;
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(408);
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 2 : 4);
	}
}
