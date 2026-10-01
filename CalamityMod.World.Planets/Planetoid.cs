using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace CalamityMod.World.Planets;

public class Planetoid : MicroBiome
{
	private Rectangle _area;

	public static void GenerateAllBasePlanetoids(GenerationProgress progress, GameConfiguration config)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		progress.Message = CalamityUtils.GetTextValue("UI.Planetoids");
		WorldGenConfiguration config2 = WorldGenConfiguration.FromEmbeddedPath("Terraria.GameContent.WorldBuilding.Configuration.json");
		int GrassPlanetoidCount = Main.maxTilesX / 750;
		int LCPlanetoidCount = Main.maxTilesX / 1500;
		int MudPlanetoidCount = Main.maxTilesX / 1000;
		int i;
		for (i = 0; i < 3000; i++)
		{
			if (config2.CreateBiome<MainPlanet>().Place(new Point(WorldGen.genRand.Next(Main.maxTilesX / 2 - 300, Main.maxTilesX / 2 + 300), WorldGen.genRand.Next(128, 134)), GenVars.structures))
			{
				break;
			}
		}
		i = 0;
		while (LCPlanetoidCount > 0 && i < 15000)
		{
			int x = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.15), (int)((double)Main.maxTilesX * 0.85));
			int y = WorldGen.genRand.Next(70, 101);
			if (config2.CreateBiome<HeartPlanet>().Place(new Point(x, y), GenVars.structures))
			{
				LCPlanetoidCount--;
			}
			i++;
		}
		i = 0;
		while (GrassPlanetoidCount > 0 && i < 12000)
		{
			int x2 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.25), (int)((double)Main.maxTilesX * 0.75));
			int y2 = WorldGen.genRand.Next(100, 131);
			if (config2.CreateBiome<GrassPlanet>().Place(new Point(x2, y2), GenVars.structures))
			{
				GrassPlanetoidCount--;
			}
			i++;
		}
		i = 0;
		while (MudPlanetoidCount > 0 && i < 12000)
		{
			int x3 = WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.25f), (int)((float)Main.maxTilesX * 0.75f));
			int y3 = WorldGen.genRand.Next(100, 131);
			if (config2.CreateBiome<MudPlanet>().Place(new Point(x3, y3), GenVars.structures))
			{
				MudPlanetoidCount--;
			}
			i++;
		}
		if (!Main.getGoodWorld)
		{
			return;
		}
		for (int j = 0; j < Main.maxTilesX; j++)
		{
			for (int k = 0; k < (int)((float)Main.maxTilesY * 0.2f); k++)
			{
				bool convertToRegularSpikes = (j % 2 == 0 && k % 2 == 0) || (j % 2 != 0 && k % 2 != 0);
				if ((Main.tile[j, k].TileType == 232) & convertToRegularSpikes)
				{
					Main.tile[j, k].TileType = 48;
				}
				if ((Main.tile[j, k].TileType == 232 || Main.tile[j, k].TileType == 48) && WorldGen.genRand.NextBool(3))
				{
					Main.tile[j, k].Get<TileWallWireStateData>().HasTile = false;
					Main.tile[j, k].LiquidAmount = byte.MaxValue;
					Main.tile[j, k].Get<LiquidData>().LiquidType = 1;
				}
			}
		}
	}

	public static bool InvalidSkyPlacementArea(Rectangle area)
	{
		for (int i = ((Rectangle)(ref area)).Left; i < ((Rectangle)(ref area)).Right; i++)
		{
			for (int j = ((Rectangle)(ref area)).Top; j < ((Rectangle)(ref area)).Bottom; j++)
			{
				if (Main.tile[i, j].TileType == 189 || Main.tile[i, j].TileType == 196 || Main.tile[i, j].TileType == 202)
				{
					return false;
				}
			}
		}
		return true;
	}

	public bool CheckIfPlaceable(Point origin, int radius, StructureMap structures)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		int fluff = 12;
		int myRadius = radius + fluff;
		int diameter = myRadius * 2;
		_area = new Rectangle(origin.X - myRadius, origin.Y - myRadius, diameter, diameter);
		if (!InvalidSkyPlacementArea(_area))
		{
			return false;
		}
		if (!structures.CanPlace(_area))
		{
			return false;
		}
		new Dictionary<ushort, int>();
		CustomActions.SolidScanner scanner = new CustomActions.SolidScanner();
		WorldUtils.Gen(((Rectangle)(ref _area)).Location, new Shapes.Rectangle(_area.Width, _area.Height), scanner);
		if (scanner.GetCount() > 2)
		{
			return false;
		}
		return true;
	}

	public override bool Place(Point origin, StructureMap structures)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		structures.AddStructure(_area);
		return true;
	}
}
