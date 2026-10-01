using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class CustomTemple
{
	public static Point NewAlterPosition;

	public static Vector2 FinalRoomSize;

	public static void NewJungleTemple()
	{
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		bool success = false;
		Rectangle InflatedSunkenSeaLocation = default(Rectangle);
		Rectangle TempleLocation = default(Rectangle);
		while (!success)
		{
			int x = ((GenVars.dungeonX >= Main.maxTilesX / 2) ? WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.15), (int)((double)Main.maxTilesX * 0.4)) : WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.6), (int)((double)Main.maxTilesX * 0.85)));
			int y = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 500);
			if (Main.remixWorld)
			{
				while (Main.tile[x, y].HasTile || Main.tile[x, y].WallType > 0 || y > (int)(Main.worldSurface - 5.0))
				{
					y--;
				}
				y++;
				if (Main.tile[x, y].HasTile && (Main.tile[x, y].TileType == 60 || Main.tile[x, y].TileType == 59))
				{
					success = true;
					GenNewTemple(x, y);
				}
			}
			else if (Main.tile[x, y].HasTile && Main.tile[x, y].TileType == 60)
			{
				Rectangle ugDesert = GenVars.UndergroundDesertLocation;
				((Rectangle)(ref InflatedSunkenSeaLocation))._002Ector(((Rectangle)(ref ugDesert)).Left - 160, ((Rectangle)(ref ugDesert)).Center.Y - 160, ugDesert.Width + 320, ugDesert.Height / 2 + 320);
				((Rectangle)(ref TempleLocation))._002Ector(x - 80, y - 80, 160, 160);
				if (!((Rectangle)(ref TempleLocation)).Intersects(InflatedSunkenSeaLocation))
				{
					success = true;
					GenNewTemple(x, y);
				}
			}
		}
	}

	public static void GenNewTemple(int x, int y)
	{
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_079d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_083f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0841: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_084d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_0739: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0757: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_0774: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0780: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6e: Unknown result type (might be due to invalid IL or missing references)
		Rectangle[] roomBounds = (Rectangle[])(object)new Rectangle[200];
		int totalRooms = (int)((float)Main.maxTilesX / 4200f * (float)WorldGen.genRand.Next(12, 16));
		if (WorldGen.getGoodWorldGen)
		{
			totalRooms *= 3;
		}
		int xDirection = WorldGen.genRand.NextBool(2).ToDirectionInt();
		int initalDirection = xDirection;
		int currentRoomPositionX = x;
		int currentRoomPositionY = y;
		int xDirectionChangePromptThreshold = WorldGen.genRand.Next(1, 3);
		int totalAttempts = 0;
		Rectangle rectangle = default(Rectangle);
		for (int i = 0; i < totalRooms; i++)
		{
			totalAttempts++;
			int currentXDirection = xDirection;
			int roomPositionX = currentRoomPositionX;
			int roomPositionY = currentRoomPositionY;
			bool isValidRoom = false;
			int width = 0;
			int height = 0;
			int xOffset = -10;
			((Rectangle)(ref rectangle))._002Ector(roomPositionX - width / 2, roomPositionY - height / 2, width, height);
			while (!isValidRoom)
			{
				roomPositionX = currentRoomPositionX;
				roomPositionY = currentRoomPositionY;
				width = WorldGen.genRand.Next(30, 46);
				height = WorldGen.genRand.Next(25, 31);
				if (i == totalRooms - 1)
				{
					width = (int)FinalRoomSize.X;
					height = (WorldGen.getGoodWorldGen ? ((int)FinalRoomSize.Y / 2) : ((int)FinalRoomSize.Y));
					roomPositionY += WorldGen.genRand.Next(6, 9);
				}
				if (totalAttempts > xDirectionChangePromptThreshold)
				{
					roomPositionY += WorldGen.genRand.Next(height + 1, height + 3) + xOffset;
					roomPositionX += WorldGen.genRand.Next(-2, 3);
					currentXDirection = xDirection * -1;
				}
				else
				{
					roomPositionX += (WorldGen.genRand.Next(width + 1, width + 3) + xOffset) * currentXDirection;
					roomPositionY += WorldGen.genRand.Next(-2, 3);
				}
				isValidRoom = true;
				((Rectangle)(ref rectangle))._002Ector(roomPositionX - width / 2, roomPositionY - height / 2, width, height);
				for (int j = 0; j < i; j++)
				{
					if (((Rectangle)(ref rectangle)).Intersects(roomBounds[j]))
					{
						isValidRoom = false;
					}
					if (WorldGen.genRand.NextBool(100))
					{
						xOffset++;
					}
				}
			}
			if (totalAttempts > xDirectionChangePromptThreshold)
			{
				xDirectionChangePromptThreshold++;
				totalAttempts = 1;
			}
			roomBounds[i] = rectangle;
			xDirection = currentXDirection;
			currentRoomPositionX = roomPositionX;
			currentRoomPositionY = roomPositionY;
		}
		for (int k = 0; k < totalRooms; k++)
		{
			for (int l = 0; l < 2; l++)
			{
				for (int m = 0; m < totalRooms; m++)
				{
					for (int n = 0; n < 2; n++)
					{
						int roomPositionX2 = roomBounds[k].X;
						if (l == 1)
						{
							roomPositionX2 += roomBounds[k].Width - 1;
						}
						int roomTop = roomBounds[k].Y;
						int roomBottom = roomTop + roomBounds[k].Height;
						int checkRoomPositionX = roomBounds[m].X;
						if (n == 1)
						{
							checkRoomPositionX += roomBounds[m].Width - 1;
						}
						int checkRoomTop = roomBounds[m].Y;
						int checkRoomBottom = checkRoomTop + roomBounds[m].Height;
						while (roomPositionX2 != checkRoomPositionX || roomTop != checkRoomTop || roomBottom != checkRoomBottom)
						{
							if (roomPositionX2 < checkRoomPositionX)
							{
								roomPositionX2++;
							}
							if (roomPositionX2 > checkRoomPositionX)
							{
								roomPositionX2--;
							}
							if (roomTop < checkRoomTop)
							{
								roomTop++;
							}
							if (roomTop > checkRoomTop)
							{
								roomTop--;
							}
							if (roomBottom < checkRoomBottom)
							{
								roomBottom++;
							}
							if (roomBottom > checkRoomBottom)
							{
								roomBottom--;
							}
							for (int num = roomTop; num < roomBottom; num++)
							{
								Main.tile[roomPositionX2, num].Get<TileWallWireStateData>().HasTile = true;
								Main.tile[roomPositionX2, num].TileType = 226;
								Main.tile[roomPositionX2, num].LiquidAmount = 0;
								Main.tile[roomPositionX2, num].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
								Main.tile[roomPositionX2, num].Get<TileWallWireStateData>().IsHalfBlock = false;
							}
						}
					}
				}
			}
		}
		for (int num2 = 0; num2 < totalRooms; num2++)
		{
			bool isLastRoom = num2 == totalRooms - 1;
			for (int num3 = roomBounds[num2].X; num3 < roomBounds[num2].X + roomBounds[num2].Width; num3++)
			{
				for (int num4 = roomBounds[num2].Y; num4 < roomBounds[num2].Y + roomBounds[num2].Height; num4++)
				{
					Main.tile[num3, num4].Get<TileWallWireStateData>().HasTile = true;
					Main.tile[num3, num4].TileType = 226;
					Main.tile[num3, num4].LiquidAmount = 0;
					Main.tile[num3, num4].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
					Main.tile[num3, num4].Get<TileWallWireStateData>().IsHalfBlock = false;
				}
			}
			if (!isLastRoom)
			{
				GenerateGenericRoom(roomBounds[num2]);
			}
			else
			{
				GenerateShrineRoom(roomBounds[num2]);
			}
		}
		Vector2D pathPosition = default(Vector2D);
		((Vector2D)(ref pathPosition))._002Ector((double)x, (double)y);
		for (int num5 = 0; num5 < totalRooms; num5++)
		{
			Rectangle destinationArea = roomBounds[num5];
			destinationArea.X += 8;
			destinationArea.Y += 8;
			destinationArea.Width -= 16;
			destinationArea.Height -= 16;
			bool reachedDestination = false;
			while (!reachedDestination)
			{
				int destinationX = WorldGen.genRand.Next(destinationArea.X, destinationArea.X + destinationArea.Width);
				int destinationY = WorldGen.genRand.Next(destinationArea.Y, destinationArea.Y + destinationArea.Height);
				pathPosition = WorldGen.templePather(pathPosition, destinationX, destinationY);
				if (pathPosition.X == (double)destinationX && pathPosition.Y == (double)destinationY)
				{
					reachedDestination = true;
				}
			}
			if (num5 >= totalRooms - 1)
			{
				continue;
			}
			reachedDestination = false;
			if (!WorldGen.genRand.NextBool(3))
			{
				Rectangle nextRoom = roomBounds[num5 + 1];
				if (nextRoom.Y >= roomBounds[num5].Y + roomBounds[num5].Height)
				{
					destinationArea.X = nextRoom.X;
					if (nextRoom.X < roomBounds[num5].X)
					{
						destinationArea.X += (int)((double)nextRoom.Width * 0.2);
					}
					else
					{
						destinationArea.X += (int)((float)nextRoom.Width * 0.8f);
					}
					destinationArea.Y = nextRoom.Y;
				}
				else
				{
					destinationArea.X = (roomBounds[num5].X + roomBounds[num5].Width / 2 + nextRoom.X + nextRoom.Width / 2) / 2;
					destinationArea.Y = (int)((double)nextRoom.Y + (double)nextRoom.Height * 0.8);
				}
				while (!reachedDestination)
				{
					int destinationX2 = WorldGen.genRand.Next(destinationArea.X - 4, destinationArea.X + 5);
					int destinationY2 = WorldGen.genRand.Next(destinationArea.Y - 4, destinationArea.Y + 5);
					pathPosition = WorldGen.templePather(pathPosition, destinationX2, destinationY2);
					if (pathPosition.X == (double)destinationX2 && pathPosition.Y == (double)destinationY2)
					{
						reachedDestination = true;
					}
				}
				continue;
			}
			Rectangle nextRoom2 = roomBounds[num5 + 1];
			int roomCenterMidpointX = (roomBounds[num5].X + roomBounds[num5].Width / 2 + nextRoom2.X + nextRoom2.Width / 2) / 2;
			int roomCenterMidpointY = (roomBounds[num5].Y + roomBounds[num5].Height / 2 + nextRoom2.Y + nextRoom2.Height / 2) / 2;
			while (!reachedDestination)
			{
				int destinationX3 = WorldGen.genRand.Next(roomCenterMidpointX - 4, roomCenterMidpointX + 5);
				int destinationY3 = WorldGen.genRand.Next(roomCenterMidpointY - 4, roomCenterMidpointY + 5);
				pathPosition = WorldGen.templePather(pathPosition, destinationX3, destinationY3);
				if (pathPosition.X == (double)destinationX3 && pathPosition.Y == (double)destinationY3)
				{
					reachedDestination = true;
				}
			}
		}
		int farthestRoomLeft = Main.maxTilesX - 20;
		int farthestRoomRight = 20;
		int farthestRoomTop = Main.maxTilesY - 20;
		int farthestRoomBottom = 20;
		for (int num6 = 0; num6 < totalRooms; num6++)
		{
			if (roomBounds[num6].X < farthestRoomLeft)
			{
				farthestRoomLeft = roomBounds[num6].X;
			}
			if (roomBounds[num6].X + roomBounds[num6].Width > farthestRoomRight)
			{
				farthestRoomRight = roomBounds[num6].X + roomBounds[num6].Width;
			}
			if (roomBounds[num6].Y < farthestRoomTop)
			{
				farthestRoomTop = roomBounds[num6].Y;
			}
			if (roomBounds[num6].Y + roomBounds[num6].Height > farthestRoomBottom)
			{
				farthestRoomBottom = roomBounds[num6].Y + roomBounds[num6].Height;
			}
		}
		farthestRoomLeft -= 10;
		farthestRoomRight += 10;
		farthestRoomTop -= 10;
		farthestRoomBottom += 10;
		for (int xRoomPosition = farthestRoomLeft; xRoomPosition < farthestRoomRight; xRoomPosition++)
		{
			for (int num7 = farthestRoomTop; num7 < farthestRoomBottom; num7++)
			{
				WorldGen.outerTempled(xRoomPosition, num7);
			}
		}
		for (int xRoomPosition2 = farthestRoomRight; xRoomPosition2 >= farthestRoomLeft; xRoomPosition2--)
		{
			for (int yRoomPosition = farthestRoomTop; yRoomPosition < farthestRoomBottom / 2; yRoomPosition++)
			{
				WorldGen.outerTempled(xRoomPosition2, yRoomPosition);
			}
		}
		for (int num8 = farthestRoomTop; num8 < farthestRoomBottom; num8++)
		{
			for (int num9 = farthestRoomLeft; num9 < farthestRoomRight; num9++)
			{
				WorldGen.outerTempled(num9, num8);
			}
		}
		for (int yRoomPosition2 = farthestRoomBottom; yRoomPosition2 >= farthestRoomTop; yRoomPosition2--)
		{
			for (int num10 = farthestRoomLeft; num10 < farthestRoomRight; num10++)
			{
				WorldGen.outerTempled(num10, yRoomPosition2);
			}
		}
		xDirection = -initalDirection;
		Vector2 endPathPosition = default(Vector2);
		((Vector2)(ref endPathPosition))._002Ector((float)x, (float)y);
		int yArea = 4;
		bool success = false;
		int totralDescentTries = 0;
		int endPathRisePrompt = WorldGen.genRand.Next(12, 14);
		while (!success)
		{
			totralDescentTries++;
			if (totralDescentTries >= endPathRisePrompt)
			{
				totralDescentTries = 0;
				endPathPosition.Y--;
			}
			endPathPosition.X += xDirection;
			success = true;
			for (int dy = (int)endPathPosition.Y - yArea; (float)dy < endPathPosition.Y + (float)yArea; dy++)
			{
				if (Main.tile[(int)endPathPosition.X, dy].WallType == 87 || (Main.tile[(int)endPathPosition.X, dy].HasTile && Main.tile[(int)endPathPosition.X, dy].TileType == 226))
				{
					success = false;
				}
				if (Main.tile[(int)endPathPosition.X, dy].HasTile && Main.tile[(int)endPathPosition.X, dy].TileType == 226)
				{
					Main.tile[(int)endPathPosition.X, dy].Get<TileWallWireStateData>().HasTile = false;
					Main.tile[(int)endPathPosition.X, dy].WallType = 87;
				}
			}
		}
		int doorPositionY;
		for (doorPositionY = y; !Main.tile[x, doorPositionY].HasTile; doorPositionY++)
		{
		}
		doorPositionY -= 4;
		int doorTop = doorPositionY;
		while ((Main.tile[x, doorTop].HasTile && Main.tile[x, doorTop].TileType == 226) || Main.tile[x, doorTop].WallType == 87)
		{
			doorTop--;
		}
		doorTop += 2;
		for (int dx = x - 1; dx <= x + 1; dx++)
		{
			for (int num11 = doorTop; num11 <= doorPositionY; num11++)
			{
				Main.tile[dx, num11].Get<TileWallWireStateData>().HasTile = true;
				Main.tile[dx, num11].TileType = 226;
				Main.tile[dx, num11].LiquidAmount = 0;
				Main.tile[dx, num11].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
				Main.tile[dx, num11].Get<TileWallWireStateData>().IsHalfBlock = false;
			}
		}
		for (int num12 = x - 4; num12 <= x + 4; num12++)
		{
			for (int num13 = doorPositionY - 1; num13 < doorPositionY + 3; num13++)
			{
				Main.tile[num12, num13].Get<TileWallWireStateData>().HasTile = false;
				Main.tile[num12, num13].WallType = 87;
			}
		}
		for (int num14 = x - 1; num14 <= x + 1; num14++)
		{
			for (int num15 = doorPositionY - 5; num15 <= doorPositionY + 8; num15++)
			{
				Main.tile[num14, num15].Get<TileWallWireStateData>().HasTile = true;
				Main.tile[num14, num15].TileType = 226;
				Main.tile[num14, num15].LiquidAmount = 0;
				Main.tile[num14, num15].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
				Main.tile[num14, num15].Get<TileWallWireStateData>().IsHalfBlock = false;
			}
		}
		for (int num16 = x - 1; num16 <= x + 1; num16++)
		{
			for (int num17 = doorPositionY; num17 < doorPositionY + 3; num17++)
			{
				Main.tile[num16, num17].Get<TileWallWireStateData>().HasTile = false;
				Main.tile[num16, num17].WallType = 87;
			}
		}
		WorldGen.PlaceTile(x, doorPositionY, 10, mute: true, forced: false, -1, 11);
		for (int num18 = farthestRoomLeft; num18 < farthestRoomRight; num18++)
		{
			for (int num19 = farthestRoomTop; num19 < farthestRoomBottom; num19++)
			{
				WorldGen.templeCleaner(num18, num19);
			}
		}
		for (int dx2 = farthestRoomBottom; dx2 >= farthestRoomTop; dx2--)
		{
			for (int dy2 = farthestRoomRight; dy2 >= farthestRoomLeft; dy2--)
			{
				WorldGen.templeCleaner(dy2, dx2);
			}
		}
		for (int num20 = farthestRoomLeft; num20 < farthestRoomRight; num20++)
		{
			for (int num21 = farthestRoomTop; num21 < farthestRoomBottom; num21++)
			{
				bool shouldPlaceWalls = true;
				for (int num22 = num20 - 1; num22 <= num20 + 1; num22++)
				{
					for (int num23 = num21 - 1; num23 <= num21 + 1; num23++)
					{
						if ((!Main.tile[num22, num23].HasTile || Main.tile[num22, num23].TileType != 226) && Main.tile[num22, num23].WallType != 87)
						{
							shouldPlaceWalls = false;
							break;
						}
					}
				}
				if (shouldPlaceWalls)
				{
					Main.tile[num20, num21].WallType = 87;
				}
			}
		}
		float totalSpikeAreasToSawpn = (float)totalRooms * 1.3f;
		int spikeAreaSpawnAttempts = 0;
		while (totalSpikeAreasToSawpn > 0f)
		{
			spikeAreaSpawnAttempts++;
			int roomToFillIndex = WorldGen.genRand.Next(totalRooms);
			int randomPointInRoomX = WorldGen.genRand.Next(roomBounds[roomToFillIndex].X, roomBounds[roomToFillIndex].X + roomBounds[roomToFillIndex].Width);
			int randomPointInRoomY = WorldGen.genRand.Next(roomBounds[roomToFillIndex].Y, roomBounds[roomToFillIndex].Y + roomBounds[roomToFillIndex].Height);
			if (Main.tile[randomPointInRoomX, randomPointInRoomY].WallType == 87 && !Main.tile[randomPointInRoomX, randomPointInRoomY].HasTile)
			{
				bool successPlacingSpikes = false;
				if (WorldGen.genRand.NextBool(2))
				{
					int xMoveDirection;
					for (xMoveDirection = WorldGen.genRand.NextBool(2).ToDirectionInt(); !Main.tile[randomPointInRoomX, randomPointInRoomY].HasTile; randomPointInRoomY += xMoveDirection)
					{
					}
					randomPointInRoomY -= xMoveDirection;
					int yMoveDirection = WorldGen.genRand.Next(2);
					int areaToCheck = WorldGen.genRand.Next(8, 10);
					bool noDoorInWay = true;
					for (int num24 = randomPointInRoomX - areaToCheck; num24 < randomPointInRoomX + areaToCheck; num24++)
					{
						for (int num25 = randomPointInRoomY - areaToCheck; num25 < randomPointInRoomY + areaToCheck; num25++)
						{
							if (Main.tile[num24, num25].HasTile && Main.tile[num24, num25].TileType == 10)
							{
								noDoorInWay = false;
								break;
							}
						}
					}
					if (noDoorInWay)
					{
						for (int num26 = randomPointInRoomX - areaToCheck; num26 < randomPointInRoomX + areaToCheck; num26++)
						{
							for (int num27 = randomPointInRoomY - areaToCheck; num27 < randomPointInRoomY + areaToCheck; num27++)
							{
								if (WorldGen.SolidTile(num26, num27) && Main.tile[num26, num27].TileType != 232 && !WorldGen.SolidTile(num26, num27 - xMoveDirection))
								{
									Main.tile[num26, num27].TileType = 232;
									successPlacingSpikes = true;
									if (yMoveDirection == 0)
									{
										Main.tile[num26, num27 - 1].TileType = 232;
										Main.tile[num26, num27 - 1].Get<TileWallWireStateData>().HasTile = true;
									}
									else
									{
										Main.tile[num26, num27 + 1].TileType = 232;
										Main.tile[num26, num27 + 1].Get<TileWallWireStateData>().HasTile = true;
									}
									yMoveDirection++;
									if (yMoveDirection > 1)
									{
										yMoveDirection = 0;
									}
								}
							}
						}
					}
					if (successPlacingSpikes)
					{
						spikeAreaSpawnAttempts = 0;
						totalSpikeAreasToSawpn--;
					}
				}
				else
				{
					int xMoveDirection2;
					for (xMoveDirection2 = WorldGen.genRand.NextBool(2).ToDirectionInt(); !Main.tile[randomPointInRoomX, randomPointInRoomY].HasTile; randomPointInRoomX += xMoveDirection2)
					{
					}
					randomPointInRoomX -= xMoveDirection2;
					int yMoveDirection2 = WorldGen.genRand.Next(2);
					int areaToCheck2 = WorldGen.genRand.Next(8, 10);
					bool noDoorInWay2 = true;
					for (int num28 = randomPointInRoomX - areaToCheck2; num28 < randomPointInRoomX + areaToCheck2; num28++)
					{
						for (int num29 = randomPointInRoomY - areaToCheck2; num29 < randomPointInRoomY + areaToCheck2; num29++)
						{
							if (Main.tile[num28, num29].HasTile && Main.tile[num28, num29].TileType == 10)
							{
								noDoorInWay2 = false;
								break;
							}
						}
					}
					if (noDoorInWay2)
					{
						for (int num30 = randomPointInRoomX - areaToCheck2; num30 < randomPointInRoomX + areaToCheck2; num30++)
						{
							for (int num31 = randomPointInRoomY - areaToCheck2; num31 < randomPointInRoomY + areaToCheck2; num31++)
							{
								if (WorldGen.SolidTile(num30, num31) && Main.tile[num30, num31].TileType != 232 && !WorldGen.SolidTile(num30 - xMoveDirection2, num31))
								{
									Main.tile[num30, num31].TileType = 232;
									successPlacingSpikes = true;
									if (yMoveDirection2 == 0)
									{
										Main.tile[num30 - 1, num31].TileType = 232;
										Main.tile[num30 - 1, num31].Get<TileWallWireStateData>().HasTile = true;
									}
									else
									{
										Main.tile[num30 + 1, num31].TileType = 232;
										Main.tile[num30 + 1, num31].Get<TileWallWireStateData>().HasTile = true;
									}
									yMoveDirection2++;
									if (yMoveDirection2 > 1)
									{
										yMoveDirection2 = 0;
									}
								}
							}
						}
					}
					if (successPlacingSpikes)
					{
						spikeAreaSpawnAttempts = 0;
						totalSpikeAreasToSawpn--;
					}
				}
			}
			if (spikeAreaSpawnAttempts > 1000)
			{
				spikeAreaSpawnAttempts = 0;
				totalSpikeAreasToSawpn--;
			}
		}
		GenVars.tLeft = farthestRoomLeft;
		GenVars.tRight = farthestRoomRight;
		GenVars.tTop = farthestRoomTop;
		GenVars.tBottom = farthestRoomBottom;
		GenVars.tRooms = totalRooms;
	}

	public static void GenerateGenericRoom(Rectangle roomBounds)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		int roomLeft = roomBounds.X;
		int roomRight = roomLeft + roomBounds.Width;
		int roomTop = roomBounds.Y;
		int roomBottom = roomTop + roomBounds.Height;
		roomLeft += WorldGen.genRand.Next(4, 7);
		roomRight -= WorldGen.genRand.Next(4, 7);
		roomTop += WorldGen.genRand.Next(4, 7);
		roomBottom -= WorldGen.genRand.Next(4, 7);
		int offsettedRoomLeft = roomLeft;
		int offsettedRoomRight = roomRight;
		int offsettedRoomTop = roomTop;
		int offsettedRoomBottom = roomBottom;
		int roomCenterX = (roomLeft + roomRight) / 2;
		int roomCenterY = (roomTop + roomBottom) / 2;
		for (int dx = roomLeft; dx < roomRight; dx++)
		{
			for (int dy = roomTop; dy < roomBottom; dy++)
			{
				if (WorldGen.genRand.NextBool(20))
				{
					offsettedRoomTop += WorldGen.genRand.NextBool(2).ToDirectionInt();
				}
				if (WorldGen.genRand.NextBool(20))
				{
					offsettedRoomBottom += WorldGen.genRand.NextBool(2).ToDirectionInt();
				}
				if (WorldGen.genRand.NextBool(20))
				{
					offsettedRoomLeft += WorldGen.genRand.NextBool(2).ToDirectionInt();
				}
				if (WorldGen.genRand.NextBool(20))
				{
					offsettedRoomRight += WorldGen.genRand.NextBool(2).ToDirectionInt();
				}
				offsettedRoomLeft = Utils.Clamp(offsettedRoomLeft, roomLeft, roomCenterX);
				offsettedRoomRight = Utils.Clamp(offsettedRoomRight, roomCenterX, roomRight);
				offsettedRoomTop = Utils.Clamp(offsettedRoomTop, roomTop, roomCenterY);
				offsettedRoomBottom = Utils.Clamp(offsettedRoomBottom, roomCenterY, roomBottom);
				if (dx >= offsettedRoomLeft && ((dx < offsettedRoomRight) & (dy >= offsettedRoomTop)) && dy <= offsettedRoomBottom)
				{
					Main.tile[dx, dy].Get<TileWallWireStateData>().HasTile = false;
					Main.tile[dx, dy].LiquidAmount = 0;
					Main.tile[dx, dy].WallType = 87;
				}
			}
		}
		for (int dy2 = roomBottom; dy2 > roomTop; dy2--)
		{
			for (int dx2 = roomRight; dx2 > roomLeft; dx2--)
			{
				if (WorldGen.genRand.NextBool(20))
				{
					offsettedRoomTop += WorldGen.genRand.NextBool(2).ToDirectionInt();
				}
				if (WorldGen.genRand.NextBool(20))
				{
					offsettedRoomBottom += WorldGen.genRand.NextBool(2).ToDirectionInt();
				}
				if (WorldGen.genRand.NextBool(20))
				{
					offsettedRoomLeft += WorldGen.genRand.NextBool(2).ToDirectionInt();
				}
				if (WorldGen.genRand.NextBool(20))
				{
					offsettedRoomRight += WorldGen.genRand.NextBool(2).ToDirectionInt();
				}
				offsettedRoomLeft = Utils.Clamp(offsettedRoomLeft, roomLeft, roomCenterX);
				offsettedRoomRight = Utils.Clamp(offsettedRoomRight, roomCenterX, roomRight);
				offsettedRoomTop = Utils.Clamp(offsettedRoomTop, roomTop, roomCenterY);
				offsettedRoomBottom = Utils.Clamp(offsettedRoomBottom, roomCenterY, roomBottom);
				if (dx2 >= offsettedRoomLeft && ((dx2 < offsettedRoomRight) & (dy2 >= offsettedRoomTop)) && dy2 <= offsettedRoomBottom)
				{
					Main.tile[dx2, dy2].Get<TileWallWireStateData>().HasTile = false;
					Main.tile[dx2, dy2].LiquidAmount = 0;
					Main.tile[dx2, dy2].WallType = 87;
				}
			}
		}
	}

	public static void GenerateShrineRoom(Rectangle roomBounds)
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		for (int dx = ((Rectangle)(ref roomBounds)).Left; dx < ((Rectangle)(ref roomBounds)).Right; dx++)
		{
			for (int dy = ((Rectangle)(ref roomBounds)).Top; dy < ((Rectangle)(ref roomBounds)).Bottom; dy++)
			{
				Main.tile[dx, dy].Get<TileWallWireStateData>().HasTile = false;
				Main.tile[dx, dy].LiquidAmount = 0;
				Main.tile[dx, dy].WallType = 87;
			}
		}
		int roomCenterX = ((Rectangle)(ref roomBounds)).Center.X;
		int roomTop = ((Rectangle)(ref roomBounds)).Top;
		int roomBottom = ((Rectangle)(ref roomBounds)).Bottom;
		int shrinePedestalWidth = roomBounds.Width / 6 + WorldGen.genRand.Next(6);
		if (shrinePedestalWidth % 2 != 0)
		{
			shrinePedestalWidth++;
		}
		for (int i = 0; i < shrinePedestalWidth / 2; i++)
		{
			int height = calculateHeightFromOutwardness(i);
			for (int y = roomBottom - height; y <= roomBottom; y++)
			{
				Main.tile[roomCenterX - i, y].Get<TileWallWireStateData>().HasTile = true;
				Main.tile[roomCenterX - i, y].TileType = 226;
				Main.tile[roomCenterX + i, y].Get<TileWallWireStateData>().HasTile = true;
				Main.tile[roomCenterX + i, y].TileType = 226;
			}
		}
		NewAlterPosition = new Point(roomCenterX - 1, roomBottom - calculateHeightFromOutwardness(0) - 2);
		for (int j = 6; j < roomBounds.Width - 6; j += 10)
		{
			WorldGen.PlaceTile(((Rectangle)(ref roomBounds)).Left + j, roomTop + 1, 42, mute: false, forced: false, -1, 20);
		}
		int totalTables = roomBounds.Width / 8;
		int tries = 0;
		for (int k = 0; k < totalTables; k++)
		{
			tries++;
			if (tries < 1600)
			{
				int x = WorldGen.genRand.Next(5, roomBounds.Width - 5) + ((Rectangle)(ref roomBounds)).Left;
				WorldGen.PlaceTile(x, roomBottom - 1, 14, mute: false, forced: false, -1, 9);
				if (Main.tile[x, roomBottom - 1].TileType != 14)
				{
					k--;
				}
				else
				{
					WorldGen.PlaceTile(x, roomBottom - 3, 33, mute: false, forced: false, -1, 11);
				}
				continue;
			}
			break;
		}
		int calculateHeightFromOutwardness(int outwardness)
		{
			int height2 = Math.Max(outwardness, 4);
			return shrinePedestalWidth / 2 - height2;
		}
	}

	public static void NewJungleTemplePart2()
	{
		int templeLeft = GenVars.tLeft;
		int templeRight = GenVars.tRight;
		int templeTop = GenVars.tTop;
		int templeBottom = GenVars.tBottom;
		int totalRooms = GenVars.tRooms;
		float totalTrapsToPlace = (float)totalRooms * 2.1f;
		float totalStatuesToPlace = (float)totalRooms * 1.6f;
		float totalChestsToPlace = (float)totalRooms * 0.4f;
		int placementAttempts = 0;
		while (totalTrapsToPlace > 0f)
		{
			int randomPointInRoomX = WorldGen.genRand.Next(templeLeft, templeRight);
			int randomPointInRoomY = WorldGen.genRand.Next(templeTop, templeBottom);
			if (Main.tile[randomPointInRoomX, randomPointInRoomY].WallType == 87 && !Main.tile[randomPointInRoomX, randomPointInRoomY].HasTile)
			{
				if (WorldGen.mayanTrap(randomPointInRoomX, randomPointInRoomY))
				{
					totalTrapsToPlace--;
					placementAttempts = 0;
				}
				else
				{
					placementAttempts++;
				}
			}
			else
			{
				placementAttempts++;
			}
			if (placementAttempts > 100)
			{
				placementAttempts = 0;
				totalTrapsToPlace--;
			}
		}
		Main.tileSolid[232] = false;
		placementAttempts = 0;
		while (totalChestsToPlace > 0f)
		{
			int randomPointInTempleX = WorldGen.genRand.Next(templeLeft, templeRight);
			int randomPointInTempleY = WorldGen.genRand.Next(templeTop, templeBottom);
			if (Main.tile[randomPointInTempleX, randomPointInTempleY].WallType == 87 && !Main.tile[randomPointInTempleX, randomPointInTempleY].HasTile && WorldGen.AddBuriedChest(randomPointInTempleX, randomPointInTempleY, 1293, notNearOtherChests: true, 16, trySlope: false, 0))
			{
				totalChestsToPlace--;
				placementAttempts = 0;
			}
			placementAttempts++;
			if (placementAttempts > 10000)
			{
				break;
			}
		}
		placementAttempts = 0;
		while (totalStatuesToPlace > 0f)
		{
			placementAttempts++;
			int randomPointInTempleX2 = WorldGen.genRand.Next(templeLeft, templeRight);
			int randomPointInTempleY2 = WorldGen.genRand.Next(templeTop, templeBottom);
			if (Main.tile[randomPointInTempleX2, randomPointInTempleY2].WallType != 87 || Main.tile[randomPointInTempleX2, randomPointInTempleY2].HasTile)
			{
				continue;
			}
			int statuePlacementPositionY = randomPointInTempleY2;
			while (!Main.tile[randomPointInTempleX2, statuePlacementPositionY].HasTile)
			{
				statuePlacementPositionY++;
				if (statuePlacementPositionY > templeBottom)
				{
					break;
				}
			}
			statuePlacementPositionY--;
			if (statuePlacementPositionY <= templeBottom)
			{
				WorldGen.PlaceTile(randomPointInTempleX2, statuePlacementPositionY, 105, mute: true, forced: false, -1, WorldGen.genRand.Next(43, 46));
				if (Main.tile[randomPointInTempleX2, statuePlacementPositionY].TileType == 105)
				{
					totalStatuesToPlace--;
				}
			}
		}
		float totalFurnitureElementsToPlace = (float)totalRooms * 1.1f;
		placementAttempts = 0;
		while (totalFurnitureElementsToPlace > 0f)
		{
			placementAttempts++;
			int randomPointInTempleX3 = WorldGen.genRand.Next(templeLeft, templeRight);
			int randomPointInTempleY3 = WorldGen.genRand.Next(templeTop, templeBottom);
			if (Main.tile[randomPointInTempleX3, randomPointInTempleY3].WallType == 87 && !Main.tile[randomPointInTempleX3, randomPointInTempleY3].HasTile)
			{
				int furniturePlacementPositionY = randomPointInTempleY3;
				while (!Main.tile[randomPointInTempleX3, furniturePlacementPositionY].HasTile)
				{
					furniturePlacementPositionY++;
					if (furniturePlacementPositionY > templeBottom)
					{
						break;
					}
				}
				furniturePlacementPositionY--;
				if (furniturePlacementPositionY <= templeBottom)
				{
					switch (WorldGen.genRand.Next(3))
					{
					case 0:
						WorldGen.PlaceTile(randomPointInTempleX3, furniturePlacementPositionY, 18, mute: true, forced: false, -1, 10);
						if (Main.tile[randomPointInTempleX3, furniturePlacementPositionY].TileType == 18)
						{
							totalFurnitureElementsToPlace--;
						}
						break;
					case 1:
						WorldGen.PlaceTile(randomPointInTempleX3, furniturePlacementPositionY, 14, mute: true, forced: false, -1, 9);
						if (Main.tile[randomPointInTempleX3, furniturePlacementPositionY].TileType == 14)
						{
							totalFurnitureElementsToPlace--;
						}
						break;
					case 2:
						WorldGen.PlaceTile(randomPointInTempleX3, furniturePlacementPositionY, 15, mute: true, forced: false, -1, 12);
						if (Main.tile[randomPointInTempleX3, furniturePlacementPositionY].TileType == 15)
						{
							totalFurnitureElementsToPlace--;
						}
						break;
					}
				}
			}
			if (placementAttempts > 10000)
			{
				break;
			}
		}
		Main.tileSolid[232] = true;
	}

	public static void NewJungleTempleLihzahrdAltar()
	{
		for (int i = 0; i <= 2; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				int framedAlterPositionX = NewAlterPosition.X + i;
				int framedAlterPositionY = NewAlterPosition.Y + j;
				Main.tile[framedAlterPositionX, framedAlterPositionY].Get<TileWallWireStateData>().HasTile = true;
				Main.tile[framedAlterPositionX, framedAlterPositionY].TileType = 237;
				Main.tile[framedAlterPositionX, framedAlterPositionY].TileFrameX = (short)(i * 18);
				Main.tile[framedAlterPositionX, framedAlterPositionY].TileFrameY = (short)(j * 18);
			}
			Main.tile[i, NewAlterPosition.Y + 2].Get<TileWallWireStateData>().HasTile = true;
			Main.tile[i, NewAlterPosition.Y + 2].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
			Main.tile[i, NewAlterPosition.Y + 2].Get<TileWallWireStateData>().IsHalfBlock = false;
			Main.tile[i, NewAlterPosition.Y + 2].TileType = 226;
		}
	}

	static CustomTemple()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		NewAlterPosition = Point.Zero;
		FinalRoomSize = new Vector2(105f, 95f);
	}
}
