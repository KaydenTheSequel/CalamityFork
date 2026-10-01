using Terraria.ModLoader;

namespace CalamityMod;

public class StealthDamageClass : DamageClass
{
	internal static StealthDamageClass Instance;

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
		if (damageClass == DamageClass.Generic || damageClass == DamageClass.Throwing || damageClass == RogueDamageClass.Instance)
		{
			return StatInheritanceData.Full;
		}
		return StatInheritanceData.None;
	}

	public override bool GetEffectInheritance(DamageClass damageClass)
	{
		if (damageClass != DamageClass.Throwing)
		{
			return damageClass == RogueDamageClass.Instance;
		}
		return true;
	}
}
