using System;
using System.Collections.Generic;
using CalamityMod.Schematics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class DungeonArchive
{
	public static void PlaceArchive()
	{
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		int worldThird = Main.maxTilesX / 3;
		int dungeonArchiveColor = 0;
		for (int j = Main.maxTilesY - 380; j > 0; j--)
		{
			int i = 100;
			if (GenVars.dungeonSide == 1)
			{
				i = Main.maxTilesX - 100;
			}
			bool shouldContinue = true;
			bool placedArchive = false;
			while (shouldContinue)
			{
				if (GenVars.dungeonSide == 1)
				{
					i--;
					if (i < Main.maxTilesX - worldThird)
					{
						shouldContinue = false;
					}
				}
				else
				{
					i++;
					if (i > worldThird)
					{
						shouldContinue = false;
					}
				}
				Tile tile = Main.tile[i, j];
				Tile tileUp1 = Main.tile[i, j - 1];
				Tile tileUp2 = Main.tile[i, j - 2];
				Tile tileUp3 = Main.tile[i, j - 3];
				Tile tileUp4 = Main.tile[i, j - 4];
				Tile tileUp5 = Main.tile[i, j - 5];
				if (Main.tileDungeon[tile.TileType] && !tileUp1.HasTile && !tileUp2.HasTile && !tileUp3.HasTile && !tileUp4.HasTile && !tileUp5.HasTile)
				{
					if (tile.TileType == 41)
					{
						dungeonArchiveColor = 0;
					}
					else if (tile.TileType == 43)
					{
						dungeonArchiveColor = 1;
					}
					else if (tile.TileType == 44)
					{
						dungeonArchiveColor = 2;
					}
					placedArchive = true;
					break;
				}
			}
			if (placedArchive)
			{
				bool firstItem = false;
				if (dungeonArchiveColor == 0)
				{
					SchematicManager.PlaceSchematic<Action<Chest, int, bool>>("Archive Blue", new Point(i, j), SchematicAnchor.TopCenter, ref firstItem, FillArchiveChests);
				}
				if (dungeonArchiveColor == 1)
				{
					SchematicManager.PlaceSchematic<Action<Chest, int, bool>>("Archive Green", new Point(i, j), SchematicAnchor.TopCenter, ref firstItem, FillArchiveChests);
				}
				if (dungeonArchiveColor == 2)
				{
					SchematicManager.PlaceSchematic<Action<Chest, int, bool>>("Archive Pink", new Point(i, j), SchematicAnchor.TopCenter, ref firstItem, FillArchiveChests);
				}
				break;
			}
		}
	}

	public static void FillArchiveChests(Chest chest, int Type, bool firstItem)
	{
		int potionType1 = Utils.SelectRandom(WorldGen.genRand, new short[2] { 304, 292 });
		int potionType2 = Utils.SelectRandom(WorldGen.genRand, new short[2] { 298, 290 });
		List<ChestItem> contents1 = new List<ChestItem>
		{
			new ChestItem(329, 1),
			new ChestItem(188, WorldGen.genRand.Next(10, 20)),
			new ChestItem(189, WorldGen.genRand.Next(10, 20)),
			new ChestItem(potionType1, WorldGen.genRand.Next(4, 8)),
			new ChestItem(potionType2, WorldGen.genRand.Next(4, 8)),
			new ChestItem(73, WorldGen.genRand.Next(5, 10))
		};
		List<ChestItem> contents2 = new List<ChestItem>
		{
			new ChestItem(531, WorldGen.genRand.Next(2, 3)),
			new ChestItem(149, WorldGen.genRand.Next(12, 25)),
			new ChestItem(3095, 1),
			new ChestItem(potionType1, WorldGen.genRand.Next(4, 8)),
			new ChestItem(potionType2, WorldGen.genRand.Next(4, 8)),
			new ChestItem(73, WorldGen.genRand.Next(5, 10))
		};
		for (int i = 0; i < contents1.Count; i++)
		{
			if (!firstItem)
			{
				chest.item[i].SetDefaults(contents1[i].Type);
				chest.item[i].stack = contents1[i].Stack;
			}
			else
			{
				chest.item[i].SetDefaults(contents2[i].Type);
				chest.item[i].Prefix(-1);
				chest.item[i].stack = contents2[i].Stack;
			}
		}
	}
}
