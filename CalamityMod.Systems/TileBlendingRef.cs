namespace CalamityMod.Systems;

public struct TileBlendingRef(ushort sheetIdx, byte blendData)
{
	public ushort SheetIndex = sheetIdx;

	public byte BlendData = blendData;
}
