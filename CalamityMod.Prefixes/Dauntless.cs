using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

public class Dauntless : ModPrefix, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Prefixes.Accessory";

	public override PrefixCategory Category => PrefixCategory.Accessory;

	public LocalizedText LifeBoostTooltip => CalamityUtils.GetText(LocalizationCategory + ".MaxLifeBoostTooltip");

	public override void ApplyAccessoryEffects(Player player)
	{
		player.statLifeMax2 += GetHealthBoostAmount();
	}

	public override void ModifyValue(ref float valueMult)
	{
		valueMult = 1.199f;
	}

	public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
	{
		yield return new TooltipLine(base.Mod, "CalamityMod:PrefixMaxLifeBoost", LifeBoostTooltip.Format(GetHealthBoostAmount()))
		{
			IsModifier = true
		};
	}

	public static int GetHealthBoostAmount()
	{
		return 15;
	}
}
