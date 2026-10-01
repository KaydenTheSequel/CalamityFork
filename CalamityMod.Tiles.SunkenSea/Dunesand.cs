using CalamityMod.Projectiles.Typeless;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

[LegacyName(new string[] { "RuneSand" })]
public class Dunesand : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		TileID.Sets.GeneralPlacementTiles[base.Type] = false;
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithDesert(base.Type);
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		Main.tileShine2[base.Type] = false;
		base.DustType = 147;
		AddMapEntry(new Color(231, 135, 100));
		Main.tileSand[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Sand"]);
		TileID.Sets.Suffocate[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		TileID.Sets.Conversion.Sand[base.Type] = true;
		TileID.Sets.ForAdvancedCollision.ForSandshark[base.Type] = true;
		TileID.Sets.Falling[base.Type] = true;
		TileID.Sets.FallingBlockProjectile[base.Type] = new TileID.Sets.FallingBlockProjectileInfo(ModContent.ProjectileType<DunesandBallFalling>());
		this.RegisterBlendMergeWith(396);
		this.RegisterBlendMergeWith(53);
		this.RegisterBlendMergeWith(397);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}
}
