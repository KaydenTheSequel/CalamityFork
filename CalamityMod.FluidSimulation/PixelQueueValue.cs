using Microsoft.Xna.Framework;

namespace CalamityMod.FluidSimulation;

public struct PixelQueueValue
{
	public Vector2 Position;

	public Vector4 Value;

	public PixelQueueValue(Vector2 p, Color v)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		Position = p;
		Value = ((Color)(ref v)).ToVector4();
	}

	public PixelQueueValue(Vector2 p, Vector4 v)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		Position = p;
		Value = v;
	}
}
