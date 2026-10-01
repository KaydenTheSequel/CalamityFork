using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Balancing;

public class ClassResistBalancingRule : IBalancingRule
{
	public float DamageMultiplier;

	public DamageClass ApplicableClass;

	public ClassResistBalancingRule(float damageMultiplier, DamageClass dc)
	{
		DamageMultiplier = damageMultiplier;
		ApplicableClass = dc;
	}

	public bool AppliesTo(NPC npc, NPC.HitModifiers modifiers, Projectile? projectile)
	{
		return modifiers.DamageType.CountsAsClass(ApplicableClass);
	}

	public void ApplyBalancingChange(NPC npc, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= DamageMultiplier;
	}
}
