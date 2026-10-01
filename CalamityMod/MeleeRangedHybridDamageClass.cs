using Terraria.ModLoader;

namespace CalamityMod;

public class MeleeRangedHybridDamageClass : DamageClass
{
	internal static MeleeRangedHybridDamageClass Instance;

	internal static readonly StatInheritanceData FiftyPercentBoost = new StatInheritanceData(0.5f, 0.5f, 0.5f, 0.5f, 0.5f);

	public override void Load()
	{
		Instance = this;
	}

	public override void Unload()
	{
		Instance = null;
	}

	public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
	{
		if (damageClass == DamageClass.Melee || damageClass == DamageClass.Ranged)
		{
			return FiftyPercentBoost;
		}
		if (damageClass == DamageClass.Generic)
		{
			return StatInheritanceData.Full;
		}
		return StatInheritanceData.None;
	}

	public override bool GetEffectInheritance(DamageClass damageClass)
	{
		if (damageClass != DamageClass.Melee)
		{
			return damageClass == DamageClass.Ranged;
		}
		return true;
	}
}
