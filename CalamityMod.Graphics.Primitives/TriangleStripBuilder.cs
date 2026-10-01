using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Graphics.Primitives;

public static class TriangleStripBuilder
{
	public static PooledPrimitiveMesh BuildStripPooled(IReadOnlyList<Vector3> path, float width, Color color, PrimitiveMeshCache? cache, Vector3? upHint = null, int smoothingSegments = 0, StripCapStyle startCap = StripCapStyle.None, StripCapStyle endCap = StripCapStyle.None, int capSegments = 8, StripJoinStyle joinStyle = StripJoinStyle.Perpendicular, bool textured = true, StripCurveType smoothingCurve = StripCurveType.CatmullRom, StripWidthAttenuation widthAttenuation = StripWidthAttenuation.None)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (width <= 0f)
		{
			throw new ArgumentOutOfRangeException("width", "Width must be positive.");
		}
		return BuildStripCorePooled(path, delegate
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return color;
		}, (float _) => width, (Vector3)(((_003F?)upHint) ?? Vector3.UnitZ), smoothingSegments, startCap, endCap, capSegments, joinStyle, textured, smoothingCurve, widthAttenuation, cache);
	}

	public static PooledPrimitiveMesh BuildStripPooled(IReadOnlyList<Vector3> path, float width, IReadOnlyList<Color> colors, PrimitiveMeshCache? cache, Vector3? upHint = null, int smoothingSegments = 0, StripCapStyle startCap = StripCapStyle.None, StripCapStyle endCap = StripCapStyle.None, int capSegments = 8, StripJoinStyle joinStyle = StripJoinStyle.Perpendicular, bool textured = true, StripCurveType smoothingCurve = StripCurveType.CatmullRom, StripWidthAttenuation widthAttenuation = StripWidthAttenuation.None)
	{
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (colors == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (colors.Count != path.Count)
		{
			throw new ArgumentException("Color count must match path length.", "colors");
		}
		if (width <= 0f)
		{
			throw new ArgumentOutOfRangeException("width", "Width must be positive.");
		}
		return BuildStripCorePooled(path, delegate(float progress)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			return SampleColor(colors, progress);
		}, (float _) => width, (Vector3)(((_003F?)upHint) ?? Vector3.UnitZ), smoothingSegments, startCap, endCap, capSegments, joinStyle, textured, smoothingCurve, widthAttenuation, cache);
	}

	public static PooledPrimitiveMesh BuildStripPooled(IReadOnlyList<Vector3> path, Func<float, float> widthFunc, Color color, PrimitiveMeshCache? cache, Func<float, float>? easing = null, Vector3? upHint = null, int smoothingSegments = 0, StripCapStyle startCap = StripCapStyle.None, StripCapStyle endCap = StripCapStyle.None, int capSegments = 8, StripJoinStyle joinStyle = StripJoinStyle.Perpendicular, bool textured = true, StripCurveType smoothingCurve = StripCurveType.CatmullRom, StripWidthAttenuation widthAttenuation = StripWidthAttenuation.None)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (widthFunc == null)
		{
			throw new ArgumentNullException("widthFunc");
		}
		return BuildStripCorePooled(path, delegate
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return color;
		}, (float progress) => EvaluateWidth(widthFunc, easing, progress), (Vector3)(((_003F?)upHint) ?? Vector3.UnitZ), smoothingSegments, startCap, endCap, capSegments, joinStyle, textured, smoothingCurve, widthAttenuation, cache);
	}

	public static PooledPrimitiveMesh BuildStripPooled(IReadOnlyList<Vector3> path, Func<float, float> widthFunc, IReadOnlyList<Color> colors, PrimitiveMeshCache? cache, Func<float, float>? easing = null, Vector3? upHint = null, int smoothingSegments = 0, StripCapStyle startCap = StripCapStyle.None, StripCapStyle endCap = StripCapStyle.None, int capSegments = 8, StripJoinStyle joinStyle = StripJoinStyle.Perpendicular, bool textured = true, StripCurveType smoothingCurve = StripCurveType.CatmullRom, StripWidthAttenuation widthAttenuation = StripWidthAttenuation.None)
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (colors == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (colors.Count != path.Count)
		{
			throw new ArgumentException("Color count must match path length.", "colors");
		}
		if (widthFunc == null)
		{
			throw new ArgumentNullException("widthFunc");
		}
		return BuildStripCorePooled(path, delegate(float progress)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			return SampleColor(colors, progress);
		}, (float progress) => EvaluateWidth(widthFunc, easing, progress), (Vector3)(((_003F?)upHint) ?? Vector3.UnitZ), smoothingSegments, startCap, endCap, capSegments, joinStyle, textured, smoothingCurve, widthAttenuation, cache);
	}

	public static PooledPrimitiveMesh BuildStripPooled(IReadOnlyList<Vector3> path, Func<float, float> widthFunc, Func<float, Color> colorFunc, PrimitiveMeshCache? cache, Func<float, float>? easing = null, Vector3? upHint = null, int smoothingSegments = 0, StripCapStyle startCap = StripCapStyle.None, StripCapStyle endCap = StripCapStyle.None, int capSegments = 8, StripJoinStyle joinStyle = StripJoinStyle.Perpendicular, bool textured = true, StripCurveType smoothingCurve = StripCurveType.CatmullRom, StripWidthAttenuation widthAttenuation = StripWidthAttenuation.None)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (widthFunc == null)
		{
			throw new ArgumentNullException("widthFunc");
		}
		if (colorFunc == null)
		{
			throw new ArgumentNullException("colorFunc");
		}
		return BuildStripCorePooled(path, delegate(float progress)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			return colorFunc(MathHelper.Clamp(progress, 0f, 1f));
		}, (float progress) => EvaluateWidth(widthFunc, easing, progress), (Vector3)(((_003F?)upHint) ?? Vector3.UnitZ), smoothingSegments, startCap, endCap, capSegments, joinStyle, textured, smoothingCurve, widthAttenuation, cache);
	}

	public static PrimitiveMesh BuildStrip(IReadOnlyList<Vector3> path, float width, Color color, Vector3? upHint = null, int smoothingSegments = 0, StripCapStyle startCap = StripCapStyle.None, StripCapStyle endCap = StripCapStyle.None, int capSegments = 8, StripJoinStyle joinStyle = StripJoinStyle.Perpendicular, bool textured = true, StripCurveType smoothingCurve = StripCurveType.CatmullRom, StripWidthAttenuation widthAttenuation = StripWidthAttenuation.None)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (width <= 0f)
		{
			throw new ArgumentOutOfRangeException("width", "Width must be positive.");
		}
		return BuildStripCore(path, delegate
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return color;
		}, (float _) => width, (Vector3)(((_003F?)upHint) ?? Vector3.UnitZ), smoothingSegments, startCap, endCap, capSegments, joinStyle, textured, smoothingCurve, widthAttenuation);
	}

	public static PrimitiveMesh BuildStrip(IReadOnlyList<Vector3> path, float width, IReadOnlyList<Color> colors, Vector3? upHint = null, int smoothingSegments = 0, StripCapStyle startCap = StripCapStyle.None, StripCapStyle endCap = StripCapStyle.None, int capSegments = 8, StripJoinStyle joinStyle = StripJoinStyle.Perpendicular, bool textured = true, StripCurveType smoothingCurve = StripCurveType.CatmullRom, StripWidthAttenuation widthAttenuation = StripWidthAttenuation.None)
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (colors == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (colors.Count != path.Count)
		{
			throw new ArgumentException("Color count must match path length.", "colors");
		}
		if (width <= 0f)
		{
			throw new ArgumentOutOfRangeException("width", "Width must be positive.");
		}
		return BuildStripCore(path, delegate(float progress)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			return SampleColor(colors, progress);
		}, (float _) => width, (Vector3)(((_003F?)upHint) ?? Vector3.UnitZ), smoothingSegments, startCap, endCap, capSegments, joinStyle, textured, smoothingCurve, widthAttenuation);
	}

	public static PrimitiveMesh BuildStrip(IReadOnlyList<Vector3> path, Func<float, float> widthFunc, Color color, Func<float, float>? easing = null, Vector3? upHint = null, int smoothingSegments = 0, StripCapStyle startCap = StripCapStyle.None, StripCapStyle endCap = StripCapStyle.None, int capSegments = 8, StripJoinStyle joinStyle = StripJoinStyle.Perpendicular, bool textured = true, StripCurveType smoothingCurve = StripCurveType.CatmullRom, StripWidthAttenuation widthAttenuation = StripWidthAttenuation.None)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (widthFunc == null)
		{
			throw new ArgumentNullException("widthFunc");
		}
		return BuildStripCore(path, delegate
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return color;
		}, (float progress) => EvaluateWidth(widthFunc, easing, progress), (Vector3)(((_003F?)upHint) ?? Vector3.UnitZ), smoothingSegments, startCap, endCap, capSegments, joinStyle, textured, smoothingCurve, widthAttenuation);
	}

	public static PrimitiveMesh BuildStrip(IReadOnlyList<Vector3> path, Func<float, float> widthFunc, IReadOnlyList<Color> colors, Func<float, float>? easing = null, Vector3? upHint = null, int smoothingSegments = 0, StripCapStyle startCap = StripCapStyle.None, StripCapStyle endCap = StripCapStyle.None, int capSegments = 8, StripJoinStyle joinStyle = StripJoinStyle.Perpendicular, bool textured = true, StripCurveType smoothingCurve = StripCurveType.CatmullRom, StripWidthAttenuation widthAttenuation = StripWidthAttenuation.None)
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (colors == null)
		{
			throw new ArgumentNullException("colors");
		}
		if (colors.Count != path.Count)
		{
			throw new ArgumentException("Color count must match path length.", "colors");
		}
		if (widthFunc == null)
		{
			throw new ArgumentNullException("widthFunc");
		}
		return BuildStripCore(path, delegate(float progress)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			return SampleColor(colors, progress);
		}, (float progress) => EvaluateWidth(widthFunc, easing, progress), (Vector3)(((_003F?)upHint) ?? Vector3.UnitZ), smoothingSegments, startCap, endCap, capSegments, joinStyle, textured, smoothingCurve, widthAttenuation);
	}

	public static PrimitiveMesh BuildStrip(IReadOnlyList<Vector3> path, Func<float, float> widthFunc, Func<float, Color> colorFunc, Func<float, float>? easing = null, Vector3? upHint = null, int smoothingSegments = 0, StripCapStyle startCap = StripCapStyle.None, StripCapStyle endCap = StripCapStyle.None, int capSegments = 8, StripJoinStyle joinStyle = StripJoinStyle.Perpendicular, bool textured = true, StripCurveType smoothingCurve = StripCurveType.CatmullRom, StripWidthAttenuation widthAttenuation = StripWidthAttenuation.None)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (widthFunc == null)
		{
			throw new ArgumentNullException("widthFunc");
		}
		if (colorFunc == null)
		{
			throw new ArgumentNullException("colorFunc");
		}
		return BuildStripCore(path, delegate(float progress)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			return colorFunc(MathHelper.Clamp(progress, 0f, 1f));
		}, (float progress) => EvaluateWidth(widthFunc, easing, progress), (Vector3)(((_003F?)upHint) ?? Vector3.UnitZ), smoothingSegments, startCap, endCap, capSegments, joinStyle, textured, smoothingCurve, widthAttenuation);
	}

	private static PrimitiveMesh BuildStripCore(IReadOnlyList<Vector3> path, Func<float, Color> colorResolver, Func<float, float> widthResolver, Vector3 up, int smoothingSegments, StripCapStyle startCap, StripCapStyle endCap, int capSegments, StripJoinStyle joinStyle, bool textured, StripCurveType smoothingCurve, StripWidthAttenuation widthAttenuation)
	{
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_072d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0739: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_077a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_061c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		if (path.Count < 2)
		{
			throw new ArgumentException("At least two points are required.", "path");
		}
		if (colorResolver == null)
		{
			throw new ArgumentNullException("colorResolver");
		}
		if (widthResolver == null)
		{
			throw new ArgumentNullException("widthResolver");
		}
		IReadOnlyList<Vector3> workingPath = RemoveDegenerates(MaybeSmoothPath(path, smoothingSegments, smoothingCurve));
		float[] progress = ComputeProgress(workingPath, out var _);
		VertexPositionColorTexture[] texturedVertices = (VertexPositionColorTexture[])(object)(textured ? new VertexPositionColorTexture[workingPath.Count * 2] : null);
		VertexPositionColor[] colorVertices = (VertexPositionColor[])(object)(textured ? null : new VertexPositionColor[workingPath.Count * 2]);
		if ((textured ? texturedVertices.Length : colorVertices.Length) > 32767)
		{
			throw new InvalidOperationException("Strip produced more vertices than supported by the index buffer.");
		}
		Vector3[] tangents = (Vector3[])(object)new Vector3[workingPath.Count];
		Vector3[] rights = (Vector3[])(object)new Vector3[workingPath.Count];
		Vector3[] centers = (Vector3[])(object)new Vector3[workingPath.Count];
		Color[] sectionColors = (Color[])(object)new Color[workingPath.Count];
		float[] halfWidths = new float[workingPath.Count];
		Vector3 upNormalized = ((((Vector3)(ref up)).LengthSquared() < 1E-06f) ? Vector3.UnitZ : Vector3.Normalize(up));
		int segmentCount = workingPath.Count - 1;
		Vector3[] segmentDirs = (Vector3[])(object)new Vector3[segmentCount];
		Vector3[] segmentRights = (Vector3[])(object)new Vector3[segmentCount];
		for (int i = 0; i < segmentCount; i++)
		{
			Vector3 dir = workingPath[i + 1] - workingPath[i];
			if (((Vector3)(ref dir)).LengthSquared() < 1E-06f)
			{
				dir = ((i > 0) ? segmentDirs[i - 1] : Vector3.UnitY);
			}
			((Vector3)(ref dir)).Normalize();
			Vector3 right = Vector3.Cross(upNormalized, dir);
			if (((Vector3)(ref right)).LengthSquared() < 1E-06f)
			{
				right = FindPerpendicular(dir);
			}
			else
			{
				((Vector3)(ref right)).Normalize();
			}
			segmentDirs[i] = dir;
			segmentRights[i] = right;
		}
		Vector3 lastTangent = segmentDirs[0];
		for (int j = 0; j < workingPath.Count; j++)
		{
			Vector3 val = workingPath[j];
			float t = progress[j];
			float num = Math.Max(0f, widthResolver(MathHelper.Clamp(t, 0f, 1f)));
			Vector3 prevDir = segmentDirs[Math.Max(j - 1, 0)];
			Vector3 nextDir = segmentDirs[Math.Min(j, segmentCount - 1)];
			Vector3 tangent;
			if (j == 0)
			{
				tangent = nextDir;
			}
			else if (j == segmentCount)
			{
				tangent = prevDir;
			}
			else
			{
				tangent = prevDir + nextDir;
				if (((Vector3)(ref tangent)).LengthSquared() < 1E-06f)
				{
					tangent = nextDir;
				}
				else
				{
					((Vector3)(ref tangent)).Normalize();
				}
			}
			float width = num;
			if (j > 0 && widthAttenuation == StripWidthAttenuation.ContinuitySquared)
			{
				float continuity = MathHelper.Clamp((Vector3.Dot(lastTangent, tangent) + 1f) * 0.5f, 0f, 1f);
				width *= continuity * continuity;
			}
			float halfWidth = width * 0.5f;
			Vector3 prevRight = segmentRights[Math.Max(j - 1, 0)];
			Vector3 nextRight = segmentRights[Math.Min(j, segmentCount - 1)];
			Vector3 rightOffset;
			Vector3 leftOffset;
			if (joinStyle == StripJoinStyle.Miter)
			{
				rightOffset = ComputeMiterOffset(prevRight, nextRight, j == 0, j == segmentCount, halfWidth);
				leftOffset = ComputeMiterOffset(-prevRight, -nextRight, j == 0, j == segmentCount, halfWidth);
			}
			else
			{
				Vector3 joinNormal;
				if (j == 0)
				{
					joinNormal = nextRight;
				}
				else if (j == segmentCount)
				{
					joinNormal = prevRight;
				}
				else
				{
					joinNormal = prevRight + nextRight;
					if (((Vector3)(ref joinNormal)).LengthSquared() < 1E-06f)
					{
						joinNormal = nextRight;
					}
				}
				if (((Vector3)(ref joinNormal)).LengthSquared() < 1E-06f)
				{
					joinNormal = Vector3.UnitY;
				}
				((Vector3)(ref joinNormal)).Normalize();
				rightOffset = joinNormal * halfWidth;
				leftOffset = -joinNormal * halfWidth;
			}
			Vector3 leftPos = val + leftOffset;
			Vector3 rightPos = val + rightOffset;
			Vector3 chord = rightPos - leftPos;
			float chordLength = ((Vector3)(ref chord)).Length();
			Vector3 lateralDir = ((chordLength > 1E-06f) ? (chord / chordLength) : ((((Vector3)(ref nextRight)).LengthSquared() > 1E-06f) ? nextRight : Vector3.UnitX));
			Vector3 crossCenter = (leftPos + rightPos) * 0.5f;
			float effectiveHalfWidth = chordLength * 0.5f;
			float uCoord = progress[j];
			Color color = colorResolver(t);
			if (textured)
			{
				texturedVertices[j * 2] = CreateEdgeVertex(leftPos, color, uCoord, isLeft: true);
				texturedVertices[j * 2 + 1] = CreateEdgeVertex(rightPos, color, uCoord, isLeft: false);
			}
			else
			{
				colorVertices[j * 2] = new VertexPositionColor(leftPos, color);
				colorVertices[j * 2 + 1] = new VertexPositionColor(rightPos, color);
			}
			Vector3 rightForCap = ((((Vector3)(ref lateralDir)).LengthSquared() > 1E-06f) ? lateralDir : Vector3.UnitX);
			tangents[j] = tangent;
			rights[j] = Vector3.Normalize(rightForCap);
			centers[j] = crossCenter;
			sectionColors[j] = color;
			halfWidths[j] = effectiveHalfWidth;
			lastTangent = tangent;
		}
		if (startCap == StripCapStyle.None && endCap == StripCapStyle.None)
		{
			short[] stripIndices = new short[textured ? texturedVertices.Length : colorVertices.Length];
			PrimitiveSimd.FillSequentialIndices(stripIndices.AsSpan());
			if (!textured)
			{
				return new PrimitiveMesh(colorVertices, stripIndices, (PrimitiveType)1);
			}
			return new PrimitiveMesh(texturedVertices, stripIndices, (PrimitiveType)1);
		}
		if (textured)
		{
			List<VertexPositionColorTexture> vertexList = new List<VertexPositionColorTexture>(texturedVertices);
			List<short> indexList = new List<short>();
			AppendStripAsTriangles(indexList, vertexList.Count);
			int startLeftIndex = 0;
			int startRightIndex = 1;
			int endLeftIndex = texturedVertices.Length - 2;
			int endRightIndex = texturedVertices.Length - 1;
			int capSteps = Math.Max(2, capSegments);
			float startU = progress[0];
			float endU = progress[^1];
			switch (startCap)
			{
			case StripCapStyle.Triangle:
				AddTriangleCap(vertexList, indexList, centers[0], tangents[0], rights[0], halfWidths[0], sectionColors[0], startU, startLeftIndex, startRightIndex, isStart: true);
				break;
			case StripCapStyle.HalfCircle:
				AddHalfCircleCap(vertexList, indexList, centers[0], tangents[0], rights[0], halfWidths[0], sectionColors[0], startU, startLeftIndex, startRightIndex, isStart: true, capSteps);
				break;
			}
			switch (endCap)
			{
			case StripCapStyle.Triangle:
				AddTriangleCap(vertexList, indexList, centers[^1], tangents[^1], rights[^1], halfWidths[^1], sectionColors[^1], endU, endLeftIndex, endRightIndex, isStart: false);
				break;
			case StripCapStyle.HalfCircle:
				AddHalfCircleCap(vertexList, indexList, centers[^1], tangents[^1], rights[^1], halfWidths[^1], sectionColors[^1], endU, endLeftIndex, endRightIndex, isStart: false, capSteps);
				break;
			}
			return new PrimitiveMesh(vertexList.ToArray(), indexList.ToArray(), (PrimitiveType)0);
		}
		List<VertexPositionColor> vertexList2 = new List<VertexPositionColor>(colorVertices);
		List<short> indexList2 = new List<short>();
		AppendStripAsTriangles(indexList2, vertexList2.Count);
		int startLeftIndex2 = 0;
		int startRightIndex2 = 1;
		int endLeftIndex2 = colorVertices.Length - 2;
		int endRightIndex2 = colorVertices.Length - 1;
		int capSteps2 = Math.Max(2, capSegments);
		switch (startCap)
		{
		case StripCapStyle.Triangle:
			AddTriangleCap(vertexList2, indexList2, centers[0], tangents[0], rights[0], halfWidths[0], sectionColors[0], startLeftIndex2, startRightIndex2, isStart: true);
			break;
		case StripCapStyle.HalfCircle:
			AddHalfCircleCap(vertexList2, indexList2, centers[0], tangents[0], rights[0], halfWidths[0], sectionColors[0], startLeftIndex2, startRightIndex2, isStart: true, capSteps2);
			break;
		}
		switch (endCap)
		{
		case StripCapStyle.Triangle:
			AddTriangleCap(vertexList2, indexList2, centers[^1], tangents[^1], rights[^1], halfWidths[^1], sectionColors[^1], endLeftIndex2, endRightIndex2, isStart: false);
			break;
		case StripCapStyle.HalfCircle:
			AddHalfCircleCap(vertexList2, indexList2, centers[^1], tangents[^1], rights[^1], halfWidths[^1], sectionColors[^1], endLeftIndex2, endRightIndex2, isStart: false, capSteps2);
			break;
		}
		return new PrimitiveMesh(vertexList2.ToArray(), indexList2.ToArray(), (PrimitiveType)0);
	}

	private static PooledPrimitiveMesh BuildStripCorePooled(IReadOnlyList<Vector3> path, Func<float, Color> colorResolver, Func<float, float> widthResolver, Vector3 up, int smoothingSegments, StripCapStyle startCap, StripCapStyle endCap, int capSegments, StripJoinStyle joinStyle, bool textured, StripCurveType smoothingCurve, StripWidthAttenuation widthAttenuation, PrimitiveMeshCache? cache)
	{
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_0815: Unknown result type (might be due to invalid IL or missing references)
		//IL_0821: Unknown result type (might be due to invalid IL or missing references)
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_068f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0856: Unknown result type (might be due to invalid IL or missing references)
		//IL_0862: Unknown result type (might be due to invalid IL or missing references)
		//IL_086e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0882: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		if (path.Count < 2)
		{
			throw new ArgumentException("At least two points are required.", "path");
		}
		if (colorResolver == null)
		{
			throw new ArgumentNullException("colorResolver");
		}
		if (widthResolver == null)
		{
			throw new ArgumentNullException("widthResolver");
		}
		if (cache == null)
		{
			cache = PrimitiveMeshCache.Shared;
		}
		IReadOnlyList<Vector3> workingPath = RemoveDegenerates(MaybeSmoothPath(path, smoothingSegments, smoothingCurve));
		float[] progress = ComputeProgress(workingPath, out var _);
		int vertexCount = workingPath.Count * 2;
		if (vertexCount > 32767)
		{
			throw new InvalidOperationException("Strip produced more vertices than supported by the index buffer.");
		}
		bool needCaps = startCap != StripCapStyle.None || endCap != StripCapStyle.None;
		VertexPositionColorTexture[] texturedVertices = null;
		VertexPositionColor[] colorVertices = null;
		PooledPrimitiveMesh pooledLease = default(PooledPrimitiveMesh);
		bool hasLease = false;
		List<VertexPositionColorTexture> texturedList = null;
		List<VertexPositionColor> colorList = null;
		Vector3[] tangents = (Vector3[])(object)(needCaps ? new Vector3[workingPath.Count] : null);
		Vector3[] rights = (Vector3[])(object)(needCaps ? new Vector3[workingPath.Count] : null);
		Vector3[] centers = (Vector3[])(object)(needCaps ? new Vector3[workingPath.Count] : null);
		Color[] sectionColors = (Color[])(object)(needCaps ? new Color[workingPath.Count] : null);
		float[] halfWidths = (needCaps ? new float[workingPath.Count] : null);
		if (needCaps)
		{
			if (textured)
			{
				texturedList = new List<VertexPositionColorTexture>(vertexCount);
			}
			else
			{
				colorList = new List<VertexPositionColor>(vertexCount);
			}
		}
		else
		{
			if (textured)
			{
				pooledLease = cache.RentTextured(vertexCount, vertexCount, (PrimitiveType)1);
				texturedVertices = pooledLease.TexturedVertices;
			}
			else
			{
				pooledLease = cache.RentColored(vertexCount, vertexCount, (PrimitiveType)1);
				colorVertices = pooledLease.ColorVertices;
			}
			hasLease = true;
		}
		Vector3 upNormalized = ((((Vector3)(ref up)).LengthSquared() < 1E-06f) ? Vector3.UnitZ : Vector3.Normalize(up));
		int segmentCount = workingPath.Count - 1;
		Vector3[] segmentDirs = (Vector3[])(object)new Vector3[segmentCount];
		Vector3[] segmentRights = (Vector3[])(object)new Vector3[segmentCount];
		for (int i = 0; i < segmentCount; i++)
		{
			Vector3 dir = workingPath[i + 1] - workingPath[i];
			if (((Vector3)(ref dir)).LengthSquared() < 1E-06f)
			{
				dir = ((i > 0) ? segmentDirs[i - 1] : Vector3.UnitY);
			}
			((Vector3)(ref dir)).Normalize();
			Vector3 right = Vector3.Cross(upNormalized, dir);
			if (((Vector3)(ref right)).LengthSquared() < 1E-06f)
			{
				right = FindPerpendicular(dir);
			}
			else
			{
				((Vector3)(ref right)).Normalize();
			}
			segmentDirs[i] = dir;
			segmentRights[i] = right;
		}
		Vector3 lastTangent = segmentDirs[0];
		for (int j = 0; j < workingPath.Count; j++)
		{
			Vector3 val = workingPath[j];
			float t = progress[j];
			float num = Math.Max(0f, widthResolver(MathHelper.Clamp(t, 0f, 1f)));
			Vector3 prevDir = segmentDirs[Math.Max(j - 1, 0)];
			Vector3 nextDir = segmentDirs[Math.Min(j, segmentCount - 1)];
			Vector3 tangent;
			if (j == 0)
			{
				tangent = nextDir;
			}
			else if (j == segmentCount)
			{
				tangent = prevDir;
			}
			else
			{
				tangent = prevDir + nextDir;
				if (((Vector3)(ref tangent)).LengthSquared() < 1E-06f)
				{
					tangent = nextDir;
				}
				else
				{
					((Vector3)(ref tangent)).Normalize();
				}
			}
			float width = num;
			if (j > 0 && widthAttenuation == StripWidthAttenuation.ContinuitySquared)
			{
				float continuity = MathHelper.Clamp((Vector3.Dot(lastTangent, tangent) + 1f) * 0.5f, 0f, 1f);
				width *= continuity * continuity;
			}
			float halfWidth = width * 0.5f;
			Vector3 prevRight = segmentRights[Math.Max(j - 1, 0)];
			Vector3 nextRight = segmentRights[Math.Min(j, segmentCount - 1)];
			Vector3 rightOffset;
			Vector3 leftOffset;
			if (joinStyle == StripJoinStyle.Miter)
			{
				rightOffset = ComputeMiterOffset(prevRight, nextRight, j == 0, j == segmentCount, halfWidth);
				leftOffset = ComputeMiterOffset(-prevRight, -nextRight, j == 0, j == segmentCount, halfWidth);
			}
			else
			{
				Vector3 joinNormal;
				if (j == 0)
				{
					joinNormal = nextRight;
				}
				else if (j == segmentCount)
				{
					joinNormal = prevRight;
				}
				else
				{
					joinNormal = prevRight + nextRight;
					if (((Vector3)(ref joinNormal)).LengthSquared() < 1E-06f)
					{
						joinNormal = nextRight;
					}
				}
				if (((Vector3)(ref joinNormal)).LengthSquared() < 1E-06f)
				{
					joinNormal = Vector3.UnitY;
				}
				((Vector3)(ref joinNormal)).Normalize();
				rightOffset = joinNormal * halfWidth;
				leftOffset = -joinNormal * halfWidth;
			}
			Vector3 leftPos = val + leftOffset;
			Vector3 rightPos = val + rightOffset;
			Vector3 chord = rightPos - leftPos;
			float chordLength = ((Vector3)(ref chord)).Length();
			Vector3 lateralDir = ((chordLength > 1E-06f) ? (chord / chordLength) : ((((Vector3)(ref nextRight)).LengthSquared() > 1E-06f) ? nextRight : Vector3.UnitX));
			Vector3 crossCenter = (leftPos + rightPos) * 0.5f;
			float effectiveHalfWidth = chordLength * 0.5f;
			float uCoord = progress[j];
			Color color = colorResolver(t);
			if (textured)
			{
				if (needCaps)
				{
					texturedList.Add(CreateEdgeVertex(leftPos, color, uCoord, isLeft: true));
					texturedList.Add(CreateEdgeVertex(rightPos, color, uCoord, isLeft: false));
				}
				else
				{
					texturedVertices[j * 2] = CreateEdgeVertex(leftPos, color, uCoord, isLeft: true);
					texturedVertices[j * 2 + 1] = CreateEdgeVertex(rightPos, color, uCoord, isLeft: false);
				}
			}
			else if (needCaps)
			{
				colorList.Add(new VertexPositionColor(leftPos, color));
				colorList.Add(new VertexPositionColor(rightPos, color));
			}
			else
			{
				colorVertices[j * 2] = new VertexPositionColor(leftPos, color);
				colorVertices[j * 2 + 1] = new VertexPositionColor(rightPos, color);
			}
			Vector3 rightForCap = ((((Vector3)(ref lateralDir)).LengthSquared() > 1E-06f) ? lateralDir : Vector3.UnitX);
			if (needCaps)
			{
				tangents[j] = tangent;
				rights[j] = Vector3.Normalize(rightForCap);
				centers[j] = crossCenter;
				sectionColors[j] = color;
				halfWidths[j] = effectiveHalfWidth;
			}
			lastTangent = tangent;
		}
		if (!needCaps)
		{
			if (!hasLease)
			{
				throw new InvalidOperationException("Pooled mesh lease was not initialized.");
			}
			PrimitiveSimd.FillSequentialIndices(pooledLease.Indices.AsSpan(0, vertexCount));
			return pooledLease;
		}
		if (textured)
		{
			List<VertexPositionColorTexture> vertexList = texturedList;
			List<short> indexList = new List<short>();
			AppendStripAsTriangles(indexList, vertexList.Count);
			int startLeftIndex = 0;
			int startRightIndex = 1;
			int endLeftIndex = texturedVertices.Length - 2;
			int endRightIndex = texturedVertices.Length - 1;
			int capSteps = Math.Max(2, capSegments);
			float startU = progress[0];
			float endU = progress[^1];
			switch (startCap)
			{
			case StripCapStyle.Triangle:
				AddTriangleCap(vertexList, indexList, centers[0], tangents[0], rights[0], halfWidths[0], sectionColors[0], startU, startLeftIndex, startRightIndex, isStart: true);
				break;
			case StripCapStyle.HalfCircle:
				AddHalfCircleCap(vertexList, indexList, centers[0], tangents[0], rights[0], halfWidths[0], sectionColors[0], startU, startLeftIndex, startRightIndex, isStart: true, capSteps);
				break;
			}
			switch (endCap)
			{
			case StripCapStyle.Triangle:
				AddTriangleCap(vertexList, indexList, centers[^1], tangents[^1], rights[^1], halfWidths[^1], sectionColors[^1], endU, endLeftIndex, endRightIndex, isStart: false);
				break;
			case StripCapStyle.HalfCircle:
				AddHalfCircleCap(vertexList, indexList, centers[^1], tangents[^1], rights[^1], halfWidths[^1], sectionColors[^1], endU, endLeftIndex, endRightIndex, isStart: false, capSteps);
				break;
			}
			PooledPrimitiveMesh lease = cache.RentTextured(vertexList.Count, indexList.Count, (PrimitiveType)0);
			vertexList.CopyTo(0, lease.TexturedVertices, 0, vertexList.Count);
			indexList.CopyTo(0, lease.Indices, 0, indexList.Count);
			return lease;
		}
		List<VertexPositionColor> vertexList2 = colorList;
		List<short> indexList2 = new List<short>();
		AppendStripAsTriangles(indexList2, vertexList2.Count);
		int startLeftIndex2 = 0;
		int startRightIndex2 = 1;
		int endLeftIndex2 = colorVertices.Length - 2;
		int endRightIndex2 = colorVertices.Length - 1;
		int capSteps2 = Math.Max(2, capSegments);
		switch (startCap)
		{
		case StripCapStyle.Triangle:
			AddTriangleCap(vertexList2, indexList2, centers[0], tangents[0], rights[0], halfWidths[0], sectionColors[0], startLeftIndex2, startRightIndex2, isStart: true);
			break;
		case StripCapStyle.HalfCircle:
			AddHalfCircleCap(vertexList2, indexList2, centers[0], tangents[0], rights[0], halfWidths[0], sectionColors[0], startLeftIndex2, startRightIndex2, isStart: true, capSteps2);
			break;
		}
		switch (endCap)
		{
		case StripCapStyle.Triangle:
			AddTriangleCap(vertexList2, indexList2, centers[^1], tangents[^1], rights[^1], halfWidths[^1], sectionColors[^1], endLeftIndex2, endRightIndex2, isStart: false);
			break;
		case StripCapStyle.HalfCircle:
			AddHalfCircleCap(vertexList2, indexList2, centers[^1], tangents[^1], rights[^1], halfWidths[^1], sectionColors[^1], endLeftIndex2, endRightIndex2, isStart: false, capSteps2);
			break;
		}
		PooledPrimitiveMesh lease2 = cache.RentColored(vertexList2.Count, indexList2.Count, (PrimitiveType)0);
		vertexList2.CopyTo(0, lease2.ColorVertices, 0, vertexList2.Count);
		indexList2.CopyTo(0, lease2.Indices, 0, indexList2.Count);
		return lease2;
	}

	private static IReadOnlyList<Vector3> MaybeSmoothPath(IReadOnlyList<Vector3> path, int subdivisions, StripCurveType curveType)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		if (subdivisions <= 0 || path.Count < 2)
		{
			return path;
		}
		List<Vector3> result = new List<Vector3>((path.Count - 1) * (subdivisions + 1) + 1);
		for (int i = 0; i < path.Count - 1; i++)
		{
			Vector3 p0 = path[Math.Max(i - 1, 0)];
			Vector3 p1 = path[i];
			Vector3 p2 = path[i + 1];
			Vector3 p3 = path[Math.Min(i + 2, path.Count - 1)];
			if (i == 0)
			{
				result.Add(p1);
			}
			for (int s = 1; s <= subdivisions; s++)
			{
				float t = (float)s / (float)(subdivisions + 1);
				Vector3 point = EvaluateCurve(curveType, p0, p1, p2, p3, t, path.Count);
				result.Add(point);
			}
			result.Add(p2);
		}
		return result;
	}

	private static Vector3 EvaluateCurve(StripCurveType curveType, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t, int count)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		switch (curveType)
		{
		case StripCurveType.Linear:
			return Vector3.Lerp(p1, p2, t);
		case StripCurveType.CatmullRom:
			if (count >= 4)
			{
				return Vector3.CatmullRom(p0, p1, p2, p3, t);
			}
			break;
		case StripCurveType.CubicBezier:
			if (count >= 4)
			{
				Vector3 c1 = p1 + (p2 - p0) / 6f;
				Vector3 c2 = p2 - (p3 - p1) / 6f;
				float inv = 1f - t;
				return inv * inv * inv * p1 + 3f * inv * inv * t * c1 + 3f * inv * t * t * c2 + t * t * t * p2;
			}
			break;
		case StripCurveType.Hermite:
			if (count >= 4)
			{
				Vector3 tan1 = (p2 - p0) * 0.5f;
				Vector3 tan2 = (p3 - p1) * 0.5f;
				return Vector3.Hermite(p1, tan1, p2, tan2, t);
			}
			break;
		}
		return Vector3.Lerp(p1, p2, t);
	}

	private static Color SampleColor(IReadOnlyList<Color> colors, float progress)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (colors.Count == 1)
		{
			return colors[0];
		}
		float scaled = MathHelper.Clamp(progress, 0f, 1f) * (float)(colors.Count - 1);
		int index = Math.Min(colors.Count - 2, (int)MathF.Floor(scaled));
		float localT = scaled - (float)index;
		return Color.Lerp(colors[index], colors[index + 1], localT);
	}

	private static float[] ComputeProgress(IReadOnlyList<Vector3> path, out float totalLength)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		float[] progress = new float[path.Count];
		float cumulative = 0f;
		for (int i = 1; i < path.Count; i++)
		{
			cumulative = (progress[i] = cumulative + Vector3.Distance(path[i - 1], path[i]));
		}
		totalLength = cumulative;
		if (cumulative > 1E-06f)
		{
			float inv = 1f / cumulative;
			for (int j = 1; j < progress.Length; j++)
			{
				progress[j] *= inv;
			}
		}
		return progress;
	}

	private static float EvaluateWidth(Func<float, float> widthFunc, Func<float, float>? easing, float progress)
	{
		float eased = easing?.Invoke(MathHelper.Clamp(progress, 0f, 1f)) ?? MathHelper.Clamp(progress, 0f, 1f);
		return Math.Max(0f, widthFunc(MathHelper.Clamp(eased, 0f, 1f)));
	}

	private static Vector3 FindPerpendicular(Vector3 vector)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		Vector3 axis = ((Math.Abs(vector.Y) < Math.Abs(vector.X)) ? Vector3.UnitY : Vector3.UnitX);
		Vector3 perpendicular = Vector3.Cross(vector, axis);
		if (((Vector3)(ref perpendicular)).LengthSquared() < 1E-06f)
		{
			perpendicular = Vector3.Cross(vector, Vector3.UnitZ);
		}
		((Vector3)(ref perpendicular)).Normalize();
		return perpendicular;
	}

	private static IReadOnlyList<Vector3> RemoveDegenerates(IReadOnlyList<Vector3> path)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		if (path.Count < 2)
		{
			return path;
		}
		List<Vector3> result = new List<Vector3>(path.Count);
		Vector3 last = path[0];
		result.Add(last);
		for (int i = 1; i < path.Count; i++)
		{
			if (!(Vector3.DistanceSquared(last, path[i]) <= 1E-06f))
			{
				last = path[i];
				result.Add(last);
			}
		}
		if (result.Count == 1)
		{
			result.Add(path[path.Count - 1]);
		}
		return result;
	}

	private static void AppendStripAsTriangles(List<short> indices, int vertexCount)
	{
		for (int i = 0; i < vertexCount - 2; i++)
		{
			if ((i & 1) == 0)
			{
				indices.Add((short)i);
				indices.Add((short)(i + 1));
				indices.Add((short)(i + 2));
			}
			else
			{
				indices.Add((short)(i + 1));
				indices.Add((short)i);
				indices.Add((short)(i + 2));
			}
		}
	}

	private static void AddTriangleCap(List<VertexPositionColorTexture> vertices, List<short> indices, Vector3 center, Vector3 tangent, Vector3 rightDir, float halfWidth, Color color, float uCoord, int leftIndex, int rightIndex, bool isStart)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (!(halfWidth <= 1E-06f))
		{
			Vector3 outward = (isStart ? (-tangent) : tangent);
			short apexIndex = AddVertex(vertices, CreateCapVertex(center + outward * halfWidth, color, uCoord, center, rightDir, halfWidth));
			if (isStart)
			{
				indices.Add(apexIndex);
				indices.Add((short)rightIndex);
				indices.Add((short)leftIndex);
			}
			else
			{
				indices.Add(apexIndex);
				indices.Add((short)leftIndex);
				indices.Add((short)rightIndex);
			}
		}
	}

	private static void AddTriangleCap(List<VertexPositionColor> vertices, List<short> indices, Vector3 center, Vector3 tangent, Vector3 rightDir, float halfWidth, Color color, int leftIndex, int rightIndex, bool isStart)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (!(halfWidth <= 1E-06f))
		{
			Vector3 outward = (isStart ? (-tangent) : tangent);
			short apexIndex = AddVertex(vertices, new VertexPositionColor(center + outward * halfWidth, color));
			if (isStart)
			{
				indices.Add(apexIndex);
				indices.Add((short)rightIndex);
				indices.Add((short)leftIndex);
			}
			else
			{
				indices.Add(apexIndex);
				indices.Add((short)leftIndex);
				indices.Add((short)rightIndex);
			}
		}
	}

	private static void AddHalfCircleCap(List<VertexPositionColorTexture> vertices, List<short> indices, Vector3 center, Vector3 tangent, Vector3 rightDir, float halfWidth, Color color, float uCoord, int leftIndex, int rightIndex, bool isStart, int segments)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		if (halfWidth <= 1E-06f)
		{
			return;
		}
		Vector3 outward = (isStart ? (-tangent) : tangent);
		short centerIndex = AddVertex(vertices, CreateCapVertex(center, color, uCoord, center, rightDir, halfWidth));
		List<short> arcVertices = new List<short> { isStart ? ((short)rightIndex) : ((short)leftIndex) };
		float step = (float)Math.PI / (float)segments;
		for (int i = 1; i < segments; i++)
		{
			float angle = (isStart ? (step * (float)i) : ((float)Math.PI - step * (float)i));
			Vector3 offset = rightDir * MathF.Cos(angle) + outward * MathF.Sin(angle);
			short arcIndex = AddVertex(vertices, CreateCapVertex(center + offset * halfWidth, color, uCoord, center, rightDir, halfWidth));
			arcVertices.Add(arcIndex);
		}
		arcVertices.Add(isStart ? ((short)leftIndex) : ((short)rightIndex));
		for (int j = 0; j < arcVertices.Count - 1; j++)
		{
			if (isStart)
			{
				indices.Add(centerIndex);
				indices.Add(arcVertices[j]);
				indices.Add(arcVertices[j + 1]);
			}
			else
			{
				indices.Add(centerIndex);
				indices.Add(arcVertices[j + 1]);
				indices.Add(arcVertices[j]);
			}
		}
	}

	private static void AddHalfCircleCap(List<VertexPositionColor> vertices, List<short> indices, Vector3 center, Vector3 tangent, Vector3 rightDir, float halfWidth, Color color, int leftIndex, int rightIndex, bool isStart, int segments)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		if (halfWidth <= 1E-06f)
		{
			return;
		}
		Vector3 outward = (isStart ? (-tangent) : tangent);
		short centerIndex = AddVertex(vertices, new VertexPositionColor(center, color));
		List<short> arcVertices = new List<short> { isStart ? ((short)rightIndex) : ((short)leftIndex) };
		float step = (float)Math.PI / (float)segments;
		for (int i = 1; i < segments; i++)
		{
			float angle = (isStart ? (step * (float)i) : ((float)Math.PI - step * (float)i));
			Vector3 offset = rightDir * MathF.Cos(angle) + outward * MathF.Sin(angle);
			short arcIndex = AddVertex(vertices, new VertexPositionColor(center + offset * halfWidth, color));
			arcVertices.Add(arcIndex);
		}
		arcVertices.Add(isStart ? ((short)leftIndex) : ((short)rightIndex));
		for (int j = 0; j < arcVertices.Count - 1; j++)
		{
			if (isStart)
			{
				indices.Add(centerIndex);
				indices.Add(arcVertices[j]);
				indices.Add(arcVertices[j + 1]);
			}
			else
			{
				indices.Add(centerIndex);
				indices.Add(arcVertices[j + 1]);
				indices.Add(arcVertices[j]);
			}
		}
	}

	private static short AddVertex(List<VertexPositionColorTexture> vertices, VertexPositionColorTexture vertex)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (vertices.Count >= 32767)
		{
			throw new InvalidOperationException("Primitive mesh exceeded 16-bit index capacity.");
		}
		vertices.Add(vertex);
		return (short)(vertices.Count - 1);
	}

	private static short AddVertex(List<VertexPositionColor> vertices, VertexPositionColor vertex)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (vertices.Count >= 32767)
		{
			throw new InvalidOperationException("Primitive mesh exceeded 16-bit index capacity.");
		}
		vertices.Add(vertex);
		return (short)(vertices.Count - 1);
	}

	private static VertexPositionColorTexture CreateEdgeVertex(Vector3 position, Color color, float u, bool isLeft)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		return new VertexPositionColorTexture(position, color, new Vector2(MathHelper.Clamp(u, 0f, 1f), isLeft ? 0f : 1f));
	}

	private static VertexPositionColorTexture CreateCapVertex(Vector3 position, Color color, float u, Vector3 center, Vector3 rightDir, float halfWidth)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		Vector3 right = ((((Vector3)(ref rightDir)).LengthSquared() > 1E-06f) ? Vector3.Normalize(rightDir) : Vector3.UnitX);
		float width = Math.Max(halfWidth, 1E-06f);
		float lateral = MathHelper.Clamp(Vector3.Dot(position - center, right) / width, -1f, 1f);
		float v = 0.5f + 0.5f * lateral;
		return new VertexPositionColorTexture(position, color, new Vector2(MathHelper.Clamp(u, 0f, 1f), MathHelper.Clamp(v, 0f, 1f)));
	}

	private static Vector3 ComputeMiterOffset(Vector3 prevNormal, Vector3 nextNormal, bool isStart, bool isEnd, float halfWidth)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		if (halfWidth <= 1E-06f)
		{
			return Vector3.Zero;
		}
		if (isStart)
		{
			return nextNormal * halfWidth;
		}
		if (isEnd)
		{
			return prevNormal * halfWidth;
		}
		float prevLenSq = ((Vector3)(ref prevNormal)).LengthSquared();
		float nextLenSq = ((Vector3)(ref nextNormal)).LengthSquared();
		if (prevLenSq < 1E-06f || nextLenSq < 1E-06f)
		{
			return ((nextLenSq >= prevLenSq) ? nextNormal : prevNormal) * halfWidth;
		}
		Vector3 sum = prevNormal + nextNormal;
		float sumLenSq = ((Vector3)(ref sum)).LengthSquared();
		if (sumLenSq < 0.0001f)
		{
			return nextNormal * halfWidth;
		}
		Vector3 miter = sum / MathF.Sqrt(sumLenSq);
		float denom = Vector3.Dot(miter, nextNormal);
		if (MathF.Abs(denom) <= 0.001f)
		{
			return nextNormal * halfWidth;
		}
		float scale = halfWidth / denom;
		float maxScale = halfWidth * 4f;
		if (MathF.Abs(scale) > maxScale)
		{
			scale = (float)MathF.Sign(scale) * maxScale;
		}
		return miter * scale;
	}
}
