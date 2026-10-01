using Terraria.ModLoader;

namespace CalamityMod;

public class TrueMeleeDamageClass : DamageClass
{
	internal static TrueMeleeDamageClass Instance;

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
		if (damageClass == DamageClass.Melee || damageClass == DamageClass.Generic)
		{
			return StatInheritanceData.Full;
		}
		return StatInheritanceData.None;
	}

	public override bool GetEffectInheritance(DamageClass damageClass)
	{
		return damageClass == DamageClass.Melee;
	}
}
