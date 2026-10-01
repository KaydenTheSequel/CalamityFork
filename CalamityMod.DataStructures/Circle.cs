using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.DataStructures;

public struct Circle
{
	public float Radius;

	public Vector2 Center;

	public Circle(Vector2 center, float radius)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Center = center;
		Radius = radius;
	}

	private Vector2 RandomPointUnitCircle()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		return Main.rand.NextVector2Unit() * (float)Math.Sqrt(Main.rand.NextDouble());
	}

	public Vector2 RandomPointInCircle()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return Center + RandomPointUnitCircle() * Radius;
	}

	public Vector2 RandomPointOnCircleEdge()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Vector2 v = RandomPointUnitCircle();
		((Vector2)(ref v)).Normalize();
		return Center + v * Radius;
	}
}
