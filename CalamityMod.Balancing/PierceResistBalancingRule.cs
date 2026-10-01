using Terraria;

namespace CalamityMod.Balancing;

public class PierceResistBalancingRule : IBalancingRule
{
	public float DamageMultiplier;

	public PierceResistBalancingRule(float damageMultiplier)
	{
		DamageMultiplier = damageMultiplier;
	}

	public bool AppliesTo(NPC npc, NPC.HitModifiers modifiers, Projectile? projectile)
	{
		if (projectile != null)
		{
			if (projectile.maxPenetrate <= 1)
			{
				return projectile.maxPenetrate == -1;
			}
			return true;
		}
		return false;
	}

	public void ApplyBalancingChange(NPC npc, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= DamageMultiplier;
	}
}
