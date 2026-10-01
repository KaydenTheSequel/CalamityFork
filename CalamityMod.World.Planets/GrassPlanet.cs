using System;
using System.Collections.Generic;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;

namespace CalamityMod.World.Planets;

public class GrassPlanet : Planetoid
{
	private ushort[] oreTypes = new ushort[2]
	{
		(ushort)((GenVars.copper != 7) ? (Main.zenithWorld ? 107 : 7) : (Main.zenithWorld ? 221 : 166)),
		(ushort)((GenVars.iron != 6) ? (Main.zenithWorld ? 107 : 6) : (Main.zenithWorld ? 221 : 167))
	};

	public override bool Place(Point origin, StructureMap structures)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		int radius = GenBase._random.Next(16, 24);
		if (!CheckIfPlaceable(origin, radius, structures))
		{
			return false;
		}
		PlacePlanet(origin, radius, GenBase._random.Next(oreTypes));
		return base.Place(origin, structures);
	}

	public void PlacePlanet(Point origin, int radius, ushort oreType)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		Circle planetoid = new Circle(origin.ToVector2() * 16f + new Vector2(8f), (float)radius * 16f);
		ShapeData crust = new ShapeData();
		ShapeData core = new ShapeData();
		int outerRadius = (int)((float)radius * WorldGen.genRand.NextFloat(0.74f, 0.82f));
		GenAction blotchMod = new Modifiers.Blotches(2, 0.4);
		WorldUtils.Gen(origin, new Shapes.Circle(radius), Actions.Chain(blotchMod.Output(crust)));
		WorldUtils.Gen(origin, new Shapes.Circle(outerRadius), Actions.Chain(blotchMod.Output(core)));
		crust.Subtract(core, origin, origin);
		WorldUtils.Gen(origin, new ModShapes.All(core), Actions.Chain(new Actions.PlaceTile((ushort)((!Main.getGoodWorld) ? 1 : 232)), new Actions.PlaceWall(55)));
		WorldUtils.Gen(origin, new ModShapes.All(crust), Actions.Chain(new Actions.PlaceTile(0), new Actions.PlaceWall(2)));
		int randDirt = (int)((float)radius * 0.4f);
		int randStone = (int)((float)randDirt * 0.6f);
		for (int i = 0; i < randStone; i++)
		{
			int x = origin.X;
			int y = origin.Y;
			while (Vector2.Distance(origin.ToVector2(), new Vector2((float)x, (float)y)) < (float)outerRadius)
			{
				x = GenBase._random.Next(origin.X - radius, origin.X + radius + 1);
				y = GenBase._random.Next(origin.Y - radius, origin.Y + radius + 1);
			}
			WorldGen.TileRunner(x, y, GenBase._random.NextFloat(4.6f, 7.6f), GenBase._random.Next(7, 16), (!Main.getGoodWorld) ? 1 : 232);
		}
		for (int j = 0; j < randDirt; j++)
		{
			int i2 = GenBase._random.Next(origin.X - outerRadius, origin.X + outerRadius + 1);
			int y2 = GenBase._random.Next(origin.Y - outerRadius, origin.Y + outerRadius + 1);
			WorldGen.TileRunner(i2, y2, GenBase._random.NextFloat(5f, 8f), GenBase._random.Next(8, 18), 0);
		}
		int numStrokes = ((radius > 20) ? 3 : 2);
		for (int k = 0; k < numStrokes; k++)
		{
			Vector2 start = planetoid.RandomPointOnCircleEdge();
			Vector2 end = planetoid.Center - (start - planetoid.Center);
			Vector2 control = planetoid.RandomPointOnCircleEdge();
			float min = (float)radius * 0.6f * 16f;
			while (Vector2.Distance(control, start) < min || Vector2.Distance(control, end) < min)
			{
				control = planetoid.RandomPointOnCircleEdge();
			}
			BezierCurve bezierCurve = new BezierCurve(start, control, end);
			int strokeSteps = 50;
			double baseStrength = GenBase._random.NextFloat(1.2f, 2.4f);
			List<Vector2> tilePoints = bezierCurve.GetPoints(strokeSteps);
			for (int l = 0; l < strokeSteps; l++)
			{
				float progress = (float)l / (float)strokeSteps * (float)Math.PI;
				double strengthMultiplier = 1.0 + Math.Sin(progress) * baseStrength * 1.100000023841858;
				Vector2 val = tilePoints[l];
				int x2 = (int)(val.X / 16f);
				int y3 = (int)(val.Y / 16f);
				WorldGen.OreRunner(x2, y3, baseStrength * strengthMultiplier, 1, oreType);
			}
		}
		bool boffsFuntime2 = GenBase._random.NextBool(10);
		if (boffsFuntime2)
		{
			int topLayer;
			for (topLayer = origin.Y - radius - 4; topLayer < origin.Y; topLayer++)
			{
				for (int m = origin.X - radius; m < origin.X + radius; m++)
				{
					if (GenBase._tiles[m, topLayer].HasTile && Main.tileSolid[GenBase._tiles[m, topLayer].TileType])
					{
						goto end_IL_0455;
					}
				}
				continue;
				end_IL_0455:
				break;
			}
			ushort brickType = 273;
			int shrineDepth = GenBase._random.Next(8, 13);
			int startX = origin.X - 3;
			int endX = origin.X + 3;
			for (int n = topLayer; n <= topLayer + shrineDepth; n++)
			{
				for (int num = startX; num <= endX; num++)
				{
					GenBase._tiles[num, n].TileType = brickType;
					GenBase._tiles[num, n].Get<TileWallWireStateData>().HasTile = true;
					if (n == topLayer)
					{
						GenBase._tiles[num, n].WallType = 0;
					}
				}
				if ((n - topLayer + 1) % 2 == 0)
				{
					startX--;
					endX++;
				}
			}
			WorldGen.PlaceTile(origin.X, topLayer - 1, 187, mute: true, forced: false, -1, 17);
			WorldGen.PlaceTile(origin.X - 3, topLayer - 1, 93, mute: true, forced: false, -1, 14);
			WorldGen.PlaceTile(origin.X + 3, topLayer - 1, 93, mute: true, forced: false, -1, 14);
		}
		WorldUtils.Gen(origin, new ModShapes.InnerOutline(crust), Actions.Chain(new Modifiers.OnlyTiles(default(ushort)), new Modifiers.IsTouchingAir(useDiagonals: true), new Actions.SetTile(2), new Actions.ClearWall(), new Actions.SetFrames(frameNeighbors: true), new Modifiers.Conditions(new CustomConditions.RandomChance(4f)), new Actions.Smooth(), new Actions.SetFrames(frameNeighbors: true)));
		WorldUtils.Gen(origin, new ModShapes.InnerOutline(crust), Actions.Chain(new Modifiers.OnlyTiles((ushort)((!Main.getGoodWorld) ? 1 : 232), oreType), new Modifiers.IsTouchingAir(useDiagonals: true), new Modifiers.OnlyWalls(2), new Actions.ClearWall(frameNeighbors: true)));
		if (!boffsFuntime2)
		{
			int campX = origin.X;
			int campY;
			for (campY = origin.Y - radius - 3; !Main.tile[campX, campY].HasTile || !Main.tileSolid[GenBase._tiles[campX, campY].TileType]; campY++)
			{
			}
			campY--;
			int startCampX = campX;
			while ((!GenBase._tiles[startCampX, campY].HasTile || !Main.tileSolid[GenBase._tiles[startCampX, campY].TileType]) && GenBase._tiles[startCampX, campY + 1].HasTile)
			{
				startCampX--;
			}
			int endCampX;
			for (endCampX = campX; (!GenBase._tiles[endCampX, campY].HasTile || !Main.tileSolid[GenBase._tiles[endCampX, campY].TileType]) && GenBase._tiles[endCampX, campY + 1].HasTile; endCampX++)
			{
			}
			int distance = (int)MathHelper.Distance((float)(++startCampX), (float)(--endCampX));
			if (distance >= 8)
			{
				int extra = distance - 8;
				if (extra > 0)
				{
					extra = GenBase._random.Next(extra + 1);
				}
				if (GenBase._random.NextBool())
				{
					WorldGen.Place3x2(startCampX + 1 + extra, campY, 187, 26);
					WorldGen.Place3x2(startCampX + 5 + extra, campY, 215);
				}
				else
				{
					WorldGen.Place3x2(endCampX - 1 - extra, campY, 187, 26);
					WorldGen.Place3x2(endCampX - 5 - extra, campY, 215);
				}
			}
		}
		WorldUtils.Gen(origin, new ModShapes.All(crust), Actions.Chain(new Modifiers.OnlyTiles(2), new Modifiers.Offset(0, -1), new ActionGrass(), new Modifiers.Conditions(new CustomConditions.RandomChance(2.5f)), new Modifiers.Offset(0, 1), new CustomActions.PlaceTree()));
		WorldUtils.Gen(origin, new ModShapes.All(crust), Actions.Chain(new Modifiers.OnlyTiles(2), new Modifiers.Conditions(new CustomConditions.RandomChance(2f)), new Modifiers.Offset(0, 1), new CustomActions.RandomFrom(new GenAction[2]
		{
			new ActionVines(3, 7),
			new ActionVines(7, 12, 382)
		}, new float[2] { 0.85f, 0.15f })));
	}
}
