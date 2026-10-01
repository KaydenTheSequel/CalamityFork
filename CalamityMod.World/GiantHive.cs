using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Potions;
using CalamityMod.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class GiantHive
{
	private static int[] FocusLootHoney = new int[4] { 223, 887, 3017, 4426 };

	private static int[] PotionLootHoney;

	private static int[] BarLootHoney;

	public static bool GrowLivingJungleTree(Point origin, StructureMap structures)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		if (!structures.CanPlace(new Rectangle(origin.X - 50, origin.Y - 50, 100, 100)))
		{
			return false;
		}
		if (TooCloseToImportantLocations(origin))
		{
			return false;
		}
		Ref<int> ref1 = new Ref<int>(0);
		Ref<int> ref2 = new Ref<int>(0);
		Ref<int> ref3 = new Ref<int>(0);
		WorldUtils.Gen(origin, new Shapes.Circle(15), Actions.Chain(new Modifiers.IsSolid(), new Actions.Scanner(ref1), new Modifiers.OnlyTiles(60, 59), new Actions.Scanner(ref2), new Modifiers.OnlyTiles(60), new Actions.Scanner(ref3)));
		if (((float)ref2.Value / (float)ref1.Value < 0.75f || ref3.Value < 2) && !WorldGen.drunkWorldGen)
		{
			return false;
		}
		int treeHeight = (int)Main.worldSurface - Main.maxTilesY / 10;
		bool validHeightFound = false;
		int attempts = 0;
		while (!validHeightFound && attempts++ < 100000)
		{
			for (; !WorldGen.SolidTile(origin.X, treeHeight); treeHeight++)
			{
			}
			if (Main.tile[origin.X, treeHeight].HasTile || Main.tile[origin.X, treeHeight].WallType > 0)
			{
				validHeightFound = true;
			}
		}
		int extraWidth = 0;
		if (validHeightFound)
		{
			for (int k = 0; k < 6; k++)
			{
				double angle = (double)k / 3.0 * 2.0 + 0.57075;
				WorldUtils.Gen(new Point(origin.X + 2, origin.Y - 30), new ShapeRoot((int)angle, WorldGen.genRand.Next(80, 120)), Actions.Chain(new Modifiers.SkipTiles(21, 467, 226, 237), new Modifiers.SkipWalls(87), new Actions.SetTile(383, setSelfFrames: true)));
			}
			for (int y = treeHeight - 50; y <= origin.Y - 30; y++)
			{
				if (y % 45 == 0 && extraWidth < 3)
				{
					extraWidth++;
				}
				if (y <= treeHeight - 45)
				{
					List<Point> list = new List<Point>();
					WorldUtils.Gen(new Point(origin.X + WorldGen.genRand.Next(-3, 3), y), new ShapeBranch(-0.6853981852531433, WorldGen.genRand.Next(5, 25)).OutputEndpoints(list), Actions.Chain(new Modifiers.SkipTiles(21, 467, 226, 237), new Modifiers.SkipWalls(87), new Actions.SetTile(383), new Actions.SetFrames(frameNeighbors: true)));
					WorldUtils.Gen(new Point(origin.X + WorldGen.genRand.Next(-3, 3), y), new ShapeBranch(-2.45619455575943, WorldGen.genRand.Next(5, 25)).OutputEndpoints(list), Actions.Chain(new Modifiers.SkipTiles(21, 467, 226, 237), new Modifiers.SkipWalls(87), new Actions.SetTile(383), new Actions.SetFrames(frameNeighbors: true)));
					foreach (Point item in list)
					{
						WorldUtils.Gen(item, new Shapes.Circle(WorldGen.genRand.Next(5, 12)), Actions.Chain(new Modifiers.Blotches(WorldGen.genRand.Next(2, 5), WorldGen.genRand.Next(3, 5)), new Modifiers.SkipTiles(383, 21, 467, 226, 237), new Modifiers.SkipWalls(78, 87), new Actions.SetTile(384), new Actions.SetFrames(frameNeighbors: true)));
					}
				}
				WorldUtils.Gen(new Point(origin.X + WorldGen.genRand.Next(-1 - extraWidth, 1), y), new Shapes.Rectangle(6 + extraWidth, 3 + extraWidth), Actions.Chain(new Modifiers.SkipTiles(21, 467, 226, 237), new Modifiers.SkipWalls(87), new Actions.RemoveWall(), new Actions.SetTile(383), new Actions.SetFrames()));
				WorldUtils.Gen(new Point(origin.X + WorldGen.genRand.Next(-1, 1), y), new Shapes.Rectangle(6 + extraWidth, 3 + extraWidth), Actions.Chain(new Modifiers.SkipTiles(21, 467, 226, 237), new Modifiers.SkipWalls(87), new Actions.RemoveWall(), new Actions.SetTile(383), new Actions.SetFrames()));
				ShapeData circle = new ShapeData();
				ShapeData biggerCircle = new ShapeData();
				GenAction blotchMod = new Modifiers.Blotches(2, 0.4);
				int radius = extraWidth - 1;
				WorldUtils.Gen(new Point(origin.X + 2, y), new Shapes.Circle(radius), Actions.Chain(blotchMod.Output(circle)));
				WorldUtils.Gen(new Point(origin.X + 2, y), new Shapes.Circle(radius + 1), Actions.Chain(blotchMod.Output(biggerCircle)));
				WorldUtils.Gen(new Point(origin.X + 2, y), new ModShapes.All(circle), Actions.Chain(new Actions.ClearTile(), new Actions.PlaceWall(78)));
				if (WorldGen.genRand.NextBool(20))
				{
					WorldUtils.Gen(new Point(origin.X + WorldGen.genRand.Next(-2, 2), y), new ModShapes.All(circle), Actions.Chain(new Actions.ClearTile(), new Actions.PlaceTile(383)));
				}
				WorldUtils.Gen(new Point(origin.X + 2, y), new ModShapes.All(biggerCircle), Actions.Chain(new Actions.PlaceWall(78)));
			}
			return true;
		}
		return false;
	}

	public static bool CanPlaceGiantHive(Point origin, StructureMap structures)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		if (!structures.CanPlace(new Rectangle(origin.X - 50, origin.Y - 50, 100, 100)))
		{
			return false;
		}
		if (TooCloseToImportantLocations(origin))
		{
			return false;
		}
		Ref<int> ref1 = new Ref<int>(0);
		Ref<int> ref2 = new Ref<int>(0);
		Ref<int> ref3 = new Ref<int>(0);
		WorldUtils.Gen(origin, new Shapes.Circle(15), Actions.Chain(new Modifiers.IsSolid(), new Actions.Scanner(ref1), new Modifiers.OnlyTiles(60, 59), new Actions.Scanner(ref2), new Modifiers.OnlyTiles(60), new Actions.Scanner(ref3)));
		if ((float)ref2.Value / (float)ref1.Value < 0.75f || ref3.Value < 2)
		{
			return false;
		}
		int arrayInc = 0;
		int[] array = new int[1000];
		int[] array2 = new int[1000];
		Vector2 larvaLocation = origin.ToVector2();
		int numHiveTunnels = WorldGen.genRand.Next(10, 13);
		for (int i = 0; i < numHiveTunnels; i++)
		{
			Vector2 movingLarvaLocation = larvaLocation;
			int hiveTunnelTries = WorldGen.genRand.Next(2, 5);
			for (int j = 0; j < hiveTunnelTries; j++)
			{
				movingLarvaLocation = MakeCell((int)larvaLocation.X, (int)larvaLocation.Y, WorldGen.genRand);
			}
			larvaLocation = movingLarvaLocation;
			array[arrayInc] = (int)larvaLocation.X;
			array2[arrayInc] = (int)larvaLocation.Y;
			arrayInc++;
		}
		for (int k = 0; k < numHiveTunnels; k++)
		{
			WorldGen.genRand.Next(1, 2);
			MakeCellHoney((int)larvaLocation.X, (int)larvaLocation.Y, WorldGen.genRand);
		}
		MakeOuterCell((int)larvaLocation.X, (int)larvaLocation.Y, WorldGen.genRand);
		FrameOutAllHiveContents(origin, 50);
		for (int l = 0; l < arrayInc; l++)
		{
			int x = array[l];
			int y = array2[l];
			int treeIndentDirection = 1;
			if (WorldGen.genRand.NextBool())
			{
				treeIndentDirection = -1;
			}
			bool flag = false;
			while (WorldGen.InWorld(x, y, 10) && BadSpotForHoneyFall(x, y))
			{
				x += treeIndentDirection;
				if (Math.Abs(x - array[l]) > 50)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				x += treeIndentDirection;
				if (!SpotActuallyNotInHive(x, y))
				{
					CreateDentForHoneyFall(x, y, treeIndentDirection);
				}
			}
		}
		CreateStandForLarva(larvaLocation);
		int maxAttempts = 1000;
		for (int m = 0; m < maxAttempts; m++)
		{
			Vector2 newStructureLocation = larvaLocation;
			newStructureLocation.X += WorldGen.genRand.Next(-50, 51);
			newStructureLocation.Y += WorldGen.genRand.Next(-50, 51);
			if (WorldGen.InWorld((int)newStructureLocation.X, (int)newStructureLocation.Y) && Vector2.Distance(larvaLocation, newStructureLocation) > 10f && !Main.tile[(int)newStructureLocation.X, (int)newStructureLocation.Y].HasTile && Main.tile[(int)newStructureLocation.X, (int)newStructureLocation.Y].WallType == 86)
			{
				CreateStandForLarva(newStructureLocation);
				break;
			}
		}
		CalamityUtils.AddProtectedStructure(new Rectangle(origin.X - 50, origin.Y - 50, 100, 100), 5);
		return true;
	}

	public static bool PlaceLayer(int X, int Y, int height, int tileType)
	{
		for (int j = Y; j <= Y + height; j++)
		{
			if (Main.tile[X, j].HasTile && Main.tile[X, j + 1].HasTile && Main.tile[X, j + 2].HasTile && Main.tile[X, j + 3].HasTile && Main.tile[X - 1, j + 3].HasTile && Main.tile[X + 1, j + 3].HasTile)
			{
				Main.tile[X, j].TileType = (ushort)tileType;
				continue;
			}
			return false;
		}
		return true;
	}

	private static void FrameOutAllHiveContents(Point origin, int squareHalfWidth)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		int num = Math.Max(10, origin.X - squareHalfWidth);
		int minXsize = Math.Min(Main.maxTilesX - 10, origin.X + squareHalfWidth);
		int maxYSize = Math.Max(10, origin.Y - squareHalfWidth);
		int minYSize = Math.Min(Main.maxTilesY - 10, origin.Y + squareHalfWidth);
		for (int i = num; i < minXsize; i++)
		{
			for (int j = maxYSize; j < minYSize; j++)
			{
				bool canPlaceSand = false;
				if (Main.tile[i, j].TileType == 225 && !Main.tile[i, j - 1].HasTile && !Main.tile[i, j - 2].HasTile && !Main.tile[i, j - 3].HasTile)
				{
					canPlaceSand = true;
				}
				if (canPlaceSand)
				{
					PlaceLayer(i, j, WorldGen.genRand.Next(2, 3), 229);
				}
			}
		}
	}

	public static void CreateHexagon(int centerX, int centerY, int hexagonSize, int tileType, bool doWalls = false)
	{
		int halfHexagonSize = hexagonSize / 2;
		for (int x = -halfHexagonSize; x <= halfHexagonSize; x++)
		{
			int num = Math.Abs(x) / 2;
			int y2 = hexagonSize - Math.Abs(x) / 2;
			for (int i = num; i < y2; i++)
			{
				int tileX = centerX + x;
				int tileY = centerY + i;
				if (WorldGen.InWorld(tileX, tileY))
				{
					Tile tTile = Main.tile[tileX, tileY];
					if (!doWalls)
					{
						tTile.HasTile = true;
						tTile.TileType = (ushort)tileType;
						WorldGen.SquareTileFrame(tileX, tileY);
					}
					else
					{
						tTile.WallType = 86;
						WorldGen.SquareWallFrame(tileX, tileY);
					}
				}
			}
		}
	}

	public static void CreateGiantHiveHexagon(int centerX, int centerY, int hexagonSize, int tileType, bool doWalls = false)
	{
		int halfHexagonSize = hexagonSize / 2;
		for (int x = -halfHexagonSize; x <= halfHexagonSize; x++)
		{
			int num = Math.Abs(x) / 2;
			int y2 = hexagonSize - Math.Abs(x) / 2;
			for (int i = num; i < y2; i++)
			{
				int tileX = centerX + x;
				int tileY = centerY + i;
				if (WorldGen.InWorld(tileX, tileY))
				{
					Tile tTile = Main.tile[tileX, tileY];
					if (!doWalls)
					{
						tTile.HasTile = true;
						tTile.TileType = (ushort)tileType;
						WorldGen.SquareTileFrame(tileX, tileY);
					}
					else
					{
						tTile.WallType = (ushort)ModContent.WallType<GiantHiveWall>();
						WorldGen.SquareWallFrame(tileX, tileY);
					}
				}
			}
		}
	}

	public static void ClearHexagon(int centerX, int centerY, int hexagonSize)
	{
		int halfHexagonSize = hexagonSize / 2;
		for (int x = -halfHexagonSize; x <= halfHexagonSize; x++)
		{
			int num = Math.Abs(x) / 2;
			int y2 = hexagonSize - Math.Abs(x) / 2;
			for (int i = num; i < y2; i++)
			{
				int tileX = centerX + x;
				int tileY = centerY + i;
				if (WorldGen.InWorld(tileX, tileY))
				{
					Tile tTile = Main.tile[tileX, tileY];
					tTile.HasTile = false;
					tTile.WallType = 86;
					tTile.LiquidAmount = 0;
					WorldGen.SquareWallFrame(tileX, tileY);
					if (WorldGen.genRand.NextBool(15))
					{
						tTile.LiquidAmount = 100;
						tTile.LiquidType = 2;
						WorldGen.SquareTileFrame(tileX, tileY);
					}
				}
			}
		}
	}

	public static void GiantHiveWallHexagon(int centerX, int centerY, int hexagonSize)
	{
		int halfHexagonSize = hexagonSize / 2;
		for (int x = -halfHexagonSize; x <= halfHexagonSize; x++)
		{
			int num = Math.Abs(x) / 2;
			int y2 = hexagonSize - Math.Abs(x) / 2;
			for (int i = num; i < y2; i++)
			{
				int tileX = centerX + x;
				int tileY = centerY + i;
				if (WorldGen.InWorld(tileX, tileY))
				{
					Tile tTile = Main.tile[tileX, tileY];
					tTile.HasTile = false;
					tTile.WallType = (ushort)ModContent.WallType<GiantHiveWall>();
					tTile.LiquidAmount = 0;
					WorldGen.SquareWallFrame(tileX, tileY);
					if (WorldGen.genRand.NextBool(15))
					{
						tTile.LiquidAmount = 100;
						tTile.LiquidType = 2;
						WorldGen.SquareTileFrame(tileX, tileY);
					}
				}
			}
		}
	}

	public static Vector2 MakeCell(int x, int y, UnifiedRandom random)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		CreateHexagon(x, y - 30, 110, 225);
		CreateGiantHiveHexagon(x, y - 30, 108, 225, doWalls: true);
		GiantHiveWallHexagon(x, y - 22, 90);
		return new Vector2((float)x, (float)y);
	}

	public static Vector2 MakeCellHoney(int x, int y, UnifiedRandom random)
	{
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		int attempts = 0;
		int hexagonsToPlace = WorldGen.genRand.Next(1, 3);
		while (attempts < 100 && hexagonsToPlace > 0)
		{
			attempts++;
			int offsetX = WorldGen.genRand.Next(-60, 60);
			int offsetY = WorldGen.genRand.Next(-50, 50);
			int targetX = x + offsetX;
			int targetY = y + offsetY;
			bool valid = true;
			for (int i = -1; (i <= 1) & valid; i++)
			{
				for (int j = -1; (j <= 1) & valid; j++)
				{
					int checkX = targetX + i;
					int checkY = targetY + j;
					if (!WorldGen.InWorld(checkX, checkY))
					{
						valid = false;
					}
					else if (!Main.tile[checkX, checkY].HasTile || Main.tile[checkX, checkY].TileType != 225)
					{
						valid = false;
					}
				}
			}
			if (valid)
			{
				int radius = WorldGen.genRand.Next(7, 10);
				CreateHexagon(targetX, targetY, radius, 229);
				hexagonsToPlace--;
			}
		}
		return new Vector2((float)x, (float)y);
	}

	public static Vector2 MakeOuterCell(int x, int y, UnifiedRandom random)
	{
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		int numConnections = WorldGen.genRand.Next(3, 6);
		List<Vector2> placedHexes = new List<Vector2>();
		int chestsPlaced = 0;
		int chestMax = 4;
		for (int i = 0; i < numConnections; i++)
		{
			bool placed = false;
			int attempts = 0;
			while (!placed && attempts < 10)
			{
				attempts++;
				float angle = MathHelper.ToRadians((float)WorldGen.genRand.Next(0, 180));
				float distance = WorldGen.genRand.Next(80, 120);
				int newX = x + (int)(Math.Cos(angle) * (double)distance);
				int newY = y + (int)(Math.Sin(angle) * (double)distance);
				Vector2 newPos = new Vector2((float)newX, (float)newY);
				if (!placedHexes.Any(delegate(Vector2 pos)
				{
					//IL_0000: Unknown result type (might be due to invalid IL or missing references)
					//IL_0002: Unknown result type (might be due to invalid IL or missing references)
					return Vector2.Distance(pos, newPos) < 60f;
				}))
				{
					int hexRadius = WorldGen.genRand.Next(30, 45);
					CreateGiantHiveHexagon(newX, newY - 15, hexRadius - 1, 225, doWalls: true);
					CreateHexagon(newX, newY - 15, hexRadius, 225);
					MiscWorldgenRoutines.ClearTunnel(x, y + 30, newX, newY, 5, 0, 2);
					MiscWorldgenRoutines.CreateTunnel(x, y + 30, newX, newY, 6, 225);
					MiscWorldgenRoutines.ClearTunnel(x, y + 30, newX, newY, 3, (ushort)ModContent.WallType<GiantHiveWall>(), 2);
					GiantHiveWallHexagon(newX, newY - 10, WorldGen.genRand.Next(19, 26));
					if (chestsPlaced < chestMax)
					{
						CreateStandAndPlaceHoneyChest(new Vector2((float)newX, (float)newY));
						chestsPlaced++;
					}
					placedHexes.Add(newPos);
					placed = true;
				}
			}
		}
		GiantHiveWallHexagon(x, y - 22, 90);
		ClearHexagon(x, y - 18, 80);
		return new Vector2((float)x, (float)y);
	}

	private static bool TooCloseToImportantLocations(Point origin)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		int x = origin.X;
		int y = origin.Y;
		int checkRadius = 150;
		for (int i = x - checkRadius; i < x + checkRadius; i += 10)
		{
			if (i <= 0 || i > Main.maxTilesX - 1)
			{
				continue;
			}
			for (int j = y - checkRadius; j < y + checkRadius; j += 10)
			{
				if (j > 0 && j <= Main.maxTilesY - 1)
				{
					if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == 226)
					{
						return true;
					}
					if (Main.tile[i, j].WallType == 83 || Main.tile[i, j].WallType == 3 || Main.tile[i, j].WallType == 87)
					{
						return true;
					}
					if (Main.tile[i, j].LiquidAmount != 0 && Main.tile[i, j].LiquidType == 3)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	private static void CreateDentForHoneyFall(int x, int y, int dir)
	{
		dir *= -1;
		y++;
		int honeyDentTries = 0;
		while ((honeyDentTries < 4 || WorldGen.SolidTile(x, y)) && x > 10 && x < Main.maxTilesX - 10)
		{
			honeyDentTries++;
			x += dir;
			if (WorldGen.SolidTile(x, y))
			{
				WorldGen.PoundTile(x, y);
				if (!Main.tile[x, y + 1].HasTile)
				{
					Main.tile[x, y + 1].Get<TileWallWireStateData>().HasTile = true;
					Main.tile[x, y + 1].TileType = 225;
				}
			}
		}
	}

	private static bool SpotActuallyNotInHive(int x, int y)
	{
		for (int i = x - 1; i <= x + 2; i++)
		{
			for (int j = y - 1; j <= y + 2; j++)
			{
				if (i < 10 || i > Main.maxTilesX - 10)
				{
					return true;
				}
				if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType != 225)
				{
					return true;
				}
			}
		}
		return false;
	}

	private static bool BadSpotForHoneyFall(int x, int y)
	{
		if (Main.tile[x, y].HasTile && Main.tile[x, y + 1].HasTile && Main.tile[x + 1, y].HasTile)
		{
			return !Main.tile[x + 1, y + 1].HasTile;
		}
		return true;
	}

	public static void CreateStandForLarva(Vector2 position)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		GenVars.larvaX[GenVars.numLarva] = Utils.Clamp((int)position.X, 5, Main.maxTilesX - 5);
		GenVars.larvaY[GenVars.numLarva] = Utils.Clamp((int)position.Y, 5, Main.maxTilesY - 5);
		GenVars.numLarva++;
		if (GenVars.numLarva >= GenVars.larvaX.Length)
		{
			GenVars.numLarva = GenVars.larvaX.Length - 1;
		}
		int larvaX = (int)position.X;
		int larvaY = (int)position.Y;
		for (int i = larvaX - 1; i <= larvaX + 1 && i > 0 && i < Main.maxTilesX; i++)
		{
			for (int j = larvaY - 2; j <= larvaY + 1 && j > 0 && j < Main.maxTilesY; j++)
			{
				if (j != larvaY + 1)
				{
					Main.tile[i, j].Get<TileWallWireStateData>().HasTile = false;
					continue;
				}
				Main.tile[i, j].Get<TileWallWireStateData>().HasTile = true;
				Main.tile[i, j].TileType = 225;
				Main.tile[i, j].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
				Main.tile[i, j].Get<TileWallWireStateData>().IsHalfBlock = false;
			}
		}
	}

	public static void CreateStandAndPlaceHoneyChest(Vector2 position)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		int chestPlacementX = Utils.Clamp((int)position.X, 5, Main.maxTilesX - 5);
		int chestPlacementY = Utils.Clamp((int)position.Y, 5, Main.maxTilesY - 5);
		int chestX = (int)position.X;
		int chestY = (int)position.Y;
		for (int i = chestX; i <= chestX + 1 && i > 0 && i < Main.maxTilesX; i++)
		{
			for (int j = chestY - 1; j <= chestY + 1 && j > 0 && j < Main.maxTilesY; j++)
			{
				if (j != chestY + 1)
				{
					Main.tile[i, j].Get<TileWallWireStateData>().HasTile = false;
					continue;
				}
				Main.tile[i, j].Get<TileWallWireStateData>().HasTile = true;
				Main.tile[i, j].TileType = 225;
				Main.tile[i, j].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
				Main.tile[i, j].Get<TileWallWireStateData>().IsHalfBlock = false;
			}
		}
		int finalChestX = chestPlacementX;
		int finalChestY = chestPlacementY;
		for (int k = finalChestX; k <= finalChestX + 1; k++)
		{
			for (int l = finalChestY - 1; l <= finalChestY + 1; l++)
			{
				if (l != finalChestY + 1)
				{
					Main.tile[k, l].Get<TileWallWireStateData>().HasTile = false;
					continue;
				}
				Main.tile[k, l].Get<TileWallWireStateData>().HasTile = true;
				Main.tile[k, l].TileType = 225;
				Main.tile[k, l].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
				Main.tile[k, l].Get<TileWallWireStateData>().IsHalfBlock = false;
			}
		}
		FillHoneyChest(WorldGen.PlaceChest(finalChestX, finalChestY, 21, notNearOtherChests: false, 29), WorldGen.genRand);
	}

	public static void FillHoneyChest(int id, UnifiedRandom random)
	{
		if (id < 0)
		{
			return;
		}
		Chest chest = Main.chest[id];
		if (chest != null)
		{
			int index = 0;
			chest.item[index].SetDefaults(random.Next(FocusLootHoney));
			chest.item[index++].Prefix(-1);
			if (random.Next(3) <= 1)
			{
				chest.item[index].SetDefaults(random.Next(BarLootHoney));
				chest.item[index++].stack = random.Next(7, 15);
			}
			else
			{
				chest.item[index].SetDefaults(73);
				chest.item[index++].stack = random.Next(3, 5);
			}
			if (random.NextBool())
			{
				chest.item[index].SetDefaults(random.Next(PotionLootHoney));
				chest.item[index++].stack = random.Next(1, 4);
			}
			else
			{
				chest.item[index].SetDefaults(1134);
				chest.item[index++].stack = random.Next(3, 7);
			}
			if (random.NextBool())
			{
				chest.item[index].SetDefaults(209);
				chest.item[index++].stack = random.Next(4, 6);
			}
			else
			{
				chest.item[index].SetDefaults(331);
				chest.item[index++].stack = random.Next(3, 5);
			}
			if (random.NextBool())
			{
				chest.item[index].SetDefaults(2350);
				chest.item[index++].stack = random.Next(1, 4);
			}
			else
			{
				chest.item[index].SetDefaults(4388);
				chest.item[index++].stack = random.Next(18, 36);
			}
		}
	}

	static GiantHive()
	{
		int[] obj = new int[5] { 2345, 289, 293, 2323, 0 };
		obj[4] = ModContent.ItemType<PhotosynthesisPotion>();
		PotionLootHoney = obj;
		BarLootHoney = new int[2]
		{
			(GenVars.silverBar == 9) ? 21 : 705,
			(GenVars.goldBar == 8) ? 19 : 706
		};
	}
}
