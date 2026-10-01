using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public static class CustomShapes
{
	public class DistortedCircle : GenShape
	{
		private readonly int baseRadius;

		private readonly float distortionFactor;

		public DistortedCircle(int radius, float distortionFactor)
		{
			baseRadius = radius;
			this.distortionFactor = distortionFactor;
		}

		public override bool Perform(Point origin, GenAction action)
		{
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			float offsetAngle = WorldGen.genRand.NextFloat(-10f, 10f);
			for (float angle = 0f; angle < (float)Math.PI * 2f; angle += 0.026389377f)
			{
				float distortionQuantity = CalamityUtils.AperiodicSin(angle, offsetAngle, (float)Math.PI / 2f, (float)Math.E / 2f) * distortionFactor;
				int currentRadius = (int)((float)baseRadius - distortionQuantity * (float)baseRadius);
				if (currentRadius <= 0)
				{
					continue;
				}
				int horizontalOffset = (int)(Math.Cos(angle) * (double)currentRadius);
				int verticalOffset = (int)(Math.Sin(angle) * (double)currentRadius);
				for (int dx = 0; dx != horizontalOffset; dx += Math.Sign(horizontalOffset))
				{
					for (int dy = 0; dy != verticalOffset; dy += Math.Sign(verticalOffset))
					{
						UnitApply(action, origin, origin.X + dx, origin.Y + dy);
					}
				}
			}
			return true;
		}
	}
}
