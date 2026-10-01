using System.Collections.Generic;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;
using Terraria;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public class JaggedPrefixChange : VanillaPrefixChange
{
	public override int TargetPrefix => 69;

	public override string TargetTooltipName => "PrefixAccDamage";

	public override IEnumerator<IVanillaPrefixStat> PopulateStats()
	{
		yield return new PrefixDamageStat(0.02f);
		if (NPC.downedMoonlord)
		{
			yield return new PrefixArmorPenStat(3);
		}
		else if (Main.hardMode)
		{
			yield return new PrefixArmorPenStat(2);
		}
		else
		{
			yield return new PrefixArmorPenStat(1);
		}
	}
}
