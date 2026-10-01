using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class HastyPrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 75;

	public override string TargetTooltipName => "PrefixAccMoveSpeed";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixMovementSpeedStat(0.02f);
		yield return new PrefixCritChanceStat(2);
	}
}
