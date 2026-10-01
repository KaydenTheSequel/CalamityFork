using CalamityMod.NPCs;
using CalamityMod.NPCs.SupremeCalamitas;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class CalamitasPhase3MusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override int NPCType => ModContent.NPCType<SupremeCalamitas>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("CalamitasPhase3");

	public override int VanillaMusic => 38;

	public override int OtherworldMusic => 84;

	public override bool AdditionalCheck()
	{
		return CalamityGlobalNPC.SCalEpiphany != -1;
	}
}
