using CalamityMod.NPCs.ProfanedGuardians;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class ProfanedGuardiansMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<ProfanedGuardianCommander>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("ProfanedGuardians");

	public override int VanillaMusic => 5;

	public override int OtherworldMusic => 81;
}
