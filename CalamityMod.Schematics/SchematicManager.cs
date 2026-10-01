using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CalamityMod.TileEntities;
using CalamityMod.Tiles.DraedonStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Schematics;

public sealed class SchematicManager : ModSystem
{
	public delegate void PilePlacementFunction(int x, int y, Rectangle placeInArea);

	internal const string ShimmerShrineKey = "Shimmer Shrine Key";

	internal const string ShimmerShrineFilename = "Schematics/Shimmer_Shrine.csch";

	internal const string RustedWorkshopKey = "Rusted Workshop";

	internal const string RustedWorkshopFilename = "Schematics/RustedWorkshop.csch";

	internal const string ResearchOutpostKey = "Research Outpost";

	internal const string ResearchOutpostFilename = "Schematics/ResearchOutpost.csch";

	internal const string SunkenSeaLabKey = "Sunken Sea Laboratory";

	internal const string SunkenSeaLabFilename = "Schematics/Arsenal_Lab_Sunken.csch";

	internal const string PlanetoidLabKey = "Planetoid Laboratory";

	internal const string PlanetoidLabFilename = "Schematics/Arsenal_Lab_Planetoid.csch";

	internal const string PlagueLabKey = "Plague Laboratory";

	internal const string PlagueLabFilename = "Schematics/Arsenal_Lab_Plague.csch";

	internal const string HellLabKey = "Hell Laboratory";

	internal const string HellLabFilename = "Schematics/Arsenal_Lab_Underworld.csch";

	internal const string IceLabKey = "Ice Laboratory";

	internal const string IceLabFilename = "Schematics/Arsenal_Lab_Ice.csch";

	internal const string CavernLabKey = "Cavern Laboratory";

	internal const string CavernLabFilename = "Schematics/Arsenal_Lab_Onyx.csch";

	internal const string CorruptionShrineKey = "Corruption Shrine";

	internal const string CorruptionShrineFilename = "Schematics/Shrine_Corruption.csch";

	internal const string CrimsonShrineKey = "Crimson Shrine";

	internal const string CrimsonShrineFilename = "Schematics/Shrine_Crimson.csch";

	internal const string DesertShrineKey = "Desert Shrine";

	internal const string DesertShrineFilename = "Schematics/Shrine_Desert.csch";

	internal const string GraniteShrineKey = "Granite Shrine";

	internal const string GraniteShrineFilename = "Schematics/Shrine_Granite.csch";

	internal const string IceShrineKey = "Ice Shrine";

	internal const string IceShrineFilename = "Schematics/Shrine_Ice.csch";

	internal const string MarbleShrineKey = "Marble Shrine";

	internal const string MarbleShrineFilename = "Schematics/Shrine_Marble.csch";

	internal const string MushroomShrineKey = "Mushroom Shrine";

	internal const string MushroomShrineFilename = "Schematics/Shrine_Mushroom.csch";

	internal const string SurfaceShrineKey = "Surface Shrine";

	internal const string SurfaceShrineFilename = "Schematics/Shrine_Surface.csch";

	internal const string RoxcaliburShrineKey1 = "Roxcalibur Shrine 1";

	internal const string RoxcaliburShrine1Filename = "Schematics/Shrine_Roxcalibur_ShrineVariant.csch";

	internal const string RoxcaliburShrineKey2 = "Roxcalibur Shrine 2";

	internal const string RoxcaliburShrine2Filename = "Schematics/Shrine_Roxcalibur_TorchVariant.csch";

	internal const string VernalKey = "Vernal Pass";

	internal const string VernalFilename = "Schematics/VernalPass.csch";

	internal const string MechanicShedKey = "Mechanic Key";

	internal const string MechanicShedFilename = "Schematics/MechanicShed.csch";

	internal const string AstralBeaconKey = "Astral Beacon";

	internal const string AstralBeaconFilename = "Schematics/AstralBeacon.csch";

	internal const string CragBridgeKey = "Crags Bridge";

	internal const string CragBridgeFilename = "Schematics/CragBridge.csch";

	internal const string BlueArchiveKey = "Archive Blue";

	internal const string BlueArchiveFilename = "Schematics/DungeonArchiveBlue.csch";

