using CalamityMod.NPCs.BrimstoneElemental;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class BrimstoneElementalMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<BrimstoneElemental>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("BrimstoneElemental");

	public override int VanillaMusic => 17;

	public override int OtherworldMusic => 80;
}
