using System;
using System.Numerics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Physics;

public sealed class RopeManagerSystem : ModSystem
{
	internal readonly Rope[] Ropes = new Rope[2048];

	internal readonly ulong[] ActivityBitChunks = new ulong[(int)Math.Ceiling(32.0)];

	public const int MaxRopeCount = 2048;

	public const int BitsPerChunk = 64;

	public override void ClearWorld()
	{
		for (int i = 0; i < ActivityBitChunks.Length; i++)
		{
			ActivityBitChunks[i] = 0uL;
		}
	}

	public override void PostUpdateWorld()
	{
		for (int i = 0; i < Ropes.Length; i++)
		{
			int bitIndex = i % 64;
			if (((ActivityBitChunks[i / 64] >> bitIndex) & 1) == 1)
			{
				Ropes[i].Update();
			}
		}
	}

	internal void ToggleActivityIndex(int chunkIndex, int bitIndex)
	{
		ActivityBitChunks[chunkIndex] ^= (ulong)(1L << bitIndex);
	}

	private int? SelectFirstAvailableIndex()
	{
		for (int i = 0; i < ActivityBitChunks.Length; i++)
		{
			int offset = BitOperations.TrailingZeroCount(~ActivityBitChunks[i]);
			if (offset != 64)
			{
				return offset + i * 64;
			}
		}
		return null;
	}

	public RopeHandle? RequestNew(Vector2 start, Vector2 end, int segmentCount, float distancePerSegment, Vector2 gravity, RopeSettings settings, int constraintSteps = 10)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		int? index = SelectFirstAvailableIndex();
		if (!index.HasValue)
		{
			return null;
		}
		ToggleActivityIndex(index.Value / 64, index.Value % 64);
		Ropes[index.Value] = new Rope(start, end, segmentCount, distancePerSegment, gravity, settings, constraintSteps);
		return new RopeHandle(index.Value);
	}

	public static float CalculateSegmentLength(float ropeSpan, float sag, int iterations = 12)
	{
		float a = (float)CalamityUtils.IterativelySearchForRoot(initialGuess: sag, fx: (double x) => x * (Math.Cosh((double)ropeSpan / (x * 2.0)) - 1.0) - (double)sag, iterations: iterations);
		return MathF.Sinh(ropeSpan / a * 0.5f) * a * 2f;
	}
}
