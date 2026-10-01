using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ObjectData;

namespace CalamityMod.Tiles;

internal static class FurnitureCommon
{
	public static bool BedRightClick(int i, int j)
	{
		Player player = Main.LocalPlayer;
		Tile tile = Main.tile[i, j];
		int spawnX = i - tile.TileFrameX / 18 + ((tile.TileFrameX >= 72) ? 5 : 2);
		int spawnY = j + 2;
		if (tile.TileFrameY % 38 != 0)
		{
			spawnY--;
		}
		if (!Player.IsHoveringOverABottomSideOfABed(i, j))
		{
			if (player.IsWithinSnappngRangeToTile(i, j, 96))
			{
				player.GamepadEnableGrappleCooldown();
				player.sleeping.StartSleeping(player, i, j);
			}
		}
		else
		{
			player.FindSpawn();
			if (player.SpawnX == spawnX && player.SpawnY == spawnY)
			{
				player.RemoveSpawn();
				Main.NewText(Language.GetTextValue("Game.SpawnPointRemoved"), byte.MaxValue, 240, 20);
			}
			else if (Player.CheckSpawn(spawnX, spawnY))
			{
				player.ChangeSpawn(spawnX, spawnY);
				Main.NewText(Language.GetTextValue("Game.SpawnPointSet"), byte.MaxValue, 240, 20);
			}
		}
		return true;
	}

	public static void BenchMouseOver(int i, int j, int itemID)
	{
		Player player = Main.LocalPlayer;
		if (player.IsWithinSnappngRangeToTile(i, j, 40))
		{
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = itemID;
		}
	}

