using System.Linq;
using Terraria;

namespace CalamityMod.Balancing;

public class ProjectileResistBalancingRule : IBalancingRule
{
	public float DamageMultiplier;

	public int[] ApplicableProjectileTypes;

	public ProjectileResistBalancingRule(float damageMultiplier, params int[] projTypes)
	{
		DamageMultiplier = damageMultiplier;
		ApplicableProjectileTypes = projTypes;
	}

	public bool AppliesTo(NPC npc, NPC.HitModifiers modifiers, Projectile? projectile)
	{
		if (projectile != null)
		{
			return ApplicableProjectileTypes.Contains(projectile.type);
		}
		return false;
	}

	public void ApplyBalancingChange(NPC npc, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= DamageMultiplier;
	}
}
