using System;
using Microsoft.Xna.Framework;

namespace CalamityMod.Pathfinding;

public readonly struct PathfindingParameters
{
	internal static readonly Func<Point, bool> DefaultTileValidity = (Point point) => true;

	internal static readonly Func<Point, Point, float> DefaultDistanceFunction = CalamityUtils.OctileDistance;

	internal static readonly Func<Point, Point, float> DefaultHeuristic = CalamityUtils.OctileDistance;

	internal readonly IPathfinder Pathfinder;

	internal readonly Vector2 Start;

	internal readonly Vector2 End;

	internal readonly Func<Point, bool> TileValidity;

	internal readonly Func<Point, Point, float> DistanceFunction;

	internal readonly Func<Point, Point, float> Heuristic;

	public PathfindingParameters(IPathfinder pathfinder, Vector2 start, Vector2 end, Func<Point, bool> tileValidity = null, Func<Point, Point, float> distanceFunction = null, Func<Point, Point, float> heuristic = null)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		Pathfinder = pathfinder;
		Start = start;
		End = end;
		TileValidity = tileValidity;
		DistanceFunction = distanceFunction;
		Heuristic = heuristic;
	}
}
