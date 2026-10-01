using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Physics;

public readonly struct RopeHandle
{
	private readonly int Identifier;

	private Rope Rope => ModContent.GetInstance<RopeManagerSystem>().Ropes[Identifier];

	public IEnumerable<Vector2> Positions => Rope.SegmentPositions;

	public int SegmentCount => Rope.Segments.Length;

	public ref Vector2 Start => ref Rope.Segments[0].Position;

	public ref Vector2 End => ref Rope.Segments[^1].Position;

	public Vector2 Gravity
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return Rope.Gravity;
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			Rope.Gravity = value;
		}
	}

	internal RopeHandle(int identifier)
	{
		Identifier = identifier;
	}

	public void Settle()
	{
		for (int i = 0; i < 20; i++)
		{
			Rope.Update();
		}
	}

	public void Dispose()
	{
		int chunkIndex = Identifier / 64;
		int bitIndex = Identifier % 64;
		ModContent.GetInstance<RopeManagerSystem>().ToggleActivityIndex(chunkIndex, bitIndex);
	}
}
