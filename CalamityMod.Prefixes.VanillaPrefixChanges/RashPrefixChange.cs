using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class RashPrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 78;

	public override string TargetTooltipName => "PrefixAccMeleeSpeed";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixMeleeSpeedStat(0.02f);
		yield return new PrefixCritChanceStat(2);
	}
}