	internal const string GreenArchiveKey = "Archive Green";

	internal const string GreenArchiveFilename = "Schematics/DungeonArchiveGreen.csch";

	internal const string PinkArchiveKey = "Archive Pink";

	internal const string PinkArchiveFilename = "Schematics/DungeonArchivePink.csch";

	internal const string CragRuinKey1 = "Crag Ruin 1";

	internal const string CragRuinKey1Filename = "Schematics/CragRuin1.csch";

	internal const string CragRuinKey2 = "Crag Ruin 21";

	internal const string CragRuinKey2Filename = "Schematics/CragRuin2.csch";

	internal const string CragRuinKey3 = "Crag Ruin 3";

	internal const string CragRuinKey3Filename = "Schematics/CragRuin3.csch";

	internal const string CragRuinKey4 = "Crag Ruin 4";

	internal const string CragRuinKey4Filename = "Schematics/CragRuin4.csch";

	internal static Dictionary<string, SchematicMetaTile[,]> TileMaps;

	internal static Dictionary<string, PilePlacementFunction> PilePlacementMaps;

	private static readonly List<int> ValidTileEntityTypes;

	public override void OnModLoad()
	{
		PilePlacementMaps = new Dictionary<string, PilePlacementFunction>();
		TileMaps = new Dictionary<string, SchematicMetaTile[,]>
		{
			["Shimmer Shrine Key"] = CalamitySchematicIO.LoadSchematic("Schematics/Shimmer_Shrine.csch"),
			["Rusted Workshop"] = CalamitySchematicIO.LoadSchematic("Schematics/RustedWorkshop.csch"),
			["Research Outpost"] = CalamitySchematicIO.LoadSchematic("Schematics/ResearchOutpost.csch"),
			["Sunken Sea Laboratory"] = CalamitySchematicIO.LoadSchematic("Schematics/Arsenal_Lab_Sunken.csch"),
			["Planetoid Laboratory"] = CalamitySchematicIO.LoadSchematic("Schematics/Arsenal_Lab_Planetoid.csch"),
			["Plague Laboratory"] = CalamitySchematicIO.LoadSchematic("Schematics/Arsenal_Lab_Plague.csch"),
			["Hell Laboratory"] = CalamitySchematicIO.LoadSchematic("Schematics/Arsenal_Lab_Underworld.csch"),
			["Ice Laboratory"] = CalamitySchematicIO.LoadSchematic("Schematics/Arsenal_Lab_Ice.csch"),
			["Cavern Laboratory"] = CalamitySchematicIO.LoadSchematic("Schematics/Arsenal_Lab_Onyx.csch"),
			["Corruption Shrine"] = CalamitySchematicIO.LoadSchematic("Schematics/Shrine_Corruption.csch"),
			["Crimson Shrine"] = CalamitySchematicIO.LoadSchematic("Schematics/Shrine_Crimson.csch"),
			["Desert Shrine"] = CalamitySchematicIO.LoadSchematic("Schematics/Shrine_Desert.csch"),
			["Granite Shrine"] = CalamitySchematicIO.LoadSchematic("Schematics/Shrine_Granite.csch"),
			["Ice Shrine"] = CalamitySchematicIO.LoadSchematic("Schematics/Shrine_Ice.csch"),
			["Marble Shrine"] = CalamitySchematicIO.LoadSchematic("Schematics/Shrine_Marble.csch"),
			["Mushroom Shrine"] = CalamitySchematicIO.LoadSchematic("Schematics/Shrine_Mushroom.csch"),
			["Surface Shrine"] = CalamitySchematicIO.LoadSchematic("Schematics/Shrine_Surface.csch"),
			["Roxcalibur Shrine 1"] = CalamitySchematicIO.LoadSchematic("Schematics/Shrine_Roxcalibur_ShrineVariant.csch"),
			["Roxcalibur Shrine 2"] = CalamitySchematicIO.LoadSchematic("Schematics/Shrine_Roxcalibur_TorchVariant.csch"),
			["Vernal Pass"] = CalamitySchematicIO.LoadSchematic("Schematics/VernalPass.csch"),
			["Mechanic Key"] = CalamitySchematicIO.LoadSchematic("Schematics/MechanicShed.csch"),
			["Astral Beacon"] = CalamitySchematicIO.LoadSchematic("Schematics/AstralBeacon.csch"),
			["Crags Bridge"] = CalamitySchematicIO.LoadSchematic("Schematics/CragBridge.csch"),
			["Archive Blue"] = CalamitySchematicIO.LoadSchematic("Schematics/DungeonArchiveBlue.csch"),
			["Archive Green"] = CalamitySchematicIO.LoadSchematic("Schematics/DungeonArchiveGreen.csch"),
			["Archive Pink"] = CalamitySchematicIO.LoadSchematic("Schematics/DungeonArchivePink.csch"),
			["Crag Ruin 1"] = CalamitySchematicIO.LoadSchematic("Schematics/CragRuin1.csch"),
			["Crag Ruin 21"] = CalamitySchematicIO.LoadSchematic("Schematics/CragRuin2.csch"),
			["Crag Ruin 3"] = CalamitySchematicIO.LoadSchematic("Schematics/CragRuin3.csch"),
			["Crag Ruin 4"] = CalamitySchematicIO.LoadSchematic("Schematics/CragRuin4.csch"),
			["Sulphurous Scrap 1"] = CalamitySchematicIO.LoadSchematic("Schematics/SulphurousScrap1.csch").ShaveOffEdge(),
			["Sulphurous Scrap 2"] = CalamitySchematicIO.LoadSchematic("Schematics/SulphurousScrap2.csch").ShaveOffEdge(),
			["Sulphurous Scrap 3"] = CalamitySchematicIO.LoadSchematic("Schematics/SulphurousScrap3.csch").ShaveOffEdge(),
			["Sulphurous Scrap 4"] = CalamitySchematicIO.LoadSchematic("Schematics/SulphurousScrap4.csch").ShaveOffEdge(),
			["Sulphurous Scrap 5"] = CalamitySchematicIO.LoadSchematic("Schematics/SulphurousScrap5.csch").ShaveOffEdge(),
			["Sulphurous Scrap 6"] = CalamitySchematicIO.LoadSchematic("Schematics/SulphurousScrap6.csch").ShaveOffEdge(),
			["Sulphurous Scrap 7"] = CalamitySchematicIO.LoadSchematic("Schematics/SulphurousScrap7.csch").ShaveOffEdge()
		};
	}

