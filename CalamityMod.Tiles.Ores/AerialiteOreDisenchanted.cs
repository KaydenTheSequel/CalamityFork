using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Ores;

public class AerialiteOreDisenchanted : ModTile
{
	private const int AnimationFrameWidth = 234;

	public override void SetStaticDefaults()
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		Main.tileBlockLight[base.Type] = true;
		Main.tileSolid[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		TileID.Sets.Ore[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.SetMerge(base.Type, ModContent.TileType<AerialiteOre>());
		CalamityUtils.SetMerge(base.Type, 189);
		CalamityUtils.SetMerge(base.Type, 196);
		CalamityUtils.SetMerge(base.Type, 460);
		Main.tileShine2[base.Type] = false;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		base.DustType = 33;
		AddMapEntry(new Color(204, 170, 81), CreateMapEntryName());
		base.MinPick = 110;
		base.HitSound = SoundID.Tink;
		Main.tileSpelunker[base.Type] = true;
		this.RegisterBlendMergeWith(189);
		this.RegisterBlendMergeWith(196);
		this.RegisterBlendMergeWith(460);
		this.RegisterBlendMergeWith(0);
	}

	public override void PostSetDefaults()
	{
		Main.tileNoSunLight[base.Type] = false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = 234 * TileFramingSystem.GetVariation4x4_012_Low0(i, j);
	}
}
