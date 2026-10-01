using CalamityMod.NPCs.VanillaNPCAIOverrides;
using Terraria;

namespace CalamityMod.NPCs;

public static class VanillaAIOverrideExtension
{
	extension(NPC npc)
	{
		public bool TryGetAIOverride<AI>(out AI aiInstance) where AI : VanillaAIOverride, new()
		{
			if (!npc.TryGetGlobalNPC<CalamityVanillaAIOverrideNPC>(out var aiOverrideNPC))
			{
				aiInstance = null;
				return false;
			}
			aiInstance = aiOverrideNPC.AIOverride as AI;
			return aiInstance != null;
		}
	}
}
