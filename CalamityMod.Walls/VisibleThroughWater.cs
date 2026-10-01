using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

internal static class VisibleThroughWater
{
	[Autoload(true, Side = ModSide.Client)]
	private sealed class VisibleThroughWaterSystem : ModSystem
	{
		public override void AddRecipes()
		{
			InitializeWaterMapEntryLookups();
		}
	}

	public const float WaterTransparency = 0.5f;

	public static readonly Color WaterColor;

	public static void AddMapEntryWithWaterVisibility(this IVisibleThroughWater visibleThroughWater, Color baseColor, LocalizedText text = null)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		AssertIsWall(visibleThroughWater, out var wall);
		wall.AddMapEntry(baseColor, text);
		wall.AddMapEntry(Color.Lerp(baseColor, WaterColor, 0.5f), text);
	}

	public static void InitializeWaterMapEntryLookups()
	{
		foreach (IVisibleThroughWater item in CalamityMod.Instance.GetContent<IVisibleThroughWater>())
		{
			AssertIsWall(item, out var wall);
			item.WaterMapEntry = MapHelper.wallLookup[wall.Type] + 1;
		}
	}

	[DebuggerStepThrough]
	[StackTraceHidden]
	private static void AssertIsWall(IVisibleThroughWater visibleThroughWater, out ModWall wall)
	{
		if (!(visibleThroughWater is ModWall theWall))
		{
			throw new InvalidCastException($"{"IVisibleThroughWater"} is implemented on type {visibleThroughWater.GetType().FullName}; said type should inherit from {"ModWall"}");
		}
		wall = theWall;
	}

	static VisibleThroughWater()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		WaterColor = new Color(9, 61, 191);
	}
}
