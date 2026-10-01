using CalamityMod.Tiles.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class AerialiteOreGen
{
	public const int CloudOreConversionChance = 365;

	public static void Generate()
	{
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode == 1)
		{
			return;
		}
		for (int x = 5; x < Main.maxTilesX - 5; x++)
		{
			for (int y = 5; (double)y < Main.worldSurface; y++)
			{
				Tile tile = Main.tile[x, y];
				if ((tile.TileType == 189 || ((tile.TileType == 474 || tile.TileType == 195) && WorldGen.remixWorldGen)) && tile.HasTile && WorldGen.genRand.NextBool(365))
				{
					int radius = (int)((float)WorldGen.genRand.Next(3, 5) * WorldGen.genRand.NextFloat(0.74f, 0.82f));
					ShapeData circle = new ShapeData();
					ShapeData biggerCircle = new ShapeData();
					GenAction blotchMod = new Modifiers.Blotches(2, 0.4);
					WorldUtils.Gen(new Point(x, y), new Shapes.Circle(radius + 1), Actions.Chain(blotchMod.Output(biggerCircle)));
					WorldUtils.Gen(new Point(x, y), new ModShapes.All(biggerCircle), Actions.Chain(new Actions.ClearTile(), new Actions.PlaceTile(tile.TileType)));
					WorldUtils.Gen(new Point(x, y), new Shapes.Circle(radius), Actions.Chain(blotchMod.Output(circle)));
					WorldUtils.Gen(new Point(x, y), new ModShapes.All(circle), Actions.Chain(new Actions.ClearTile(), new Actions.PlaceTile((ushort)ModContent.TileType<AerialiteOreDisenchanted>())));
				}
			}
		}
	}

	public static void Enchant()
	{
		if (Main.netMode == 1)
		{
			return;
		}
		ushort disenchantedOreID = (ushort)ModContent.TileType<AerialiteOreDisenchanted>();
		ushort enchantedOreID = (ushort)ModContent.TileType<AerialiteOre>();
		for (int x = 5; x < Main.maxTilesX - 5; x++)
		{
			for (int y = 5; (double)y < Main.worldSurface; y++)
			{
				if (Main.tile[x, y].TileType == disenchantedOreID)
				{
					Main.tile[x, y].TileType = enchantedOreID;
					WorldGen.SquareTileFrame(x, y);
					if (Main.dedServ)
					{
						NetMessage.SendTileSquare(-1, x, y);
					}
				}
			}
		}
	}
}
