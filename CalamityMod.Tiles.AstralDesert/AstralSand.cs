using CalamityMod.Projectiles.Typeless;
using CalamityMod.Tiles.Astral;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.AstralDesert;

public class AstralSand : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileSand[base.Type] = true;
		Main.tileBrick[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Sand"]);
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithDesert(base.Type);
		CalamityUtils.MergeAstralTiles(base.Type);
		base.DustType = 108;
		AddMapEntry(new Color(187, 220, 237));
		TileID.Sets.Suffocate[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		TileID.Sets.Conversion.Sand[base.Type] = true;
		TileID.Sets.ForAdvancedCollision.ForSandshark[base.Type] = true;
		TileID.Sets.Falling[base.Type] = true;
		TileID.Sets.FallingBlockProjectile[base.Type] = new TileID.Sets.FallingBlockProjectileInfo(ModContent.ProjectileType<AstralSandBallFalling>(), 15);
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(ModContent.TileType<AstralDirt>());
		this.RegisterBlendMergeWith(53);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool HasWalkDust()
	{
		return Main.rand.NextBool(3);
	}

	public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color)
	{
		base.DustType = 108;
	}

	public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		sightColor = Color.Cyan;
		return true;
	}
}
