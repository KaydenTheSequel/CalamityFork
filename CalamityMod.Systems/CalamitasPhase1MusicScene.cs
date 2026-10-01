using CalamityMod.NPCs;
using CalamityMod.NPCs.SupremeCalamitas;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class CalamitasPhase1MusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override int NPCType => ModContent.NPCType<SupremeCalamitas>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("CalamitasPhase1");

	public override int VanillaMusic => 12;

	public override int OtherworldMusic => 80;

	public override bool AdditionalCheck()
	{
		return CalamityGlobalNPC.SCalGrief != -1;
	}
}
