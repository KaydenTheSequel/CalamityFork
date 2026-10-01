using Terraria;

namespace CalamityMod.Balancing;

public interface IBalancingRule
{
	bool AppliesTo(NPC npc, NPC.HitModifiers modifiers, Projectile? projectile);

	void ApplyBalancingChange(NPC npc, ref NPC.HitModifiers modifiers);
}
