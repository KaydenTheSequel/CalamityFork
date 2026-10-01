using CalamityMod.NPCs.CeaselessVoid;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class CeaselessVoidMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<CeaselessVoid>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("CeaselessVoid");

	public override int VanillaMusic => 13;

	public override int OtherworldMusic => 80;
}
