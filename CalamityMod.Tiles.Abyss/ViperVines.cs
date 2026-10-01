using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Abyss;

public class ViperVines : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		Main.tileCut[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Plant"]);
		AddMapEntry(new Color(0, 50, 0));
		base.HitSound = SoundID.Grass;
		base.DustType = 2;
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		Main.instance.TilesRenderer.CrawlToTopOfVineAndAddSpecialPoint(j, i);
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		if (WorldGen.loadSuccess && !Framing.GetTileSafely(i, j - 1).HasTile)
		{
			WorldGen.KillTile(i, j);
			return true;
		}
		return true;
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (WorldGen.genRand.NextBool() && Main.player[Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].cordage)
		{
			Item.NewItem((IEntitySource)new EntitySource_TileBreak(i, j), new Vector2((float)(i * 16) + 8f, (float)(j * 16) + 8f), 2996, 1, false, 0, false, false);
		}
		if (Main.tile[i, j + 1] != null && Main.tile[i, j + 1].HasTile && Main.tile[i, j + 1].TileType == ModContent.TileType<ViperVines>())
		{
			WorldGen.KillTile(i, j + 1);
			if (!Main.tile[i, j + 1].HasTile && Main.netMode != 0)
			{
				NetMessage.SendData(17, -1, -1, null, 0, i, (float)j + 1f);
			}
		}
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
			if (!testTile.HasTile || testTile.TileType != ModContent.TileType<PlantyMush>())
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
