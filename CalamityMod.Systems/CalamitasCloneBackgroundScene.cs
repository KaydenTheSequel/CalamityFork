using CalamityMod.NPCs.CalClone;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class CalamitasCloneBackgroundScene : ModSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override bool IsSceneEffectActive(Player player)
	{
		return NPC.AnyNPCs(ModContent.NPCType<CalamitasClone>());
	}

	public override void SpecialVisuals(Player player, bool isActive)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		player.ManageSpecialBiomeVisuals("CalamityMod:CalamitasRun3", isActive);
	}
}