	public static void BenchSitInfo(int i, int j, ref TileRestingInfo info, int nextStyleHeight = 40)
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Framing.GetTileSafely(i, j);
		Player player = Main.LocalPlayer;
		info.DirectionOffset = 0;
		float offset = 0f;
		if (tile.TileFrameX < 17 && player.direction == 1)
		{
			offset = 8f;
		}
		if (tile.TileFrameX < 17 && player.direction == -1)
		{
			offset = -8f;
		}
		if (tile.TileFrameX > 34 && player.direction == 1)
		{
			offset = -8f;
		}
		if (tile.TileFrameX > 34 && player.direction == -1)
		{
			offset = 8f;
		}
		info.VisualOffset = new Vector2(offset, 0f);
		info.TargetDirection = player.direction;
		info.AnchorTilePosition.X = i;
		info.AnchorTilePosition.Y = j;
		if (tile.TileFrameY % nextStyleHeight == 0)
		{
			info.AnchorTilePosition.Y++;
		}
	}

	public static void ChairMouseOver(int i, int j, int itemID, bool fat = false)
	{
		Player player = Main.LocalPlayer;
		if (player.IsWithinSnappngRangeToTile(i, j, 40))
		{
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = itemID;
			if (fat ? (Main.tile[i, j].TileFrameX <= 35) : (Main.tile[i, j].TileFrameX / 18 < 0))
			{
				player.cursorItemIconReversed = true;
			}
		}
	}

	public static bool ChairRightClick(int i, int j)
	{
		Player player = Main.LocalPlayer;
		if (player.IsWithinSnappngRangeToTile(i, j, 40))
		{
			player.GamepadEnableGrappleCooldown();
			player.sitting.SitDown(player, i, j);
		}
		return true;
	}

	public static void ChairSitInfo(int i, int j, ref TileRestingInfo info, int nextStyleHeight = 40, bool fat = false, bool hasOffset = false, bool shitter = false)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if (hasOffset)
		{
			info.DirectionOffset = 0;
			info.VisualOffset = new Vector2(-8f, 0f);
		}
		Tile tile = Framing.GetTileSafely(i, j);
		bool num = (fat ? (tile.TileFrameX >= 35) : (tile.TileFrameX != 0));
		if (shitter)
		{
			info.ExtraInfo.IsAToilet = true;
		}
		info.TargetDirection = -1;
		if (num)
		{
			info.TargetDirection = 1;
		}
		if (fat)
		{
			int num2 = tile.TileFrameX / 18;
			if (num2 == 1)
			{
				i--;
			}
			if (num2 == 2)
			{
				i++;
			}
		}
		info.AnchorTilePosition.X = i;
		info.AnchorTilePosition.Y = j;
		if (tile.TileFrameY % nextStyleHeight == 0)
		{
			info.AnchorTilePosition.Y++;
		}
	}

	public static void ChestMouseFar<T>(int i, int j) where T : ModItem
	{
		ChestMouseOver<T>(i, j);
		Player player = Main.LocalPlayer;
		if (player.cursorItemIconText == "")
		{
			player.cursorItemIconEnabled = false;
			player.cursorItemIconID = 0;
		}
	}

	public static void ChestMouseOver<T>(int i, int j) where T : ModItem
	{
		Player player = Main.LocalPlayer;
		Tile tile = Main.tile[i, j];
		string chestName = TileLoader.DefaultContainerName(tile.TileType, tile.TileFrameX, tile.TileFrameY);
		int left = i;
		int top = j;
		if (tile.TileFrameX % 36 != 0)
		{
			left--;
		}
		if (tile.TileFrameY != 0)
		{
			top--;
		}
		int chest = Chest.FindChest(left, top);
		player.cursorItemIconID = -1;
		if (chest < 0)
		{
			player.cursorItemIconText = Language.GetTextValue("LegacyChestType.0");
		}
		else
		{
			player.cursorItemIconText = ((Main.chest[chest].name.Length > 0) ? Main.chest[chest].name : chestName);
			if (player.cursorItemIconText == chestName)
			{
				player.cursorItemIconID = ModContent.ItemType<T>();
				player.cursorItemIconText = "";
			}
		}
		player.noThrow = 2;
		player.cursorItemIconEnabled = true;
	}

	public static bool ChestRightClick(int i, int j)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.LocalPlayer;
		Tile tile = Main.tile[i, j];
		Main.mouseRightRelease = false;
		int left = i;
		int top = j;
		if (tile.TileFrameX % 36 != 0)
		{
			left--;
		}
		if (tile.TileFrameY != 0)
		{
			top--;
		}
		if (player.sign >= 0)
		{
			SoundEngine.PlaySound(in SoundID.MenuClose);
			player.sign = -1;
			Main.editSign = false;
			Main.npcChatText = "";
		}
		if (Main.editChest)
		{
			SoundEngine.PlaySound(in SoundID.MenuTick);
			Main.editChest = false;
			Main.npcChatText = "";
		}
		if (player.editedChestName)
		{
			NetMessage.SendData(33, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f);
			player.editedChestName = false;
		}
		if (Main.netMode == 1)
		{
			if (left == player.chestX && top == player.chestY && player.chest >= 0)
			{
				player.chest = -1;
				Recipe.FindRecipes();
				SoundEngine.PlaySound(in SoundID.MenuClose);
			}
			else
			{
				NetMessage.SendData(31, -1, -1, null, left, top);
				Main.stackSplit = 600;
			}
		}
		else
		{
			int chest = Chest.FindChest(left, top);
			if (chest >= 0)
			{
				Main.stackSplit = 600;
				if (chest == player.chest)
				{
					player.chest = -1;
					SoundEngine.PlaySound(in SoundID.MenuClose);
				}
				else
				{
					player.chest = chest;
					Main.playerInventory = true;
					Main.recBigList = false;
					player.chestX = left;
					player.chestY = top;
					SoundEngine.PlaySound((player.chest < 0) ? SoundID.MenuOpen : SoundID.MenuTick);
				}
				Recipe.FindRecipes();
			}
		}
		return true;
	}

	public static bool ClockRightClick()
	{
		string text = "AM";
		double time = Main.time;
		if (!Main.dayTime)
		{
			time += 54000.0;
		}
		time /= 3600.0;
		time -= 19.5;
		if (time < 0.0)
		{
			time += 24.0;
		}
		if (time >= 12.0)
		{
			text = "PM";
		}
		int intTime = (int)time;
		double deltaTime = time - (double)intTime;
		deltaTime = (int)(deltaTime * 60.0);
		string minuteText = deltaTime.ToString();
		if (deltaTime < 10.0)
		{
			minuteText = "0" + minuteText;
		}
		if (intTime > 12)
		{
			intTime -= 12;
		}
		if (intTime == 0)
		{
			intTime = 12;
		}
		Main.NewText("Time: " + intTime + ":" + minuteText + " " + text, byte.MaxValue, 240, 20);
		return true;
	}

	public static void DresserMouseFar<T>() where T : ModItem
	{
		Player player = Main.LocalPlayer;
		Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
		string chestName = TileLoader.DefaultContainerName(tile.TileType, tile.TileFrameX, tile.TileFrameY);
		int tileTargetX = Player.tileTargetX;
		int top = Player.tileTargetY;
		int x = tileTargetX - tile.TileFrameX % 54 / 18;
		if (tile.TileFrameY % 36 != 0)
		{
			top--;
		}
		int chestIndex = Chest.FindChest(x, top);
		player.cursorItemIconID = -1;
		if (chestIndex < 0)
		{
			player.cursorItemIconText = Language.GetTextValue("LegacyDresserType.0");
		}
		else
		{
			if (Main.chest[chestIndex].name != "")
			{
				player.cursorItemIconText = Main.chest[chestIndex].name;
			}
			else
			{
				player.cursorItemIconText = chestName;
			}
			if (player.cursorItemIconText == chestName)
			{
				player.cursorItemIconID = ModContent.ItemType<T>();
				player.cursorItemIconText = "";
			}
		}
		player.noThrow = 2;
		player.cursorItemIconEnabled = true;
		if (player.cursorItemIconText == "")
		{
			player.cursorItemIconEnabled = false;
			player.cursorItemIconID = 0;
		}
	}

	public static void DresserMouseOver<T>() where T : ModItem
	{
		Player player = Main.LocalPlayer;
		Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
		string chestName = TileLoader.DefaultContainerName(tile.TileType, tile.TileFrameX, tile.TileFrameY);
		int tileTargetX = Player.tileTargetX;
		int top = Player.tileTargetY;
		int x = tileTargetX - tile.TileFrameX % 54 / 18;
		if (tile.TileFrameY % 36 != 0)
		{
			top--;
		}
		int chestIndex = Chest.FindChest(x, top);
		player.cursorItemIconID = -1;
		if (chestIndex < 0)
		{
			player.cursorItemIconText = Language.GetTextValue("LegacyDresserType.0");
		}
		else
		{
			if (Main.chest[chestIndex].name != "")
			{
				player.cursorItemIconText = Main.chest[chestIndex].name;
			}
			else
			{
				player.cursorItemIconText = chestName;
			}
			if (player.cursorItemIconText == chestName)
			{
				player.cursorItemIconID = ModContent.ItemType<T>();
				player.cursorItemIconText = "";
			}
		}
		player.noThrow = 2;
		player.cursorItemIconEnabled = true;
		if (Main.tile[Player.tileTargetX, Player.tileTargetY].TileFrameY > 0)
		{
			player.cursorItemIconID = 269;
		}
	}

	public static bool DresserRightClick()
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.LocalPlayer;
		if (Main.tile[Player.tileTargetX, Player.tileTargetY].TileFrameY == 0)
		{
			Main.CancelClothesWindow(quiet: true);
			int left = Main.tile[Player.tileTargetX, Player.tileTargetY].TileFrameX / 18;
			left %= 3;
			left = Player.tileTargetX - left;
			int top = Player.tileTargetY - Main.tile[Player.tileTargetX, Player.tileTargetY].TileFrameY / 18;
			if (player.sign > -1)
			{
				SoundEngine.PlaySound(in SoundID.MenuClose);
				player.sign = -1;
				Main.editSign = false;
				Main.npcChatText = string.Empty;
			}
			if (Main.editChest)
			{
				SoundEngine.PlaySound(in SoundID.MenuTick);
				Main.editChest = false;
				Main.npcChatText = string.Empty;
			}
			if (player.editedChestName)
			{
				NetMessage.SendData(33, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f);
				player.editedChestName = false;
			}
			if (Main.netMode == 1)
			{
				if (left == player.chestX && top == player.chestY && player.chest != -1)
				{
					player.chest = -1;
					Recipe.FindRecipes();
					SoundEngine.PlaySound(in SoundID.MenuClose);
				}
				else
				{
					NetMessage.SendData(31, -1, -1, null, left, top);
					Main.stackSplit = 600;
				}
				return true;
			}
			player.piggyBankProjTracker.Clear();
			player.voidLensChest.Clear();
			int dresserChestID = Chest.FindChest(left, top);
			if (dresserChestID != -1)
			{
				Main.stackSplit = 600;
				if (dresserChestID == player.chest)
				{
					player.chest = -1;
					Recipe.FindRecipes();
					SoundEngine.PlaySound(in SoundID.MenuClose);
				}
				else if (dresserChestID != player.chest && player.chest == -1)
				{
					player.chest = dresserChestID;
					Main.playerInventory = true;
					Main.recBigList = false;
					SoundEngine.PlaySound(in SoundID.MenuOpen);
					player.chestX = left;
					player.chestY = top;
				}
				else
				{
					player.chest = dresserChestID;
					Main.playerInventory = true;
					Main.recBigList = false;
					SoundEngine.PlaySound(in SoundID.MenuTick);
					player.chestX = left;
					player.chestY = top;
				}
				Recipe.FindRecipes();
				return true;
			}
			return false;
		}
		Main.playerInventory = false;
		player.chest = -1;
		Recipe.FindRecipes();
		Main.interactedDresserTopLeftX = Player.tileTargetX;
		Main.interactedDresserTopLeftY = Player.tileTargetY;
		Main.OpenClothesWindow();
		return true;
	}

	public static string GetMapChestName(string baseName, int x, int y)
	{
		if (!WorldGen.InWorld(x, y, 2))
		{
			return baseName;
		}
		Tile tile = Main.tile[x, y];
		int left = x;
		int top = y;
		if (tile.TileFrameX % 36 != 0)
		{
			left--;
		}
		if (tile.TileFrameY != 0)
		{
			top--;
		}
		int chest = Chest.FindChest(left, top);
		if (chest < 0)
		{
			return baseName;
		}
		string name = baseName;
		if (!string.IsNullOrEmpty(Main.chest[chest].name))
		{
			name = name + ": " + Main.chest[chest].name;
		}
		return name;
	}

	public static void LightHitWire(int type, int i, int j, int tileX, int tileY)
	{
		Tile tile = Main.tile[i, j];
		int x = i - tile.TileFrameX / 18 % tileX;
		tile = Main.tile[i, j];
		int y = j - tile.TileFrameY / 18 % tileY;
		int tileXX18 = 18 * tileX;
		for (int l = x; l < x + tileX; l++)
		{
			for (int m = y; m < y + tileY; m++)
			{
				tile = Main.tile[l, m];
				if (!tile.HasTile)
				{
					continue;
				}
				tile = Main.tile[l, m];
				if (tile.TileType == type)
				{
					tile = Main.tile[l, m];
					if (tile.TileFrameX < tileXX18)
					{
						tile = Main.tile[l, m];
						tile.TileFrameX += (short)tileXX18;
					}
					else
					{
						tile = Main.tile[l, m];
						tile.TileFrameX -= (short)tileXX18;
					}
				}
			}
		}
		if (Wiring.running)
		{
			for (int k = 0; k < tileX; k++)
			{
				for (int n = 0; n < tileY; n++)
				{
					Wiring.SkipWire(x + k, y + n);
				}
			}
		}
		if (Main.netMode != 0)
		{
			NetMessage.SendTileSquare(-1, x, y, tileX, tileY);
		}
	}

	public static void LockedChestMouseOver<K, C>(int i, int j) where K : ModItem where C : ModItem
	{
		Player player = Main.LocalPlayer;
		Tile tile = Main.tile[i, j];
		string chestName = TileLoader.DefaultContainerName(tile.TileType, tile.TileFrameX, tile.TileFrameY);
		int left = i;
		int top = j;
		if (tile.TileFrameX % 36 != 0)
		{
			left--;
		}
		if (tile.TileFrameY != 0)
		{
			top--;
		}
		int chest = Chest.FindChest(left, top);
		player.cursorItemIconID = -1;
		if (chest < 0)
		{
			player.cursorItemIconText = Language.GetTextValue("LegacyChestType.0");
		}
		else
		{
			player.cursorItemIconText = ((Main.chest[chest].name.Length > 0) ? Main.chest[chest].name : chestName);
			if (player.cursorItemIconText == chestName)
			{
				player.cursorItemIconID = ModContent.ItemType<C>();
				if (Main.tile[left, top].TileFrameX / 36 == 1)
				{
					player.cursorItemIconID = ModContent.ItemType<K>();
				}
				player.cursorItemIconText = "";
			}
		}
		player.noThrow = 2;
		player.cursorItemIconEnabled = true;
	}

	public static void LockedChestMouseOverFar<K, C>(int i, int j) where K : ModItem where C : ModItem
	{
		LockedChestMouseOver<K, C>(i, j);
		Player player = Main.LocalPlayer;
		if (player.cursorItemIconText == "")
		{
			player.cursorItemIconEnabled = false;
			player.cursorItemIconID = 0;
		}
	}

	public static bool LockedChestRightClick(bool isLocked, int left, int top, int i, int j)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.LocalPlayer;
		if (player.sign >= 0)
		{
			SoundEngine.PlaySound(in SoundID.MenuClose);
			player.sign = -1;
			Main.editSign = false;
			Main.npcChatText = "";
		}
		if (Main.editChest)
		{
			SoundEngine.PlaySound(in SoundID.MenuTick);
			Main.editChest = false;
			Main.npcChatText = "";
		}
		if (player.editedChestName)
		{
			NetMessage.SendData(33, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f);
			player.editedChestName = false;
		}
		if (Main.netMode == 1 && !isLocked)
		{
			if (left == player.chestX && top == player.chestY && player.chest >= 0)
			{
				player.chest = -1;
				Recipe.FindRecipes();
				SoundEngine.PlaySound(in SoundID.MenuClose);
			}
			else
			{
				NetMessage.SendData(31, -1, -1, null, left, top);
				Main.stackSplit = 600;
			}
			return true;
		}
		if (isLocked)
		{
			if (Chest.Unlock(left, top))
			{
				if (Main.netMode == 1)
				{
					NetMessage.SendData(52, -1, -1, null, player.whoAmI, 1f, left, top);
				}
				return true;
			}
		}
		else
		{
			int chest = Chest.FindChest(left, top);
			if (chest >= 0)
			{
				Main.stackSplit = 600;
				if (chest == player.chest)
				{
					player.chest = -1;
					SoundEngine.PlaySound(in SoundID.MenuClose);
				}
				else
				{
					player.chest = chest;
					Main.playerInventory = true;
					Main.recBigList = false;
					player.chestX = left;
					player.chestY = top;
					SoundEngine.PlaySound((player.chest < 0) ? SoundID.MenuOpen : SoundID.MenuTick);
				}
				Recipe.FindRecipes();
				return true;
			}
		}
		return false;
	}

	public static void MouseOver(int i, int j, int itemID)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = itemID;
	}

	public static void RightClickBreak(int i, int j)
	{
		if (Main.tile[i, j] != null && Main.tile[i, j].HasTile)
		{
			WorldGen.KillTile(i, j);
			if (!Main.tile[i, j].HasTile && Main.netMode != 0)
			{
				NetMessage.SendData(17, -1, -1, null, 0, i, j);
			}
		}
	}

	internal static void SetUp6x6Painting(this ModTile mt, bool lavaImmune = false)
	{
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileSpelunker[mt.Type] = true;
		Main.tileWaterDeath[mt.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
		TileObjectData.newTile.Width = 6;
		TileObjectData.newTile.Height = 6;
		TileObjectData.newTile.Origin = new Point16(2, 2);
		TileObjectData.newTile.CoordinateHeights = new int[6] { 16, 16, 16, 16, 16, 16 };
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.addTile(mt.Type);
	}

	internal static void SetUpBar(this ModTile mt, int itemDropID, Color mapColor, bool lavaImmune = true)
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileShine[mt.Type] = 1100;
		Main.tileSolid[mt.Type] = true;
		Main.tileSolidTop[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.addTile(mt.Type);
		mt.AddMapEntry(mapColor, Language.GetText("MapObject.MetalBar"));
	}

	internal static void SetUpBathtub(this ModTile mt, int itemDropID, bool lavaImmune = false)
	{
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileLighted[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileObjectData.newTile.Width = 4;
		TileObjectData.newTile.Height = 2;
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.Origin = new Point16(1, 1);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 4, 0);
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
		TileObjectData.addAlternate(1);
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
		mt.AddMapEntry(new Color(144, 148, 144), Language.GetText("ItemName.Bathtub"));
	}

	internal static void SetUpBed(this ModTile mt, int itemDropID, bool lavaImmune = false)
	{
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileID.Sets.HasOutlines[mt.Type] = true;
		TileID.Sets.CanBeSleptIn[mt.Type] = true;
		TileID.Sets.InteractibleByNPCs[mt.Type] = true;
		TileID.Sets.IsValidSpawnPoint[mt.Type] = true;
		TileID.Sets.DisableSmartCursor[mt.Type] = true;
		TileObjectData.newTile.Width = 4;
		TileObjectData.newTile.Height = 2;
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.Origin = new Point16(1, 1);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 4, 0);
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
		TileObjectData.addAlternate(1);
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
		mt.AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.Bed"));
		mt.AdjTiles = new int[1] { 79 };
	}

	internal static void SetUpBookcase(this ModTile mt, int itemDropID, bool lavaImmune = false, bool solidTop = true, bool autoBookcase = true)
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileSolidTop[mt.Type] = solidTop;
		Main.tileLighted[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileTable[mt.Type] = solidTop;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.addTile(mt.Type);
		if (autoBookcase)
		{
			mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			mt.AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.Bookcase"));
			mt.AdjTiles = new int[1] { 101 };
		}
	}

	internal static void SetUpCandelabra(this ModTile mt, int itemDropID, bool lavaImmune = false)
	{
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileLighted[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileID.Sets.DisableSmartCursor[mt.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newTile.StyleLineSkip = 2;
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		mt.AddMapEntry(new Color(253, 221, 3), Language.GetText("ItemName.Candelabra"));
		mt.AdjTiles = new int[1] { 100 };
	}

	internal static void SetUpCandle(this ModTile mt, int itemDropID, bool lavaImmune = false, bool autoMapEntry = true, int offset = -4)
	{
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileLighted[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileID.Sets.DisableSmartCursor[mt.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
		TileObjectData.newTile.CoordinateHeights = new int[1] { 20 };
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newTile.DrawYOffset = offset;
		TileObjectData.newTile.StyleLineSkip = 2;
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		if (autoMapEntry)
		{
			mt.AddMapEntry(new Color(253, 221, 3), Language.GetText("ItemName.Candle"));
		}
		mt.AdjTiles = new int[1] { 33 };
	}

	internal static void SetUpChair(this ModTile mt, int itemDropID, bool lavaImmune = false)
	{
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileNoAttach[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileID.Sets.CanBeSatOnForNPCs[mt.Type] = true;
		TileID.Sets.CanBeSatOnForPlayers[mt.Type] = true;
		TileID.Sets.HasOutlines[mt.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
		TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
		TileObjectData.newTile.StyleWrapLimit = 2;
		TileObjectData.newTile.StyleMultiplier = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
		TileObjectData.addAlternate(1);
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
		mt.AddMapEntry(new Color(191, 142, 111), Language.GetText("MapObject.Chair"));
		mt.AdjTiles = new int[1] { 15 };
	}

	internal static void SetUpChandelier(this ModTile mt, int itemDropID, bool lavaImmune = false)
	{
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileLighted[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileNoAttach[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileID.Sets.MultiTileSway[mt.Type] = true;
		TileID.Sets.IsAMechanism[mt.Type] = true;
		TileObjectData.newTile.Width = 3;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.Origin = new Point16(1, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newTile.StyleLineSkip = 2;
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		mt.AddMapEntry(new Color(235, 166, 135), Language.GetText("MapObject.Chandelier"));
		mt.AdjTiles = new int[1] { 34 };
	}

	internal static void SetUpChest(this ModTile mt, int itemDropID, bool offset = false, int offsetAmt = 4)
	{
		mt.RegisterItemDrop(itemDropID);
		Main.tileSpelunker[mt.Type] = true;
		Main.tileContainer[mt.Type] = true;
		Main.tileShine2[mt.Type] = true;
		Main.tileShine[mt.Type] = 1200;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileNoAttach[mt.Type] = true;
		Main.tileOreFinderPriority[mt.Type] = 500;
		TileID.Sets.BasicChest[mt.Type] = true;
		TileID.Sets.HasOutlines[mt.Type] = true;
		TileID.Sets.DisableSmartCursor[mt.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		if (offset)
		{
			TileObjectData.newTile.DrawYOffset = offsetAmt;
		}
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
		TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(Chest.FindEmptyChest, -1, 0, processedCoordinates: true);
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(Chest.AfterPlacement_Hook, -1, 0, processedCoordinates: false);
		TileObjectData.newTile.AnchorInvalidTiles = new int[1] { 127 };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.LavaPlacement = LiquidPlacement.Allowed;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.addTile(mt.Type);
		mt.AdjTiles = new int[1] { 21 };
	}

	internal static void SetUpClock(this ModTile mt, int itemDropID, bool lavaImmune = false)
	{
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileNoAttach[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		TileID.Sets.HasOutlines[mt.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
		TileObjectData.newTile.Height = 5;
		TileObjectData.newTile.CoordinateHeights = new int[5] { 16, 16, 16, 16, 16 };
		TileObjectData.newTile.Origin = new Point16(0, 4);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.addTile(mt.Type);
		mt.AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.GrandfatherClock"));
		mt.AdjTiles = new int[1] { 104 };
	}

	internal static void SetUpDoorClosed(this ModTile mt, int itemDropID, bool lavaImmune = false)
	{
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileBlockLight[mt.Type] = true;
		Main.tileSolid[mt.Type] = true;
		Main.tileNoAttach[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileID.Sets.NotReallySolid[mt.Type] = true;
		TileID.Sets.DrawsWalls[mt.Type] = true;
		TileID.Sets.HasOutlines[mt.Type] = true;
		TileID.Sets.DisableSmartCursor[mt.Type] = true;
		TileObjectData.newTile.Width = 1;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.Origin = new Point16(0, 0);
		TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Origin = new Point16(0, 1);
		TileObjectData.addAlternate(0);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Origin = new Point16(0, 2);
		TileObjectData.addAlternate(0);
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
		mt.AddMapEntry(new Color(119, 105, 79), Language.GetText("MapObject.Door"));
		mt.AdjTiles = new int[1] { 10 };
	}

	internal static void SetUpDoorOpen(this ModTile mt, int itemDropID, bool lavaImmune = false)
	{
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileSolid[mt.Type] = false;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		Main.tileNoSunLight[mt.Type] = true;
		TileObjectData.newTile.Width = 2;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.Origin = new Point16(0, 0);
		TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 0);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleMultiplier = 2;
		TileObjectData.newTile.StyleWrapLimit = 2;
		TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Origin = new Point16(0, 1);
		TileObjectData.addAlternate(0);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Origin = new Point16(0, 2);
		TileObjectData.addAlternate(0);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Origin = new Point16(1, 0);
		TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
		TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
		TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
		TileObjectData.addAlternate(1);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Origin = new Point16(1, 1);
		TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
		TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
		TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
		TileObjectData.addAlternate(1);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Origin = new Point16(1, 2);
		TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile, 1, 1);
		TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile, 1, 1);
		TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
		TileObjectData.addAlternate(1);
		TileObjectData.addTile(mt.Type);
		TileID.Sets.HousingWalls[mt.Type] = true;
		TileID.Sets.HasOutlines[mt.Type] = true;
		TileID.Sets.DisableSmartCursor[mt.Type] = true;
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
		mt.AddMapEntry(new Color(119, 105, 79), Language.GetText("MapObject.Door"));
		mt.AdjTiles = new int[1] { 11 };
	}

	internal static void SetUpDresser(this ModTile mt, int itemDropID)
	{
		mt.RegisterItemDrop(itemDropID);
		Main.tileSolidTop[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileNoAttach[mt.Type] = true;
		Main.tileTable[mt.Type] = true;
		Main.tileContainer[mt.Type] = true;
		Main.tileWaterDeath[mt.Type] = false;
		Main.tileLavaDeath[mt.Type] = false;
		TileID.Sets.BasicDresser[mt.Type] = true;
		TileID.Sets.HasOutlines[mt.Type] = true;
		TileID.Sets.DisableSmartCursor[mt.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.Origin = new Point16(1, 1);
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
		TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(Chest.FindEmptyChest, -1, 0, processedCoordinates: true);
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(Chest.AfterPlacement_Hook, -1, 0, processedCoordinates: false);
		TileObjectData.newTile.AnchorInvalidTiles = new int[1] { 127 };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
		mt.AdjTiles = new int[1] { 88 };
	}

	internal static void SetUpFountain(this ModTile mt, int itemDropID, Color mapColor, bool lava = false)
	{
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileLighted[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = false;
		Main.tileWaterDeath[mt.Type] = false;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.LavaPlacement = LiquidPlacement.Allowed;
		TileObjectData.addTile(mt.Type);
		TileID.Sets.HasOutlines[mt.Type] = true;
		TileObjectData.newTile.Width = 2;
		TileObjectData.newTile.Height = 4;
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.Origin = new Point16(0, 3);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, 2, 0);
		TileObjectData.newTile.StyleLineSkip = 2;
		TileObjectData.addTile(mt.Type);
		mt.AddMapEntry(mapColor, lava ? CalamityUtils.GetText("Tiles.LavaFountain") : Language.GetText("MapObject.WaterFountain"));
		mt.AnimationFrameHeight = 72;
	}

	internal static void SetUpLamp(this ModTile mt, int itemDropID, bool lavaImmune = false)
	{
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileLighted[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileID.Sets.DisableSmartCursor[mt.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newTile.StyleLineSkip = 2;
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		mt.AddMapEntry(new Color(253, 221, 3), Language.GetText("MapObject.FloorLamp"));
		mt.AdjTiles = new int[1] { 93 };
	}

	internal static void SetUpLantern(this ModTile mt, int itemDropID, bool lavaImmune = false, bool autoMapEntry = true)
	{
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileLighted[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileID.Sets.DisableSmartCursor[mt.Type] = true;
		TileID.Sets.MultiTileSway[mt.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newTile.StyleLineSkip = 2;
		TileObjectData.newTile.DrawYOffset = -2;
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.Platform, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.DrawYOffset = -10;
		TileObjectData.addAlternate(0);
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		if (autoMapEntry)
		{
			mt.AddMapEntry(new Color(251, 235, 127), Language.GetText("MapObject.Lantern"));
		}
		mt.AdjTiles = new int[1] { 42 };
	}

	internal static void SetUpPiano(this ModTile mt, int itemDropID, bool lavaImmune = false, bool solidTop = true)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileTable[mt.Type] = solidTop;
		Main.tileSolidTop[mt.Type] = solidTop;
		Main.tileLighted[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
		mt.AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.Piano"));
	}

	internal static void SetUpPlatform(this ModTile mt, int itemDropID, bool lavaImmune = false)
	{
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileLighted[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileSolidTop[mt.Type] = true;
		Main.tileSolid[mt.Type] = true;
		Main.tileNoAttach[mt.Type] = true;
		Main.tileTable[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		TileID.Sets.Platforms[mt.Type] = true;
		TileID.Sets.DisableSmartCursor[mt.Type] = true;
		TileObjectData.newTile.CoordinateHeights = new int[1] { 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleMultiplier = 27;
		TileObjectData.newTile.StyleWrapLimit = 27;
		TileObjectData.newTile.UsesCustomCanPlace = false;
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
		mt.AddMapEntry(new Color(191, 142, 111));
		mt.AdjTiles = new int[1] { 19 };
	}

	internal static void SetUpPylon(this ModPylon mp, TEModdedPylon pylonHook, bool lavaImmune = false, int offset = 2)
	{
		Main.tileLighted[mp.Type] = true;
		Main.tileFrameImportant[mp.Type] = true;
		Main.tileLavaDeath[mp.Type] = !lavaImmune;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
		TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(pylonHook.PlacementPreviewHook_CheckIfCanPlace, 1, 0, processedCoordinates: true);
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(pylonHook.Hook_AfterPlacement, -1, 0, processedCoordinates: false);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newTile.DrawYOffset = offset;
		TileObjectData.addTile(mp.Type);
		mp.AddToArray(ref TileID.Sets.CountsAsPylon);
	}

	internal static void SetUpSink(this ModTile mt, int itemDropID, bool lavaImmune = false, bool water = true, bool lava = false, bool honey = false)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileLighted[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileID.Sets.CountsAsWaterSource[mt.Type] = water;
		TileID.Sets.CountsAsLavaSource[mt.Type] = lava;
		TileID.Sets.CountsAsHoneySource[mt.Type] = honey;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.addTile(mt.Type);
		mt.AddMapEntry(new Color(191, 142, 111), Language.GetText("MapObject.Sink"));
		if (water)
		{
			mt.AdjTiles = new int[1] { 172 };
		}
	}

	internal static void SetUpSofa(this ModTile mt, int itemDropID, bool lavaImmune = false, bool bench = false)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileLighted[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileID.Sets.CanBeSatOnForPlayers[mt.Type] = true;
		TileID.Sets.HasOutlines[mt.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);
		mt.AddMapEntry(new Color(191, 142, 111), bench ? Language.GetText("ItemName.Bench") : Language.GetText("ItemName.Sofa"));
	}

	internal static void SetUpTable(this ModTile mt, int itemDropID, bool lavaImmune = false)
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileSolidTop[mt.Type] = true;
		Main.tileLighted[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileNoAttach[mt.Type] = true;
		Main.tileTable[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
		mt.AddMapEntry(new Color(191, 142, 111), Language.GetText("MapObject.Table"));
		mt.AdjTiles = new int[1] { 14 };
	}

	internal static void SetUpTorch(this ModTile mt, int itemDropID, bool waterImmune = false, bool lavaImmune = false)
	{
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileLighted[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileSolid[mt.Type] = false;
		Main.tileNoAttach[mt.Type] = true;
		Main.tileNoFail[mt.Type] = true;
		Main.tileWaterDeath[mt.Type] = !waterImmune;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		TileID.Sets.DisableSmartCursor[mt.Type] = true;
		TileID.Sets.Torch[mt.Type] = true;
		TileID.Sets.FramesOnKillWall[mt.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.StyleTorch);
		TileObjectData.newTile.WaterDeath = !waterImmune;
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.WaterPlacement = ((!waterImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
		TileObjectData.newAlternate.WaterDeath = !waterImmune;
		TileObjectData.newAlternate.LavaDeath = !lavaImmune;
		TileObjectData.newAlternate.WaterPlacement = ((!waterImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newAlternate.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
		TileObjectData.newAlternate.AnchorAlternateTiles = new int[1] { 124 };
		TileObjectData.addAlternate(1);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
		TileObjectData.newAlternate.WaterDeath = !waterImmune;
		TileObjectData.newAlternate.LavaDeath = !lavaImmune;
		TileObjectData.newAlternate.WaterPlacement = ((!waterImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newAlternate.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
		TileObjectData.newAlternate.AnchorAlternateTiles = new int[1] { 124 };
		TileObjectData.addAlternate(2);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
		TileObjectData.newAlternate.WaterDeath = !waterImmune;
		TileObjectData.newAlternate.LavaDeath = !lavaImmune;
		TileObjectData.newAlternate.WaterPlacement = ((!waterImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newAlternate.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.newAlternate.AnchorWall = true;
		TileObjectData.addAlternate(0);
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		mt.AddMapEntry(new Color(253, 221, 3), Language.GetText("ItemName.Torch"));
		mt.AdjTiles = new int[1] { 4 };
	}

	internal static void SetUpTrophy(this ModTile mt)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = true;
		Main.tileSpelunker[mt.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
		TileObjectData.addTile(mt.Type);
		TileID.Sets.DisableSmartCursor[mt.Type] = true;
		TileID.Sets.FramesOnKillWall[mt.Type] = true;
		mt.AddMapEntry(new Color(120, 85, 60), Language.GetText("MapObject.Trophy"));
		mt.DustType = 7;
	}

	internal static void SetUpWorkBench(this ModTile mt, int itemDropID, bool lavaImmune = false)
	{
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		mt.RegisterItemDrop(itemDropID);
		Main.tileSolidTop[mt.Type] = true;
		Main.tileFrameImportant[mt.Type] = true;
		Main.tileNoAttach[mt.Type] = true;
		Main.tileTable[mt.Type] = true;
		Main.tileLavaDeath[mt.Type] = !lavaImmune;
		Main.tileWaterDeath[mt.Type] = false;
		TileID.Sets.DisableSmartCursor[mt.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
		TileObjectData.newTile.CoordinateHeights = new int[1] { 18 };
		TileObjectData.newTile.LavaDeath = !lavaImmune;
		TileObjectData.newTile.LavaPlacement = ((!lavaImmune) ? LiquidPlacement.NotAllowed : LiquidPlacement.Allowed);
		TileObjectData.addTile(mt.Type);
		mt.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
		mt.AddMapEntry(new Color(191, 142, 111), Language.GetText("ItemName.WorkBench"));
		mt.AdjTiles = new int[1] { 18 };
	}
}
