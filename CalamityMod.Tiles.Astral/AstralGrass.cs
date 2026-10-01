using CalamityMod.Dusts;
using CalamityMod.Items.Placeables.Astral;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Astral;

public class AstralGrass : ModTile
{
	private int animationFrameWidth = 288;

	public override void SetStaticDefaults()
	{
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileBrick[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Grass"]);
		CalamityUtils.SetMerge(base.Type, ModContent.TileType<AstralDirt>());
		CalamityUtils.SetMerge(base.Type, 2);
		CalamityUtils.SetMerge(base.Type, 23);
		CalamityUtils.SetMerge(base.Type, 109);
		CalamityUtils.SetMerge(base.Type, 199);
		base.DustType = ModContent.DustType<AstralBasic>();
		RegisterItemDrop(ModContent.ItemType<global::CalamityMod.Items.Placeables.Astral.AstralDirt>());
		AddMapEntry(new Color(133, 109, 140));
		TileID.Sets.Grass[base.Type] = true;
		TileID.Sets.Conversion.Grass[base.Type] = true;
		TileID.Sets.NeedsGrassFraming[base.Type] = true;
		TileID.Sets.NeedsGrassFramingDirt[base.Type] = ModContent.TileType<AstralDirt>();
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
	}

	public override void NumDust(int i, int j, bool fail, ref int Type)
	{
		Type = (fail ? 1 : 3);
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		Tile up = Main.tile[i, j - 1];
		Tile up2 = Main.tile[i, j - 2];
		if (WorldGen.genRand.NextBool(10) && !up.HasTile && !up2.HasTile && (up.LiquidAmount <= 0 || up2.LiquidAmount <= 0) && !tile.LeftSlope && !tile.RightSlope && !tile.IsHalfBlock)
		{
			up.TileType = (ushort)ModContent.TileType<AstralTallPlants>();
			up.HasTile = true;
			up.TileFrameY = 0;
			up.TileFrameX = (short)(WorldGen.genRand.Next(20) * 18);
			WorldGen.SquareTileFrame(i, j - 1);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, j - 1, 3);
			}
		}
		if (WorldGen.genRand.NextBool(10) && !up.HasTile && !up2.HasTile && (up.LiquidAmount <= 0 || up2.LiquidAmount <= 0) && !tile.LeftSlope && !tile.RightSlope && !tile.IsHalfBlock)
		{
			up.TileType = (ushort)ModContent.TileType<AstralShortPlants>();
			up.HasTile = true;
			up.TileFrameY = 0;
			up.TileFrameX = (short)(WorldGen.genRand.Next(23) * 18);
			WorldGen.SquareTileFrame(i, j - 1);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, j - 1, 3);
			}
		}
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = animationFrameWidth * TileFramingSystem.GetVariation4x4_01_Low0(i, j);
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		if (fail && !effectOnly)
		{
			Main.tile[i, j].TileType = (ushort)ModContent.TileType<AstralDirt>();
		}
	}

	public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		sightColor = Color.Cyan;
		return true;
	}
}
