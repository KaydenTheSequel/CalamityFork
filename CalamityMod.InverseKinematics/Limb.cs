using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.InverseKinematics;

public class Limb
{
	public double Rotation;

	public double Length;

	public Vector2 ConnectPoint;

	public Vector2 EndPoint
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			return ConnectPoint + ((float)Rotation).ToRotationVector2() * (float)Length;
		}
	}

	public Limb(float rotation, float length)
	{
		Rotation = rotation;
		Length = length;
	}
}
