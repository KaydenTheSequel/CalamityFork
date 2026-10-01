using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class BrimstoneCragScene : ModSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

	public override bool IsSceneEffectActive(Player player)
	{
		return player.Calamity().ZoneCalamity;
	}

	public override void SpecialVisuals(Player player, bool isActive)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		player.ManageSpecialBiomeVisuals("CalamityMod:BrimstoneCrag", isActive);
	}
}
