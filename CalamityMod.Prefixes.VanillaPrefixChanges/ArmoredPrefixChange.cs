using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class ArmoredPrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 64;

	public override string TargetTooltipName => "PrefixAccDefense";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixDefenseStat(2);
		yield return new PrefixDRStat(0.015f);
	}
}
