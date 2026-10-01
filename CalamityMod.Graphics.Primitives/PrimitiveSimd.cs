using System;
using System.Numerics;

namespace CalamityMod.Graphics.Primitives;

internal static class PrimitiveSimd
{
	private static readonly Vector<int> LaneOffsets = CreateLaneOffsets();

	public static void FillSequentialIndices(Span<short> indices)
	{
		int i = 0;
		if (Vector.IsHardwareAccelerated)
		{
			int width = Vector<int>.Count;
			Span<int> span = ((width > 16) ? ((Span<int>)new int[width]) : stackalloc int[width]);
			Span<int> temp = span;
			for (; i <= indices.Length - width; i += width)
			{
				(LaneOffsets + new Vector<int>(i)).CopyTo(temp);
				for (int lane = 0; lane < width; lane++)
				{
					indices[i + lane] = (short)temp[lane];
				}
			}
		}
		for (; i < indices.Length; i++)
		{
			indices[i] = (short)i;
		}
	}

	private static Vector<int> CreateLaneOffsets()
	{
		int width = Vector<int>.Count;
		Span<int> lanes = stackalloc int[width];
		for (int i = 0; i < width; i++)
		{
			lanes[i] = i;
		}
		return new Vector<int>(lanes);
	}
}
