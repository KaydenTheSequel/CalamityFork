using System;
using System.Collections.Generic;
using CalamityMod.Schematics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class MechanicShed
{
	public static void PlaceMechanicShed(StructureMap structures)
	{
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		string mapKey = "Mechanic Key";
		SchematicMetaTile[,] schematic = SchematicManager.TileMaps[mapKey];
		int leftLimit = GenVars.snowOriginLeft + 100;
		int rightLimit = GenVars.snowOriginRight - 100;
		int placementPositionX = WorldGen.genRand.Next(leftLimit, rightLimit);
		int placementPositionY = (int)Main.worldSurface - 50;
		int distanceToCheckForSnowTilesX = 30;
		int distanceToCheckForSnowTilesY = 5;
		int snowTilesRequired = 100;
		int emptyTilesRequired = 100;
		bool foundValidGround = false;
		int attempts = 0;
		int maxAttempts = 100000;
		while (!foundValidGround && attempts <= maxAttempts)
		{
			attempts++;
			int snowTileCount = 0;
			bool enoughSnowTilesOnBottom = false;
			for (int shedTileCheckIndexX = placementPositionX - 15; shedTileCheckIndexX < placementPositionX - 15 + distanceToCheckForSnowTilesX; shedTileCheckIndexX++)
			{
				if (enoughSnowTilesOnBottom)
				{
					break;
				}
				for (int shedTileCheckIndexY = placementPositionY - 5; shedTileCheckIndexY < placementPositionY - 5 + distanceToCheckForSnowTilesY; shedTileCheckIndexY++)
				{
					if (Main.tile[shedTileCheckIndexX, shedTileCheckIndexY] != null && Main.tile[shedTileCheckIndexX, shedTileCheckIndexY].HasTile && Main.tile[shedTileCheckIndexX, shedTileCheckIndexY].TileType == 147)
					{
						snowTileCount++;
						if (snowTileCount >= snowTilesRequired)
						{
							enoughSnowTilesOnBottom = true;
							break;
						}
					}
				}
			}
			int emptyTileCount = 0;
			bool enoughEmptyTilesOnTop = false;
			for (int i = placementPositionX - 15; i < placementPositionX - 15 + distanceToCheckForSnowTilesX; i++)
			{
				if (enoughEmptyTilesOnTop)
				{
					break;
				}
				for (int j = placementPositionY - 20; j < placementPositionY - 20 + distanceToCheckForSnowTilesY; j++)
				{
					if ((Main.tile[i, j] == null || !Main.tile[i, j].HasTile) && Main.tile[i, j].WallType == 0)
					{
						emptyTileCount++;
						if (emptyTileCount >= emptyTilesRequired)
						{
							enoughEmptyTilesOnTop = true;
							break;
						}
					}
				}
			}
			if (enoughSnowTilesOnBottom & enoughEmptyTilesOnTop)
			{
				break;
			}
			placementPositionX += 5;
			if (placementPositionX > rightLimit)
			{
				placementPositionX = leftLimit;
			}
			if (!enoughEmptyTilesOnTop)
			{
				placementPositionY -= 5;
			}
			else if (!enoughSnowTilesOnBottom)
			{
				placementPositionY += 5;
			}
		}
		Point placementPoint = default(Point);
		((Point)(ref placementPoint))._002Ector(placementPositionX, placementPositionY);
		SchematicAnchor anchorType = SchematicAnchor.BottomCenter;
		bool place = true;
		SchematicManager.PlaceSchematic<Action<Chest, int, bool>>(mapKey, placementPoint, anchorType, ref place, FillMechanicChest);
		CalamityUtils.AddProtectedStructure(CalamityUtils.GetSchematicProtectionArea(schematic, placementPoint, anchorType), 30);
	}

	public static void FillMechanicChest(Chest chest, int Type, bool place)
	{
		int gizmoGoobabGadgets = Utils.SelectRandom(WorldGen.genRand, new short[4] { 2214, 2215, 2216, 2217 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(1923, 1),
			new ChestItem(3624, 1),
			new ChestItem(gizmoGoobabGadgets, 1),
			new ChestItem(2325, WorldGen.genRand.Next(1, 3)),
			new ChestItem(73, WorldGen.genRand.Next(1, 3))
		};
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].Prefix(-1);
			chest.item[i].stack = contents[i].Stack;
		}
	}
}
