using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class QuickPrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 76;

	public override string TargetTooltipName => "PrefixAccMoveSpeed";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixMovementSpeedStat(0.04f);
	}
}
