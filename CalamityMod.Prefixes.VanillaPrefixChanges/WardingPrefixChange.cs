using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class WardingPrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 65;

	public override string TargetTooltipName => "PrefixAccDefense";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixDefenseStat(4);
		yield return new PrefixDRStat(0.01f);
	}
}
