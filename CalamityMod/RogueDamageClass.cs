using Terraria.ModLoader;

namespace CalamityMod;

public class RogueDamageClass : DamageClass
{
	internal static RogueDamageClass Instance;

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
		if (damageClass == DamageClass.Throwing || damageClass == DamageClass.Generic)
		{
			return StatInheritanceData.Full;
		}
		return StatInheritanceData.None;
	}

	public override bool GetEffectInheritance(DamageClass damageClass)
	{
		return damageClass == DamageClass.Throwing;
	}
}
