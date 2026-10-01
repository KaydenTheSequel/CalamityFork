using CalamityMod.NPCs.Bumblebirb;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class DragonfollyMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<Dragonfolly>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("Dragonfolly");

	public override int VanillaMusic => 17;

	public override int OtherworldMusic => 80;
}
