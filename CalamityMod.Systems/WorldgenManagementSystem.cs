using System.Collections.Generic;
using System.Threading;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.SummonItems;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.World;
using CalamityMod.World.Planets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.Systems;

public class WorldgenManagementSystem : ModSystem
{
	public override void PreWorldGen()
	{
		Abyss.TotalPlacedIslandsSoFar = 0;
		CalamityWorld.IsWorldAfterDraedonUpdate = true;
	}

	public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
	{
		int underworldStructuresIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Smooth World"));
		if (underworldStructuresIndex != -1)
		{
			tasks.Insert(underworldStructuresIndex + 2, new PassLegacy("Shimmer Shrine", delegate(GenerationProgress progress, GameConfiguration config)
			{
				progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.ShimmerShrine").Value;
				ShimmerShrine.PlaceShimmerShrine(GenVars.structures);
			}));
		}
		int islandIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Floating Island Houses"));
		if (islandIndex != -1)
		{
			tasks.Insert(islandIndex + 2, new PassLegacy("Evil Island", delegate(GenerationProgress progress, GameConfiguration config)
			{
				progress.Message = Language.GetOrRegister(WorldGen.crimson ? "Mods.CalamityMod.UI.EvilIslandCrimson" : "Mods.CalamityMod.UI.EvilIslandCorrupt").Value;
				WorldEvilIsland.PlaceEvilIsland();
			}));
		}
		int dungeonIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Dungeon"));
		if (dungeonIndex != -1)
		{
			tasks.Insert(dungeonIndex + 1, new PassLegacy("Astral Chest", delegate(GenerationProgress progress, GameConfiguration config)
			{
				progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.AstralChest").Value;
				AstralChestGeneration.PlaceAstralChest();
			}));
		}
		int livingTreeIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Living Trees"));
		if (livingTreeIndex != -1)
		{
			tasks.Insert(livingTreeIndex + 1, new PassLegacy("Living Mahogany Tree", delegate(GenerationProgress progress, GameConfiguration config)
			{
				//IL_003a: Unknown result type (might be due to invalid IL or missing references)
				progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.LivingMahoganyTree").Value;
				int num = 0;
				while (num < 1000)
				{
					num++;
					if (GiantHive.GrowLivingJungleTree(WorldGen.RandomWorldPoint((int)Main.worldSurface + 25, 100, Main.maxTilesY - (int)Main.worldSurface - 125, 100), GenVars.structures))
					{
						break;
					}
				}
			}));
		}
		int jungleTempleIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Jungle Temple"));
		if (jungleTempleIndex != -1)
		{
			tasks[jungleTempleIndex] = new PassLegacy("Jungle Temple", delegate(GenerationProgress progress, GameConfiguration config)
			{
				progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.BetterJungleTemple").Value;
				CustomTemple.NewJungleTemple();
			});
		}
		int jungleTempleIndex2 = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Temple"));
		if (jungleTempleIndex2 != -1)
		{
			tasks[jungleTempleIndex2] = new PassLegacy("Temple", delegate(GenerationProgress progress, GameConfiguration config)
			{
				progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.BetterJungleTemple").Value;
				Main.tileSolid[162] = false;
				Main.tileSolid[226] = true;
				CustomTemple.NewJungleTemplePart2();
				Main.tileSolid[232] = false;
			});
		}
		int lihzahrdAltarIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Lihzahrd Altars"));
		if (lihzahrdAltarIndex != -1)
		{
			tasks[lihzahrdAltarIndex] = new PassLegacy("Lihzahrd Altars", delegate(GenerationProgress progress, GameConfiguration config)
			{
				progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.JungleTempleAltar").Value;
				CustomTemple.NewJungleTempleLihzahrdAltar();
			});
		}
		int giantHiveIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Hives"));
		if (giantHiveIndex != -1)
		{
			tasks.Insert(giantHiveIndex + 1, new PassLegacy("Giant Hive", delegate(GenerationProgress progress, GameConfiguration config)
			{
				//IL_0039: Unknown result type (might be due to invalid IL or missing references)
				progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.GiantBeehive").Value;
				int num = 0;
				while (num < 1000)
				{
					num++;
					if (GiantHive.CanPlaceGiantHive(WorldGen.RandomWorldPoint((int)Main.worldSurface + 25, 100, Main.maxTilesY - Main.UnderworldLayer + 125, 100), GenVars.structures))
					{
						break;
					}
				}
			}));
		}
		int spawnPointIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Spawn Point"));
		if (spawnPointIndex != -1 && WorldGen.tenthAnniversaryWorldGen && !WorldGen.remixWorldGen)
		{
			tasks.Insert(spawnPointIndex + 1, new PassLegacy("Fix Tenth Anniversary Spawn", delegate
			{
				if ((Main.spawnTileX < Main.maxTilesX / 2 && GenVars.dungeonSide == -1) || (Main.spawnTileX > Main.maxTilesX / 2 && GenVars.dungeonSide == 1))
				{
					Main.spawnTileX = Main.maxTilesX - Main.spawnTileX;
					for (int i = 0; i < Main.maxTilesY; i++)
					{
						if (Main.tile[Main.spawnTileX, i].HasTile)
						{
							Main.spawnTileY = i;
							break;
						}
					}
				}
			}));
		}
		int mechanicIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Sunflowers"));
		if (mechanicIndex != -1)
		{
			tasks.Insert(mechanicIndex + 1, new PassLegacy("Mechanic Shed", delegate(GenerationProgress progress, GameConfiguration config)
			{
				progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.MechanicShed").Value;
				MechanicShed.PlaceMechanicShed(GenVars.structures);
			}));
		}
		int vernalIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Muds Walls In Jungle"));
		if (vernalIndex != -1)
		{
			tasks.Insert(vernalIndex + 1, new PassLegacy("Vernal Pass", delegate(GenerationProgress progress, GameConfiguration config)
			{
				progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.VernalPass").Value;
				VernalPass.PlaceVernalPass(GenVars.structures);
			}));
		}
		int SunkenSeaIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Settle Liquids Again"));
		if (SunkenSeaIndex != -1)
		{
			tasks.Insert(SunkenSeaIndex + 1, new PassLegacy("Sunken Sea", delegate(GenerationProgress progress, GameConfiguration config)
			{
				//IL_002b: Unknown result type (might be due to invalid IL or missing references)
				progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.SunkenSea").Value;
				SunkenSea.Place(new Point(((Rectangle)(ref GenVars.UndergroundDesertLocation)).Left, Main.maxTilesY - 400));
			}));
		}
		int finalIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Final Cleanup"));
		if (finalIndex == -1)
		{
			return;
		}
		int currentFinalIndex = finalIndex - 1;
		tasks.Insert(++currentFinalIndex, new PassLegacy("Gem Depth Adjustment", delegate(GenerationProgress progress, GameConfiguration config)
		{
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.GemAdjustment").Value;
			MiscWorldgenRoutines.SmartGemGen();
		}));
		tasks.Insert(++currentFinalIndex, new PassLegacy("Forsaken Archive", delegate(GenerationProgress progress, GameConfiguration config)
		{
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.DungeonArchive").Value;
			DungeonArchive.PlaceArchive();
		}));
		tasks.Insert(++currentFinalIndex, new PassLegacy("Planetoids", Planetoid.GenerateAllBasePlanetoids));
		int sulphurIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Micro Biomes"));
		if (sulphurIndex != -1)
		{
			tasks.Insert(sulphurIndex + 1, new PassLegacy("Sulphur Sea", delegate(GenerationProgress progress, GameConfiguration config)
			{
				progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.SulphurSea").Value;
				SulphurousSea.PlaceSulphurSea();
			}));
		}
		tasks.Insert(++currentFinalIndex, new PassLegacy("Brimstone Crag", delegate(GenerationProgress progress, GameConfiguration config)
		{
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.BrimstoneCrags").Value;
			BrimstoneCrag.GenAllCragsStuff();
		}));
		tasks.Insert(++currentFinalIndex, new PassLegacy("Special Shrines", delegate(GenerationProgress progress, GameConfiguration config)
		{
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.HiddenShrines").Value;
			if (WorldGen.crimson || Main.drunkWorld)
			{
				progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.CrimsonShrine").Value;
				UndergroundShrines.PlaceCrimsonShrine(GenVars.structures);
			}
			if (!WorldGen.crimson || Main.drunkWorld)
			{
				progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.CorruptShrine").Value;
				UndergroundShrines.PlaceCorruptionShrine(GenVars.structures);
			}
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.DesertShrine").Value;
			UndergroundShrines.PlaceDesertShrine(GenVars.structures);
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.GraniteShrine").Value;
			UndergroundShrines.PlaceGraniteShrine(GenVars.structures);
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.IceShrine").Value;
			UndergroundShrines.PlaceIceShrine(GenVars.structures);
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.MarbleShrine").Value;
			UndergroundShrines.PlaceMarbleShrine(GenVars.structures);
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.MushroomShrine").Value;
			UndergroundShrines.PlaceMushroomShrine(GenVars.structures);
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.SurfaceShrine").Value;
			UndergroundShrines.PlaceSurfaceShrine(GenVars.structures);
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.Roxcalibur").Value;
			UndergroundShrines.PlaceRoxShrine(GenVars.structures);
		}));
		tasks.Insert(++currentFinalIndex, new PassLegacy("Aerialite", delegate(GenerationProgress progress, GameConfiguration config)
		{
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.Aerialite").Value;
			AerialiteOreGen.Generate();
		}));
		tasks.Insert(++currentFinalIndex, new PassLegacy("Draedon Structures", delegate(GenerationProgress progress, GameConfiguration config)
		{
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_0133: Unknown result type (might be due to invalid IL or missing references)
			//IL_016e: Unknown result type (might be due to invalid IL or missing references)
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.DraedonLabs").Value;
			List<Point> list = new List<Point>();
			int num = Main.maxTilesX / 900;
			int num2 = Main.maxTilesX / 1500;
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.HellLab").Value;
			DraedonStructures.PlaceHellLab(out var placementPoint, list, GenVars.structures);
			list.Add(placementPoint);
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.SunkenLab").Value;
			DraedonStructures.PlaceSunkenSeaLab(out var placementPoint2, list, GenVars.structures);
			list.Add(placementPoint2);
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.IceLab").Value;
			DraedonStructures.PlaceIceLab(out var placementPoint3, list, GenVars.structures);
			list.Add(placementPoint3);
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.PlagueLab").Value;
			DraedonStructures.PlacePlagueLab(out var placementPoint4, list, GenVars.structures);
			list.Add(placementPoint4);
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.CavernLab").Value;
			DraedonStructures.PlaceCavernLab(out var placementPoint5, list, GenVars.structures);
			list.Add(placementPoint5);
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.DraedonWorkshop").Value;
			for (int i = 0; i < num; i++)
			{
				DraedonStructures.PlaceWorkshop(out var placementPoint6, list, GenVars.structures);
				list.Add(placementPoint6);
			}
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.DraedonFacility").Value;
			for (int j = 0; j < num2; j++)
			{
				DraedonStructures.PlaceResearchFacility(out var placementPoint7, list, GenVars.structures);
				list.Add(placementPoint7);
			}
		}));
		tasks.Insert(++currentFinalIndex, new PassLegacy("Abyss", delegate(GenerationProgress progress, GameConfiguration config)
		{
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.Abyss").Value;
			Abyss.PlaceAbyss();
			Abyss.AbyssCleanup();
		}));
		tasks.Insert(++currentFinalIndex, new PassLegacy("Sulphur Sea 2", delegate(GenerationProgress progress, GameConfiguration config)
		{
			progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.SulphurSea2").Value;
			SulphurousSea.SulphurSeaGenerationAfterAbyss();
		}));
		if (Main.noTrapsWorld)
		{
			tasks.Insert(++currentFinalIndex, new PassLegacy("Auric Land Mines", delegate(GenerationProgress progress, GameConfiguration config)
			{
				progress.Message = Language.GetOrRegister("Mods.CalamityMod.UI.AuricLandMines").Value;
				MiscWorldgenRoutines.GenerateAuricLandMines();
			}));
		}
	}

