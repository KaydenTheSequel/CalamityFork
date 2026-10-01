using CalamityMod.NPCs.PlaguebringerGoliath;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class PBGBackgroundScene : ModSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override bool IsSceneEffectActive(Player player)
	{
		if (!NPC.AnyNPCs(ModContent.NPCType<PlaguebringerGoliath>()))
		{
			return Main.LocalPlayer.Calamity().monolithPlagueShader > 0;
		}
		return true;
	}

	public override void SpecialVisuals(Player player, bool isActive)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		player.ManageSpecialBiomeVisuals("CalamityMod:PlaguebringerGoliath", isActive);
	}
}
