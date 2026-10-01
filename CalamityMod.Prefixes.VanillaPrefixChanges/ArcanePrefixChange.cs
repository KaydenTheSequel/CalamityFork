using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class ArcanePrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 66;

	public override string TargetTooltipName => "PrefixAccMaxMana";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixArcaneStat(20, 0.02f, 0.02f);
	}
}
