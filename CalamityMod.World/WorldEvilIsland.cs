using System;
using System.Collections.Generic;
using CalamityMod.DataStructures;
using CalamityMod.World.Planets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class WorldEvilIsland
{
	public static void PlaceEvilIsland()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		int x = Main.maxTilesX;
		int yIslandGen;
		Rectangle potentialArea;
		int xIslandGen;
		if (WorldGen.drunkWorldGen)
		{
			do
			{
				xIslandGen = WorldGen.genRand.Next((int)((double)x * 0.1), (int)((double)x * 0.2));
				yIslandGen = WorldGen.genRand.Next(95, 126);
				yIslandGen = Math.Min(yIslandGen, (int)GenVars.worldSurfaceLow - 50);
				int checkAreaX = 160;
				int checkAreaY = 90;
				potentialArea = Utils.CenteredRectangle(new Vector2((float)xIslandGen, (float)yIslandGen), new Vector2((float)checkAreaX, (float)checkAreaY));
			}
			while (!Planetoid.InvalidSkyPlacementArea(potentialArea));
			int tileXLookup;
			for (tileXLookup = xIslandGen; Main.tile[tileXLookup, yIslandGen].HasTile; tileXLookup++)
			{
			}
			xIslandGen = tileXLookup;
			EvilIsland(xIslandGen, yIslandGen, genCorruptIsland: true);
			EvilIslandHouse(xIslandGen, yIslandGen, genCorruptHouse: true);
			do
			{
				xIslandGen = WorldGen.genRand.Next((int)((double)x * 0.8), (int)((double)x * 0.9));
				yIslandGen = WorldGen.genRand.Next(95, 126);
				yIslandGen = Math.Min(yIslandGen, (int)GenVars.worldSurfaceLow - 50);
				int checkAreaX2 = 160;
				int checkAreaY2 = 90;
				potentialArea = Utils.CenteredRectangle(new Vector2((float)xIslandGen, (float)yIslandGen), new Vector2((float)checkAreaX2, (float)checkAreaY2));
			}
			while (!Planetoid.InvalidSkyPlacementArea(potentialArea));
			tileXLookup = xIslandGen;
			while (Main.tile[tileXLookup, yIslandGen].HasTile)
			{
				tileXLookup--;
			}
			xIslandGen = tileXLookup;
			EvilIsland(xIslandGen, yIslandGen, genCorruptIsland: false);
			EvilIslandHouse(xIslandGen, yIslandGen, genCorruptHouse: false);
			return;
		}
		do
		{
			xIslandGen = (WorldGen.crimson ? WorldGen.genRand.Next((int)((double)x * 0.1), (int)((double)x * 0.3)) : WorldGen.genRand.Next((int)((double)x * 0.7), (int)((double)x * 0.9)));
			yIslandGen = WorldGen.genRand.Next(95, 126);
			yIslandGen = Math.Min(yIslandGen, (int)GenVars.worldSurfaceLow - 50);
			int checkAreaX3 = 160;
			int checkAreaY3 = 90;
			potentialArea = Utils.CenteredRectangle(new Vector2((float)xIslandGen, (float)yIslandGen), new Vector2((float)checkAreaX3, (float)checkAreaY3));
		}
		while (!Planetoid.InvalidSkyPlacementArea(potentialArea));
		int i = xIslandGen;
		if (WorldGen.crimson)
		{
			for (; Main.tile[i, yIslandGen].HasTile; i++)
			{
			}
		}
		else
		{
			while (Main.tile[i, yIslandGen].HasTile)
			{
				i--;
			}
		}
		xIslandGen = i;
		EvilIsland(xIslandGen, yIslandGen, WorldGen.crimson);
		EvilIslandHouse(xIslandGen, yIslandGen, WorldGen.crimson);
	}

	public static void EvilIsland(int i, int j, bool genCorruptIsland)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0616: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Unknown result type (might be due to invalid IL or missing references)
		//IL_063c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_0732: Unknown result type (might be due to invalid IL or missing references)
		//IL_073c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_0766: Unknown result type (might be due to invalid IL or missing references)
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0776: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		int leftOffset = 86;
		int rightOffset = 86;
		int maxVerticalOffset = 24;
		List<Point> cloudPositions = new List<Point>();
		Point cloudPosition = default(Point);
		for (int dx = -leftOffset; dx < rightOffset; dx += WorldGen.genRand.Next(21, 26))
		{
			float completionRatio010 = CalamityUtils.Convert01To010(Utils.GetLerpValue((float)(-leftOffset) - 22f, (float)rightOffset + 22f, dx, clamped: true));
			int verticalOffset = (int)(completionRatio010 * (float)maxVerticalOffset);
			((Point)(ref cloudPosition))._002Ector(i + dx, j + verticalOffset);
			int radius = WorldGen.genRand.Next(22, 26) - (int)((1f - completionRatio010) * 15f);
			WorldUtils.Gen(cloudPosition, new CustomShapes.DistortedCircle(radius, 0.1f), Actions.Chain(new Actions.SetTile(189), new Modifiers.Blotches(4, 1.0)));
			cloudPositions.Add(cloudPosition);
		}
		WorldUtils.Gen(new Point(i, j - 4), new Shapes.Circle((leftOffset + rightOffset - 30) / 2, 16), new Actions.ClearTile());
		Tile tile;
		foreach (Point item in cloudPositions)
		{
			Vector2 offsetCloudPosition = item.ToVector2();
			while (true)
			{
				tile = CalamityUtils.ParanoidTileRetrieval((int)offsetCloudPosition.X, (int)offsetCloudPosition.Y);
				if (tile.HasTile)
				{
					break;
				}
				offsetCloudPosition.Y++;
			}
			for (int k = 0; k < 3; k++)
			{
				int radius2 = WorldGen.genRand.Next(6, 9);
				Vector2 randomCloudPosition = offsetCloudPosition + WorldGen.genRand.NextVector2Circular(radius2, radius2) * 0.75f;
				randomCloudPosition.Y -= (float)radius2 * 0.4f;
				randomCloudPosition.Y += 5f;
				WorldUtils.Gen(randomCloudPosition.ToPoint(), new Shapes.Circle(radius2 / 2), new Actions.SetTile(189));
			}
		}
		for (int l = -leftOffset + 10; l < rightOffset - 10; l++)
		{
			float completionRatio11 = CalamityUtils.Convert01To010(Utils.GetLerpValue((float)(-leftOffset) - 22f, (float)rightOffset + 22f, l, clamped: true));
			int verticalOffset2 = (int)((1f - completionRatio11) * (float)maxVerticalOffset);
			for (int dy = -5; dy < maxVerticalOffset + 13 - verticalOffset2; dy++)
			{
				tile = CalamityUtils.ParanoidTileRetrieval(i + l, j + dy);
				if (!tile.HasTile)
				{
					tile = Main.tile[i + l, j + dy];
					tile.TileType = (ushort)(genCorruptIsland ? 400 : 401);
					tile = Main.tile[i + l, j + dy];
					tile.Get<TileWallWireStateData>().HasTile = true;
				}
			}
		}
		List<Point> borderPoints = new List<Point>();
		for (int m = -leftOffset + 14; m < rightOffset - 14; m++)
		{
			int verticalBorder = j - 10;
			do
			{
				tile = CalamityUtils.ParanoidTileRetrieval(i + m, verticalBorder);
				if (tile.TileType == 189)
				{
					break;
				}
				verticalBorder++;
			}
			while (verticalBorder <= j + 35);
			if (verticalBorder < j + 35)
			{
				borderPoints.Add(new Point(i + m, verticalBorder));
			}
		}
		for (int n = 0; n < 10; n++)
		{
			Point borderToGenerateAt = borderPoints[WorldGen.genRand.Next(borderPoints.Count)];
			Vector2 moveDirection = Vector2.UnitY.RotatedBy(WorldGen.genRand.NextFloat(-0.4f, 0.4f));
			for (int num = 0; num < 4; num++)
			{
				WorldUtils.Gen((borderToGenerateAt.ToVector2() + Main.rand.NextVector2Circular(5f, 5f) + moveDirection * (float)num * 3f).ToPoint(), new CustomShapes.DistortedCircle(WorldGen.genRand.Next(8, 10) - num, 0.4f), Actions.Chain(new Actions.SetTile((ushort)(genCorruptIsland ? 400 : 401)), new Modifiers.IsSolid(), new Modifiers.Blotches(5, 1.0), new Modifiers.OnlyTiles(400, 401)));
			}
		}
		Point blotchPosition = default(Point);
		for (int num2 = 0; num2 < 12; num2++)
		{
			int radius3 = WorldGen.genRand.Next(6, 8);
			ushort tileType = (ushort)(genCorruptIsland ? 398 : 399);
			((Point)(ref blotchPosition))._002Ector(i + WorldGen.genRand.Next(-leftOffset + 20, rightOffset - 20), j + WorldGen.genRand.Next(-3, 12));
			if (num2 > 4)
			{
				if (blotchPosition.Y <= j + 3)
				{
					continue;
				}
				radius3 -= WorldGen.genRand.Next(3);
				tileType = (ushort)(genCorruptIsland ? 22 : 204);
			}
			WorldUtils.Gen(blotchPosition, new CustomShapes.DistortedCircle(radius3, 0.35f), Actions.Chain(new Actions.SetTile(tileType), new Modifiers.IsSolid(), new Modifiers.Blotches(5, 1.0), new Modifiers.OnlyTiles(400, 401)));
		}
		List<Point> surfacePoints = new List<Point>();
		for (int num3 = -leftOffset + 24; num3 < rightOffset - 24; num3++)
		{
			if (Math.Abs(num3) < 15)
			{
				continue;
			}
			int surface = j - 15;
			bool hitCloud = false;
			do
			{
				tile = CalamityUtils.ParanoidTileRetrieval(i + num3, surface);
				if (tile.HasTile)
				{
					break;
				}
				surface++;
				tile = CalamityUtils.ParanoidTileRetrieval(i + num3, surface);
				if (tile.TileType == 189)
				{
					hitCloud = true;
					break;
				}
			}
			while (surface <= j + 35);
			if (!hitCloud && surface < j + 35)
			{
				surfacePoints.Add(new Point(i + num3, surface));
			}
		}
		for (int num4 = 0; num4 < 4; num4++)
		{
			Point surfaceToGenerateAt = surfacePoints[WorldGen.genRand.Next(surfacePoints.Count)];
			Point origin = (surfaceToGenerateAt.ToVector2() + Vector2.UnitY * 5f).ToPoint();
			Shapes.Circle shape = new Shapes.Circle(WorldGen.genRand.Next(8, 10));
			GenAction[] array = new GenAction[3];
			tile = CalamityUtils.ParanoidTileRetrieval(surfaceToGenerateAt.X, surfaceToGenerateAt.Y);
			array[0] = new Actions.SetTile(tile.TileType);
			array[1] = new Modifiers.SkipTiles(22, 204);
			array[2] = new Modifiers.Blotches(5, 1.0);
			WorldUtils.Gen(origin, shape, Actions.Chain(array));
		}
		Vector2 oreStartPosition = default(Vector2);
		((Vector2)(ref oreStartPosition))._002Ector((float)(i + WorldGen.genRand.Next(-leftOffset + 28, rightOffset - 28)), (float)(j + WorldGen.genRand.Next(8, 14)));
		Vector2 oreEndPosition = oreStartPosition + Vector2.UnitX.RotatedBy(WorldGen.genRand.NextFloat(-0.36f, -0.14f)) * (float)WorldGen.genRand.NextBool(2).ToDirectionInt() * WorldGen.genRand.NextFloat(18f, 25f);
		Vector2 oreMiddlePosition = (oreStartPosition + oreEndPosition) * 0.5f + WorldGen.genRand.NextVector2Circular(6f, 6f);
		List<Vector2> smoothOrePositions = new BezierCurve(oreStartPosition, oreMiddlePosition, oreStartPosition).GetPoints(25);
		for (int num5 = 0; num5 < smoothOrePositions.Count; num5++)
		{
			float strength = MathHelper.Lerp(1f, 4f, CalamityUtils.Convert01To010((float)num5 / (float)smoothOrePositions.Count));
			WorldUtils.Gen(smoothOrePositions[num5].ToPoint(), new Shapes.Circle((int)strength), Actions.Chain(new Actions.SetTile((ushort)(genCorruptIsland ? 22 : 204))));
		}
		for (int num6 = -leftOffset - 30; num6 < rightOffset + 30; num6++)
		{
			for (int num7 = -8; num7 < 55; num7++)
			{
				tile = CalamityUtils.ParanoidTileRetrieval(i + num6, j + num7);
				if (tile.TileType == 189)
				{
					WorldGen.paintTile(i + num6, j + num7, (byte)((!genCorruptIsland) ? 1 : 10));
				}
			}
		}
	}

	public static void EvilIslandHouse(int i, int j, bool genCorruptHouse)
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		ushort type = (ushort)(genCorruptHouse ? 152u : 347u);
		byte wall = (byte)(genCorruptHouse ? 35u : 174u);
		Vector2 vector = default(Vector2);
		((Vector2)(ref vector))._002Ector((float)i, (float)j);
		int houseDirection = 1;
		if (WorldGen.genRand.NextBool(2))
		{
			houseDirection = -1;
		}
		int largerRandValue = WorldGen.genRand.Next(7, 12);
		int smallerRandValue = WorldGen.genRand.Next(5, 7);
		vector.X = i + (largerRandValue + 2) * houseDirection;
		for (int k = j - 15; k < j + 30; k++)
		{
			if (Main.tile[(int)vector.X, k].HasTile)
			{
				vector.Y = k - 1;
				break;
			}
		}
		vector.X = i;
		int minXSize = (int)(vector.X - (float)largerRandValue - 1f);
		int maxXSize = (int)(vector.X + (float)largerRandValue + 1f);
		int minYSize = (int)(vector.Y - (float)smallerRandValue - 1f);
		int maxYSize = (int)(vector.Y + 2f);
		if (minXSize < 0)
		{
			minXSize = 0;
		}
		if (maxXSize > Main.maxTilesX)
		{
			maxXSize = Main.maxTilesX;
		}
		if (minYSize < 0)
		{
			minYSize = 0;
		}
		if (maxYSize > Main.maxTilesY)
		{
			maxYSize = Main.maxTilesY;
		}
		for (int l = minXSize; l <= maxXSize; l++)
		{
			for (int m = minYSize - 1; m < maxYSize + 1; m++)
			{
				if (m != minYSize - 1 || (l != minXSize && l != maxXSize))
				{
					Main.tile[l, m].Get<TileWallWireStateData>().HasTile = true;
					Main.tile[l, m].LiquidAmount = 0;
					Main.tile[l, m].TileType = type;
					Main.tile[l, m].WallType = 0;
					Main.tile[l, m].Get<TileWallWireStateData>().IsHalfBlock = false;
					Main.tile[l, m].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
				}
			}
		}
		minXSize = (int)(vector.X - (float)largerRandValue);
		maxXSize = (int)(vector.X + (float)largerRandValue);
		minYSize = (int)(vector.Y - (float)smallerRandValue);
		maxYSize = (int)(vector.Y + 1f);
		if (minXSize < 0)
		{
			minXSize = 0;
		}
		if (maxXSize > Main.maxTilesX)
		{
			maxXSize = Main.maxTilesX;
		}
		if (minYSize < 0)
		{
			minYSize = 0;
		}
		if (maxYSize > Main.maxTilesY)
		{
			maxYSize = Main.maxTilesY;
		}
		for (int n = minXSize; n <= maxXSize; n++)
		{
			for (int p = minYSize; p < maxYSize; p++)
			{
				if ((p != minYSize || (n != minXSize && n != maxXSize)) && Main.tile[n, p].WallType == 0)
				{
					Main.tile[n, p].Get<TileWallWireStateData>().HasTile = false;
					Main.tile[n, p].WallType = wall;
				}
			}
		}
		int xPos = i + (largerRandValue + 1) * houseDirection;
		int t = (int)vector.Y;
		for (int s = xPos - 2; s <= xPos + 2; s++)
		{
			Main.tile[s, t].Get<TileWallWireStateData>().HasTile = false;
			Main.tile[s, t - 1].Get<TileWallWireStateData>().HasTile = false;
			Main.tile[s, t - 2].Get<TileWallWireStateData>().HasTile = false;
		}
		WorldGen.PlaceTile(xPos, t, 10, mute: true, forced: false, -1, genCorruptHouse ? 1 : 10);
		xPos = i + (largerRandValue + 1) * -houseDirection - houseDirection;
		for (int yPos = minYSize; yPos <= maxYSize + 1; yPos++)
		{
			Main.tile[xPos, yPos].Get<TileWallWireStateData>().HasTile = true;
			Main.tile[xPos, yPos].LiquidAmount = 0;
			Main.tile[xPos, yPos].TileType = type;
			Main.tile[xPos, yPos].WallType = 0;
			Main.tile[xPos, yPos].Get<TileWallWireStateData>().IsHalfBlock = false;
			Main.tile[xPos, yPos].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
		}
		int contain = ((!genCorruptHouse) ? 1569 : 1571);
		WorldGen.AddBuriedChest(i, t - 3, contain, notNearOtherChests: false, genCorruptHouse ? 19 : 20, trySlope: false, 0);
		int wallXMinSize = i - largerRandValue / 2 + 1;
		int wallXMaxSize = i + largerRandValue / 2 - 1;
		int wallYSize = 1;
		if (largerRandValue > 10)
		{
			wallYSize = 2;
		}
		int wallYRange = (minYSize + maxYSize) / 2 - 1;
		for (int wallX = wallXMinSize - wallYSize; wallX <= wallXMinSize + wallYSize; wallX++)
		{
			for (int wallY = wallYRange - 1; wallY <= wallYRange + 1; wallY++)
			{
				Main.tile[wallX, wallY].WallType = 21;
			}
		}
		for (int num = wallXMaxSize - wallYSize; num <= wallXMaxSize + wallYSize; num++)
		{
			for (int num2 = wallYRange - 1; num2 <= wallYRange + 1; num2++)
			{
				Main.tile[num, num2].WallType = 21;
			}
		}
		int furnitureXPos = i + (largerRandValue / 2 + 1) * -houseDirection;
		WorldGen.PlaceTile(furnitureXPos, maxYSize - 1, 14, mute: true, forced: false, -1, genCorruptHouse ? 1 : 8);
		WorldGen.PlaceTile(furnitureXPos - 2, maxYSize - 1, 15, mute: true, forced: false, 0, genCorruptHouse ? 2 : 11);
		Main.tile[furnitureXPos - 2, maxYSize - 1].TileFrameX += 18;
		Main.tile[furnitureXPos - 2, maxYSize - 2].TileFrameX += 18;
		WorldGen.PlaceTile(furnitureXPos + 2, maxYSize - 1, 15, mute: true, forced: false, 0, genCorruptHouse ? 2 : 11);
	}
}
