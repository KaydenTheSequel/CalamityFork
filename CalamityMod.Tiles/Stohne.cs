using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public class Stohne : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		base.HitSound = SoundID.Tink;
		AddMapEntry(new Color(117, 42, 14));
		base.DustType = 148;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithOres(base.Type);
		TileID.Sets.Stone[base.Type] = true;
		TileID.Sets.Conversion.Stone[base.Type] = true;
		TileID.Sets.CanBeClearedDuringOreRunner[base.Type] = true;
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(59);
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}
}
