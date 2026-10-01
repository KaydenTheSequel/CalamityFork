using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Ores;

public class CryonicOre : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileOreFinderPriority[base.Type] = 675;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithSnow(base.Type);
		TileID.Sets.Ore[base.Type] = true;
		AddMapEntry(new Color(0, 0, 150), CreateMapEntryName());
		base.MineResist = 2f;
		base.MinPick = 180;
		base.HitSound = SoundID.Tink;
		Main.tileSpelunker[base.Type] = true;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		TileFramingSystem.CustomMergeFrame(i, j, base.Type, 147, false, false, false, false, true);
		return false;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.02f;
		g = 0.02f;
		b = 0.06f;
	}
}
