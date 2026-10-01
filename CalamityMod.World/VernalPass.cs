using System;
using System.Collections.Generic;
using CalamityMod.Items.Tools;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Schematics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class VernalPass
{
	public static void PlaceVernalPass(StructureMap structures)
	{
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		string mapKey = "Vernal Pass";
		SchematicMetaTile[,] schematic = SchematicManager.TileMaps[mapKey];
		int placementPositionX = WorldGen.genRand.Next(GenVars.tLeft, GenVars.tRight);
		int initialPlacementPositionY = ((GenVars.tTop < Main.maxTilesY / 2) ? (GenVars.tBottom + 150) : (GenVars.tTop - 150));
		int placementPositionY = ((GenVars.tTop < Main.maxTilesY / 2) ? (GenVars.tBottom + 150) : (GenVars.tTop - 150));
		bool foundValidPosition = false;
		int attempts = 0;
		while (!foundValidPosition && attempts++ < 100000)
		{
			for (; !NoTempleNearby(placementPositionX, placementPositionY); placementPositionY += ((initialPlacementPositionY < Main.maxTilesX / 2) ? (-10) : 10))
			{
			}
			if (NoTempleNearby(placementPositionX, placementPositionY))
			{
				foundValidPosition = true;
			}
		}
		Point placementPoint = default(Point);
		((Point)(ref placementPoint))._002Ector(placementPositionX, placementPositionY);
		new Vector2((float)schematic.GetLength(0), (float)schematic.GetLength(1));
		SchematicAnchor anchorType = SchematicAnchor.Center;
		bool firstItem = false;
		SchematicManager.PlaceSchematic<Action<Chest, int, bool>>(mapKey, placementPoint, anchorType, ref firstItem, FillVernalPassChests);
		CalamityUtils.AddProtectedStructure(CalamityUtils.GetSchematicProtectionArea(schematic, placementPoint, anchorType), 30);
	}

	public static void FillVernalPassChests(Chest chest, int Type, bool firstItem)
	{
		UnifiedRandom genRand = WorldGen.genRand;
		int[] obj = new int[4] { 213, 212, 211, 0 };
		obj[3] = ModContent.ItemType<BelladonnaSpiritStaff>();
		int mainItem = Utils.SelectRandom(genRand, obj);
		int bars = Utils.SelectRandom(WorldGen.genRand, new short[2] { 19, 706 });
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[4] { 301, 300, 298, 304 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(bars, WorldGen.genRand.Next(4, 7)),
			new ChestItem(331, WorldGen.genRand.Next(4, 8)),
			new ChestItem(209, WorldGen.genRand.Next(2, 5)),
			new ChestItem(4388, WorldGen.genRand.Next(2, 5)),
			new ChestItem(potionType, WorldGen.genRand.Next(1, 4)),
			new ChestItem(73, WorldGen.genRand.Next(1, 3))
		};
		if (!firstItem)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<FellerofEvergreens>(), 1));
		}
		else
		{
			contents.RemoveAt(0);
			contents.Insert(0, new ChestItem(mainItem, 1));
			contents.Insert(1, new ChestItem(bars, WorldGen.genRand.Next(4, 7)));
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].Prefix(-1);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static bool NoTempleNearby(int X, int Y)
	{
		for (int i = X - 140; i < X + 140; i++)
		{
			for (int j = Y - 140; j < Y + 140; j++)
			{
				if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == 226)
				{
					return false;
				}
			}
		}
		return true;
	}
}
