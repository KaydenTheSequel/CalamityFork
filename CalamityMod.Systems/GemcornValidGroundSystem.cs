using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Systems;

internal sealed class GemcornValidGroundSystem : ModSystem
{
	public int[] ValidGemcornGrounds = Array.Empty<int>();

	public static TileObjectData GemSaplingData => TileObjectData.GetTileData(590, 0);

	public override void PostSetupContent()
	{
		ValidGemcornGrounds = (from tile in CalamityMod.Instance.GetContent<ModTile>()
			where WorldGen.GemTreeGroundTest(tile.Type)
			select tile).Select((Func<ModTile, int>)((ModTile tile) => tile.Type)).ToArray();
		TileObjectData gemSaplingData = GemSaplingData;
		int[] anchorValidTiles = gemSaplingData.AnchorValidTiles;
		int[] validGemcornGrounds = ValidGemcornGrounds;
		int num = 0;
		int[] array = new int[anchorValidTiles.Length + validGemcornGrounds.Length];
		ReadOnlySpan<int> readOnlySpan = new ReadOnlySpan<int>(anchorValidTiles);
		readOnlySpan.CopyTo(new Span<int>(array).Slice(num, readOnlySpan.Length));
		num += readOnlySpan.Length;
		ReadOnlySpan<int> readOnlySpan2 = new ReadOnlySpan<int>(validGemcornGrounds);
		readOnlySpan2.CopyTo(new Span<int>(array).Slice(num, readOnlySpan2.Length));
		num += readOnlySpan2.Length;
		gemSaplingData.AnchorValidTiles = array;
	}

	public override void Unload()
	{
		TileObjectData gemSaplingData = GemSaplingData;
		gemSaplingData.AnchorValidTiles = gemSaplingData.AnchorValidTiles.Where((int type) => !Enumerable.Contains(ValidGemcornGrounds, type)).ToArray();
	}
}
