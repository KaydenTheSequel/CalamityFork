using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class ViolentPrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 80;

	public override string TargetTooltipName => "PrefixAccMeleeSpeed";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixMeleeSpeedStat(0.04f);
	}
}
