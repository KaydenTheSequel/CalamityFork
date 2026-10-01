using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class AstralMonolithScene : ModSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

	public override bool IsSceneEffectActive(Player player)
	{
		return Main.LocalPlayer.Calamity().monolithAstralShader > 0;
	}

	public override void SpecialVisuals(Player player, bool isActive)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		player.ManageSpecialBiomeVisuals("CalamityMod:Astral", isActive);
	}
}
