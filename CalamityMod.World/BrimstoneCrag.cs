using System;
using System.Collections.Generic;
using CalamityMod.Items.Fishing.BrimstoneCragCatches;
using CalamityMod.Items.Fishing.FishingRods;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Schematics;
using CalamityMod.Tiles.Crags;
using CalamityMod.Tiles.Crags.Lily;
using CalamityMod.Tiles.Crags.Spike;
using CalamityMod.Tiles.Crags.Tree;
using CalamityMod.Tiles.Ores;
using CalamityMod.Walls.UnsafeWalls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class BrimstoneCrag
{
	private static int StartX;

	private static int lavaLakeBigPlaceDelay;

	private static int numLavaLakes;

	private static void GenCrags()
	{
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_082a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_091c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0968: Unknown result type (might be due to invalid IL or missing references)
		StartX = ((GenVars.dungeonX < Main.maxTilesX / 2) ? 25 : (Main.maxTilesX - Main.maxTilesX / 5 - 25));
		int biomeStart = StartX;
		int biomeEdge = biomeStart + Main.maxTilesX / 5;
		int biomeMiddle = (biomeStart + biomeEdge) / 2;
		for (int x = biomeStart; x <= biomeEdge; x++)
		{
			for (int y = Main.UnderworldLayer; y < Main.maxTilesY - 2; y++)
			{
				Main.tile[x, y].ClearEverything();
				WorldGen.KillWall(x, y);
			}
		}
		for (int i = biomeStart; i <= biomeEdge; i++)
		{
			for (int j = Main.maxTilesY - 90; j <= Main.maxTilesY - 5; j++)
			{
				Tile tile = Main.tile[i, j];
				tile.TileType = (ushort)ModContent.TileType<BrimstoneSlag>();
				tile.HasTile = true;
				tile.WallType = (ushort)ModContent.WallType<UnsafeBrimstoneSlagWall>();
			}
		}
		for (int k = biomeStart; k <= biomeEdge; k++)
		{
			for (int l = Main.maxTilesY - 110; l <= Main.maxTilesY - 20; l++)
			{
				Tile tile2 = Main.tile[k, l];
				Tile tileUp = Main.tile[k, l - 1];
				Tile tileDown = Main.tile[k, l + 1];
				Tile tileLeft = Main.tile[k - 1, l];
				Tile tileRight = Main.tile[k + 1, l];
				if (tile2.TileType == ModContent.TileType<BrimstoneSlag>() && (tileUp.TileType != ModContent.TileType<BrimstoneSlag>() || tileDown.TileType != ModContent.TileType<BrimstoneSlag>() || tileLeft.TileType != ModContent.TileType<BrimstoneSlag>() || tileRight.TileType != ModContent.TileType<BrimstoneSlag>()))
				{
					ShapeData circle = new ShapeData();
					GenAction blotchMod = new Modifiers.Blotches(2, 0.4);
					int radius = WorldGen.genRand.Next(5, 20);
					WorldUtils.Gen(new Point(k, l), new Shapes.Circle(radius), Actions.Chain(blotchMod.Output(circle)));
					WorldUtils.Gen(new Point(k, l), new ModShapes.All(circle), Actions.Chain(new Actions.ClearTile(), new Actions.PlaceTile((ushort)ModContent.TileType<BrimstoneSlag>())));
				}
			}
		}
		for (int m = biomeStart; m <= biomeEdge; m++)
		{
			for (int n = Main.UnderworldLayer; n <= Main.maxTilesY - 192; n++)
			{
				if (WorldGen.genRand.NextBool(25))
				{
					ShapeData circle2 = new ShapeData();
					GenAction blotchMod2 = new Modifiers.Blotches(2, 0.4);
					int radius2 = WorldGen.genRand.Next(3, 7);
					WorldUtils.Gen(new Point(m, n), new Shapes.Circle(radius2), Actions.Chain(blotchMod2.Output(circle2)));
					WorldUtils.Gen(new Point(m, n), new ModShapes.All(circle2), Actions.Chain(new Actions.ClearTile(), new Actions.PlaceTile((ushort)ModContent.TileType<BrimstoneSlag>())));
				}
			}
		}
		for (int num = biomeStart; num <= biomeEdge; num++)
		{
			for (int num2 = Main.maxTilesY - 140; num2 <= Main.maxTilesY - 5; num2++)
			{
				Tile tile3 = Main.tile[num, num2];
				_ = Main.tile[num, num2 - 1];
				Tile tileDown2 = Main.tile[num, num2 + 1];
				Tile tileDown3 = Main.tile[num, num2 + 2];
				_ = Main.tile[num - 1, num2];
				_ = Main.tile[num + 1, num2];
				if (tile3.TileType == ModContent.TileType<BrimstoneSlag>() && !tileDown2.HasTile)
				{
					WorldGen.PlaceTile(num, num2 + 1, (ushort)ModContent.TileType<BrimstoneSlag>());
				}
				if (tile3.TileType == ModContent.TileType<ScorchedRemains>() && !tileDown2.HasTile)
				{
					WorldGen.PlaceTile(num, num2 + 1, (ushort)ModContent.TileType<ScorchedRemains>());
				}
				if (tile3.HasTile && tileDown2.HasTile && tileDown3.HasTile && Main.tile[num, num2 + 5].HasTile)
				{
					Main.tile[num, num2 + 5].WallType = (ushort)ModContent.WallType<UnsafeBrimstoneSlagWall>();
				}
				if (tile3.LiquidType == 0 && tile3.LiquidAmount > 0)
				{
					tile3.ClearEverything();
					tile3.LiquidType = 1;
					tile3.LiquidAmount = byte.MaxValue;
				}
			}
		}
		for (int num3 = biomeStart; num3 <= biomeEdge; num3++)
		{
			for (int num4 = Main.maxTilesY - 195; num4 <= Main.maxTilesY - 180; num4++)
			{
				Tile tile4 = Main.tile[num3, num4];
				Tile tileUp2 = Main.tile[num3, num4 - 1];
				if (tile4.TileType == ModContent.TileType<BrimstoneSlag>() && !tileUp2.HasTile)
				{
					WorldGen.PlaceTile(num3, num4 - 1, (ushort)ModContent.TileType<BrimstoneSlag>());
				}
			}
		}
		for (int num5 = biomeStart; num5 <= biomeEdge; num5++)
		{
			if (lavaLakeBigPlaceDelay > 0)
			{
				lavaLakeBigPlaceDelay--;
			}
			if (lavaLakeBigPlaceDelay == 0)
			{
				if (numLavaLakes != 2 && numLavaLakes != 3)
				{
					new LavaTileRunner(new Vector2((float)num5, (float)(Main.maxTilesY - 165)), new Vector2(0f, 5f), new Point16(-500, 500), new Point16(250, 1000), 15.0, WorldGen.genRand.Next(300, 1000), 0, addTile: true, overRide: true).Start();
				}
				numLavaLakes++;
				lavaLakeBigPlaceDelay = 200;
			}
		}
		for (int num6 = biomeStart; num6 <= biomeEdge; num6++)
		{
			for (int num7 = Main.maxTilesY - 100; num7 <= Main.maxTilesY - 20; num7++)
			{
				if (WorldGen.genRand.NextBool(1200))
				{
					new LavaTileRunner(new Vector2((float)num6, (float)num7), new Vector2(0f, 5f), new Point16(0, 5), new Point16(-12, 12), 15.0, WorldGen.genRand.Next(-12, 25), 0, addTile: true, overRide: true).Start();
				}
			}
		}
		for (int num8 = biomeStart + 30; num8 <= biomeEdge - 30; num8++)
		{
			if (WorldGen.genRand.NextBool(145))
			{
				ScorchedGrassPatches((object)new Point(num8, Main.maxTilesY - 135));
			}
		}
		for (int num9 = biomeStart; num9 <= biomeEdge; num9++)
		{
			for (int num10 = Main.maxTilesY - 150; num10 <= Main.maxTilesY - 45; num10++)
			{
				Tile tile5 = Main.tile[num9, num10];
				Tile tileUp3 = Main.tile[num9, num10 - 1];
				Tile tileDown4 = Main.tile[num9, num10 + 1];
				Tile tileLeft2 = Main.tile[num9 - 1, num10];
				Tile tileRight2 = Main.tile[num9 + 1, num10];
				if (WorldGen.genRand.NextBool(180) && tile5.TileType == ModContent.TileType<BrimstoneSlag>() && (tileUp3.LiquidAmount > 0 || tileDown4.LiquidAmount > 0 || tileLeft2.LiquidAmount > 0 || tileRight2.LiquidAmount > 0))
				{
					WorldGen.TileRunner(num9 + WorldGen.genRand.Next(-15, 15), num10 + WorldGen.genRand.Next(-15, 15), WorldGen.genRand.Next(10, 12), WorldGen.genRand.Next(10, 12), ModContent.TileType<InfernalSuevite>());
				}
			}
		}
		bool firstItem = false;
		SchematicManager.PlaceSchematic<Action<Chest, int, bool>>("Crags Bridge", new Point(biomeMiddle, Main.maxTilesY - 100), SchematicAnchor.Center, ref firstItem, FillBrimstoneChests);
		bool place = true;
		int house1Offset = WorldGen.genRand.Next(0, 55);
		PlaceSquareForCragHouses(biomeStart + 150 + house1Offset, Main.maxTilesY - 125);
		SchematicManager.PlaceSchematic<Action<Chest>>("Crag Ruin 3", new Point(biomeStart + 150 + house1Offset, Main.maxTilesY - 125), SchematicAnchor.BottomCenter, ref place);
		int house2Offset = WorldGen.genRand.Next(-55, 0);
		PlaceSquareForCragHouses(biomeMiddle - 235 + house2Offset, Main.maxTilesY - 125);
		SchematicManager.PlaceSchematic<Action<Chest>>("Crag Ruin 1", new Point(biomeMiddle - 235 + house2Offset, Main.maxTilesY - 125), SchematicAnchor.BottomCenter, ref place);
		int house3Offset = WorldGen.genRand.Next(0, 55);
		PlaceSquareForCragHouses(biomeMiddle + 235 + house3Offset, Main.maxTilesY - 125);
		SchematicManager.PlaceSchematic<Action<Chest>>("Crag Ruin 4", new Point(biomeMiddle + 235 + house3Offset, Main.maxTilesY - 125), SchematicAnchor.BottomCenter, ref place);
		int house4Offset = WorldGen.genRand.Next(-55, 0);
		PlaceSquareForCragHouses(biomeEdge - 150 + house4Offset, Main.maxTilesY - 125);
		SchematicManager.PlaceSchematic<Action<Chest>>("Crag Ruin 21", new Point(biomeEdge - 150 + house4Offset, Main.maxTilesY - 125), SchematicAnchor.BottomCenter, ref place);
		for (int num11 = biomeStart; num11 <= biomeEdge; num11++)
		{
			for (int num12 = Main.UnderworldLayer; num12 <= Main.maxTilesY - 5; num12++)
			{
				Tile tile6 = Main.tile[num11, num12];
				Tile tileAbove = Main.tile[num11, num12 - 1];
				if (tile6.LiquidAmount > 0)
				{
					tile6.LiquidType = 1;
					tile6.LiquidAmount = byte.MaxValue;
				}
				if (tile6.TileType != ModContent.TileType<ScorchedRemains>() || tileAbove.HasTile)
				{
					continue;
				}
				for (int num13 = num11 - 1; num13 <= num11 + 1; num13++)
				{
					for (int num14 = num12 - 5; num14 <= num12; num14++)
					{
						Tile lavaTile = Main.tile[num13, num14];
						Tile lavaTileDown = Main.tile[num13, num14 + 1];
						if (lavaTile.WallType == 0 && lavaTileDown.WallType == 0)
						{
							lavaTile.LiquidAmount = 0;
						}
					}
				}
			}
		}
		for (int num15 = biomeStart; num15 <= biomeEdge; num15++)
		{
			for (int num16 = Main.maxTilesY - 150; num16 <= Main.maxTilesY - 122; num16++)
			{
				Main.tile[num15, num16].LiquidAmount = 0;
			}
		}
		CalamityUtils.SettleWater(convertToLava: false);
		for (int num17 = biomeStart; num17 <= biomeEdge; num17++)
		{
			for (int num18 = Main.UnderworldLayer; num18 <= Main.maxTilesY - 110; num18++)
			{
				Tile tile7 = Main.tile[num17, num18];
				Tile tileUp4 = Main.tile[num17, num18 - 1];
				if (tile7.TileType == ModContent.TileType<ScorchedRemains>() && !tileUp4.HasTile && tileUp4.LiquidAmount == 0)
				{
					tile7.TileType = (ushort)ModContent.TileType<ScorchedRemainsGrass>();
				}
			}
		}
		for (int num19 = biomeStart; num19 <= biomeEdge; num19++)
		{
			for (int num20 = Main.UnderworldLayer; num20 <= Main.maxTilesY - 5; num20++)
			{
				Tile tile8 = Main.tile[num19, num20];
				Tile tileUp5 = Main.tile[num19, num20 - 1];
				Tile tileDown5 = Main.tile[num19, num20 + 1];
				Tile tileLeft3 = Main.tile[num19 - 1, num20];
				Tile tileRight3 = Main.tile[num19 + 1, num20];
				if (tile8.TileType == ModContent.TileType<BrimstoneSlag>() || tile8.TileType == ModContent.TileType<ScorchedRemains>() || tile8.TileType == ModContent.TileType<ScorchedRemainsGrass>())
				{
					Tile.SmoothSlope(num19, num20);
					if (!tileUp5.HasTile && !tileDown5.HasTile && !tileLeft3.HasTile && !tileRight3.HasTile)
					{
						WorldGen.KillTile(num19, num20);
					}
				}
			}
		}
	}

	private static void GenCragsAmbience()
	{
		int startX = StartX;
		int biomeEdge = startX + Main.maxTilesX / 5;
		_ = (startX + biomeEdge) / 2;
		for (int x = startX; x <= biomeEdge; x++)
		{
			for (int y = Main.UnderworldLayer; y <= Main.maxTilesY - 5; y++)
			{
				Tile tile = Main.tile[x, y];
				if (tile.TileType == ModContent.TileType<BrimstoneSlag>())
				{
					if (WorldGen.genRand.NextBool(20))
					{
						ushort[] Stalactites = new ushort[3]
						{
							(ushort)ModContent.TileType<CragStalactiteGiant1>(),
							(ushort)ModContent.TileType<CragStalactiteGiant2>(),
							(ushort)ModContent.TileType<CragStalactiteGiant3>()
						};
						WorldGen.PlaceObject(x, y + 2, WorldGen.genRand.Next(Stalactites));
					}
					if (WorldGen.genRand.NextBool(8))
					{
						ushort[] Stalactites2 = new ushort[6]
						{
							(ushort)ModContent.TileType<CragStalactiteLarge1>(),
							(ushort)ModContent.TileType<CragStalactiteLarge2>(),
							(ushort)ModContent.TileType<CragStalactiteLarge3>(),
							(ushort)ModContent.TileType<CragStalactiteSmall1>(),
							(ushort)ModContent.TileType<CragStalactiteSmall2>(),
							(ushort)ModContent.TileType<CragStalactiteSmall3>()
						};
						WorldGen.PlaceObject(x, y + 2, WorldGen.genRand.Next(Stalactites2));
					}
					if (WorldGen.genRand.NextBool(25))
					{
						ushort[] Stalagmites = new ushort[3]
						{
							(ushort)ModContent.TileType<CragStalagmiteGiant1>(),
							(ushort)ModContent.TileType<CragStalagmiteGiant2>(),
							(ushort)ModContent.TileType<CragStalagmiteGiant3>()
						};
						WorldGen.PlaceObject(x, y - 1, WorldGen.genRand.Next(Stalagmites));
					}
					if (WorldGen.genRand.NextBool(8))
					{
						ushort[] Stalagmites2 = new ushort[6]
						{
							(ushort)ModContent.TileType<CragStalagmiteLarge1>(),
							(ushort)ModContent.TileType<CragStalagmiteLarge2>(),
							(ushort)ModContent.TileType<CragStalagmiteLarge3>(),
							(ushort)ModContent.TileType<CragStalagmiteSmall1>(),
							(ushort)ModContent.TileType<CragStalagmiteSmall2>(),
							(ushort)ModContent.TileType<CragStalagmiteSmall3>()
						};
						WorldGen.PlaceObject(x, y - 1, WorldGen.genRand.Next(Stalagmites2));
					}
				}
				if (tile.TileType == ModContent.TileType<ScorchedRemainsGrass>())
				{
					ushort[] Lillies = new ushort[6]
					{
						(ushort)ModContent.TileType<LavaLily1>(),
						(ushort)ModContent.TileType<LavaLily2>(),
						(ushort)ModContent.TileType<LavaLily3>(),
						(ushort)ModContent.TileType<LavaLily4>(),
						(ushort)ModContent.TileType<LavaLily5>(),
						(ushort)ModContent.TileType<LavaLily6>()
					};
					PlaceCragLily(x, y - 1, WorldGen.genRand.Next(Lillies));
				}
			}
			for (int i = Main.maxTilesY - 150; i <= Main.maxTilesY - 122; i++)
			{
				Tile tile2 = Main.tile[x, i];
				Tile.SmoothSlope(x, i);
				if (tile2.TileType == ModContent.TileType<BrimstoneSlag>() && WorldGen.genRand.NextBool(8) && !tile2.LeftSlope && !tile2.RightSlope && !tile2.IsHalfBlock)
				{
					PlaceTree(x, i - 1, ModContent.TileType<SpineTree>());
				}
			}
		}
	}

	public static void PlaceSquareForCragHouses(int x, int y)
	{
		for (int i = x - 25; i <= x + 25; i++)
		{
			for (int j = y; j <= y + 15; j++)
			{
				WorldGen.PlaceTile(i, j, ModContent.TileType<BrimstoneSlag>());
			}
		}
	}

	public static bool PlaceTree(int x, int y, int tileType)
	{
		int minDistance = 5;
		int treeNearby = 0;
		for (int i = x - minDistance; i < x + minDistance; i++)
		{
			for (int j = y - minDistance; j < y + minDistance; j++)
			{
				if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == tileType)
				{
					treeNearby++;
					if (treeNearby > 0)
					{
						return false;
					}
				}
			}
		}
		SpineTree.Spawn(x, y, 22, 28);
		return true;
	}

	public static bool PlaceCragLily(int x, int y, int tileType)
	{
		int minDistance = 15;
		int lilyNearby = 0;
		for (int i = x - minDistance; i < x + minDistance; i++)
		{
			for (int j = y - minDistance; j < y + minDistance; j++)
			{
				if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == tileType)
				{
					lilyNearby++;
					if (lilyNearby > 0)
					{
						return false;
					}
				}
			}
		}
		for (int upChechY = y - 10; upChechY < y; upChechY++)
		{
			if (Main.tile[x, upChechY].HasTile)
			{
				return false;
			}
		}
		WorldGen.PlaceObject(x, y, tileType);
		return true;
	}

	public static void FillBrimstoneChests(Chest chest, int Type, bool firstItem)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[4] { 288, 300, 2348, 4870 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(175, WorldGen.genRand.Next(4, 6)),
			new ChestItem(ModContent.ItemType<CoastalDemonfish>(), WorldGen.genRand.Next(2, 5)),
			new ChestItem(265, WorldGen.genRand.Next(25, 50)),
			new ChestItem(potionType, WorldGen.genRand.Next(1, 3)),
			new ChestItem(73, WorldGen.genRand.Next(2, 12))
		};
		if (!firstItem)
		{
			contents.Insert(0, new ChestItem(907, 1));
		}
		else
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<SlurperPole>(), 1));
			contents.Insert(0, new ChestItem(ModContent.ItemType<SlagfireDouser>(), 1));
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void ScorchedGrassPatches(object obj)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		Point origin = (Point)obj;
		Vector2 center = origin.ToVector2() * 16f + new Vector2(8f);
		float angle = 0.47123894f;
		float otherAngle = (float)Math.PI / 2f - angle;
		int distanceInTiles = WorldGen.genRand.Next(30, 45);
		float num = (float)distanceInTiles * 16f;
		float constant = num * 2f / (float)Math.Sin(angle);
		float fociSpacing = num * (float)Math.Sin(otherAngle) / (float)Math.Sin(angle);
		int verticalRadius = (int)(constant / 16f);
		Vector2 fociOffset = Vector2.UnitY * fociSpacing;
		Vector2 topFoci = center - fociOffset;
		Vector2 bottomFoci = center + fociOffset;
		UnifiedRandom rand = WorldGen.genRand;
		for (int x = origin.X - distanceInTiles - 2; x <= origin.X + distanceInTiles + 2; x++)
		{
			for (int y = (int)((float)origin.Y - (float)verticalRadius * 0.4f) - 3; y <= origin.Y + verticalRadius + 3; y++)
			{
				if (!CheckInEllipse(new Point(x, y), topFoci, bottomFoci, constant, center, out var dist, y < origin.Y))
				{
					continue;
				}
				float percent = dist / constant;
				float blurPercent = 0.98f;
				if (percent > blurPercent)
				{
					float outerEdgePercent = (percent - blurPercent) / (1f - blurPercent);
					if (rand.NextFloat(1f) > outerEdgePercent && Main.tile[x, y].HasTile && WorldGen.InWorld(x, y))
					{
						Main.tile[x, y].TileType = (ushort)ModContent.TileType<ScorchedRemains>();
					}
				}
				else if (Main.tile[x, y].HasTile && WorldGen.InWorld(x, y))
				{
					Main.tile[x, y].TileType = (ushort)ModContent.TileType<ScorchedRemains>();
				}
			}
		}
	}

	public static bool CheckInEllipse(Point tile, Vector2 focus1, Vector2 focus2, float distanceConstant, Vector2 center, out float distance, bool collapse = false)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		Vector2 point = tile.ToWorldCoordinates();
		if (collapse)
		{
			float distY = center.Y - point.Y;
			point.Y -= distY * 8f;
		}
		float distance2 = Vector2.Distance(point, focus1);
		float distance3 = Vector2.Distance(point, focus2);
		distance = distance2 + distance3;
		return distance <= distanceConstant;
	}

	public static void GenAllCragsStuff()
	{
		GenCrags();
		GenCragsAmbience();
	}
}
