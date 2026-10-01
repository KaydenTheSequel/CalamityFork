using System.Collections.Generic;
using CalamityMod.Items;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Prefixes;

public abstract class RogueWeaponPrefix : ModPrefix, ILocalizedModType, IModType
{
	internal const string StealthTooltipID = "CalamityMod:PrefixStealthDamage";

	public new string LocalizationCategory => "Prefixes.Weapon";

	public virtual float damageMult => 1f;

	public virtual float useTimeMult => 1f;

	public virtual int critBonus => 0;

	public virtual float shootSpeedMult => 1f;

	public virtual float stealthDmgMult => 1f;

	public override PrefixCategory Category => PrefixCategory.AnyWeapon;

	public LocalizedText StealthDamageTooltip => CalamityUtils.GetText(LocalizationCategory + ".StealthDamageTooltip");

	public override bool CanRoll(Item item)
	{
		if (item.CountsAsClass<ThrowingDamageClass>() && (item.maxStack == 1 || item.AllowReforgeForStackableItem))
		{
			return GetType() != typeof(RogueWeaponPrefix);
		}
		return false;
	}

	public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
	{
		damageMult = this.damageMult;
		useTimeMult = this.useTimeMult;
		critBonus = this.critBonus;
		shootSpeedMult = this.shootSpeedMult;
	}

	public override void Apply(Item item)
	{
		if (item.CountsAsClass<RogueDamageClass>() && item.TryGetGlobalItem<RogueGlobalItem>(out var rogueItem))
		{
			rogueItem.StealthStrikePrefixBonus = stealthDmgMult;
		}
	}

	public override void ModifyValue(ref float valueMult)
	{
		float extraStealthDamage = stealthDmgMult - 1f;
		float stealthDamageValueMultiplier = 1f;
		float extraValue = 1f + stealthDamageValueMultiplier * extraStealthDamage;
		valueMult *= extraValue;
	}

	public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
	{
		if (stealthDmgMult != 1f)
		{
			yield return new TooltipLine(base.Mod, "CalamityMod:PrefixStealthDamage", StealthDamageTooltip.Format(((stealthDmgMult >= 1f) ? "+" : string.Empty) + (stealthDmgMult * 100f - 100f).ToString("N0")))
			{
				IsModifier = true,
				IsModifierBad = (stealthDmgMult < 1f)
			};
		}
	}
}
