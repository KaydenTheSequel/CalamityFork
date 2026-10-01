using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

public abstract class RogueAccessoryPrefix : ModPrefix, ILocalizedModType, IModType
{
	internal const string StealthTooltipID = "CalamityMod:PrefixAccStealthGen";

	public new string LocalizationCategory => "Prefixes.Accessory";

	public virtual float stealthGenBonus => 0f;

	public override PrefixCategory Category => PrefixCategory.Accessory;

	public LocalizedText StealthGenTooltip => CalamityUtils.GetText(LocalizationCategory + ".StealthGenTooltip");

	public override bool CanRoll(Item item)
	{
		return GetType() != typeof(RogueAccessoryPrefix);
	}

	public override void ApplyAccessoryEffects(Player player)
	{
		player.Calamity().accStealthGenBoost += stealthGenBonus;
	}

	public override void ModifyValue(ref float valueMult)
	{
		valueMult = 1.199f;
	}

	public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
	{
		yield return new TooltipLine(base.Mod, "CalamityMod:PrefixAccStealthGen", StealthGenTooltip.Format((stealthGenBonus * 100f).ToString("N0")))
		{
			IsModifier = true
		};
	}
}
