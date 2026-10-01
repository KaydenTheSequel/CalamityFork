using CalamityMod.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class DoGPhase2DelayScene : ModSceneEffect
{
	public override int Music => CalamityMod.Instance.GetMusicFromMusicMod("DevourerofGodsPhase2") ?? 38;

	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override bool IsSceneEffectActive(Player player)
	{
		if (CalamityGlobalNPC.DoGHead < 0 || !Main.npc[CalamityGlobalNPC.DoGHead].active)
		{
			return false;
		}
		if (Main.npc[CalamityGlobalNPC.DoGHead].localAI[2] <= 530f)
		{
			return Main.npc[CalamityGlobalNPC.DoGHead].localAI[2] > 50f;
		}
		return false;
	}
}