	public override void ModifyHardmodeTasks(List<GenPass> tasks)
	{
		int announceIndex = tasks.FindIndex((GenPass match) => match.Name == "Hardmode Announcement");
		PassLegacy hardmodeOreT1Pass = new PassLegacy("CalamityMod:EarlyHMRework_HardmodeOreTier1", delegate
		{
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			string value = CalamityMod.Instance.GetLocalization("Status.Progression.HardmodeOreTier1Text").Value;
			Color value2 = default(Color);
			((Color)(ref value2))._002Ector(50, 255, 130);
			CalamityUtils.SpawnOre(107, 0.00012, 0.45f, 0.7f, 3, 8);
			CalamityUtils.SpawnOre(221, 0.00012, 0.45f, 0.7f, 3, 8);
			CalamityUtils.BroadcastLocalizedText(value, value2);
		});
		if (!CalamityServerConfig.Instance.EarlyHardmodeProgressionRework)
		{
			hardmodeOreT1Pass.Disable();
		}
		tasks.Insert(announceIndex, hardmodeOreT1Pass);
		tasks.Insert(announceIndex, new PassLegacy("AstralMeteor", delegate
		{
			ThreadPool.QueueUserWorkItem(delegate
			{
				AstralBiome.PlaceAstralMeteor();
			});
		}));
	}

