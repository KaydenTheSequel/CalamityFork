using CalamityMod.NPCs.Perforator;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class PerforatorsMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<PerforatorHive>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("Perforators");

	public override int VanillaMusic => 12;

	public override int OtherworldMusic => 87;
}
