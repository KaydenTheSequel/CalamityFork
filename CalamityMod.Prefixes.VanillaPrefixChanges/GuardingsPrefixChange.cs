using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class GuardingsPrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 63;

	public override string TargetTooltipName => "PrefixAccDefense";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixDefenseStat(2);
		yield return new PrefixMovementSpeedStat(0.02f);
	}
}
