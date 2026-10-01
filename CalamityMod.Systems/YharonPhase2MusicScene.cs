using CalamityMod.NPCs;
using CalamityMod.NPCs.Yharon;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class YharonPhase2MusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override int NPCType => ModContent.NPCType<Yharon>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("YharonPhase2");

	public override int VanillaMusic => 38;

	public override int OtherworldMusic => 84;

	public override bool AdditionalCheck()
	{
		return CalamityGlobalNPC.yharonP2 != -1;
	}
}
