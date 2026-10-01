using CalamityMod.Skies;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class ExoMechBackgroundScene : ModSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override bool IsSceneEffectActive(Player player)
	{
		if (!ExoMechsSky.CanSkyBeActive)
		{
			return player.Calamity().monolithExoShader > 0;
		}
		return true;
	}

	public override void SpecialVisuals(Player player, bool isActive)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		player.ManageSpecialBiomeVisuals("CalamityMod:ExoMechs", isActive);
		if (isActive)
		{
			SkyManager.Instance.Activate("CalamityMod:ExoMechs", player.Center);
		}
		else
		{
			SkyManager.Instance.Deactivate("CalamityMod:ExoMechs");
		}
	}
}
