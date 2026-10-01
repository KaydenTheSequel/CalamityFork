using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Ores;

public class HallowedOre : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileSolid[base.Type] = true;
		Main.tileMergeDirt[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileOreFinderPriority[base.Type] = 690;
		TileID.Sets.Ore[base.Type] = true;
		TileID.Sets.OreMergesWithMud[base.Type] = true;
		Main.tileShine[base.Type] = 2000;
		Main.tileShine2[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		AddMapEntry(new Color(250, 250, 150), CreateMapEntryName());
		base.MineResist = 2f;
		base.MinPick = 180;
		base.HitSound = SoundID.Tink;
		Main.tileSpelunker[base.Type] = true;
		this.RegisterBlendMergeWith(117);
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 28f / 75f;
		g = 0.365f;
		b = 31f / 150f;
	}
}
