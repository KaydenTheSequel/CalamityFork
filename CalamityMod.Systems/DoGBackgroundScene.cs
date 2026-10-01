using CalamityMod.NPCs.DevourerofGods;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class DoGBackgroundScene : ModSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override bool IsSceneEffectActive(Player player)
	{
		bool monolithIsActive = Main.LocalPlayer.Calamity().monolithDevourerBShader > 0 || Main.LocalPlayer.Calamity().monolithDevourerPShader > 0;
		return NPC.AnyNPCs(ModContent.NPCType<DevourerofGodsHead>()) | monolithIsActive;
	}

	public override void SpecialVisuals(Player player, bool isActive)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		player.ManageSpecialBiomeVisuals("CalamityMod:DevourerofGodsHead", isActive);
		if (isActive)
		{
			SkyManager.Instance.Activate("CalamityMod:DevourerofGodsHead", default(Vector2));
		}
		else
		{
			SkyManager.Instance.Deactivate("CalamityMod:DevourerofGodsHead");
		}
	}
}
