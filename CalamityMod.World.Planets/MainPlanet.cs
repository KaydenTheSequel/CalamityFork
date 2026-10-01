using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.DataStructures;
using CalamityMod.Schematics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;

namespace CalamityMod.World.Planets;

public class MainPlanet : Planetoid
{
	public override bool Place(Point origin, StructureMap structures)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		int radius = 54;
		if (!CheckIfPlaceable(origin, radius, structures))
		{
			return false;
		}
		PlacePlanet(origin, radius);
		return base.Place(origin, structures);
	}

	public void PlacePlanet(Point origin, int radius)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		Circle planetoid = new Circle(origin.ToVector2() * 16f + new Vector2(8f), (float)radius * 16f);
		bool labLeftSide = GenBase._random.NextBool();
		int corridorLength = (int)((float)radius * GenBase._random.NextFloat(0.7f, 0.8f));
		ShapeData mainShape = new ShapeData();
		WorldUtils.Gen(origin, new Shapes.Circle(radius), Actions.Chain(new Modifiers.Blotches(2, 0.6).Output(mainShape), new Actions.SetTile(0, setSelfFrames: true), new Actions.PlaceWall(2)));
		float outerRadiusPercentage = GenBase._random.NextFloat(0.75f, 0.85f);
		Circle outerCore = new Circle(planetoid.Center, planetoid.Radius * outerRadiusPercentage);
		ShapeData outerCoreShape = new ShapeData();
		WorldUtils.Gen(origin, new Shapes.Circle((int)((float)radius * outerRadiusPercentage)), Actions.Chain(new Modifiers.Blotches(2, 0.45).Output(outerCoreShape), new Actions.SetTile((ushort)((!Main.getGoodWorld) ? 1 : 232), setSelfFrames: true), new Actions.ClearWall(), new Actions.PlaceWall(54)));
		for (int numStone = GenBase._random.Next(30, 40); numStone > 0; numStone--)
		{
			Point p = outerCore.RandomPointOnCircleEdge().ToTileCoordinates();
			WorldGen.TileRunner(p.X, p.Y, GenBase._random.NextFloat(3f, 6f), GenBase._random.Next(5, 15), (!Main.getGoodWorld) ? 1 : 232);
		}
		for (int numDirt = GenBase._random.Next(80, 110); numDirt > 0; numDirt--)
		{
			Point p2 = outerCore.RandomPointInCircle().ToTileCoordinates();
			WorldGen.TileRunner(p2.X, p2.Y, GenBase._random.NextFloat(3f, 6f), GenBase._random.Next(7, 17), 0);
		}
		int caveCount = GenBase._random.Next(8, 14);
		while (caveCount > 0)
		{
			caveCount--;
			Point tileCoords = planetoid.RandomPointInCircle().ToTileCoordinates();
			WorldGen.TileRunner(tileCoords.X, tileCoords.Y, GenBase._random.NextFloat(5f, 10f), GenBase._random.Next(64, 91), -1);
		}
		WorldGen.TileRunner(origin.X + (labLeftSide ? corridorLength : (-corridorLength)), origin.Y, GenBase._random.NextFloat(11f, 15f), GenBase._random.Next(3, 5), -1);
		mainShape.Subtract(outerCoreShape, origin, origin);
		bool hasPlacedLogAndSchematic = false;
		SchematicManager.PlaceSchematic<Action<Chest, int, bool>>("Planetoid Laboratory", new Point(origin.X, origin.Y), SchematicAnchor.Center, ref hasPlacedLogAndSchematic, DraedonStructures.FillPlanetoidLaboratoryChest);
		CalamityWorld.PlanetoidLabCenter = origin.ToWorldCoordinates();
		origin.ToWorldCoordinates();
		WorldUtils.Gen(origin, new ModShapes.OuterOutline(mainShape, useDiagonals: true, useInterior: true), Actions.Chain(new Modifiers.OnlyTiles(default(ushort)), new Modifiers.IsTouchingAir(useDiagonals: true), new Actions.SetTile(2), new CustomActions.DistanceFromOrigin(greater: true, (float)radius * 16f - 48f), new Actions.ClearWall(), new Actions.SetFrames(frameNeighbors: true), new Modifiers.Conditions(new CustomConditions.RandomChance(4f)), new Actions.Smooth(), new Actions.SetFrames(frameNeighbors: true)));
		WorldUtils.Gen(origin, new ModShapes.OuterOutline(mainShape, useDiagonals: true, useInterior: true), Actions.Chain(new Modifiers.OnlyTiles(2), new Modifiers.Offset(0, -1), new ActionGrass(), new Modifiers.Conditions(new CustomConditions.RandomChance(1.5f)), new Modifiers.Offset(0, 1), new CustomActions.PlaceTree()));
		WorldUtils.Gen(origin, new ModShapes.OuterOutline(mainShape, useDiagonals: true, useInterior: true), Actions.Chain(new Modifiers.OnlyTiles(2), new Modifiers.Conditions(new CustomConditions.RandomChance(2f)), new Modifiers.Offset(0, 1), new CustomActions.RandomFrom(new GenAction[2]
		{
			new ActionVines(7, 12),
			new ActionVines(11, 17, 382)
		}, new float[2] { 0.85f, 0.15f })));
	}

	public void PlaceBeams(ushort beamType, int x, int startY, Vector2 planetCenter, int radius, params ushort[] ignoreWalls)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		List<ushort> walls = ignoreWalls.ToList();
		while (true)
		{
			Tile tile = GenBase._tiles[x, startY];
			if (!tile.HasTile)
			{
				tile = GenBase._tiles[x, startY];
				if (!walls.Contains(tile.WallType))
				{
					tile = GenBase._tiles[x, startY];
					tile.Get<TileWallWireStateData>().HasTile = true;
					tile = GenBase._tiles[x, startY];
					tile.TileType = beamType;
					startY++;
					if (Vector2.Distance(planetCenter, new Vector2((float)(x * 16 + 8), (float)(startY * 16 + 8))) > (float)radius * 16f - 64f)
					{
						tile = GenBase._tiles[x, startY];
						tile.Get<TileWallWireStateData>().HasTile = true;
						tile = GenBase._tiles[x, startY];
						tile.TileType = (ushort)((!Main.getGoodWorld) ? 1 : 232);
					}
					continue;
				}
				break;
			}
			break;
		}
	}
}
