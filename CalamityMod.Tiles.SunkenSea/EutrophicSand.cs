using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class EutrophicSand : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		TileID.Sets.GeneralPlacementTiles[base.Type] = false;
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Sand"]);
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithDesert(base.Type);
		Main.tileShine[base.Type] = 2500;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		base.DustType = 146;
		AddMapEntry(new Color(157, 183, 206));
		Main.tileSand[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Sand"]);
		TileID.Sets.Suffocate[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		TileID.Sets.Conversion.Sand[base.Type] = true;
		TileID.Sets.ForAdvancedCollision.ForSandshark[base.Type] = true;
		this.RegisterBlendMergeWith(ModContent.TileType<EutrophicSand>());
		this.RegisterBlendMergeWith(ModContent.TileType<Navystone>());
		this.RegisterBlendMergeWith(ModContent.TileType<Shellstone>());
		this.RegisterBlendMergeWith(396);
		this.RegisterBlendMergeWith(53);
		this.RegisterBlendMergeWith(397);
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		Tile up = Main.tile[i, j - 1];
		Tile up2 = Main.tile[i, j - 2];
		if (WorldGen.genRand.NextBool(2) && !up.HasTile && !up2.HasTile && up.LiquidAmount > 0 && up2.LiquidAmount > 0 && !tile.LeftSlope && !tile.RightSlope && !tile.IsHalfBlock)
		{
			up.TileType = (ushort)ModContent.TileType<SunkenKelp>();
			up.HasTile = true;
			WorldGen.SquareTileFrame(i, j - 1);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, j - 1, 3);
			}
		}
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
