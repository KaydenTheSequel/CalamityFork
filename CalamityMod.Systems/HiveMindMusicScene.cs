using CalamityMod.NPCs.HiveMind;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class HiveMindMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<HiveMind>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("HiveMind");

	public override int VanillaMusic => 12;

	public override int OtherworldMusic => 87;
}
