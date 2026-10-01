using CalamityMod.Systems;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea;

public class HardenedEutrophicSand : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		TileID.Sets.HasSlopeFrames[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithDesert(base.Type);
		Main.tileShine[base.Type] = 2500;
		Main.tileShine2[base.Type] = true;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		base.DustType = 146;
		AddMapEntry(new Color(66, 162, 209));
		this.RegisterBlendMergeWith(ModContent.TileType<EutrophicSand>());
		this.RegisterBlendMergeWith(ModContent.TileType<Navystone>());
		this.RegisterBlendMergeWith(396);
		this.RegisterBlendMergeWith(397);
		this.RegisterBlendMergeWith(53);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		Tile up = Main.tile[i, j - 1];
		Tile up2 = Main.tile[i, j - 2];
		if (!up.HasTile && !up2.HasTile && up.LiquidAmount > 0 && up2.LiquidAmount > 0 && !tile.LeftSlope && !tile.RightSlope && !tile.IsHalfBlock)
		{
			if (WorldGen.genRand.NextBool(8))
			{
				ushort[] TubeCorals = new ushort[2]
				{
					(ushort)ModContent.TileType<TubeCoral>(),
					(ushort)ModContent.TileType<SmallTubeCoral>()
				};
				ushort newObject = Main.rand.Next(TubeCorals);
				WorldGen.PlaceObject(i, j - 1, newObject, mute: true);
				NetMessage.SendObjectPlacement(-1, i, j - 1, newObject, 0, 0, -1, -1);
			}
			if (WorldGen.genRand.NextBool(10))
			{
				WorldGen.PlaceObject(i, j - 1, (ushort)ModContent.TileType<SeaAnemone>(), mute: true);
				NetMessage.SendObjectPlacement(-1, i, j - 1, (ushort)ModContent.TileType<SeaAnemone>(), 0, 0, -1, -1);
			}
		}
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
}
