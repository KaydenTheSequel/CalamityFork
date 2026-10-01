using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.DataStructures;

public class VerletSimulatedSegment
{
	public Vector2 position;

	public Vector2 oldPosition;

	public bool locked;

	public VerletSimulatedSegment(Vector2 _position, bool _locked = false)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		position = _position;
		oldPosition = _position;
		locked = _locked;
	}

	public static List<VerletSimulatedSegment> SimpleSimulation(List<VerletSimulatedSegment> segments, float segmentDistance, int loops = 10, float gravity = 0.3f)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		foreach (VerletSimulatedSegment segment in segments)
		{
			if (!segment.locked)
			{
				Vector2 positionBeforeUpdate = segment.position;
				segment.position += segment.position - segment.oldPosition;
				segment.position += Vector2.UnitY * gravity;
				segment.oldPosition = positionBeforeUpdate;
			}
		}
		int segmentCount = segments.Count;
		for (int k = 0; k < loops; k++)
		{
			for (int j = 0; j < segmentCount - 1; j++)
			{
				VerletSimulatedSegment pointA = segments[j];
				VerletSimulatedSegment pointB = segments[j + 1];
				Vector2 segmentCenter = (pointA.position + pointB.position) / 2f;
				Vector2 segmentDirection = (pointA.position - pointB.position).SafeNormalize(Vector2.UnitY);
				if (!pointA.locked)
				{
					pointA.position = segmentCenter + segmentDirection * segmentDistance / 2f;
				}
				if (!pointB.locked)
				{
					pointB.position = segmentCenter - segmentDirection * segmentDistance / 2f;
				}
				segments[j] = pointA;
				segments[j + 1] = pointB;
			}
		}
		return segments;
	}
}
