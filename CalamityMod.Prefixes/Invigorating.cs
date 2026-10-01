using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

public class Invigorating : ModPrefix, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Prefixes.Accessory";

	public override PrefixCategory Category => PrefixCategory.Accessory;

	public LocalizedText LifeRegenTooltip => CalamityUtils.GetText(LocalizationCategory + ".LifeRegenTooltip");

	public override void ApplyAccessoryEffects(Player player)
	{
		player.lifeRegen += GetLifeRegenAmount();
	}

	public override void ModifyValue(ref float valueMult)
	{
		valueMult = 1.199f;
	}

	public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
	{
		string perSecHP = ((float)GetLifeRegenAmount() / 2f).ToString("0.#");
		yield return new TooltipLine(base.Mod, "CalamityMod:PrefixLifeRegenBoost", LifeRegenTooltip.Format(perSecHP))
		{
			IsModifier = true
		};
	}

	public static int GetLifeRegenAmount()
	{
		return 1;
	}
}
