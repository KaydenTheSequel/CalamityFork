using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.SunkenSea.Ambient;

public class GilHerb : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Main.tileCut[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileNoFail[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		TileID.Sets.IsVine[base.Type] = true;
		TileID.Sets.VineThreads[base.Type] = true;
		AddMapEntry(new Color(124, 67, 197));
		base.DustType = 2;
		base.HitSound = SoundID.Grass;
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		Tile tile = Framing.GetTileSafely(i, j + 1);
		if (tile.HasTile && tile.TileType == base.Type)
		{
			WorldGen.KillTile(i, j + 1);
		}
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		Tile tileAbove = Framing.GetTileSafely(i, j - 1);
		int type = -1;
		if (tileAbove.HasTile && !tileAbove.BottomSlope)
		{
			type = tileAbove.TileType;
		}
		if (type == ModContent.TileType<Limestone>() || type == base.Type)
		{
			return true;
		}
		WorldGen.KillTile(i, j);
		return true;
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tileBelow = Framing.GetTileSafely(i, j + 1);
		if (!WorldGen.genRand.NextBool(5) || tileBelow.HasTile || tileBelow.LiquidType == 1)
		{
			return;
		}
		bool PlaceVine = false;
		int Test = j;
		while (Test > j - 10)
		{
			Tile testTile = Framing.GetTileSafely(i, Test);
			if (testTile.BottomSlope)
			{
				break;
			}
			if (!testTile.HasTile || testTile.TileType != ModContent.TileType<Limestone>())
			{
				Test--;
				continue;
			}
			PlaceVine = true;
			break;
		}
		if (PlaceVine)
		{
			tileBelow.TileType = base.Type;
			tileBelow.HasTile = true;
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, j + 1, 3);
			}
		}
	}
}
