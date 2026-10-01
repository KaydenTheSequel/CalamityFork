using Terraria;

namespace CalamityMod.Balancing;

public class ProjectileSpecificRequirementBalancingRule : IBalancingRule
{
	public delegate bool ProjectileApplicationRequirement(Projectile proj);

	public float DamageMultiplier;

	public ProjectileApplicationRequirement Requirement;

	public ProjectileSpecificRequirementBalancingRule(float damageMultiplier, ProjectileApplicationRequirement projApplicationRequirement)
	{
		DamageMultiplier = damageMultiplier;
		Requirement = projApplicationRequirement;
	}

	public bool AppliesTo(NPC npc, NPC.HitModifiers modifiers, Projectile? projectile)
	{
		if (projectile != null)
		{
			return Requirement(projectile);
		}
		return false;
	}

	public void ApplyBalancingChange(NPC npc, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= DamageMultiplier;
	}
}
