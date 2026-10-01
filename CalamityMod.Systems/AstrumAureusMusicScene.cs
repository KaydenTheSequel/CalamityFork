using CalamityMod.NPCs.AstrumAureus;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class AstrumAureusMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<AstrumAureus>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("AstrumAureus");

	public override int VanillaMusic => 13;

	public override int OtherworldMusic => 80;
}
