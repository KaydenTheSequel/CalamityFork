using CalamityMod.NPCs;
using CalamityMod.NPCs.SupremeCalamitas;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class CalamitasPhase2MusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override int NPCType => ModContent.NPCType<SupremeCalamitas>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("CalamitasPhase2");

	public override int VanillaMusic => 13;

	public override int OtherworldMusic => 87;

	public override bool AdditionalCheck()
	{
		return CalamityGlobalNPC.SCalLament != -1;
	}
}
