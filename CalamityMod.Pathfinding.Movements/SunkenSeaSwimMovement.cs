using System;
using System.Collections.Generic;
using CalamityMod.NPCs.SunkenSea;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Pathfinding.Movements;

public class SunkenSeaSwimMovement(NPC creature) : IMovement
{
	public NPC Creature = creature;

	private SunkenSeaNPC ssn => Creature.ModNPC<SunkenSeaNPC>();

	public Func<Point, Point, float> Cost => (Point _, Point _) => 0f;

	public string RegistrationName => "Swim";

	public bool FollowPath(Vector2 nextPoint)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		NPC creature = Creature;
		creature.velocity += (nextPoint - Creature.Center).SafeNormalize(Vector2.Zero) * ssn.Acceleration;
		if (((Vector2)(ref Creature.velocity)).LengthSquared() > ssn.MaxSpeed * ssn.MaxSpeed)
		{
			Creature.velocity = Creature.velocity.SafeNormalize(Vector2.UnitY) * ssn.MaxSpeed;
		}
		if (Vector2.DistanceSquared(Creature.Center, nextPoint) < ssn.MinimumPointDistance * ssn.MinimumPointDistance)
		{
			return true;
		}
		return false;
	}

	public IEnumerable<Point> GetDestinations(Point current)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		int nodeX = current.X;
		int nodeY = current.Y;
		foreach (Vector2 direction in CalamityUtils.Directions)
		{
			yield return new Point(nodeX + (int)direction.X, nodeY + (int)direction.Y);
		}
	}

	public void Start()
	{
	}
}
