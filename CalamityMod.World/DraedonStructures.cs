using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.DraedonMisc;
using CalamityMod.Items.LabFinders;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Schematics;
using CalamityMod.Tiles.SunkenSea;
using CalamityMod.Walls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public static class DraedonStructures
{
	public const int HellVerticalAvoidance = 100;

	public static bool ShouldAvoidLocation(Point placementPoint, bool careAboutLava = true)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = CalamityUtils.ParanoidTileRetrieval(placementPoint.X, placementPoint.Y);
		if ((tile.LiquidType == 1) & careAboutLava)
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
		if (tile.TileType == 203 || tile.WallType == 83 || tile.TileType == 25 || tile.WallType == 3)
		{
			return true;
		}
		if (tile.TileType == ModContent.TileType<Basalt>() || tile.TileType == ModContent.TileType<Navystone>() || tile.TileType == ModContent.TileType<HardenedEutrophicSand>() || tile.TileType == ModContent.TileType<Shellstone>() || tile.TileType == ModContent.TileType<EutrophicSand>() || tile.TileType == ModContent.TileType<Limestone>() || tile.TileType == ModContent.TileType<PolypSand>() || tile.TileType == ModContent.TileType<ScarletSeaGrassTile>() || tile.TileType == ModContent.TileType<LimestoneCobble>() || tile.TileType == ModContent.TileType<Runestone>() || tile.TileType == ModContent.TileType<Dunesand>() || tile.WallType == ModContent.WallType<NavystoneWall>() || tile.WallType == ModContent.WallType<ShellstoneWall>() || tile.WallType == ModContent.WallType<LimestoneWall>() || tile.WallType == ModContent.WallType<RunestoneWall>())
		{
			return true;
		}
		return false;
	}

	public static void FillWorkshopChest(Chest chest)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[4] { 2346, 305, 2323, 2345 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<DubiousPlating>(), WorldGen.genRand.Next(8, 15)),
			new ChestItem(ModContent.ItemType<MysteriousCircuitry>(), WorldGen.genRand.Next(7, 13)),
			new ChestItem(ModContent.ItemType<SuspiciousScrap>(), 1),
			new ChestItem(8, WorldGen.genRand.Next(15, 30)),
			new ChestItem(73, WorldGen.genRand.Next(5, 12)),
			new ChestItem(188, WorldGen.genRand.Next(5, 8)),
			new ChestItem(166, WorldGen.genRand.Next(6, 8)),
			new ChestItem(potionType, WorldGen.genRand.Next(3, 6))
		};
		float rng = WorldGen.genRand.NextFloat();
		if (rng < 0.5f)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<LabSeekingMechanism>(), 1));
		}
		else if (rng < 0.6f)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<CyanSeekingMechanism>(), 1));
		}
		else if (rng < 0.7f)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<GreenSeekingMechanism>(), 1));
		}
		else if (rng < 0.8f)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<WhiteSeekingMechanism>(), 1));
		}
		else if (rng < 0.9f)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<RedSeekingMechanism>(), 1));
		}
		else
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<YellowSeekingMechanism>(), 1));
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlaceWorkshop(out Point placementPoint, List<Point> workshopPoints, StructureMap structures)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = "Rusted Workshop";
		Vector2 schematicSize = default(Vector2);
		do
		{
			int underworldTop = Main.UnderworldLayer;
			int placementPositionX = WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.1f), (int)((float)Main.maxTilesX * 0.9f));
			int placementPositionY = WorldGen.genRand.Next(underworldTop - 550, underworldTop - 50);
			placementPoint = new Point(placementPositionX, placementPositionY);
			((Vector2)(ref schematicSize))._002Ector((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1));
			int xCheckArea = 40;
			bool canGenerateInLocation = true;
			bool nearbyOtherWorkshop = workshopPoints.Any(delegate(Point point)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0014: Unknown result type (might be due to invalid IL or missing references)
				return Vector2.Distance(point.ToVector2(), new Vector2((float)placementPositionX, (float)placementPositionY)) < 240f;
			});
			for (int x = placementPoint.X - xCheckArea; (float)x < (float)placementPoint.X + schematicSize.X + (float)xCheckArea; x++)
			{
				for (int y = placementPoint.Y; (float)y < (float)placementPoint.Y + schematicSize.Y; y++)
				{
					CalamityUtils.ParanoidTileRetrieval(x, y);
					if (ShouldAvoidLocation(new Point(x, y)))
					{
						canGenerateInLocation = false;
					}
				}
			}
			if ((!canGenerateInLocation | nearbyOtherWorkshop) || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y)))
			{
				tries++;
				continue;
			}
			bool _ = true;
			SchematicManager.PlaceSchematic<Action<Chest>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref _, FillWorkshopChest);
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 20);
			break;
		}
		while (tries <= 10000);
	}

	public static void FillLaboratoryChest(Chest chest)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[4] { 2346, 305, 2323, 2345 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<DubiousPlating>(), WorldGen.genRand.Next(10, 18)),
			new ChestItem(ModContent.ItemType<MysteriousCircuitry>(), WorldGen.genRand.Next(10, 16)),
			new ChestItem(ModContent.ItemType<SuspiciousScrap>(), 1),
			new ChestItem(8, WorldGen.genRand.Next(20, 41)),
			new ChestItem(73, WorldGen.genRand.Next(8, 17)),
			new ChestItem(188, WorldGen.genRand.Next(7, 11)),
			new ChestItem(167, WorldGen.genRand.Next(4, 7)),
			new ChestItem(potionType, WorldGen.genRand.Next(4, 8))
		};
		float rng = WorldGen.genRand.NextFloat();
		if (rng < 0.5f)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<LabSeekingMechanism>(), 1));
		}
		else if (rng < 0.6f)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<CyanSeekingMechanism>(), 1));
		}
		else if (rng < 0.7f)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<GreenSeekingMechanism>(), 1));
		}
		else if (rng < 0.8f)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<WhiteSeekingMechanism>(), 1));
		}
		else if (rng < 0.9f)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<RedSeekingMechanism>(), 1));
		}
		else
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<YellowSeekingMechanism>(), 1));
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlaceResearchFacility(out Point placementPoint, List<Point> workshopPoints, StructureMap structures)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = "Research Outpost";
		Vector2 schematicSize = default(Vector2);
		do
		{
			int underworldTop = Main.UnderworldLayer;
			int placementPositionX = WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.15f), (int)((float)Main.maxTilesX * 0.85f));
			int placementPositionY = WorldGen.genRand.Next(underworldTop - 400, underworldTop - 50);
			placementPoint = new Point(placementPositionX, placementPositionY);
			((Vector2)(ref schematicSize))._002Ector((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1));
			int xCheckArea = 30;
			bool canGenerateInLocation = true;
			bool nearbyOtherWorkshop = workshopPoints.Any(delegate(Point point)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0014: Unknown result type (might be due to invalid IL or missing references)
				return Vector2.Distance(point.ToVector2(), new Vector2((float)placementPositionX, (float)placementPositionY)) < 240f;
			});
			for (int x = placementPoint.X - xCheckArea; (float)x < (float)placementPoint.X + schematicSize.X + (float)xCheckArea; x++)
			{
				for (int y = placementPoint.Y; (float)y < (float)placementPoint.Y + schematicSize.Y; y++)
				{
					CalamityUtils.ParanoidTileRetrieval(x, y);
					if (ShouldAvoidLocation(new Point(x, y)))
					{
						canGenerateInLocation = false;
					}
				}
			}
			if ((!canGenerateInLocation | nearbyOtherWorkshop) || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y)))
			{
				tries++;
				continue;
			}
			bool _ = true;
			SchematicManager.PlaceSchematic<Action<Chest>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref _, FillLaboratoryChest);
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 20);
			break;
		}
		while (tries <= 10000);
	}

	public static void FillHellLaboratoryChest(Chest chest, int type, bool hasPlacedMurasama)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[4] { 2346, 305, 2323, 2345 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<DubiousPlating>(), WorldGen.genRand.Next(8, 15)),
			new ChestItem(ModContent.ItemType<MysteriousCircuitry>(), WorldGen.genRand.Next(7, 13)),
			new ChestItem(8, WorldGen.genRand.Next(15, 30)),
			new ChestItem(73, WorldGen.genRand.Next(5, 12)),
			new ChestItem(188, WorldGen.genRand.Next(5, 8)),
			new ChestItem(166, WorldGen.genRand.Next(6, 8)),
			new ChestItem(potionType, WorldGen.genRand.Next(3, 6))
		};
		contents.Insert(0, new ChestItem(ModContent.ItemType<WhiteSeekingMechanism>(), 1));
		if (!hasPlacedMurasama)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<DraedonsLogHell>(), 1));
			contents.Insert(1, new ChestItem(ModContent.ItemType<Murasama>(), 1));
			contents.Insert(2, new ChestItem(ModContent.ItemType<EncryptedSchematicHell>(), 1));
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].Prefix(-1);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlaceHellLab(out Point placementPoint, List<Point> workshopPoints, StructureMap structures)
	{
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = "Hell Laboratory";
		SchematicManager.PilePlacementMaps.TryGetValue(mapKey, out var _);
		SchematicMetaTile[,] schematic = SchematicManager.TileMaps[mapKey];
		Vector2 schematicSize = default(Vector2);
		do
		{
			_ = Main.UnderworldLayer;
			bool num = Main.dungeonX > Main.maxTilesX / 2;
			int midLeft = (Main.remixWorld ? 6 : 9);
			float midRight = (Main.remixWorld ? 0.6f : 0.82f);
			int placementPositionX = (num ? WorldGen.genRand.Next(Main.maxTilesX / 12, Main.maxTilesX / midLeft) : WorldGen.genRand.Next((int)((float)Main.maxTilesX * midRight), (int)((double)Main.maxTilesX * 0.925)));
			if (Main.drunkWorld && !Main.remixWorld)
			{
				placementPositionX = WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.4f), (int)((float)Main.maxTilesX * 0.6f));
			}
			int placementPositionY = WorldGen.genRand.Next(Main.maxTilesY - 150, Main.maxTilesY - 125);
			placementPoint = new Point(placementPositionX, placementPositionY);
			((Vector2)(ref schematicSize))._002Ector((float)schematic.GetLength(0), (float)schematic.GetLength(1));
			int xCheckArea = 30;
			bool canGenerateInLocation = true;
			for (int x = placementPoint.X - xCheckArea; (float)x < (float)placementPoint.X + schematicSize.X + (float)xCheckArea; x++)
			{
				for (int y = placementPoint.Y; (float)y < (float)placementPoint.Y + schematicSize.Y; y++)
				{
					CalamityUtils.ParanoidTileRetrieval(x, y);
					if (ShouldAvoidLocation(new Point(x, y), careAboutLava: false))
					{
						canGenerateInLocation = false;
					}
				}
			}
			if (!canGenerateInLocation || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y)))
			{
				tries++;
				continue;
			}
			bool hasPlacedMurasama = false;
			SchematicManager.PlaceSchematic<Action<Chest, int, bool>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref hasPlacedMurasama, FillHellLaboratoryChest);
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 20);
			CalamityWorld.HellLabCenter = placementPoint.ToWorldCoordinates() + new Vector2((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1)) * 8f;
			break;
		}
		while (tries <= 50000);
	}

	public static void FillSunkenSeaLaboratoryChest(Chest chest, int type, bool hasPlacedLogAndSchematic)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[4] { 2346, 305, 2323, 2345 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<DubiousPlating>(), WorldGen.genRand.Next(8, 15)),
			new ChestItem(ModContent.ItemType<MysteriousCircuitry>(), WorldGen.genRand.Next(7, 13)),
			new ChestItem(8, WorldGen.genRand.Next(15, 30)),
			new ChestItem(73, WorldGen.genRand.Next(5, 12)),
			new ChestItem(188, WorldGen.genRand.Next(5, 8)),
			new ChestItem(166, WorldGen.genRand.Next(6, 8)),
			new ChestItem(potionType, WorldGen.genRand.Next(3, 6))
		};
		contents.Insert(0, new ChestItem(ModContent.ItemType<YellowSeekingMechanism>(), 1));
		if (!hasPlacedLogAndSchematic)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<DraedonsLogSunkenSea>(), 1));
			contents.Insert(1, new ChestItem(ModContent.ItemType<EncryptedSchematicSunkenSea>(), 1));
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlaceSunkenSeaLab(out Point placementPoint, List<Point> workshopPoints, StructureMap structures)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		string mapKey = "Sunken Sea Laboratory";
		SchematicManager.PilePlacementMaps.TryGetValue(mapKey, out var _);
		SchematicMetaTile[,] schematic = SchematicManager.TileMaps[mapKey];
		schematic.GetLength(0);
		int labHeight = schematic.GetLength(1);
		int sunkenSeaX = (((Rectangle)(ref GenVars.UndergroundDesertLocation)).Left + ((Rectangle)(ref GenVars.UndergroundDesertLocation)).Right) / 2;
		int num = Main.maxTilesY / 2;
		int placementPositionX = ((sunkenSeaX < Main.maxTilesX / 2) ? (sunkenSeaX - 120) : (sunkenSeaX + 120));
		int placementPositionY = num + Main.maxTilesY / 4 - 25 - labHeight;
		placementPoint = new Point(placementPositionX, placementPositionY);
		Vector2 schematicSize = default(Vector2);
		((Vector2)(ref schematicSize))._002Ector((float)schematic.GetLength(0), (float)schematic.GetLength(1));
		bool hasPlacedLogAndSchematic = false;
		SchematicManager.PlaceSchematic<Action<Chest, int, bool>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref hasPlacedLogAndSchematic, FillSunkenSeaLaboratoryChest);
		CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 20);
		CalamityWorld.SunkenSeaLabCenter = placementPoint.ToWorldCoordinates() + new Vector2((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1)) * 8f;
	}

	public static void FillIceLaboratoryChest(Chest chest, int type, bool hasPlacedLogAndSchematic)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[4] { 2346, 305, 2323, 2345 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<DubiousPlating>(), WorldGen.genRand.Next(8, 15)),
			new ChestItem(ModContent.ItemType<MysteriousCircuitry>(), WorldGen.genRand.Next(7, 13)),
			new ChestItem(8, WorldGen.genRand.Next(15, 30)),
			new ChestItem(73, WorldGen.genRand.Next(5, 12)),
			new ChestItem(188, WorldGen.genRand.Next(5, 8)),
			new ChestItem(166, WorldGen.genRand.Next(6, 8)),
			new ChestItem(potionType, WorldGen.genRand.Next(3, 6))
		};
		contents.Insert(0, new ChestItem(ModContent.ItemType<LabSeekingMechanism>(), 1));
		if (!hasPlacedLogAndSchematic)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<DraedonsLogSnowBiome>(), 1));
			contents.Insert(1, new ChestItem(ModContent.ItemType<EncryptedSchematicIce>(), 1));
		}
		if (type == 21)
		{
			contents.Insert(0, new ChestItem(1861, 1));
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlaceIceLab(out Point placementPoint, List<Point> workshopPoints, StructureMap structures)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = "Ice Laboratory";
		SchematicManager.PilePlacementMaps.TryGetValue(mapKey, out var _);
		SchematicMetaTile[,] schematic = SchematicManager.TileMaps[mapKey];
		Vector2 schematicSize = default(Vector2);
		do
		{
			int underworldTop = Main.UnderworldLayer;
			int placementPositionX = WorldGen.genRand.Next(120, Main.maxTilesX - 120);
			int placementPositionY = WorldGen.genRand.Next((int)Main.worldSurface + 160, underworldTop - 100);
			placementPoint = new Point(placementPositionX, placementPositionY);
			((Vector2)(ref schematicSize))._002Ector((float)schematic.GetLength(0), (float)schematic.GetLength(1));
			int activeTilesInArea = 0;
			int iceTilesInArea = 0;
			int xCheckArea = 30;
			bool canGenerateInLocation = true;
			bool nearbyOtherWorkshop = workshopPoints.Any(delegate(Point point)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0014: Unknown result type (might be due to invalid IL or missing references)
				return Vector2.Distance(point.ToVector2(), new Vector2((float)placementPositionX, (float)placementPositionY)) < 180f;
			});
			float totalTiles = (schematicSize.X + (float)(xCheckArea * 2)) * schematicSize.Y;
			for (int x = placementPoint.X - xCheckArea; (float)x < (float)placementPoint.X + schematicSize.X + (float)xCheckArea; x++)
			{
				for (int y = placementPoint.Y; (float)y < (float)placementPoint.Y + schematicSize.Y; y++)
				{
					Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
					if (tile.HasTile)
					{
						if (tile.TileType == 147 || tile.TileType == 161)
						{
							iceTilesInArea++;
						}
						activeTilesInArea++;
					}
					if (ShouldAvoidLocation(new Point(x, y)))
					{
						canGenerateInLocation = false;
					}
				}
			}
			if (Main.drunkWorld)
			{
				iceTilesInArea *= 3;
			}
			if ((!canGenerateInLocation | nearbyOtherWorkshop) || (float)iceTilesInArea < totalTiles * 0.35f || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y)))
			{
				tries++;
				continue;
			}
			bool hasPlacedLogAndSchematic = false;
			SchematicManager.PlaceSchematic<Action<Chest, int, bool>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref hasPlacedLogAndSchematic, FillIceLaboratoryChest);
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 20);
			CalamityWorld.IceLabCenter = placementPoint.ToWorldCoordinates() + new Vector2((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1)) * 8f;
			break;
		}
		while (tries <= 20000);
	}

	public static void FillPlagueLaboratoryChest(Chest chest, int type, bool hasPlacedLogAndSchematic)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[4] { 2346, 305, 2323, 2345 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<DubiousPlating>(), WorldGen.genRand.Next(8, 15)),
			new ChestItem(ModContent.ItemType<MysteriousCircuitry>(), WorldGen.genRand.Next(7, 13)),
			new ChestItem(8, WorldGen.genRand.Next(15, 30)),
			new ChestItem(73, WorldGen.genRand.Next(5, 12)),
			new ChestItem(188, WorldGen.genRand.Next(5, 8)),
			new ChestItem(166, WorldGen.genRand.Next(6, 8)),
			new ChestItem(potionType, WorldGen.genRand.Next(3, 6))
		};
		contents.Insert(0, new ChestItem(ModContent.ItemType<RedSeekingMechanism>(), 1));
		if (!hasPlacedLogAndSchematic)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<DraedonsLogJungle>(), 1));
			contents.Insert(1, new ChestItem(ModContent.ItemType<EncryptedSchematicJungle>(), 1));
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlacePlagueLab(out Point placementPoint, List<Point> workshopPoints, StructureMap structures)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = "Plague Laboratory";
		SchematicManager.PilePlacementMaps.TryGetValue(mapKey, out var _);
		SchematicMetaTile[,] schematic = SchematicManager.TileMaps[mapKey];
		Vector2 schematicSize = default(Vector2);
		do
		{
			int underworldTop = Main.UnderworldLayer;
			int placementPositionX = WorldGen.genRand.Next(120, Main.maxTilesX - 120);
			int placementPositionY = WorldGen.genRand.Next((int)Main.worldSurface + 160, underworldTop - 100);
			placementPoint = new Point(placementPositionX, placementPositionY);
			((Vector2)(ref schematicSize))._002Ector((float)schematic.GetLength(0), (float)schematic.GetLength(1));
			int activeTilesInArea = 0;
			int jungleTilesInArea = 0;
			int xCheckArea = 30;
			bool canGenerateInLocation = true;
			bool nearbyOtherWorkshop = workshopPoints.Any(delegate(Point point)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0014: Unknown result type (might be due to invalid IL or missing references)
				return Vector2.Distance(point.ToVector2(), new Vector2((float)placementPositionX, (float)placementPositionY)) < 200f;
			});
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
					if (tile.HasTile)
					{
						if (tile.TileType == 59 || tile.TileType == 60)
						{
							jungleTilesInArea++;
						}
						if (tile.TileType == 70 || tile.TileType == 71 || tile.TileType == 528)
						{
							jungleTilesInArea -= 10;
						}
						activeTilesInArea++;
					}
				}
			}
			if (Main.drunkWorld)
			{
				jungleTilesInArea *= 3;
			}
			if ((!canGenerateInLocation | nearbyOtherWorkshop) || (float)jungleTilesInArea < totalTiles * 0.4f || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y)))
			{
				tries++;
				continue;
			}
			bool hasPlacedLogAndSchematic = false;
			SchematicManager.PlaceSchematic<Action<Chest, int, bool>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref hasPlacedLogAndSchematic, FillPlagueLaboratoryChest);
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 20);
			CalamityWorld.JungleLabCenter = placementPoint.ToWorldCoordinates() + new Vector2((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1)) * 8f;
			break;
		}
		while (tries <= 20000);
	}

	public static void FillPlanetoidLaboratoryChest(Chest chest, int type, bool hasPlacedLogAndSchematic)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[4] { 2346, 305, 2323, 2345 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<DubiousPlating>(), WorldGen.genRand.Next(8, 15)),
			new ChestItem(ModContent.ItemType<MysteriousCircuitry>(), WorldGen.genRand.Next(7, 13)),
			new ChestItem(3093, WorldGen.genRand.Next(12, 18)),
			new ChestItem(3219, WorldGen.genRand.Next(5, 10)),
			new ChestItem(3222, WorldGen.genRand.Next(5, 10)),
			new ChestItem(3216, WorldGen.genRand.Next(5, 10)),
			new ChestItem(3221, WorldGen.genRand.Next(5, 10)),
			new ChestItem(3220, WorldGen.genRand.Next(5, 10)),
			new ChestItem(8, WorldGen.genRand.Next(15, 30)),
			new ChestItem(73, WorldGen.genRand.Next(5, 12)),
			new ChestItem(188, WorldGen.genRand.Next(5, 8)),
			new ChestItem(166, WorldGen.genRand.Next(6, 8)),
			new ChestItem(potionType, WorldGen.genRand.Next(3, 6))
		};
		if (!WorldGen.crimson)
		{
			contents.Insert(8, new ChestItem(3217, WorldGen.genRand.Next(5, 10)));
		}
		else
		{
			contents.Insert(8, new ChestItem(3218, WorldGen.genRand.Next(5, 10)));
		}
		Mod thorium = ExternalMods.thorium;
		if (thorium != null)
		{
			ModItem marineKelpPlanterBox = thorium.Find<ModItem>("MarineKelpPlanterBox");
			contents.Add(new ChestItem(marineKelpPlanterBox.Type, WorldGen.genRand.Next(5, 10)));
		}
		else
		{
			CalamityMod.Log.Warn((object)"Could not find Thorium Marine Kelp Planter Box. This item will not be added to the Draedon Planetoid Lab.");
		}
		contents.Insert(0, new ChestItem(ModContent.ItemType<GreenSeekingMechanism>(), 1));
		if (!hasPlacedLogAndSchematic)
		{
			contents.Insert(0, new ChestItem(ModContent.ItemType<DraedonsLogPlanetoid>(), 1));
			contents.Insert(1, new ChestItem(ModContent.ItemType<EncryptedSchematicPlanetoid>(), 1));
			contents.Insert(2, new ChestItem(ModContent.ItemType<PlasmaDriveCore>(), 1));
		}
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void FillCavernLaboratoryChest(Chest chest)
	{
		int potionType = Utils.SelectRandom(WorldGen.genRand, new short[4] { 2346, 305, 2323, 2345 });
		List<ChestItem> contents = new List<ChestItem>
		{
			new ChestItem(ModContent.ItemType<DubiousPlating>(), WorldGen.genRand.Next(10, 18)),
			new ChestItem(ModContent.ItemType<MysteriousCircuitry>(), WorldGen.genRand.Next(10, 16)),
			new ChestItem(8, WorldGen.genRand.Next(20, 41)),
			new ChestItem(73, WorldGen.genRand.Next(8, 17)),
			new ChestItem(188, WorldGen.genRand.Next(7, 11)),
			new ChestItem(167, WorldGen.genRand.Next(4, 7)),
			new ChestItem(potionType, WorldGen.genRand.Next(4, 8)),
			new ChestItem(ModContent.ItemType<LabSeekingMechanism>(), 1)
		};
		for (int i = 0; i < contents.Count; i++)
		{
			chest.item[i].SetDefaults(contents[i].Type);
			chest.item[i].stack = contents[i].Stack;
		}
	}

	public static void PlaceCavernLab(out Point placementPoint, List<Point> workshopPoints, StructureMap structures)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		int tries = 0;
		string mapKey = "Cavern Laboratory";
		Vector2 schematicSize = default(Vector2);
		do
		{
			int placementPositionX = WorldGen.genRand.Next((int)((float)Main.maxTilesX * 0.3f), (int)((float)Main.maxTilesX * 0.7f));
			int placementPositionY = WorldGen.genRand.Next((int)((float)Main.maxTilesY * 0.55f), (int)((float)Main.maxTilesY * 0.8f));
			placementPoint = new Point(placementPositionX, placementPositionY);
			((Vector2)(ref schematicSize))._002Ector((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1));
			int plainTilesInArea = 0;
			int xCheckArea = 30;
			bool canGenerateInLocation = true;
			bool nearbyOtherWorkshop = workshopPoints.Any(delegate(Point point)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0014: Unknown result type (might be due to invalid IL or missing references)
				return Vector2.Distance(point.ToVector2(), new Vector2((float)placementPositionX, (float)placementPositionY)) < 240f;
			});
			float totalTiles = (schematicSize.X + (float)(xCheckArea * 2)) * schematicSize.Y;
			for (int x = placementPoint.X - xCheckArea; (float)x < (float)placementPoint.X + schematicSize.X + (float)xCheckArea; x++)
			{
				for (int y = placementPoint.Y; (float)y < (float)placementPoint.Y + schematicSize.Y; y++)
				{
					Tile tile = CalamityUtils.ParanoidTileRetrieval(x, y);
					if (ShouldAvoidLocation(new Point(x, y), careAboutLava: false))
					{
						canGenerateInLocation = false;
					}
					if (tile.HasTile)
					{
						if (tile.TileType == 0 || tile.TileType == 1 || tile.TileType == 59 || TileID.Sets.Conversion.Moss[tile.TileType])
						{
							plainTilesInArea++;
						}
						if (tile.TileType == 60 || tile.TileType == 70)
						{
							plainTilesInArea -= 1000;
						}
					}
				}
			}
			if ((!canGenerateInLocation | nearbyOtherWorkshop) || (float)plainTilesInArea < totalTiles * 0.3f || !structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y)))
			{
				tries++;
				continue;
			}
			bool _ = true;
			SchematicManager.PlaceSchematic<Action<Chest>>(mapKey, new Point(placementPoint.X, placementPoint.Y), SchematicAnchor.TopLeft, ref _, FillCavernLaboratoryChest);
			CalamityUtils.AddProtectedStructure(new Rectangle(placementPoint.X, placementPoint.Y, (int)schematicSize.X, (int)schematicSize.Y), 20);
			CalamityWorld.CavernLabCenter = placementPoint.ToWorldCoordinates() + new Vector2((float)SchematicManager.TileMaps[mapKey].GetLength(0), (float)SchematicManager.TileMaps[mapKey].GetLength(1)) * 8f;
			break;
		}
		while (tries <= 20000);
	}
}
