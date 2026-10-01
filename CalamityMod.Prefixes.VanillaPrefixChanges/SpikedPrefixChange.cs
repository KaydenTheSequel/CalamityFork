using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class SpikedPrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 70;

	public override string TargetTooltipName => "PrefixAccDamage";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixDamageStat(0.02f);
		yield return new PrefixDefenseStat(2);
	}
}
