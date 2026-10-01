using System.Collections.Generic;

namespace CalamityMod.Schematics;

public struct SchematicData(int width, int height)
{
	private const int DefaultUniqueTileCount = 1024;

	public const int MaxUniqueTileCount = 1048576;

	private const int DefaultModTileCount = 256;

	private const int DefaultModWallCount = 32;

	public readonly IList<SchematicMetaTile> uniqueTiles = new List<SchematicMetaTile>(1024) { default(SchematicMetaTile) };

	public readonly IList<string> modTileNames = new List<string>(256);

	public readonly IList<string> modWallNames = new List<string>(32);

	public readonly uint[,] areaIndices = new uint[width, height];
}
