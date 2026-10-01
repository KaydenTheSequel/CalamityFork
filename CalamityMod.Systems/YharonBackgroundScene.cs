using CalamityMod.NPCs.Yharon;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class YharonBackgroundScene : ModSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override bool IsSceneEffectActive(Player player)
	{
		if (!NPC.AnyNPCs(ModContent.NPCType<Yharon>()))
		{
			return Main.LocalPlayer.Calamity().monolithYharonShader > 0;
		}
		return true;
	}

	public override void SpecialVisuals(Player player, bool isActive)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		player.ManageSpecialBiomeVisuals("CalamityMod:Yharon", isActive);
	}
}
