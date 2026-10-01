using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Ores;

public class UelibloomOre : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileOreFinderPriority[base.Type] = 950;
		CalamityUtils.MergeWithGeneral(base.Type);
		TileID.Sets.Ore[base.Type] = true;
		TileID.Sets.OreMergesWithMud[base.Type] = true;
		AddMapEntry(new Color(255, 170, 51), CreateMapEntryName());
		base.MineResist = 3f;
		base.MinPick = 225;
		base.HitSound = SoundID.Tink;
		Main.tileSpelunker[base.Type] = true;
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(59);
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
