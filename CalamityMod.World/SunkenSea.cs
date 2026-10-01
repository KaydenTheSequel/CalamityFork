using System;
using System.Collections.Generic;
using CalamityMod.Tiles.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using CalamityMod.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class SunkenSea
{
	private struct Hub
	{
		public Vector2 Position;

		public Hub(Vector2 position)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			Position = position;
		}

		public Hub(float x, float y)
		{
			//IL_0003: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			Position = new Vector2(x, y);
		}
	}

	private class Cluster : List<Hub>
	{
	}

	private class ClusterGroup : List<Cluster>
	{
		public int Width;

		public int Height;

		private void SearchForCluster(bool[,] hubMap, List<Point> pointCluster, int x, int y, int level = 2)
		{
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			pointCluster.Add(new Point(x, y));
			hubMap[x, y] = false;
			level--;
			if (level != -1)
			{
				if (x > 0 && hubMap[x - 1, y])
				{
					SearchForCluster(hubMap, pointCluster, x - 1, y, level);
				}
				if (x < hubMap.GetLength(0) - 1 && hubMap[x + 1, y])
				{
					SearchForCluster(hubMap, pointCluster, x + 1, y, level);
				}
				if (y > 0 && hubMap[x, y - 1])
				{
					SearchForCluster(hubMap, pointCluster, x, y - 1, level);
				}
				if (y < hubMap.GetLength(1) - 1 && hubMap[x, y + 1])
				{
					SearchForCluster(hubMap, pointCluster, x, y + 1, level);
				}
			}
		}

		private void AttemptClaim(int x, int y, int[,] clusterIndexMap, List<List<Point>> pointClusters, int index)
		{
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			int clusterPos = clusterIndexMap[x, y];
			if (clusterPos == -1 || clusterPos == index)
			{
				return;
			}
			int clusterHeight = (WorldGen.genRand.NextBool(2) ? (-1) : index);
			foreach (Point current in pointClusters[clusterPos])
			{
				clusterIndexMap[current.X, current.Y] = clusterHeight;
			}
		}

		public void Generate(int width, int height)
		{
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0196: Unknown result type (might be due to invalid IL or missing references)
			//IL_019b: Unknown result type (might be due to invalid IL or missing references)
			//IL_019f: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0202: Unknown result type (might be due to invalid IL or missing references)
			//IL_032d: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
			Width = width;
			Height = height;
			Clear();
			bool[,] array = new bool[width, height];
			int clusterPos = (width >> 1) - 1;
			int clusterHeight = (height >> 1) - 1;
			int clusterSize = (clusterPos + 1) * (clusterPos + 1);
			Point point = default(Point);
			((Point)(ref point))._002Ector(clusterPos, clusterHeight);
			for (int i = point.Y - clusterHeight; i <= point.Y + clusterHeight; i++)
			{
				float num4 = (float)clusterPos / (float)clusterHeight * (float)(i - point.Y);
				int num5 = Math.Min(clusterPos, (int)Math.Sqrt((float)clusterSize - num4 * num4));
				for (int j = point.X - num5; j <= point.X + num5; j++)
				{
					array[j, i] = WorldGen.genRand.NextBool(2);
				}
			}
			List<List<Point>> list = new List<List<Point>>();
			for (int k = 0; k < array.GetLength(0); k++)
			{
				for (int l = 0; l < array.GetLength(1); l++)
				{
					if (array[k, l] && WorldGen.genRand.NextBool(2))
					{
						List<Point> list2 = new List<Point>();
						SearchForCluster(array, list2, k, l);
						if (list2.Count > 2)
						{
							list.Add(list2);
						}
					}
				}
			}
			int[,] array2 = new int[array.GetLength(0), array.GetLength(1)];
			for (int m = 0; m < array2.GetLength(0); m++)
			{
				for (int n = 0; n < array2.GetLength(1); n++)
				{
					array2[m, n] = -1;
				}
			}
			for (int num6 = 0; num6 < list.Count; num6++)
			{
				foreach (Point current in list[num6])
				{
					array2[current.X, current.Y] = num6;
				}
			}
			for (int num7 = 0; num7 < list.Count; num7++)
			{
				foreach (Point item in list[num7])
				{
					int x = item.X;
					int y = item.Y;
					if (array2[x, y] == -1)
					{
						break;
					}
					int index = array2[x, y];
					if (x > 0)
					{
						AttemptClaim(x - 1, y, array2, list, index);
					}
					if (x < array2.GetLength(0) - 1)
					{
						AttemptClaim(x + 1, y, array2, list, index);
					}
					if (y > 0)
					{
						AttemptClaim(x, y - 1, array2, list, index);
					}
					if (y < array2.GetLength(1) - 1)
					{
						AttemptClaim(x, y + 1, array2, list, index);
					}
				}
			}
			foreach (List<Point> item2 in list)
			{
				item2.Clear();
			}
			for (int num8 = 0; num8 < array2.GetLength(0); num8++)
			{
				for (int num9 = 0; num9 < array2.GetLength(1); num9++)
				{
					if (array2[num8, num9] != -1)
					{
						list[array2[num8, num9]].Add(new Point(num8, num9));
					}
				}
			}
			foreach (List<Point> current3 in list)
			{
				if (current3.Count < 4)
				{
					current3.Clear();
				}
			}
			foreach (List<Point> current4 in list)
			{
				Cluster cluster = new Cluster();
				if (current4.Count <= 0)
				{
					continue;
				}
				foreach (Point current5 in current4)
				{
					cluster.Add(new Hub((float)current5.X + (WorldGen.genRand.NextFloat() - 0.5f) * 0.5f, (float)current5.Y + (WorldGen.genRand.NextFloat() - 0.5f) * 0.5f));
				}
				Add(cluster);
			}
		}
	}

	private static bool foundValidPosition;

	private static void PlaceClusters(ClusterGroup clusters, Point start, Vector2 terrainApplicationScaleVector)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		int totalWidth = (int)(terrainApplicationScaleVector.X * (float)clusters.Width);
		int totalHeight = (int)(terrainApplicationScaleVector.Y * (float)clusters.Height);
		Vector2 totalScale = default(Vector2);
		((Vector2)(ref totalScale))._002Ector((float)totalWidth, (float)totalHeight);
		Vector2 individualScale = default(Vector2);
		((Vector2)(ref individualScale))._002Ector((float)clusters.Width, (float)clusters.Height);
		for (int i = -20; i < totalWidth + 20; i++)
		{
			for (int j = -20; j < totalHeight + 20; j++)
			{
				float num3 = 0f;
				int clusterAmt = -1;
				float num5 = 0f;
				int num6 = i + start.X;
				int num7 = j + start.Y;
				Vector2 vector = new Vector2((float)i, (float)j) / totalScale * individualScale;
				Vector2 val = new Vector2((float)i, (float)j) / totalScale * 2f - Vector2.One;
				float num8 = ((Vector2)(ref val)).Length();
				for (int k = 0; k < clusters.Count; k++)
				{
					Cluster cluster = clusters[k];
					if (!(Math.Abs(cluster[0].Position.X - vector.X) <= 10f) || !(Math.Abs(cluster[0].Position.Y - vector.Y) <= 10f))
					{
						continue;
					}
					float num9 = 0f;
					foreach (Hub item in cluster)
					{
						num9 += 1f / Vector2.DistanceSquared(item.Position, vector);
					}
					if (num9 > num3)
					{
						if (num3 > num5)
						{
							num5 = num3;
						}
						num3 = num9;
						clusterAmt = k;
					}
					else if (num9 > num5)
					{
						num5 = num9;
					}
				}
				float num10 = num3 + num5;
				Tile tile = Main.tile[num6, num7];
				bool flag = num8 >= 0.8f;
				if (num10 > 4.5f)
				{
					tile.ClearEverything();
					tile.LiquidAmount = 192;
					if (clusterAmt % 5 == 2)
					{
						tile.ResetToType((ushort)ModContent.TileType<EutrophicSand>());
						tile.Get<TileWallWireStateData>().HasTile = true;
						tile.LiquidAmount = 0;
					}
					Tile.SmoothSlope(num6, num7);
				}
				else if (num10 > 1.8f)
				{
					tile.WallType = (ushort)ModContent.WallType<NavystoneWall>();
					tile.LiquidAmount = 192;
					if (!flag || tile.HasTile)
					{
						tile.ResetToType((ushort)ModContent.TileType<EutrophicSand>());
						tile.Get<TileWallWireStateData>().HasTile = true;
						Tile.SmoothSlope(num6, num7);
						tile.LiquidAmount = 0;
					}
				}
				else if (num10 > 0.7f || !flag)
				{
					tile.LiquidAmount = 192;
					if (!flag || tile.HasTile)
					{
						tile.ResetToType((ushort)ModContent.TileType<EutrophicSand>());
						tile.Get<TileWallWireStateData>().HasTile = true;
						Tile.SmoothSlope(num6, num7);
						tile.LiquidAmount = 0;
					}
					tile.WallType = (ushort)ModContent.WallType<EutrophicSandWall>();
				}
				else
				{
					if (!(num10 > 0.25f))
					{
						continue;
					}
					float num11 = (num10 - 0.25f) / 0.45f;
					if (WorldGen.genRand.NextFloat() < num11)
					{
						if (tile.HasTile)
						{
							tile.ResetToType((ushort)ModContent.TileType<EutrophicSand>());
							tile.Get<TileWallWireStateData>().HasTile = true;
							Tile.SmoothSlope(num6, num7);
							tile.WallType = (ushort)ModContent.WallType<EutrophicSandWall>();
							tile.LiquidAmount = 0;
						}
						else
						{
							tile.WallType = (ushort)ModContent.WallType<NavystoneWall>();
							tile.LiquidAmount = 192;
						}
					}
				}
			}
		}
		for (int l = -20; l < totalWidth + 20; l++)
		{
			for (int m = -20; m < totalHeight + 20; m++)
			{
				int x = l + start.X;
				int y = m + start.Y;
				Tile tile2 = Main.tile[x, y];
				Tile tileUp = Main.tile[x, y - 1];
				Tile tileDown = Main.tile[x, y + 1];
				_ = Main.tile[x, y + 2];
				Tile tileLeft = Main.tile[x - 1, y];
				Tile tileRight = Main.tile[x + 1, y];
				if (tile2.WallType == ModContent.WallType<NavystoneWall>() || tile2.WallType == ModContent.WallType<EutrophicSandWall>())
				{
					if (tile2.LiquidType == 1 && tile2.LiquidAmount > 0)
					{
						tile2.LiquidType = 0;
						tile2.LiquidAmount = byte.MaxValue;
					}
					if (tile2.TileType == 56)
					{
						WorldGen.KillTile(x, y);
					}
				}
				if (WorldGen.genRand.NextBool(1000) && tile2 != null && tile2.HasTile && (tile2.TileType == ModContent.TileType<Navystone>() || tile2.TileType == ModContent.TileType<EutrophicSand>()))
				{
					new TileRunner(new Vector2((float)x, (float)y), new Vector2(0f, 5f), new Point16(-35, 35), new Point16(-35, 35), 15.0, WorldGen.genRand.Next(25, 50), 0, addTile: false, overRide: true).Start();
				}
				if ((tile2.TileType == ModContent.TileType<Navystone>() || tile2.TileType == ModContent.TileType<EutrophicSand>()) && !tileUp.HasTile && !tileDown.HasTile && !tileLeft.HasTile && !tileRight.HasTile)
				{
					WorldGen.KillTile(x, y);
				}
				if (tile2.TileType == ModContent.TileType<Navystone>() && tileUp.TileType != ModContent.TileType<EutrophicSand>() && tileDown.TileType != ModContent.TileType<EutrophicSand>() && tileLeft.TileType != ModContent.TileType<EutrophicSand>() && tileRight.TileType != ModContent.TileType<EutrophicSand>())
				{
					WorldGen.KillTile(x, y);
				}
			}
		}
	}

	private static void AddGeodes(ClusterGroup clusters, Point start, Vector2 terrainApplicationScaleVector, float overallBiomeScale)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		int totalClusterZoneWidth = (int)(terrainApplicationScaleVector.X * (float)clusters.Width);
		int totalClusterZoneHeight = (int)(terrainApplicationScaleVector.Y * (float)clusters.Height);
		bool genCentalHole = true;
		Rectangle rectangle = default(Rectangle);
		int radius = (int)((float)WorldGen.genRand.Next(24, 28) * overallBiomeScale);
		int diameter = radius * 2;
		Point point = default(Point);
		((Point)(ref point))._002Ector(start.X + totalClusterZoneWidth / 2, start.Y + (int)((float)totalClusterZoneHeight * 0.33f));
		ShapeData holeShape = new ShapeData();
		float outerRadiusPercentage = (float)WorldGen.genRand.Next(40, 56) * 0.01f;
		int sunkenSeaBottom = start.Y + (int)((float)totalClusterZoneHeight * 0.7f);
		int smallHoles = 0;
		int totalHoleCount = (int)(4f * overallBiomeScale);
		for (int i = -20; i < totalClusterZoneWidth + 20; i++)
		{
			for (int j = -20; j < totalClusterZoneHeight + 20; j++)
			{
				if (genCentalHole)
				{
					genCentalHole = false;
					((Rectangle)(ref rectangle))._002Ector(start.X + totalClusterZoneWidth / 2 - radius, start.Y + (int)((float)totalClusterZoneHeight * 0.33f) - radius, diameter, diameter);
					WorldUtils.Gen(point, new Shapes.Circle(radius), Actions.Chain(new Modifiers.Blotches(2, 0.6).Output(holeShape), new Actions.ClearTile(frameNeighbors: true), new Actions.SetLiquid(0, 192)));
					WorldUtils.Gen(point, new ModShapes.OuterOutline(holeShape, useDiagonals: true, useInterior: true), Actions.Chain(new CustomActions.DistanceFromOrigin(greater: true, (float)radius * 16f - 48f), new Modifiers.Conditions(new CustomConditions.RandomChance(4f)), new Actions.Smooth(), new Actions.SetFrames(frameNeighbors: true)));
					WorldUtils.Gen(point, new Shapes.Circle((int)((float)radius * outerRadiusPercentage)), Actions.Chain(new Modifiers.Blotches().Output(holeShape), new Actions.SetTile((ushort)ModContent.TileType<Navystone>(), setSelfFrames: true)));
					WorldUtils.Gen(point, new ModShapes.OuterOutline(holeShape, useDiagonals: true, useInterior: true), Actions.Chain(new CustomActions.DistanceFromOrigin(greater: true, (float)radius * 16f - 48f), new Modifiers.Conditions(new CustomConditions.RandomChance(3f)), new Actions.Smooth(), new Actions.SetFrames(frameNeighbors: true)));
					WorldUtils.Gen(point, new Shapes.Circle((int)((float)radius * (outerRadiusPercentage * 0.6f))), Actions.Chain(new Modifiers.Blotches(), new Actions.SetTile((ushort)ModContent.TileType<SeaPrism>(), setSelfFrames: true)));
					WorldUtils.Gen(point, new Shapes.Circle((int)((float)radius * (outerRadiusPercentage * 0.3f))), Actions.Chain(new Modifiers.Blotches().Output(holeShape), new Actions.ClearTile(frameNeighbors: true), new Actions.SetLiquid()));
					WorldUtils.Gen(point, new ModShapes.OuterOutline(holeShape, useDiagonals: true, useInterior: true), Actions.Chain(new CustomActions.DistanceFromOrigin(greater: true, (float)radius * 16f - 48f), new Modifiers.Conditions(new CustomConditions.RandomChance(2f)), new Actions.Smooth(), new Actions.SetFrames(frameNeighbors: true)));
				}
				int smallHoleX = WorldGen.genRand.Next(start.X + 30, start.X + totalClusterZoneWidth - 30);
				int smallHoleY = WorldGen.genRand.Next(start.Y + 20, sunkenSeaBottom);
				((Point)(ref point))._002Ector(smallHoleX, smallHoleY);
				if (smallHoles < totalHoleCount && WorldGen.genRand.NextBool(3) && !((Rectangle)(ref rectangle)).Contains(point))
				{
					smallHoles++;
					int radiusSmall = (int)((float)WorldGen.genRand.Next(8, 11) * overallBiomeScale);
					WorldUtils.Gen(point, new Shapes.Circle(radiusSmall), Actions.Chain(new Modifiers.Blotches(2, 0.45).Output(holeShape), new Actions.ClearTile(frameNeighbors: true), new Actions.SetLiquid(0, 192)));
					WorldUtils.Gen(point, new ModShapes.OuterOutline(holeShape, useDiagonals: true, useInterior: true), Actions.Chain(new CustomActions.DistanceFromOrigin(greater: true, (float)radiusSmall * 16f - 48f), new Modifiers.Conditions(new CustomConditions.RandomChance(3f)), new Actions.Smooth(), new Actions.SetFrames(frameNeighbors: true)));
					outerRadiusPercentage = (float)((double)WorldGen.genRand.Next(65, 81) * 0.01);
					WorldUtils.Gen(point, new Shapes.Circle((int)((float)radiusSmall * outerRadiusPercentage)), Actions.Chain(new Modifiers.Blotches().Output(holeShape), new Actions.SetTile((ushort)ModContent.TileType<Navystone>(), setSelfFrames: true)));
					WorldUtils.Gen(point, new ModShapes.OuterOutline(holeShape, useDiagonals: true, useInterior: true), Actions.Chain(new CustomActions.DistanceFromOrigin(greater: true, (float)radiusSmall * 16f - 48f), new Modifiers.Conditions(new CustomConditions.RandomChance(3f)), new Actions.Smooth(), new Actions.SetFrames(frameNeighbors: true)));
					WorldUtils.Gen(point, new Shapes.Circle((int)((float)radiusSmall * (outerRadiusPercentage * 0.6f))), Actions.Chain(new Modifiers.Blotches(), new Actions.SetTile((ushort)ModContent.TileType<SeaPrism>(), setSelfFrames: true)));
					WorldUtils.Gen(point, new Shapes.Circle((int)((float)radiusSmall * (outerRadiusPercentage * 0.3f))), Actions.Chain(new Modifiers.Blotches().Output(holeShape), new Actions.ClearTile(frameNeighbors: true), new Actions.SetLiquid()));
					WorldUtils.Gen(point, new ModShapes.OuterOutline(holeShape, useDiagonals: true, useInterior: true), Actions.Chain(new CustomActions.DistanceFromOrigin(greater: true, (float)radius * 16f - 48f), new Modifiers.Conditions(new CustomConditions.RandomChance(2f)), new Actions.Smooth(), new Actions.SetFrames(frameNeighbors: true)));
				}
				int num3 = i + start.X;
				int num4 = j + start.Y;
				Tile tile = Main.tile[num3, num4];
				Tile testTile = Main.tile[num3, num4 + 1];
				Tile testTile2 = Main.tile[num3, num4 + 2];
				if (tile.TileType == ModContent.TileType<EutrophicSand>() && (!WorldGen.SolidTile(testTile) || !WorldGen.SolidTile(testTile2)))
				{
					tile.TileType = (ushort)ModContent.TileType<Navystone>();
				}
			}
		}
	}

	private static void AddTileVariance(ClusterGroup clusters, Point start, Vector2 terrainApplicationScaleVector, float overallBiomeScale)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		int totalClusterZoneWidth = (int)(terrainApplicationScaleVector.X * (float)clusters.Width);
		int totalClusterZoneHeight = (int)(terrainApplicationScaleVector.Y * (float)clusters.Height);
		for (int k = -20; k < totalClusterZoneWidth + 20; k++)
		{
			for (int l = -20; l < totalClusterZoneHeight + 20; l++)
			{
				int num5 = k + start.X;
				int num6 = l + start.Y;
				Tile tile = Main.tile[num5, num6];
				if (tile.HasTile && (tile.TileType == ModContent.TileType<SeaPrism>() || tile.TileType == ModContent.TileType<Navystone>() || tile.TileType == ModContent.TileType<EutrophicSand>()))
				{
					bool flag = true;
					for (int m = -1; m >= -3; m--)
					{
						if (Main.tile[num5, num6 + m].HasTile)
						{
							flag = false;
							break;
						}
					}
					bool flag2 = true;
					for (int n = 1; n <= 3; n++)
					{
						if (Main.tile[num5, num6 + n].HasTile)
						{
							flag2 = false;
							break;
						}
					}
					bool flag3 = true;
					for (int o = -1; o >= -3; o--)
					{
						if (Main.tile[num5 + o, num6].HasTile)
						{
							flag3 = false;
							break;
						}
					}
					bool flag4 = true;
					for (int p = 1; p <= 3; p++)
					{
						if (Main.tile[num5 + p, num6].HasTile)
						{
							flag4 = false;
							break;
						}
					}
					if (tile.TileType == ModContent.TileType<SeaPrism>() || ((tile.TileType == ModContent.TileType<Navystone>() || tile.TileType == ModContent.TileType<EutrophicSand>()) && WorldGen.genRand.NextBool(8)))
					{
						if ((flag3 ^ flag4) && tile.Slope == SlopeType.Solid && !tile.IsHalfBlock)
						{
							Tile tile3 = Main.tile[num5 + ((!flag3) ? 1 : (-1)), num6];
							tile3.TileType = (ushort)ModContent.TileType<SeaPrismCrystals>();
							if (Main.tile[num5 - 1, num6].TileType == ModContent.TileType<SeaPrismCrystals>())
							{
								Main.tile[num5 - 1, num6].TileFrameY = 36;
							}
							else if (Main.tile[num5 + 1, num6].TileType == ModContent.TileType<SeaPrismCrystals>())
							{
								Main.tile[num5 + 1, num6].TileFrameY = 54;
							}
							tile3.TileFrameX = (short)(WorldGen.genRand.Next(18) * 18);
							tile3.Get<TileWallWireStateData>().HasTile = true;
						}
						if ((flag ^ flag2) && tile.Slope == SlopeType.Solid && !tile.IsHalfBlock)
						{
							Tile tile4 = Main.tile[num5, num6 + ((!flag) ? 1 : (-1))];
							tile4.TileType = (ushort)ModContent.TileType<SeaPrismCrystals>();
							if (Main.tile[num5, num6 - 1].TileType == ModContent.TileType<SeaPrismCrystals>())
							{
								Main.tile[num5, num6 - 1].TileFrameY = 0;
							}
							else if (Main.tile[num5, num6 + 1].TileType == ModContent.TileType<SeaPrismCrystals>())
							{
								Main.tile[num5, num6 + 1].TileFrameY = 18;
							}
							tile4.TileFrameX = (short)(WorldGen.genRand.Next(18) * 18);
							tile4.Get<TileWallWireStateData>().HasTile = true;
						}
					}
				}
				if (tile.TileType == ModContent.TileType<Navystone>() || tile.TileType == ModContent.TileType<EutrophicSand>())
				{
					if (WorldGen.genRand.NextBool(10))
					{
						WorldGen.PlaceTile(num5, num6 - 1, (ushort)ModContent.TileType<BrainCoral>(), mute: true);
					}
					if (WorldGen.genRand.NextBool(20))
					{
						WorldGen.PlaceTile(num5, num6 - 1, (ushort)ModContent.TileType<SmallBrainCoral>(), mute: true);
					}
					if (WorldGen.genRand.NextBool(8))
					{
						WorldGen.PlaceTile(num5, num6 - 1, (ushort)ModContent.TileType<FanCoral>(), mute: true);
					}
					if (WorldGen.genRand.NextBool(30))
					{
						WorldGen.PlaceTile(num5, num6 - 1, (ushort)ModContent.TileType<TubeCoral>(), mute: true);
					}
					if (WorldGen.genRand.NextBool(30))
					{
						WorldGen.PlaceTile(num5, num6 - 1, (ushort)ModContent.TileType<SmallTubeCoral>(), mute: true);
					}
					if (WorldGen.genRand.NextBool(20))
					{
						WorldGen.PlaceTile(num5, num6 - 1, (ushort)ModContent.TileType<SeaAnemone>(), mute: true);
					}
					if (WorldGen.genRand.NextBool(20))
					{
						WorldGen.PlaceTile(num5, num6 - 1, (ushort)ModContent.TileType<MediumCoral>(), mute: true);
					}
					if (WorldGen.genRand.NextBool(20))
					{
						WorldGen.PlaceTile(num5, num6 - 1, (ushort)ModContent.TileType<MediumCoral2>(), mute: true);
					}
					if (WorldGen.genRand.NextBool(20))
					{
						WorldGen.PlaceTile(num5, num6 - 1, (ushort)ModContent.TileType<SmallWideCoral>(), mute: true);
					}
					if (WorldGen.genRand.NextBool(20))
					{
						WorldGen.PlaceTile(num5, num6 - 1, (ushort)ModContent.TileType<CoralPileLarge>(), mute: true);
					}
					if (WorldGen.genRand.NextBool(15))
					{
						WorldGen.PlaceObject(num5, num6 + 2, ModContent.TileType<SunkenStalactites>(), mute: false, WorldGen.genRand.Next(3));
					}
					if (WorldGen.genRand.NextBool(15))
					{
						WorldGen.PlaceTile(num5, num6 + 1, ModContent.TileType<SmallSunkenStalactites>(), mute: true, forced: false, -1, WorldGen.genRand.Next(3));
					}
					if (WorldGen.genRand.NextBool(10))
					{
						WorldGen.PlaceObject(num5, num6 - 2, ModContent.TileType<SunkenStalagmites>(), mute: false, WorldGen.genRand.Next(3));
					}
					if (WorldGen.genRand.NextBool(10))
					{
						WorldGen.PlaceTile(num5, num6 - 1, ModContent.TileType<SmallSunkenStalagmites>(), mute: true, forced: false, -1, WorldGen.genRand.Next(3));
					}
				}
				if (!tile.HasTile && (tile.TileType == ModContent.TileType<Navystone>() || tile.WallType == ModContent.WallType<EutrophicSandWall>()) && WorldGen.genRand.NextBool(10))
				{
					WorldGen.PlaceTile(num5, num6 + 1, (ushort)ModContent.TileType<TableCoral>(), mute: true);
				}
			}
		}
	}

	public static bool Place(Point origin)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		for (int y = origin.Y; y >= (int)Main.worldSurface; y--)
		{
			if (Main.tile[origin.X, y].WallType == 187 || Main.tile[origin.X, y].WallType == 216)
			{
				origin.Y = y + 50;
				foundValidPosition = true;
				break;
			}
		}
		if (foundValidPosition)
		{
			float scale = (float)Main.maxTilesX / 4200f;
			scale = MathHelper.Clamp(scale, 1f, 2f);
			int sunkenSeaAreaX = (int)(80f * scale);
			float baseVerticalSize = 60f * scale;
			int sunkenSeaAreaY = (int)((1.4f + WorldGen.genRand.NextFloat(0.3f)) * baseVerticalSize);
			Vector2 arbitrary42GodVector = default(Vector2);
			((Vector2)(ref arbitrary42GodVector))._002Ector(4f, 2f);
			int sunkenSeaRealWidth = (int)((float)sunkenSeaAreaX * arbitrary42GodVector.X);
			origin.X = ((Rectangle)(ref GenVars.UndergroundDesertLocation)).Center.X - sunkenSeaRealWidth / 2;
			ClusterGroup clusterGroup = new ClusterGroup();
			clusterGroup.Generate(sunkenSeaAreaX, sunkenSeaAreaY);
			PlaceClusters(clusterGroup, origin, arbitrary42GodVector);
			AddGeodes(clusterGroup, origin, arbitrary42GodVector, scale);
			int totalWidth = (int)(arbitrary42GodVector.X * (float)clusterGroup.Width);
			int totalHeight = (int)(arbitrary42GodVector.Y * (float)clusterGroup.Height);
			int frameExcessRadius = 40;
			for (int i = -frameExcessRadius; i < totalWidth + frameExcessRadius; i++)
			{
				for (int j = -frameExcessRadius; j < totalHeight + frameExcessRadius; j++)
				{
					if (i + origin.X > 0 && i + origin.X < Main.maxTilesX - 1 && j + origin.Y > 0 && j + origin.Y < Main.maxTilesY - 1)
					{
						WorldGen.SquareWallFrame(i + origin.X, j + origin.Y);
						WorldUtils.TileFrame(i + origin.X, j + origin.Y, frameNeighbors: true);
						Tile.SmoothSlope(i + origin.X, j + origin.Y);
					}
				}
			}
			AddTileVariance(clusterGroup, origin, arbitrary42GodVector, scale);
			return true;
		}
		return false;
	}
}
