using System;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Potions.Food;
using CalamityMod.Tiles.Abyss;
using CalamityMod.Tiles.Astral;
using CalamityMod.Tiles.FurnitureAuric;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class MiscWorldgenRoutines
{
	internal static Chest AddChestWithLoot(int i, int j, ushort type = 21, uint startingSlot = 1u, int tileStyle = 0)
	{
		int chestIndex = -1;
		while (j < Main.maxTilesY - 210)
		{
			if (!WorldGen.SolidTile(i, j))
			{
				j++;
				continue;
			}
			chestIndex = WorldGen.PlaceChest(i - 1, j - 1, type, notNearOtherChests: false, tileStyle);
			break;
		}
		if (chestIndex < 0)
		{
			return null;
		}
		Chest chest = Main.chest[chestIndex];
		PlaceLootInChest(ref chest, type, startingSlot);
		return chest;
	}

	internal static void PlaceLootInChest(ref Chest chest, ushort type, uint startingSlot)
	{
		if (type == ModContent.TileType<AstralChestLocked>())
		{
			PutItemInChest(ref chest, ModContent.ItemType<StarblightSoot>(), 30, 80);
			PutItemInChest(ref chest, ModContent.ItemType<AureusCell>(), 10, 14);
			PutItemInChest(ref chest, ModContent.ItemType<ZergPotion>(), 8);
			PutItemInChest(ref chest, ModContent.ItemType<ZenPotion>(), 3, 5);
			PutItemInChest(ref chest, 75, 12, 30);
			int goldCoins = WorldGen.genRand.Next(30, 120);
			if (goldCoins > 100)
			{
				PutItemInChest(ref chest, 74);
				goldCoins -= 100;
			}
			PutItemInChest(ref chest, 73, goldCoins);
		}
		else if (type == ModContent.TileType<RustyChestTile>())
		{
			PutItemInChest(ref chest, 8, 15, 29);
			int[] obj = new int[5] { 0, 302, 298, 291, 2327 };
			obj[0] = ModContent.ItemType<HadalStew>();
			int[] potions = obj;
			PutItemInChest(ref chest, ModContent.ItemType<SulphurskinPotion>(), 4, 7, WorldGen.genRand.NextBool());
			PutItemInChest(ref chest, WorldGen.genRand.Next(potions), 1, 2, WorldGen.genRand.NextBool());
			PutItemInChest(ref chest, WorldGen.genRand.Next(potions), 1, 2, WorldGen.genRand.NextBool());
			PutItemInChest(ref chest, 187, 0, 0, WorldGen.genRand.NextBool(3));
			PutItemInChest(ref chest, 73, 2, 4);
		}
		else
		{
			int barID = (WorldGen.genRand.NextBool() ? GenVars.goldBar : GenVars.silverBar);
			PutItemInChest(ref chest, barID, 3, 10);
			PutItemInChest(ref chest, 516, 25, 50, WorldGen.genRand.NextBool());
			int[] potions2 = new int[6] { 296, 295, 299, 302, 303, 305 };
			PutItemInChest(ref chest, WorldGen.genRand.Next(potions2), 1, 2, WorldGen.genRand.NextBool());
			potions2 = new int[7] { 301, 302, 297, 293, 2351, 2329, 2329 };
			PutItemInChest(ref chest, WorldGen.genRand.Next(potions2), 1, 2, WorldGen.genRand.NextBool());
			PutItemInChest(ref chest, 2350, 1, 2, WorldGen.genRand.NextBool());
			PutItemInChest(ref chest, 73, 1, 2);
		}
		void PutItemInChest(ref Chest c, int id, int minQuantity = 0, int maxQuantity = 0, bool condition = true)
		{
			if (condition)
			{
				c.item[startingSlot].SetDefaults(id, false, null);
				c.item[startingSlot].Prefix(-1);
				if (minQuantity > 0)
				{
					if (maxQuantity < minQuantity)
					{
						maxQuantity = minQuantity;
					}
					c.item[startingSlot].stack = WorldGen.genRand.Next(minQuantity, maxQuantity + 1);
				}
				startingSlot++;
			}
		}
	}

	public static void CreateTunnel(int startX, int startY, int endX, int endY, int width = 6, int tileType = 0)
	{
		int dx = endX - startX;
		int dy = endY - startY;
		int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
		for (int i = 0; i <= steps; i++)
		{
			float t = (float)i / (float)steps;
			int x = (int)((float)startX + (float)dx * t);
			int y = (int)((float)startY + (float)dy * t);
			for (int xi = -width; xi <= width; xi++)
			{
				for (int yi = -width; yi <= width; yi++)
				{
					if (xi * xi + yi * yi <= width * width)
					{
						int tileX = x + xi;
						int tileY = y + yi;
						if (WorldGen.InWorld(tileX, tileY))
						{
							Tile tTile = Main.tile[tileX, tileY];
							tTile.HasTile = true;
							tTile.TileType = (ushort)tileType;
							WorldGen.SquareTileFrame(tileX, tileY);
						}
					}
				}
			}
		}
	}

	public static void ClearTunnel(int startX, int startY, int endX, int endY, int width = 3, int wallType = 16, int liquidType = 0, bool liquidOn = true, bool wallOn = true)
	{
		int dx = endX - startX;
		int dy = endY - startY;
		int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
		for (int i = 0; i <= steps; i++)
		{
			float t = (float)i / (float)steps;
			int x = (int)((float)startX + (float)dx * t);
			int y = (int)((float)startY + (float)dy * t);
			for (int xi = -width; xi <= width; xi++)
			{
				for (int yi = -width; yi <= width; yi++)
				{
					if (xi * xi + yi * yi > width * width)
					{
						continue;
					}
					int tileX = x + xi;
					int tileY = y + yi;
					if (WorldGen.InWorld(tileX, tileY))
					{
						Tile tTile = Main.tile[tileX, tileY];
						tTile.HasTile = false;
						if (wallOn)
						{
							tTile.WallType = (ushort)wallType;
						}
						tTile.LiquidAmount = 0;
						WorldGen.SquareWallFrame(tileX, tileY);
						if (liquidOn && WorldGen.genRand.NextBool(10))
						{
							tTile.LiquidAmount = 100;
							tTile.LiquidType = liquidType;
							WorldGen.SquareTileFrame(tileX, tileY);
						}
					}
				}
			}
		}
	}

	public static void ChasmGenerator(int i, int j, int steps, bool ocean = false)
	{
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		float maxChasmSize = steps;
		int limitIncrease = (Main.remixWorld ? 110 : 0);
		if (ocean)
		{
			int tileYLookup = j;
			if (Abyss.AtLeftSideOfWorld)
			{
				for (; !Main.tile[i + 125, tileYLookup].HasTile; tileYLookup++)
				{
				}
			}
			else
			{
				for (; !Main.tile[i - 125, tileYLookup].HasTile; tileYLookup++)
				{
				}
			}
			j = tileYLookup;
		}
		Vector2 chasmGenPosition = default(Vector2);
		chasmGenPosition.X = i;
		chasmGenPosition.Y = j;
		Vector2 randChasmGenOffset = default(Vector2);
		randChasmGenOffset.X = (float)WorldGen.genRand.Next(-1, 2) * 0.1f;
		randChasmGenOffset.Y = (float)WorldGen.genRand.Next(3, 8) * 0.2f + 0.5f;
		int five = 5;
		double chasmWidth = WorldGen.genRand.Next(5, 7) + 20;
		while (chasmWidth > 0.0)
		{
			if (maxChasmSize > 0f)
			{
				chasmWidth += (double)WorldGen.genRand.Next(10);
				chasmWidth -= (double)WorldGen.genRand.Next(10);
				float smallHoleLimit = 790f;
				if (Main.maxTilesY > 1500)
				{
					smallHoleLimit = 1360f;
					if (Main.maxTilesY > 2100)
					{
						smallHoleLimit = 1950f;
					}
				}
				if (ocean && maxChasmSize > smallHoleLimit)
				{
					if (chasmWidth < 7.0)
					{
						chasmWidth = 7.0;
					}
					if (chasmWidth > 45.0)
					{
						chasmWidth = 45.0;
					}
				}
				else
				{
					if (chasmWidth < (ocean ? 30.0 : 8.0))
					{
						chasmWidth = (ocean ? 30.0 : 8.0);
					}
					if (chasmWidth > (ocean ? 70.0 : 20.0))
					{
						chasmWidth = (ocean ? 70.0 : 20.0);
					}
					if (maxChasmSize == 1f && chasmWidth < (ocean ? 50.0 : 15.0))
					{
						chasmWidth = (ocean ? 50.0 : 15.0);
					}
				}
			}
			else if ((double)chasmGenPosition.Y > (double)(Abyss.AbyssChasmBottom + limitIncrease))
			{
				chasmWidth -= (double)(WorldGen.genRand.Next(5) + 8);
			}
			if (Main.maxTilesY > 2100)
			{
				if ((((double)chasmGenPosition.Y > (double)(Abyss.AbyssChasmBottom + limitIncrease) && maxChasmSize > 0f) & ocean) || (chasmGenPosition.Y >= (float)Main.maxTilesY && maxChasmSize > 0f && !ocean))
				{
					maxChasmSize = 0f;
				}
			}
			else if (Main.maxTilesY > 1500)
			{
				if ((((double)chasmGenPosition.Y > (double)(Abyss.AbyssChasmBottom + limitIncrease) && maxChasmSize > 0f) & ocean) || (chasmGenPosition.Y > (float)Main.maxTilesY && maxChasmSize > 0f && !ocean))
				{
					maxChasmSize = 0f;
				}
			}
			else if ((((double)chasmGenPosition.Y > (double)(Abyss.AbyssChasmBottom + limitIncrease) && maxChasmSize > 0f) & ocean) || (chasmGenPosition.Y > (float)Main.maxTilesY && maxChasmSize > 0f && !ocean))
			{
				maxChasmSize = 0f;
			}
			maxChasmSize--;
			int chasmWidthMin;
			int chasmWidthMax;
			int chasmHeightMin;
			int chasmHeightMax;
			if (maxChasmSize > (float)five)
			{
				chasmWidthMin = (int)((double)chasmGenPosition.X - chasmWidth * 0.5);
				chasmWidthMax = (int)((double)chasmGenPosition.X + chasmWidth * 0.5);
				chasmHeightMin = (int)((double)chasmGenPosition.Y - chasmWidth * 0.5);
				chasmHeightMax = (int)((double)chasmGenPosition.Y + chasmWidth * 0.5);
				if (chasmWidthMin < 0)
				{
					chasmWidthMin = 0;
				}
				if (chasmWidthMax > Main.maxTilesX - 1)
				{
					chasmWidthMax = Main.maxTilesX - 1;
				}
				if (chasmHeightMin < 0)
				{
					chasmHeightMin = 0;
				}
				if (chasmHeightMax > Main.maxTilesY)
				{
					chasmHeightMax = Main.maxTilesY;
				}
				for (int k = chasmWidthMin; k < chasmWidthMax; k++)
				{
					for (int l = chasmHeightMin; l < chasmHeightMax; l++)
					{
						if ((double)(Math.Abs((float)k - chasmGenPosition.X) + Math.Abs((float)l - chasmGenPosition.Y)) < chasmWidth * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-5, 6) * 0.015))
						{
							if (ocean)
							{
								Main.tile[k, l].Get<TileWallWireStateData>().HasTile = false;
								Main.tile[k, l].LiquidAmount = byte.MaxValue;
								Main.tile[k, l].Get<LiquidData>().LiquidType = 0;
							}
							else
							{
								Main.tile[k, l].Get<TileWallWireStateData>().HasTile = false;
								Main.tile[k, l].LiquidAmount = byte.MaxValue;
								Main.tile[k, l].Get<LiquidData>().LiquidType = 1;
							}
						}
					}
				}
			}
			chasmGenPosition += randChasmGenOffset;
			randChasmGenOffset.X += (float)WorldGen.genRand.Next(-1, 2) * 0.01f;
			if ((double)randChasmGenOffset.X > 0.02)
			{
				randChasmGenOffset.X = 0.02f;
			}
			if ((double)randChasmGenOffset.X < -0.02)
			{
				randChasmGenOffset.X = -0.02f;
			}
			chasmWidthMin = (int)((double)chasmGenPosition.X - chasmWidth * 1.1);
			chasmWidthMax = (int)((double)chasmGenPosition.X + chasmWidth * 1.1);
			chasmHeightMin = (int)((double)chasmGenPosition.Y - chasmWidth * 1.1);
			chasmHeightMax = (int)((double)chasmGenPosition.Y + chasmWidth * 1.1);
			if (chasmWidthMin < 1)
			{
				chasmWidthMin = 1;
			}
			if (chasmWidthMax > Main.maxTilesX - 1)
			{
				chasmWidthMax = Main.maxTilesX - 1;
			}
			if (chasmHeightMin < 0)
			{
				chasmHeightMin = 0;
			}
			if (chasmHeightMax > Main.maxTilesY)
			{
				chasmHeightMax = Main.maxTilesY;
			}
			for (int m = chasmWidthMin; m < chasmWidthMax; m++)
			{
				for (int n = chasmHeightMin; n < chasmHeightMax; n++)
				{
					if ((double)(Math.Abs((float)m - chasmGenPosition.X) + Math.Abs((float)n - chasmGenPosition.Y)) < chasmWidth * 1.1 * (1.0 + (double)WorldGen.genRand.Next(-5, 6) * 0.015))
					{
						if (n > j + WorldGen.genRand.Next(7, 16))
						{
							Main.tile[m, n].Get<TileWallWireStateData>().HasTile = false;
						}
						if (steps <= five)
						{
							Main.tile[m, n].Get<TileWallWireStateData>().HasTile = false;
						}
						if (ocean)
						{
							Main.tile[m, n].LiquidAmount = byte.MaxValue;
							Main.tile[m, n].Get<LiquidData>().LiquidType = 0;
						}
						else
						{
							Main.tile[m, n].LiquidAmount = byte.MaxValue;
							Main.tile[m, n].Get<LiquidData>().LiquidType = 1;
						}
					}
				}
			}
			for (int r = chasmWidthMin; r < chasmWidthMax; r++)
			{
				for (int s = chasmHeightMin; s < chasmHeightMax; s++)
				{
					if ((double)(Math.Abs((float)r - chasmGenPosition.X) + Math.Abs((float)s - chasmGenPosition.Y)) < chasmWidth * 1.1 * (1.0 + (double)WorldGen.genRand.Next(-5, 6) * 0.015))
					{
						if (ocean)
						{
							Main.tile[r, s].LiquidAmount = byte.MaxValue;
							Main.tile[r, s].Get<LiquidData>().LiquidType = 0;
						}
						else
						{
							Main.tile[r, s].LiquidAmount = byte.MaxValue;
							Main.tile[r, s].Get<LiquidData>().LiquidType = 1;
						}
						if (steps <= five)
						{
							Main.tile[r, s].Get<TileWallWireStateData>().HasTile = false;
						}
					}
				}
			}
		}
	}

	public static void SmartGemGen()
	{
		double oneThirdOfUnderground = ((double)Main.UnderworldLayer - Main.worldSurface) / 3.0;
		double verticalStartFactor_Layer1 = Main.worldSurface;
		double verticalStartFactor_Layer2 = verticalStartFactor_Layer1 + oneThirdOfUnderground;
		double verticalStartFactor_Layer3 = verticalStartFactor_Layer1 + oneThirdOfUnderground * 2.0;
		for (int x = 0; x < Main.maxTilesX; x++)
		{
			for (int y = 0; y < Main.maxTilesY; y++)
			{
				if ((double)y > verticalStartFactor_Layer3)
				{
					if (!(Main.tile[x, y] != null))
					{
						continue;
					}
					if (Main.remixWorld)
					{
						if (Main.tile[x, y].TileType == 68 || Main.tile[x, y].TileType == 64)
						{
							Main.tile[x, y].TileType = 66;
						}
						else if (Main.tile[x, y].TileType == 65 || Main.tile[x, y].TileType == 63)
						{
							Main.tile[x, y].TileType = 67;
						}
					}
					else if (Main.tile[x, y].TileType == 65 || Main.tile[x, y].TileType == 63)
					{
						Main.tile[x, y].TileType = 68;
					}
					else if (Main.tile[x, y].TileType == 66 || Main.tile[x, y].TileType == 67)
					{
						Main.tile[x, y].TileType = 64;
					}
				}
				else if ((double)y > verticalStartFactor_Layer2)
				{
					if (Main.tile[x, y] != null)
					{
						if (Main.tile[x, y].TileType == 68 || Main.tile[x, y].TileType == 64)
						{
							Main.tile[x, y].TileType = 65;
						}
						else if (Main.tile[x, y].TileType == 66 || Main.tile[x, y].TileType == 67)
						{
							Main.tile[x, y].TileType = 63;
						}
					}
				}
				else
				{
					if (!((double)y > verticalStartFactor_Layer1) || !(Main.tile[x, y] != null))
					{
						continue;
					}
					if (Main.remixWorld)
					{
						if (Main.tile[x, y].TileType == 65 || Main.tile[x, y].TileType == 63)
						{
							Main.tile[x, y].TileType = 68;
						}
						else if (Main.tile[x, y].TileType == 66 || Main.tile[x, y].TileType == 67)
						{
							Main.tile[x, y].TileType = 64;
						}
					}
					else if (Main.tile[x, y].TileType == 68 || Main.tile[x, y].TileType == 64)
					{
						Main.tile[x, y].TileType = 66;
					}
					else if (Main.tile[x, y].TileType == 65 || Main.tile[x, y].TileType == 63)
					{
						Main.tile[x, y].TileType = 67;
					}
				}
			}
		}
	}

	public static void GenerateAuricLandMines()
	{
		int landMineID = ModContent.TileType<AuricLandMineTile>();
		int landMineChance = (Main.zenithWorld ? 150 : 300);
		float maxDepth = (float)Main.maxTilesY * (Main.zenithWorld ? 0.75f : 0.5f);
		for (int x = 0; x < Main.maxTilesX; x++)
		{
			for (int y = 0; (float)y < maxDepth; y++)
			{
				Tile t = CalamityUtils.ParanoidTileRetrieval(x, y);
				Tile above = CalamityUtils.ParanoidTileRetrieval(x, y - 1);
				if (t != null && above != null && (t.TileType == 25 || t.TileType == 203) && !above.HasTile && WorldGen.genRand.NextBool(landMineChance))
				{
					WorldGen.SlopeTile(x, y);
					WorldGen.PlaceTile(x, y - 1, landMineID);
				}
			}
		}
	}
}
