using Microsoft.Xna.Framework;

namespace CalamityMod.Physics;

public struct RopeSegment
{
	public Vector2 Position;

	public Vector2 OldPosition;

	public bool FixedInPlace;

	public RopeSegment(Vector2 position)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		FixedInPlace = false;
		Position = position;
		OldPosition = position;
	}
}
