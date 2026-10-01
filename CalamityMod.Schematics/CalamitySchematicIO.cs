using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.IO;

namespace CalamityMod.Schematics;

public static class CalamitySchematicIO
{
	public const ushort TML_14_TileID_Count = 625;

	public const ushort TML_14_WallID_Count = 316;

	private const int SchematicBufferStartingSize = 16384;

	public static bool UseCompression = true;

	private static readonly byte[] SchematicMagicNumberHeader_TML13 = new byte[3] { 202, 26, 92 };

	private static readonly byte[] SchematicMagicNumberHeader_TML14 = new byte[3] { 202, 20, 92 };

	private static readonly byte[] SchematicMagicNumberHeader_Infernum14 = new byte[3] { 31, 20, 92 };

	private static readonly byte[] SchematicMagicNumberHeader_TML144 = new byte[3] { 202, 68, 92 };

	private const byte UncompressedMagicNumber = 0;

	private const byte CompressedMagicNumber = 192;

	private const string PreserveTileName = "_";

	public static ushort PreserveTileID = 0;

	public static ushort PreserveWallID = 0;

	private const string InvalidFormatString = "Provided file is not a valid Calamity Schematic.";

	private const string TML13ValidString = "An attempt was made to load a valid Calamity Schematic for TML 1.3. These files cannot be translated into TML 1.4. The schematic will show up empty.";

	internal static void AssignWallWireState(ref TileWallWireStateData target, int source)
	{
		target.HasTile = TileDataPacking.GetBit(source, 0);
		target.IsActuated = TileDataPacking.GetBit(source, 1);
		target.HasActuator = TileDataPacking.GetBit(source, 2);
		target.TileColor = (byte)TileDataPacking.Unpack(source, 3, 5);
		target.WallColor = (byte)TileDataPacking.Unpack(source, 8, 5);
		target.TileFrameNumber = TileDataPacking.Unpack(source, 13, 2);
		target.WallFrameNumber = TileDataPacking.Unpack(source, 15, 2);
		target.WallFrameX = TileDataPacking.Unpack(source, 17, 4);
		target.WallFrameY = TileDataPacking.Unpack(source, 21, 3);
		target.IsHalfBlock = TileDataPacking.GetBit(source, 24);
		target.Slope = (SlopeType)TileDataPacking.Unpack(source, 25, 3);
		target.WireData = TileDataPacking.Unpack(source, 28, 4);
	}

	private static SchematicMetaTile ReadSchematicMetaTile(this BinaryReader reader, bool TML144 = true)
	{
		SchematicMetaTile smt = new SchematicMetaTile
		{
			TileType = reader.ReadUInt16(),
			WallType = reader.ReadUInt16(),
			LiquidAmount = reader.ReadByte(),
			LiquidType = reader.ReadByte(),
			wallWireState = 
			{
				TileFrameX = reader.ReadInt16(),
				TileFrameY = reader.ReadInt16()
			}
		};
		AssignWallWireState(ref smt.wallWireState, reader.ReadInt32());
		if (TML144)
		{
			byte biBitpack = reader.ReadByte();
			smt.brightnessInvisibility = new TileWallBrightnessInvisibilityData
			{
				IsTileInvisible = ((biBitpack & 1) != 0),
				IsWallInvisible = ((biBitpack & 2) != 0),
				IsTileFullbright = ((biBitpack & 4) != 0),
				IsWallFullbright = ((biBitpack & 8) != 0)
			};
		}
		else
		{
			smt.brightnessInvisibility = null;
		}
		return smt;
	}

	private static void WriteSchematicMetaTile(this BinaryWriter writer, SchematicMetaTile smt)
	{
		writer.Write(smt.TileType);
		writer.Write(smt.WallType);
		writer.Write(smt.LiquidAmount);
		writer.Write(smt.LiquidType);
		writer.Write(smt.wallWireState.TileFrameX);
		writer.Write(smt.wallWireState.TileFrameY);
		writer.Write(smt.wallWireState.NonFrameBits);
		if (smt.brightnessInvisibility.HasValue)
		{
			writer.Write(smt.brightnessInvisibility.Value.Data);
		}
		else
		{
			writer.Write((byte)0);
		}
	}

