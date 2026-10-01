using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;

namespace CalamityMod.World.Planets;

public class MudPlanet : Planetoid
{
	private int[] FocusLoot = new int[3] { 187, 268, 277 };

	private int[] PotionLoot = new int[6] { 2327, 289, 302, 299, 305, 2346 };

	private int[] BarLoot = new int[2]
	{
		(GenVars.copper == 7) ? 20 : 703,
		(GenVars.iron == 6) ? 22 : 704
	};

	public override bool Place(Point origin, StructureMap structures)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		int radius = GenBase._random.Next(12, 19);
		if (!CheckIfPlaceable(origin, radius, structures))
		{
			return false;
		}
		PlacePlanet(origin, radius);
		return base.Place(origin, structures);
	}

	public void PlacePlanet(Point origin, int radius)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		ShapeData shape = new ShapeData();
		GenAction blotchMod = new Modifiers.Blotches(2, 0.4);
		WorldUtils.Gen(origin, new Shapes.Circle(radius), Actions.Chain(blotchMod.Output(shape)));
		WorldUtils.Gen(origin, new ModShapes.All(shape), Actions.Chain(new Actions.PlaceTile(59), new Actions.PlaceWall(15)));
		int numStone = GenBase._random.Next(3, 9);
		for (int i = 0; i < numStone; i++)
		{
			int i2 = GenBase._random.Next(origin.X - radius + 3, origin.X + radius - 2);
			int y = GenBase._random.Next(origin.Y - radius + 3, origin.Y + radius - 2);
			WorldGen.TileRunner(i2, y, GenBase._random.NextFloat(5f, 9f), GenBase._random.Next(5, 15), (!Main.getGoodWorld) ? 1 : 232);
		}
		WorldUtils.Gen(origin, new ModShapes.InnerOutline(shape), Actions.Chain(new Actions.ClearWall(frameNeighbors: true), new Modifiers.OnlyTiles(59), new Modifiers.IsTouchingAir(useDiagonals: true), new Actions.SetTile(60), new Modifiers.Conditions(new CustomConditions.RandomChance(3f)), new Actions.Smooth(), new Actions.SetFrames(frameNeighbors: true)));
		if (GenBase._random.NextBool())
		{
			ShapeData hive = new ShapeData();
			int blobs = GenBase._random.Next(3, 6);
			float smallerRadius = (float)radius * 0.55f;
			int minSize = (int)((float)radius * 0.4f);
			int maxSize = (int)((float)radius * 0.6f);
			for (int k = 0; k < blobs; k++)
			{
				int moveX = GenBase._random.Next(-(int)smallerRadius, (int)smallerRadius);
				int moveY = GenBase._random.Next(-(int)smallerRadius, (int)smallerRadius);
				WorldUtils.Gen(origin, new Shapes.Circle(GenBase._random.Next(minSize, maxSize), GenBase._random.Next(minSize, maxSize)), Actions.Chain(new Modifiers.Offset(moveX, moveY), new Modifiers.Blotches(2, 0.12), new Actions.ClearTile(frameNeighbors: true).Output(hive), new Actions.PlaceWall(86)));
			}
			ShapeData hiveOutline = new ShapeData();
			WorldUtils.Gen(origin, new ModShapes.InnerOutline(hive), Actions.Chain(new Actions.PlaceTile(225), new Actions.ClearWall().Output(hiveOutline), new Modifiers.Conditions(new CustomConditions.IsNotTouchingAir(diagonals: false)), new Actions.PlaceWall(15)));
			hive.Subtract(hiveOutline, origin, origin);
			WorldUtils.Gen(origin, new ModShapes.InnerOutline(hive), new Actions.PlaceTile(225).Output(hiveOutline));
			hive.Subtract(hiveOutline, origin, origin);
			WorldUtils.Gen(origin, new ModShapes.All(hive), Actions.Chain(new Modifiers.Conditions(new CustomConditions.RandomChance(7f)), new Actions.SetLiquid(2)));
			bool placedChest = false;
			while (!placedChest)
			{
				int testX = origin.X + GenBase._random.Next(-radius, radius);
				int testY = origin.Y + GenBase._random.Next(-radius, radius);
				if (WorldGen.EmptyTileCheck(testX - 1, testX + 1, testY - 1, testY + 1) && GenBase._tiles[testX, testY].WallType == 86)
				{
					for (int floorX = testX - 1; floorX <= testX + 1; floorX++)
					{
						GenBase._tiles[floorX, testY + 2].Get<TileWallWireStateData>().HasTile = true;
						GenBase._tiles[floorX, testY + 2].TileType = 225;
						WorldGen.SquareTileFrame(floorX, testY + 2);
					}
					GiantHive.FillHoneyChest(WorldGen.PlaceChest(testX, testY + 1, 21, notNearOtherChests: false, 29), WorldGen.genRand);
					placedChest = true;
				}
			}
		}
		else if (GenBase._random.Next(4) <= 2)
		{
			ShapeData cavernData = new ShapeData();
			float waterChance = GenBase._random.NextFloat(2.2f, 3.5f);
			WorldUtils.Gen(origin, new Shapes.Circle((int)((float)radius * GenBase._random.NextFloat(0.35f, 0.5f))), Actions.Chain(new Modifiers.Blotches(), new Actions.ClearTile().Output(cavernData), new Modifiers.Conditions(new CustomConditions.RandomChance(waterChance)), new Actions.SetLiquid()));
			shape.Subtract(cavernData, origin, origin);
			int chestY;
			for (chestY = origin.Y; !GenBase._tiles[origin.X, chestY].HasTile; chestY++)
			{
			}
			for (int x = origin.X; x <= origin.X + 1; x++)
			{
				for (int j = chestY - 2; j <= chestY - 1; j++)
				{
					WorldGen.KillTile(x, j);
				}
			}
			GenBase._tiles[origin.X, chestY].Get<TileWallWireStateData>().HasTile = true;
			GenBase._tiles[origin.X, chestY].TileType = 59;
			GenBase._tiles[origin.X + 1, chestY].Get<TileWallWireStateData>().HasTile = true;
			GenBase._tiles[origin.X + 1, chestY].TileType = 59;
			int chestID = WorldGen.PlaceChest(origin.X, chestY - 1, 21, notNearOtherChests: false, 17);
			FillChest(chestID);
		}
		else
		{
			WorldUtils.Gen(origin, new ModShapes.All(shape), Actions.Chain(new Modifiers.OnlyTiles(60), new Actions.SetTile(70, setSelfFrames: true)));
		}
		WorldUtils.Gen(origin, new ModShapes.All(shape), Actions.Chain(new Modifiers.OnlyTiles(60, 70), new Modifiers.Offset(0, -1), new CustomActions.JungleGrass(mush: true), new Modifiers.Conditions(new CustomConditions.RandomChance(3.5f)), new Modifiers.Offset(0, 1), new CustomActions.PlaceTree()));
		WorldUtils.Gen(origin, new ModShapes.All(shape), Actions.Chain(new Modifiers.OnlyTiles(60), new Modifiers.Conditions(new CustomConditions.RandomChance(2f)), new Modifiers.Offset(0, 1), new ActionVines(3, 7, 62)));
	}

	private void FillChest(int id)
	{
		Chest chest = Main.chest[id];
		int index = 0;
		chest.item[index].SetDefaults(GenBase._random.Next(FocusLoot));
		chest.item[index++].Prefix(-1);
		if (GenBase._random.Next(3) <= 1)
		{
			chest.item[index].SetDefaults(GenBase._random.Next(BarLoot));
			chest.item[index++].stack = GenBase._random.Next(7, 15);
		}
		else
		{
			chest.item[index].SetDefaults(73);
			chest.item[index++].stack = GenBase._random.Next(2, 4);
		}
		if (GenBase._random.NextBool())
		{
			chest.item[index].SetDefaults(GenBase._random.Next(PotionLoot));
			chest.item[index++].stack = GenBase._random.Next(1, 4);
		}
		else
		{
			chest.item[index].SetDefaults(188);
			chest.item[index++].stack = GenBase._random.Next(3, 7);
		}
		if (GenBase._random.NextBool())
		{
			chest.item[index].SetDefaults(GenBase._random.Next(new int[2] { 42, 279 }));
			chest.item[index++].stack = GenBase._random.Next(50, 100);
		}
		else
		{
			chest.item[index].SetDefaults(GenBase._random.Next(new int[2] { 168, 166 }));
			chest.item[index++].stack = GenBase._random.Next(5, 10);
		}
		if (GenBase._random.NextBool())
		{
			chest.item[index].SetDefaults(2350);
			chest.item[index++].stack = GenBase._random.Next(1, 4);
		}
		else
		{
			chest.item[index].SetDefaults(282);
			chest.item[index++].stack = GenBase._random.Next(18, 36);
		}
	}
}
