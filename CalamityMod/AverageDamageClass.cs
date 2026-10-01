using Terraria.ModLoader;

namespace CalamityMod;

public class AverageDamageClass : DamageClass
{
	internal static AverageDamageClass Instance;

	internal static readonly StatInheritanceData TwentyPercentBoost = new StatInheritanceData(0.2f, 0.2f, 0.2f, 0.2f, 0.2f);

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
		if (damageClass == DamageClass.Melee || damageClass == DamageClass.Ranged || damageClass == DamageClass.Magic || damageClass == DamageClass.Summon || damageClass == RogueDamageClass.Instance)
		{
			return TwentyPercentBoost;
		}
		if (damageClass == DamageClass.Generic)
		{
			return StatInheritanceData.Full;
		}
		return StatInheritanceData.None;
	}
}