	public override void PostWorldGen()
	{
		for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
		{
			Chest chest = Main.chest[chestIndex];
			if (chest == null)
			{
				continue;
			}
			bool num = Main.tile[chest.x, chest.y].TileType == 21;
			bool isContainer2 = Main.tile[chest.x, chest.y].TileType == 467;
			bool isBrownChest = num && Main.tile[chest.x, chest.y].TileFrameX == 0;
			bool isGoldChest = num && (Main.tile[chest.x, chest.y].TileFrameX == 36 || Main.tile[chest.x, chest.y].TileFrameX == 72);
			bool isMahoganyChest = num && Main.tile[chest.x, chest.y].TileFrameX == 288;
			bool isIvyChest = num && Main.tile[chest.x, chest.y].TileFrameX == 360;
			bool isIceChest = num && Main.tile[chest.x, chest.y].TileFrameX == 396;
			bool isLihzahrdChest = num && Main.tile[chest.x, chest.y].TileFrameX == 576;
			bool isMushroomChest = num && Main.tile[chest.x, chest.y].TileFrameX == 1152;
			bool isMarniteChest = num && (Main.tile[chest.x, chest.y].TileFrameX == 1800 || Main.tile[chest.x, chest.y].TileFrameX == 1836);
			bool isDeadManChest = isContainer2 && Main.tile[chest.x, chest.y].TileFrameX == 144;
			bool isSandstoneChest = isContainer2 && Main.tile[chest.x, chest.y].TileFrameX == 360;
			if (isBrownChest | isGoldChest | isMahoganyChest | isIvyChest | isIceChest | isLihzahrdChest | isMushroomChest | isMarniteChest | isDeadManChest | isSandstoneChest)
			{
				for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
				{
					if (chest.item[inventoryIndex].type != 43)
					{
						continue;
					}
					if (isMushroomChest)
					{
						chest.item[inventoryIndex].SetDefaults(4764);
						chest.item[inventoryIndex].Prefix(-1);
						break;
					}
					if (isGoldChest)
					{
						chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<EnchantedKnifeStaff>());
						chest.item[inventoryIndex].Prefix(-1);
						break;
					}
					float rng = WorldGen.genRand.NextFloat();
					if (rng < 0.2f)
					{
						chest.item[inventoryIndex].SetDefaults(298);
						chest.item[inventoryIndex].stack = WorldGen.genRand.Next(5, 10);
					}
					else if (rng < 0.4f)
					{
						chest.item[inventoryIndex].SetDefaults(2325);
						chest.item[inventoryIndex].stack = WorldGen.genRand.Next(2, 4);
					}
					else
					{
						chest.item[inventoryIndex].SetDefaults(2322);
						chest.item[inventoryIndex].stack = WorldGen.genRand.Next(3, 6);
					}
					break;
				}
			}
			if (isBrownChest && chest.item[0].type == 4341)
			{
				chest.item[0].SetDefaults(ModContent.ItemType<Kylie>());
				chest.item[0].Prefix(-1);
			}
			if (!isSandstoneChest)
			{
				continue;
			}
			float rng2 = WorldGen.genRand.NextFloat();
			if (rng2 < 0.2f)
			{
				for (int i = 0; i < 40; i++)
				{
					if (chest.item[i].IsAir)
					{
						chest.item[i].SetDefaults(ModContent.ItemType<DesertMedallion>());
						chest.item[i].stack = 1;
						break;
					}
				}
			}
			else
			{
				if (!(rng2 < 0.4f))
				{
					continue;
				}
				for (int j = 0; j < 40; j++)
				{
					if (chest.item[j].IsAir)
					{
						chest.item[j].SetDefaults(ModContent.ItemType<TheComb>());
						chest.item[j].stack = 1;
						break;
					}
				}
			}
		}
	}
}
