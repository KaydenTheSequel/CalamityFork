using System;
using System.Collections.Generic;
using CalamityMod.Schematics;
using CalamityMod.Tiles.Abyss;
using CalamityMod.Tiles.Astral;
using CalamityMod.Tiles.AstralDesert;
using CalamityMod.Tiles.AstralSnow;
using CalamityMod.Tiles.FurnitureMonolith;
using CalamityMod.Tiles.Ores;
using CalamityMod.Walls;
using CalamityMod.Walls.UnsafeWalls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class AstralBiome
{
	public static readonly SoundStyle MeteorSound = new SoundStyle("CalamityMod/Sounds/Custom/AstralStarFall");

	public static int YStart { get; set; } = (int)Main.worldSurface;

	public static bool CanAstralMeteorSpawn()
	{
		int astralOreCount = 0;
		float worldSizeFactor = (float)Main.maxTilesX / 4200f;
		int astralOreAllowed = (int)(200f * worldSizeFactor);
		for (int x = 5; x < Main.maxTilesX - 5; x++)
		{
			for (int y = 5; (double)y < Main.worldSurface; y++)
			{
				if (Main.tile[x, y].HasTile && Main.tile[x, y].TileType == ModContent.TileType<AstralOre>())
				{
					astralOreCount++;
					if (astralOreCount > astralOreAllowed)
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	public static bool CanAstralBiomeSpawn()
	{
		int astralTileCount = 0;
		float worldSizeFactor = (float)Main.maxTilesX / 4200f;
		int astralTilesAllowed = (int)(400f * worldSizeFactor);
		for (int x = 5; x < Main.maxTilesX - 5; x++)
		{
			for (int y = 5; (double)y < Main.worldSurface; y++)
			{
				if (Main.tile[x, y].HasTile && (Main.tile[x, y].TileType == ModContent.TileType<AstralSand>() || Main.tile[x, y].TileType == ModContent.TileType<AstralSandstone>() || Main.tile[x, y].TileType == ModContent.TileType<HardenedAstralSand>() || Main.tile[x, y].TileType == ModContent.TileType<AstralIce>() || Main.tile[x, y].TileType == ModContent.TileType<AstralDirt>() || Main.tile[x, y].TileType == ModContent.TileType<AstralStone>() || Main.tile[x, y].TileType == ModContent.TileType<AstralGrass>() || Main.tile[x, y].TileType == ModContent.TileType<NovaeSlag>() || Main.tile[x, y].TileType == ModContent.TileType<CelestialRemains>() || Main.tile[x, y].TileType == ModContent.TileType<AstralSnow>() || Main.tile[x, y].TileType == ModContent.TileType<AstralClay>() || Main.tile[x, y].TileType == ModContent.TileType<AstralStone>()))
				{
					astralTileCount++;
					if (astralTileCount > astralTilesAllowed)
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	public static void PlaceAstralMeteor()
	{
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		Mod ancientsAwakened = ExternalMods.ancientsAwakened;
		bool meteorDropped = true;
		if (Main.netMode == 1)
		{
			return;
		}
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		if (enumerator.MoveNext())
		{
			_ = enumerator.Current;
			meteorDropped = false;
		}
		if (!CanAstralMeteorSpawn())
		{
			return;
		}
		UnifiedRandom rand = WorldGen.genRand;
		float solidTileRequirement = 600f;
		_ = GenVars.dungeonX;
		_ = Main.maxTilesX / 2;
		IList<ushort> aaTilesToAvoid = new List<ushort>(16);
		if (ancientsAwakened != null)
		{
			string[] array = new string[11]
			{
				"InfernoGrass", "Torchstone", "Torchsand", "Torchsandstone", "Torchsandhardened", "Torchice", "Depthstone", "Depthsand", "Depthsandstone", "Depthsandhardened",
				"Depthice"
			};
			foreach (string tileName in array)
			{
				if (ancientsAwakened.TryFind<ModTile>(tileName, out var value))
				{
					aaTilesToAvoid.Add(value.Type);
				}
			}
		}
		while (!meteorDropped)
		{
			_ = Main.maxTilesX;
			int xLimit = Main.maxTilesX / 2;
			int x = Utils.Clamp(Abyss.AtLeftSideOfWorld ? rand.Next(SulphurousSea.BiomeWidth + 400, xLimit - 400) : rand.Next(xLimit + 400, Main.maxTilesX - SulphurousSea.BiomeWidth - 400), SulphurousSea.BiomeWidth + 400, Main.maxTilesX - SulphurousSea.BiomeWidth - 400);
			for (int y = (int)(Main.worldSurface * 0.5); y < Main.maxTilesY; y++)
			{
				if (((!Main.tile[x, y].HasTile || !Main.tileSolid[Main.tile[x, y].TileType]) && Main.tile[x, y].WallType != 3) || TileID.Sets.Platforms[Main.tile[x, y].TileType])
				{
					continue;
				}
				int suitableTiles = 0;
				int checkRadius = 15;
				for (int l = x - checkRadius; l < x + checkRadius; l++)
				{
					for (int m = y - checkRadius; m < y + checkRadius; m++)
					{
						if (WorldGen.SolidTile(l, m))
						{
							suitableTiles++;
							if (Main.tile[l, m].TileType == 189 || Main.tile[l, m].TileType == 202)
							{
								suitableTiles -= 100;
							}
							else if (Main.tile[l, m].TileType == 109 || Main.tile[l, m].TileType == 117)
							{
								suitableTiles -= 100;
							}
							else if (Main.tile[l, m].TileType == 191 || Main.tile[l, m].TileType == 192)
							{
								suitableTiles -= 100;
							}
							else if (Main.tile[l, m].TileType == ModContent.TileType<SulphurousSand>() || Main.tile[l, m].TileType == ModContent.TileType<SulphurousSandstone>())
							{
								suitableTiles -= 100;
							}
							else if (ancientsAwakened != null && aaTilesToAvoid.Contains(Main.tile[l, m].TileType))
							{
								suitableTiles -= 100;
							}
						}
						else if (Main.tile[l, m].LiquidAmount > 0)
						{
							suitableTiles--;
						}
					}
				}
				if ((float)suitableTiles < solidTileRequirement)
				{
					solidTileRequirement -= 0.5f;
					break;
				}
				meteorDropped = GenerateAstralMeteor(x, y);
				if (meteorDropped)
				{
					Color messageColor = Color.Gold;
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.AstralText", messageColor);
				}
				break;
			}
			if (solidTileRequirement < 100f)
			{
				break;
			}
		}
	}

	public static bool GenerateAstralMeteor(int i, int j)
	{
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_099e: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ece: Unknown result type (might be due to invalid IL or missing references)
		//IL_1175: Unknown result type (might be due to invalid IL or missing references)
		WorldGen.gen = true;
		Mod magicStorage = ExternalMods.magicStorage;
		IList<ushort> MSTilesToAvoid = new List<ushort>(16);
		if (magicStorage != null)
		{
			string[] array = new string[17]
			{
				"CombinedFurnitureStations1Tile", "CombinedFurnitureStations2Tile", "CombinedStations1Tile", "CombinedStations2Tile", "CombinedStations3Tile", "CombinedStations4Tile", "CraftingAccess", "CreativeStorageUnit", "DecraftingAccess", "EnvironmentAccess",
				"EvilAltarTile", "RemoteAccess", "StorageAccess", "StorageComponent", "StorageConnector", "StorageHeart", "StorageUnit"
			};
			foreach (string tileName in array)
			{
				if (magicStorage.TryFind<ModTile>(tileName, out var value))
				{
					MSTilesToAvoid.Add(value.Type);
				}
			}
		}
		UnifiedRandom rand = WorldGen.genRand;
		if (i < 50 || i > Main.maxTilesX - 50)
		{
			return false;
		}
		if (Math.Abs(i - GenVars.dungeonX) < 65)
		{
			return false;
		}
		if (j < 50 || j > Main.maxTilesY - 50)
		{
			return false;
		}
		int avoidRectangleSize = 35;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector((i - avoidRectangleSize) * 16, (j - avoidRectangleSize) * 16, avoidRectangleSize * 2 * 16, avoidRectangleSize * 2 * 16);
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		Rectangle value2 = default(Rectangle);
		while (enumerator.MoveNext())
		{
			Player player = enumerator.Current;
			if (player.active)
			{
				((Rectangle)(ref value2))._002Ector((int)(player.position.X + (float)(player.width / 2) - (float)(NPC.sWidth / 2) - (float)NPC.safeRangeX), (int)(player.position.Y + (float)(player.height / 2) - (float)(NPC.sHeight / 2) - (float)NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
				if (((Rectangle)(ref rectangle)).Intersects(value2))
				{
					return false;
				}
			}
		}
		Rectangle value3 = default(Rectangle);
		for (int l = 0; l < Main.maxNPCs; l++)
		{
			if (Main.npc[l].active)
			{
				((Rectangle)(ref value3))._002Ector((int)Main.npc[l].position.X, (int)Main.npc[l].position.Y, Main.npc[l].width, Main.npc[l].height);
				if (((Rectangle)(ref rectangle)).Intersects(value3))
				{
					return false;
				}
			}
		}
		for (int m = i - avoidRectangleSize; m < i + avoidRectangleSize; m++)
		{
			for (int n = j - avoidRectangleSize; n < j + avoidRectangleSize; n++)
			{
				if (Main.tile[m, n].HasTile && Main.tile[m, n].TileType == 21)
				{
					return false;
				}
			}
		}
		avoidRectangleSize = rand.Next(17, 23);
		for (int inc = i - avoidRectangleSize; inc < i + avoidRectangleSize; inc++)
		{
			for (int doubleinc = j - avoidRectangleSize; doubleinc < j + avoidRectangleSize; doubleinc++)
			{
				if (doubleinc <= j + rand.Next(-2, 3) - 5)
				{
					continue;
				}
				float num = Math.Abs(i - inc);
				float tileYDist = Math.Abs(j - doubleinc);
				if ((double)(float)Math.Sqrt(num * num + tileYDist * tileYDist) < (double)avoidRectangleSize * 0.9 + (double)rand.Next(-4, 5) && Main.tile[inc, doubleinc] != null)
				{
					if (!Main.tileSolid[Main.tile[inc, doubleinc].TileType])
					{
						Main.tile[inc, doubleinc].Get<TileWallWireStateData>().HasTile = false;
					}
					Main.tile[inc, doubleinc].TileType = (ushort)ModContent.TileType<AstralOre>();
				}
			}
		}
		avoidRectangleSize = WorldGen.genRand.Next(8, 14);
		for (int num2 = i - avoidRectangleSize; num2 < i + avoidRectangleSize; num2++)
		{
			for (int num3 = j - avoidRectangleSize; num3 < j + avoidRectangleSize; num3++)
			{
				if (num3 > j + rand.Next(-2, 3) - 4)
				{
					float num4 = Math.Abs(i - num2);
					float tileYDist2 = Math.Abs(j - num3);
					if ((double)(float)Math.Sqrt(num4 * num4 + tileYDist2 * tileYDist2) < (double)avoidRectangleSize * 0.8 + (double)rand.Next(-3, 4) && Main.tile[num2, num3] != null)
					{
						Main.tile[num2, num3].Get<TileWallWireStateData>().HasTile = false;
					}
				}
			}
		}
		avoidRectangleSize = WorldGen.genRand.Next(25, 35);
		for (int num5 = i - avoidRectangleSize; num5 < i + avoidRectangleSize; num5++)
		{
			for (int num6 = j - avoidRectangleSize; num6 < j + avoidRectangleSize; num6++)
			{
				float num7 = Math.Abs(i - num5);
				float tileYDist3 = Math.Abs(j - num6);
				float tileDistance3 = (float)Math.Sqrt(num7 * num7 + tileYDist3 * tileYDist3);
				if (!(Main.tile[num5, num6] != null))
				{
					continue;
				}
				if ((double)tileDistance3 < (double)avoidRectangleSize * 0.7)
				{
					if (Main.tile[num5, num6].TileType == 5 || Main.tile[num5, num6].TileType == 32 || Main.tile[num5, num6].TileType == 352)
					{
						try
						{
							WorldGen.KillTile(num5, num6, fail: false, effectOnly: false, noItem: true);
						}
						catch (NullReferenceException)
						{
						}
					}
					Main.tile[num5, num6].LiquidAmount = 0;
				}
				if (Main.tile[num5, num6].TileType == (ushort)ModContent.TileType<AstralOre>())
				{
					if (!WorldGen.SolidTile(num5 - 1, num6) && !WorldGen.SolidTile(num5 + 1, num6) && !WorldGen.SolidTile(num5, num6 - 1) && !WorldGen.SolidTile(num5, num6 + 1))
					{
						Main.tile[num5, num6].Get<TileWallWireStateData>().HasTile = false;
					}
					else if ((Main.tile[num5, num6].IsHalfBlock || Main.tile[num5 - 1, num6].TopSlope) && !WorldGen.SolidTile(num5, num6 + 1))
					{
						Main.tile[num5, num6].Get<TileWallWireStateData>().HasTile = false;
					}
				}
				WorldGen.SquareTileFrame(num5, num6);
				WorldGen.SquareWallFrame(num5, num6);
			}
		}
		avoidRectangleSize = WorldGen.genRand.Next(23, 32);
		for (int num8 = i - avoidRectangleSize; num8 < i + avoidRectangleSize; num8++)
		{
			for (int num9 = j - avoidRectangleSize; num9 < j + avoidRectangleSize; num9++)
			{
				if (num9 <= j + WorldGen.genRand.Next(-3, 4) - 3 || !Main.tile[num8, num9].HasTile || !rand.NextBool(10))
				{
					continue;
				}
				float num10 = Math.Abs(i - num8);
				float tileYDist4 = Math.Abs(j - num9);
				if ((double)(float)Math.Sqrt(num10 * num10 + tileYDist4 * tileYDist4) < (double)avoidRectangleSize * 0.8 && Main.tile[num8, num9] != null)
				{
					if (Main.tile[num8, num9].TileType == 5 || Main.tile[num8, num9].TileType == 32 || Main.tile[num8, num9].TileType == 352)
					{
						WorldGen.KillTile(num8, num9);
					}
					Main.tile[num8, num9].TileType = (ushort)ModContent.TileType<AstralOre>();
					WorldGen.SquareTileFrame(num8, num9);
				}
			}
		}
		avoidRectangleSize = WorldGen.genRand.Next(30, 38);
		for (int num11 = i - avoidRectangleSize; num11 < i + avoidRectangleSize; num11++)
		{
			for (int num12 = j - avoidRectangleSize; num12 < j + avoidRectangleSize; num12++)
			{
				if (num12 <= j + WorldGen.genRand.Next(-2, 3) || !Main.tile[num11, num12].HasTile || !rand.NextBool(20))
				{
					continue;
				}
				float num13 = Math.Abs(i - num11);
				float tileYDist5 = Math.Abs(j - num12);
				if ((double)(float)Math.Sqrt(num13 * num13 + tileYDist5 * tileYDist5) < (double)avoidRectangleSize * 0.85 && Main.tile[num11, num12] != null)
				{
					if (Main.tile[num11, num12].TileType == 5 || Main.tile[num11, num12].TileType == 32 || Main.tile[num11, num12].TileType == 352)
					{
						WorldGen.KillTile(num11, num12);
					}
					Main.tile[num11, num12].TileType = (ushort)ModContent.TileType<AstralOre>();
					WorldGen.SquareTileFrame(num11, num12);
				}
			}
		}
		WorldGen.gen = false;
		if (Main.netMode != 1)
		{
			NetMessage.SendTileSquare(-1, i, j, 40);
			if (CanAstralBiomeSpawn())
			{
				if (!Main.LocalPlayer.dead && Main.LocalPlayer.active)
				{
					SoundEngine.PlaySound(in MeteorSound, Main.LocalPlayer.position);
				}
				YStart = j;
				DoAstralConversion((object)new Point(i, j));
				if (j < 181)
				{
					j = 181;
				}
				IList<ushort> AstralTilesToCheckFor = new List<ushort>(16);
				int[] array2 = new int[12]
				{
					ModContent.TileType<AstralSand>(),
					ModContent.TileType<AstralSandstone>(),
					ModContent.TileType<HardenedAstralSand>(),
					ModContent.TileType<CelestialRemains>(),
					ModContent.TileType<AstralIce>(),
					ModContent.TileType<AstralSnow>(),
					ModContent.TileType<AstralDirt>(),
					ModContent.TileType<AstralStone>(),
					ModContent.TileType<AstralGrass>(),
					ModContent.TileType<AstralOre>(),
					ModContent.TileType<NovaeSlag>(),
					ModContent.TileType<AstralClay>()
				};
				foreach (int modTile in array2)
				{
					AstralTilesToCheckFor.Add((ushort)modTile);
				}
				bool altarPlaced = false;
				int altarX = i + ((GenVars.dungeonX < Main.maxTilesX / 2) ? (-50) : 50);
				int altarY = j - 5;
				int distanceToCheckForAstralTilesX = 40;
				int distanceToCheckForAstralTilesY = 10;
				int startCheckingX = altarX - 20;
				int startCheckingY = altarY + 10;
				int startCheckingY2 = altarY - 25;
				int astralTilesRequired = 200;
				int emptyTilesRequiredToMove = 200;
				int emptyTilesRequired = 200;
				bool needsToCheckForMSTiles = magicStorage != null;
				int distanceToCheckForCriticalTilesX = 40;
				int distanceToCheckForCriticalTilesY = 50;
				int attempts = 0;
				int maxAttempts = 100000;
				while (!altarPlaced)
				{
					WorldGen.gen = true;
					int astralTileCount = 0;
					bool enoughAstralTilesOnBottom = false;
					for (int astralTileCheckIndexX = startCheckingX; astralTileCheckIndexX < startCheckingX + distanceToCheckForAstralTilesX; astralTileCheckIndexX++)
					{
						if (enoughAstralTilesOnBottom)
						{
							break;
						}
						for (int astralTileCheckIndexY = startCheckingY; astralTileCheckIndexY < startCheckingY + distanceToCheckForAstralTilesY; astralTileCheckIndexY++)
						{
							if (Main.tile[astralTileCheckIndexX, astralTileCheckIndexY] != null && Main.tile[astralTileCheckIndexX, astralTileCheckIndexY].HasTile && AstralTilesToCheckFor.Contains(Main.tile[astralTileCheckIndexX, astralTileCheckIndexY].TileType))
							{
								astralTileCount++;
								if (astralTileCount >= astralTilesRequired)
								{
									enoughAstralTilesOnBottom = true;
									break;
								}
							}
						}
					}
					int emptyTileCount = 0;
					bool tooManyEmptyTilesOnBottom = false;
					for (int num14 = startCheckingX; num14 < startCheckingX + distanceToCheckForAstralTilesX; num14++)
					{
						if (tooManyEmptyTilesOnBottom)
						{
							break;
						}
						for (int num15 = startCheckingY; num15 < startCheckingY + distanceToCheckForAstralTilesY; num15++)
						{
							if (Main.tile[num14, num15] == null || !Main.tile[num14, num15].HasTile)
							{
								emptyTileCount++;
								if (emptyTileCount >= emptyTilesRequiredToMove)
								{
									tooManyEmptyTilesOnBottom = true;
									break;
								}
							}
						}
					}
					int emptyTileCount2 = 0;
					bool enoughEmptyTilesOnTop = false;
					for (int emptyTileCheckIndexX = startCheckingX; emptyTileCheckIndexX < startCheckingX + distanceToCheckForAstralTilesX; emptyTileCheckIndexX++)
					{
						if (enoughEmptyTilesOnTop)
						{
							break;
						}
						for (int emptyTileCheckIndexY = startCheckingY2; emptyTileCheckIndexY < startCheckingY2 + distanceToCheckForAstralTilesY; emptyTileCheckIndexY++)
						{
							if (Main.tile[emptyTileCheckIndexX, emptyTileCheckIndexY] == null || !Main.tile[emptyTileCheckIndexX, emptyTileCheckIndexY].HasTile)
							{
								emptyTileCount2++;
								if (emptyTileCount2 >= emptyTilesRequired)
								{
									enoughEmptyTilesOnTop = true;
									break;
								}
							}
						}
					}
					bool criticalTileDetected = false;
					for (int criticalTileCheckIndexX = startCheckingX; criticalTileCheckIndexX < startCheckingX + distanceToCheckForCriticalTilesX; criticalTileCheckIndexX++)
					{
						if (criticalTileDetected)
						{
							break;
						}
						for (int criticalTileCheckIndexY = startCheckingY2; criticalTileCheckIndexY < startCheckingY2 + distanceToCheckForCriticalTilesY; criticalTileCheckIndexY++)
						{
							if (!(Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY] != null) || !Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY].HasTile)
							{
								continue;
							}
							if (needsToCheckForMSTiles)
							{
								if (TileID.Sets.Clock[Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY].TileType] || TileID.Sets.Paintings[Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY].TileType] || TileID.Sets.InteractibleByNPCs[Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY].TileType] || TileID.Sets.CanBeSleptIn[Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY].TileType] || TileID.Sets.AvoidedByNPCs[Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY].TileType] || TileID.Sets.IsAContainer[Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY].TileType] || MSTilesToAvoid.Contains(Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY].TileType))
								{
									criticalTileDetected = true;
									break;
								}
							}
							else if (TileID.Sets.Clock[Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY].TileType] || TileID.Sets.Paintings[Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY].TileType] || TileID.Sets.InteractibleByNPCs[Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY].TileType] || TileID.Sets.CanBeSleptIn[Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY].TileType] || TileID.Sets.AvoidedByNPCs[Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY].TileType] || TileID.Sets.IsAContainer[Main.tile[criticalTileCheckIndexX, criticalTileCheckIndexY].TileType])
							{
								criticalTileDetected = true;
								break;
							}
						}
					}
					bool canPlaceAltar = ((enoughAstralTilesOnBottom && !tooManyEmptyTilesOnBottom) & enoughEmptyTilesOnTop) && !criticalTileDetected;
					if (canPlaceAltar)
					{
						SchematicManager.PlaceSchematic<Action<Chest>>("Astral Beacon", new Point(altarX, altarY), SchematicAnchor.Center, ref canPlaceAltar);
						WorldGen.gen = false;
						altarPlaced = true;
						continue;
					}
					altarX += 5;
					if (altarX >= i + 150)
					{
						altarX -= 300;
					}
					if (!enoughEmptyTilesOnTop)
					{
						altarY -= 5;
					}
					else if (tooManyEmptyTilesOnBottom)
					{
						altarY += 5;
					}
					if ((double)altarY <= Main.worldSurface - 150.0)
					{
						altarY += 150;
					}
					else if ((double)altarY >= Main.worldSurface + 150.0)
					{
						altarY -= 150;
					}
					attempts++;
					if (attempts < maxAttempts)
					{
						continue;
					}
					int num16 = j - 50;
					bool forcedPlacedAltar = false;
					while (!forcedPlacedAltar)
					{
						for (; !WorldGen.SolidTile(i, num16 + 15) && (double)(num16 + 15) <= Main.worldSurface; num16 += 5)
						{
						}
						bool canPlaceAltar2 = false;
						canPlaceAltar2 = ((!needsToCheckForMSTiles) ? (!TileID.Sets.Clock[Main.tile[i, num16].TileType] && !TileID.Sets.Paintings[Main.tile[i, num16].TileType] && !TileID.Sets.InteractibleByNPCs[Main.tile[i, num16].TileType] && !TileID.Sets.CanBeSleptIn[Main.tile[i, num16].TileType] && !TileID.Sets.AvoidedByNPCs[Main.tile[i, num16].TileType] && !TileID.Sets.IsAContainer[Main.tile[i, num16].TileType]) : (!TileID.Sets.Clock[Main.tile[i, num16].TileType] && !TileID.Sets.Paintings[Main.tile[i, num16].TileType] && !TileID.Sets.InteractibleByNPCs[Main.tile[i, num16].TileType] && !TileID.Sets.CanBeSleptIn[Main.tile[i, num16].TileType] && !TileID.Sets.AvoidedByNPCs[Main.tile[i, num16].TileType] && !TileID.Sets.IsAContainer[Main.tile[i, num16].TileType] && !MSTilesToAvoid.Contains(Main.tile[i, num16].TileType)));
						if (canPlaceAltar2)
						{
							SchematicManager.PlaceSchematic<Action<Chest>>("Astral Beacon", new Point(i, num16), SchematicAnchor.Center, ref canPlaceAltar2);
							WorldGen.gen = false;
							forcedPlacedAltar = true;
							altarPlaced = true;
						}
					}
				}
			}
		}
		return true;
	}

	public static void DoAstralConversion(object obj)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		Point origin = (Point)obj;
		Vector2 center = origin.ToVector2() * 16f + new Vector2(8f);
		float angle = 0.47123894f;
		float otherAngle = (float)Math.PI / 2f - angle;
		int distanceInTiles = 150 + (Main.maxTilesX - 4200) / 4200 * 200;
		float num = (float)distanceInTiles * 16f;
		float constant = num * 2f / (float)Math.Sin(angle);
		float fociSpacing = num * (float)Math.Sin(otherAngle) / (float)Math.Sin(angle);
		int verticalRadius = (int)(constant / 16f);
		Vector2 fociOffset = Vector2.UnitY * fociSpacing;
		Vector2 topFoci = center - fociOffset;
		Vector2 bottomFoci = center + fociOffset;
		UnifiedRandom rand = WorldGen.genRand;
		for (int x = origin.X - distanceInTiles - 2; x <= origin.X + distanceInTiles + 2; x++)
		{
			for (int y = (int)((float)origin.Y - (float)verticalRadius * 0.4f) - 3; y <= origin.Y + verticalRadius + 3; y++)
			{
				if (!CheckInEllipse(new Point(x, y), topFoci, bottomFoci, constant, center, out var dist, y < origin.Y))
				{
					continue;
				}
				float percent = dist / constant;
				float blurPercent = 0.98f;
				if (percent > blurPercent)
				{
					float outerEdgePercent = (percent - blurPercent) / (1f - blurPercent);
					if (rand.NextFloat(1f) > outerEdgePercent)
					{
						ConvertToAstral(x, y, convertOre: true);
					}
				}
				else
				{
					ConvertToAstral(x, y, convertOre: true);
				}
			}
		}
	}

	public static void ConvertToAstral(int startX, int endX, int startY, int endY, bool convertOre = false)
	{
		for (int x = startX; x <= endX; x++)
		{
			for (int y = startY; y <= endY; y++)
			{
				ConvertToAstral(x, y, convertOre);
			}
		}
	}

	public static void ConvertToAstral(int x, int y, bool convertOre = false)
	{
		if (!WorldGen.InWorld(x, y, 1))
		{
			return;
		}
		Tile tile = Main.tile[x, y];
		int type = tile.TileType;
		tile = Main.tile[x, y];
		int wallType = tile.WallType;
		if (!(Main.tile[x, y] != null))
		{
			return;
		}
		if (wallType != 0 && wallType != ModContent.WallType<UnsafeAstralGrassWall>() && wallType != ModContent.WallType<UnsafeHardenedAstralSandWall>() && wallType != ModContent.WallType<UnsafeAstralSandstoneWall>() && wallType != ModContent.WallType<UnsafeAstralStoneWall>() && wallType != ModContent.WallType<UnsafeAstralDirtWall>() && wallType != ModContent.WallType<UnsafeAstralSnowWall>() && wallType != ModContent.WallType<CelestialRemainsWall>() && wallType != ModContent.WallType<UnsafeAstralIceWall>() && wallType != ModContent.WallType<AstralMonolithWall>())
		{
			if (WallID.Sets.Conversion.Grass[wallType])
			{
				tile = Main.tile[x, y];
				tile.WallType = (ushort)ModContent.WallType<UnsafeAstralGrassWall>();
				WorldGen.SquareWallFrame(x, y);
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
			else if (WallID.Sets.Conversion.HardenedSand[wallType])
			{
				tile = Main.tile[x, y];
				tile.WallType = (ushort)ModContent.WallType<UnsafeHardenedAstralSandWall>();
				WorldGen.SquareWallFrame(x, y);
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
			else if (WallID.Sets.Conversion.Sandstone[wallType])
			{
				tile = Main.tile[x, y];
				tile.WallType = (ushort)ModContent.WallType<UnsafeAstralSandstoneWall>();
				WorldGen.SquareWallFrame(x, y);
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
			else if (WallID.Sets.Conversion.Stone[wallType])
			{
				tile = Main.tile[x, y];
				tile.WallType = (ushort)ModContent.WallType<UnsafeAstralStoneWall>();
				WorldGen.SquareWallFrame(x, y);
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
			else
			{
				switch (wallType)
				{
				case 2:
				case 16:
				case 59:
				case 196:
				case 197:
				case 198:
				case 199:
					tile = Main.tile[x, y];
					tile.WallType = (ushort)ModContent.WallType<UnsafeAstralDirtWall>();
					WorldGen.SquareWallFrame(x, y);
					NetMessage.SendTileSquare(-1, x, y, 1);
					break;
				case 40:
					tile = Main.tile[x, y];
					tile.WallType = (ushort)ModContent.WallType<UnsafeAstralSnowWall>();
					WorldGen.SquareWallFrame(x, y);
					NetMessage.SendTileSquare(-1, x, y, 1);
					break;
				case 223:
					tile = Main.tile[x, y];
					tile.WallType = (ushort)ModContent.WallType<CelestialRemainsWall>();
					WorldGen.SquareWallFrame(x, y);
					NetMessage.SendTileSquare(-1, x, y, 1);
					break;
				case 71:
					tile = Main.tile[x, y];
					tile.WallType = (ushort)ModContent.WallType<UnsafeAstralIceWall>();
					WorldGen.SquareWallFrame(x, y);
					NetMessage.SendTileSquare(-1, x, y, 1);
					break;
				case 244:
					tile = Main.tile[x, y];
					tile.WallType = (ushort)ModContent.WallType<AstralMonolithWall>();
					WorldGen.SquareWallFrame(x, y);
					NetMessage.SendTileSquare(-1, x, y, 1);
					break;
				}
			}
		}
		if (type < 0 || type == ModContent.TileType<AstralGrass>() || type == ModContent.TileType<AstralStone>() || type == ModContent.TileType<AstralSand>() || type == ModContent.TileType<HardenedAstralSand>() || type == ModContent.TileType<AstralSandstone>() || type == ModContent.TileType<AstralIce>() || type == ModContent.TileType<AstralDirt>() || type == ModContent.TileType<AstralSnow>() || type == ModContent.TileType<NovaeSlag>() || type == ModContent.TileType<CelestialRemains>() || type == ModContent.TileType<AstralClay>() || type == ModContent.TileType<AstralVines>() || type == ModContent.TileType<AstralMonolith>() || type == ModContent.TileType<AstralOre>() || type == ModContent.TileType<AstralNormalLargePiles>() || type == ModContent.TileType<AstralIceLargePiles>() || type == ModContent.TileType<AstralDesertLargePiles>() || type == ModContent.TileType<AstralStoneLargePiles>() || type == ModContent.TileType<AstralDesertMediumPiles>() || type == ModContent.TileType<AstralNormalMediumPiles>() || type == ModContent.TileType<AstralIceMediumPiles>() || type == ModContent.TileType<AstralStoneMediumPiles>() || type == ModContent.TileType<AstralDesertSmallPiles>() || type == ModContent.TileType<AstralNormalSmallPiles>() || type == ModContent.TileType<AstralIceSmallPiles>() || type == ModContent.TileType<AstralStoneSmallPiles>() || type == ModContent.TileType<AstralDesertStalactite>() || type == ModContent.TileType<AstralNormalStalactite>() || type == ModContent.TileType<AstralIceStalactite>() || type == ModContent.TileType<AstralDesertStalactite>() || type == ModContent.TileType<AstralStoneStalactite>())
		{
			return;
		}
		if (TileID.Sets.Conversion.Grass[type] && !TileID.Sets.GrassSpecial[type])
		{
			tile = Main.tile[x, y];
			tile.TileType = (ushort)ModContent.TileType<AstralGrass>();
			WorldGen.SquareTileFrame(x, y);
			NetMessage.SendTileSquare(-1, x, y, 1);
			return;
		}
		if (TileID.Sets.Conversion.Stone[type] || Main.tileMoss[type])
		{
			tile = Main.tile[x, y];
			tile.TileType = (ushort)ModContent.TileType<AstralStone>();
			WorldGen.SquareTileFrame(x, y);
			NetMessage.SendTileSquare(-1, x, y, 1);
			return;
		}
		if (TileID.Sets.Conversion.Sand[type])
		{
			tile = Main.tile[x, y];
			tile.TileType = (ushort)ModContent.TileType<AstralSand>();
			WorldGen.SquareTileFrame(x, y);
			NetMessage.SendTileSquare(-1, x, y, 1);
			return;
		}
		if (TileID.Sets.Conversion.HardenedSand[type])
		{
			tile = Main.tile[x, y];
			tile.TileType = (ushort)ModContent.TileType<HardenedAstralSand>();
			WorldGen.SquareTileFrame(x, y);
			NetMessage.SendTileSquare(-1, x, y, 1);
			return;
		}
		if (TileID.Sets.Conversion.Sandstone[type])
		{
			tile = Main.tile[x, y];
			tile.TileType = (ushort)ModContent.TileType<AstralSandstone>();
			WorldGen.SquareTileFrame(x, y);
			NetMessage.SendTileSquare(-1, x, y, 1);
			return;
		}
		if (TileID.Sets.Conversion.Ice[type])
		{
			tile = Main.tile[x, y];
			tile.TileType = (ushort)ModContent.TileType<AstralIce>();
			WorldGen.SquareTileFrame(x, y);
			NetMessage.SendTileSquare(-1, x, y, 1);
			return;
		}
		Tile tile2 = Main.tile[x, y];
		switch (type)
		{
		case 0:
			tile = Main.tile[x, y];
			tile.TileType = (ushort)ModContent.TileType<AstralDirt>();
			WorldGen.SquareTileFrame(x, y);
			NetMessage.SendTileSquare(-1, x, y, 1);
			break;
		case 147:
			tile = Main.tile[x, y];
			tile.TileType = (ushort)ModContent.TileType<AstralSnow>();
			WorldGen.SquareTileFrame(x, y);
			NetMessage.SendTileSquare(-1, x, y, 1);
			break;
		case 123:
		case 224:
			tile = Main.tile[x, y];
			tile.TileType = (ushort)ModContent.TileType<NovaeSlag>();
			WorldGen.SquareTileFrame(x, y);
			NetMessage.SendTileSquare(-1, x, y, 1);
			break;
		case 404:
			tile = Main.tile[x, y];
			tile.TileType = (ushort)ModContent.TileType<CelestialRemains>();
			WorldGen.SquareTileFrame(x, y);
			NetMessage.SendTileSquare(-1, x, y, 1);
			break;
		case 40:
			tile = Main.tile[x, y];
			tile.TileType = (ushort)ModContent.TileType<AstralClay>();
			WorldGen.SquareTileFrame(x, y);
			NetMessage.SendTileSquare(-1, x, y, 1);
			break;
		case 52:
			tile = Main.tile[x, y];
			tile.TileType = (ushort)ModContent.TileType<AstralVines>();
			WorldGen.SquareTileFrame(x, y);
			NetMessage.SendTileSquare(-1, x, y, 1);
			break;
		case 191:
			tile = Main.tile[x, y];
			tile.TileType = (ushort)ModContent.TileType<AstralMonolith>();
			WorldGen.SquareTileFrame(x, y);
			NetMessage.SendTileSquare(-1, x, y, 1);
			break;
		case 6:
		case 7:
		case 8:
		case 9:
		case 37:
		case 166:
		case 167:
		case 168:
		case 169:
			if (convertOre)
			{
				tile = Main.tile[x, y];
				tile.TileType = (ushort)ModContent.TileType<AstralOre>();
				WorldGen.SquareTileFrame(x, y);
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
			break;
		case 27:
		case 192:
			WorldGen.KillTile(x, y);
			if (Main.netMode == 1)
			{
				NetMessage.SendData(17, -1, -1, null, 0, x, y);
			}
			break;
		case 186:
			if (tile2.TileFrameX <= 1170)
			{
				RecursiveReplaceToAstral(186, (ushort)ModContent.TileType<AstralStoneLargePiles>(), x, y, 324, 0, 1170, 0, 18);
			}
			if (tile2.TileFrameX >= 1728)
			{
				RecursiveReplaceToAstral(186, (ushort)ModContent.TileType<AstralNormalLargePiles>(), x, y, 324, 1728, 1872, 0, 18);
			}
			if (tile2.TileFrameX >= 1404 && tile2.TileFrameX <= 1710)
			{
				RecursiveReplaceToAstral(186, (ushort)ModContent.TileType<AstralIceLargePiles>(), x, y, 324, 1404, 1710, 0, 18);
			}
			break;
		case 187:
			if (tile2.TileFrameX >= 1566 && tile2.TileFrameY < 36)
			{
				RecursiveReplaceToAstral(187, (ushort)ModContent.TileType<AstralDesertLargePiles>(), x, y, 324, 1566, 1872, 0, 18);
			}
			if (tile2.TileFrameX >= 756 && tile2.TileFrameX <= 900)
			{
				RecursiveReplaceToAstral(187, (ushort)ModContent.TileType<AstralNormalLargePiles>(), x, y, 324, 756, 900, 0, 18);
			}
			break;
		case 185:
			if (tile2.TileFrameY == 18)
			{
				ushort newType3;
				if (tile2.TileFrameX >= 1476 && tile2.TileFrameX <= 1674)
				{
					newType3 = (ushort)ModContent.TileType<AstralDesertMediumPiles>();
				}
				else if (tile2.TileFrameX >= 1368 && tile2.TileFrameX <= 1458)
				{
					newType3 = (ushort)ModContent.TileType<AstralNormalMediumPiles>();
				}
				else if (tile2.TileFrameX <= 558)
				{
					newType3 = (ushort)ModContent.TileType<AstralStoneMediumPiles>();
				}
				else
				{
					if (tile2.TileFrameX < 900 || tile2.TileFrameX > 1098)
					{
						break;
					}
					newType3 = (ushort)ModContent.TileType<AstralIceMediumPiles>();
				}
				int leftMost = x;
				if (tile2.TileFrameX % 36 != 0)
				{
					leftMost--;
				}
				if (Main.tile[leftMost, y] != null)
				{
					tile = Main.tile[leftMost, y];
					tile.TileType = newType3;
					WorldGen.SquareTileFrame(leftMost, y);
					NetMessage.SendTileSquare(-1, leftMost, y, 1);
				}
				if (Main.tile[leftMost + 1, y] != null)
				{
					tile = Main.tile[leftMost + 1, y];
					tile.TileType = newType3;
					WorldGen.SquareTileFrame(leftMost + 1, y);
					NetMessage.SendTileSquare(-1, leftMost + 1, y, 1);
				}
				while (true)
				{
					tile = Main.tile[leftMost, y];
					if (tile.TileFrameX >= 216)
					{
						if (Main.tile[leftMost, y] != null)
						{
							tile = Main.tile[leftMost, y];
							tile.TileFrameX -= 216;
						}
						if (Main.tile[leftMost + 1, y] != null)
						{
							tile = Main.tile[leftMost + 1, y];
							tile.TileFrameX -= 216;
						}
						continue;
					}
					break;
				}
			}
			else
			{
				if (tile2.TileFrameY != 0)
				{
					break;
				}
				ushort newType4;
				if (tile2.TileFrameX >= 972 && tile2.TileFrameX <= 1062)
				{
					newType4 = (ushort)ModContent.TileType<AstralDesertSmallPiles>();
				}
				else if (tile2.TileFrameX >= 110 && tile2.TileFrameX <= 198)
				{
					newType4 = (ushort)ModContent.TileType<AstralNormalSmallPiles>();
				}
				else if (tile2.TileFrameX <= 486)
				{
					newType4 = (ushort)ModContent.TileType<AstralStoneSmallPiles>();
				}
				else
				{
					if (tile2.TileFrameX < 648 || tile2.TileFrameX > 846)
					{
						break;
					}
					newType4 = (ushort)ModContent.TileType<AstralIceSmallPiles>();
				}
				tile = Main.tile[x, y];
				tile.TileType = newType4;
				while (true)
				{
					tile = Main.tile[x, y];
					if (tile.TileFrameX < 108)
					{
						break;
					}
					tile = Main.tile[x, y];
					tile.TileFrameX -= 108;
				}
				WorldGen.SquareTileFrame(x, y);
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
			break;
		case 165:
		{
			int topMost = ((tile2.TileFrameY > 54) ? y : ((tile2.TileFrameY % 36 == 0) ? y : (y - 1)));
			bool twoTall = tile2.TileFrameY <= 54;
			bool hanging = tile2.TileFrameY <= 18 || tile2.TileFrameY == 72;
			ushort newType2;
			if (tile2.TileFrameX >= 378 && tile2.TileFrameX <= 414)
			{
				newType2 = (ushort)ModContent.TileType<AstralDesertStalactite>();
			}
			else if ((tile2.TileFrameX >= 54 && tile2.TileFrameX <= 90) || (tile2.TileFrameX >= 216 && tile2.TileFrameX <= 360))
			{
				newType2 = (ushort)ModContent.TileType<AstralStoneStalactite>();
			}
			else
			{
				if (tile2.TileFrameX > 36)
				{
					break;
				}
				newType2 = (ushort)ModContent.TileType<AstralIceStalactite>();
			}
			if (Main.tile[x, topMost] != null)
			{
				tile = Main.tile[x, topMost];
				tile.TileType = newType2;
			}
			if (twoTall && Main.tile[x, topMost + 1] != null)
			{
				tile = Main.tile[x, topMost + 1];
				tile.TileType = newType2;
			}
			while (true)
			{
				tile = Main.tile[x, topMost];
				if (tile.TileFrameX < 54)
				{
					break;
				}
				if (Main.tile[x, topMost] != null)
				{
					tile = Main.tile[x, topMost];
					tile.TileFrameX -= 54;
				}
				if (twoTall && Main.tile[x, topMost + 1] != null)
				{
					tile = Main.tile[x, topMost + 1];
					tile.TileFrameX -= 54;
				}
			}
			if (Main.tile[x, topMost] != null)
			{
				WorldGen.SquareTileFrame(x, topMost);
				NetMessage.SendTileSquare(-1, x, topMost, 1);
			}
			if (Main.tile[x, topMost + 1] != null)
			{
				WorldGen.SquareTileFrame(x, topMost + 1);
				NetMessage.SendTileSquare(-1, x, topMost + 1, 1);
			}
			if (hanging)
			{
				ConvertToAstral(x, topMost - 1);
			}
			else if (twoTall)
			{
				ConvertToAstral(x, topMost + 2);
			}
			else
			{
				ConvertToAstral(x, topMost + 1);
			}
			break;
		}
		case 530:
			tile = Main.tile[x, y];
			tile.Get<TileWallWireStateData>().HasTile = false;
			NetMessage.SendTileSquare(-1, x, y, 1);
			break;
		}
	}

	public static void RecursiveReplaceToAstral(ushort checkType, ushort replaceType, int x, int y, int replaceTextureWidth, int minFrameX = 0, int maxFrameX = int.MaxValue, int minFrameY = 0, int maxFrameY = int.MaxValue)
	{
		Tile tile = Main.tile[x, y];
		if (tile == null || !tile.HasTile || tile.TileType != checkType || tile.TileFrameX < minFrameX || tile.TileFrameX > maxFrameX || tile.TileFrameY < minFrameY || tile.TileFrameY > maxFrameY)
		{
			return;
		}
		Tile tile2 = Main.tile[x, y];
		tile2.TileType = replaceType;
		while (true)
		{
			tile2 = Main.tile[x, y];
			if (tile2.TileFrameX < replaceTextureWidth)
			{
				break;
			}
			tile2 = Main.tile[x, y];
			tile2.TileFrameX -= (short)replaceTextureWidth;
		}
		if (Main.tile[x - 1, y] != null)
		{
			RecursiveReplaceToAstral(checkType, replaceType, x - 1, y, replaceTextureWidth, minFrameX, maxFrameX, minFrameY, maxFrameY);
		}
		if (Main.tile[x + 1, y] != null)
		{
			RecursiveReplaceToAstral(checkType, replaceType, x + 1, y, replaceTextureWidth, minFrameX, maxFrameX, minFrameY, maxFrameY);
		}
		if (Main.tile[x, y - 1] != null)
		{
			RecursiveReplaceToAstral(checkType, replaceType, x, y - 1, replaceTextureWidth, minFrameX, maxFrameX, minFrameY, maxFrameY);
		}
		if (Main.tile[x, y + 1] != null)
		{
			RecursiveReplaceToAstral(checkType, replaceType, x, y + 1, replaceTextureWidth, minFrameX, maxFrameX, minFrameY, maxFrameY);
		}
	}

	public static void ReplaceAstralStalactite(ushort replaceType, ushort replaceOriginTile, int x, int y)
	{
		Tile tile = Main.tile[x, y];
		int topMost = ((tile.TileFrameY > 54) ? y : ((tile.TileFrameY % 36 == 0) ? y : (y - 1)));
		bool twoTall = tile.TileFrameY <= 54;
		int yOriginTile = ((tile.TileFrameY <= 18 || tile.TileFrameY == 72) ? (topMost - 1) : (twoTall ? (topMost + 2) : (y + 1)));
		if (Main.tile[x, topMost++] != null)
		{
			Main.tile[x, topMost++].TileType = replaceType;
		}
		if (twoTall && Main.tile[x, topMost] != null)
		{
			Main.tile[x, topMost].TileType = replaceType;
		}
		if (Main.tile[x, yOriginTile] != null)
		{
			Main.tile[x, yOriginTile].TileType = replaceOriginTile;
		}
	}

	public static bool CheckInEllipse(Point tile, Vector2 focus1, Vector2 focus2, float distanceConstant, Vector2 center, out float distance, bool collapse = false)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		Vector2 point = tile.ToWorldCoordinates();
		if (collapse)
		{
			float distY = center.Y - point.Y;
			point.Y -= distY * 3f;
		}
		float distance2 = Vector2.Distance(point, focus1);
		float distance3 = Vector2.Distance(point, focus2);
		distance = distance2 + distance3;
		return distance <= distanceConstant;
	}
}
