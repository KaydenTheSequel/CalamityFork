using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Crags;

public class LavaPistil : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		Main.tileCut[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileNoAttach[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		TileID.Sets.ReplaceTileBreakUp[base.Type] = true;
		TileID.Sets.SwaysInWindBasic[base.Type] = false;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Plant"]);
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
		TileObjectData.addTile(base.Type);
		base.HitSound = SoundID.Grass;
		AddMapEntry(new Color(170, 50, 180));
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
		if (type == ModContent.TileType<ScorchedRemainsGrass>())
		{
			return true;
		}
		WorldGen.KillTile(i, j);
		return true;
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = -128;
		height = 144;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 1f;
		g = 0.80784315f;
		b = 42f / 85f;
	}
}
