using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CalamityMod.DataStructures;
using CalamityMod.Pathfinding.Movements;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Pathfinding;

public class PathfindingManager(IPathfinder p)
{
	private class PathfindingTask(PathfindingParameters parameters)
	{
		internal readonly TaskCompletionSource<List<PathfindingNode>> _task = new TaskCompletionSource<List<PathfindingNode>>();

		internal readonly PathfindingWork work = new PathfindingWork(parameters);

		internal List<PathfindingNode> Result => _task.Task.Result;

		internal bool Ready { get; private set; }

		internal bool Running { get; private set; }

		internal void Run()
		{
			Running = true;
			ThreadPool.QueueUserWorkItem(delegate(object? pathfindingTask)
			{
				PathfindingTask pathfindingTask2 = pathfindingTask as PathfindingTask;
				pathfindingTask2._task.TrySetResult(pathfindingTask2.work.CalculatePath());
				pathfindingTask2.Ready = true;
				pathfindingTask2.Running = false;
			}, this);
		}
	}

	private record PathfindingWork(IPathfinder Pathfinder, Point Start, Point End, Func<Point, bool> TileValidity, Func<Point, Point, float> DistanceFunction, Func<Point, Point, float> Heuristic)
	{
		public PathfindingWork(PathfindingParameters p)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			this._002Ector(p.Pathfinder, p.Start.ToTileCoordinates(), p.End.ToTileCoordinates(), p.TileValidity ?? PathfindingParameters.DefaultTileValidity, p.DistanceFunction ?? PathfindingParameters.DefaultDistanceFunction, p.Heuristic ?? PathfindingParameters.DefaultHeuristic);
		}

		internal List<PathfindingNode> CalculatePath()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0120: Unknown result type (might be due to invalid IL or missing references)
			//IL_013c: Unknown result type (might be due to invalid IL or missing references)
			//IL_015a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0161: Unknown result type (might be due to invalid IL or missing references)
			//IL_0197: Unknown result type (might be due to invalid IL or missing references)
			//IL_019d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
			if (CalamityUtils.ParanoidTileRetrieval(End.X, End.Y).IsTileSolid())
			{
				return null;
			}
			HeapDict<PathfindingNode, float> candidates = new HeapDict<PathfindingNode, float>();
			HashSet<PathfindingNode> explored = new HashSet<PathfindingNode>();
			foreach (IMovement movement in Pathfinder.Movements)
			{
				PathfindingNode start = new PathfindingNode(Start, movement);
				PathfindingNode pathfindingNode = start;
				float total = Heuristic(start.Position, End);
				start.Distance = 0f;
				pathfindingNode.Total = total;
				candidates.Add(start, start.Total);
			}
			while (candidates.Count > 0)
			{
				PathfindingNode current = candidates.PeekMin().Item1;
				if (current.Position == End)
				{
					return current.ReconstructPath();
				}
				candidates.PopMin();
				explored.Add(current);
				foreach (PathfindingNode neighbor in current.GetNeighborSteps(Pathfinder))
				{
					if (!explored.Contains(neighbor) && !Main.tile[neighbor.Position].IsTileSolid() && TileValidity(neighbor.Position))
					{
						float newDistance = current.Distance + DistanceFunction(current.Position, neighbor.Position);
						if (!(newDistance >= neighbor.Distance))
						{
							neighbor.Parent = current;
							neighbor.Distance = newDistance;
							neighbor.Total = newDistance + Heuristic(neighbor.Position, End) + neighbor.Move.Cost(current.Position, neighbor.Position);
							candidates.Add(neighbor, neighbor.Total);
						}
					}
				}
			}
			return null;
		}
	}

	public class PathfindingNode
	{
		[CompilerGenerated]
		private Point _003CPosition_003Ek__BackingField;

		internal Point Position
		{
			[CompilerGenerated]
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return _003CPosition_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				_003CPosition_003Ek__BackingField = value;
			}
		}

		internal float Distance { get; set; }

		internal float Total { get; set; }

		internal IMovement Move { get; set; }

		internal PathfindingNode Parent { get; set; }

		public PathfindingNode(Point position, IMovement move)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			Position = position;
			Distance = float.MaxValue;
			Total = float.MaxValue;
			Move = move;
			base._002Ector();
		}

		public override bool Equals(object obj)
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			if (obj is PathfindingNode other)
			{
				if (Position == other.Position)
				{
					return Move.RegistrationName.Equals(other.Move.RegistrationName);
				}
				return false;
			}
			return false;
		}

		public override int GetHashCode()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return ((object)Position/*cast due to constrained. prefix*/).GetHashCode() ^ Move.RegistrationName.GetHashCode();
		}

		internal IEnumerable<PathfindingNode> GetNeighborSteps(IPathfinder pathfinder)
		{
			foreach (IMovement movement in pathfinder.Movements)
			{
				foreach (Point neighbor in movement.GetDestinations(Position))
				{
					yield return new PathfindingNode(neighbor, movement);
				}
			}
		}

		internal List<PathfindingNode> ReconstructPath()
		{
			List<PathfindingNode> path = new List<PathfindingNode>();
			for (PathfindingNode current = this; current != null; current = current.Parent)
			{
				path.Add(current);
			}
			path.Reverse();
			return path;
		}
	}

	internal readonly IPathfinder pathfinder = p;

	private PathfindingTask lastSuccessfulTask;

	private PathfindingTask currentTask;

	public List<Vector2> Path
	{
		get
		{
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			if (lastSuccessfulTask == null)
			{
				return new List<Vector2>();
			}
			if (lastSuccessfulTask.Result == null)
			{
				return new List<Vector2>();
			}
			List<Vector2> path = new List<Vector2>();
			foreach (PathfindingNode node in lastSuccessfulTask.Result)
			{
				path.Add(node.Position.ToWorldCoordinates());
			}
			return path;
		}
	}

	public void FindPath(PathfindingParameters parameters, bool forceNewTask = true)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		PathfindingTask potentialNewTask = new PathfindingTask(parameters);
		if (currentTask == null)
		{
			currentTask = potentialNewTask;
			currentTask.Run();
		}
		if (currentTask.Running)
		{
			bool sameEndPosition = parameters.End.ToTileCoordinates() == currentTask.work.End;
			if (forceNewTask && !sameEndPosition)
			{
				currentTask = potentialNewTask;
				currentTask.Run();
			}
			return;
		}
		bool num = currentTask.Result == null;
		bool previousTaskPathFollowed = (currentTask.Result?.Count ?? (-1)) == 0;
		if (num | previousTaskPathFollowed | forceNewTask)
		{
			currentTask = potentialNewTask;
			currentTask.Run();
		}
		else
		{
			lastSuccessfulTask = currentTask;
		}
	}

	public void PathfindingBehavior()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (lastSuccessfulTask == null)
		{
			pathfinder.AwaitingPathBehavior();
			return;
		}
		PathfindingNode nextPoint = lastSuccessfulTask.Result[0];
		if (nextPoint.Move.FollowPath(nextPoint.Position.ToWorldCoordinates()))
		{
			lastSuccessfulTask.Result.RemoveAt(0);
		}
		if (lastSuccessfulTask.Result.Count == 0)
		{
			lastSuccessfulTask = null;
		}
	}

	public void DoPathfinding(PathfindingParameters parameters, bool forceNewTask = false)
	{
		FindPath(parameters, forceNewTask);
		PathfindingBehavior();
	}

	public void ClearResults()
	{
		currentTask = null;
		lastSuccessfulTask = null;
	}
}
