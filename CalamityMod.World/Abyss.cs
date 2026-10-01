using System;
using System.Collections.Generic;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Items.Tools.ClimateChange;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Packets;
using CalamityMod.Systems.Collections;
using CalamityMod.Tiles.Abyss;
using CalamityMod.Tiles.Abyss.AbyssAmbient;
using CalamityMod.Tiles.Ores;
using CalamityMod.Walls;
using CalamityMod.Walls.UnsafeWalls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class Abyss
{
	public static int TotalPlacedIslandsSoFar = 0;

	public static Point[] AbyssIslandPositions = (Point[])(object)new Point[20];

	public static int[] AbyssItemArray = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

	public static bool AtLeftSideOfWorld = false;

	public static int AbyssChasmBottom = 0;

	public static bool UnlockChests { get; set; }

	public static void PlaceAbyss()
	{
		//IL_169d: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d4: Unknown result type (might be due to invalid IL or missing references)
		int x = Main.maxTilesX;
		int y = Main.maxTilesY;
		int genLimit = x / 2;
		int rockLayer = (Main.remixWorld ? SulphurousSea.YStart : ((int)Main.rockLayer));
		int abyssChasmY = (Main.remixWorld ? SulphurousSea.YStart : (y - 250));
		AbyssChasmBottom = abyssChasmY - 100;
		int abyssChasmX = (AtLeftSideOfWorld ? (genLimit - (genLimit - 135) + 35) : (genLimit + (genLimit - 135) - 35));
		int abyssMinX = ((!AtLeftSideOfWorld) ? (abyssChasmX - 160) : 0);
		int abyssMaxX = (AtLeftSideOfWorld ? (abyssChasmX + 160) : x);
		for (int X = abyssMinX; X < abyssMaxX; X++)
		{
			for (int Y = 0; Y < abyssChasmY + 50; Y++)
			{
				Tile tile = Framing.GetTileSafely(X, Y);
				if (tile.LiquidType == 1 && tile.LiquidAmount > 0)
				{
					tile.Get<LiquidData>().LiquidType = 0;
					tile.LiquidAmount = byte.MaxValue;
				}
				bool canConvert = false;
				if (tile.HasTile)
				{
					bool num = tile.TileType < TileID.Count;
					bool validModdedTile = tile.TileType != ModContent.TileType<SulphurousSandstone>() && CalamityTileSets.CanBeReplacedByAbyssGeneration[tile.TileType];
					canConvert = num | validModdedTile;
				}
				if (Main.remixWorld)
				{
					if (Y > rockLayer)
					{
						continue;
					}
					if (canConvert)
					{
						if (Y <= rockLayer - (int)((float)(y - 200) * 0.6f))
						{
							tile.TileType = (ushort)ModContent.TileType<Voidstone>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeVoidstoneWall>();
						}
						else if (Y <= rockLayer - (int)((float)(y - 200) * 0.59f) && WorldGen.genRand.NextBool(2))
						{
							tile.TileType = (ushort)ModContent.TileType<Voidstone>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeVoidstoneWall>();
						}
						else if (Y <= rockLayer - (int)((float)(y - 200) * 0.4f))
						{
							tile.TileType = (ushort)ModContent.TileType<PyreMantle>();
							tile.WallType = (ushort)ModContent.WallType<PyreMantleWall>();
						}
						else if (Y <= rockLayer - (int)((float)(y - 200) * 0.39f) && WorldGen.genRand.NextBool(2))
						{
							tile.TileType = (ushort)ModContent.TileType<PyreMantle>();
							tile.WallType = (ushort)ModContent.WallType<PyreMantleWall>();
						}
						else if (Y <= rockLayer - (int)((float)(y - 200) * 0.2f))
						{
							tile.TileType = (ushort)ModContent.TileType<AbyssGravel>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeAbyssGravelWall>();
						}
						else if (Y <= rockLayer - (int)((float)(y - 200) * 0.19f))
						{
							if (WorldGen.genRand.NextBool(2))
							{
								tile.TileType = (ushort)ModContent.TileType<AbyssGravel>();
								tile.WallType = (ushort)ModContent.WallType<UnsafeAbyssGravelWall>();
							}
							else
							{
								tile.TileType = (ushort)ModContent.TileType<SulphurousShale>();
								tile.WallType = (ushort)ModContent.WallType<UnsafeSulphurousShaleWall>();
							}
						}
						else
						{
							tile.TileType = (ushort)ModContent.TileType<SulphurousShale>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeSulphurousShaleWall>();
						}
					}
					else
					{
						if (tile.HasTile)
						{
							continue;
						}
						tile.Get<TileWallWireStateData>().HasTile = true;
						if (Y <= rockLayer - (int)((float)(y - 200) * 0.6f))
						{
							tile.TileType = (ushort)ModContent.TileType<Voidstone>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeVoidstoneWall>();
						}
						else if (Y <= rockLayer - (int)((float)(y - 200) * 0.59f) && WorldGen.genRand.NextBool(2))
						{
							tile.TileType = (ushort)ModContent.TileType<Voidstone>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeVoidstoneWall>();
						}
						else if (Y <= rockLayer - (int)((float)(y - 200) * 0.4f))
						{
							tile.TileType = (ushort)ModContent.TileType<PyreMantle>();
							tile.WallType = (ushort)ModContent.WallType<PyreMantleWall>();
						}
						else if (Y <= rockLayer - (int)((float)(y - 200) * 0.39f) && WorldGen.genRand.NextBool(2))
						{
							tile.TileType = (ushort)ModContent.TileType<PyreMantle>();
							tile.WallType = (ushort)ModContent.WallType<PyreMantleWall>();
						}
						else if (Y <= rockLayer - (int)((float)(y - 200) * 0.2f))
						{
							tile.TileType = (ushort)ModContent.TileType<AbyssGravel>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeAbyssGravelWall>();
						}
						else if (Y <= rockLayer - (int)((float)(y - 200) * 0.19f))
						{
							if (WorldGen.genRand.NextBool(2))
							{
								tile.TileType = (ushort)ModContent.TileType<AbyssGravel>();
								tile.WallType = (ushort)ModContent.WallType<UnsafeAbyssGravelWall>();
							}
							else
							{
								tile.TileType = (ushort)ModContent.TileType<SulphurousShale>();
								tile.WallType = (ushort)ModContent.WallType<UnsafeSulphurousShaleWall>();
							}
						}
						else
						{
							tile.TileType = (ushort)ModContent.TileType<SulphurousShale>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeSulphurousShaleWall>();
						}
					}
				}
				else
				{
					if (Y <= rockLayer - Main.maxTilesY / 15 + 35)
					{
						continue;
					}
					if (canConvert)
					{
						if ((double)Y > (double)rockLayer + (double)y * 0.27)
						{
							tile.TileType = (ushort)ModContent.TileType<Voidstone>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeVoidstoneWall>();
						}
						else if ((double)Y > (double)rockLayer + (double)y * 0.268 && WorldGen.genRand.NextBool(2))
						{
							tile.TileType = (ushort)ModContent.TileType<Voidstone>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeVoidstoneWall>();
						}
						else if ((double)Y > (double)rockLayer + (double)y * 0.145)
						{
							tile.TileType = (ushort)ModContent.TileType<PyreMantle>();
							tile.WallType = (ushort)ModContent.WallType<PyreMantleWall>();
						}
						else if ((double)Y > (double)rockLayer + (double)y * 0.143 && WorldGen.genRand.NextBool(2))
						{
							tile.TileType = (ushort)ModContent.TileType<PyreMantle>();
							tile.WallType = (ushort)ModContent.WallType<PyreMantleWall>();
						}
						else if (Y >= rockLayer - 10 && Y <= rockLayer)
						{
							if (WorldGen.genRand.NextBool(2))
							{
								tile.TileType = (ushort)ModContent.TileType<AbyssGravel>();
								tile.WallType = (ushort)ModContent.WallType<UnsafeAbyssGravelWall>();
							}
							else
							{
								tile.TileType = (ushort)ModContent.TileType<SulphurousShale>();
								tile.WallType = (ushort)ModContent.WallType<UnsafeSulphurousShaleWall>();
							}
						}
						else if (Y <= rockLayer - 10)
						{
							tile.TileType = (ushort)ModContent.TileType<SulphurousShale>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeSulphurousShaleWall>();
						}
						else
						{
							tile.TileType = (ushort)ModContent.TileType<AbyssGravel>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeAbyssGravelWall>();
						}
					}
					else
					{
						if (tile.HasTile)
						{
							continue;
						}
						tile.Get<TileWallWireStateData>().HasTile = true;
						if ((double)Y > (double)rockLayer + (double)y * 0.27)
						{
							tile.TileType = (ushort)ModContent.TileType<Voidstone>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeVoidstoneWall>();
						}
						else if ((double)Y > (double)rockLayer + (double)y * 0.268 && WorldGen.genRand.NextBool(2))
						{
							tile.TileType = (ushort)ModContent.TileType<Voidstone>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeVoidstoneWall>();
						}
						else if ((double)Y > (double)rockLayer + (double)y * 0.145)
						{
							tile.TileType = (ushort)ModContent.TileType<PyreMantle>();
							tile.WallType = (ushort)ModContent.WallType<PyreMantleWall>();
						}
						else if ((double)Y > (double)rockLayer + (double)y * 0.143 && WorldGen.genRand.NextBool(2))
						{
							tile.TileType = (ushort)ModContent.TileType<PyreMantle>();
							tile.WallType = (ushort)ModContent.WallType<PyreMantleWall>();
						}
						else if (Y >= rockLayer - 10 && Y <= rockLayer)
						{
							if (WorldGen.genRand.NextBool(2))
							{
								tile.TileType = (ushort)ModContent.TileType<AbyssGravel>();
								tile.WallType = (ushort)ModContent.WallType<UnsafeAbyssGravelWall>();
							}
							else
							{
								tile.TileType = (ushort)ModContent.TileType<SulphurousShale>();
								tile.WallType = (ushort)ModContent.WallType<UnsafeSulphurousShaleWall>();
							}
						}
						else if (Y <= rockLayer - 10)
						{
							tile.TileType = (ushort)ModContent.TileType<SulphurousShale>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeSulphurousShaleWall>();
						}
						else
						{
							tile.TileType = (ushort)ModContent.TileType<AbyssGravel>();
							tile.WallType = (ushort)ModContent.WallType<UnsafeAbyssGravelWall>();
						}
					}
				}
			}
		}
		MiscWorldgenRoutines.ChasmGenerator(abyssChasmX, Main.remixWorld ? 100 : ((int)GenVars.worldSurfaceLow + 65), Main.remixWorld ? (AbyssChasmBottom + 110) : AbyssChasmBottom, ocean: true);
		MiscWorldgenRoutines.ChasmGenerator(abyssChasmX - 22, Main.remixWorld ? 35 : ((int)GenVars.worldSurfaceLow), AbyssChasmBottom, ocean: true);
		MiscWorldgenRoutines.ChasmGenerator(abyssChasmX + 22, Main.remixWorld ? 35 : ((int)GenVars.worldSurfaceLow), AbyssChasmBottom, ocean: true);
		int maxAbyssIslands = 11;
		if (y > 2100)
		{
			maxAbyssIslands = 20;
		}
		else if (y > 1500)
		{
			maxAbyssIslands = 16;
		}
		PlaceSnailFossil(abyssChasmX, Main.remixWorld ? 145 : (AbyssChasmBottom + 45));
		AbyssIsland(abyssChasmX, Main.remixWorld ? 105 : (AbyssChasmBottom + 5), 65, 75, 40, 45, ModContent.TileType<Voidstone>(), hasChest: false, hasClumps: false, hasScoria: false);
		UndergroundShrines.PlaceAbyssShrine(abyssChasmX, Main.remixWorld ? 100 : AbyssChasmBottom);
		int sulphurIslandY;
		for (sulphurIslandY = (Main.remixWorld ? (rockLayer - (int)((float)(y - 200) * 0.2f)) : ((SulphurousSea.YStart + (int)Main.worldSurface) / 2 + 90)); sulphurIslandY <= rockLayer - 25; sulphurIslandY++)
		{
			int islandLocationX = abyssChasmX;
			int islandLocationOffset = WorldGen.genRand.Next(15, 22);
			int randomPositon = WorldGen.genRand.Next(35, 80);
			switch (WorldGen.genRand.Next(5))
			{
			case 0:
				AbyssIsland(islandLocationX - randomPositon - 10, sulphurIslandY + 15, 60, 65, 45, 55, ModContent.TileType<SulphurousShale>(), hasChest: false, hasClumps: false, hasScoria: false);
				AbyssIsland(islandLocationX, sulphurIslandY, 60, 65, 45, 55, ModContent.TileType<SulphurousShale>(), hasChest: false, hasClumps: false, hasScoria: false);
				AbyssIsland(islandLocationX + randomPositon + 10, sulphurIslandY + 15, 60, 65, 45, 55, ModContent.TileType<SulphurousShale>(), hasChest: false, hasClumps: false, hasScoria: false);
				break;
			case 1:
				AbyssIsland(islandLocationX - randomPositon, sulphurIslandY + 10, 60, 85, 30, 35, ModContent.TileType<SulphurousShale>(), hasChest: false, hasClumps: false, hasScoria: false);
				AbyssIsland(islandLocationX, sulphurIslandY + 15, 60, 85, 30, 35, ModContent.TileType<SulphurousShale>(), hasChest: false, hasClumps: false, hasScoria: false);
				AbyssIsland(islandLocationX + randomPositon, sulphurIslandY, 60, 85, 30, 35, ModContent.TileType<SulphurousShale>(), hasChest: false, hasClumps: false, hasScoria: false);
				islandLocationX += 15;
				break;
			case 2:
				AbyssIsland(islandLocationX - randomPositon, sulphurIslandY + 15, 55, 65, 30, 35, ModContent.TileType<SulphurousShale>(), hasChest: false, hasClumps: false, hasScoria: false);
				AbyssIsland(islandLocationX + WorldGen.genRand.Next(15, 30), sulphurIslandY + 15, 60, 85, 30, 35, ModContent.TileType<SulphurousShale>(), hasChest: false, hasClumps: false, hasScoria: false);
				islandLocationX -= 15;
				break;
			case 3:
				AbyssIsland(islandLocationX - WorldGen.genRand.Next(15, 30), sulphurIslandY + 10, 55, 65, 30, 35, ModContent.TileType<SulphurousShale>(), hasChest: false, hasClumps: false, hasScoria: false);
				AbyssIsland(islandLocationX + randomPositon, sulphurIslandY + 15, 60, 85, 30, 35, ModContent.TileType<SulphurousShale>(), hasChest: false, hasClumps: false, hasScoria: false);
				islandLocationX += 25;
				break;
			case 4:
				AbyssIsland(islandLocationX - randomPositon, sulphurIslandY, 60, 75, 30, 40, ModContent.TileType<SulphurousShale>(), hasChest: false, hasClumps: false, hasScoria: false);
				AbyssIsland(islandLocationX + randomPositon, sulphurIslandY + 5, 60, 75, 30, 40, ModContent.TileType<SulphurousShale>(), hasChest: false, hasClumps: false, hasScoria: false);
				islandLocationX -= 25;
				break;
			}
			sulphurIslandY += islandLocationOffset;
		}
		int islandLocationY = (Main.remixWorld ? (rockLayer - (int)((float)(y - 200) * 0.4f)) : rockLayer);
		for (int islands = 0; islands < maxAbyssIslands; islands++)
		{
			int islandLocationX2 = abyssChasmX;
			int islandLocationOffset2 = WorldGen.genRand.Next(18, 25);
			int randomPositon2 = WorldGen.genRand.Next(45, 80);
			AbyssIslandPositions[TotalPlacedIslandsSoFar].Y = islandLocationY;
			switch (WorldGen.genRand.Next(5))
			{
			case 0:
				AbyssIsland(islandLocationX2 - randomPositon2 - 10, islandLocationY + 15, 60, 65, 30, 35, ModContent.TileType<AbyssGravel>(), hasChest: false, hasClumps: true, hasScoria: false);
				AbyssIsland(islandLocationX2, islandLocationY, 60, 65, 30, 35, ModContent.TileType<AbyssGravel>(), hasChest: true, hasClumps: true, hasScoria: true);
				AbyssIsland(islandLocationX2 + randomPositon2 + 10, islandLocationY + 15, 60, 65, 30, 35, ModContent.TileType<AbyssGravel>(), hasChest: false, hasClumps: true, hasScoria: false);
				break;
			case 1:
				AbyssIsland(islandLocationX2 - randomPositon2, islandLocationY + 10, 60, 65, 30, 35, ModContent.TileType<AbyssGravel>(), hasChest: false, hasClumps: true, hasScoria: true);
				AbyssIsland(islandLocationX2, islandLocationY + 15, 60, 65, 30, 35, ModContent.TileType<AbyssGravel>(), hasChest: true, hasClumps: true, hasScoria: false);
				AbyssIsland(islandLocationX2 + randomPositon2, islandLocationY, 60, 65, 30, 35, ModContent.TileType<AbyssGravel>(), hasChest: false, hasClumps: true, hasScoria: false);
				islandLocationX2 += 30;
				break;
			case 2:
				AbyssIsland(islandLocationX2 - randomPositon2 - 20, islandLocationY, 55, 65, 30, 35, ModContent.TileType<AbyssGravel>(), hasChest: false, hasClumps: true, hasScoria: true);
				AbyssIsland(islandLocationX2 + 15, islandLocationY + 15, 55, 65, 30, 35, ModContent.TileType<AbyssGravel>(), hasChest: false, hasClumps: true, hasScoria: false);
				AbyssIsland(islandLocationX2 + randomPositon2 + 20, islandLocationY + 10, 55, 65, 30, 35, ModContent.TileType<AbyssGravel>(), hasChest: false, hasClumps: true, hasScoria: false);
				islandLocationX2 -= 30;
				break;
			case 3:
				AbyssIsland(islandLocationX2 - randomPositon2, islandLocationY + 15, 60, 65, 30, 45, ModContent.TileType<AbyssGravel>(), hasChest: false, hasClumps: true, hasScoria: false);
				AbyssIsland(islandLocationX2 - 15, islandLocationY + 5, 60, 65, 30, 45, ModContent.TileType<AbyssGravel>(), hasChest: false, hasClumps: true, hasScoria: false);
				AbyssIsland(islandLocationX2 + randomPositon2, islandLocationY, 60, 65, 30, 45, ModContent.TileType<AbyssGravel>(), hasChest: true, hasClumps: true, hasScoria: true);
				islandLocationX2 += 25;
				break;
			case 4:
				AbyssIsland(islandLocationX2 - randomPositon2, islandLocationY, 60, 75, 30, 40, ModContent.TileType<AbyssGravel>(), hasChest: true, hasClumps: true, hasScoria: false);
				AbyssIsland(islandLocationX2, islandLocationY + 15, 60, 75, 30, 40, ModContent.TileType<AbyssGravel>(), hasChest: false, hasClumps: true, hasScoria: false);
				AbyssIsland(islandLocationX2 + randomPositon2, islandLocationY + 5, 60, 75, 30, 40, ModContent.TileType<AbyssGravel>(), hasChest: false, hasClumps: true, hasScoria: false);
				islandLocationX2 -= 25;
				break;
			}
			AbyssIslandPositions[TotalPlacedIslandsSoFar].X = islandLocationX2;
			TotalPlacedIslandsSoFar++;
			islandLocationY += islandLocationOffset2;
			if ((double)islandLocationY >= (Main.remixWorld ? ((double)(rockLayer - (int)((float)(y - 200) * 0.2f))) : ((double)rockLayer + (double)y * 0.145 - 10.0)))
			{
				break;
			}
		}
		int thermalIslandY;
		for (thermalIslandY = (Main.remixWorld ? (rockLayer - (int)((float)(y - 200) * 0.6f)) : ((int)((double)rockLayer + (double)y * 0.145))); thermalIslandY <= (Main.remixWorld ? (rockLayer - (int)((float)(y - 200) * 0.4f)) : ((int)((double)rockLayer + (double)y * 0.27))); thermalIslandY++)
		{
			int islandLocationX3 = abyssChasmX;
			int islandLocationOffset3 = WorldGen.genRand.Next(18, 30);
			int randomPositon3 = WorldGen.genRand.Next(40, 75);
			WorldGen.genRand.NextBool(2);
			switch (WorldGen.genRand.Next(4))
			{
			case 0:
				AbyssIsland(islandLocationX3 - randomPositon3 - 10, thermalIslandY + 15, 55, 65, 45, 55, ModContent.TileType<PyreMantle>(), hasChest: false, hasClumps: true, hasScoria: false);
				AbyssIsland(islandLocationX3, thermalIslandY, 60, 65, 45, 55, ModContent.TileType<PyreMantle>(), hasChest: false, hasClumps: true, hasScoria: true);
				AbyssIsland(islandLocationX3 + randomPositon3 + 10, thermalIslandY + 15, 60, 65, 45, 55, ModContent.TileType<PyreMantle>(), hasChest: false, hasClumps: true, hasScoria: true);
				islandLocationX3 -= 30;
				break;
			case 1:
				AbyssIsland(islandLocationX3 - randomPositon3, thermalIslandY + 10, 60, 75, 30, 35, ModContent.TileType<PyreMantle>(), hasChest: false, hasClumps: true, hasScoria: true);
				AbyssIsland(islandLocationX3, thermalIslandY + 15, 75, 85, 30, 35, ModContent.TileType<PyreMantle>(), hasChest: false, hasClumps: true, hasScoria: false);
				AbyssIsland(islandLocationX3 + randomPositon3, thermalIslandY, 55, 85, 30, 35, ModContent.TileType<PyreMantle>(), hasChest: false, hasClumps: true, hasScoria: false);
				islandLocationX3 += 30;
				break;
			case 2:
				AbyssIsland(islandLocationX3 - randomPositon3 - 20, thermalIslandY, 55, 65, 30, 35, ModContent.TileType<PyreMantle>(), hasChest: false, hasClumps: true, hasScoria: true);
				AbyssIsland(islandLocationX3 - 20, thermalIslandY + 15, 60, 70, 30, 35, ModContent.TileType<PyreMantle>(), hasChest: false, hasClumps: true, hasScoria: false);
				AbyssIsland(islandLocationX3 + randomPositon3 + 20, thermalIslandY + 10, 65, 70, 30, 35, ModContent.TileType<PyreMantle>(), hasChest: false, hasClumps: true, hasScoria: true);
				islandLocationX3 -= 25;
				break;
			case 3:
				AbyssIsland(islandLocationX3 - randomPositon3, thermalIslandY + 15, 60, 75, 30, 55, ModContent.TileType<PyreMantle>(), hasChest: false, hasClumps: true, hasScoria: true);
				AbyssIsland(islandLocationX3 + 20, thermalIslandY + 5, 60, 75, 30, 55, ModContent.TileType<PyreMantle>(), hasChest: false, hasClumps: true, hasScoria: false);
				AbyssIsland(islandLocationX3 + randomPositon3, thermalIslandY, 60, 75, 30, 55, ModContent.TileType<PyreMantle>(), hasChest: false, hasClumps: true, hasScoria: true);
				islandLocationX3 += 25;
				break;
			}
			thermalIslandY += islandLocationOffset3;
			if ((double)thermalIslandY >= (Main.remixWorld ? ((double)(rockLayer - (int)((float)(y - 200) * 0.4f))) : ((double)rockLayer + (double)y * 0.27 - 10.0)))
			{
				break;
			}
		}
		for (int voidIslandY = (Main.remixWorld ? (rockLayer - (int)((float)(y - 200) * 0.8f)) : ((int)((double)rockLayer + (double)y * 0.275))); voidIslandY <= (Main.remixWorld ? (rockLayer - (int)((float)(y - 200) * 0.6f)) : (AbyssChasmBottom - 20)); voidIslandY++)
		{
			if (WorldGen.genRand.NextBool(8))
			{
				switch (WorldGen.genRand.Next(3))
				{
				case 0:
					AbyssIsland(abyssChasmX - WorldGen.genRand.Next(70, 78), voidIslandY, 65, 85, 35, 45, ModContent.TileType<Voidstone>(), hasChest: false, hasClumps: false, hasScoria: false);
					break;
				case 1:
					AbyssIsland(abyssChasmX + WorldGen.genRand.Next(70, 78), voidIslandY, 65, 85, 35, 45, ModContent.TileType<Voidstone>(), hasChest: false, hasClumps: false, hasScoria: false);
					break;
				case 2:
					AbyssIsland(abyssChasmX - WorldGen.genRand.Next(70, 78), voidIslandY, 65, 85, 35, 45, ModContent.TileType<Voidstone>(), hasChest: false, hasClumps: false, hasScoria: false);
					AbyssIsland(abyssChasmX + WorldGen.genRand.Next(70, 78), voidIslandY, 65, 85, 35, 45, ModContent.TileType<Voidstone>(), hasChest: false, hasClumps: false, hasScoria: false);
					break;
				}
				voidIslandY += WorldGen.genRand.Next(20, 32);
			}
		}
		AbyssItemArray = CalamityUtils.ShuffleArray(AbyssItemArray);
		for (int abyssHouse = 0; abyssHouse < TotalPlacedIslandsSoFar; abyssHouse++)
		{
			_ = ref AbyssIslandPositions[abyssHouse];
			if (abyssHouse != 20)
			{
				AbyssChest(AbyssIslandPositions[abyssHouse].X, AbyssIslandPositions[abyssHouse].Y, AbyssItemArray[(abyssHouse > 9) ? (abyssHouse - 10) : abyssHouse]);
			}
		}
		for (int i = abyssMinX + 5; i < abyssMaxX - 5; i++)
		{
			for (int j = 5; j < abyssChasmY; j++)
			{
				Tile tile2 = Main.tile[i, j];
				Tile tileUp = Main.tile[i, j - 1];
				Tile tileDown = Main.tile[i, j + 1];
				Tile tileLeft = Main.tile[i - 1, j];
				Tile tileRight = Main.tile[i + 1, j];
				if (tile2.TileType == ModContent.TileType<AbyssGravel>() || tile2.TileType == ModContent.TileType<PyreMantle>() || tile2.TileType == ModContent.TileType<Voidstone>() || tile2.TileType == ModContent.TileType<PlantyMush>() || tile2.TileType == ModContent.TileType<ScoriaOre>() || tile2.TileType == ModContent.TileType<SulphurousShale>())
				{
					Tile.SmoothSlope(i, j);
					if (!tileUp.HasTile && !tileDown.HasTile && !tileLeft.HasTile && !tileRight.HasTile)
					{
						WorldGen.KillTile(i, j);
					}
				}
				if (!tile2.HasTile && tile2.WallType > 0)
				{
					tile2.Get<LiquidData>().LiquidType = 0;
					tile2.LiquidAmount = byte.MaxValue;
				}
				if (tile2.TileType == 56)
				{
					WorldGen.KillTile(i, j);
				}
			}
		}
		for (int k = abyssMinX + 5; k < abyssMaxX - 5; k++)
		{
			for (int l = 0; l < (Main.remixWorld ? rockLayer : Main.UnderworldLayer); l++)
			{
				Tile tileToGrowVineOn = Main.tile[k, l];
				if (!Main.tile[k, l].HasTile)
				{
					Tile tile3 = Main.tile[k, l + 1];
					if (l < (Main.remixWorld ? rockLayer : Main.UnderworldLayer) && WorldGen.SolidTile(k, l + 1))
					{
						if (tile3.TileType == ModContent.TileType<SulphurousShale>())
						{
							if (WorldGen.genRand.NextBool(85))
							{
								WorldGen.PlaceObject(k, l, (ushort)ModContent.TileType<SulphurTubeCoral>());
							}
							if (WorldGen.genRand.NextBool(18))
							{
								ushort[] ShalePiles = new ushort[3]
								{
									(ushort)ModContent.TileType<ShalePile1>(),
									(ushort)ModContent.TileType<ShalePile2>(),
									(ushort)ModContent.TileType<ShalePile3>()
								};
								WorldGen.PlaceObject(k, l, WorldGen.genRand.Next(ShalePiles));
							}
							if (WorldGen.genRand.NextBool(15))
							{
								ushort[] PireCorals = new ushort[3]
								{
									(ushort)ModContent.TileType<SulphurPireCoral1>(),
									(ushort)ModContent.TileType<SulphurPireCoral2>(),
									(ushort)ModContent.TileType<SulphurPireCoral3>()
								};
								WorldGen.PlaceObject(k, l, WorldGen.genRand.Next(PireCorals));
							}
							if (WorldGen.genRand.NextBool(12))
							{
								ushort[] SulphuricFossils = new ushort[3]
								{
									(ushort)ModContent.TileType<SulphuricFossil1>(),
									(ushort)ModContent.TileType<SulphuricFossil2>(),
									(ushort)ModContent.TileType<SulphuricFossil3>()
								};
								WorldGen.PlaceObject(k, l, WorldGen.genRand.Next(SulphuricFossils));
							}
							if (WorldGen.genRand.NextBool(12))
							{
								ushort[] Ribs = new ushort[5]
								{
									(ushort)ModContent.TileType<SulphurousRib1>(),
									(ushort)ModContent.TileType<SulphurousRib2>(),
									(ushort)ModContent.TileType<SulphurousRib3>(),
									(ushort)ModContent.TileType<SulphurousRib4>(),
									(ushort)ModContent.TileType<SulphurousRib5>()
								};
								WorldGen.PlaceObject(k, l, WorldGen.genRand.Next(Ribs));
							}
						}
						if (tile3.TileType == ModContent.TileType<PlantyMush>() && WorldGen.genRand.NextBool(8))
						{
							ushort[] PlantPiles = new ushort[3]
							{
								(ushort)ModContent.TileType<PlantyMushPile1>(),
								(ushort)ModContent.TileType<PlantyMushPile2>(),
								(ushort)ModContent.TileType<PlantyMushPile3>()
							};
							WorldGen.PlaceObject(k, l, WorldGen.genRand.Next(PlantPiles));
						}
						if (tile3.TileType == ModContent.TileType<AbyssGravel>())
						{
							if (WorldGen.genRand.NextBool(125) && !Main.tile[k, l - 1].HasTile)
							{
								ShapeData circle = new ShapeData();
								GenAction blotchMod = new Modifiers.Blotches(2, 0.4);
								WorldUtils.Gen(new Point(k, l), new Shapes.Circle(WorldGen.genRand.Next(3, 4)), Actions.Chain(blotchMod.Output(circle)));
								WorldUtils.Gen(new Point(k, l), new ModShapes.All(circle), Actions.Chain(new Actions.PlaceTile((ushort)ModContent.TileType<AbyssCoral>())));
							}
							if (WorldGen.genRand.NextBool(50))
							{
								WorldGen.PlaceObject(k, l, (ushort)ModContent.TileType<MassiveRarePearl>());
							}
							if (WorldGen.genRand.NextBool(15))
							{
								ushort[] Kelps = new ushort[4]
								{
									(ushort)ModContent.TileType<AbyssGiantKelp1>(),
									(ushort)ModContent.TileType<AbyssGiantKelp2>(),
									(ushort)ModContent.TileType<AbyssGiantKelp3>(),
									(ushort)ModContent.TileType<AbyssGiantKelp4>()
								};
								WorldGen.PlaceObject(k, l, WorldGen.genRand.Next(Kelps));
							}
							if (WorldGen.genRand.NextBool(15))
							{
								ushort[] PlantPiles2 = new ushort[3]
								{
									(ushort)ModContent.TileType<PlantyMushPile1>(),
									(ushort)ModContent.TileType<PlantyMushPile2>(),
									(ushort)ModContent.TileType<PlantyMushPile3>()
								};
								WorldGen.PlaceObject(k, l, WorldGen.genRand.Next(PlantPiles2));
							}
							if (WorldGen.genRand.NextBool(15))
							{
								ushort[] GravelPiles = new ushort[3]
								{
									(ushort)ModContent.TileType<GravelPile1>(),
									(ushort)ModContent.TileType<GravelPile2>(),
									(ushort)ModContent.TileType<GravelPile3>()
								};
								WorldGen.PlaceObject(k, l, WorldGen.genRand.Next(GravelPiles));
							}
							if (WorldGen.genRand.NextBool(45))
							{
								ushort[] Vents = new ushort[3]
								{
									(ushort)ModContent.TileType<AbyssVent1>(),
									(ushort)ModContent.TileType<AbyssVent2>(),
									(ushort)ModContent.TileType<AbyssVent3>()
								};
								WorldGen.PlaceObject(k, l, WorldGen.genRand.Next(Vents));
							}
							if (WorldGen.genRand.NextBool(17))
							{
								ushort[] PirateCrate = new ushort[6]
								{
									(ushort)ModContent.TileType<PirateCrate1>(),
									(ushort)ModContent.TileType<PirateCrate2>(),
									(ushort)ModContent.TileType<PirateCrate3>(),
									(ushort)ModContent.TileType<PirateCrate4>(),
									(ushort)ModContent.TileType<PirateCrate5>(),
									(ushort)ModContent.TileType<PirateCrate6>()
								};
								WorldGen.PlaceObject(k, l, WorldGen.genRand.Next(PirateCrate));
							}
						}
						if (tile3.TileType == ModContent.TileType<PyreMantle>())
						{
							if (WorldGen.genRand.NextBool(12))
							{
								ushort[] SpiderCorals = new ushort[5]
								{
									(ushort)ModContent.TileType<SpiderCoral1>(),
									(ushort)ModContent.TileType<SpiderCoral2>(),
									(ushort)ModContent.TileType<SpiderCoral3>(),
									(ushort)ModContent.TileType<SpiderCoral4>(),
									(ushort)ModContent.TileType<SpiderCoral5>()
								};
								WorldGen.PlaceObject(k, l, WorldGen.genRand.Next(SpiderCorals));
							}
							if (WorldGen.genRand.NextBool(15))
							{
								ushort[] Vents2 = new ushort[3]
								{
									(ushort)ModContent.TileType<ThermalVent1>(),
									(ushort)ModContent.TileType<ThermalVent2>(),
									(ushort)ModContent.TileType<ThermalVent3>()
								};
								WorldGen.PlaceObject(k, l, WorldGen.genRand.Next(Vents2));
							}
						}
						if (tile3.TileType == ModContent.TileType<Voidstone>() && WorldGen.genRand.NextBool(25))
						{
							ushort[] BulbTrees = new ushort[3]
							{
								(ushort)ModContent.TileType<BulbTree1>(),
								(ushort)ModContent.TileType<BulbTree2>(),
								(ushort)ModContent.TileType<BulbTree3>()
							};
							WorldGen.PlaceObject(k, l, WorldGen.genRand.Next(BulbTrees));
						}
					}
					if ((tile3.TileType == ModContent.TileType<AbyssGravel>() || tile3.TileType == ModContent.TileType<PyreMantle>() || tile3.TileType == ModContent.TileType<Voidstone>()) && l > (Main.remixWorld ? (rockLayer - (int)((float)(y - 200) * 0.8f)) : rockLayer))
					{
						if (WorldGen.genRand.NextBool(5))
						{
							WorldGen.PlacePot(k, l, (ushort)ModContent.TileType<AbyssalPots>());
							CalamityUtils.SafeSquareTileFrame(k, l);
						}
					}
					else if (tile3.TileType == ModContent.TileType<SulphurousShale>() && l < (Main.remixWorld ? Main.UnderworldLayer : ((int)Main.worldSurface)) && WorldGen.genRand.NextBool(3))
					{
						WorldGen.PlacePot(k, l, (ushort)ModContent.TileType<SulphurousPots>());
						CalamityUtils.SafeSquareTileFrame(k, l);
					}
				}
				if (tileToGrowVineOn.TileType == ModContent.TileType<PlantyMush>() && Main.tile[k, l].Slope == SlopeType.Solid && !Main.tile[k, l + 1].HasTile && WorldGen.genRand.NextBool(2))
				{
					WorldGen.PlaceTile(k, l + 1, (ushort)ModContent.TileType<ViperVines>());
				}
				if (tileToGrowVineOn.TileType == ModContent.TileType<ViperVines>())
				{
					CalamityUtils.GrowVines(k, l, WorldGen.genRand.Next(1, 4), (ushort)ModContent.TileType<ViperVines>());
				}
				if (tileToGrowVineOn.TileType == ModContent.TileType<SulphurousShale>() && Main.tile[k, l].Slope == SlopeType.Solid && !Main.tile[k, l + 1].HasTile && WorldGen.genRand.NextBool(5))
				{
					WorldGen.PlaceTile(k, l + 1, (ushort)ModContent.TileType<SulphurousVines>());
				}
				if (tileToGrowVineOn.TileType == ModContent.TileType<SulphurousVines>())
				{
					CalamityUtils.GrowVines(k, l, WorldGen.genRand.Next(1, 4), (ushort)ModContent.TileType<SulphurousVines>());
				}
			}
		}
	}

	public static void AbyssIsland(int i, int j, int sizeMin, int sizeMax, int sizeMin2, int sizeMax2, int tileType, bool hasChest, bool hasClumps, bool hasScoria)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		float islandWidth = WorldGen.genRand.Next(sizeMin, sizeMax);
		float smallIslandWidth = (float)WorldGen.genRand.Next(sizeMin, sizeMax) / 5f;
		int islandPositionX = i;
		int islandPositionXAgain = i;
		int islandPositionY = j;
		int islandPositionYAgain = j;
		Vector2 islandOrigin = default(Vector2);
		islandOrigin.X = i;
		islandOrigin.Y = j;
		Vector2 vector2 = default(Vector2);
		vector2.X = (float)WorldGen.genRand.Next(-20, 21) * 0.2f;
		while (vector2.X > -2f && vector2.X < 2f)
		{
			vector2.X = (float)WorldGen.genRand.Next(-20, 21) * 0.2f;
		}
		vector2.Y = (float)WorldGen.genRand.Next(-20, -10) * 0.02f;
		while (islandWidth > 0f && smallIslandWidth > 0f)
		{
			islandWidth -= (float)WorldGen.genRand.Next(4);
			smallIslandWidth--;
			int num = Math.Clamp((int)((double)islandOrigin.X - (double)islandWidth * 0.5), 0, Main.maxTilesX);
			int islandRightX = Math.Clamp((int)((double)islandOrigin.X + (double)islandWidth * 0.5), 0, Main.maxTilesX);
			int islandTopY = Math.Clamp((int)((double)islandOrigin.Y - (double)islandWidth * 0.5), 0, Main.maxTilesY);
			int islandBottomY = Math.Clamp((int)((double)islandOrigin.Y + (double)islandWidth * 0.5), 0, Main.maxTilesY);
			double extraIslandWidth = (double)islandWidth * (double)WorldGen.genRand.Next(sizeMin, sizeMax) * 0.01;
			float islandYOffset = islandOrigin.Y + 1f;
			for (int k = num; k < islandRightX; k++)
			{
				if (WorldGen.genRand.NextBool(2))
				{
					islandYOffset += (float)WorldGen.genRand.Next(-1, 2);
				}
				if (islandYOffset < islandOrigin.Y)
				{
					islandYOffset = islandOrigin.Y;
				}
				if (islandYOffset > islandOrigin.Y + 2f)
				{
					islandYOffset = islandOrigin.Y + 2f;
				}
				for (int l = islandTopY; l < islandBottomY; l++)
				{
					if (!((float)l > islandYOffset))
					{
						continue;
					}
					float num2 = Math.Abs((float)k - islandOrigin.X);
					float tileCheckYDist = Math.Abs((float)l - islandOrigin.Y) * 3f;
					if (Math.Sqrt(num2 * num2 + tileCheckYDist * tileCheckYDist) < extraIslandWidth * 0.4)
					{
						if (k < islandPositionX)
						{
							islandPositionX = k;
						}
						if (k > islandPositionXAgain)
						{
							islandPositionXAgain = k;
						}
						if (l < islandPositionYAgain)
						{
							islandPositionYAgain = l;
						}
						if (l > islandPositionY)
						{
							islandPositionY = l;
						}
						Main.tile[k, l].Get<TileWallWireStateData>().HasTile = true;
						Main.tile[k, l].TileType = (ushort)tileType;
						CalamityUtils.SafeSquareTileFrame(k, l);
					}
				}
			}
			islandOrigin += vector2;
			vector2.X += (float)WorldGen.genRand.Next(-20, 21) * 0.05f;
			if (vector2.X > 1f)
			{
				vector2.X = 1f;
			}
			if (vector2.X < -1f)
			{
				vector2.X = -1f;
			}
			if ((double)vector2.Y > 0.2)
			{
				vector2.Y = -0.2f;
			}
			if ((double)vector2.Y < -0.2)
			{
				vector2.Y = -0.2f;
			}
		}
		int m = islandPositionX;
		int randMinMaxValues;
		for (m += WorldGen.genRand.Next(5); m < islandPositionXAgain; m += WorldGen.genRand.Next(randMinMaxValues, (int)((double)randMinMaxValues * 1.5)))
		{
			int islandTileY = islandPositionY;
			while (!Main.tile[m, islandTileY].HasTile)
			{
				islandTileY--;
			}
			islandTileY += WorldGen.genRand.Next(-3, 4);
			randMinMaxValues = WorldGen.genRand.Next(4, 8);
			int placedTile = tileType;
			if (hasClumps && WorldGen.genRand.NextBool(3))
			{
				placedTile = ((tileType == ModContent.TileType<PyreMantle>()) ? (hasScoria ? ModContent.TileType<ScoriaOre>() : ModContent.TileType<PyreMantleMolten>()) : (hasScoria ? ModContent.TileType<ScoriaOre>() : ModContent.TileType<PlantyMush>()));
			}
			for (int n = m - randMinMaxValues; n <= m + randMinMaxValues; n++)
			{
				for (int p = islandTileY - randMinMaxValues; p <= islandTileY + randMinMaxValues; p++)
				{
					if (p > islandPositionYAgain)
					{
						float num3 = Math.Abs(n - m);
						float islandTileYDist = Math.Abs(p - islandTileY) * 2;
						if (Math.Sqrt(num3 * num3 + islandTileYDist * islandTileYDist) < (double)(randMinMaxValues + WorldGen.genRand.Next(2)))
						{
							Main.tile[n, p].Get<TileWallWireStateData>().HasTile = true;
							Main.tile[n, p].TileType = (ushort)placedTile;
							CalamityUtils.SafeSquareTileFrame(n, p);
						}
					}
				}
			}
		}
		int sizeMinSmall2 = sizeMin2 / 8;
		int sizeMaxSmall2 = sizeMax2 / 8;
		islandWidth = WorldGen.genRand.Next(sizeMin2, sizeMax2);
		smallIslandWidth = WorldGen.genRand.Next(sizeMinSmall2, sizeMaxSmall2);
		islandOrigin.X = i;
		islandOrigin.Y = islandPositionYAgain;
		vector2.X = (float)WorldGen.genRand.Next(-20, 21) * 0.2f;
		while (vector2.X > -2f && vector2.X < 2f)
		{
			vector2.X = (float)WorldGen.genRand.Next(-20, 21) * 0.2f;
		}
		vector2.Y = (float)WorldGen.genRand.Next(-20, -10) * 0.02f;
		while ((double)islandWidth > 0.0 && smallIslandWidth > 0f)
		{
			islandWidth -= (float)WorldGen.genRand.Next(4);
			smallIslandWidth--;
			islandOrigin += vector2;
			vector2.X += (float)WorldGen.genRand.Next(-20, 21) * 0.05f;
			if (vector2.X > 1f)
			{
				vector2.X = 1f;
			}
			if (vector2.X < -1f)
			{
				vector2.X = -1f;
			}
			if ((double)vector2.Y > 0.2)
			{
				vector2.Y = -0.2f;
			}
			if ((double)vector2.Y < -0.2)
			{
				vector2.Y = -0.2f;
			}
		}
		int islandXOffsetPos = islandPositionX;
		islandXOffsetPos += WorldGen.genRand.Next(5);
		while (islandXOffsetPos < islandPositionXAgain)
		{
			int islandYOffsetPos = islandPositionY;
			while ((!Main.tile[islandXOffsetPos, islandYOffsetPos].HasTile || Main.tile[islandXOffsetPos, islandYOffsetPos].TileType != 0) && islandXOffsetPos < islandPositionXAgain)
			{
				islandYOffsetPos--;
				if (islandYOffsetPos < islandPositionYAgain)
				{
					islandYOffsetPos = islandPositionY;
					islandXOffsetPos += WorldGen.genRand.Next(1, 4);
				}
			}
			if (islandXOffsetPos >= islandPositionXAgain)
			{
				continue;
			}
			islandYOffsetPos += WorldGen.genRand.Next(0, 4);
			int islandOffsetRandValues = WorldGen.genRand.Next(2, 5);
			for (int r = islandXOffsetPos - islandOffsetRandValues; r <= islandXOffsetPos + islandOffsetRandValues; r++)
			{
				for (int s = islandYOffsetPos - islandOffsetRandValues; s <= islandYOffsetPos + islandOffsetRandValues; s++)
				{
					if (s > islandPositionYAgain)
					{
						float num4 = Math.Abs(r - islandXOffsetPos);
						float islandOffsetYDist = Math.Abs(s - islandYOffsetPos) * 2;
						if (Math.Sqrt(num4 * num4 + islandOffsetYDist * islandOffsetYDist) < (double)islandOffsetRandValues)
						{
							Main.tile[r, s].TileType = (ushort)tileType;
							CalamityUtils.SafeSquareTileFrame(r, s);
						}
					}
				}
			}
			islandXOffsetPos += WorldGen.genRand.Next(islandOffsetRandValues, (int)((double)islandOffsetRandValues * 1.5));
		}
	}

	public static void AbyssChest(int i, int j, int itemChoice)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		AbyssIsland(i, j + 2, 55, 75, 35, 45, ModContent.TileType<AbyssGravel>(), hasChest: false, hasClumps: true, hasScoria: false);
		for (int clearX = i - 2; clearX <= i + 2; clearX++)
		{
			for (int clearY = j - 8; clearY <= j - 1; clearY++)
			{
				ShapeData circle = new ShapeData();
				GenAction blotchMod = new Modifiers.Blotches(2, 0.4);
				int radius = (int)((float)WorldGen.genRand.Next(3, 5) * WorldGen.genRand.NextFloat(0.74f, 0.82f));
				WorldUtils.Gen(new Point(clearX, clearY), new Shapes.Circle(radius), Actions.Chain(blotchMod.Output(circle)));
				WorldUtils.Gen(new Point(clearX, clearY), new ModShapes.All(circle), Actions.Chain(new Actions.ClearTile()));
			}
		}
		WorldGen.TileRunner(i, j + 4, 8.0, 5, ModContent.TileType<AbyssGravel>(), addTile: true);
		for (int k = i - 7; k <= i + 7; k++)
		{
			for (int l = j - 1; l <= j + 5; l++)
			{
				WorldGen.PlaceTile(k, l, ModContent.TileType<AbyssGravel>());
			}
		}
		itemChoice = itemChoice switch
		{
			0 => ModContent.ItemType<TorrentialTear>(), 
			1 => ModContent.ItemType<IronBoots>(), 
			2 => ModContent.ItemType<DepthCharm>(), 
			3 => ModContent.ItemType<Archerfish>(), 
			4 => ModContent.ItemType<AnechoicPlating>(), 
			5 => ModContent.ItemType<BallOFugu>(), 
			6 => ModContent.ItemType<StrangeOrb>(), 
			7 => ModContent.ItemType<HerringStaff>(), 
			8 => ModContent.ItemType<BlackAnurian>(), 
			9 => ModContent.ItemType<Lionfish>(), 
			_ => 497, 
		};
		int ChestIndex = WorldGen.PlaceChest(i, j - 2, (ushort)ModContent.TileType<global::CalamityMod.Tiles.Abyss.AbyssTreasureChest>(), notNearOtherChests: false, 1);
		int[] Potions1 = new int[4] { 298, 291, 2351, 4477 };
		int[] Potions2 = new int[4] { 301, 2327, 300, 292 };
		if (ChestIndex != -1)
		{
			Main.chest[ChestIndex].item[0].SetDefaults(itemChoice);
			Main.chest[ChestIndex].item[0].Prefix(-1);
			Main.chest[ChestIndex].item[1].SetDefaults(WorldGen.genRand.Next(Potions1));
			Main.chest[ChestIndex].item[1].stack = WorldGen.genRand.Next(1, 3);
			Main.chest[ChestIndex].item[2].SetDefaults(WorldGen.genRand.Next(Potions2));
			Main.chest[ChestIndex].item[2].stack = WorldGen.genRand.Next(1, 3);
			Main.chest[ChestIndex].item[3].SetDefaults(188);
			Main.chest[ChestIndex].item[3].stack = WorldGen.genRand.Next(1, 3);
			Main.chest[ChestIndex].item[4].SetDefaults(189);
			Main.chest[ChestIndex].item[4].stack = WorldGen.genRand.Next(2, 5);
			Main.chest[ChestIndex].item[5].SetDefaults(ModContent.ItemType<global::CalamityMod.Items.Placeables.Furniture.KelpTorch>());
			Main.chest[ChestIndex].item[5].stack = WorldGen.genRand.Next(3, 12);
			Main.chest[ChestIndex].item[6].SetDefaults(73);
			Main.chest[ChestIndex].item[6].stack = WorldGen.genRand.Next(2, 5);
		}
	}

	public static void PlaceSnailFossil(int i, int j)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		AbyssIsland(i, j + 2, 55, 75, 35, 45, ModContent.TileType<Voidstone>(), hasChest: false, hasClumps: false, hasScoria: false);
		for (int clearX = i - 2; clearX <= i + 2; clearX++)
		{
			for (int clearY = j - 8; clearY <= j - 1; clearY++)
			{
				ShapeData circle = new ShapeData();
				GenAction blotchMod = new Modifiers.Blotches(2, 0.4);
				int radius = (int)((float)WorldGen.genRand.Next(3, 5) * WorldGen.genRand.NextFloat(0.74f, 0.82f));
				WorldUtils.Gen(new Point(clearX, clearY), new Shapes.Circle(radius), Actions.Chain(blotchMod.Output(circle)));
				WorldUtils.Gen(new Point(clearX, clearY), new ModShapes.All(circle), Actions.Chain(new Actions.ClearTile()));
			}
		}
		WorldGen.TileRunner(i, j + 4, 8.0, 5, ModContent.TileType<Voidstone>(), addTile: true);
		for (int k = i - 7; k <= i + 7; k++)
		{
			for (int l = j - 1; l <= j + 5; l++)
			{
				WorldGen.PlaceTile(k, l, ModContent.TileType<Voidstone>());
			}
		}
		WorldGen.PlaceObject(i, j - 2, (ushort)ModContent.TileType<AbyssFossilTile>());
	}

	public static void UnlockAllAbyssChests()
	{
		if (Main.dedServ)
		{
			UnlockAbyssChestsPacket.Send();
			DoUnlockAllAbyssChests();
		}
		else if (Main.netMode == 0)
		{
			DoUnlockAllAbyssChests();
		}
	}

	internal static void DoUnlockAllAbyssChests()
	{
		UnlockChests = true;
		for (int c = 0; c < Main.maxChests; c++)
		{
			Chest chest = Main.chest[c];
			if (chest != null)
			{
				Tile chestTile = Framing.GetTileSafely(chest.x, chest.y);
				if (chestTile.HasTile && chestTile.TileType == ModContent.TileType<global::CalamityMod.Tiles.Abyss.AbyssTreasureChest>() && Chest.IsLocked(chest.x, chest.y))
				{
					Chest.Unlock(chest.x, chest.y);
				}
			}
		}
		UnlockChests = false;
	}

	public static void AbyssCleanup()
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		List<ushort> blockTileTypes = new List<ushort>
		{
			(ushort)ModContent.TileType<SulphurousShale>(),
			(ushort)ModContent.TileType<AbyssGravel>(),
			(ushort)ModContent.TileType<PyreMantle>(),
			(ushort)ModContent.TileType<Voidstone>()
		};
		for (int x = 20; x < Main.maxTilesX - 20; x++)
		{
			for (int y = 20; y < Main.maxTilesY - 20; y++)
			{
				List<Point> chunkPoints = new List<Point>();
				getAttachedPoints(x, y, chunkPoints);
				int cutoffLimit = 75;
				if (chunkPoints.Count >= 1 && chunkPoints.Count < cutoffLimit)
				{
					foreach (Point item in chunkPoints)
					{
						WorldUtils.Gen(item, new Shapes.Rectangle(1, 1), Actions.Chain(new Actions.ClearTile(frameNeighbors: true), new Actions.SetLiquid()));
					}
				}
				Tile tile = Main.tile[x, y];
				if (blockTileTypes.Contains(tile.TileType))
				{
					bool num = !Main.tile[x, y - 1].HasTile && !Main.tile[x, y + 1].HasTile && !Main.tile[x - 1, y].HasTile;
					bool OnlyLeft = !Main.tile[x, y - 1].HasTile && !Main.tile[x, y + 1].HasTile && !Main.tile[x + 1, y].HasTile;
					bool OnlyDown = !Main.tile[x, y - 1].HasTile && !Main.tile[x - 1, y].HasTile && !Main.tile[x + 1, y].HasTile;
					bool OnlyUp = !Main.tile[x, y + 1].HasTile && !Main.tile[x - 1, y].HasTile && !Main.tile[x + 1, y].HasTile;
					if (num | OnlyLeft | OnlyDown | OnlyUp)
					{
						WorldGen.KillTile(x, y);
					}
				}
			}
		}
		void getAttachedPoints(int num2, int num3, List<Point> points)
		{
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			Tile t = CalamityUtils.ParanoidTileRetrieval(num2, num3);
			Point p = default(Point);
			((Point)(ref p))._002Ector(num2, num3);
			if (blockTileTypes.Contains(t.TileType) && t.HasTile && points.Count <= 75 && !points.Contains(p))
			{
				points.Add(p);
				getAttachedPoints(num2 + 1, num3, points);
				getAttachedPoints(num2 - 1, num3, points);
				getAttachedPoints(num2, num3 + 1, points);
				getAttachedPoints(num2, num3 - 1, points);
			}
		}
	}

	public static void FillTileWithWater(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		if (CalamityTileSets.IsAbyssWall[tile.WallType])
		{
			tile.LiquidAmount = byte.MaxValue;
			tile.LiquidType = 0;
		}
	}
}
