using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class LuckyPrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 68;

	public override string TargetTooltipName => "PrefixAccCritChance";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixCritChanceStat(4);
		yield return new PrefixLuckStat(0.05f);
	}
}
