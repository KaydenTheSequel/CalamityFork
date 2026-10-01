using Terraria;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses;

public static class EmpressOfLightAIUtils
{
	public static int CalculateDamageForEnrage(this int damage)
	{
		if (!NPC.ShouldEmpressBeEnraged())
		{
			return damage;
		}
		return 9999;
	}
}
