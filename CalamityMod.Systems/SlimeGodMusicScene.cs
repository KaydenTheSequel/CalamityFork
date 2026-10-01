using CalamityMod.NPCs.SlimeGod;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class SlimeGodMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<SlimeGodCore>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("SlimeGod");

	public override int VanillaMusic => 5;

	public override int OtherworldMusic => 81;
}
