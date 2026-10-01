using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics;

namespace CalamityMod.Waters;

internal static class WaterStyleCommon
{
	public static void ModifySulphuricWaterColor(int x, int y, ref VertexColors initialColor, bool isSlope)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		Color cleanWaterColor;
		Point closestSafeZonePoint;
		float lerpAmt;
		if (SulphuricWaterSafeZoneSystem.NearbySafeTiles.Count >= 1)
		{
			cleanWaterColor = new Color(10, 62, 193);
			KeyValuePair<Point, float> closestSafeZone = SulphuricWaterSafeZoneSystem.NearbySafeTiles.OrderBy(delegate(KeyValuePair<Point, float> t)
			{
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				//IL_0007: Unknown result type (might be due to invalid IL or missing references)
				//IL_001a: Unknown result type (might be due to invalid IL or missing references)
				return t.Key.ToVector2().DistanceSQ(new Vector2((float)x, (float)y));
			}).First();
			closestSafeZonePoint = closestSafeZone.Key;
			float closestSafeZoneAmt = closestSafeZone.Value;
			lerpAmt = (1f - closestSafeZoneAmt) * 21f;
			ModifyColor(new Vector2((float)x - 0.5f, (float)y - 0.5f), ref initialColor.TopLeftColor);
			ModifyColor(new Vector2((float)x + 0.5f, (float)y - 0.5f), ref initialColor.TopRightColor);
			ModifyColor(new Vector2((float)x - 0.5f, (float)y + 0.5f), ref initialColor.BottomLeftColor);
			ModifyColor(new Vector2((float)x + 0.5f, (float)y + 0.5f), ref initialColor.BottomRightColor);
		}
		ModifyTransparentWaterColor(x, y, ref initialColor, isSlope);
		void ModifyColor(Vector2 point, ref Color vertexColor)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			float distanceToClosest = point.Distance(closestSafeZonePoint.ToVector2());
			float acidicWaterInterpolant = Utils.GetLerpValue(12f, 20.5f, distanceToClosest + lerpAmt, clamped: true);
			vertexColor = Color.Lerp(cleanWaterColor, vertexColor, acidicWaterInterpolant);
		}
	}

	public static void ModifyTransparentWaterColor(int x, int y, ref VertexColors initialColor, bool isSlope)
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (isSlope)
		{
			ref Color topLeftColor = ref initialColor.TopLeftColor;
			topLeftColor *= 1f / 3f;
			ref Color topRightColor = ref initialColor.TopRightColor;
			topRightColor *= 1f / 3f;
			ref Color bottomLeftColor = ref initialColor.BottomLeftColor;
			bottomLeftColor *= 1f / 3f;
			ref Color bottomRightColor = ref initialColor.BottomRightColor;
			bottomRightColor *= 1f / 3f;
		}
		else
		{
			ref Color topLeftColor2 = ref initialColor.TopLeftColor;
			topLeftColor2 *= 0.4f;
			ref Color topRightColor2 = ref initialColor.TopRightColor;
			topRightColor2 *= 0.4f;
			ref Color bottomLeftColor2 = ref initialColor.BottomLeftColor;
			bottomLeftColor2 *= 0.4f;
			ref Color bottomRightColor2 = ref initialColor.BottomRightColor;
			bottomRightColor2 *= 0.4f;
		}
	}

	public static void ModifySunkenSeaWaterLight(int x, int y, Vector3 waterColor, ref float r, ref float g, ref float b)
	{
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		float tick = (float)Main.timeForVisualEffects;
		float brightness = MathHelper.Clamp(0.07f, 0f, 0.07f);
		float waveScale1 = tick * 0.028f;
		float waveScale2 = tick * 0.1f;
		int yScale = -y / 2;
		int xScale = x / 15;
		float wave1 = tick * 0.024f * -50f + (float)((-x / 30 + y / 30) * 25);
		float wave2 = waveScale2 * -10f + (float)((-xScale + yScale) * 45);
		float wave3 = waveScale1 * -100f + (float)((x / 7 + y / 50) * 25);
		float wave4 = tick * 0.15f * 10f + (float)((x / 3 + yScale) * 45);
		float wave5 = waveScale1 * -70f + (float)((-x / 25 + -y / 25) * 20);
		float wave6 = waveScale2 * -10f + (float)((xScale + yScale) * 45);
		float bigwave = tick * 0.01f * -70f + (float)((-x / 2 + -y / 40) * 5);
		float wave1angle = 0.55f + 0.45f * MathF.Sin(MathHelper.ToRadians(wave1));
		float wave2angle = 0.55f + 0.45f * MathF.Sin(MathHelper.ToRadians(wave2));
		float wave3angle = 0.55f + 0.45f * MathF.Sin(MathHelper.ToRadians(wave3));
		float wave4angle = 0.55f + 0.45f * MathF.Sin(MathHelper.ToRadians(wave4));
		float wave5angle = 0.55f + 0.45f * MathF.Sin(MathHelper.ToRadians(wave5));
		float wave6angle = 0.55f + 0.45f * MathF.Sin(MathHelper.ToRadians(wave6));
		float bigwaveangle = 0.55f + 0.8f * MathF.Sin(MathHelper.ToRadians(bigwave));
		float sumofwave = 0.07f + wave1angle + wave2angle + wave3angle + wave4angle + wave5angle + wave6angle + bigwaveangle;
		r = MathHelper.Lerp(r, waterColor.X, sumofwave) * brightness;
		g = MathHelper.Lerp(g, waterColor.Y, sumofwave) * brightness;
		b = MathHelper.Lerp(b, waterColor.Z, sumofwave) * brightness;
	}
}
