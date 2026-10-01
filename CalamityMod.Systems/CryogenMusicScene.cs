using CalamityMod.NPCs.Cryogen;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class CryogenMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<Cryogen>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("Cryogen");

	public override int VanillaMusic => 32;

	public override int OtherworldMusic => 77;
}
