using CalamityMod.NPCs.StormWeaver;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class StormWeaverBackgroundScene : ModSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override bool IsSceneEffectActive(Player player)
	{
		return NPC.AnyNPCs(ModContent.NPCType<StormWeaverHead>());
	}

	public override void SpecialVisuals(Player player, bool isActive)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (SkyManager.Instance["CalamityMod:StormWeaverFlash"] != null && isActive != SkyManager.Instance["CalamityMod:StormWeaverFlash"].IsActive())
		{
			if (isActive)
			{
				SkyManager.Instance.Activate("CalamityMod:StormWeaverFlash", player.Center);
			}
			else
			{
				SkyManager.Instance.Deactivate("CalamityMod:StormWeaverFlash");
			}
		}
	}
}
