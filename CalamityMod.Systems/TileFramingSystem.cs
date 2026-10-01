using System.Collections.Generic;
using System.Linq;
using CalamityMod.Tiles.Abyss;
using CalamityMod.Tiles.Abyss.AbyssAmbient;
using CalamityMod.Tiles.Astral;
using CalamityMod.Tiles.AstralDesert;
using CalamityMod.Tiles.AstralSnow;
using CalamityMod.Tiles.Crags;
using CalamityMod.Tiles.Ores;
using CalamityMod.Tiles.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class TileFramingSystem : ModSystem
{
	private enum Similarity
	{
		Same,
		MergeLink,
		None
	}

	private sealed class MergeableTileGlobalTile : GlobalTile
	{
		public override bool TileFrame(int i, int j, int type, ref bool resetFrame, ref bool noBreak)
		{
			for (int k = 0; k < PlantTypes.Length; k++)
			{
				if (type == PlantTypes[k])
				{
					PlantFrame(i, j);
					return false;
				}
			}
			if (type == 52 || type == 205 || type == 115 || type == ModContent.TileType<AstralVines>())
			{
				VineFrame(i, j);
			}
			return base.TileFrame(i, j, type, ref resetFrame, ref noBreak);
		}
	}

	public static bool[][] tileMergeTypes;

	private static ushort[] PlantTypes;

	private static int[][] PlantValidGrounds;

	private static Dictionary<ushort, ushort> VineToGrass;

	private static Similarity GetSimilarity(Tile check, int myType, int mergeType)
	{
		if (!check.HasTile)
		{
			return Similarity.None;
		}
		if (check.TileType == myType || Main.tileMerge[myType][check.TileType])
		{
			return Similarity.Same;
		}
		if (check.TileType == mergeType)
		{
			return Similarity.MergeLink;
		}
		return Similarity.None;
	}

	public override void PostAddRecipes()
	{
		PlantValidGrounds = new int[TileLoader.TileCount][];
		PlantValidGrounds[3] = new int[3] { 2, 380, 78 };
		PlantValidGrounds[24] = new int[1] { 23 };
		PlantValidGrounds[61] = new int[1] { 60 };
		PlantValidGrounds[71] = new int[1] { 70 };
		PlantValidGrounds[73] = new int[3] { 2, 380, 78 };
		PlantValidGrounds[74] = new int[1] { 60 };
		PlantValidGrounds[110] = new int[1] { 109 };
		PlantValidGrounds[113] = new int[1] { 109 };
		PlantValidGrounds[201] = new int[1] { 199 };
		PlantValidGrounds[ModContent.TileType<AstralShortPlants>()] = new int[1] { ModContent.TileType<AstralGrass>() };
		PlantValidGrounds[ModContent.TileType<AstralTallPlants>()] = new int[1] { ModContent.TileType<AstralGrass>() };
		PlantValidGrounds[ModContent.TileType<CinderBlossomTallPlants>()] = new int[1] { ModContent.TileType<ScorchedRemainsGrass>() };
		PlantValidGrounds[ModContent.TileType<SulphurTentacleCorals>()] = new int[1] { ModContent.TileType<SulphurousShale>() };
		PlantValidGrounds[ModContent.TileType<AbyssKelp>()] = new int[1] { ModContent.TileType<AbyssGravel>() };
		PlantValidGrounds[ModContent.TileType<TenebrisRemnant>()] = new int[1] { ModContent.TileType<Voidstone>() };
		PlantValidGrounds[ModContent.TileType<PhoviamareHalm>()] = new int[2]
		{
			ModContent.TileType<PyreMantle>(),
			ModContent.TileType<PyreMantleMolten>()
		};
		Dictionary<ushort, ushort> dictionary = new Dictionary<ushort, ushort>();
		dictionary[52] = 2;
		dictionary[52] = 192;
		dictionary[205] = 199;
		dictionary[115] = 109;
		ushort key = (ushort)ModContent.TileType<AstralVines>();
		dictionary[key] = (ushort)ModContent.TileType<AstralGrass>();
		VineToGrass = dictionary;
		tileMergeTypes = new bool[TileLoader.TileCount][];
		for (int i = 0; i < tileMergeTypes.Length; i++)
		{
			tileMergeTypes[i] = new bool[TileLoader.TileCount];
		}
		tileMergeTypes[ModContent.TileType<AstralDirt>()][ModContent.TileType<AstralOre>()] = true;
		tileMergeTypes[ModContent.TileType<AstralDirt>()][ModContent.TileType<AstralStone>()] = true;
		tileMergeTypes[ModContent.TileType<AstralDirt>()][ModContent.TileType<AstralSand>()] = true;
		tileMergeTypes[ModContent.TileType<AstralDirt>()][ModContent.TileType<AstralSnow>()] = true;
		tileMergeTypes[ModContent.TileType<AstralDirt>()][ModContent.TileType<AstralClay>()] = true;
		tileMergeTypes[ModContent.TileType<AstralDirt>()][ModContent.TileType<NovaeSlag>()] = true;
		tileMergeTypes[ModContent.TileType<AstralSnow>()][ModContent.TileType<AstralIce>()] = true;
		tileMergeTypes[ModContent.TileType<AstralSand>()][ModContent.TileType<HardenedAstralSand>()] = true;
		tileMergeTypes[ModContent.TileType<HardenedAstralSand>()][ModContent.TileType<AstralSandstone>()] = true;
		tileMergeTypes[ModContent.TileType<AstralSandstone>()][ModContent.TileType<CelestialRemains>()] = true;
		tileMergeTypes[ModContent.TileType<BrimstoneSlag>()][ModContent.TileType<InfernalSuevite>()] = true;
		tileMergeTypes[396][ModContent.TileType<EutrophicSand>()] = true;
		tileMergeTypes[ModContent.TileType<EutrophicSand>()][ModContent.TileType<Navystone>()] = true;
		tileMergeTypes[ModContent.TileType<Navystone>()][ModContent.TileType<SeaPrism>()] = true;
		tileMergeTypes[ModContent.TileType<AbyssGravel>()][ModContent.TileType<ScoriaOre>()] = true;
		tileMergeTypes[ModContent.TileType<AbyssGravel>()][ModContent.TileType<PlantyMush>()] = true;
		tileMergeTypes[ModContent.TileType<AbyssGravel>()][ModContent.TileType<Voidstone>()] = true;
		tileMergeTypes[ModContent.TileType<AbyssGravel>()][ModContent.TileType<SulphurousSandstone>()] = true;
		tileMergeTypes[ModContent.TileType<SulphurousSandstone>()][ModContent.TileType<SulphurousSand>()] = true;
	}

	public override void Unload()
	{
		PlantValidGrounds = null;
		VineToGrass?.Clear();
		VineToGrass = null;
		tileMergeTypes = null;
	}

	public static int GetVariation4x4_012_Low0(int i, int j)
	{
		int xRel = i & 3;
		int yRel = j & 3;
		return xRel switch
		{
			0 => yRel switch
			{
				0 => 0, 
				1 => 2, 
				2 => 1, 
				_ => 2, 
			}, 
			1 => yRel switch
			{
				0 => 2, 
				1 => 0, 
				2 => 2, 
				_ => 2, 
			}, 
			2 => yRel switch
			{
				0 => 2, 
				1 => 0, 
				2 => 1, 
				_ => 2, 
			}, 
			_ => yRel switch
			{
				0 => 1, 
				1 => 2, 
				2 => 0, 
				_ => 2, 
			}, 
		};
	}

	public static int GetVariation4x4_01_Low0(int i, int j)
	{
		int xRel = i & 3;
		int yRel = j & 3;
		return xRel switch
		{
			0 => yRel switch
			{
				0 => 0, 
				1 => 0, 
				2 => 1, 
				_ => 1, 
			}, 
			1 => yRel switch
			{
				0 => 1, 
				1 => 0, 
				2 => 1, 
				_ => 1, 
			}, 
			2 => yRel switch
			{
				0 => 1, 
				1 => 0, 
				2 => 0, 
				_ => 1, 
			}, 
			_ => yRel switch
			{
				0 => 0, 
				1 => 1, 
				2 => 0, 
				_ => 1, 
			}, 
		};
	}

	public static int GetVariation3x3_01234_Low3(int i, int j)
	{
		int xRel = i % 3;
		int yRel = j % 3;
		return xRel switch
		{
			0 => yRel switch
			{
				0 => 0, 
				1 => 1, 
				_ => 2, 
			}, 
			1 => yRel switch
			{
				0 => 2, 
				1 => 3, 
				_ => 4, 
			}, 
			_ => yRel switch
			{
				0 => 4, 
				1 => 0, 
				_ => 1, 
			}, 
		};
	}

	private static bool GetMerge(Tile myTile, Tile mergeTile)
	{
		if (!mergeTile.HasTile)
		{
			return false;
		}
		int myTileID = myTile.TileType;
		int otherTileID = mergeTile.TileType;
		if (myTileID != otherTileID)
		{
			return Main.tileMerge[myTileID][otherTileID];
		}
		return true;
	}

	private static bool GetBlendSpecific(Tile myTile, Tile mergeTile, int blendType, bool includeSame)
	{
		if (!mergeTile.HasTile)
		{
			return false;
		}
		int myTileID = myTile.TileType;
		int otherTileID = mergeTile.TileType;
		if (otherTileID != blendType)
		{
			if (includeSame)
			{
				return myTileID == otherTileID;
			}
			return false;
		}
		return true;
	}

	private static void GetAdjacentTiles(int x, int y, out bool up, out bool down, out bool left, out bool right, out bool upLeft, out bool upRight, out bool downLeft, out bool downRight)
	{
		Tile tile = Main.tile[x, y];
		Tile north = Main.tile[x, y - 1];
		Tile south = Main.tile[x, y + 1];
		Tile west = Main.tile[x - 1, y];
		Tile east = Main.tile[x + 1, y];
		Tile southwest = Main.tile[x - 1, y + 1];
		Tile southeast = Main.tile[x + 1, y + 1];
		Tile northwest = Main.tile[x - 1, y - 1];
		Tile northeast = Main.tile[x + 1, y - 1];
		left = false;
		right = false;
		up = false;
		down = false;
		upLeft = false;
		upRight = false;
		downLeft = false;
		downRight = false;
		if (GetMerge(tile, north) && (north.Slope == SlopeType.Solid || north.Slope == SlopeType.SlopeDownLeft || north.Slope == SlopeType.SlopeDownRight))
		{
			up = true;
		}
		if (GetMerge(tile, south) && (south.Slope == SlopeType.Solid || south.Slope == SlopeType.SlopeUpLeft || south.Slope == SlopeType.SlopeUpRight))
		{
			down = true;
		}
		if (GetMerge(tile, west) && (west.Slope == SlopeType.Solid || west.Slope == SlopeType.SlopeDownRight || west.Slope == SlopeType.SlopeUpRight))
		{
			left = true;
		}
		if (GetMerge(tile, east) && (east.Slope == SlopeType.Solid || east.Slope == SlopeType.SlopeDownLeft || east.Slope == SlopeType.SlopeUpLeft))
		{
			right = true;
		}
		if (GetMerge(tile, north) && GetMerge(tile, west) && GetMerge(tile, northwest) && (northwest.Slope == SlopeType.Solid || northwest.Slope == SlopeType.SlopeDownRight) && (north.Slope == SlopeType.Solid || north.Slope == SlopeType.SlopeDownLeft || north.Slope == SlopeType.SlopeUpLeft) && (west.Slope == SlopeType.Solid || west.Slope == SlopeType.SlopeUpLeft || west.Slope == SlopeType.SlopeUpRight))
		{
			upLeft = true;
		}
		if (GetMerge(tile, north) && GetMerge(tile, east) && GetMerge(tile, northeast) && (northeast.Slope == SlopeType.Solid || northeast.Slope == SlopeType.SlopeDownLeft) && (north.Slope == SlopeType.Solid || north.Slope == SlopeType.SlopeDownRight || north.Slope == SlopeType.SlopeUpRight) && (east.Slope == SlopeType.Solid || east.Slope == SlopeType.SlopeUpLeft || east.Slope == SlopeType.SlopeUpRight))
		{
			upRight = true;
		}
		if (GetMerge(tile, south) && GetMerge(tile, west) && GetMerge(tile, southwest) && !southwest.IsHalfBlock && (southwest.Slope == SlopeType.Solid || southwest.Slope == SlopeType.SlopeUpRight) && (south.Slope == SlopeType.Solid || south.Slope == SlopeType.SlopeDownLeft || south.Slope == SlopeType.SlopeUpLeft) && (west.Slope == SlopeType.Solid || west.Slope == SlopeType.SlopeDownLeft || west.Slope == SlopeType.SlopeDownRight))
		{
			downLeft = true;
		}
		if (GetMerge(tile, south) && GetMerge(tile, east) && GetMerge(tile, southeast) && !southeast.IsHalfBlock && (southeast.Slope == SlopeType.Solid || southeast.Slope == SlopeType.SlopeUpLeft) && (south.Slope == SlopeType.Solid || south.Slope == SlopeType.SlopeDownRight || south.Slope == SlopeType.SlopeUpRight) && (east.Slope == SlopeType.Solid || east.Slope == SlopeType.SlopeDownLeft || east.Slope == SlopeType.SlopeDownRight))
		{
			downRight = true;
		}
	}

	private static void SetFrameAt(int x, int y, int frameX, int frameY)
	{
		Tile tile = Main.tile[x, y];
		if (tile != null)
		{
			tile.TileFrameX = (short)frameX;
			tile.TileFrameY = (short)frameY;
		}
	}

	internal static void PlantFrame(int x, int y)
	{
		if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
		{
			return;
		}
		Tile tile = Main.tile[x, y];
		int plantType = tile.TileType;
		if (y + 1 >= Main.maxTilesY)
		{
			WorldGen.KillTile(x, y);
			return;
		}
		Tile below = Main.tile[x, y + 1];
		if (!below.HasTile || !below.HasUnactuatedTile || below.IsHalfBlock || below.Slope != SlopeType.Solid)
		{
			WorldGen.KillTile(x, y);
			return;
		}
		int belowTileType = below.TileType;
		if (PlantValidGrounds[plantType] != null && PlantValidGrounds[plantType].Contains(belowTileType))
		{
			return;
		}
		int newPlantType = plantType;
		Tile tile2;
		if ((plantType == 3 || plantType == 73) && belowTileType != 2 && tile.TileFrameX >= 162)
		{
			tile2 = Main.tile[x, y];
			tile2.TileFrameX = 126;
		}
		if (plantType == 74 && belowTileType != 60 && tile.TileFrameX >= 162)
		{
			tile2 = Main.tile[x, y];
			tile2.TileFrameX = 126;
		}
		switch (belowTileType)
		{
		case 23:
			newPlantType = 24;
			if (tile.TileFrameX >= 162)
			{
				tile2 = Main.tile[x, y];
				tile2.TileFrameX = 126;
			}
			break;
		case 2:
			newPlantType = ((plantType == 113) ? 73 : 3);
			break;
		case 109:
			newPlantType = ((plantType == 73) ? 113 : 110);
			break;
		case 199:
			newPlantType = 201;
			break;
		case 70:
			newPlantType = 71;
			while (true)
			{
				tile2 = Main.tile[x, y];
				if (tile2.TileFrameX > 72)
				{
					tile2 = Main.tile[x, y];
					tile2.TileFrameX -= 72;
					continue;
				}
				break;
			}
			break;
		default:
			if (belowTileType == ModContent.TileType<AstralGrass>())
			{
				newPlantType = ((plantType == 3 || plantType == 24 || plantType == 201 || plantType == 110 || plantType == 71 || plantType == 61) ? ModContent.TileType<AstralShortPlants>() : ModContent.TileType<AstralTallPlants>());
			}
			break;
		}
		if (plantType != newPlantType)
		{
			tile2 = Main.tile[x, y];
			tile2.TileType = (ushort)newPlantType;
		}
	}

	internal static void VineFrame(int x, int y)
	{
		if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
		{
			return;
		}
		int myType = Main.tile[x, y].TileType;
		Tile north = ((y <= 0) ? default(Tile) : Main.tile[x, y - 1]);
		int northType = ((north == default(Tile)) ? myType : ((!north.HasTile || north.BottomSlope) ? (-1) : north.TileType));
		ushort[] vines = VineToGrass.Keys.ToArray();
		for (int i = 0; i < vines.Length; i++)
		{
			ushort correspondingGrass = VineToGrass[vines[i]];
			if (myType != vines[i] && (northType == correspondingGrass || northType == vines[i]))
			{
				Main.tile[x, y].TileType = vines[i];
				WorldGen.SquareTileFrame(x, y);
				return;
			}
		}
		if (northType == myType)
		{
			return;
		}
		bool tileMustDie = northType == -1;
		if (northType != -1)
		{
			if (myType == 52 && northType != 2 && northType != 192)
			{
				tileMustDie = true;
			}
			else if (myType != 52)
			{
				for (int j = 0; j < vines.Length; j++)
				{
					if (myType == vines[j] && northType != VineToGrass[vines[j]])
					{
						tileMustDie = true;
						break;
					}
				}
			}
		}
		if (tileMustDie)
		{
			WorldGen.KillTile(x, y);
		}
	}

	internal static bool BetterGemsparkFraming(int x, int y, bool resetFrame)
	{
		if (x < 0 || x >= Main.maxTilesX)
		{
			return false;
		}
		if (y < 0 || y >= Main.maxTilesY)
		{
			return false;
		}
		Tile tile = Main.tile[x, y];
		if (tile.Slope > SlopeType.Solid && TileID.Sets.HasSlopeFrames[tile.TileType])
		{
			return true;
		}
		GetAdjacentTiles(x, y, out var up, out var down, out var left, out var right, out var upLeft, out var upRight, out var downLeft, out var downRight);
		int randomFrame;
		if (resetFrame)
		{
			randomFrame = WorldGen.genRand.Next(3);
			Main.tile[x, y].Get<TileWallWireStateData>().TileFrameNumber = randomFrame;
		}
		else
		{
			randomFrame = Main.tile[x, y].TileFrameNumber;
		}
		if ((((!up & down) && !left) & right) && !downRight)
		{
			tile.TileFrameX = 234;
			tile.TileFrameY = 0;
			return false;
		}
		if ((!up & down & left) && !right && !downLeft)
		{
			tile.TileFrameX = 270;
			tile.TileFrameY = 0;
			return false;
		}
		if (((up && !down && !left) & right) && !upRight)
		{
			tile.TileFrameX = 234;
			tile.TileFrameY = 36;
			return false;
		}
		if (((up && !down) & left) && !right && !upLeft)
		{
			tile.TileFrameX = 270;
			tile.TileFrameY = 36;
			return false;
		}
		if ((!up & down & left & right) && !downLeft && !downRight)
		{
			tile.TileFrameX = 252;
			tile.TileFrameY = 0;
			return false;
		}
		if (((up && !down) & left & right) && !upLeft && !upRight)
		{
			tile.TileFrameX = 252;
			tile.TileFrameY = 36;
			return false;
		}
		if ((((up & down) && !left) & right) && !downRight && !upRight)
		{
			tile.TileFrameX = 234;
			tile.TileFrameY = 18;
			return false;
		}
		if ((up & down & left) && !right && !downLeft && !upLeft)
		{
			tile.TileFrameX = 270;
			tile.TileFrameY = 18;
			return false;
		}
		if ((up & down & left & right) && !downLeft && !downRight && !upLeft && !upRight)
		{
			tile.TileFrameX = 252;
			tile.TileFrameY = 18;
			return false;
		}
		if (((up & down & left & right) && !downLeft) & downRight & upLeft & upRight)
		{
			tile.TileFrameX = 270;
			tile.TileFrameY = 54;
			return false;
		}
		if (((up & down & left & right & downLeft) && !downRight) & upLeft & upRight)
		{
			tile.TileFrameX = 252;
			tile.TileFrameY = 54;
			return false;
		}
		if (((up & down & left & right & downLeft & downRight) && !upLeft) & upRight)
		{
			tile.TileFrameX = 270;
			tile.TileFrameY = 72;
			return false;
		}
		if ((up & down & left & right & downLeft & downRight & upLeft) && !upRight)
		{
			tile.TileFrameX = 252;
			tile.TileFrameY = 72;
			return false;
		}
		if (((up & down & left & right) && !downLeft && !downRight) & upLeft & upRight)
		{
			tile.TileFrameX = (short)(108 + randomFrame * 18);
			tile.TileFrameY = 36;
			return false;
		}
		if ((up & down & left & right & downLeft & downRight) && !upLeft && !upRight)
		{
			tile.TileFrameX = (short)(108 + randomFrame * 18);
			tile.TileFrameY = 18;
			return false;
		}
		if (((((up & down & left & right) && !downLeft) & downRight) && !upLeft) & upRight)
		{
			tile.TileFrameX = 180;
			tile.TileFrameY = (short)(randomFrame * 18);
			return false;
		}
		if ((((up & down & left & right & downLeft) && !downRight) & upLeft) && !upRight)
		{
			tile.TileFrameX = 198;
			tile.TileFrameY = (short)(randomFrame * 18);
			return false;
		}
		if ((((up & down & left & right) && !downLeft) & downRight & upLeft) && !upRight)
		{
			tile.TileFrameX = 288;
			tile.TileFrameY = 72;
			return false;
		}
		if (((up & down & left & right & downLeft) && !downRight && !upLeft) & upRight)
		{
			tile.TileFrameX = 306;
			tile.TileFrameY = 72;
			return false;
		}
		if (((up & down & left & right) && !downLeft && !downRight && !upLeft) & upRight)
		{
			tile.TileFrameX = 216;
			tile.TileFrameY = 72;
			return false;
		}
		if ((((up & down & left & right) && !downLeft) & downRight) && !upLeft && !upRight)
		{
			tile.TileFrameX = 216;
			tile.TileFrameY = 54;
			return false;
		}
		if ((((up & down & left & right) && !downLeft && !downRight) & upLeft) && !upRight)
		{
			tile.TileFrameX = 234;
			tile.TileFrameY = 72;
			return false;
		}
		if ((up & down & left & right & downLeft) && !downRight && !upLeft && !upRight)
		{
			tile.TileFrameX = 234;
			tile.TileFrameY = 54;
			return false;
		}
		if ((((!up & down & left & right) && !downLeft) & downRight) && !upLeft && !upRight)
		{
			tile.TileFrameX = 306;
			tile.TileFrameY = 36;
			return false;
		}
		if ((!up & down & left & right & downLeft) && !downRight && !upLeft && !upRight)
		{
			tile.TileFrameX = 288;
			tile.TileFrameY = 36;
			return false;
		}
		if ((((up && !down) & left & right) && !downLeft && !downRight && !upLeft) & upRight)
		{
			tile.TileFrameX = 306;
			tile.TileFrameY = 54;
			return false;
		}
		if (((((up && !down) & left & right) && !downLeft && !downRight) & upLeft) && !upRight)
		{
			tile.TileFrameX = 288;
			tile.TileFrameY = 54;
			return false;
		}
		if (((((up & down) && !left) & right) && !downLeft && !downRight && !upLeft) & upRight)
		{
			tile.TileFrameX = 288;
			tile.TileFrameY = 0;
			return false;
		}
		if ((((((up & down) && !left) & right) && !downLeft) & downRight) && !upLeft && !upRight)
		{
			tile.TileFrameX = 288;
			tile.TileFrameY = 18;
			return false;
		}
		if ((((up & down & left) && !right && !downLeft && !downRight) & upLeft) && !upRight)
		{
			tile.TileFrameX = 306;
			tile.TileFrameY = 0;
			return false;
		}
		if ((((up & down & left) && !right) & downLeft) && !downRight && !upLeft && !upRight)
		{
			tile.TileFrameX = 306;
			tile.TileFrameY = 18;
			return false;
		}
		return true;
	}

	internal static bool BrimstoneFraming(int x, int y, bool resetFrame)
	{
		if (x < 0 || x >= Main.maxTilesX)
		{
			return false;
		}
		if (y < 0 || y >= Main.maxTilesY)
		{
			return false;
		}
		Tile tile = Main.tile[x, y];
		if (tile.Slope > SlopeType.Solid && TileID.Sets.HasSlopeFrames[tile.TileType])
		{
			return true;
		}
		GetAdjacentTiles(x, y, out var up, out var down, out var left, out var right, out var upLeft, out var upRight, out var downLeft, out var downRight);
		int randomFrame;
		if (resetFrame)
		{
			randomFrame = WorldGen.genRand.Next(3);
			Main.tile[x, y].Get<TileWallWireStateData>().TileFrameNumber = randomFrame;
		}
		else
		{
			randomFrame = Main.tile[x, y].TileFrameNumber;
		}
		int randomFrameX54 = randomFrame * 54;
		if ((((!up & down) && !left) & right) && !downRight)
		{
			tile.TileFrameX = (short)(288 + randomFrameX54);
			tile.TileFrameY = 0;
			return false;
		}
		if ((!up & down & left) && !right && !downLeft)
		{
			tile.TileFrameX = (short)(324 + randomFrameX54);
			tile.TileFrameY = 0;
			return false;
		}
		if (((up && !down && !left) & right) && !upRight)
		{
			tile.TileFrameX = (short)(288 + randomFrameX54);
			tile.TileFrameY = 36;
			return false;
		}
		if (((up && !down) & left) && !right && !upLeft)
		{
			tile.TileFrameX = (short)(324 + randomFrameX54);
			tile.TileFrameY = 36;
			return false;
		}
		if ((!up & down & left & right) && !downLeft && !downRight)
		{
			tile.TileFrameX = (short)(306 + randomFrameX54);
			tile.TileFrameY = 0;
			return false;
		}
		if (((up && !down) & left & right) && !upLeft && !upRight)
		{
			tile.TileFrameX = (short)(306 + randomFrameX54);
			tile.TileFrameY = 36;
			return false;
		}
		if ((((up & down) && !left) & right) && !downRight && !upRight)
		{
			tile.TileFrameX = (short)(288 + randomFrameX54);
			tile.TileFrameY = 18;
			return false;
		}
		if ((up & down & left) && !right && !downLeft && !upLeft)
		{
			tile.TileFrameX = (short)(324 + randomFrameX54);
			tile.TileFrameY = 18;
			return false;
		}
		if ((up & down & left & right) && !downLeft && !downRight && !upLeft && !upRight)
		{
			tile.TileFrameX = (short)(306 + randomFrameX54);
			tile.TileFrameY = 18;
			return false;
		}
		if (((up & down & left & right) && !downLeft) & downRight & upLeft & upRight)
		{
			tile.TileFrameX = 252;
			tile.TileFrameY = (short)(90 + randomFrame * 36);
			return false;
		}
		if (((up & down & left & right & downLeft) && !downRight) & upLeft & upRight)
		{
			tile.TileFrameX = 234;
			tile.TileFrameY = (short)(90 + randomFrame * 36);
			return false;
		}
		if (((up & down & left & right & downLeft & downRight) && !upLeft) & upRight)
		{
			tile.TileFrameX = 252;
			tile.TileFrameY = (short)(108 + randomFrame * 36);
			return false;
		}
		if ((up & down & left & right & downLeft & downRight & upLeft) && !upRight)
		{
			tile.TileFrameX = 234;
			tile.TileFrameY = (short)(108 + randomFrame * 36);
			return false;
		}
		if (((up & down & left & right) && !downLeft && !downRight) & upLeft & upRight)
		{
			tile.TileFrameX = (short)(108 + randomFrame * 18);
			tile.TileFrameY = 36;
			return false;
		}
		if ((up & down & left & right & downLeft & downRight) && !upLeft && !upRight)
		{
			tile.TileFrameX = (short)(108 + randomFrame * 18);
			tile.TileFrameY = 18;
			return false;
		}
		if (((((up & down & left & right) && !downLeft) & downRight) && !upLeft) & upRight)
		{
			tile.TileFrameX = 180;
			tile.TileFrameY = (short)(randomFrame * 18);
			return false;
		}
		if ((((up & down & left & right & downLeft) && !downRight) & upLeft) && !upRight)
		{
			tile.TileFrameX = 198;
			tile.TileFrameY = (short)(randomFrame * 18);
			return false;
		}
		if ((((up & down & left & right) && !downLeft) & downRight & upLeft) && !upRight)
		{
			tile.TileFrameX = (short)(180 + randomFrame * 18);
			tile.TileFrameY = 72;
			return false;
		}
		if (((up & down & left & right & downLeft) && !downRight && !upLeft) & upRight)
		{
			tile.TileFrameX = (short)(234 + randomFrame * 18);
			tile.TileFrameY = 72;
			return false;
		}
		if (((up & down & left & right) && !downLeft && !downRight && !upLeft) & upRight)
		{
			tile.TileFrameX = 270;
			tile.TileFrameY = (short)(108 + randomFrame * 36);
			return false;
		}
		if ((((up & down & left & right) && !downLeft) & downRight) && !upLeft && !upRight)
		{
			tile.TileFrameX = 270;
			tile.TileFrameY = (short)(90 + randomFrame * 36);
			return false;
		}
		if ((((up & down & left & right) && !downLeft && !downRight) & upLeft) && !upRight)
		{
			tile.TileFrameX = 288;
			tile.TileFrameY = (short)(108 + randomFrame * 36);
			return false;
		}
		if ((up & down & left & right & downLeft) && !downRight && !upLeft && !upRight)
		{
			tile.TileFrameX = 288;
			tile.TileFrameY = (short)(90 + randomFrame * 36);
			return false;
		}
		if ((((!up & down & left & right) && !downLeft) & downRight) && !upLeft && !upRight)
		{
			tile.TileFrameX = (short)(306 + randomFrame * 36);
			tile.TileFrameY = 54;
			return false;
		}
		if ((!up & down & left & right & downLeft) && !downRight && !upLeft && !upRight)
		{
			tile.TileFrameX = (short)(288 + randomFrame * 36);
			tile.TileFrameY = 54;
			return false;
		}
		if ((((up && !down) & left & right) && !downLeft && !downRight && !upLeft) & upRight)
		{
			tile.TileFrameX = (short)(306 + randomFrame * 36);
			tile.TileFrameY = 72;
			return false;
		}
		if (((((up && !down) & left & right) && !downLeft && !downRight) & upLeft) && !upRight)
		{
			tile.TileFrameX = (short)(288 + randomFrame * 36);
			tile.TileFrameY = 72;
			return false;
		}
		if (((((up & down) && !left) & right) && !downLeft && !downRight && !upLeft) & upRight)
		{
			tile.TileFrameX = 306;
			tile.TileFrameY = (short)(90 + randomFrame * 36);
			return false;
		}
		if ((((((up & down) && !left) & right) && !downLeft) & downRight) && !upLeft && !upRight)
		{
			tile.TileFrameX = 306;
			tile.TileFrameY = (short)(108 + randomFrame * 36);
			return false;
		}
		if ((((up & down & left) && !right && !downLeft && !downRight) & upLeft) && !upRight)
		{
			tile.TileFrameX = 324;
			tile.TileFrameY = (short)(90 + randomFrame * 36);
			return false;
		}
		if ((((up & down & left) && !right) & downLeft) && !downRight && !upLeft && !upRight)
		{
			tile.TileFrameX = 324;
			tile.TileFrameY = (short)(108 + randomFrame * 36);
			return false;
		}
		return true;
	}

	internal static void CompactFraming(int x, int y, bool resetFrame = true)
	{
		if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
		{
			return;
		}
		Tile tile = Main.tile[x, y];
		if (tile.Slope <= SlopeType.Solid || !TileID.Sets.HasSlopeFrames[tile.TileType])
		{
			if (resetFrame)
			{
				int randomFrame = WorldGen.genRand.Next(3);
				Main.tile[x, y].Get<TileWallWireStateData>().TileFrameNumber = (byte)randomFrame;
			}
			else
			{
				int randomFrame = Main.tile[x, y].TileFrameNumber;
			}
			GetAdjacentTiles(x, y, out var up, out var down, out var left, out var right, out var upLeft, out var upRight, out var downLeft, out var downRight);
			if (up & down & left & right & upLeft & upRight & downLeft & downRight)
			{
				tile.TileFrameX = 18;
				tile.TileFrameY = 18;
			}
			else if (!up && !down && !left && !right)
			{
				tile.TileFrameX = 54;
				tile.TileFrameY = 54;
			}
			else if (!up & down & left & right & downLeft & downRight)
			{
				tile.TileFrameX = 18;
				tile.TileFrameY = 0;
			}
			else if (((up & down) && !left) & right & upRight & downRight)
			{
				tile.TileFrameX = 0;
				tile.TileFrameY = 18;
			}
			else if ((up && !down) & left & right & upLeft & upRight)
			{
				tile.TileFrameX = 18;
				tile.TileFrameY = 36;
			}
			else if (((up & down & left) && !right) & upLeft & downLeft)
			{
				tile.TileFrameX = 36;
				tile.TileFrameY = 18;
			}
			else if (((!up & down) && !left) & right & downRight)
			{
				tile.TileFrameX = 0;
				tile.TileFrameY = 0;
			}
			else if (((!up & down & left) && !right) & downLeft)
			{
				tile.TileFrameX = 36;
				tile.TileFrameY = 0;
			}
			else if ((up && !down && !left) & right & upRight)
			{
				tile.TileFrameX = 0;
				tile.TileFrameY = 36;
			}
			else if ((((up && !down) & left) && !right) & upLeft)
			{
				tile.TileFrameX = 36;
				tile.TileFrameY = 36;
			}
			else if ((up & down) && !left && !right)
			{
				tile.TileFrameX = 54;
				tile.TileFrameY = 18;
			}
			else if ((!up && !down) & left & right)
			{
				tile.TileFrameX = 18;
				tile.TileFrameY = 54;
			}
			else if ((!up & down) && !left && !right)
			{
				tile.TileFrameX = 54;
				tile.TileFrameY = 0;
			}
			else if (up && !down && !left && !right)
			{
				tile.TileFrameX = 54;
				tile.TileFrameY = 36;
			}
			else if ((!up && !down && !left) & right)
			{
				tile.TileFrameX = 0;
				tile.TileFrameY = 54;
			}
			else if (((!up && !down) & left) && !right)
			{
				tile.TileFrameX = 36;
				tile.TileFrameY = 54;
			}
			else if ((((!up & down) && !left) & right) && !downRight)
			{
				tile.TileFrameX = 72;
				tile.TileFrameY = 0;
			}
			else if ((!up & down & left) && !right && !downLeft)
			{
				tile.TileFrameX = 108;
				tile.TileFrameY = 0;
			}
			else if (((up && !down && !left) & right) && !upRight)
			{
				tile.TileFrameX = 72;
				tile.TileFrameY = 36;
			}
			else if (((up && !down) & left) && !right && !upLeft)
			{
				tile.TileFrameX = 108;
				tile.TileFrameY = 36;
			}
			else if ((!up & down & left & right) && !downLeft && !downRight)
			{
				tile.TileFrameX = 90;
				tile.TileFrameY = 0;
			}
			else if (((up && !down) & left & right) && !upLeft && !upRight)
			{
				tile.TileFrameX = 90;
				tile.TileFrameY = 36;
			}
			else if ((((up & down) && !left) & right) && !downRight && !upRight)
			{
				tile.TileFrameX = 72;
				tile.TileFrameY = 18;
			}
			else if ((up & down & left) && !right && !downLeft && !upLeft)
			{
				tile.TileFrameX = 108;
				tile.TileFrameY = 18;
			}
			else if ((up & down & left & right) && !downLeft && !downRight && !upLeft && !upRight)
			{
				tile.TileFrameX = 90;
				tile.TileFrameY = 18;
			}
			else if (((up & down & left & right) && !downLeft) & downRight & upLeft & upRight)
			{
				tile.TileFrameX = 144;
				tile.TileFrameY = 36;
			}
			else if (((up & down & left & right & downLeft) && !downRight) & upLeft & upRight)
			{
				tile.TileFrameX = 126;
				tile.TileFrameY = 36;
			}
			else if (((up & down & left & right & downLeft & downRight) && !upLeft) & upRight)
			{
				tile.TileFrameX = 144;
				tile.TileFrameY = 54;
			}
			else if ((up & down & left & right & downLeft & downRight & upLeft) && !upRight)
			{
				tile.TileFrameX = 126;
				tile.TileFrameY = 54;
			}
			else if (((up & down & left & right) && !downLeft && !downRight) & upLeft & upRight)
			{
				tile.TileFrameX = 198;
				tile.TileFrameY = 0;
			}
			else if ((up & down & left & right & downLeft & downRight) && !upLeft && !upRight)
			{
				tile.TileFrameX = 198;
				tile.TileFrameY = 18;
			}
			else if (((((up & down & left & right) && !downLeft) & downRight) && !upLeft) & upRight)
			{
				tile.TileFrameX = 198;
				tile.TileFrameY = 36;
			}
			else if ((((up & down & left & right & downLeft) && !downRight) & upLeft) && !upRight)
			{
				tile.TileFrameX = 198;
				tile.TileFrameY = 54;
			}
			else if ((((up & down & left & right) && !downLeft) & downRight & upLeft) && !upRight)
			{
				tile.TileFrameX = 108;
				tile.TileFrameY = 54;
			}
			else if (((up & down & left & right & downLeft) && !downRight && !upLeft) & upRight)
			{
				tile.TileFrameX = 90;
				tile.TileFrameY = 54;
			}
			else if (((up & down & left & right) && !downLeft && !downRight && !upLeft) & upRight)
			{
				tile.TileFrameX = 126;
				tile.TileFrameY = 18;
			}
			else if ((((up & down & left & right) && !downLeft) & downRight) && !upLeft && !upRight)
			{
				tile.TileFrameX = 126;
				tile.TileFrameY = 0;
			}
			else if ((((up & down & left & right) && !downLeft && !downRight) & upLeft) && !upRight)
			{
				tile.TileFrameX = 144;
				tile.TileFrameY = 18;
			}
			else if ((up & down & left & right & downLeft) && !downRight && !upLeft && !upRight)
			{
				tile.TileFrameX = 144;
				tile.TileFrameY = 0;
			}
			else if ((((!up & down & left & right) && !downLeft) & downRight) && !upLeft && !upRight)
			{
				tile.TileFrameX = 180;
				tile.TileFrameY = 0;
			}
			else if ((!up & down & left & right & downLeft) && !downRight && !upLeft && !upRight)
			{
				tile.TileFrameX = 162;
				tile.TileFrameY = 0;
			}
			else if ((((up && !down) & left & right) && !downLeft && !downRight && !upLeft) & upRight)
			{
				tile.TileFrameX = 180;
				tile.TileFrameY = 18;
			}
			else if (((((up && !down) & left & right) && !downLeft && !downRight) & upLeft) && !upRight)
			{
				tile.TileFrameX = 162;
				tile.TileFrameY = 18;
			}
			else if (((((up & down) && !left) & right) && !downLeft && !downRight && !upLeft) & upRight)
			{
				tile.TileFrameX = 162;
				tile.TileFrameY = 36;
			}
			else if ((((((up & down) && !left) & right) && !downLeft) & downRight) && !upLeft && !upRight)
			{
				tile.TileFrameX = 162;
				tile.TileFrameY = 54;
			}
			else if ((((up & down & left) && !right && !downLeft && !downRight) & upLeft) && !upRight)
			{
				tile.TileFrameX = 180;
				tile.TileFrameY = 36;
			}
			else if ((((up & down & left) && !right) & downLeft) && !downRight && !upLeft && !upRight)
			{
				tile.TileFrameX = 180;
				tile.TileFrameY = 54;
			}
		}
	}

	internal static void SlopedGlowmask(ref readonly Tile tile, int i, int j, Texture2D texture, Rectangle? sourceRectangle, Color drawColor, Vector2 positionOffset)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		int frameX = tile.TileFrameX;
		int frameY = tile.TileFrameY;
		int width = 16;
		int height = 16;
		if (sourceRectangle.HasValue)
		{
			frameX = sourceRectangle.Value.X;
			frameY = sourceRectangle.Value.Y;
		}
		int iX16 = i * 16;
		int jX16 = j * 16;
		Vector2 val = new Vector2((float)iX16, (float)jX16);
		Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange));
		Vector2 offsets = -Main.screenPosition + zero + positionOffset;
		Vector2 drawCoordinates = val + offsets;
		if ((tile.Slope == SlopeType.Solid && !tile.IsHalfBlock) || (Main.tileSolid[tile.TileType] && Main.tileSolidTop[tile.TileType]))
		{
			Main.spriteBatch.Draw(texture, drawCoordinates, (Rectangle?)new Rectangle(frameX, frameY, width, height), drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			return;
		}
		if (tile.IsHalfBlock)
		{
			Main.spriteBatch.Draw(texture, new Vector2(drawCoordinates.X, drawCoordinates.Y + 8f), (Rectangle?)new Rectangle(frameX, frameY, width, 8), drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			return;
		}
		byte b = (byte)tile.Slope;
		Rectangle TileFrame = default(Rectangle);
		Vector2 drawPos;
		if (b == 1 || b == 2)
		{
			for (int a = 0; a < 8; a++)
			{
				int aX2 = a * 2;
				int length;
				int height2;
				if (b == 2)
				{
					length = 16 - aX2 - 2;
					height2 = 14 - aX2;
				}
				else
				{
					length = aX2;
					height2 = 14 - length;
				}
				((Rectangle)(ref TileFrame))._002Ector(frameX + length, frameY, 2, height2);
				drawPos = new Vector2((float)(iX16 + length), (float)(jX16 + aX2)) + offsets;
				Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)TileFrame, drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
			((Rectangle)(ref TileFrame))._002Ector(frameX, frameY + 14, 16, 2);
			drawPos = new Vector2((float)iX16, (float)(jX16 + 14)) + offsets;
			Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)TileFrame, drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			return;
		}
		for (int k = 0; k < 8; k++)
		{
			int aX3 = k * 2;
			int length2;
			int height3;
			if (b == 3)
			{
				length2 = aX3;
				height3 = 16 - length2;
			}
			else
			{
				length2 = 16 - aX3 - 2;
				height3 = 16 - aX3;
			}
			((Rectangle)(ref TileFrame))._002Ector(frameX + length2, frameY + 16 - height3, 2, height3);
			drawPos = new Vector2((float)(iX16 + length2), (float)jX16) + offsets;
			Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)TileFrame, drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		drawPos = new Vector2((float)iX16, (float)jX16) + offsets;
		if (tile.TileType != EutrophicGlass.TypeCache)
		{
			((Rectangle)(ref TileFrame))._002Ector(frameX, frameY, 16, 2);
			Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)TileFrame, drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
	}

	internal static void CustomMergeFrameExplicit(int x, int y, int myType, int mergeType, out bool mergedUp, out bool mergedLeft, out bool mergedRight, out bool mergedDown, bool forceSameDown = false, bool forceSameUp = false, bool forceSameLeft = false, bool forceSameRight = false, bool resetFrame = true, bool myTypeBrimFrame = false)
	{
		if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
		{
			mergedUp = (mergedLeft = (mergedRight = (mergedDown = false)));
			return;
		}
		Main.tileMerge[myType][mergeType] = false;
		Tile tileLeft = Main.tile[x - 1, y];
		Tile tileRight = Main.tile[x + 1, y];
		Tile tileUp = Main.tile[x, y - 1];
		Tile tileDown = Main.tile[x, y + 1];
		Tile tileTopLeft = Main.tile[x - 1, y - 1];
		Tile tileTopRight = Main.tile[x + 1, y - 1];
		Tile tileBottomLeft = Main.tile[x - 1, y + 1];
		Tile check = Main.tile[x + 1, y + 1];
		Similarity leftSim = ((!forceSameLeft) ? GetSimilarity(tileLeft, myType, mergeType) : Similarity.Same);
		Similarity rightSim = ((!forceSameRight) ? GetSimilarity(tileRight, myType, mergeType) : Similarity.Same);
		Similarity upSim = ((!forceSameUp) ? GetSimilarity(tileUp, myType, mergeType) : Similarity.Same);
		Similarity downSim = ((!forceSameDown) ? GetSimilarity(tileDown, myType, mergeType) : Similarity.Same);
		Similarity topLeftSim = GetSimilarity(tileTopLeft, myType, mergeType);
		Similarity topRightSim = GetSimilarity(tileTopRight, myType, mergeType);
		Similarity bottomLeftSim = GetSimilarity(tileBottomLeft, myType, mergeType);
		Similarity bottomRightSim = GetSimilarity(check, myType, mergeType);
		int randomFrame;
		if (resetFrame)
		{
			randomFrame = WorldGen.genRand.Next(3);
			Main.tile[x, y].Get<TileWallWireStateData>().TileFrameNumber = (byte)randomFrame;
		}
		else
		{
			randomFrame = Main.tile[x, y].TileFrameNumber;
		}
		mergedDown = (mergedLeft = (mergedRight = (mergedUp = false)));
		switch (leftSim)
		{
		case Similarity.None:
			switch (upSim)
			{
			case Similarity.Same:
				switch (downSim)
				{
				case Similarity.Same:
					switch (rightSim)
					{
					case Similarity.Same:
						SetFrameAt(x, y, 0, 18 * randomFrame);
						break;
					case Similarity.MergeLink:
						mergedRight = true;
						SetFrameAt(x, y, 234 + 18 * randomFrame, 36);
						break;
					default:
						SetFrameAt(x, y, 90, 18 * randomFrame);
						break;
					}
					break;
				case Similarity.MergeLink:
					switch (rightSim)
					{
					case Similarity.Same:
						mergedDown = true;
						SetFrameAt(x, y, 72, 90 + 18 * randomFrame);
						break;
					case Similarity.MergeLink:
						SetFrameAt(x, y, 108 + 18 * randomFrame, 54);
						break;
					default:
						mergedDown = true;
						SetFrameAt(x, y, 126, 90 + 18 * randomFrame);
						break;
					}
					break;
				default:
					if (rightSim == Similarity.Same)
					{
						SetFrameAt(x, y, 36 * randomFrame, 72);
					}
					else
					{
						SetFrameAt(x, y, 108 + 18 * randomFrame, 54);
					}
					break;
				}
				break;
			case Similarity.MergeLink:
				switch (downSim)
				{
				case Similarity.Same:
					switch (rightSim)
					{
					case Similarity.Same:
						mergedUp = true;
						SetFrameAt(x, y, 72, 144 + 18 * randomFrame);
						break;
					case Similarity.MergeLink:
						SetFrameAt(x, y, 108 + 18 * randomFrame, 0);
						break;
					default:
						mergedUp = true;
						SetFrameAt(x, y, 126, 144 + 18 * randomFrame);
						break;
					}
					break;
				case Similarity.MergeLink:
					switch (rightSim)
					{
					case Similarity.Same:
						SetFrameAt(x, y, 162, 18 * randomFrame);
						break;
					case Similarity.MergeLink:
						SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
						break;
					default:
						mergedUp = true;
						mergedDown = true;
						SetFrameAt(x, y, 108, 216 + 18 * randomFrame);
						break;
					}
					break;
				default:
					switch (rightSim)
					{
					case Similarity.Same:
						SetFrameAt(x, y, 162, 18 * randomFrame);
						break;
					case Similarity.MergeLink:
						SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
						break;
					default:
						mergedUp = true;
						SetFrameAt(x, y, 108, 144 + 18 * randomFrame);
						break;
					}
					break;
				}
				break;
			default:
				switch (downSim)
				{
				case Similarity.Same:
					if (rightSim == Similarity.Same)
					{
						SetFrameAt(x, y, 36 * randomFrame, 54);
						break;
					}
					_ = 1;
					SetFrameAt(x, y, 108 + 18 * randomFrame, 0);
					break;
				case Similarity.MergeLink:
					switch (rightSim)
					{
					case Similarity.Same:
						SetFrameAt(x, y, 162, 18 * randomFrame);
						break;
					case Similarity.MergeLink:
						SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
						break;
					default:
						mergedDown = true;
						SetFrameAt(x, y, 108, 90 + 18 * randomFrame);
						break;
					}
					break;
				default:
					switch (rightSim)
					{
					case Similarity.Same:
						SetFrameAt(x, y, 162, 18 * randomFrame);
						break;
					case Similarity.MergeLink:
						mergedRight = true;
						SetFrameAt(x, y, 54 + 18 * randomFrame, 234);
						break;
					default:
						SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
						break;
					}
					break;
				}
				break;
			}
			return;
		case Similarity.MergeLink:
			switch (upSim)
			{
			case Similarity.Same:
				switch (downSim)
				{
				case Similarity.Same:
					switch (rightSim)
					{
					case Similarity.Same:
						mergedLeft = true;
						SetFrameAt(x, y, 162, 126 + 18 * randomFrame);
						break;
					case Similarity.MergeLink:
						mergedLeft = true;
						mergedRight = true;
						SetFrameAt(x, y, 180, 126 + 18 * randomFrame);
						break;
					default:
						mergedLeft = true;
						SetFrameAt(x, y, 234 + 18 * randomFrame, 54);
						break;
					}
					break;
				case Similarity.MergeLink:
					switch (rightSim)
					{
					case Similarity.Same:
						mergedLeft = (mergedDown = true);
						SetFrameAt(x, y, 36, 108 + 36 * randomFrame);
						break;
					case Similarity.MergeLink:
						mergedLeft = (mergedRight = (mergedDown = true));
						SetFrameAt(x, y, 198, 144 + 18 * randomFrame);
						break;
					default:
						SetFrameAt(x, y, 108 + 18 * randomFrame, 54);
						break;
					}
					break;
				default:
					if (rightSim == Similarity.Same)
					{
						mergedLeft = true;
						SetFrameAt(x, y, 18 * randomFrame, 216);
					}
					else
					{
						SetFrameAt(x, y, 108 + 18 * randomFrame, 54);
					}
					break;
				}
				break;
			case Similarity.MergeLink:
				switch (downSim)
				{
				case Similarity.Same:
					switch (rightSim)
					{
					case Similarity.Same:
						mergedUp = (mergedLeft = true);
						SetFrameAt(x, y, 36, 90 + 36 * randomFrame);
						break;
					case Similarity.MergeLink:
						mergedLeft = (mergedRight = (mergedUp = true));
						SetFrameAt(x, y, 198, 90 + 18 * randomFrame);
						break;
					default:
						SetFrameAt(x, y, 108 + 18 * randomFrame, 0);
						break;
					}
					break;
				case Similarity.MergeLink:
					switch (rightSim)
					{
					case Similarity.Same:
						mergedUp = (mergedLeft = (mergedDown = true));
						SetFrameAt(x, y, 216, 90 + 18 * randomFrame);
						break;
					case Similarity.MergeLink:
						mergedDown = (mergedLeft = (mergedRight = (mergedUp = true)));
						SetFrameAt(x, y, 108 + 18 * randomFrame, 198);
						break;
					default:
						SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
						break;
					}
					break;
				default:
					if (rightSim == Similarity.Same)
					{
						SetFrameAt(x, y, 162, 18 * randomFrame);
					}
					else
					{
						SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
					}
					break;
				}
				break;
			default:
				switch (downSim)
				{
				case Similarity.Same:
					if (rightSim == Similarity.Same)
					{
						mergedLeft = true;
						SetFrameAt(x, y, 18 * randomFrame, 198);
					}
					else
					{
						_ = 1;
						SetFrameAt(x, y, 108 + 18 * randomFrame, 0);
					}
					break;
				case Similarity.MergeLink:
					if (rightSim == Similarity.Same)
					{
						SetFrameAt(x, y, 162, 18 * randomFrame);
						break;
					}
					_ = 1;
					SetFrameAt(x, y, 162 + 18 * randomFrame, 54);
					break;
				default:
					switch (rightSim)
					{
					case Similarity.Same:
						mergedLeft = true;
						SetFrameAt(x, y, 18 * randomFrame, 252);
						break;
					case Similarity.MergeLink:
						mergedRight = (mergedLeft = true);
						SetFrameAt(x, y, 162 + 18 * randomFrame, 198);
						break;
					default:
						mergedLeft = true;
						SetFrameAt(x, y, 18 * randomFrame, 234);
						break;
					}
					break;
				}
				break;
			}
			return;
		}
		switch (upSim)
		{
		case Similarity.Same:
			switch (downSim)
			{
			case Similarity.Same:
				switch (rightSim)
				{
				case Similarity.Same:
					if (topLeftSim == Similarity.MergeLink || topRightSim == Similarity.MergeLink || bottomLeftSim == Similarity.MergeLink || bottomRightSim == Similarity.MergeLink)
					{
						if (bottomRightSim == Similarity.MergeLink)
						{
							SetFrameAt(x, y, 0, 90 + 36 * randomFrame);
						}
						else if (bottomLeftSim == Similarity.MergeLink)
						{
							SetFrameAt(x, y, 18, 90 + 36 * randomFrame);
						}
						else if (topRightSim == Similarity.MergeLink)
						{
							SetFrameAt(x, y, 0, 108 + 36 * randomFrame);
						}
						else
						{
							SetFrameAt(x, y, 18, 108 + 36 * randomFrame);
						}
						break;
					}
					switch (topLeftSim)
					{
					case Similarity.Same:
						if (topRightSim == Similarity.Same)
						{
							if (bottomLeftSim == Similarity.Same)
							{
								SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
							}
							else if (bottomRightSim == Similarity.Same)
							{
								SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
							}
							else
							{
								SetFrameAt(x, y, 108 + 18 * randomFrame, 36);
							}
							return;
						}
						if (bottomLeftSim != Similarity.Same)
						{
							break;
						}
						if (bottomRightSim == Similarity.Same)
						{
							if (topRightSim == Similarity.MergeLink)
							{
								SetFrameAt(x, y, 0, 108 + 36 * randomFrame);
							}
							else
							{
								SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
							}
						}
						else
						{
							SetFrameAt(x, y, 198, 18 * randomFrame);
						}
						return;
					case Similarity.None:
						if (topRightSim == Similarity.Same)
						{
							if (bottomRightSim == Similarity.Same)
							{
								SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
							}
							else
							{
								SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
							}
						}
						else
						{
							SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
						}
						return;
					}
					SetFrameAt(x, y, 18 + 18 * randomFrame, 18);
					break;
				case Similarity.MergeLink:
					mergedRight = true;
					SetFrameAt(x, y, 144, 126 + 18 * randomFrame);
					break;
				default:
					SetFrameAt(x, y, 72, 18 * randomFrame);
					break;
				}
				break;
			case Similarity.MergeLink:
				switch (rightSim)
				{
				case Similarity.Same:
					mergedDown = true;
					SetFrameAt(x, y, 144 + 18 * randomFrame, 90);
					break;
				case Similarity.MergeLink:
					mergedDown = (mergedRight = true);
					SetFrameAt(x, y, 54, 108 + 36 * randomFrame);
					break;
				default:
					mergedDown = true;
					SetFrameAt(x, y, 90, 90 + 18 * randomFrame);
					break;
				}
				break;
			default:
				switch (rightSim)
				{
				case Similarity.Same:
					SetFrameAt(x, y, 18 + 18 * randomFrame, 36);
					break;
				case Similarity.MergeLink:
					mergedRight = true;
					SetFrameAt(x, y, 54 + 18 * randomFrame, 216);
					break;
				default:
					SetFrameAt(x, y, 18 + 36 * randomFrame, 72);
					break;
				}
				break;
			}
			return;
		case Similarity.MergeLink:
			switch (downSim)
			{
			case Similarity.Same:
				switch (rightSim)
				{
				case Similarity.Same:
					mergedUp = true;
					SetFrameAt(x, y, 144 + 18 * randomFrame, 108);
					break;
				case Similarity.MergeLink:
					mergedRight = (mergedUp = true);
					SetFrameAt(x, y, 54, 90 + 36 * randomFrame);
					break;
				default:
					mergedUp = true;
					SetFrameAt(x, y, 90, 144 + 18 * randomFrame);
					break;
				}
				break;
			case Similarity.MergeLink:
				switch (rightSim)
				{
				case Similarity.Same:
					mergedUp = (mergedDown = true);
					SetFrameAt(x, y, 144 + 18 * randomFrame, 180);
					break;
				case Similarity.MergeLink:
					mergedUp = (mergedRight = (mergedDown = true));
					SetFrameAt(x, y, 216, 144 + 18 * randomFrame);
					break;
				default:
					SetFrameAt(x, y, 216, 18 * randomFrame);
					break;
				}
				break;
			default:
				if (rightSim == Similarity.Same)
				{
					mergedUp = true;
					SetFrameAt(x, y, 234 + 18 * randomFrame, 18);
				}
				else
				{
					SetFrameAt(x, y, 216, 18 * randomFrame);
				}
				break;
			}
			return;
		}
		switch (downSim)
		{
		case Similarity.Same:
			switch (rightSim)
			{
			case Similarity.Same:
				SetFrameAt(x, y, 18 + 18 * randomFrame, 0);
				break;
			case Similarity.MergeLink:
				mergedRight = true;
				SetFrameAt(x, y, 54 + 18 * randomFrame, 198);
				break;
			default:
				SetFrameAt(x, y, 18 + 36 * randomFrame, 54);
				break;
			}
			break;
		case Similarity.MergeLink:
			if (rightSim == Similarity.Same)
			{
				mergedDown = true;
				SetFrameAt(x, y, 234 + 18 * randomFrame, 0);
			}
			else
			{
				SetFrameAt(x, y, 216, 18 * randomFrame);
			}
			break;
		default:
			switch (rightSim)
			{
			case Similarity.Same:
				SetFrameAt(x, y, 108 + 18 * randomFrame, 72);
				break;
			case Similarity.MergeLink:
				mergedRight = true;
				SetFrameAt(x, y, 54 + 18 * randomFrame, 252);
				break;
			default:
				SetFrameAt(x, y, 216, 18 * randomFrame);
				break;
			}
			break;
		}
	}

	internal static void CustomMergeFrame(int x, int y, int myType, int mergeType, bool forceSameDown = false, bool forceSameUp = false, bool forceSameLeft = false, bool forceSameRight = false, bool resetFrame = true)
	{
		CustomMergeFrameExplicit(x, y, myType, mergeType, out var _, out var _, out var _, out var _, forceSameDown, forceSameUp, forceSameLeft, forceSameRight, resetFrame);
	}

	internal static void CustomMergeFrame(int x, int y, int myType, int mergeType)
	{
		if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
		{
			bool forceSameUp = false;
			bool forceSameDown = false;
			bool forceSameLeft = false;
			bool forceSameRight = false;
			Tile north = Main.tile[x, y - 1];
			Tile south = Main.tile[x, y + 1];
			Tile west = Main.tile[x - 1, y];
			Tile east = Main.tile[x + 1, y];
			bool mergedUp;
			bool mergedLeft;
			bool mergedRight;
			if (north != null && north.HasTile && tileMergeTypes[myType][north.TileType])
			{
				CalamityUtils.SetMerge(myType, north.TileType, merge: false);
				TileID.Sets.ChecksForMerge[myType] = true;
				CustomMergeFrameExplicit(x, y - 1, north.TileType, myType, out mergedUp, out mergedLeft, out mergedRight, out forceSameUp, forceSameDown: false, forceSameUp: false, forceSameLeft: false, forceSameRight: false, resetFrame: false);
			}
			if (west != null && west.HasTile && tileMergeTypes[myType][west.TileType])
			{
				CalamityUtils.SetMerge(myType, west.TileType, merge: false);
				TileID.Sets.ChecksForMerge[myType] = true;
				CustomMergeFrameExplicit(x - 1, y, west.TileType, myType, out mergedRight, out mergedLeft, out forceSameLeft, out mergedUp, forceSameDown: false, forceSameUp: false, forceSameLeft: false, forceSameRight: false, resetFrame: false);
			}
			if (east != null && east.HasTile && tileMergeTypes[myType][east.TileType])
			{
				CalamityUtils.SetMerge(myType, east.TileType, merge: false);
				TileID.Sets.ChecksForMerge[myType] = true;
				CustomMergeFrameExplicit(x + 1, y, east.TileType, myType, out mergedUp, out forceSameRight, out mergedLeft, out mergedRight, forceSameDown: false, forceSameUp: false, forceSameLeft: false, forceSameRight: false, resetFrame: false);
			}
			if (south != null && south.HasTile && tileMergeTypes[myType][south.TileType])
			{
				CalamityUtils.SetMerge(myType, south.TileType, merge: false);
				TileID.Sets.ChecksForMerge[myType] = true;
				CustomMergeFrameExplicit(x, y + 1, south.TileType, myType, out forceSameDown, out mergedRight, out mergedLeft, out mergedUp, forceSameDown: false, forceSameUp: false, forceSameLeft: false, forceSameRight: false, resetFrame: false);
			}
			CustomMergeFrameExplicit(x, y, myType, mergeType, out mergedUp, out mergedLeft, out mergedRight, out var _, forceSameDown, forceSameUp, forceSameLeft, forceSameRight);
		}
	}

	static TileFramingSystem()
	{
		ushort[] obj = new ushort[19]
		{
			3, 24, 61, 71, 73, 74, 110, 113, 201, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0
		};
		obj[9] = (ushort)ModContent.TileType<AstralShortPlants>();
		obj[10] = (ushort)ModContent.TileType<AstralTallPlants>();
		obj[11] = (ushort)ModContent.TileType<LavaPistil>();
		obj[12] = (ushort)ModContent.TileType<CinderBlossomTallPlants>();
		obj[13] = (ushort)ModContent.TileType<SulphurTentacleCorals>();
		obj[14] = (ushort)ModContent.TileType<AbyssKelp>();
		obj[15] = (ushort)ModContent.TileType<TenebrisRemnant>();
		obj[16] = (ushort)ModContent.TileType<PhoviamareHalm>();
		obj[17] = (ushort)ModContent.TileType<LongScarletSeagrass>();
		obj[18] = (ushort)ModContent.TileType<SunkenKelp>();
		PlantTypes = obj;
	}
}
