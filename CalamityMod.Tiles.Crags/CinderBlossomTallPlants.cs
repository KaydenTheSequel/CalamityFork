using CalamityMod.Items.Placeables.Crags;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Crags;

public class CinderBlossomTallPlants : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		Main.tileCut[base.Type] = true;
		Main.tileSolid[base.Type] = false;
		Main.tileNoAttach[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		TileID.Sets.ReplaceTileBreakUp[base.Type] = true;
		TileID.Sets.SwaysInWindBasic[base.Type] = true;
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
		offsetY = -30;
		height = 48;
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 1 && Main.rand.NextBool(20))
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), new Vector2((float)i, (float)j) * 16f, ModContent.ItemType<CinderBlossomSeeds>());
		}
	}
}
