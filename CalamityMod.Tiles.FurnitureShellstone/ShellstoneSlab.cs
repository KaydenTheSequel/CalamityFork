using CalamityMod.Systems;
using CalamityMod.Tiles.SunkenSea;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureShellstone;

public class ShellstoneSlab : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		base.HitSound = SoundID.Tink;
		base.DustType = 17;
		AddMapEntry(new Color(156, 191, 199));
		this.RegisterBlendMergeWith(ModContent.TileType<Navystone>());
		this.RegisterBlendMergeWith(ModContent.TileType<EutrophicSand>());
		this.RegisterBlendMergeWith(ModContent.TileType<Runestone>());
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}
}
