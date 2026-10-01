using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class HardPrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 62;

	public override string TargetTooltipName => "PrefixAccDefense";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixDefenseStat(5);
	}
}
