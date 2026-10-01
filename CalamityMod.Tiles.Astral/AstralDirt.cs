using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Astral;

public class AstralDirt : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Dirt"]);
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeAstralTiles(base.Type);
		CalamityUtils.MergeWithOres(base.Type);
		CalamityUtils.SetMerge(base.Type, 2);
		CalamityUtils.SetMerge(base.Type, 23);
		CalamityUtils.SetMerge(base.Type, 109);
		CalamityUtils.SetMerge(base.Type, 199);
		base.DustType = ModContent.DustType<AstralBasic>();
		AddMapEntry(new Color(59, 50, 77));
		TileID.Sets.ChecksForMerge[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		TileID.Sets.CanBeClearedDuringOreRunner[base.Type] = true;
		TileID.Sets.Conversion.Dirt[base.Type] = true;
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile up = Main.tile[i, j - 1];
		Tile down = Main.tile[i, j + 1];
		Tile left = Main.tile[i - 1, j];
		Tile right = Main.tile[i + 1, j];
		if (WorldGen.genRand.NextBool(3) && (up.TileType == ModContent.TileType<AstralGrass>() || down.TileType == ModContent.TileType<AstralGrass>() || left.TileType == ModContent.TileType<AstralGrass>() || right.TileType == ModContent.TileType<AstralGrass>()))
		{
			WorldGen.SpreadGrass(i, j, base.Type, ModContent.TileType<AstralGrass>(), repeat: false);
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		sightColor = Color.Cyan;
		return true;
	}
}
