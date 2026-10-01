using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CalamityMod.DataStructures;

public class BezierCurve
{
	public Vector2[] ControlPoints;

	public BezierCurve(params Vector2[] controls)
	{
		ControlPoints = controls;
	}

	public Vector2 Evaluate(float interpolant)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return PrivateEvaluate(ControlPoints, MathHelper.Clamp(interpolant, 0f, 1f));
	}

	public List<Vector2> GetPoints(int totalPoints)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		float perStep = 1f / (float)totalPoints;
		List<Vector2> points = new List<Vector2>();
		for (float step = 0f; step <= 1f; step += perStep)
		{
			points.Add(Evaluate(step));
		}
		return points;
	}

	private Vector2 PrivateEvaluate(Vector2[] points, float T)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		while (points.Length > 2)
		{
			Vector2[] nextPoints = (Vector2[])(object)new Vector2[points.Length - 1];
			for (int k = 0; k < points.Length - 1; k++)
			{
				nextPoints[k] = Vector2.Lerp(points[k], points[k + 1], T);
			}
			points = nextPoints;
		}
		if (points.Length <= 1)
		{
			return Vector2.Zero;
		}
		return Vector2.Lerp(points[0], points[1], T);
	}
}
