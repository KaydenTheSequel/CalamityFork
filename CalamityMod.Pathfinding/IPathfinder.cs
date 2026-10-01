using System.Collections.Generic;
using CalamityMod.Pathfinding.Movements;

namespace CalamityMod.Pathfinding;

public interface IPathfinder
{
	IEnumerable<IMovement> Movements { get; }

	void AwaitingPathBehavior();
}
