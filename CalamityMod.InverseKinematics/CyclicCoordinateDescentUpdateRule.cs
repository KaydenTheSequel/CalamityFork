using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.InverseKinematics;

public class CyclicCoordinateDescentUpdateRule : IInverseKinematicsUpdateRule
{
	public float AngularOffsetAcceleration;

	public float AngularDeviationLenience;

	public CyclicCoordinateDescentUpdateRule(float angularOffsetAcceleration, float angularDeviationLenience)
	{
		AngularOffsetAcceleration = angularOffsetAcceleration;
		AngularDeviationLenience = angularDeviationLenience;
	}

	public void Update(LimbCollection limbs, Vector2 destination)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		float distanceFromStart = Vector2.Distance(destination, limbs.ConnectPoint);
		float distanceFromEnd = Vector2.Distance(destination, limbs.EndPoint);
		float slowdownInterpolant = Utils.GetLerpValue(8f, 40f, distanceFromEnd, clamped: true);
		slowdownInterpolant *= Utils.GetLerpValue(8f, 40f, distanceFromStart, clamped: true);
		Vector2 originalEndPoint = limbs.EndPoint;
		int i = limbs.Limbs.Length - 1;
		while (i >= 0)
		{
			Vector2 v = originalEndPoint - limbs.Limbs[i].ConnectPoint;
			Vector2 currentToDestinationOffset = destination - limbs.Limbs[i].ConnectPoint;
			Vector2 perpendicularDirection = currentToDestinationOffset.RotatedBy(1.5707963705062866);
			float angularOffset = v.AngleBetween(currentToDestinationOffset) * (float)Math.Sqrt(((float)i + 1f) / (float)limbs.Limbs.Length);
			float leftAngularOffset = v.AngleBetween(currentToDestinationOffset - perpendicularDirection);
			float rightAngularOffset = v.AngleBetween(currentToDestinationOffset + perpendicularDirection);
			if (leftAngularOffset > rightAngularOffset)
			{
				angularOffset *= -1f;
			}
			if (!float.IsNaN(angularOffset))
			{
				limbs.Limbs[i].Rotation += angularOffset * slowdownInterpolant * AngularOffsetAcceleration;
				if (i > 0)
				{
					float behindRotation = (float)limbs.Limbs[i - 1].Rotation;
					limbs.Limbs[i].Rotation = MathHelper.Clamp((float)limbs.Limbs[i].Rotation, behindRotation - AngularDeviationLenience, behindRotation + AngularDeviationLenience);
				}
				i--;
				continue;
			}
			break;
		}
	}
}
