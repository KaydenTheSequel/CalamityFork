using Terraria;

namespace CalamityMod.Balancing;

public class StealthStrikeBalancingRule : IBalancingRule
{
	public float DamageMultiplier;

	public int[] ApplicableProjectileTypes;

	public StealthStrikeBalancingRule(float damageMultiplier, params int[] projTypes)
	{
		DamageMultiplier = damageMultiplier;
		ApplicableProjectileTypes = projTypes;
	}

	public bool AppliesTo(NPC npc, NPC.HitModifiers modifiers, Projectile? projectile)
	{
		if (projectile != null)
		{
			if (modifiers.DamageType != StealthDamageClass.Instance)
			{
				return projectile.Calamity().stealthStrike;
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
