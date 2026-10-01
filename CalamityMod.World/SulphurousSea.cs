using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Items.Tools.SpawnBlocker;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Schematics;
using CalamityMod.Tiles.Abyss;
using CalamityMod.Tiles.Abyss.AbyssAmbient;
using CalamityMod.Tiles.Abyss.Stalactite;
using CalamityMod.Walls;
using CalamityMod.Walls.UnsafeWalls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.RGB;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class SulphurousSea
{
	public const float SandBlockEdgeDescentSmoothness = 0.24f;

	public const float DitherStartFactor = 0.9f;

	public const int DepthForWater = 12;

	public const float TopWaterDepthPercentage = 0.125f;

	public const float TopWaterDescentSmoothnessMin = 0.26f;

	public const float TopWaterDescentSmoothnessMax = 0.39f;

	public const int TotalSandTilesBeforeWaterMin = 32;

	public const int TotalSandTilesBeforeWaterMax = 45;

	public const float OpenSeaWidthPercentage = 0.795f;

	public const float IslandWidthPercentage = 0.36f;

	public const float IslandCurvatureSharpness = 0.74f;

	public const float SmallCavesJaggedness = 0.51f;

	public const float SmallCavesBiasTowardsTightness = 2.21f;

	public const float SpaghettiCaveMagnification = 0.00193f;

	public static readonly float[] SpaghettiCaveCarveOutThresholds = new float[2] { 0.033f, 0.089f };

	public const float CheeseCaveMagnification = 0.00237f;

	public static readonly float[] CheeseCaveCarveOutThresholds = new float[1] { 0.32f };

	public const float OpenCavernStartDepthPercentage = 0.42f;

	public const float WaterSpreadPercentage = 0.91f;

	public const float HardenedSandstoneLineMagnification = 0.004f;

	public const int MaxIslandHeight = 16;

	public const int MaxIslandDepth = 9;

	public const float IslandLineMagnification = 0.0079f;

	public const int TreeGrowChance = 5;

	public const int MinColumnHeight = 5;

	public const int MaxColumnHeight = 50;

	public const int BeachMaxDepth = 50;

	public const int ScrapPileAnticlumpDistance = 80;

	public const float SandstoneEdgeNoiseMagnification = 0.00115f;

	public const int StalactitePairMinDistance = 6;

	public const int StalactitePairMaxDistance = 44;

	public static readonly List<int> SulphSeaTiles = new List<int>
	{
		ModContent.TileType<SulphurousSand>(),
		ModContent.TileType<SulphurousSandstone>(),
		ModContent.TileType<HardenedSulphurousSandstone>()
	};

	public static readonly List<int> YStartWhitelist = new List<int>
	{
		1, 0, 53, 112, 234, 2, 23, 199, 40, 59,
		7, 166, 6, 167, 9, 168, 203, 25, 397, 398,
		399, 81, 324, 3, 73, 185, 186, 187, 5, 52,
		32, 352, 205, 21, 227, 60, 529
	};

	public static readonly List<int> OtherTilesForSulphSeaToDestroy = new List<int>
	{
		323, 27, 32, 352, 23, 24, 165, 82, 83, 28,
		254, 488, 518, 596, 616, 495
	};

	public static readonly List<int> WallsForSulphSeaToDestroy = new List<int>
	{
		16, 2, 196, 197, 198, 199, 59, 66, 63, 68,
		65, 69, 3, 83
	};

	public static readonly List<int> ValidBeachCovertTiles = new List<int>
	{
		0, 1, 203, 25, 53, 112, 234, 2, 23, 199,
		40, 59
	};

	public static readonly List<int> ValidBeachDestroyTiles = new List<int>
	{
		81, 324, 3, 73, 185, 186, 187, 32, 352, 227,
		5, 27, 518, 529, 82, 83, 84, 596, 616
	};

	public static int BiomeWidth => Main.maxTilesX switch
	{
		4200 => 370, 
		6400 => 445, 
		_ => (int)((float)Main.maxTilesX / 16.8f), 
	};

	public static int BlockDepth
	{
		get
		{
			if (Main.remixWorld)
			{
				return (int)((float)Main.UnderworldLayer * 0.2f);
			}
			int maxTilesX = Main.maxTilesX;
			return (int)((Main.rockLayer + 112.0 - (double)YStart) * (double)(maxTilesX switch
			{
				4200 => 0.8f, 
				6400 => 0.85f, 
				_ => 0.925f, 
			}));
		}
	}

	public static int TotalCavesInShallowWater => (int)Math.Ceiling((float)Main.maxTilesX / 2000f);

	public static int MaxTopWaterDepth => (int)((float)BlockDepth * 0.125f);

	public static int MinCaveWidth => Main.maxTilesX / 2500;

	public static int MaxCaveWidth => (int)Math.Ceiling((float)Main.maxTilesX / 566f);

	public static int MinCaveMovementSteps => (int)Math.Ceiling((float)Main.maxTilesX / 70f);

	public static int MaxCaveMovementSteps => (int)Math.Ceiling((float)Main.maxTilesX / 40f);

	public static int ColumnCount => Main.maxTilesX / 96;

	public static int GeyserCount => Main.maxTilesX / 137;

	public static int YStart { get; set; }

	public static int VineGrowTopLimit
	{
		get
		{
			if (!Main.remixWorld)
			{
				return YStart + 100;
			}
			return (int)Main.rockLayer;
		}
	}

	public static void PlaceSulphurSea()
	{
		Abyss.AtLeftSideOfWorld = Main.dungeonX < Main.maxTilesX / 2;
		DetermineYStart();
		GenerateSandBlock();
		if (!Main.remixWorld)
		{
			RemoveStupidTilesAboveSea();
		}
		GenerateShallowTopWater();
		GenerateIsland();
		GenerateSmallWaterCaverns();
		GenerateSpaghettiWaterCaves();
		GenerateCheeseWaterCaves();
		DecideHardSandstoneLine();
		MakeSurfaceLessRigid();
		if (!Main.remixWorld)
		{
			LayTreesOnSurface();
		}
	}

	public static void SulphurSeaGenerationAfterAbyss()
	{
		CreateBeach();
		ClearOutStrayTiles();
		ClearAloneTiles();
		List<Vector2> scrapPilePositions = PlaceScrapPiles();
		GenerateColumnsInCaverns();
		GenerateHardenedSandstone();
		PlaceAmbience();
		GenerateChests(scrapPilePositions);
	}

	public static void DetermineYStart()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		int xCheckPosition = GetActualX(BiomeWidth + 1);
		GenSearch searchCondition = Searches.Chain(new Searches.Down(3000), new Conditions.IsSolid());
		Point determinedPoint;
		do
		{
			WorldUtils.Find(new Point(xCheckPosition, (int)GenVars.worldSurfaceLow - 20), searchCondition, out determinedPoint);
			xCheckPosition += Abyss.AtLeftSideOfWorld.ToDirectionInt();
		}
		while (CalamityUtils.ParanoidTileRetrieval(determinedPoint.X, determinedPoint.Y).TileType == 25);
		YStart = (Main.remixWorld ? ((int)((float)Main.UnderworldLayer * 0.8f)) : determinedPoint.Y);
	}

	public static void GenerateSandBlock()
	{
		int width = BiomeWidth + 1;
		int maxDepth = BlockDepth;
		ushort blockTileType = (ushort)ModContent.TileType<SulphurousSand>();
		ushort wallID = (ushort)ModContent.WallType<UnsafeSulphurousSandWall>();
		for (int i = 1; i < width; i++)
		{
			int x = GetActualX(i);
			float depthFactor = (float)Math.Pow(Math.Sin((1f - (float)i / (float)width) * ((float)Math.PI / 2f)), 0.23999999463558197);
			int top = YStart;
			int bottom = top + (int)((float)maxDepth * depthFactor);
			for (int y = top; y < bottom; y++)
			{
				float ditherChance = CalculateDitherChance(width, top, bottom, i, y);
				if (WorldGen.genRand.NextFloat() >= ditherChance)
				{
					Main.tile[x, y].TileType = blockTileType;
					if (y >= top + 45)
					{
						Main.tile[x, y].WallType = wallID;
					}
				}
				if (ditherChance <= 0f)
				{
					Main.tile[x, y].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
					Main.tile[x, y].Get<TileWallWireStateData>().IsHalfBlock = false;
					Main.tile[x, y].Get<TileWallWireStateData>().HasTile = true;
				}
			}
			if (Main.remixWorld)
			{
				continue;
			}
			for (int j = top - 75; j < top + 50; j++)
			{
				if (Main.tile[x, j].TileType == 323)
				{
					WorldGen.KillTile(x, j);
				}
			}
		}
	}

	public static void RemoveStupidTilesAboveSea()
	{
		for (int i = 0; i < BiomeWidth; i++)
		{
			int x = GetActualX(i);
			for (int y = YStart - 140; y < YStart + 80; y++)
			{
				int type = CalamityUtils.ParanoidTileRetrieval(x, y).TileType;
				if (YStartWhitelist.Contains(type) || OtherTilesForSulphSeaToDestroy.Contains(type))
				{
					CalamityUtils.ParanoidTileRetrieval(x, y).Get<TileWallWireStateData>().HasTile = false;
				}
				if (WallsForSulphSeaToDestroy.Contains(CalamityUtils.ParanoidTileRetrieval(x, y).WallType))
				{
					CalamityUtils.ParanoidTileRetrieval(x, y).WallType = 0;
				}
			}
		}
	}

	public static void GenerateShallowTopWater()
	{
		int maxDepth = MaxTopWaterDepth;
		int totalSandTilesBeforeWater = WorldGen.genRand.Next(32, 45);
		int width = (int)((float)(BiomeWidth - totalSandTilesBeforeWater) * 0.795f);
		float descentSmoothness = WorldGen.genRand.NextFloat(0.26f, 0.39f);
		for (int i = 1; i < width; i++)
		{
			int x = GetActualX(i);
			float depthFactor = (float)Math.Pow(Math.Sin((1f - (float)i / (float)width) * ((float)Math.PI / 2f)), descentSmoothness);
			int top = YStart - 20;
			int bottom = top + (int)((float)maxDepth * depthFactor * 2f);
			for (int y = top; y < bottom; y++)
			{
				if (y >= top + 12)
				{
					Main.tile[x, y + WorldGen.genRand.Next(22, 25)].WallType = (ushort)ModContent.WallType<UnsafeSulphurousSandWall>();
				}
				Main.tile[x, y].LiquidAmount = byte.MaxValue;
				Main.tile[x, y].Get<TileWallWireStateData>().HasTile = false;
			}
			for (int j = top - 150; j < top + 12; j++)
			{
				Main.tile[x, j].LiquidAmount = 0;
			}
		}
	}

	public static void GenerateIsland()
	{
		int left = -32;
		int right = (int)((float)BiomeWidth * 0.36f);
		int maxDepth = MaxTopWaterDepth;
		ushort blockTileType = (ushort)ModContent.TileType<SulphurousSand>();
		ushort wallID = (ushort)ModContent.WallType<UnsafeSulphurousSandWall>();
		for (int i = left; i < right; i++)
		{
			if (i < 0)
			{
				continue;
			}
			int x = GetActualX(i);
			int islandHeight = (int)Math.Round(Math.Pow(CalamityUtils.Convert01To010(Utils.GetLerpValue(left, right, i, clamped: true)), 0.7400000095367432) * (double)((float)maxDepth + 4f));
			for (int dy = -30; dy <= islandHeight; dy++)
			{
				int y = YStart + maxDepth - dy;
				Main.tile[x, y].TileType = blockTileType;
				if (dy < islandHeight)
				{
					Main.tile[x, y].WallType = wallID;
				}
				Main.tile[x, y].LiquidAmount = byte.MaxValue;
				Main.tile[x, y].Get<TileWallWireStateData>().HasTile = true;
				if (i >= 2)
				{
					Tile.SmoothSlope(x, y);
				}
			}
		}
	}

	public static void GenerateSmallWaterCaverns()
	{
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		int width = BiomeWidth;
		int depth = BlockDepth;
		int shallowWaterCaveCount = TotalCavesInShallowWater;
		int minCaveWidth = MinCaveWidth;
		int maxCaveWidth = MaxCaveWidth;
		if (maxCaveWidth > 15)
		{
			maxCaveWidth = 15;
		}
		ushort wallID = (ushort)ModContent.WallType<UnsafeSulphurousSandWall>();
		Vector2 cavePosition = default(Vector2);
		for (int i = 2; i < shallowWaterCaveCount; i++)
		{
			int caveHorizontalOffset = WorldGen.genRand.Next((int)((float)width * 0.1f));
			int x = GetActualX((int)MathHelper.Lerp((float)width * 0.8f, (float)width * 0.24f, (float)i / ((float)shallowWaterCaveCount - 1f)) + caveHorizontalOffset);
			int caveWidth = (minCaveWidth + maxCaveWidth) / 3 + WorldGen.genRand.Next(8);
			int caveSteps = WorldGen.genRand.Next(MinCaveMovementSteps, MaxCaveMovementSteps);
			int caveSeed = WorldGen.genRand.Next();
			Vector2 baseCaveDirection = Vector2.UnitY.RotatedBy(WorldGen.genRand.NextFloatDirection() * 0.54f);
			((Vector2)(ref cavePosition))._002Ector((float)x, (float)(YStart + MaxTopWaterDepth + 6));
			for (int j = 0; j < caveSteps; j++)
			{
				float caveOffsetAngleAtStep = CalamityUtils.PerlinNoise2D((float)i / 50f, (float)j / 50f, 4, caveSeed) * (float)Math.PI * 1.9f;
				Vector2 caveDirection = baseCaveDirection.RotatedBy(caveOffsetAngleAtStep);
				if (cavePosition.X < (float)(Main.maxTilesX - 15) && cavePosition.X >= 15f)
				{
					WorldGen.digTunnel(cavePosition.X, cavePosition.Y, caveDirection.X, caveDirection.Y, 1, (int)((float)caveWidth * 1.18f), Wet: true);
					WorldUtils.Gen(cavePosition.ToPoint(), new Shapes.Circle(caveWidth), Actions.Chain(new Actions.ClearTile(frameNeighbors: true), new Actions.PlaceWall(wallID), new Actions.SetLiquid(0, (byte)((!(WorldGen.genRand.NextFloat() > 0.91f)) ? 255u : 0u)), new Actions.Smooth(applyToNeighbors: true)));
				}
				cavePosition += caveDirection * (float)caveWidth;
				if ((float)GetActualX((int)cavePosition.X) > (float)width * 0.8f || cavePosition.Y > (float)YStart + (float)depth * 0.7f)
				{
					cavePosition.X -= Abyss.AtLeftSideOfWorld.ToDirectionInt() * caveWidth;
					cavePosition.Y -= caveWidth;
				}
				float caveWidthFactorInterpolant = (float)Math.Pow(WorldGen.genRand.NextFloat(), 2.2100000381469727);
				caveWidth = (int)Math.Round((float)caveWidth * MathHelper.Lerp(0.49f, 1.51f, caveWidthFactorInterpolant));
				if (WorldGen.genRand.NextBool(12))
				{
					caveWidth = (int)((float)caveWidth * 1.4f);
				}
				caveWidth = Utils.Clamp(caveWidth, minCaveWidth, maxCaveWidth);
			}
		}
	}

	public static void GenerateSpaghettiWaterCaves()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		int width = BiomeWidth;
		int depth = (int)((float)BlockDepth * 0.96f);
		ushort wallID = (ushort)ModContent.WallType<UnsafeSulphurousSandWall>();
		for (int c = 0; c < SpaghettiCaveCarveOutThresholds.Length; c++)
		{
			int caveSeed = WorldGen.genRand.Next();
			for (int i = 2; i < width; i++)
			{
				int x = GetActualX(i);
				for (int y = YStart; y < YStart + depth; y++)
				{
					float num = FractalBrownianMotion((float)i * 0.00193f, (float)y * 0.00193f, caveSeed, 5);
					Vector2 val = new Vector2((float)i / (float)width, (float)(y - YStart) / (float)depth);
					float distanceFromEdge = ((Vector2)(ref val)).Length();
					float biasAwayFrom0Interpolant = Utils.GetLerpValue(0.82f, 0.96f, distanceFromEdge * 0.8f, clamped: true);
					biasAwayFrom0Interpolant += Utils.GetLerpValue((float)YStart + 12f, YStart, y, clamped: true) * 0.2f;
					biasAwayFrom0Interpolant += Utils.GetLerpValue((float)width - 19f, (float)width - 4f, i, clamped: true) * 0.6f;
					if (Math.Abs(MathHelper.Lerp(num, (float)Math.Sign(num), biasAwayFrom0Interpolant)) < SpaghettiCaveCarveOutThresholds[c])
					{
						WorldUtils.Gen(new Point(x, y), new Shapes.Rectangle(1, 1), Actions.Chain(new Actions.ClearTile(frameNeighbors: true), new Actions.PlaceWall(wallID), new Actions.SetLiquid(0, (byte)((!(WorldGen.genRand.NextFloat() > 0.91f)) ? 255u : 0u)), new Actions.Smooth(applyToNeighbors: true)));
					}
				}
			}
		}
	}

	public static void GenerateCheeseWaterCaves()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		int width = BiomeWidth;
		int depth = (int)((float)BlockDepth * 0.96f);
		ushort wallID = (ushort)ModContent.WallType<UnsafeSulphurousSandWall>();
		for (int c = 0; c < CheeseCaveCarveOutThresholds.Length; c++)
		{
			int caveSeed = WorldGen.genRand.Next();
			for (int i = 2; i < width; i++)
			{
				int x = GetActualX(i);
				for (int y = YStart; y < YStart + depth; y++)
				{
					float num = FractalBrownianMotion((float)i * 0.00237f, (float)y * 0.00237f, caveSeed, 6);
					Vector2 val = new Vector2((float)i / (float)width, (float)(y - YStart) / (float)depth);
					float distanceFromEdge = ((Vector2)(ref val)).Length();
					float biasToNegativeOneInterpolant = Utils.GetLerpValue(0.82f, 0.96f, distanceFromEdge * 0.8f, clamped: true);
					biasToNegativeOneInterpolant += Utils.GetLerpValue((float)YStart + 0.42f * (float)depth, (float)YStart + 0.42f * (float)depth - 25f, y, clamped: true);
					biasToNegativeOneInterpolant += Utils.GetLerpValue((float)width - 19f, (float)width - 4f, i, clamped: true);
					if (num - biasToNegativeOneInterpolant > CheeseCaveCarveOutThresholds[c])
					{
						WorldUtils.Gen(new Point(x, y), new Shapes.Rectangle(1, 1), Actions.Chain(new Actions.ClearTile(frameNeighbors: true), new Actions.PlaceWall(wallID), new Actions.SetLiquid(0, (byte)((!(WorldGen.genRand.NextFloat() > 0.91f)) ? 255u : 0u)), new Actions.Smooth(applyToNeighbors: true)));
					}
				}
			}
		}
	}

	public static void ClearOutStrayTiles()
	{
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		int width = BiomeWidth;
		int depth = BlockDepth;
		List<ushort> blockTileTypes = new List<ushort>
		{
			(ushort)ModContent.TileType<SulphurousSand>(),
			(ushort)ModContent.TileType<SulphurousSandstone>(),
			(ushort)ModContent.TileType<HardenedSulphurousSandstone>()
		};
		ushort wallID = (ushort)ModContent.WallType<SulphurousSandWall>();
		for (int i = 1; i < width; i++)
		{
			int x = GetActualX(i);
			for (int y = YStart; y < YStart + depth; y++)
			{
				List<Point> chunkPoints = new List<Point>();
				getAttachedPoints(x, y, chunkPoints);
				int cutoffLimit = (((float)y >= (float)YStart + (float)depth * 0.42f) ? 432 : 50);
				if (chunkPoints.Count < 2 || chunkPoints.Count >= cutoffLimit)
				{
					continue;
				}
				foreach (Point item in chunkPoints)
				{
					WorldUtils.Gen(item, new Shapes.Rectangle(1, 1), Actions.Chain(new Actions.ClearTile(frameNeighbors: true), new Actions.PlaceWall(wallID), new Actions.SetLiquid()));
				}
			}
		}
		void getAttachedPoints(int num, int num2, List<Point> points)
		{
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			Tile t = CalamityUtils.ParanoidTileRetrieval(num, num2);
			Point p = default(Point);
			((Point)(ref p))._002Ector(num, num2);
			if (blockTileTypes.Contains(t.TileType) && t.HasTile && points.Count <= 432 && !points.Contains(p) && !(CalculateDitherChance(width, YStart, YStart + depth, num, num2) > 0f))
			{
				points.Add(p);
				getAttachedPoints(num + 1, num2, points);
				getAttachedPoints(num - 1, num2, points);
				getAttachedPoints(num, num2 + 1, points);
				getAttachedPoints(num, num2 - 1, points);
			}
		}
	}

	public static void DecideHardSandstoneLine()
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		int width = BiomeWidth;
		int depth = BlockDepth;
		int sandstoneSeed = WorldGen.genRand.Next();
		ushort blockTypeToReplace = (ushort)ModContent.TileType<SulphurousSand>();
		ushort blockTypeToPlace = (ushort)ModContent.TileType<HardenedSulphurousSandstone>();
		ushort wallID = (ushort)ModContent.WallType<HardenedSulphurousSandstoneWall>();
		Point p = default(Point);
		for (int i = 0; i < width; i++)
		{
			for (int y = YStart; y < YStart + depth; y++)
			{
				int sandstoneLineOffset = (int)(FractalBrownianMotion((float)i * 0.004f, (float)y * 0.004f, sandstoneSeed, 7) * 30f) + (int)((float)depth * 0.42f);
				sandstoneLineOffset -= (int)(Math.Pow(Utils.GetLerpValue((float)width * 0.1f, (float)width * 0.8f, i, clamped: true), 1.7200000286102295) * 67.0);
				((Point)(ref p))._002Ector(GetActualX(i), y);
				Tile t = CalamityUtils.ParanoidTileRetrieval(p.X, p.Y);
				if (y >= YStart + sandstoneLineOffset && t.HasTile && t.TileType == blockTypeToReplace)
				{
					WorldUtils.Gen(p, new Shapes.Rectangle(1, 1), Actions.Chain(new Actions.SetTile(blockTypeToPlace, setSelfFrames: true), new Actions.PlaceWall(wallID), new Actions.SetLiquid()));
				}
			}
		}
	}

	public static void MakeSurfaceLessRigid()
	{
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		int y = YStart;
		int width = BiomeWidth;
		int heightSeed = WorldGen.genRand.Next();
		ushort blockTileType = (ushort)ModContent.TileType<SulphurousSand>();
		ushort wallID = (ushort)ModContent.WallType<HardenedSulphurousSandstoneWall>();
		for (int i = 2; i < width; i++)
		{
			int x = GetActualX(i);
			if (CalamityUtils.ParanoidTileRetrieval(x, y).HasTile)
			{
				float noise = FractalBrownianMotion((float)i * 0.0079f, (float)y * 0.0079f, heightSeed, 5) * 0.5f + 0.5f;
				noise = MathHelper.Lerp(noise, 0.5f, Utils.GetLerpValue((float)width - 13f, (float)width - 1f, i, clamped: true));
				int heightOffset = -(int)Math.Round(MathHelper.Lerp(-9f, 16f, noise));
				for (int dy = 0; dy != heightOffset; dy += Math.Sign(heightOffset))
				{
					WorldUtils.Gen(new Point(x, y + dy), new Shapes.Rectangle(1, 1), Actions.Chain((heightOffset > 0) ? ((GenAction)new Actions.ClearTile()) : ((GenAction)new Actions.SetTile(blockTileType, setSelfFrames: true)), new Actions.PlaceWall((ushort)((MathHelper.Distance((float)dy, (float)heightOffset) >= 3f && (float)heightOffset < 0f) ? wallID : 0)), new Actions.SetLiquid(), new Actions.Smooth(applyToNeighbors: true)));
				}
			}
		}
	}

	public static void LayTreesOnSurface()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		int width = BiomeWidth;
		for (int i = 0; i < width - 8; i++)
		{
			if (!WorldGen.genRand.NextBool(5))
			{
				continue;
			}
			int x = GetActualX(i);
			int y = YStart - 30;
			if (!WorldUtils.Find(new Point(x, y), Searches.Chain(new Searches.Down(40), new Conditions.IsSolid()), out var growPoint))
			{
				continue;
			}
			x = growPoint.X;
			y = growPoint.Y - 1;
			if (CalamityUtils.ParanoidTileRetrieval(x, y).LiquidAmount <= 0)
			{
				Main.tile[x, y].TileType = 20;
				Main.tile[x, y].Get<TileWallWireStateData>().HasTile = true;
				if (!WorldGen.GrowPalmTree(x, y))
				{
					WorldGen.KillTile(x, y);
				}
			}
		}
	}

	public static void CreateBeach()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		int beachWidth = WorldGen.genRand.Next(150, 191);
		GenSearch searchCondition = Searches.Chain(new Searches.Down(3000), new Conditions.IsSolid());
		ushort sandID = (ushort)ModContent.TileType<SulphurousSand>();
		ushort wallID = (ushort)ModContent.WallType<UnsafeSulphurousSandWall>();
		if (!WorldUtils.Find(new Point(BiomeWidth + 4, Main.remixWorld ? YStart : ((int)GenVars.worldSurfaceLow - 20)), searchCondition, out var determinedPoint))
		{
			return;
		}
		ushort tileType = CalamityUtils.ParanoidTileRetrieval(determinedPoint.X, determinedPoint.Y).TileType;
		if ((tileType == 53 || tileType == 112 || tileType == 234) ? true : false)
		{
			beachWidth += 85;
		}
		for (int i = BiomeWidth - 10; i <= BiomeWidth + beachWidth; i++)
		{
			int x = GetActualX(i);
			float xRatio = Utils.GetLerpValue(BiomeWidth - 10, BiomeWidth + beachWidth, i, clamped: true);
			float ditherChance = Utils.GetLerpValue(0.92f, 0.99f, xRatio, clamped: true);
			int depth = (int)(Math.Sin((1f - xRatio) * ((float)Math.PI / 2f)) * 50.0 + 1.0);
			for (int y = YStart - 50; y < YStart + depth; y++)
			{
				Tile tileAtPosition = CalamityUtils.ParanoidTileRetrieval(x, y);
				if (tileAtPosition.HasTile && ValidBeachDestroyTiles.Contains(tileAtPosition.TileType))
				{
					if (Main.tile[x, y].TileType == 5)
					{
						WorldGen.KillTile(x, y);
					}
					else
					{
						Main.tile[x, y].Get<TileWallWireStateData>().HasTile = false;
					}
				}
				else if (tileAtPosition.HasTile && ValidBeachCovertTiles.Contains(tileAtPosition.TileType) && WorldGen.genRand.NextFloat() >= ditherChance)
				{
					Main.tile[x, y].TileType = sandID;
				}
				int[] DungeonWalls = new int[9] { 7, 94, 95, 8, 98, 99, 9, 96, 97 };
				if (tileAtPosition.WallType > 0 && !DungeonWalls.Contains(tileAtPosition.WallType))
				{
					Main.tile[x, y].WallType = wallID;
				}
			}
		}
		if (Main.remixWorld)
		{
			return;
		}
		for (int j = BiomeWidth - 10; j <= BiomeWidth + beachWidth; j++)
		{
			int trueX = (Abyss.AtLeftSideOfWorld ? j : (Main.maxTilesX - j));
			if (!WorldGen.genRand.NextBool(10))
			{
				continue;
			}
			int y2 = YStart - 30;
			if (WorldUtils.Find(new Point(trueX, y2), Searches.Chain(new Searches.Down(100), new Conditions.IsTile(sandID)), out var treePlantPosition))
			{
				treePlantPosition.Y--;
				WorldGen.PlaceTile(treePlantPosition.X, treePlantPosition.Y, ModContent.TileType<AcidWoodTreeSapling>());
				Main.tile[treePlantPosition].TileType = 20;
				Main.tile[treePlantPosition].Get<TileWallWireStateData>().HasTile = true;
				if (!WorldGen.GrowPalmTree(treePlantPosition.X, treePlantPosition.Y))
				{
					WorldGen.KillTile(treePlantPosition.X, treePlantPosition.Y);
				}
			}
		}
	}

	public static void ClearAloneTiles()
	{
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		int width = BiomeWidth;
		int depth = BlockDepth;
		List<ushort> blockTileTypes = new List<ushort>
		{
			(ushort)ModContent.TileType<SulphurousSand>(),
			(ushort)ModContent.TileType<SulphurousSandstone>(),
			(ushort)ModContent.TileType<HardenedSulphurousSandstone>()
		};
		for (int i = 0; i < width; i++)
		{
			int x = GetActualX(i);
			for (int y = YStart; y < YStart + depth; y++)
			{
				Tile t = CalamityUtils.ParanoidTileRetrieval(x, y);
				if (t.HasTile && blockTileTypes.Contains(t.TileType) && !CalamityUtils.ParanoidTileRetrieval(x - 1, y).HasTile && !CalamityUtils.ParanoidTileRetrieval(x + 1, y).HasTile && !CalamityUtils.ParanoidTileRetrieval(x, y - 1).HasTile && !CalamityUtils.ParanoidTileRetrieval(x, y + 1).HasTile)
				{
					WorldUtils.Gen(new Point(x, y), new Shapes.Rectangle(1, 1), Actions.Chain(new Actions.ClearTile(frameNeighbors: true), new Actions.ClearWall(frameNeighbors: true), new Actions.SetLiquid()));
				}
			}
		}
	}

	public static List<Vector2> PlaceScrapPiles()
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		List<Vector2> pastPlacementPositions = new List<Vector2>();
		Point bottomCenter = default(Point);
		for (int i = 0; i < 3; i++)
		{
			tries++;
			if (tries > 20000)
			{
				continue;
			}
			int x = GetActualX(WorldGen.genRand.Next(75, BiomeWidth - 85));
			int y = WorldGen.genRand.Next(YStart + (int)((float)BlockDepth * 0.3f), YStart + (int)((float)BlockDepth * 0.8f));
			Point pilePlacementPosition = new Point(x, y);
			if (WorldGen.SolidTile(pilePlacementPosition.X, pilePlacementPosition.Y))
			{
				i--;
				continue;
			}
			if (pastPlacementPositions.Any(delegate(Vector2 p)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				//IL_0007: Unknown result type (might be due to invalid IL or missing references)
				return Vector2.Distance(p, pilePlacementPosition.ToVector2()) < 80f;
			}))
			{
				i--;
				continue;
			}
			int pileVariant = WorldGen.genRand.Next(7);
			string schematicName = $"Sulphurous Scrap {pileVariant + 1}";
			Vector2? wrappedSchematicArea = SchematicManager.GetSchematicArea(schematicName);
			if (!wrappedSchematicArea.HasValue)
			{
				CalamityMod.Log.Warn((object)("Tried to place a schematic with name \"" + schematicName + "\". No matching schematic file found."));
				continue;
			}
			Vector2 schematicArea = wrappedSchematicArea.Value;
			Vector2 left = pilePlacementPosition.ToVector2() - Vector2.UnitX * schematicArea.X * 0.5f;
			Vector2 right = pilePlacementPosition.ToVector2() + Vector2.UnitX * schematicArea.X * 0.5f;
			while (!WorldGen.SolidTile(CalamityUtils.ParanoidTileRetrieval((int)left.X, (int)left.Y)))
			{
				left.Y++;
			}
			while (!WorldGen.SolidTile(CalamityUtils.ParanoidTileRetrieval((int)right.X, (int)right.Y)))
			{
				right.Y++;
			}
			if (Math.Abs(left.Y - right.Y) >= 20f)
			{
				i--;
				continue;
			}
			if (left.Y >= (float)(YStart + BlockDepth - 50) || right.Y >= (float)(YStart + BlockDepth - 50))
			{
				i--;
				continue;
			}
			((Point)(ref bottomCenter))._002Ector(pilePlacementPosition.X, (int)Math.Max(left.Y, right.Y) + 6);
			bool _ = false;
			SchematicManager.PlaceSchematic<Action<Chest>>(schematicName, bottomCenter, SchematicAnchor.BottomCenter, ref _);
			pastPlacementPositions.Add(bottomCenter.ToVector2());
			tries = 0;
		}
		return pastPlacementPositions;
	}

	public static void GenerateColumnsInCaverns()
	{
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		int columnCount = ColumnCount;
		int width = BiomeWidth;
		int depth = BlockDepth;
		GenSearch searchCondition = Searches.Chain(new Searches.Up(50), new Conditions.IsSolid());
		for (int c = 0; c < columnCount; c++)
		{
			int x = GetActualX(WorldGen.genRand.Next(20, width - 32));
			int y = WorldGen.genRand.Next(YStart, YStart + depth - 55);
			bool tryAgain = false;
			Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
			Tile right = CalamityUtils.ParanoidTileRetrieval(x + 1, y);
			Tile testTile = CalamityUtils.ParanoidTileRetrieval(x, y + 1);
			Tile bottomRight = CalamityUtils.ParanoidTileRetrieval(x + 1, y + 1);
			if (tile.HasTile || right.HasTile)
			{
				tryAgain = true;
			}
			if (!WorldGen.SolidTile(testTile) || !WorldGen.SolidTile(bottomRight))
			{
				tryAgain = true;
			}
			if (!WorldUtils.Find(new Point(x, y), searchCondition, out var top) || !WorldUtils.Find(new Point(x + 1, y), searchCondition, out var topRight) || top.Y != topRight.Y)
			{
				tryAgain = true;
			}
			if (MathHelper.Distance((float)y, (float)top.Y) < 5f)
			{
				tryAgain = true;
			}
			if (tryAgain)
			{
				c--;
			}
			else if (WorldGen.genRand.NextBool(2))
			{
				GenerateColumn(x, top.Y, y);
			}
		}
	}

	public static void GenerateHardenedSandstone()
	{
		int sandstoneSeed = WorldGen.genRand.Next();
		ushort sandstoneID = (ushort)ModContent.TileType<SulphurousSandstone>();
		ushort sandstoneWallID = (ushort)ModContent.WallType<UnsafeSulphurousSandstoneWall>();
		for (int i = 1; i < BiomeWidth; i++)
		{
			for (int y = YStart; y <= YStart + BlockDepth; y++)
			{
				int x = GetActualX(i);
				float sandstoneConvertChance = FractalBrownianMotion((float)i * 0.00115f, (float)y * 0.00115f, sandstoneSeed, 7) * 0.5f + 0.5f;
				sandstoneConvertChance *= Utils.GetLerpValue(4f, 11f, getEdgeScore(x, y), clamped: true);
				sandstoneConvertChance *= Utils.GetLerpValue((float)YStart + 30f, (float)YStart + 54f, y, clamped: true);
				if (WorldGen.genRand.NextFloat() > sandstoneConvertChance || sandstoneConvertChance < 0.5f)
				{
					continue;
				}
				for (int dx = -2; dx <= 2; dx++)
				{
					for (int dy = -2; dy <= 2; dy++)
					{
						if (WorldGen.InWorld(x + dx, y + dy) && CalamityUtils.ParanoidTileRetrieval(x + dx, y + dy).TileType != sandstoneID && SulphSeaTiles.Contains(CalamityUtils.ParanoidTileRetrieval(x + dx, y + dy).TileType))
						{
							Main.tile[x + dx, y + dy].WallType = sandstoneWallID;
							Main.tile[x + dx, y + dy].TileType = sandstoneID;
						}
					}
				}
			}
		}
		static int getEdgeScore(int num, int num2)
		{
			int edgeScore = 0;
			for (int j = num - 6; j <= num + 6; j++)
			{
				if (j != num && !CalamityUtils.ParanoidTileRetrieval(j, num2).HasTile)
				{
					edgeScore++;
				}
			}
			for (int k = num2 - 6; k <= num2 + 6; k++)
			{
				if (k != num2 && !CalamityUtils.ParanoidTileRetrieval(num, k).HasTile)
				{
					edgeScore++;
				}
			}
			return edgeScore;
		}
	}

	public static void PlaceAmbience()
	{
		for (int i = 0; i < BiomeWidth; i++)
		{
			int x = GetActualX(i);
			for (int y = YStart - 140; (double)y < (Main.remixWorld ? ((double)Main.UnderworldLayer) : Main.rockLayer); y++)
			{
				Tile tile = Main.tile[x, y];
				Tile tileUp = Main.tile[x, y - 1];
				Tile tileDown = Main.tile[x, y + 1];
				if (tile.TileType != ModContent.TileType<SulphurousSand>() && tile.TileType != ModContent.TileType<SulphurousSandstone>() && tile.TileType != ModContent.TileType<HardenedSulphurousSandstone>() && tile.TileType != ModContent.TileType<SulphurousShale>())
				{
					continue;
				}
				if (tileUp.LiquidType == 0 && tileUp.LiquidAmount > 0 && !tileUp.HasTile)
				{
					if (WorldGen.genRand.NextBool(25))
					{
						ushort[] Crates = new ushort[3]
						{
							(ushort)ModContent.TileType<PirateCrate4>(),
							(ushort)ModContent.TileType<PirateCrate5>(),
							(ushort)ModContent.TileType<PirateCrate6>()
						};
						WorldGen.PlaceObject(x, y - 1, WorldGen.genRand.Next(Crates));
					}
					if (WorldGen.genRand.NextBool(18))
					{
						ushort[] Vents = new ushort[3]
						{
							(ushort)ModContent.TileType<SteamGeyser1>(),
							(ushort)ModContent.TileType<SteamGeyser2>(),
							(ushort)ModContent.TileType<SteamGeyser3>()
						};
						WorldGen.PlaceObject(x, y - 1, WorldGen.genRand.Next(Vents));
					}
					if (WorldGen.genRand.NextBool(12))
					{
						ushort[] Stalagmites = new ushort[6]
						{
							(ushort)ModContent.TileType<SulphurousStalacmite1>(),
							(ushort)ModContent.TileType<SulphurousStalacmite2>(),
							(ushort)ModContent.TileType<SulphurousStalacmite3>(),
							(ushort)ModContent.TileType<SulphurousStalacmite4>(),
							(ushort)ModContent.TileType<SulphurousStalacmite5>(),
							(ushort)ModContent.TileType<SulphurousStalacmite6>()
						};
						WorldGen.PlaceObject(x, y - 1, WorldGen.genRand.Next(Stalagmites));
					}
					if (WorldGen.genRand.NextBool(15))
					{
						ushort[] SulphuricFossils = new ushort[3]
						{
							(ushort)ModContent.TileType<SulphuricFossil1>(),
							(ushort)ModContent.TileType<SulphuricFossil2>(),
							(ushort)ModContent.TileType<SulphuricFossil3>()
						};
						WorldGen.PlaceObject(x, y - 1, WorldGen.genRand.Next(SulphuricFossils));
					}
					if (WorldGen.genRand.NextBool(18))
					{
						ushort[] Ribs = new ushort[5]
						{
							(ushort)ModContent.TileType<SulphurousRib1>(),
							(ushort)ModContent.TileType<SulphurousRib2>(),
							(ushort)ModContent.TileType<SulphurousRib3>(),
							(ushort)ModContent.TileType<SulphurousRib4>(),
							(ushort)ModContent.TileType<SulphurousRib5>()
						};
						WorldGen.PlaceObject(x, y - 1, WorldGen.genRand.Next(Ribs));
					}
				}
				if (tileDown.LiquidType == 0 && tileDown.LiquidAmount > 0 && !tileDown.HasTile && WorldGen.genRand.NextBool(12))
				{
					ushort[] Stalactites = new ushort[6]
					{
						(ushort)ModContent.TileType<SulphurousStalactite1>(),
						(ushort)ModContent.TileType<SulphurousStalactite2>(),
						(ushort)ModContent.TileType<SulphurousStalactite3>(),
						(ushort)ModContent.TileType<SulphurousStalactite4>(),
						(ushort)ModContent.TileType<SulphurousStalactite5>(),
						(ushort)ModContent.TileType<SulphurousStalactite6>()
					};
					WorldGen.PlaceObject(x, y + 1, WorldGen.genRand.Next(Stalactites));
				}
			}
		}
	}

	public static void GenerateChests(List<Vector2> scrapPilePositions)
	{
		GenerateTreasureChest();
		CalamityUtils.SettleWater(convertToLava: false);
		GenerateOpenAirChestChest();
		GenerateScrapPileChest(scrapPilePositions);
		GenerateDeepWaterChest();
	}

	public static void GenerateTreasureChest()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		Point islandChestPoint = default(Point);
		((Point)(ref islandChestPoint))._002Ector(GetActualX((int)((float)BiomeWidth * 0.36f * 0.5f) + WorldGen.genRand.Next(-8, 9)), YStart - 100);
		while (!tryToGenerateTreasureChest(islandChestPoint))
		{
			islandChestPoint.X += Abyss.AtLeftSideOfWorld.ToDirectionInt();
		}
		static bool tryToGenerateTreasureChest(Point chestPoint)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0128: Unknown result type (might be due to invalid IL or missing references)
			//IL_0131: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0158: Unknown result type (might be due to invalid IL or missing references)
			//IL_017b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0184: Unknown result type (might be due to invalid IL or missing references)
			//IL_021c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0225: Unknown result type (might be due to invalid IL or missing references)
			//IL_0252: Unknown result type (might be due to invalid IL or missing references)
			//IL_025b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0273: Unknown result type (might be due to invalid IL or missing references)
			//IL_027c: Unknown result type (might be due to invalid IL or missing references)
			//IL_029a: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
			WorldUtils.Find(chestPoint, Searches.Chain(new Searches.Down(300), new Conditions.IsSolid()), out var p);
			chestPoint = p;
			int minDepth = 32;
			int digDepth = 0;
			Point startingIslandChestPoint = chestPoint;
			while (true)
			{
				Tile down = CalamityUtils.ParanoidTileRetrieval(chestPoint.X, chestPoint.Y + digDepth);
				Tile downRight = CalamityUtils.ParanoidTileRetrieval(chestPoint.X + 1, chestPoint.Y + digDepth);
				bool downSolidAndValid = down.HasTile && down.IsTileSolid();
				bool downRightSolidAndValid = downRight.HasTile && downRight.IsTileSolid();
				if (digDepth >= minDepth && (!downSolidAndValid || !downRightSolidAndValid))
				{
					break;
				}
				digDepth++;
				if (digDepth >= 80)
				{
					return false;
				}
			}
			chestPoint.Y += digDepth - 12;
			bool nearbyAreaIsClosed = false;
			while (!nearbyAreaIsClosed)
			{
				nearbyAreaIsClosed = true;
				for (int dx = -2; dx < 4; dx++)
				{
					for (int dy = -1; dy < 3; dy++)
					{
						if (!Main.tile[chestPoint.X + dx, chestPoint.Y - dy].HasTile)
						{
							nearbyAreaIsClosed = false;
						}
					}
				}
				if (!nearbyAreaIsClosed)
				{
					chestPoint.Y++;
				}
			}
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					Main.tile[chestPoint.X + i, chestPoint.Y - j].LiquidAmount = 0;
					Main.tile[chestPoint.X + i, chestPoint.Y - j].WallType = (ushort)ModContent.WallType<UnsafeSulphurousSandWall>();
					Main.tile[chestPoint.X + i, chestPoint.Y - j].Get<TileWallWireStateData>().HasTile = false;
				}
			}
			Chest chest = MiscWorldgenRoutines.AddChestWithLoot(chestPoint.X + 1, chestPoint.Y + 1, (ushort)ModContent.TileType<RustyChestTile>());
			if (chest == null)
			{
				return false;
			}
			chest.item[0].SetDefaults(ModContent.ItemType<EffigyOfDecay>());
			chest.item[0].Prefix(-1);
			for (int k = 0; k < 2; k++)
			{
				for (int l = -1; l < 3; l++)
				{
					int oldTileType = Main.tile[startingIslandChestPoint.X + k, startingIslandChestPoint.Y + l].TileType;
					if (oldTileType == 323 || !Main.tileSolid[oldTileType])
					{
						WorldGen.KillTile(startingIslandChestPoint.X + k, startingIslandChestPoint.Y + l);
					}
					else
					{
						Main.tile[startingIslandChestPoint.X + k, startingIslandChestPoint.Y + l].LiquidAmount = 0;
						Main.tile[startingIslandChestPoint.X + k, startingIslandChestPoint.Y + l].TileType = (ushort)ModContent.TileType<SulphurousSandstone>();
					}
				}
			}
			return true;
		}
	}

	public static void GenerateOpenAirChestChest()
	{
		int width = BiomeWidth;
		Dictionary<int, int> depthMap = new Dictionary<int, int>();
		for (int i = 60; i < width - 50; i++)
		{
			int x = GetActualX(i);
			int y = YStart + 9 + 2;
			int dy;
			for (dy = 0; CalamityUtils.ParanoidTileRetrieval(x, y + dy).HasTile || CalamityUtils.ParanoidTileRetrieval(x, y + dy).LiquidAmount <= 0; dy++)
			{
			}
			depthMap[x] = (CalamityUtils.ParanoidTileRetrieval(x, y).HasTile ? (y + dy) : 0);
		}
		for (int j = 0; j < 400; j++)
		{
			int x2 = depthMap.Keys.ElementAt(WorldGen.genRand.Next(10, depthMap.Count - 10));
			int num = depthMap[x2 - 1];
			int currentY = depthMap[x2];
			int rightY = depthMap[x2 + 1];
			if (!((float)Math.Abs((num + currentY + rightY) / 3 - currentY) < 3f) || currentY <= 0)
			{
				continue;
			}
			currentY += 3;
			if (!CalamityUtils.AnySolidTileInSelection(x2, currentY - 1, 4, -4))
			{
				for (int dx = -1; dx < 3; dx++)
				{
					Main.tile[x2 + dx, currentY + 1].LiquidAmount = 0;
					Main.tile[x2 + dx, currentY + 1].TileType = (ushort)ModContent.TileType<SulphurousSand>();
					Main.tile[x2 + dx, currentY + 1].Get<TileWallWireStateData>().HasTile = true;
				}
				Chest chest = MiscWorldgenRoutines.AddChestWithLoot(x2, currentY - 2, (ushort)ModContent.TileType<RustyChestTile>());
				if (chest != null)
				{
					chest.item[0].SetDefaults(ModContent.ItemType<BrokenWaterFilter>());
					chest.item[0].Prefix(-1);
					break;
				}
			}
		}
	}

	public static void GenerateScrapPileChest(List<Vector2> scrapPilePositions)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 800; i++)
		{
			Point val = WorldGen.genRand.Next(scrapPilePositions).ToPoint();
			int x = val.X + WorldGen.genRand.Next(-25 - i / 12, 25 + i / 12);
			int y = val.Y + WorldGen.genRand.Next(-16 - i / 25, 4 + i / 25);
			if (!WorldGen.SolidTile(x, y))
			{
				Chest chest = MiscWorldgenRoutines.AddChestWithLoot(x, y, (ushort)ModContent.TileType<RustyChestTile>());
				if (chest != null)
				{
					chest.item[0].SetDefaults(ModContent.ItemType<RustyBeaconPrototype>());
					chest.item[0].Prefix(-1);
					break;
				}
			}
		}
	}

	public static void GenerateDeepWaterChest()
	{
		for (int i = 0; i < 400; i++)
		{
			int x = GetActualX(WorldGen.genRand.Next(60, BiomeWidth - 60));
			int y = YStart + WorldGen.genRand.Next(BlockDepth - 150, BlockDepth - 60);
			if (WorldGen.SolidTile(x, y))
			{
				continue;
			}
			for (; y < Main.maxTilesY - 210; y++)
			{
				if (WorldGen.SolidTile(x, y))
				{
					y -= 3;
					break;
				}
			}
			if (y < YStart + BlockDepth - 60)
			{
				Chest chest = MiscWorldgenRoutines.AddChestWithLoot(x, y, (ushort)ModContent.TileType<RustyChestTile>());
				if (chest != null)
				{
					chest.item[0].SetDefaults(ModContent.ItemType<ScionsCurio>());
					chest.item[0].Prefix(-1);
					break;
				}
			}
		}
	}

	public static int GetActualX(int x)
	{
		if (Abyss.AtLeftSideOfWorld)
		{
			return x;
		}
		return Main.maxTilesX - 1 - x;
	}

	public static float CalculateDitherChance(int width, int top, int bottom, int x, int y)
	{
		float verticalCompletion = Utils.GetLerpValue(top, bottom, y, clamped: true);
		float lerpValue = Utils.GetLerpValue(0.9f, 1f, (float)x / (float)width, clamped: true);
		float verticalDitherChance = Utils.GetLerpValue(0.9f, 1f, verticalCompletion, clamped: true);
		float ditherChance = lerpValue + verticalDitherChance;
		if (ditherChance > 1f)
		{
			ditherChance = 1f;
		}
		ditherChance -= Utils.GetLerpValue(0.56f, 0.5f, verticalCompletion, clamped: true);
		if (ditherChance < 0f)
		{
			ditherChance = 0f;
		}
		return ditherChance;
	}

	public static float FractalBrownianMotion(float x, float y, int seed, int octaves, float gain = 0.5f, float lacunarity = 2f)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		float result = 0f;
		float frequency = 1f;
		float amplitude = 0.5f;
		x += (float)seed * 0.00489937f % 10f;
		for (int i = 0; i < octaves; i++)
		{
			float noise = NoiseHelper.GetStaticNoise(new Vector2(x, y) * frequency) * 2f - 1f;
			result += noise * amplitude;
			amplitude *= gain;
			frequency *= lacunarity;
		}
		return result;
	}

	public static void GenerateColumn(int left, int top, int bottom)
	{
		int depth = BlockDepth;
		ushort columnID = (ushort)ModContent.TileType<SulphurousColumn>();
		ushort hardenedSandstoneWallID = (ushort)ModContent.WallType<HardenedSulphurousSandstoneWall>();
		ushort sandWallID = (ushort)ModContent.WallType<UnsafeSulphurousSandWall>();
		short variantFrameOffset = (short)(WorldGen.genRand.Next(3) * 36);
		for (int x = left; x < left + 2; x++)
		{
			for (int y = top; y <= bottom; y++)
			{
				short frameX = (short)((x - left) * 18 + variantFrameOffset);
				short frameY = 18;
				if (y == top)
				{
					frameY = 0;
				}
				else if (y == bottom)
				{
					frameY = 36;
				}
				Tile tile = Main.tile[x, y];
				tile.TileType = columnID;
				tile = Main.tile[x, y];
				tile.TileFrameX = frameX;
				tile = Main.tile[x, y];
				tile.TileFrameY = frameY;
				tile = Main.tile[x, y];
				tile.WallType = (((float)y >= (float)YStart + (float)depth * 0.42f) ? hardenedSandstoneWallID : sandWallID);
				tile = Main.tile[x, y];
				tile.Get<TileWallWireStateData>().HasTile = true;
			}
		}
	}

	public static void PlaceStalactite(int x, int y, int height, ushort type)
	{
		for (int dy = 0; dy < height; dy++)
		{
			ushort oldWall = Main.tile[x, y + dy].WallType;
			Main.tile[x, y + dy].ClearEverything();
			Main.tile[x, y + dy].WallType = oldWall;
			Main.tile[x, y + dy].TileType = type;
			Main.tile[x, y + dy].TileFrameY = (short)(dy * 18);
			Main.tile[x, y + dy].Get<TileWallWireStateData>().HasTile = true;
		}
	}

	public static void PlaceStalacmite(int x, int y, int height, ushort type)
	{
		for (int dy = height - 1; dy > 0; dy--)
		{
			ushort oldWall = Main.tile[x, y + dy].WallType;
			Main.tile[x, y - dy].ClearEverything();
			Main.tile[x, y - dy].WallType = oldWall;
			Main.tile[x, y - dy].TileType = type;
			Main.tile[x, y - dy].TileFrameY = (short)(height * 18 - dy * 18);
			Main.tile[x, y - dy].Get<TileWallWireStateData>().HasTile = true;
		}
	}
}
