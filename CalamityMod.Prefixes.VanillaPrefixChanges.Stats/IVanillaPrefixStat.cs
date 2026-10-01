using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Prefixes.VanillaPrefixChanges.Stats;

public interface IVanillaPrefixStat
{
	void ApplyEffects(Player player);

	void ModifyTooltip(TooltipLine line);
}
