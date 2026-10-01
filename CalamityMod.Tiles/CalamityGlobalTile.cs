using System;
using System.Collections.Generic;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.Tools;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Tiles.Abyss;
using CalamityMod.Tiles.SunkenSea;
using CalamityMod.Tiles.SunkenSea.Ambient;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public class CalamityGlobalTile : GlobalTile
{
	public static List<int> GrowthTiles = new List<int>
	{
		ModContent.TileType<SeaPrism>(),
		ModContent.TileType<AbyssGravel>(),
		ModContent.TileType<PyreMantle>(),
		ModContent.TileType<Navystone>(),
		ModContent.TileType<Shellstone>(),
		ModContent.TileType<EutrophicSand>(),
		ModContent.TileType<PolypSand>(),
		ModContent.TileType<VolcanicSand>(),
		ModContent.TileType<HardenedEutrophicSand>(),
		ModContent.TileType<Dunesand>(),
		ModContent.TileType<Limestone>(),
		ModContent.TileType<LimestoneCobble>(),
		ModContent.TileType<Voidstone>()
	};

	public override void SetStaticDefaults()
	{
		Main.tileSpelunker[408] = true;
		Main.tileOreFinderPriority[408] = 900;
		TileID.Sets.TileCutIgnore.IgnoreDontHurtNature[231] = true;
	}

	public override void PreShakeTree(int x, int y, TreeTypes treeType)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 worldPosition = Utils.ToWorldCoordinates(new Vector2((float)x, (float)y), 8f, 8f);
		Player nearestPlayer = Main.player[Player.FindClosest(worldPosition, 16, 16)];
		if (!nearestPlayer.active || nearestPlayer.HeldItem.type != ModContent.ItemType<FellerofEvergreens>() || !WorldGen.genRand.NextBool(3))
		{
			return;
		}
		int treeDropItemType = 0;
		switch (treeType)
		{
		case TreeTypes.Forest:
			treeDropItemType = WorldGen.genRand.Next(5) switch
			{
				0 => 4009, 
				1 => 4282, 
				2 => 4293, 
				3 => 4290, 
				_ => 4291, 
			};
			break;
		case TreeTypes.Snow:
			treeDropItemType = (WorldGen.genRand.NextBool() ? 4286 : 4295);
			break;
		case TreeTypes.Jungle:
			treeDropItemType = (WorldGen.genRand.NextBool() ? 4292 : 4294);
			break;
		case TreeTypes.Palm:
			if (WorldGen.IsPalmOasisTree(x))
			{
				treeDropItemType = (WorldGen.genRand.NextBool() ? 4283 : 4287);
			}
			break;
		case TreeTypes.PalmCorrupt:
			treeDropItemType = ((!WorldGen.genRand.NextBool()) ? ((!WorldGen.IsPalmOasisTree(x)) ? (WorldGen.genRand.NextBool() ? 4284 : 4289) : (WorldGen.genRand.NextBool() ? 4283 : 4287)) : (WorldGen.genRand.NextBool() ? 4284 : 4289));
			break;
		case TreeTypes.Corrupt:
			treeDropItemType = (WorldGen.genRand.NextBool() ? 4284 : 4289);
			break;
		case TreeTypes.PalmHallowed:
			treeDropItemType = ((!WorldGen.genRand.NextBool()) ? ((!WorldGen.IsPalmOasisTree(x)) ? (WorldGen.genRand.NextBool() ? 4288 : 4297) : (WorldGen.genRand.NextBool() ? 4283 : 4287)) : (WorldGen.genRand.NextBool() ? 4288 : 4297));
			break;
		case TreeTypes.Hallowed:
			treeDropItemType = (WorldGen.genRand.NextBool() ? 4288 : 4297);
			break;
		case TreeTypes.PalmCrimson:
			treeDropItemType = ((!WorldGen.genRand.NextBool()) ? ((!WorldGen.IsPalmOasisTree(x)) ? (WorldGen.genRand.NextBool() ? 4285 : 4296) : (WorldGen.genRand.NextBool() ? 4283 : 4287)) : (WorldGen.genRand.NextBool() ? 4285 : 4296));
			break;
		case TreeTypes.Crimson:
			treeDropItemType = (WorldGen.genRand.NextBool() ? 4285 : 4296);
			break;
		case TreeTypes.Ash:
			treeDropItemType = (WorldGen.genRand.NextBool() ? 5278 : 5277);
			break;
		}
		if (treeDropItemType != 0)
		{
			Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), x * 16, y * 16, 16, 16, treeDropItemType);
		}
	}

	public override bool ShakeTree(int x, int y, TreeTypes treeType)
	{
		if (WorldGen.genRand.NextBool(100) || (DateTime.Now.Month == 2 && DateTime.Now.Day == 14 && WorldGen.genRand.NextBool(15)))
		{
			Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), x * 16, y * 16, 16, 16, ModContent.ItemType<HapuFruit>());
			return true;
		}
		return base.ShakeTree(x, y, treeType);
	}

	public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Main.tile[i, j];
		if (Main.tileSolid[tile.TileType] && tile.TileType != ModContent.TileType<LumenylCrystals>() && tile.TileType != ModContent.TileType<SeaPrismCrystals>() && tile.TileType != ModContent.TileType<SmallCorals>())
		{
			bool dontShatter = fail | effectOnly;
			CheckShatterCrystal(i + 1, j, dontShatter);
			CheckShatterCrystal(i - 1, j, dontShatter);
			CheckShatterCrystal(i, j + 1, dontShatter);
			CheckShatterCrystal(i, j - 1, dontShatter);
		}
		if (Main.netMode != 1 && tile.TileType >= 481 && tile.TileType <= 483)
		{
			for (int m = 0; m < 8; m++)
			{
				int x = i;
				int y = j;
				switch (m)
				{
				case 0:
					x--;
					break;
				case 1:
					x++;
					break;
				case 2:
					y--;
					break;
				case 3:
					y++;
					break;
				case 4:
					x--;
					y--;
					break;
				case 5:
					x++;
					y--;
					break;
				case 6:
					x--;
					y++;
					break;
				case 7:
					x++;
					y++;
					break;
				}
				Tile tile3 = Main.tile[x, y];
				if (tile3.HasTile && tile3.TileType >= 481 && tile3.TileType <= 483)
				{
					tile.Get<TileWallWireStateData>().HasTile = false;
					WorldGen.KillTile(x, y, fail: false, effectOnly: false, noItem: true);
					if (Main.dedServ)
					{
						NetMessage.TrySendData(17, -1, -1, null, 20, x, y);
					}
				}
			}
			int projectileType = tile.TileType - 481 + 736;
			int damage = 20;
			if (Main.netMode == 0)
			{
				Projectile.NewProjectile(new EntitySource_TileBreak(i, j), i * 16 + 8, j * 16 + 8, 0f, 0.41f, projectileType, damage, 0f, Main.myPlayer);
			}
			else if (Main.dedServ)
			{
				int proj = Projectile.NewProjectile(new EntitySource_TileBreak(i, j), i * 16 + 8, j * 16 + 8, 0f, 0.41f, projectileType, damage, 0f, Main.myPlayer);
				Main.projectile[proj].netUpdate = true;
			}
		}
		Player player = Main.LocalPlayer;
		if (player == null || !player.active || !player.Calamity().miningSet || player.Calamity().miningSetCooldown > 0 || fail || !TileID.Sets.Ore[tile.TileType] || !Main.rand.NextBool(4))
		{
			return;
		}
		EntitySource_TileBreak source = new EntitySource_TileBreak(i, j);
		Vector2 pos = new Vector2((float)i, (float)j) * 16f;
		ModTile moddedTile = TileLoader.GetTile(tile.TileType);
		if (moddedTile != null)
		{
			IEnumerable<Item> itemDrops = moddedTile.GetItemDrops(i, j);
			if (itemDrops == null)
			{
				return;
			}
			foreach (Item item in itemDrops)
			{
				item.Prefix(-1);
				int moddedOre = Item.NewItem(source, pos, item);
				Main.item[moddedOre].TryCombiningIntoNearbyItems(moddedOre);
			}
		}
		else
		{
			int itemType = TileLoader.GetItemDropFromTypeAndStyle(tile.TileType);
			Item.NewItem(source, pos, itemType);
		}
		player.Calamity().miningSetCooldown = Main.rand.Next(180, 361);
		static void CheckShatterCrystal(int xPos, int yPos, bool flag)
		{
			if (!((xPos < 0 || xPos >= Main.maxTilesX || yPos < 0 || yPos >= Main.maxTilesY) | flag))
			{
				Tile t = Main.tile[xPos, yPos];
				if (t.HasTile && (t.TileType == ModContent.TileType<LumenylCrystals>() || t.TileType == ModContent.TileType<SeaPrismCrystals>() || t.TileType == ModContent.TileType<SmallCorals>()))
				{
					WorldGen.KillTile(xPos, yPos);
					if (!Main.tile[xPos, yPos].HasTile && Main.netMode != 0)
					{
						NetMessage.SendData(17, -1, -1, null, 0, xPos, yPos);
					}
				}
			}
		}
	}

	public override void Drop(int i, int j, int type)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		if (type == 26 && Main.hardMode)
		{
			Vector2 spreadMinMax = default(Vector2);
			((Vector2)(ref spreadMinMax))._002Ector(-32f, 32f);
			if (CalamityServerConfig.Instance.EarlyHardmodeProgressionRework)
			{
				DropItem(i, j, 521, 4, asStack: false, spreadMinMax);
				WorldGen.altarCount++;
				AchievementsHelper.NotifyProgressionEvent(6);
			}
			if (WorldGen.altarCount > 1 && WorldGen.altarCount % 12 == 0)
			{
				DropItem(i, j, ModContent.ItemType<EvilSmasher>(), 1, asStack: true, default(Vector2), prefix: true);
			}
		}
	}

	private static void DropItem(int i, int j, int itemType, int quantity, bool asStack, Vector2 spreadMinMax = default(Vector2), bool prefix = false)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode == 1)
		{
			return;
		}
		Vector2 worldPos = new Vector2((float)i, (float)j) * 16f;
		if (asStack)
		{
			Vector2 spawnOffset = Main.rand.NextVector2Unit(spreadMinMax.X, spreadMinMax.Y);
			Item.NewItem(new EntitySource_TileBreak(i, j), worldPos + spawnOffset, itemType, quantity, noBroadcast: false, prefix ? (-1) : 0);
			return;
		}
		for (int k = 0; k < quantity; k++)
		{
			Vector2 spawnOffset2 = Main.rand.NextVector2Unit(spreadMinMax.X, spreadMinMax.Y);
			Item.NewItem(new EntitySource_TileBreak(i, j), worldPos + spawnOffset2, itemType, 1, noBroadcast: false, prefix ? (-1) : 0);
		}
	}
}
