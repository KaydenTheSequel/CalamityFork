using System;
using CalamityMod.CalPlayer;
using CalamityMod.Events;
using CalamityMod.NPCs;
using CalamityMod.NPCs.ExoMechs;
using CalamityMod.NPCs.PrimordialWyrm;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Tiles;
using CalamityMod.Tiles.Abyss;
using CalamityMod.Tiles.Crags;
using CalamityMod.Tiles.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using CalamityMod.Walls;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.Systems;

public class WorldMiscUpdateSystem : ModSystem
{
	public override void PostUpdateWorld()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.draedon != -1 && !NPC.AnyNPCs(ModContent.NPCType<Draedon>()))
		{
			CalamityGlobalNPC.draedon = -1;
		}
		if (CalamityWorld.DraedonMechToSummon != ExoMech.None && CalamityGlobalNPC.draedon == -1)
		{
			CalamityWorld.DraedonMechToSummon = ExoMech.None;
		}
		if (Main.netMode != 1 && CalamityWorld.DraedonSummonCountdown > 0)
		{
			CalamityWorld.DraedonSummonCountdown--;
			HandleDraedonSummoning();
		}
		Rectangle ugDesert = GenVars.UndergroundDesertLocation;
		CalamityWorld.SunkenSeaLocation = new Rectangle(((Rectangle)(ref ugDesert)).Left, ((Rectangle)(ref ugDesert)).Center.Y, ugDesert.Width, ugDesert.Height / 2);
		int closestPlayer = Player.FindClosest(new Vector2((float)(Main.maxTilesX / 2), (float)Main.worldSurface / 2f) * 16f, 0, 0);
		Player player = Main.player[closestPlayer];
		if (!BossRushEvent.DeactivateStupidFuckingBullshit)
		{
			BossRushEvent.DeactivateStupidFuckingBullshit = true;
			BossRushEvent.BossRushActive = false;
			CalamityNetcode.SyncWorld();
		}
		AcidRainEvent.TryToStartEventNaturally();
		if (AcidRainEvent.AcidRainEventIsOngoing)
		{
			AcidRainEvent.Update();
		}
		else
		{
			if (AcidRainEvent.TimeSinceEventStarted != 0)
			{
				AcidRainEvent.TimeSinceEventStarted = 0;
			}
			AcidRainEvent.HasStartedAcidicDownpour = false;
		}
		HandleTileGrowth();
		BossRushEvent.Update();
		if (player != null && player.active)
		{
			CalamityPlayer modPlayer = player.Calamity();
			TrySpawnDungeonGuardian(player);
			TrySpawnAEoW(player, modPlayer);
		}
		if (Main.rand.NextBool(100000000) && DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
		{
			string key = (Main.zenithWorld ? "Mods.CalamityMod.Status.Boss.AprilFoolsGFB" : "Mods.CalamityMod.Status.Boss.AprilFools");
			Color messageColor = Color.Crimson;
			CalamityUtils.BroadcastLocalizedText(key, messageColor);
		}
		if (!DownedBossSystem.downedDesertScourge && Main.netMode != 1 && !Main.hardMode)
		{
			CalamityWorld.StopSandstorm();
		}
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player p = enumerator.Current;
			if (!p.dead)
			{
				CalamityGlobalNPC.AttemptToSpawnLabCritters(p);
			}
		}
		TrySpawnOldMan();
		if (Main.netMode != 1)
		{
			CultistRitual.delay -= Main.dayRate * 10.0;
			CultistRitual.recheck -= Main.dayRate * 10.0;
			if (CultistRitual.recheck < 0.0)
			{
				CultistRitual.recheck = 0.0;
			}
			if (CultistRitual.delay < 0.0)
			{
				CultistRitual.delay = 0.0;
			}
		}
	}

	public static void HandleDraedonSummoning()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityWorld.DraedonSummonCountdown == 215)
		{
			Projectile.NewProjectile(new EntitySource_WorldEvent(), CalamityWorld.DraedonSummonPosition + Vector2.UnitY * 80f, Vector2.Zero, ModContent.ProjectileType<DraedonSummonLaser>(), 0, 0f);
		}
		if (CalamityWorld.DraedonSummonCountdown == 0)
		{
			NPC.NewNPC(new EntitySource_WorldEvent(), (int)CalamityWorld.DraedonSummonPosition.X, (int)CalamityWorld.DraedonSummonPosition.Y, ModContent.NPCType<Draedon>());
		}
	}

	public static void HandleTileGrowth()
	{
		double worldUpdateRate = WorldGen.GetWorldUpdateRate();
		if (worldUpdateRate == 0.0)
		{
			return;
		}
		double herbGrowthRateSurface = 3E-05f * (float)worldUpdateRate;
		double herbGrowthRateUnderground = 1.5E-05f * (float)worldUpdateRate;
		double remixWorldHerbGrowthRate = 2.5E-05f * (float)worldUpdateRate;
		int oneInXChanceToBoostHerbGrowthRate = 20;
		int oneInXChanceToBoostHerbGrowthRate_PlanterBox = 2;
		int oneInXMinimumChanceToPlaceHerb = 42280;
		double chanceAdjustmentBasedOnWorldSize = Utils.Clamp((double)Main.maxTilesX / 4200.0 - 1.0, 0.0, 1.0);
		int herbPlacementChance = (int)Utils.Lerp(15100.0, oneInXMinimumChanceToPlaceHerb, chanceAdjustmentBasedOnWorldSize);
		for (int herbPlacementIndex = 0; (double)herbPlacementIndex < (double)(Main.maxTilesX * Main.maxTilesY) * herbGrowthRateSurface; herbPlacementIndex++)
		{
			if (Main.rand.NextBool(herbPlacementChance))
			{
				WorldGen.PlantAlch();
			}
			int herbGrowthRangeX = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
			int herbGrowthRangeY = WorldGen.genRand.Next(10, (int)Main.worldSurface - 1);
			if (!Main.tileAlch[Main.tile[herbGrowthRangeX, herbGrowthRangeY].TileType])
			{
				continue;
			}
			if (Main.tile[herbGrowthRangeX, herbGrowthRangeY + 1].TileType == 380)
			{
				int herbType = Main.tile[herbGrowthRangeX, herbGrowthRangeY].TileFrameX / 18;
				int planterBoxType = Main.tile[herbGrowthRangeX, herbGrowthRangeY + 1].TileFrameY / 18;
				if (IsCorrectPlanterBox(herbType, planterBoxType) && WorldGen.genRand.NextBool(oneInXChanceToBoostHerbGrowthRate_PlanterBox))
				{
					WorldGen.GrowAlch(herbGrowthRangeX, herbGrowthRangeY);
				}
			}
			else if (WorldGen.genRand.NextBool(oneInXChanceToBoostHerbGrowthRate))
			{
				WorldGen.GrowAlch(herbGrowthRangeX, herbGrowthRangeY);
			}
		}
		if (Main.remixWorld)
		{
			for (int herbGrowthIndex = 0; (double)herbGrowthIndex < (double)(Main.maxTilesX * Main.maxTilesY) * remixWorldHerbGrowthRate; herbGrowthIndex++)
			{
				int herbGrowthRangeX2 = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
				int herbGrowthRangeY2 = WorldGen.genRand.Next((int)Main.worldSurface - 1, Main.maxTilesY - 20);
				if (!Main.tileAlch[Main.tile[herbGrowthRangeX2, herbGrowthRangeY2].TileType])
				{
					continue;
				}
				if (Main.tile[herbGrowthRangeX2, herbGrowthRangeY2 + 1].TileType == 380)
				{
					int herbType2 = Main.tile[herbGrowthRangeX2, herbGrowthRangeY2].TileFrameX / 18;
					int planterBoxType2 = Main.tile[herbGrowthRangeX2, herbGrowthRangeY2 + 1].TileFrameY / 18;
					if (IsCorrectPlanterBox(herbType2, planterBoxType2) && WorldGen.genRand.NextBool(oneInXChanceToBoostHerbGrowthRate_PlanterBox))
					{
						WorldGen.GrowAlch(herbGrowthRangeX2, herbGrowthRangeY2);
					}
				}
				else if (WorldGen.genRand.NextBool(oneInXChanceToBoostHerbGrowthRate))
				{
					WorldGen.GrowAlch(herbGrowthRangeX2, herbGrowthRangeY2);
				}
			}
		}
		else
		{
			for (int i = 0; (double)i < (double)(Main.maxTilesX * Main.maxTilesY) * herbGrowthRateUnderground; i++)
			{
				int herbGrowthRangeX3 = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
				int herbGrowthRangeY3 = WorldGen.genRand.Next((int)Main.worldSurface - 1, Main.maxTilesY - 20);
				if (!Main.tileAlch[Main.tile[herbGrowthRangeX3, herbGrowthRangeY3].TileType])
				{
					continue;
				}
				if (Main.tile[herbGrowthRangeX3, herbGrowthRangeY3 + 1].TileType == 380)
				{
					int herbType3 = Main.tile[herbGrowthRangeX3, herbGrowthRangeY3].TileFrameX / 18;
					int planterBoxType3 = Main.tile[herbGrowthRangeX3, herbGrowthRangeY3 + 1].TileFrameY / 18;
					if (IsCorrectPlanterBox(herbType3, planterBoxType3) && WorldGen.genRand.NextBool(oneInXChanceToBoostHerbGrowthRate_PlanterBox))
					{
						WorldGen.GrowAlch(herbGrowthRangeX3, herbGrowthRangeY3);
					}
				}
				else if (WorldGen.genRand.NextBool(oneInXChanceToBoostHerbGrowthRate))
				{
					WorldGen.GrowAlch(herbGrowthRangeX3, herbGrowthRangeY3);
				}
			}
		}
		int searchTop = (int)Main.worldSurface - 1;
		int searchBottom = Main.maxTilesY - 20;
		if (searchBottom <= searchTop)
		{
			return;
		}
		int l = 0;
		for (float mult2 = (float)(1.4999999621068127E-05 * worldUpdateRate); (float)l < (float)(Main.maxTilesX * Main.maxTilesY) * mult2; l++)
		{
			int x = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
			int y = WorldGen.genRand.Next(searchTop, searchBottom);
			int y2 = y - 1;
			if (y2 < 10)
			{
				y2 = 10;
			}
			if (!WorldGen.InWorld(x, y, 1) || !Main.tile[x, y].HasTile || !Main.tile[x, y].HasUnactuatedTile)
			{
				continue;
			}
			if (Main.tile[x, y].LiquidAmount <= 32 && Main.tile[x, y].TileType == 60 && Main.tile[x, y2].LiquidAmount == 0)
			{
				if (WorldGen.genRand.NextBool(1500) && Main.hardMode && (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3))
				{
					bool placeBulb = true;
					int minDistanceFromOtherBulbs = 150;
					for (int j = x - minDistanceFromOtherBulbs; j < x + minDistanceFromOtherBulbs; j += 2)
					{
						for (int k = y - minDistanceFromOtherBulbs; k < y + minDistanceFromOtherBulbs; k += 2)
						{
							if (j > 1 && j < Main.maxTilesX - 2 && k > 1 && k < Main.maxTilesY - 2 && Main.tile[j, k].HasTile && Main.tile[j, k].TileType == 238)
							{
								placeBulb = false;
								break;
							}
						}
					}
					if (placeBulb)
					{
						WorldGen.PlaceJunglePlant(x, y2, 238, 0, 0);
						WorldGen.SquareTileFrame(x, y2);
						WorldGen.SquareTileFrame(x + 2, y2);
						WorldGen.SquareTileFrame(x - 1, y2);
						if (Main.tile[x, y2].TileType == 238 && Main.dedServ)
						{
							NetMessage.SendTileSquare(-1, x, y2, 5);
						}
					}
				}
				int random = (Main.expertMode ? 90 : 120);
				if (WorldGen.genRand.NextBool(random) && Main.hardMode && !NPC.downedMechBossAny)
				{
					bool placeFruit = true;
					int minDistanceFromOtherFruit = (Main.expertMode ? 50 : 60);
					for (int m = x - minDistanceFromOtherFruit; m < x + minDistanceFromOtherFruit; m += 2)
					{
						for (int n = y - minDistanceFromOtherFruit; n < y + minDistanceFromOtherFruit; n += 2)
						{
							if (m > 1 && m < Main.maxTilesX - 2 && n > 1 && n < Main.maxTilesY - 2 && Main.tile[m, n].HasTile && Main.tile[m, n].TileType == 236)
							{
								placeFruit = false;
								break;
							}
						}
					}
					if (placeFruit)
					{
						WorldGen.PlaceJunglePlant(x, y2, 236, WorldGen.genRand.Next(3), 0);
						WorldGen.SquareTileFrame(x, y2);
						WorldGen.SquareTileFrame(x + 1, y2 + 1);
						if (Main.tile[x, y2].TileType == 236 && Main.dedServ)
						{
							NetMessage.SendTileSquare(-1, x, y2, 4);
						}
					}
				}
			}
			Tile growthTile = Main.tile[x, y];
			int tileType = growthTile.TileType;
			if (CalamityGlobalTile.GrowthTiles.Contains(tileType) && growthTile.Slope == SlopeType.Solid && !growthTile.IsHalfBlock)
			{
				int growthChance = 2;
				if (tileType == ModContent.TileType<Navystone>())
				{
					growthChance *= 5;
				}
				if (Main.rand.NextBool(growthChance))
				{
					switch (WorldGen.genRand.Next(4))
					{
					case 0:
						x++;
						break;
					case 1:
						x--;
						break;
					case 2:
						y++;
						break;
					case 3:
						y--;
						break;
					}
					if (Main.tile[x, y] != null)
					{
						Tile tile = Main.tile[x, y];
						bool num = !tile.HasTile && tile.LiquidAmount >= 128;
						bool isSunkenSeaTile = tileType == ModContent.TileType<Navystone>() || tileType == ModContent.TileType<SeaPrism>();
						bool meetsAdditionalGrowConditions = tile.Slope == SlopeType.Solid && !tile.IsHalfBlock && tile.LiquidType != 1;
						if (num & meetsAdditionalGrowConditions)
						{
							int tileType2 = ModContent.TileType<SeaPrismCrystals>();
							if (tileType == ModContent.TileType<Voidstone>())
							{
								tileType2 = ModContent.TileType<LumenylCrystals>();
							}
							if (tileType == ModContent.TileType<Shellstone>())
							{
								tileType2 = ModContent.TileType<SmallCorals>();
							}
							bool canPlaceBasedOnAttached = true;
							if (tileType2 == ModContent.TileType<SeaPrismCrystals>() && !isSunkenSeaTile)
							{
								canPlaceBasedOnAttached = false;
							}
							if (canPlaceBasedOnAttached && CanPlaceBasedOnProximity(x, y, tileType2))
							{
								tile.TileType = (ushort)tileType2;
								tile.HasTile = true;
								if (Main.tile[x, y + 1].HasTile && Main.tileSolid[Main.tile[x, y + 1].TileType] && Main.tile[x, y + 1].Slope == SlopeType.Solid && !Main.tile[x, y + 1].IsHalfBlock)
								{
									tile.TileFrameY = 0;
								}
								else if (Main.tile[x, y - 1].HasTile && Main.tileSolid[Main.tile[x, y - 1].TileType] && Main.tile[x, y - 1].Slope == SlopeType.Solid && !Main.tile[x, y - 1].IsHalfBlock)
								{
									tile.TileFrameY = 18;
								}
								else if (Main.tile[x + 1, y].HasTile && Main.tileSolid[Main.tile[x + 1, y].TileType] && Main.tile[x + 1, y].Slope == SlopeType.Solid && !Main.tile[x + 1, y].IsHalfBlock)
								{
									tile.TileFrameY = 36;
								}
								else if (Main.tile[x - 1, y].HasTile && Main.tileSolid[Main.tile[x - 1, y].TileType] && Main.tile[x - 1, y].Slope == SlopeType.Solid && !Main.tile[x - 1, y].IsHalfBlock)
								{
									tile.TileFrameY = 54;
								}
								tile.TileFrameX = (short)(WorldGen.genRand.Next(18) * 18);
								WorldGen.SquareTileFrame(x, y);
								if (Main.dedServ)
								{
									NetMessage.SendTileSquare(-1, x, y, 1);
								}
							}
						}
					}
				}
			}
			if (growthTile.LiquidAmount != 0 || y <= Main.UnderworldLayer)
			{
				continue;
			}
			bool num2 = tileType == ModContent.TileType<BrimstoneSlag>() || tileType == ModContent.TileType<BrimstoneSlab>() || tileType == ModContent.TileType<ScorchedRemains>() || tileType == ModContent.TileType<ScorchedRemainsGrass>() || tileType == ModContent.TileType<ScorchedBone>();
			int wallType = Main.tile[x, y2].WallType;
			bool isCragHouseWall = wallType == ModContent.WallType<BrimstoneSlagWall>() || wallType == ModContent.WallType<BrimstoneSlabWall>() || wallType == ModContent.WallType<ScorchedBoneWall>() || wallType == ModContent.WallType<SmoothBrimstoneSlagWall>();
			if (!(num2 & isCragHouseWall) || Main.tile[x, y2].LiquidAmount != 0 || !WorldGen.genRand.NextBool(20) || !DownedBossSystem.downedYharon)
			{
				continue;
			}
			ushort tileTypeToPlace = (ushort)ModContent.TileType<LiliesOfFinalityTile>();
			int tileTypeToPlaceThickness = 3;
			bool placeLilies = true;
			int minDistanceFromOtherLilies = 66;
			for (int num3 = x - minDistanceFromOtherLilies; num3 < x + minDistanceFromOtherLilies; num3 += 2)
			{
				for (int num4 = y - minDistanceFromOtherLilies; num4 < y + minDistanceFromOtherLilies; num4 += 2)
				{
					if (num3 > tileTypeToPlaceThickness && num3 < Main.maxTilesX - tileTypeToPlaceThickness && num4 > tileTypeToPlaceThickness && num4 < Main.maxTilesY - tileTypeToPlaceThickness && Main.tile[num3, num4].HasTile && Main.tile[num3, num4].TileType == tileTypeToPlace)
					{
						placeLilies = false;
						break;
					}
				}
			}
			if (!placeLilies)
			{
				continue;
			}
			if (x < tileTypeToPlaceThickness || x > Main.maxTilesX - tileTypeToPlaceThickness || y2 < tileTypeToPlaceThickness || y2 > Main.maxTilesY - tileTypeToPlaceThickness)
			{
				break;
			}
			bool placeTile = true;
			for (int num5 = x - 1; num5 < x + 2; num5++)
			{
				for (int num6 = y2 - 2; num6 < y2 + 1; num6++)
				{
					if (Main.tile[num5, num6] == null)
					{
						return;
					}
					if (Main.tile[num5, num6].HasTile)
					{
						placeTile = false;
					}
				}
				if (Main.tile[num5, y2 + 1] == null)
				{
					return;
				}
				if (!WorldGen.SolidTile2(num5, y2 + 1))
				{
					placeTile = false;
				}
			}
			if (placeTile)
			{
				WorldGen.PlaceObject(x, y2, tileTypeToPlace, mute: true);
				NetMessage.SendObjectPlacement(-1, x, y2, tileTypeToPlace, 0, 0, -1, -1);
			}
		}
	}

	public static bool CanPlaceBasedOnProximity(int x, int y, int tileType)
	{
		if (tileType == ModContent.TileType<LumenylCrystals>() && !DownedBossSystem.downedLeviathan)
		{
			return false;
		}
		int minDistanceFromOtherTiles = 10;
		int sameTilesNearby = 0;
		for (int i = x - minDistanceFromOtherTiles; i < x + minDistanceFromOtherTiles; i++)
		{
			for (int j = y - minDistanceFromOtherTiles; j < y + minDistanceFromOtherTiles; j++)
			{
				if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == tileType)
				{
					sameTilesNearby++;
					if (sameTilesNearby > 1)
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	public static bool IsCorrectPlanterBox(int herbType, int planterBoxType)
	{
		bool usingCorrectPlanterBox = false;
		switch (herbType)
		{
		case 0:
			if (planterBoxType == 0)
			{
				usingCorrectPlanterBox = true;
			}
			break;
		case 1:
			if (planterBoxType == 1)
			{
				usingCorrectPlanterBox = true;
			}
			break;
		case 2:
			if (planterBoxType == 4)
			{
				usingCorrectPlanterBox = true;
			}
			break;
		case 3:
			if (planterBoxType == 2 || planterBoxType == 3)
			{
				usingCorrectPlanterBox = true;
			}
			break;
		case 4:
			if (planterBoxType == 5)
			{
				usingCorrectPlanterBox = true;
			}
			break;
		case 5:
			if (planterBoxType == 7)
			{
				usingCorrectPlanterBox = true;
			}
			break;
		case 6:
			if (planterBoxType == 6)
			{
				usingCorrectPlanterBox = true;
			}
			break;
		}
		return usingCorrectPlanterBox;
	}

	public static void TrySpawnOldMan()
	{
		if (Main.netMode != 1 && !NPC.downedBoss3 && !Main.dayTime && !NPC.AnyNPCs(37) && !NPC.AnyNPCs(35))
		{
			int oldMan = NPC.NewNPC(Entity.GetSource_TownSpawn(), Main.dungeonX * 16 + 8, Main.dungeonY * 16, 37);
			Main.npc[oldMan].homeless = false;
			Main.npc[oldMan].homeTileX = Main.dungeonX;
			Main.npc[oldMan].homeTileY = Main.dungeonY;
		}
	}

	public static void TrySpawnDungeonGuardian(Player player)
	{
		if (Main.netMode != 1 && player.ZoneDungeon && !player.dead)
		{
			bool spawn = !NPC.downedBoss3;
			if (Main.drunkWorld && player.position.Y / 16f < (float)(Main.dungeonY + 40))
			{
				spawn = false;
			}
			if (spawn && !NPC.AnyNPCs(68))
			{
				NPC.SpawnOnPlayer(player.whoAmI, 68);
			}
		}
	}

	public static void TrySpawnAEoW(Player player, CalamityPlayer modPlayer)
	{
		if (Main.netMode != 1 && (modPlayer.ZoneAbyss || Main.zenithWorld) && player.chaosState && !player.dead && !BossRushEvent.BossRushActive && (CalamityGlobalNPC.adultEidolonWyrmHead == -1 || !Main.npc[CalamityGlobalNPC.adultEidolonWyrmHead].active))
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<PrimordialWyrmHead>());
		}
	}
}
