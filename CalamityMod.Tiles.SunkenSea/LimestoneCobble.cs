using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class LimestoneCobble : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		base.HitSound = SoundID.Tink;
		base.DustType = 22;
		AddMapEntry(new Color(200, 120, 101));
		this.RegisterBlendMergeWith(ModContent.TileType<Shellstone>());
		this.RegisterBlendMergeWith(ModContent.TileType<Navystone>());
		this.RegisterBlendMergeWith(ModContent.TileType<Runestone>());
		this.RegisterBlendMergeWith(ModContent.TileType<PolypSand>());
		this.RegisterBlendMergeWith(ModContent.TileType<ScarletSeaGrassTile>());
		this.RegisterBlendMergeWith(ModContent.TileType<EutrophicSand>());
		this.RegisterBlendMergeWith(ModContent.TileType<VolcanicSand>());
		this.RegisterBlendMergeWith(396);
		this.RegisterBlendMergeWith(53);
		this.RegisterBlendMergeWith(397);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(57);
		this.RegisterBlendMergeWith(59);
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}
}
