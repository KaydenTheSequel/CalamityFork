using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class IntrepidPrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 79;

	public override string TargetTooltipName => "PrefixAccMeleeSpeed";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixMeleeSpeedStat(0.02f);
		yield return new PrefixMovementSpeedStat(0.02f);
	}
}
