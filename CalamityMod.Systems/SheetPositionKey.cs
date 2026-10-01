namespace CalamityMod.Systems;

public readonly struct SheetPositionKey(BlendSideFlags blendSides, byte randomFrameIndex)
{
	public ushort Key { get; init; } = (ushort)((uint)blendSides + (uint)(randomFrameIndex * 256));

	public BlendSideFlags BlendSides => (BlendSideFlags)(Key % 256);

	public byte RandomFrameIndex => (byte)(Key / 256);

	public static implicit operator int(SheetPositionKey key)
	{
		return key.Key;
	}
}
