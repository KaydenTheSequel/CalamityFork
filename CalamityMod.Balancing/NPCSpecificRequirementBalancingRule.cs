using Terraria;

namespace CalamityMod.Balancing;

public class NPCSpecificRequirementBalancingRule : IBalancingRule
{
	public delegate bool NPCApplicationRequirement(NPC npc);

	public NPCApplicationRequirement Requirement;

	public NPCSpecificRequirementBalancingRule(NPCApplicationRequirement npcApplicationRequirement)
	{
		Requirement = npcApplicationRequirement;
	}

	public bool AppliesTo(NPC npc, NPC.HitModifiers modifiers, Projectile? projectile)
	{
		return Requirement(npc);
	}

	public void ApplyBalancingChange(NPC npc, ref NPC.HitModifiers modifiers)
	{
	}
}