	private static Tile[,] GetTilesInRectangle(Rectangle area)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Tile[,] tiles = new Tile[area.Width, area.Height];
		for (int i = ((Rectangle)(ref area)).Left; i < ((Rectangle)(ref area)).Right; i++)
		{
			for (int j = ((Rectangle)(ref area)).Top; j < ((Rectangle)(ref area)).Bottom; j++)
			{
				Tile t = Main.tile[i, j];
				tiles[i - ((Rectangle)(ref area)).Left, j - ((Rectangle)(ref area)).Top] = t;
			}
		}
		return tiles;
	}

	public static bool EqualToMetaTile(this Tile t, SchematicMetaTile smt)
	{
		if (t.Get<TileWallWireStateData>().NonFrameBits != smt.wallWireState.NonFrameBits)
		{
			return false;
		}
		if (t.WallType != smt.WallType || t.LiquidAmount != smt.LiquidAmount)
		{
			return false;
		}
		if (t.LiquidAmount > 0 && t.LiquidType != smt.LiquidType)
		{
			return false;
		}
		if (t.TileType != smt.TileType)
		{
			return false;
		}
		if (Main.tileFrameImportant[t.TileType] && (t.TileFrameX != smt.wallWireState.TileFrameX || t.TileFrameY != smt.wallWireState.TileFrameY))
		{
			return false;
		}
		byte tileBIData = t.Get<TileWallBrightnessInvisibilityData>().Data;
		if (smt.brightnessInvisibility.HasValue && smt.brightnessInvisibility.Value.Data != tileBIData)
		{
			return false;
		}
		return true;
	}

	private static int GetMetaTileIndex(IList<SchematicMetaTile> metaTiles, ref Tile toSearch)
	{
		int numTiles = metaTiles.Count;
		for (int i = 0; i < numTiles; i++)
		{
			if (toSearch.EqualToMetaTile(metaTiles[i]))
			{
				return i;
			}
		}
		return -1;
	}

	private static string GetFullName(this ModTile mt)
	{
		return mt.Mod.Name + "/" + mt.Name;
	}

	private static string GetFullName(this ModWall mw)
	{
		return mw.Mod.Name + "/" + mw.Name;
	}

	private static void ComputeMetaIndices(ref SchematicData schematic, ref SchematicMetaTile smt)
	{
		if (PreserveTileID > 0 && smt.TileType == PreserveTileID)
		{
			smt.TileType = TileID.Count;
			smt.keepTile = true;
		}
		else if (smt.TileType >= TileID.Count)
		{
			ModTile mt = ModContent.GetModTile(smt.TileType);
			if (mt != null)
			{
				string tileFullName = mt.GetFullName();
				int tileNameIndex = schematic.modTileNames.IndexOf(tileFullName);
				if (tileNameIndex == -1)
				{
					tileNameIndex = schematic.modTileNames.Count;
					schematic.modTileNames.Add(tileFullName);
				}
				smt.TileType = (ushort)(TileID.Count + tileNameIndex);
			}
		}
		if (PreserveWallID > 0 && smt.WallType == PreserveWallID)
		{
			smt.WallType = WallID.Count;
			smt.keepWall = true;
		}
		else
		{
			if (smt.WallType < WallID.Count)
			{
				return;
			}
			ModWall mw = ModContent.GetModWall(smt.WallType);
			if (mw != null)
			{
				string wallFullName = mw.GetFullName();
				int wallNameIndex = schematic.modWallNames.IndexOf(wallFullName);
				if (wallNameIndex == -1)
				{
					wallNameIndex = schematic.modWallNames.Count;
					schematic.modWallNames.Add(wallFullName);
				}
				smt.WallType = (ushort)(WallID.Count + wallNameIndex);
			}
		}
	}

	private static SchematicData ConstructSchematicData(Tile[,] tiles, bool fourByteIndices)
	{
		int width = tiles.GetLength(0);
		int height = tiles.GetLength(1);
		SchematicData schematic = new SchematicData(tiles.GetLength(0), tiles.GetLength(1));
		schematic.modTileNames.Add("_");
		schematic.modWallNames.Add("_");
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				ref Tile t = ref tiles[x, y];
				int metaTileIndex = GetMetaTileIndex(schematic.uniqueTiles, ref t);
				if (metaTileIndex == -1)
				{
					metaTileIndex = schematic.uniqueTiles.Count;
					SchematicMetaTile smt = new SchematicMetaTile(t);
					ComputeMetaIndices(ref schematic, ref smt);
					schematic.uniqueTiles.Add(smt);
				}
				if (metaTileIndex >= 1048576)
				{
					goto end_IL_00e5;
				}
				if (fourByteIndices)
				{
					schematic.areaIndices[x, y] = (uint)metaTileIndex;
				}
				else
				{
					schematic.areaIndices[x, y] = (ushort)metaTileIndex;
				}
			}
			continue;
			end_IL_00e5:
			break;
		}
		if (schematic.uniqueTiles.Count > 1048576)
		{
			schematic.uniqueTiles.Clear();
		}
		return schematic;
	}

	public static ExportResult ExportSchematic(Rectangle area)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref area)).Top < 0 || ((Rectangle)(ref area)).Left < 0 || ((Rectangle)(ref area)).Right >= Main.maxTilesX || ((Rectangle)(ref area)).Bottom >= Main.maxTilesY)
		{
			return ExportResult.CornerOutOfWorld;
		}
		if (area.Width <= 0 || area.Height <= 0)
		{
			return ExportResult.ZeroArea;
		}
		Tile[,] tiles = GetTilesInRectangle(area);
		bool fourByteIndices = true;
		byte[] magicHeader = SchematicMagicNumberHeader_TML144;
		byte[] renderedStream;
		using (MemoryStream stream = new MemoryStream(16384))
		{
			using BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);
			if (!UseCompression)
			{
				writer.Write(magicHeader);
				writer.Write((byte)0);
			}
			SchematicData schematic = ConstructSchematicData(tiles, fourByteIndices);
			if (schematic.uniqueTiles.Count <= 0)
			{
				writer.Close();
				return ExportResult.TooManyUniqueTiles;
			}
			uint numModTileNames = (uint)((!fourByteIndices) ? ((ushort)schematic.modTileNames.Count) : schematic.modTileNames.Count);
			writer.Write(numModTileNames);
			for (int i = 0; i < numModTileNames; i++)
			{
				writer.Write(schematic.modTileNames[i]);
			}
			uint numModWallNames = (uint)((!fourByteIndices) ? ((ushort)schematic.modWallNames.Count) : schematic.modWallNames.Count);
			writer.Write(numModWallNames);
			for (int j = 0; j < numModWallNames; j++)
			{
				writer.Write(schematic.modWallNames[j]);
			}
			uint numUniqueTiles = (uint)((!fourByteIndices) ? ((ushort)schematic.uniqueTiles.Count) : schematic.uniqueTiles.Count);
			writer.Write(numUniqueTiles);
			for (int k = 0; k < numUniqueTiles; k++)
			{
				writer.WriteSchematicMetaTile(schematic.uniqueTiles[k]);
			}
			ushort tileWidth = (ushort)schematic.areaIndices.GetLength(0);
			writer.Write(tileWidth);
			ushort tileHeight = (ushort)schematic.areaIndices.GetLength(1);
			writer.Write(tileHeight);
			for (ushort y = 0; y < tileHeight; y++)
			{
				for (ushort x = 0; x < tileWidth; x++)
				{
					if (fourByteIndices)
					{
						writer.Write(schematic.areaIndices[x, y]);
					}
					else
					{
						writer.Write((ushort)schematic.areaIndices[x, y]);
					}
				}
			}
			renderedStream = stream.ToArray();
		}
		if (UseCompression)
		{
			using MemoryStream gzMem = new MemoryStream(renderedStream.Length);
			gzMem.Write(magicHeader, 0, magicHeader.Length);
			gzMem.WriteByte(192);
			using (GZipStream gz = new GZipStream(gzMem, CompressionLevel.Optimal))
			{
				gz.Write(renderedStream, 0, renderedStream.Length);
			}
			renderedStream = gzMem.ToArray();
		}
		long fileTimestamp = DateTime.Now.ToFileTime();
		string filename = $"schematic_{fileTimestamp}.csch";
		File.WriteAllBytes(Path.Combine(Main.SavePath, filename), renderedStream);
		return ExportResult.Success;
	}

	private static void ReplaceMetaIndicesWithLoadedIDs(ref SchematicMetaTile smt, string[] modTileNames, string[] modWallNames, ushort tileIDCount, ushort wallIDCount)
	{
		if (smt.TileType >= tileIDCount)
		{
			string tileFullName = modTileNames[smt.TileType - tileIDCount];
			if (tileFullName == "_")
			{
				smt.keepTile = true;
			}
			else
			{
				ModContent.SplitName(tileFullName, out var mod, out var tileName);
				smt.TileType = (ushort)(((int?)ModLoader.GetMod(mod)?.Find<ModTile>(tileName).Type) ?? ModContent.TileType<UnloadedTile>());
			}
		}
		if (smt.WallType >= wallIDCount)
		{
			string wallFullName = modWallNames[smt.WallType - wallIDCount];
			if (wallFullName == "_")
			{
				smt.keepWall = true;
				return;
			}
			ModContent.SplitName(wallFullName, out var mod2, out var wallName);
			smt.WallType = (ushort)(((int?)ModLoader.GetMod(mod2)?.Find<ModWall>(wallName).Type) ?? ModContent.WallType<UnloadedWall>());
		}
	}

	public static SchematicMetaTile[,] LoadSchematic(string filename)
	{
		SchematicMetaTile[,] ret = null;
		using Stream st = CalamityMod.Instance.GetFileStream(filename, newFileStream: true);
		return ImportSchematic(st);
	}

	private static SchematicMetaTile[,] ImportSchematic(Stream fileInputStream)
	{
		byte[] header = fileInputStream.ReadBytes(4);
		bool isTML13Schematic = true;
		bool isTML14Schematic = true;
		bool isInfernumSchematic = true;
		bool isTML144Schematic = true;
		for (int i = 0; i < SchematicMagicNumberHeader_TML14.Length; i++)
		{
			if (header[i] != SchematicMagicNumberHeader_TML13[i])
			{
				isTML13Schematic = false;
			}
			if (header[i] != SchematicMagicNumberHeader_TML14[i])
			{
				isTML14Schematic = false;
			}
			if (header[i] != SchematicMagicNumberHeader_Infernum14[i])
			{
				isInfernumSchematic = false;
			}
			if (header[i] != SchematicMagicNumberHeader_TML144[i])
			{
				isTML144Schematic = false;
			}
		}
		if (!isTML13Schematic && !isTML14Schematic && !isInfernumSchematic && !isTML144Schematic)
		{
			throw new InvalidDataException("Provided file is not a valid Calamity Schematic. The magic number signature is invalid.");
		}
		if (isTML13Schematic)
		{
			CalamityMod.Log.Error((object)"An attempt was made to load a valid Calamity Schematic for TML 1.3. These files cannot be translated into TML 1.4. The schematic will show up empty.");
			return new SchematicMetaTile[0, 0];
		}
		ushort TileIDCount = (ushort)((isTML14Schematic | isInfernumSchematic) ? 625 : TileID.Count);
		ushort WallIDCount = (ushort)((isTML14Schematic | isInfernumSchematic) ? 316 : WallID.Count);
		bool useFourByteLookupIndices = isInfernumSchematic | isTML144Schematic;
		bool compression = false;
		if (header[3] == 192)
		{
			compression = true;
		}
		else if (header[3] != 0)
		{
			throw new InvalidDataException("Provided file is not a valid Calamity Schematic. The file is not properly marked as compressed or uncompressed.");
		}
		byte[] buffer;
		using (MemoryStream stream = new MemoryStream(16384))
		{
			if (compression)
			{
				using GZipStream gz = new GZipStream(fileInputStream, CompressionMode.Decompress);
				gz.CopyTo(stream);
			}
			else
			{
				fileInputStream.CopyTo(stream);
			}
			buffer = stream.ToArray();
		}
		using MemoryStream bufferStream = new MemoryStream(buffer, writable: false);
		using BinaryReader reader = new BinaryReader(bufferStream, Encoding.UTF8);
		uint numModTileNames = ((!useFourByteLookupIndices) ? reader.ReadUInt16() : reader.ReadUInt32());
		string[] modTileNames = new string[numModTileNames];
		for (int j = 0; j < numModTileNames; j++)
		{
			modTileNames[j] = reader.ReadString();
		}
		uint numModWallNames = ((!useFourByteLookupIndices) ? reader.ReadUInt16() : reader.ReadUInt32());
		string[] modWallNames = new string[numModWallNames];
		for (int k = 0; k < numModWallNames; k++)
		{
			modWallNames[k] = reader.ReadString();
		}
		uint numUniqueTiles = ((!useFourByteLookupIndices) ? reader.ReadUInt16() : reader.ReadUInt32());
		SchematicMetaTile[] uniqueTiles = new SchematicMetaTile[numUniqueTiles];
		for (int l = 0; l < numUniqueTiles; l++)
		{
			SchematicMetaTile smt = reader.ReadSchematicMetaTile(isTML144Schematic);
			ReplaceMetaIndicesWithLoadedIDs(ref smt, modTileNames, modWallNames, TileIDCount, WallIDCount);
			uniqueTiles[l] = smt;
		}
		ushort tileWidth = reader.ReadUInt16();
		ushort tileHeight = reader.ReadUInt16();
		SchematicMetaTile[,] ret = new SchematicMetaTile[tileWidth, tileHeight];
		for (ushort y = 0; y < tileHeight; y++)
		{
			for (ushort x = 0; x < tileWidth; x++)
			{
				uint tileIndex = ((!useFourByteLookupIndices) ? reader.ReadUInt16() : reader.ReadUInt32());
				ret[x, y] = uniqueTiles[tileIndex];
			}
		}
		return ret;
	}
}
