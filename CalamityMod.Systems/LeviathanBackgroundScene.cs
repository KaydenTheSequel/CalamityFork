using CalamityMod.NPCs.Leviathan;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class LeviathanBackgroundScene : ModSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override bool IsSceneEffectActive(Player player)
	{
		if (!(Main.zenithWorld ? NPC.AnyNPCs(ModContent.NPCType<Anahita>()) : NPC.AnyNPCs(ModContent.NPCType<Leviathan>())))
		{
			return Main.LocalPlayer.Calamity().monolithLeviathanShader > 0;
		}
		return true;
	}

	public override void SpecialVisuals(Player player, bool isActive)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		player.ManageSpecialBiomeVisuals("CalamityMod:Leviathan", isActive);
	}
}
