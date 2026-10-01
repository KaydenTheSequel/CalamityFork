namespace CalamityMod.Balancing;

public struct NPCBalancingChange(int npcType, params IBalancingRule[] balancingRules)
{
	public int NPCType = npcType;

	public IBalancingRule[] BalancingRules = balancingRules;
}
