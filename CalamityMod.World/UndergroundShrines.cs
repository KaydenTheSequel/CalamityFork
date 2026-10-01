using System;
using System.Collections.Generic;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Mounts;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Items.Potions.Alcohol;
using CalamityMod.Items.Potions.Food;
using CalamityMod.Items.SummonItems;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Schematics;
using CalamityMod.Tiles;
using CalamityMod.Tiles.FurnitureVoid;
using CalamityMod.Tiles.SunkenSea;
using CalamityMod.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class UndergroundShrines
{
	public static bool ShouldAvoidLocation(Point placementPoint, bool careAboutLiquids = true)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = CalamityUtils.ParanoidTileRetrieval(placementPoint.X, placementPoint.Y);
		if ((tile.LiquidAmount > 0) & careAboutLiquids)
		{
			return true;
		}
		if (tile.TileType == 41 || tile.TileType == 43 || tile.TileType == 44)
		{
			return true;
		}
		if (tile.TileType == 226 || tile.WallType == 87)
		{
			return true;
		}
		if (tile.TileType == ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.Navystone>() || tile.TileType == ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.EutrophicSand>() || tile.WallType == ModContent.WallType<NavystoneWall>())
		{
			return true;
		}
		return false;
	}

	public static void FillCorruptionShrineChest(Chest chest)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[3] { 300, 304, 2329 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<CorruptionEffigy>(), 1),
			new ChestItem(68, WorldGen.genRand.Next(24, 29)),
			new ChestItem(1534, 1),
			new ChestItem(4385, WorldGen.genRand.Next(100, 111)),
			new ChestItem(73, WorldGen.genRand.Next(8, 11)),
			new ChestItem(188, WorldGen.genRand.Next(10, 13)),
			new ChestItem(potionType, WorldGen.genRand.Next(10, 13))
		};
		if (Main.zenithWorld)
		{
			int evil = Utils.SelectRandom<int>(WorldGen.genRand, ModContent.ItemType<StressPills>(), ModContent.ItemType<Laudanum>(), ModContent.ItemType<HeartofDarkness>());
			contents = new List<ChestItem>
			{
				new ChestItem(ModContent.ItemType<CorruptionEffigy>(), 1),
				new ChestItem(68, WorldGen.genRand.Next(24, 29)),
				new ChestItem(1534, 1),
				new ChestItem(4385, WorldGen.genRand.Next(100, 111)),
				new ChestItem(73, WorldGen.genRand.Next(8, 11)),
				new ChestItem(evil, 1),
				new ChestItem(678, WorldGen.genRand.Next(1, 3)),
				new ChestItem(5346, 1)
			};
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlaceCorruptionShrine(StructureMap structures)
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = "Corruption Shrine";
		Point placementPoint = default(Point);
		Vector2 schematicSize = default(Vector2);
		do
		{
			int placementPositionX = WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.05f), (int)((float)Main.maxTilesX * 0.95f));
			int placementPositionY = WorldGen.genRand.Next((int)Main.worldSurface, (int)((float)Main.maxTilesY * 0.5f));
			((Point)(ref placementPoint))._002Ector(placementPositionX, placementPositionY);
			((Vector2)(ref schematicSize))._002Ector((float)(SchematicManager.TileMaps[mapKey].GetLength(0) / 2), (float)SchematicManager.TileMaps[mapKey].GetLength(1));
			int corruptStuffInArea = 0;
			bool canGenerateInLocation = true;
			bool inYourWalls = false;
			float totalTiles = schematicSize.X * schematicSize.Y;
			for (int x = placementPoint.X; (float)x < (float)placementPoint.X + schematicSize.X; x++)
			{
				for (int y = placementPoint.Y; (float)y < (float)placementPoint.Y + schematicSize.Y; y++)
				{
					Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
					if (ShouldAvoidLocation(new Point(x, y)))
					{
						canGenerateInLocation = false;
					}
					if (tile.TileType == 25 || tile.WallType == 3)
					{
						corruptStuffInArea++;
					}
					if (tile.WallType == 3)
					{
						inYourWalls = true;
					}
					if (tile.TileType == 26)
					{
						canGenerateInLocation = false;
					}
				}
			}
			if (!canGenerateInLocation || (float)corruptStuffInArea < totalTiles * 0.9f || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y)) || !inYourWalls)
			{
				tries++;
				continue;
			}
			bool _ = true;
			SchematicManager.PlaceSchematic<Action<Chest>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref _, FillCorruptionShrineChest);
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X * 2, (int)schematicSize.Y), 4);
			break;
		}
		while (tries <= 60000);
	}

	public static void FillCrimsonShrineChest(Chest chest)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[3] { 300, 304, 2329 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<CrimsonEffigy>(), 1),
			new ChestItem(1330, WorldGen.genRand.Next(24, 29)),
			new ChestItem(1535, 1),
			new ChestItem(4386, WorldGen.genRand.Next(100, 111)),
			new ChestItem(73, WorldGen.genRand.Next(8, 11)),
			new ChestItem(188, WorldGen.genRand.Next(10, 13)),
			new ChestItem(potionType, WorldGen.genRand.Next(10, 13))
		};
		if (Main.zenithWorld)
		{
			contents = new List<ChestItem>
			{
				new ChestItem(ModContent.ItemType<CrimsonEffigy>(), 1),
				new ChestItem(1330, WorldGen.genRand.Next(24, 29)),
				new ChestItem(1535, 1),
				new ChestItem(4386, WorldGen.genRand.Next(100, 111)),
				new ChestItem(73, WorldGen.genRand.Next(8, 11)),
				new ChestItem(ModContent.ItemType<BloodyMary>(), WorldGen.genRand.Next(2, 3)),
				new ChestItem(678, WorldGen.genRand.Next(1, 3)),
				new ChestItem(5346, 1)
			};
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlaceCrimsonShrine(StructureMap structures)
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = "Crimson Shrine";
		Point placementPoint = default(Point);
		Vector2 schematicSize = default(Vector2);
		do
		{
			int placementPositionX = WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.05f), (int)((float)Main.maxTilesX * 0.95f));
			int placementPositionY = WorldGen.genRand.Next((int)Main.worldSurface, (int)((float)Main.maxTilesY * 0.5f));
			((Point)(ref placementPoint))._002Ector(placementPositionX, placementPositionY);
			((Vector2)(ref schematicSize))._002Ector((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1));
			int crimsonStuffInArea = 0;
			bool canGenerateInLocation = true;
			bool inYourWalls = false;
			float groundThreshold = schematicSize.Y * 0.4f;
			float totalTiles = schematicSize.X * schematicSize.Y;
			for (int x = placementPoint.X; (float)x < (float)placementPoint.X + schematicSize.X; x++)
			{
				for (int y = placementPoint.Y; (float)y < (float)placementPoint.Y + schematicSize.Y; y++)
				{
					Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
					if (ShouldAvoidLocation(new Point(x, y)))
					{
						canGenerateInLocation = false;
					}
					if (tile.TileType == 203 || tile.WallType == 83)
					{
						crimsonStuffInArea++;
					}
					if (tile.WallType == 83)
					{
						inYourWalls = true;
					}
					if (tile.TileType == 26)
					{
						canGenerateInLocation = false;
					}
				}
			}
			if (!canGenerateInLocation || (float)crimsonStuffInArea < totalTiles * 0.4f || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y)) || !inYourWalls)
			{
				tries++;
				continue;
			}
			bool _ = true;
			SchematicManager.PlaceSchematic<Action<Chest>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref _, FillCrimsonShrineChest);
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 4);
			break;
		}
		while (tries <= 60000);
	}

	public static void FillDesertShrineChest(Chest chest)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[3] { 298, 2322, 2325 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<LuxorsGift>(), 1),
			new ChestItem(ModContent.ItemType<PrismShard>(), WorldGen.genRand.Next(6, 9)),
			new ChestItem(4714, 1),
			new ChestItem(4383, WorldGen.genRand.Next(100, 111)),
			new ChestItem(73, WorldGen.genRand.Next(8, 11)),
			new ChestItem(188, WorldGen.genRand.Next(10, 13)),
			new ChestItem(potionType, WorldGen.genRand.Next(10, 13))
		};
		if (Main.zenithWorld)
		{
			int golfClub = Utils.SelectRandom(WorldGen.genRand, new short[3] { 4589, 4093, 5346 });
			contents = new List<ChestItem>
			{
				new ChestItem(ModContent.ItemType<LuxorsGift>(), 1),
				new ChestItem(ModContent.ItemType<PrismShard>(), WorldGen.genRand.Next(6, 9)),
				new ChestItem(4714, 1),
				new ChestItem(4383, WorldGen.genRand.Next(100, 111)),
				new ChestItem(73, WorldGen.genRand.Next(8, 11)),
				new ChestItem(ModContent.ItemType<SpelunkersAmulet>(), 1),
				new ChestItem(678, WorldGen.genRand.Next(1, 3)),
				new ChestItem(golfClub, 1)
			};
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].Prefix(-1);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlaceDesertShrine(StructureMap structures)
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = "Desert Shrine";
		Point placementPoint = default(Point);
		Vector2 schematicSize = default(Vector2);
		do
		{
			int placementPositionX = WorldGen.genRand.Next(((Rectangle)(ref GenVars.UndergroundDesertLocation)).Left, ((Rectangle)(ref GenVars.UndergroundDesertLocation)).Right);
			int placementPositionY = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.3f), (int)((float)Main.maxTilesY * 0.55f));
			((Point)(ref placementPoint))._002Ector(placementPositionX, placementPositionY);
			((Vector2)(ref schematicSize))._002Ector((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1));
			int desertTilesInArea = 0;
			int xCheckArea = 50;
			bool canGenerateInLocation = true;
			float totalTiles = (schematicSize.X + (float)(xCheckArea * 2)) * schematicSize.Y;
			for (int x = placementPoint.X - xCheckArea; (float)x < (float)placementPoint.X + schematicSize.X + (float)xCheckArea; x++)
			{
				for (int y = placementPoint.Y; (float)y < (float)placementPoint.Y + schematicSize.Y; y++)
				{
					Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
					if (ShouldAvoidLocation(new Point(x, y)))
					{
						canGenerateInLocation = false;
					}
					if (tile.TileType == 404 || tile.TileType == 53 || tile.TileType == 397 || tile.TileType == 396)
					{
						desertTilesInArea++;
					}
				}
			}
			if (!canGenerateInLocation || (float)desertTilesInArea < totalTiles * 0.3f || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y)))
			{
				tries++;
				continue;
			}
			bool _ = true;
			SchematicManager.PlaceSchematic<Action<Chest>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref _, FillDesertShrineChest);
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 4);
			break;
		}
		while (tries <= 20000);
	}

	public static void FillGraniteShrineChest(Chest chest)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[3] { 2346, 2323, 2345 });
		if (Main.zenithWorld)
		{
			potionType = 678;
		}
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<UnstableGraniteCore>(), 1),
			new ChestItem(4400, WorldGen.genRand.Next(6, 9)),
			new ChestItem(427, WorldGen.genRand.Next(100, 111)),
			new ChestItem(73, WorldGen.genRand.Next(8, 11)),
			new ChestItem(188, WorldGen.genRand.Next(10, 13)),
			new ChestItem(potionType, WorldGen.genRand.Next(Main.zenithWorld ? 1 : 10, (Main.zenithWorld ? 2 : 12) + 1)),
			new ChestItem((Main.rand.NextBool() && Main.zenithWorld) ? 5346 : 3086, Main.zenithWorld ? 1 : WorldGen.genRand.Next(7, 16))
		};
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].Prefix(-1);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlaceGraniteShrine(StructureMap structures)
	{
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = "Granite Shrine";
		Point placementPoint = default(Point);
		Vector2 schematicSize = default(Vector2);
		do
		{
			int placementPositionX = (Main.rand.NextBool() ? WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.55f), Main.maxTilesX - WorldGen.beachDistance) : WorldGen.genRand.Next(WorldGen.beachDistance, (int)((float)Main.maxTilesX * 0.45f)));
			int placementPositionY = WorldGen.genRand.Next((int)GenVars.rockLayer + 20, Main.maxTilesY - 220);
			if (Main.remixWorld)
			{
				placementPositionY = WorldGen.genRand.Next((int)GenVars.worldSurface + 100, (int)GenVars.rockLayer);
			}
			((Point)(ref placementPoint))._002Ector(placementPositionX, placementPositionY);
			((Vector2)(ref schematicSize))._002Ector((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1));
			int graniteWallsInArea = 0;
			bool canGenerateInLocation = true;
			float totalTiles = schematicSize.X * schematicSize.Y;
			for (int x = placementPoint.X; (float)x < (float)placementPoint.X + schematicSize.X; x++)
			{
				for (int y = placementPoint.Y; (float)y < (float)placementPoint.Y + schematicSize.Y; y++)
				{
					Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
					if (ShouldAvoidLocation(new Point(x, y), careAboutLiquids: false))
					{
						canGenerateInLocation = false;
					}
					if (tile.WallType == 180 && !tile.HasTile && !Main.drunkWorld)
					{
						graniteWallsInArea++;
					}
					else if ((tile.WallType == 178 || tile.TileType == 367) && Main.drunkWorld)
					{
						graniteWallsInArea++;
					}
				}
			}
			if (!canGenerateInLocation || (float)graniteWallsInArea < totalTiles * 0.95f || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y)))
			{
				tries++;
				continue;
			}
			bool _ = true;
			SchematicManager.PlaceSchematic(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref _, Main.drunkWorld ? new Action<Chest>(FillMarbleShrineChest) : new Action<Chest>(FillGraniteShrineChest));
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 4);
			if (!Main.drunkWorld)
			{
				break;
			}
			for (int i = placementPoint.X; (float)i < (float)placementPoint.X + schematicSize.X; i++)
			{
				for (int j = placementPoint.Y; (float)j < (float)placementPoint.Y + schematicSize.Y; j++)
				{
					Tile tile2 = CalamityUtils.ParanoidTileRetrieval(i, j);
					switch (tile2.TileType)
					{
					case 368:
						tile2.TileType = 367;
						break;
					case 369:
						tile2.TileType = 357;
						break;
					case 21:
						tile2.TileFrameX += 36;
						break;
					case 178:
						tile2.TileFrameX += 54;
						break;
					}
					switch (tile2.WallType)
					{
					case 184:
						tile2.WallType = 183;
						break;
					case 165:
						tile2.WallType = 155;
						tile2.WallColor = 0;
						break;
					}
				}
			}
			break;
		}
		while (tries <= 30000);
	}

	public static void FillIceShrineChest(Chest chest)
	{
		int foodType = Utils.SelectRandom(WorldGen.genRand, new short[3] { 1911, 1919, 1920 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<TundraLeash>(), 1),
			new ChestItem(5070, WorldGen.genRand.Next(6, 9)),
			new ChestItem(1537, 1),
			new ChestItem(974, WorldGen.genRand.Next(100, 111)),
			new ChestItem(73, WorldGen.genRand.Next(8, 11)),
			new ChestItem(188, WorldGen.genRand.Next(10, 13)),
			new ChestItem(foodType, WorldGen.genRand.Next(10, 13))
		};
		if (Main.zenithWorld)
		{
			contents = new List<ChestItem>
			{
				new ChestItem(ModContent.ItemType<TundraLeash>(), 1),
				new ChestItem(5070, WorldGen.genRand.Next(6, 9)),
				new ChestItem(1537, 1),
				new ChestItem(974, WorldGen.genRand.Next(100, 111)),
				new ChestItem(73, WorldGen.genRand.Next(8, 11)),
				new ChestItem(1912, WorldGen.genRand.Next(10, 13)),
				new ChestItem(ModContent.ItemType<DeliciousMeat>(), WorldGen.genRand.Next(200, 350)),
				new ChestItem(Main.rand.NextBool() ? 5346 : 967, 1)
			};
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlaceIceShrine(StructureMap structures)
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = "Ice Shrine";
		Point placementPoint = default(Point);
		Vector2 schematicSize = default(Vector2);
		do
		{
			int placementPositionX = WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.25f), (int)((float)Main.maxTilesX * 0.75f));
			int placementPositionY = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.35f), (int)((float)Main.maxTilesY * 0.7f));
			((Point)(ref placementPoint))._002Ector(placementPositionX, placementPositionY);
			((Vector2)(ref schematicSize))._002Ector((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1));
			int iceTilesInArea = 0;
			int xCheckArea = 80;
			int yCheckArea = 20;
			bool canGenerateInLocation = true;
			float totalTiles = (schematicSize.X + (float)(xCheckArea * 2)) * (schematicSize.Y + (float)(yCheckArea * 2));
			for (int x = placementPoint.X - xCheckArea; (float)x < (float)placementPoint.X + schematicSize.X + (float)xCheckArea; x++)
			{
				for (int y = placementPoint.Y - yCheckArea; (float)y < (float)placementPoint.Y + schematicSize.Y + (float)yCheckArea; y++)
				{
					Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
					if (ShouldAvoidLocation(new Point(x, y), careAboutLiquids: false))
					{
						canGenerateInLocation = false;
					}
					if (tile.TileType == 147 || tile.TileType == 161)
					{
						iceTilesInArea++;
					}
				}
			}
			if (!canGenerateInLocation || (float)iceTilesInArea < totalTiles * 0.35f || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y)))
			{
				tries++;
				continue;
			}
			bool _ = true;
			SchematicManager.PlaceSchematic<Action<Chest>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref _, FillIceShrineChest);
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 4);
			break;
		}
		while (tries <= 20000);
	}

	public static void FillMarbleShrineChest(Chest chest)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[3] { 2346, 2323, 2345 });
		if (Main.zenithWorld)
		{
			potionType = 678;
		}
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<GladiatorsLocket>(), 1),
			new ChestItem((GenVars.goldBar == 8) ? 19 : 706, WorldGen.genRand.Next(12, 16)),
			new ChestItem(431, WorldGen.genRand.Next(100, 111)),
			new ChestItem(73, WorldGen.genRand.Next(8, 11)),
			new ChestItem(188, WorldGen.genRand.Next(10, 13)),
			new ChestItem(potionType, WorldGen.genRand.Next(Main.zenithWorld ? 1 : 10, (Main.zenithWorld ? 2 : 12) + 1)),
			new ChestItem((Main.rand.NextBool() && Main.zenithWorld) ? 5346 : 3081, Main.zenithWorld ? 1 : WorldGen.genRand.Next(7, 16))
		};
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].Prefix(-1);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlaceMarbleShrine(StructureMap structures)
	{
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = "Marble Shrine";
		Point placementPoint = default(Point);
		Vector2 schematicSize = default(Vector2);
		do
		{
			int placementPositionX = (Main.rand.NextBool() ? WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.55f), Main.maxTilesX - WorldGen.beachDistance) : WorldGen.genRand.Next(WorldGen.beachDistance, (int)((float)Main.maxTilesX * 0.45f)));
			int placementPositionY = WorldGen.genRand.Next((int)GenVars.rockLayer + 20, Main.maxTilesY - 220);
			if (Main.remixWorld)
			{
				placementPositionY = WorldGen.genRand.Next((int)GenVars.worldSurface + 100, (int)GenVars.rockLayer);
			}
			((Point)(ref placementPoint))._002Ector(placementPositionX, placementPositionY);
			((Vector2)(ref schematicSize))._002Ector((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1));
			int marbleStuffInArea = 0;
			int airTilesBetweenPillar = 0;
			bool canGenerateInLocation = true;
			float totalTiles = schematicSize.X * schematicSize.Y;
			for (int x = placementPoint.X; (float)x < (float)placementPoint.X + schematicSize.X; x++)
			{
				for (int y = placementPoint.Y; (float)y < (float)placementPoint.Y + schematicSize.Y; y++)
				{
					Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
					if (ShouldAvoidLocation(new Point(x, y)))
					{
						canGenerateInLocation = false;
					}
					if ((tile.TileType == 367 || tile.WallType == 178) && !Main.drunkWorld)
					{
						marbleStuffInArea++;
					}
					else if ((tile.TileType == 368 || tile.WallType == 180) && Main.drunkWorld)
					{
						marbleStuffInArea++;
					}
					float pillarFoundationBound = schematicSize.Y * 0.2f;
					if ((float)y <= (float)placementPoint.Y + schematicSize.Y - pillarFoundationBound && (float)y >= (float)placementPoint.Y + pillarFoundationBound && !tile.HasTile)
					{
						airTilesBetweenPillar++;
					}
				}
			}
			if (!canGenerateInLocation || (float)marbleStuffInArea < totalTiles * 0.9f || (float)airTilesBetweenPillar < totalTiles * 0.3f || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y)))
			{
				tries++;
				continue;
			}
			bool _ = true;
			SchematicManager.PlaceSchematic(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref _, Main.drunkWorld ? new Action<Chest>(FillGraniteShrineChest) : new Action<Chest>(FillMarbleShrineChest));
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 4);
			if (!Main.drunkWorld)
			{
				break;
			}
			for (int i = placementPoint.X; (float)i < (float)placementPoint.X + schematicSize.X; i++)
			{
				for (int j = placementPoint.Y; (float)j < (float)placementPoint.Y + schematicSize.Y; j++)
				{
					Tile tile2 = CalamityUtils.ParanoidTileRetrieval(i, j);
					switch (tile2.TileType)
					{
					case 367:
						tile2.TileType = 368;
						break;
					case 357:
						tile2.TileType = 369;
						break;
					case 561:
						tile2.TileType = 576;
						break;
					case 21:
						tile2.TileFrameX -= 36;
						break;
					case 19:
						tile2.TileFrameY -= 18;
						break;
					}
					switch (tile2.WallType)
					{
					case 183:
						tile2.WallType = 184;
						break;
					case 179:
						tile2.WallType = 181;
						tile2.WallColor = 0;
						break;
					}
				}
			}
			break;
		}
		while (tries <= 30000);
	}

	public static void FillMushroomShrineChest(Chest chest)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[3] { 298, 2322, 2325 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<FungalSymbiote>(), 1),
			new ChestItem(2673, 3),
			new ChestItem(5293, WorldGen.genRand.Next(100, 111)),
			new ChestItem(73, WorldGen.genRand.Next(8, 11)),
			new ChestItem(188, WorldGen.genRand.Next(10, 13)),
			new ChestItem(potionType, WorldGen.genRand.Next(10, 13))
		};
		if (Main.zenithWorld)
		{
			contents = new List<ChestItem>
			{
				new ChestItem(ModContent.ItemType<FungalSymbiote>(), 1),
				new ChestItem(2673, 3),
				new ChestItem(5293, WorldGen.genRand.Next(100, 111)),
				new ChestItem(73, WorldGen.genRand.Next(8, 11)),
				new ChestItem(ModContent.ItemType<OddMushroom>(), WorldGen.genRand.Next(2, 4)),
				new ChestItem(678, WorldGen.genRand.Next(1, 3)),
				new ChestItem(5346, 1)
			};
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].Prefix(-1);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlaceMushroomShrine(StructureMap structures)
	{
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = "Mushroom Shrine";
		Point placementPoint = default(Point);
		Vector2 schematicSize = default(Vector2);
		do
		{
			int placementPositionX = WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.2f), (int)((float)Main.maxTilesX * 0.8f));
			int placementPositionY = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.2f), (int)((float)Main.maxTilesY * 0.85f));
			if (Main.remixWorld)
			{
				do
				{
					placementPositionX = WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.2f), (int)((float)Main.maxTilesX * 0.8f));
				}
				while (placementPositionX > (int)((float)Main.maxTilesX * 0.4f) && placementPositionX < (int)((float)Main.maxTilesX * 0.6f));
				placementPositionY = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.85f), (int)((float)Main.maxTilesY * 0.9f));
			}
			((Point)(ref placementPoint))._002Ector(placementPositionX, placementPositionY);
			((Vector2)(ref schematicSize))._002Ector((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1));
			int realMushroomsInArea = 0;
			int extraArea = 20;
			int yExtraArea = 40;
			bool canGenerateInLocation = true;
			int requiredShrooms = 20;
			for (int x = placementPoint.X - extraArea; (float)x < (float)placementPoint.X + schematicSize.X + (float)extraArea; x++)
			{
				for (int y = placementPoint.Y; (float)y < (float)placementPoint.Y + schematicSize.Y + (float)yExtraArea; y++)
				{
					Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
					if (ShouldAvoidLocation(new Point(x, y), careAboutLiquids: false))
					{
						canGenerateInLocation = false;
					}
					if (tile.TileType == 71 || tile.TileType == 528 || tile.TileType == 72 || tile.TileType == 70)
					{
						realMushroomsInArea++;
					}
				}
			}
			if ((!canGenerateInLocation || realMushroomsInArea < requiredShrooms || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y))) && !Main.remixWorld)
			{
				tries++;
				continue;
			}
			if (!canGenerateInLocation && Main.remixWorld)
			{
				tries++;
				continue;
			}
			bool _ = true;
			SchematicManager.PlaceSchematic<Action<Chest>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref _, FillMushroomShrineChest);
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 4);
			break;
		}
		while (tries <= 20000);
	}

	public static void FillSurfaceShrineChest(Chest chest)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[3] { 2350, 2324, 290 });
		if (Main.zenithWorld)
		{
			potionType = 2266;
		}
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<TrinketofChi>(), 1),
			new ChestItem(3111, WorldGen.genRand.Next(12, 16)),
			new ChestItem(8, WorldGen.genRand.Next(50, 61)),
			new ChestItem(73, WorldGen.genRand.Next(2, 5)),
			new ChestItem(Main.zenithWorld ? 227 : 28, WorldGen.genRand.Next(10, 13)),
			new ChestItem(potionType, WorldGen.genRand.Next(10, 13)),
			new ChestItem(Main.zenithWorld ? 5346 : 5, Main.zenithWorld ? 1 : WorldGen.genRand.Next(5, 10))
		};
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].Prefix(-1);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlaceSurfaceShrine(StructureMap structures)
	{
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = "Surface Shrine";
		Point placementPoint = default(Point);
		Vector2 schematicSize = default(Vector2);
		Point shrineTunnelPlacementPoint = default(Point);
		do
		{
			int placementPositionX = WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.2f), (int)((float)Main.maxTilesX * 0.8f));
			do
			{
				placementPositionX = WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.2f), (int)((float)Main.maxTilesX * 0.8f));
			}
			while (placementPositionX > (int)((float)Main.maxTilesX * 0.4f) && placementPositionX < (int)((float)Main.maxTilesX * 0.6f));
			int numTilesBelowSurface = WorldGen.genRand.Next(25, 50);
			int placementPositionY = (int)Main.worldSurface + numTilesBelowSurface;
			if (Main.remixWorld)
			{
				placementPositionY = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.65f), (int)((float)Main.maxTilesY * 0.7f));
			}
			((Point)(ref placementPoint))._002Ector(placementPositionX, placementPositionY);
			((Vector2)(ref schematicSize))._002Ector((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1));
			int normalTilesInArea = 0;
			int activeTilesInArea = 0;
			bool canGenerateInLocation = true;
			for (int x = placementPoint.X; (float)x < (float)placementPoint.X + schematicSize.X; x++)
			{
				for (int y = placementPoint.Y; (float)y < (float)placementPoint.Y + schematicSize.Y; y++)
				{
					Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
					if (ShouldAvoidLocation(new Point(x, y), careAboutLiquids: false))
					{
						canGenerateInLocation = false;
					}
					if (tile.TileType == 0 || tile.TileType == 1 || tile.TileType == 40 || tile.TileType == 53)
					{
						normalTilesInArea++;
					}
					if (tile.HasTile)
					{
						activeTilesInArea++;
					}
					if (tile.WallType == 216 || tile.WallType == 187)
					{
						canGenerateInLocation = false;
					}
				}
			}
			if (!canGenerateInLocation || (float)normalTilesInArea < (float)activeTilesInArea * 0.8f || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y)))
			{
				tries++;
				continue;
			}
			if (!Main.remixWorld)
			{
				((Point)(ref shrineTunnelPlacementPoint))._002Ector(placementPoint.X + (int)(schematicSize.X * 0.5f), placementPoint.Y);
				bool flag = WorldUtils.Find(shrineTunnelPlacementPoint, Searches.Chain(new Searches.Up(1000), new Conditions.IsSolid().AreaOr(1, 50).Not()), out var result);
				if (WorldUtils.Find(shrineTunnelPlacementPoint, Searches.Chain(new Searches.Up(shrineTunnelPlacementPoint.Y - result.Y), new Conditions.IsTile(53)), out var _))
				{
					tries++;
					continue;
				}
				if (!flag)
				{
					tries++;
					continue;
				}
				result.Y += numTilesBelowSurface;
				bool[] array = new bool[TileID.Sets.GeneralPlacementTiles.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = TileID.Sets.GeneralPlacementTiles[i];
				}
				array[21] = false;
				array[467] = false;
				if (!structures.CanPlace(new Rectangle(shrineTunnelPlacementPoint.X, result.Y + 10, 1, shrineTunnelPlacementPoint.Y - result.Y - 9), array, 2))
				{
					tries++;
					continue;
				}
				bool _ = true;
				SchematicManager.PlaceSchematic<Action<Chest>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref _, FillSurfaceShrineChest);
				CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 4);
				ShapeData data = new ShapeData();
				WorldUtils.Gen(new Point(shrineTunnelPlacementPoint.X, result.Y + 10), new Shapes.Rectangle(1, shrineTunnelPlacementPoint.Y - result.Y - 9), Actions.Chain(new Modifiers.Blotches(2, 0.2), new Modifiers.SkipTiles(191, 192), new Actions.ClearTile().Output(data), new Modifiers.Expand(1), new Modifiers.OnlyTiles(53), new Actions.SetTile(397).Output(data)));
				WorldUtils.Gen(new Point(shrineTunnelPlacementPoint.X, result.Y + 10), new ModShapes.All(data), new Actions.SetFrames(frameNeighbors: true));
				break;
			}
			bool _2 = true;
			SchematicManager.PlaceSchematic<Action<Chest>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref _2, FillSurfaceShrineChest);
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 4);
			break;
		}
		while (tries <= 30000);
	}

	public static void PlaceRoxShrine(StructureMap structures)
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = (Main.rand.NextBool() ? "Roxcalibur Shrine 1" : "Roxcalibur Shrine 2");
		Vector2 schematicSize = default(Vector2);
		((Vector2)(ref schematicSize))._002Ector((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1));
		Point placementPoint = default(Point);
		do
		{
			int placementPositionX = WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.15f), (int)((float)Main.maxTilesX * 0.85f));
			int placementPositionY = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.75f), Main.UnderworldLayer - 50);
			((Point)(ref placementPoint))._002Ector(placementPositionX, placementPositionY);
			int yExtraArea = 10;
			bool canGenerateInLocation = true;
			for (int x = placementPoint.X; (float)x < (float)placementPoint.X + schematicSize.X; x++)
			{
				for (int y = placementPoint.Y; (float)y < (float)placementPoint.Y + schematicSize.Y + (float)yExtraArea; y++)
				{
					Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
					if (tile.TileType == 30 || tile.TileType == 59 || tile.TileType == ModContent.TileType<VernalSoil>())
					{
						canGenerateInLocation = false;
					}
					if (ShouldAvoidLocation(new Point(x, y - 20)))
					{
						canGenerateInLocation = false;
					}
					if (ShouldAvoidLocation(new Point(x, y), careAboutLiquids: false))
					{
						canGenerateInLocation = false;
					}
				}
			}
			if ((!canGenerateInLocation || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y))) && !Main.remixWorld)
			{
				tries++;
				continue;
			}
			bool _ = false;
			SchematicManager.PlaceSchematic<Action<Chest>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref _);
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 4);
			return;
		}
		while (tries <= 100000);
		CalamityMod.Log.Debug((object)"Rox Shrine failed to generate");
	}

	public static void PlaceAbyssShrine(int chestLeftX, int chestTopY)
	{
		int shrineLeftX = chestLeftX - 5;
		int shrineRightX = chestLeftX + 4;
		int shrineTopY = chestTopY - 5;
		int shrineBottomY = chestTopY + 3;
		for (int x = shrineLeftX; x <= shrineRightX; x++)
		{
			for (int y = shrineTopY; y <= shrineBottomY; y++)
			{
				Main.tile[x, y].Get<TileWallWireStateData>().HasTile = true;
				Main.tile[x, y].TileType = (ushort)ModContent.TileType<SmoothVoidstone>();
				Main.tile[x, y].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
				Main.tile[x, y].Get<LiquidData>().LiquidType = 0;
			}
		}
		for (int i = shrineLeftX + 1; i <= shrineRightX - 1; i++)
		{
			for (int j = shrineTopY + 1; j <= shrineBottomY - 1; j++)
			{
				Main.tile[i, j].Get<TileWallWireStateData>().HasTile = false;
			}
		}
		for (int k = shrineLeftX; k <= shrineRightX; k++)
		{
			for (int l = shrineBottomY - 3; l <= shrineBottomY - 1; l++)
			{
				Main.tile[k, l].Get<TileWallWireStateData>().HasTile = false;
			}
		}
		int yTop = shrineTopY - 1;
		int halfWidth = 4;
		while (halfWidth > -1)
		{
			halfWidth -= WorldGen.genRand.Next(1, 3);
			for (int m = chestLeftX - halfWidth - 1; m <= chestLeftX + halfWidth; m++)
			{
				Main.tile[m, yTop].Get<TileWallWireStateData>().HasTile = true;
				Main.tile[m, yTop].TileType = (ushort)ModContent.TileType<SmoothVoidstone>();
			}
			yTop--;
		}
		Chest chest = MiscWorldgenRoutines.AddChestWithLoot(chestLeftX, chestTopY, (ushort)ModContent.TileType<VoidChest>());
		if (chest != null)
		{
			FillAbyssShrineChest(chest);
		}
	}

	public static void FillAbyssShrineChest(Chest chest)
	{
		int dropType = Utils.SelectRandom<int>(WorldGen.genRand, ModContent.ItemType<AbyssShocker>(), ModContent.ItemType<DepthCrusher>(), ModContent.ItemType<InkBomb>());
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[2] { 4870, 4479 });
		if (Main.zenithWorld)
		{
			dropType = 2337;
			potionType = 678;
		}
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<Terminus>(), 1),
			new ChestItem(dropType, 1),
			new ChestItem(ModContent.ItemType<VoidTorch>(), WorldGen.genRand.Next(100, 111)),
			new ChestItem(73, WorldGen.genRand.Next(8, 11)),
			new ChestItem(ModContent.ItemType<HadalStew>(), WorldGen.genRand.Next(10, 13)),
			new ChestItem(potionType, WorldGen.genRand.Next(10, 13))
		};
		if (Main.zenithWorld)
		{
			contents.Add(new ChestItem(5346, 1));
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].Prefix(-1);
			chest.item[i].stack = contents[i].Stack;
		}
	}
}
