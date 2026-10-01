using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Abyss.AbyssAmbient;

public class SulphurTentacleCorals : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileCut[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileNoAttach[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		Main.tileFrameImportant[base.Type] = true;
		TileID.Sets.ReplaceTileBreakUp[base.Type] = true;
		TileID.Sets.SwaysInWindBasic[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Plant"]);
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(32, 65, 65));
		base.DustType = 32;
		base.SetStaticDefaults();
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		Tile tileBelow = Framing.GetTileSafely(i, j + 1);
		int type = -1;
		if (tileBelow.HasTile)
		{
			type = tileBelow.TileType;
		}
		if (type == ModContent.TileType<SulphurousShale>())
		{
			return true;
		}
		WorldGen.KillTile(i, j);
		return true;
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = -30;
		height = 48;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		Tile tile = Framing.GetTileSafely(i, j);
		if (tile.TileFrameX <= 324)
		{
			r = 0.26f;
			g = 0.4f;
			b = 0.41f;
		}
		if (tile.TileFrameX > 324)
		{
			r = 0.46f;
			g = 0.51f;
			b = 0f;
		}
	}
}
