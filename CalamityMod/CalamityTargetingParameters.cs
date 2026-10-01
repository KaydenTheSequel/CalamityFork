using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CalamityMod.Enums;
using Microsoft.Xna.Framework;

namespace CalamityMod;

public struct CalamityTargetingParameters : IEquatable<CalamityTargetingParameters>
{
	public bool faceTarget = true;

	public Vector2? targetingCenter = null;

	public float maxSearchRange = 9600f;

	public NPCTargetType targetType = NPCTargetType.Anyone;

	public float aggroRatio = 1f;

	public bool requireLineOfSight = false;

	public bool finishThemOff = false;

	internal const float FinishThemOff_MaxAggroBoost = 4000f;

	public bool ignoreTankMinions = false;

	public bool ignoreStealthedPlayers = true;

	public bool forceNetUpdate = false;

	public HashSet<int> excludedPlayers = new HashSet<int>();

	public static CalamityTargetingParameters Defaults => new CalamityTargetingParameters();

	public static CalamityTargetingParameters BossDefaults => new CalamityTargetingParameters(isBoss: true);

	public CalamityTargetingParameters()
	{
	}

	public CalamityTargetingParameters(bool isBoss)
	{
		ignoreTankMinions = isBoss;
	}

	public static bool operator ==(CalamityTargetingParameters ctp1, CalamityTargetingParameters ctp2)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		Vector2? val = ctp1.targetingCenter;
		if (!val.HasValue)
		{
			val = ctp2.targetingCenter;
			if (!val.HasValue)
			{
				goto IL_0067;
			}
		}
		val = ctp1.targetingCenter;
		Vector2? val2 = ctp2.targetingCenter;
		if (val.HasValue != val2.HasValue || (val.HasValue && !(val.GetValueOrDefault() == val2.GetValueOrDefault())))
		{
			return false;
		}
		goto IL_0067;
		IL_0067:
		if (ctp1.faceTarget == ctp2.faceTarget && ctp1.maxSearchRange == ctp2.maxSearchRange && ctp1.targetType == ctp2.targetType && ctp1.aggroRatio == ctp2.aggroRatio && ctp1.requireLineOfSight == ctp2.requireLineOfSight && ctp1.finishThemOff == ctp2.finishThemOff && ctp1.ignoreTankMinions == ctp2.ignoreTankMinions && ctp1.ignoreStealthedPlayers == ctp2.ignoreStealthedPlayers)
		{
			return ctp1.forceNetUpdate == ctp2.forceNetUpdate;
		}
		return false;
	}

	public static bool operator !=(CalamityTargetingParameters ctp1, CalamityTargetingParameters ctp2)
	{
		return !(ctp1 == ctp2);
	}

	public readonly bool Equals(CalamityTargetingParameters other)
	{
		return this == other;
	}

	public override readonly bool Equals([NotNullWhen(true)] object obj)
	{
		if (!(obj is CalamityTargetingParameters))
		{
			return false;
		}
		return this == (CalamityTargetingParameters)obj;
	}

	public override readonly int GetHashCode()
	{
		return base.GetHashCode();
	}
}