	public override void Unload()
	{
		TileMaps = null;
		PilePlacementMaps = null;
	}

	public static Vector2? GetSchematicArea(string name)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (!TileMaps.TryGetValue(name, out var schematic))
		{
			return null;
		}
		return new Vector2((float)schematic.GetLength(0), (float)schematic.GetLength(1));
	}

	public static void PlaceSchematic<T>(string name, Point pos, SchematicAnchor anchorType, ref bool specialCondition, T chestDelegate = null, bool flipHorizontal = false) where T : Delegate
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad0: Unknown result type (might be due to invalid IL or missing references)
		if (!TileMaps.TryGetValue(name, out var schematic))
		{
			CalamityMod.Log.Warn((object)("Tried to place a schematic with name \"" + name + "\". No matching schematic file found."));
			return;
		}
		if (chestDelegate != null && !(chestDelegate is Action<Chest>) && !(chestDelegate is Action<Chest, int, bool>))
		{
			throw new ArgumentException("The chest interaction function has invalid parameters.", "chestDelegate");
		}
		PilePlacementMaps.TryGetValue(name, out var pilePlacementFunction);
		int width = schematic.GetLength(0);
		int height = schematic.GetLength(1);
		int cornerX = pos.X;
		int cornerY = pos.Y;
		switch (anchorType)
		{
		case SchematicAnchor.TopCenter:
			cornerX -= width / 2;
			break;
		case SchematicAnchor.TopRight:
			cornerX -= width;
			break;
		case SchematicAnchor.CenterLeft:
			cornerY -= height / 2;
			break;
		case SchematicAnchor.Center:
			cornerX -= width / 2;
			cornerY -= height / 2;
			break;
		case SchematicAnchor.CenterRight:
			cornerX -= width;
			cornerY -= height / 2;
			break;
		case SchematicAnchor.BottomLeft:
			cornerY -= height;
			break;
		case SchematicAnchor.BottomCenter:
			cornerX -= width / 2;
			cornerY -= height;
			break;
		case SchematicAnchor.BottomRight:
			cornerX -= width;
			cornerY -= height;
			break;
		}
		if (!WorldGen.InWorld(cornerX, cornerY) || !WorldGen.InWorld(cornerX + width, cornerY + height))
		{
			CalamityMod.Log.Warn((object)"Schematic failed to place: Part of the target location is outside the game world.");
			return;
		}
		SchematicMetaTile[,] originalTiles = new SchematicMetaTile[width, height];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				Tile t = Main.tile[x + cornerX, y + cornerY];
				if (t.TileType == 5 || t.TileType == 170 || t.TileType == 80)
				{
					WorldGen.KillTile(x + cornerX, y + cornerY, fail: false, effectOnly: false, noItem: true);
				}
			}
		}
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				Tile t2 = Main.tile[i + cornerX, j + cornerY];
				originalTiles[i, j] = new SchematicMetaTile(t2);
			}
		}
		for (int k = 0; k < width; k++)
		{
			for (int l = 0; l < height; l++)
			{
				if (originalTiles[k, l].TileType != 21)
				{
					WorldGen.KillTile(k + cornerX, l + cornerY, fail: false, effectOnly: false, noItem: true);
				}
			}
		}
		Rectangle placeInArea = default(Rectangle);
		for (int m = 0; m < width; m++)
		{
			for (int n = 0; n < height; n++)
			{
				SchematicMetaTile smt = schematic[flipHorizontal ? (width - 1 - m) : m, n];
				smt.ApplyTo(m + cornerX, n + cornerY, originalTiles[m, n]);
				Tile worldTile = Main.tile[m + cornerX, n + cornerY];
				if (flipHorizontal && !smt.keepTile)
				{
					if (worldTile.Slope != SlopeType.Solid)
					{
						worldTile.Slope += (((int)worldTile.Slope % 2 != 0) ? 1 : (-1));
					}
					int style = 0;
					int alt = 0;
					TileObjectData.GetTileInfo(worldTile, ref style, ref alt);
					TileObjectData data = TileObjectData.GetTileData(worldTile.TileType, style, alt);
					if (data != null && !TileID.Sets.Platforms[worldTile.TileType])
					{
						int sheetSquare = 16 + data.CoordinatePadding;
						if (data.Width > 1)
						{
							int frameNum = worldTile.TileFrameX / sheetSquare % data.Width;
							worldTile.TileFrameX += (short)((-frameNum + (data.Width - (frameNum + 1))) * (16 + data.CoordinatePadding));
						}
						if (data.Direction != TileObjectDirection.None)
						{
							int range = 1;
							if (data.RandomStyleRange > range)
							{
								range = data.RandomStyleRange;
							}
							if (worldTile.TileFrameX / sheetSquare % (data.Width * data.StyleMultiplier * range) < data.Width)
							{
								worldTile.TileFrameX += (short)(sheetSquare * data.Width);
							}
							else
							{
								worldTile.TileFrameX -= (short)(sheetSquare * data.Width);
							}
						}
					}
					else if (TileID.Sets.Platforms[worldTile.TileType])
					{
						switch (worldTile.TileFrameX / 18)
						{
						case 1:
							worldTile.TileFrameX += 18;
							break;
						case 2:
							worldTile.TileFrameX -= 18;
							break;
						case 3:
							worldTile.TileFrameX += 18;
							break;
						case 4:
							worldTile.TileFrameX -= 18;
							break;
						case 8:
							worldTile.TileFrameX += 36;
							break;
						case 10:
							worldTile.TileFrameX -= 36;
							break;
						case 12:
							worldTile.TileFrameX += 18;
							break;
						case 13:
							worldTile.TileFrameX -= 18;
							break;
						case 15:
							worldTile.TileFrameX += 18;
							break;
						case 16:
							worldTile.TileFrameX -= 18;
							break;
						case 19:
							worldTile.TileFrameX += 18;
							break;
						case 20:
							worldTile.TileFrameX -= 18;
							break;
						case 25:
							worldTile.TileFrameX += 18;
							break;
						case 26:
							worldTile.TileFrameX -= 18;
							break;
						}
					}
					else
					{
						switch (worldTile.TileType)
						{
						case 28:
							if (worldTile.TileFrameX / 18 == 0)
							{
								worldTile.TileFrameX += 18;
							}
							else
							{
								worldTile.TileFrameX -= 18;
							}
							break;
						case 149:
							if (worldTile.TileFrameY / 18 == 3)
							{
								worldTile.TileFrameY -= 18;
							}
							else if (worldTile.TileFrameY / 18 == 2)
							{
								worldTile.TileFrameY += 18;
							}
							break;
						case 314:
							switch (worldTile.TileFrameX)
							{
							case 2:
								worldTile.TileFrameX++;
								break;
							case 3:
								worldTile.TileFrameX--;
								break;
							case 4:
								worldTile.TileFrameX++;
								break;
							case 5:
								worldTile.TileFrameX--;
								break;
							case 6:
								worldTile.TileFrameX++;
								break;
							case 7:
								worldTile.TileFrameX--;
								break;
							case 8:
								worldTile.TileFrameX++;
								break;
							case 9:
								worldTile.TileFrameX--;
								break;
							case 14:
								worldTile.TileFrameX++;
								break;
							case 15:
								worldTile.TileFrameX--;
								break;
							case 18:
								worldTile.TileFrameX++;
								break;
							case 19:
								worldTile.TileFrameX--;
								break;
							case 24:
								worldTile.TileFrameX++;
								break;
							case 25:
								worldTile.TileFrameX--;
								break;
							}
							if (worldTile.TileFrameY == 8)
							{
								worldTile.TileFrameY++;
							}
							else if (worldTile.TileFrameY == 9)
							{
								worldTile.TileFrameY--;
							}
							break;
						case 178:
							if (worldTile.TileFrameY / 54 == 3)
							{
								worldTile.TileFrameY -= 54;
							}
							else if (worldTile.TileFrameY / 54 == 2)
							{
								worldTile.TileFrameY += 54;
							}
							break;
						case 5:
							if (worldTile.TileFrameY / 22 >= 9)
							{
								if (worldTile.TileFrameX / 22 != 1)
								{
									if (worldTile.TileFrameX / 22 == 2)
									{
										worldTile.TileFrameX += 22;
									}
									else if (worldTile.TileFrameX / 22 == 3)
									{
										worldTile.TileFrameX -= 22;
									}
								}
								break;
							}
							switch (worldTile.TileFrameX / 22)
							{
							case 0:
								if (worldTile.TileFrameY / 22 > 5)
								{
									worldTile.TileFrameX += 66;
								}
								break;
							case 1:
								worldTile.TileFrameX += 22;
								break;
							case 2:
								worldTile.TileFrameX -= 22;
								break;
							case 3:
								worldTile.TileFrameX += 22;
								if (worldTile.TileFrameY / 22 < 3)
								{
									worldTile.TileFrameY += 66;
								}
								else if (worldTile.TileFrameY / 18 < 6)
								{
									worldTile.TileFrameY -= 66;
								}
								break;
							case 4:
								worldTile.TileFrameX -= 22;
								if (worldTile.TileFrameY / 22 < 3)
								{
									worldTile.TileFrameY += 66;
								}
								else if (worldTile.TileFrameY / 22 < 6)
								{
									worldTile.TileFrameY -= 66;
								}
								else if (worldTile.TileFrameY / 22 >= 6)
								{
									worldTile.TileFrameX -= 66;
								}
								break;
							}
							break;
						}
					}
				}
				bool isChest = worldTile.TileType == 21 || TileID.Sets.BasicChest[worldTile.TileType];
				if ((!smt.keepTile & isChest) && worldTile.TileFrameX % 36 == 0 && worldTile.TileFrameY == 0)
				{
					int chestIndex = Chest.FindChestByGuessing(m + cornerX - 1, n + cornerY - 1);
					if (chestIndex == -1)
					{
						chestIndex = Chest.CreateChest(m + cornerX, n + cornerY);
						Chest chest = Main.chest[chestIndex];
						if (chestDelegate is Action<Chest, int, bool>)
						{
							(chestDelegate as Action<Chest, int, bool>)?.Invoke(chest, worldTile.TileType, specialCondition);
							specialCondition = true;
						}
						else if (chestDelegate is Action<Chest>)
						{
							(chestDelegate as Action<Chest>)?.Invoke(chest);
						}
					}
				}
				TryToPlaceTileEntities(m + cornerX, n + cornerY, worldTile);
				((Rectangle)(ref placeInArea))._002Ector(m, n, width, height);
				pilePlacementFunction?.Invoke(m + cornerX, n + cornerY, placeInArea);
			}
		}
	}

	private static void TryToPlaceTileEntities(int x, int y, Tile t)
	{
		int tileType = t.TileType;
		if (!ValidTileEntityTypes.Contains(tileType))
		{
			return;
		}
		int index = ValidTileEntityTypes.IndexOf(tileType);
		int style = 0;
		int alt = 0;
		TileObjectData.GetTileInfo(t, ref style, ref alt);
		TileObjectData data = TileObjectData.GetTileData(t.TileType, style, alt);
		int FrameX;
		int FrameY;
		if (data != null)
		{
			int sheetSquare = 16 + data.CoordinatePadding;
			FrameX = t.TileFrameX / sheetSquare % data.Width;
			FrameY = t.TileFrameY / sheetSquare % data.Height;
		}
		else
		{
			FrameX = t.TileFrameX;
			FrameY = t.TileFrameY;
		}
		if (t.HasTile && FrameX == 0 && FrameY == 0)
		{
			switch (index)
			{
			case 0:
				TileEntity.PlaceEntityNet(x, y, ModContent.TileEntityType<TEChargingStation>());
				break;
			case 1:
				TileEntity.PlaceEntityNet(x, y, ModContent.TileEntityType<TEHostileLabTurret>());
				break;
			case 2:
				TileEntity.PlaceEntityNet(x, y, ModContent.TileEntityType<TELabHologramProjector>());
				break;
			case 3:
				TileEntity.PlaceEntityNet(x, y, ModContent.TileEntityType<TEHostileFireTurret>());
				break;
			case 4:
				TileEntity.PlaceEntityNet(x, y, ModContent.TileEntityType<TEHostileIceTurret>());
				break;
			case 5:
				TileEntity.PlaceEntityNet(x, y, ModContent.TileEntityType<TEHostileLaserTurret>());
				break;
			case 6:
				TileEntity.PlaceEntityNet(x, y, ModContent.TileEntityType<TEHostileOnyxTurret>());
				break;
			case 7:
				TileEntity.PlaceEntityNet(x, y, ModContent.TileEntityType<TEHostilePlagueTurret>());
				break;
			case 8:
				TileEntity.PlaceEntityNet(x, y, ModContent.TileEntityType<TEHostileWaterTurret>());
				break;
			}
		}
	}

	static SchematicManager()
	{
		int num = 9;
		List<int> list = new List<int>(num);
		CollectionsMarshal.SetCount(list, num);
		Span<int> span = CollectionsMarshal.AsSpan(list);
		int num2 = 0;
		span[num2] = ModContent.TileType<ChargingStation>();
		num2++;
		span[num2] = ModContent.TileType<DraedonLabTurret>();
		num2++;
		span[num2] = ModContent.TileType<LabHologramProjector>();
		num2++;
		span[num2] = ModContent.TileType<HostileFireTurret>();
		num2++;
		span[num2] = ModContent.TileType<HostileIceTurret>();
		num2++;
		span[num2] = ModContent.TileType<HostileLaserTurret>();
		num2++;
		span[num2] = ModContent.TileType<HostileOnyxTurret>();
		num2++;
		span[num2] = ModContent.TileType<HostilePlagueTurret>();
		num2++;
		span[num2] = ModContent.TileType<HostileWaterTurret>();
		ValidTileEntityTypes = list;
	}
}
