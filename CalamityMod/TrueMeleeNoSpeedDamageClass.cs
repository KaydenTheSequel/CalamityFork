using Terraria.ModLoader;

namespace CalamityMod;

public class TrueMeleeNoSpeedDamageClass : DamageClass
{
	internal static TrueMeleeNoSpeedDamageClass Instance;

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
		if (damageClass == DamageClass.Generic || damageClass == DamageClass.Melee || damageClass == TrueMeleeDamageClass.Instance)
		{
			StatInheritanceData full = StatInheritanceData.Full;
			full.attackSpeedInheritance = 0f;
			return full;
		}
		return StatInheritanceData.None;
	}

	public override bool GetEffectInheritance(DamageClass damageClass)
	{
		if (damageClass != DamageClass.Melee && damageClass != DamageClass.MeleeNoSpeed)
		{
			return damageClass == TrueMeleeDamageClass.Instance;
		}
		return true;
	}
}
