using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class MenacingPrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 72;

	public override string TargetTooltipName => "PrefixAccDamage";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixDamageStat(0.04f);
	}
}
