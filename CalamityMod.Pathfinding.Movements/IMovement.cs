using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CalamityMod.Pathfinding.Movements;

public interface IMovement
{
	string RegistrationName { get; }

	Func<Point, Point, float> Cost { get; }

	void Start();

	bool FollowPath(Vector2 nextPoint);

	IEnumerable<Point> GetDestinations(Point current);
}
