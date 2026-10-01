using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.FluidSimulation;

public class FluidFieldManager : ModSystem
{
	internal static List<FluidField> Fields = new List<FluidField>();

	public static FluidField CreateField(int size, float scale, float viscosity, float diffusionFactor, float dissipationFactor)
	{
		FluidField field = new FluidField(size, scale, viscosity, diffusionFactor, dissipationFactor);
		Fields.Add(field);
		return field;
	}

	public static void AdjustSizeRelativeToGraphicsQuality(ref int size, int min = 245, int max = 530)
	{
		float graphicsQuality = MathHelper.Clamp(Main.gfxQuality, 0f, 1f);
		if (Main.qaStyle == 0)
		{
			graphicsQuality = 0.5f;
		}
		graphicsQuality = (float)Math.Pow(graphicsQuality, 2.3);
		int lowerBound = (int)MathHelper.Lerp((float)size, (float)min, 1f - graphicsQuality);
		int upperBound = (int)MathHelper.Lerp((float)size, (float)max, graphicsQuality);
		size = (upperBound + lowerBound) / 2;
	}

	public static void Update()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Vector2 old = Main.GameViewMatrix.Zoom;
		Main.GameViewMatrix.Zoom = Vector2.One;
		foreach (FluidField field in Fields)
		{
			field.Update();
		}
		Main.GameViewMatrix.Zoom = old;
	}

	public override void OnModUnload()
	{
		Main.QueueMainThreadAction(delegate
		{
			while (Fields.Count > 0)
			{
				Fields[0].Dispose();
			}
		});
	}
}
