using CalamityMod.NPCs.Crabulon;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class DrunkCrabBackgroundScene : ModSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override bool IsSceneEffectActive(Player player)
	{
		if (Main.zenithWorld && NPC.AnyNPCs(ModContent.NPCType<Crabulon>()))
		{
			return true;
		}
		return false;
	}

	public override void SpecialVisuals(Player player, bool isActive)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		player.ManageSpecialBiomeVisuals("CalamityMod:DrunkCrabulon", isActive);
	}
}
