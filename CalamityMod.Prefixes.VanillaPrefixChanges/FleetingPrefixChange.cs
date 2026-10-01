using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class FleetingPrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 74;

	public override string TargetTooltipName => "PrefixAccMoveSpeed";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixMovementSpeedStat(0.02f);
		yield return new PrefixDamageStat(0.02f);
	}
}
